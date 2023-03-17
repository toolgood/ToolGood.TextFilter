/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ToolGood.TextFilter.App.Datas.Results;
using ToolGood.TextFilter.App.Datas.TextFilters;
using ToolGood.TextFilter.Commons;

namespace ToolGood.TextFilter.Application
{
    static class TextFilterHelper
    {
        private const string _skipList = " 　\u00A0\t\r\n~!@#$%^&*()_+-=【】、[]{}|;':\"，。、《》？。，、；：？！…—·ˉ¨‘’“”～‖∶＂＇｀｜〃〔〕〈〉《》「」『』．〖〗【】（）［］｛｝≈≡≠＝≤≥＜＞≮≯∷±＋－×÷／∫∮∝∞∧∨∑∏∪∩∈∵∴⊥∥∠⌒⊙≌∽√§☆★○●◎◇◆□℃‰€■△▲※→←↑↓〓¤°＃＆＠＼︿＿￣―┌┍┎┐┑┒┓─┄┈├┝┞┟┠┡┢┣│┆┊┬┭┮┯┰┱┲┳┼┽┾┿╀╁╂╃└┕┖┗┘┙┚┛━┅┉┤┥┦┧┨┩┪┫┃┇┋┴┵┶┷┸┹┺┻╋╊╉╈╇╆╅╄";
        private static bool[] _skipBitArray;
        static TextFilterHelper()
        {
            _skipBitArray = new bool[0x10000];
            for (int i = 0; i < _skipList.Length; i++) {
                _skipBitArray[_skipList[i]] = true;
            }
        }
 
        #region 查找所有敏感词
        public unsafe static List<TempWordsResultItem> FindIllegalWords(in char[] txt)
        {
            List<TempWordsResultItem> illegalWordsResults = new List<TempWordsResultItem>();
            var len = txt.Length;
            fixed (char* _ptext = &txt[0]) {
                if (MemoryCache.Instance.UseBig) {
                    if (len < 5000) {
                        MemoryCache.Instance.KeywordSearch_34.FindAll(_ptext, in len, illegalWordsResults);// 2
                    } else {
                        MemoryCache.Instance.BigKeywordSearch_34.FindAll(_ptext, in len, illegalWordsResults);//  这个是联系方式为主的
                        MemoryCache.Instance.BigACTextFilterSearch_34.FindAll(_ptext, in len, illegalWordsResults);// 2
                    }
                } else {
                    MemoryCache.Instance.KeywordSearch_34.FindAll(_ptext, in len, illegalWordsResults);// 2
                }
                MemoryCache.Instance.KeywordSearch_012.FindAll(_ptext, in len, illegalWordsResults);// 1
      
                MemoryCache.Instance.FenciSearch.FindAll(_ptext, in len, illegalWordsResults); //  5
            }
            return illegalWordsResults;
        }


        #endregion

        #region 生成文本分割
        /// <summary>
        /// 生成文本分割
        /// </summary>
        /// <param name="text"></param>
        /// <param name="illegalWordsResults"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TextSplit4 BuildTextSplit(in char[] txt, in List<TempWordsResultItem> illegalWordsResults, in int keyword34_start_index)
        {
            TextSplit4 line = new TextSplit4(txt.Length);
            for (int i = 0; i < illegalWordsResults.Count; i++) {
                line.AddWords(illegalWordsResults[i], keyword34_start_index);
            }
            var contactDict = MemoryCache.Instance.ContactSearch.GetContactDict();
            line.RemoveMaxLengthContact(contactDict, txt);

            line.Calculation(txt, _skipBitArray);
            return line;
        }
        #endregion

        #region 分析 单组敏感词
        /// <summary>
        /// 分析 单组敏感词
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="result"></param>
        /// <param name="items"></param>
        public static void AnalysisSingleWordsResult(in char[] txt, IllegalWordsFindAllResult result, in List<TempWordsResultItem> items)
        {
            HashSet<uint> passSet = new HashSet<uint>();
            HashSet<uint> reviewSet = new HashSet<uint>();

            #region 优先采用自定义 1）加 违规 ；2）加 触线 ；3）加 正常
            for (int i = 0; i < items.Count; i++) {
                var item = items[i];
                if (item.DiyIndex == 0) { continue; }

                if (item.RiskLevel == IllegalWordsRiskLevel.Reject) {
                    result.RejectSingleItems.Add(new SingleWordsResult(item.Start, item.End, item.DiyIndex));
                } else if (item.RiskLevel == IllegalWordsRiskLevel.Review) {
                    result.ReviewSingleItems.Add(new SingleWordsResult(item.Start, item.End, item.DiyIndex));
                    reviewSet.Add(item.GetPostion());
                } else if (item.RiskLevel == IllegalWordsRiskLevel.Pass) {
                    passSet.Add(item.GetPostion());
                }
            }
            #endregion

            List<TempWordsResultItem> reviewDict = new List<TempWordsResultItem>();
            List<TempWordsResultItem> rejectDict = new List<TempWordsResultItem>();

            var memoryCache = MemoryCache.Instance;
            #region 内置敏感词 1）分 违规、触线
            for (int i = 0; i < items.Count; i++) {
                var item = items[i];
                if (item.DiyIndex > 0) { continue; }
                if (item.SrcRiskLevel == IllegalWordsSrcRiskLevel.Part) { continue; }

                if (MatchText(txt, item)) {
                    var riskLevel = memoryCache.GetRiskLevel(item.TypeId, item.SrcRiskLevel.Value);
                    if (riskLevel == IllegalWordsRiskLevel.Reject) {
                        rejectDict.Add(item);
                    } else if (riskLevel == IllegalWordsRiskLevel.Review) {
                        reviewDict.Add(item);
                    } else if (riskLevel == IllegalWordsRiskLevel.Pass) {
                        passSet.Add(item.GetPostion());
                    }
                }
            }
            #endregion

            var types = memoryCache.KeywordTypes;
            #region 内置敏感词 2）先加触线
            foreach (var review in reviewDict) {
                var postions = review.GetPostion();
                if (passSet.Contains(postions)) {
                    continue;
                } else if (reviewSet.Add(postions)) {
                    result.ReviewSingleItems.Add(new SingleWordsResult(review.TypeId, review.Start, review.End, types[review.TypeId].Code, review.SingleIndex));
                }
            }
            #endregion
            reviewDict = null;

            HashSet<uint> rejectSet = new HashSet<uint>();
            #region 内置敏感词 3）加 违规
            foreach (var reject in rejectDict) {
                var postions = reject.GetPostion();
                if (passSet.Contains(postions)) {
                    continue;
                } else if (reviewSet.Contains(postions)) {
                    continue;
                } else if (rejectSet.Add(postions)) {
                    result.RejectSingleItems.Add(new SingleWordsResult(reject.TypeId, reject.Start, reject.End, types[reject.TypeId].Code, reject.SingleIndex));
                }
            }
            #endregion

            passSet = null;
            reviewSet = null;
            rejectSet = null;
            rejectDict = null;
        }

        #endregion

        #region 分析 多组敏感词

        /// <summary>
        /// 分析 多组敏感词
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="result"></param>
        /// <param name="multiWords"></param>
        /// <param name="src"></param>
        public static void AnalysisMultiWordsResult(in char[] txt, IllegalWordsFindAllResult result, in List<TempMultiWordsResult> multiWords)
        {
            var keywordInfos = MemoryCache.Instance.KeywordInfos;
            var keywordTypes = MemoryCache.Instance.KeywordTypes;

            for (int idx = 0; idx < multiWords.Count; idx++) {
                var multi = multiWords[idx];
                var mutliKeyword = keywordInfos[multi.ResultIndex];

                if (MatchText(txt, multi, mutliKeyword)) {
                    var srcRiskLevel = mutliKeyword.GetRiskLevel();
                    var riskLevel = MemoryCache.Instance.GetRiskLevel(mutliKeyword.TypeId, srcRiskLevel);
                    if (riskLevel == IllegalWordsRiskLevel.Pass) { continue; }

                    MultiWordsResultItem[] items = new MultiWordsResultItem[multi.KeywordIndexs.Length];
                    for (int i = 0; i < items.Length; i++) {
                        var item = multi.KeywordIndexs[i];
                        items[i] = new MultiWordsResultItem(item.Start, item.End);
                    }
                    var r = new MultiWordsResult(multi.ResultIndex, mutliKeyword.TypeId, keywordTypes[mutliKeyword.TypeId].Code, items);

                    if (riskLevel == IllegalWordsRiskLevel.Review) {
                        result.ReviewMultiItems.Add(r);
                    } else {
                        result.RejectMultiItems.Add(r);
                    }
                }

            }
        }

        #endregion

        #region FindAll
        /// <summary>
        /// 查找所有敏感词
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public unsafe static IllegalWordsFindAllResult FindAll(in ReadStreamBase stream)
        {
            var illegalWords1 = FindIllegalWords(in stream.TestingText);
            foreach (var item in illegalWords1) {
                item.Start = stream.Start[item.Start];
                item.End = stream.End[item.End];
            }
            var textSplit = BuildTextSplit(stream.Source, illegalWords1, MemoryCache.Instance.Keyword_34_Index_Start);
            var fenciwords = textSplit.GetWordsContext();
            textSplit.SetNplIndex(fenciwords);
            var illegalWords2 = textSplit.GetIllegalWords();
            var illegalWords3 = textSplit.GetIllegalWords2();
            textSplit = null;
            illegalWords1 = null;
            if (illegalWords2.Count == 0 && illegalWords3.Count == 0) {
                IllegalWordsFindAllResult illegalWordsFindAllResult = new IllegalWordsFindAllResult();
                illegalWordsFindAllResult.SentimentScore = CalcEmotionScore(fenciwords);
                illegalWords3 = null;
                illegalWords2 = null;
                fenciwords = null;
                return illegalWordsFindAllResult;
            }

            IllegalWordsFindAllResult result = new IllegalWordsFindAllResult();
            AnalysisSingleWordsResult(stream.Source, result, illegalWords2);
            var items = MemoryCache.Instance.MultiWordsSearch.FindAll(illegalWords3);
            AnalysisMultiWordsResult(stream.Source, result, items);
            result.ContactItems = MemoryCache.Instance.ContactSearch.FindAll(illegalWords3);

            items = null;
            illegalWords2 = null;
            illegalWords3 = null;

            result.SentimentScore = CalcEmotionScore(fenciwords);
            if (result.RejectSingleItems.Count > 0 || result.RejectMultiItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Reject;
                result.Code = GetCode(result.RejectSingleItems, result.RejectMultiItems);
            } else if (result.ReviewSingleItems.Count > 0 || result.ReviewMultiItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Review;
                result.Code = GetCode(result.ReviewSingleItems, result.ReviewMultiItems);
            } else if (result.ContactItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Review;
                result.Code = "Contact";
            }
            fenciwords = null;

            return result;
        }

        #region  CalcEmotionScore

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CalcEmotionScore(in List<TempWordsResultItem> keywordInfos)
        {
            double emotionScore = 0.0;
            double preEmotionScore = 1.0;

            var index = 0;
            while (index < keywordInfos.Count) {
                var info = keywordInfos[index++];
                if (info.EmotionalColor == 0) { continue; }
                if (info.EmotionalColor >= 10) {
                    preEmotionScore = preEmotionScore * info.GetEmotionScore();
                    emotionScore += preEmotionScore;
                    preEmotionScore = 1.0;
                    continue;
                }
                preEmotionScore = preEmotionScore * info.GetEmotionScore();
            }
            return emotionScore;
        }

        #endregion
        #endregion


        #region Replace

        public unsafe static IllegalWordsReplaceResult Replace(in ReadStreamBase stream, in char replaceChar, in bool reviewReplace, in bool contactReplace)
        {
            var temp = FindAll_Replace(stream, contactReplace);
            //    return Replace(stream, temp, replaceChar, reviewReplace, contactReplace);
            //}
            //public unsafe static IllegalWordsReplaceResult Replace(in ReadStreamBase stream, in IllegalWordsFindAllResult temp, in char replaceChar, in bool reviewReplace, in bool contactReplace)
            //{
            IllegalWordsReplaceResult result = new IllegalWordsReplaceResult();
            if (temp.RejectSingleItems.Count > 0 || temp.RejectMultiItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Reject;
                byte[] bytes = new byte[stream.Source.Length];
                fixed (byte* _pbytes = &bytes[0]) {
                    FindReplacePostion(stream, _pbytes, temp.RejectSingleItems, temp.RejectMultiItems);
                    if (reviewReplace && temp.ReviewSingleItems.Count > 0 || temp.ReviewMultiItems.Count > 0) {
                        FindReplacePostion(stream, _pbytes, temp.ReviewSingleItems, temp.ReviewMultiItems);
                    }
                    if (contactReplace && temp.ContactItems.Count > 0) {
                        FindReplacePostion(stream, _pbytes, temp.ContactItems);
                    }
                    result.Result = Replace(stream, _pbytes, replaceChar, stream.Source.Length);
                }
                bytes = null;
            } else if (reviewReplace) {
                if (temp.ReviewSingleItems.Count > 0 || temp.ReviewMultiItems.Count > 0) {
                    result.RiskLevel = IllegalWordsRiskLevel.Reject;
                    byte[] bytes = new byte[stream.Source.Length];
                    fixed (byte* _pbytes = &bytes[0]) {
                        FindReplacePostion(stream, _pbytes, temp.ReviewSingleItems, temp.ReviewMultiItems);
                        if (contactReplace && temp.ContactItems.Count > 0) {
                            FindReplacePostion(stream, _pbytes, temp.ContactItems);
                        }
                        result.Result = Replace(stream, _pbytes, replaceChar, stream.Source.Length);
                    }
                    bytes = null;
                } else if (contactReplace && temp.ContactItems.Count > 0) {
                    result.RiskLevel = IllegalWordsRiskLevel.Reject;
                    byte[] bytes = new byte[stream.Source.Length];
                    fixed (byte* _pbytes = &bytes[0]) {
                        FindReplacePostion(stream, _pbytes, temp.ContactItems);
                        result.Result = Replace(stream, _pbytes, replaceChar, stream.Source.Length);
                    }
                    bytes = null;
                }
            } else if (contactReplace && temp.ContactItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Reject;
                byte[] bytes = new byte[stream.Source.Length];
                fixed (byte* _pbytes = &bytes[0]) {
                    FindReplacePostion(stream, _pbytes, temp.ContactItems);
                    result.Result = Replace(stream, _pbytes, replaceChar, stream.Source.Length);
                }
                bytes = null;
            } else if (temp.ReviewSingleItems.Count > 0 || temp.ReviewMultiItems.Count > 0) {
                result.RiskLevel = IllegalWordsRiskLevel.Review;
                result.ReviewSingleItems = temp.ReviewSingleItems;
                result.ReviewMultiItems = temp.ReviewMultiItems;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static string Replace(in ReadStreamBase stream, in byte* _pbytes, in char replaceChar, in int length)
        {
            var text = stream.Source;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++) {
                var b = _pbytes[i];
                if (b == 0) {
                    stringBuilder.Append(text[i]);
                } else if (b == 1) {
                    stringBuilder.Append(replaceChar);
                }
            }
            return stringBuilder.ToString();
        }

        #region FindReplacePostion
        internal unsafe static void FindReplacePostion(in ReadStreamBase stream, in byte* _pbytes, in List<SingleWordsResult> singles
            , in List<MultiWordsResult> multis)
        {
            for (int idx = 0; idx < singles.Count; idx++) {
                var item = singles[idx];
                if (item.TypeId == 2) {
                    var start = stream.Start[item.Start];
                    var end = stream.Start[item.End];
                    for (int j = start; j <= end; j++) {
                        _pbytes[j] = 2;
                    }
                } else {
                    for (int j = item.Start; j <= item.End; j++) {
                        if (_skipBitArray[stream.TestingText[j]] == false) {//跳過符號
                            var start = stream.Start[j];
                            var end = stream.End[j];
                            _pbytes[start] = 1;
                            for (int k = start + 1; k <= end; k++) {
                                _pbytes[k] = 2;
                            }
                        }
                    }
                }
            }
            for (int idx = 0; idx < multis.Count; idx++) {
                var multi = multis[idx];
                for (int i = 0; i < multi.Items.Length; i++) {
                    var item = multi.Items[i];
                    for (int j = item.Start; j <= item.End; j++) {
                        if (_skipBitArray[stream.TestingText[j]] == false) {//跳過符號
                            var start = stream.Start[j];
                            var end = stream.End[j];
                            _pbytes[start] = 1;
                            for (int k = start + 1; k <= end; k++) {
                                _pbytes[k] = 2;
                            }
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static void FindReplacePostion(in ReadStreamBase stream, in byte* _pbytes, in List<ContactResult> singles)
        {
            for (int idx = 0; idx < singles.Count; idx++) {
                var item = singles[idx];
                var start = stream.Start[item.Start];
                var end = stream.Start[item.End];
                for (int j = start; j <= end; j++) {
                    _pbytes[j] = 1;
                }
            }
        }

        #endregion

        internal static IllegalWordsFindAllResult FindAll_Replace(in ReadStreamBase stream, in bool contactReplace)
        {
            var illegalWords1 = FindIllegalWords(in stream.TestingText);
            var textSplit = BuildTextSplit(stream.TestingText, illegalWords1, MemoryCache.Instance.Keyword_34_Index_Start);
            var fenciwords = textSplit.GetWordsContext();
            textSplit.SetNplIndex(fenciwords);
            var illegalWords2 = textSplit.GetIllegalWords();
            var illegalWords3 = textSplit.GetIllegalWords2();
            textSplit = null;
            fenciwords = null;
            illegalWords1 = null;

            if (illegalWords2.Count == 0 && illegalWords3.Count == 0) {
                illegalWords2 = null;
                illegalWords3 = null;
                return new IllegalWordsFindAllResult();
            }

            IllegalWordsFindAllResult result = new IllegalWordsFindAllResult();
            AnalysisSingleWordsResult(stream.TestingText, result, illegalWords2);
            var items = MemoryCache.Instance.MultiWordsSearch.FindAll(illegalWords3);
            AnalysisMultiWordsResult(stream.TestingText, result, items);
            if (contactReplace) {
                result.ContactItems = MemoryCache.Instance.ContactSearch.FindAll(illegalWords3);
            }
            items = null;

            illegalWords2 = null;
            illegalWords3 = null;
            return result;
        }

        #endregion

        #region 获取 风险等级
        /// <summary>
        /// 获取 风险等级
        /// </summary>
        /// <param name="singles"></param>
        /// <param name="multis"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetCode(in List<SingleWordsResult> singles, in List<MultiWordsResult> multis)
        {
            var min = int.MaxValue;
            string code = "";
            for (int i = 0; i < singles.Count; i++) {
                var singleWordsResultItem = singles[i];
                if (singleWordsResultItem.TypeId < min) {
                    min = singleWordsResultItem.TypeId;
                    code = singleWordsResultItem.Code;
                }
            }
            for (int i = 0; i < multis.Count; i++) {
                var multiIllegalWordsResult = multis[i];
                if (multiIllegalWordsResult.TypeId < min) {
                    min = multiIllegalWordsResult.TypeId;
                    code = multiIllegalWordsResult.Code;
                }
            }
            return code;
        }
        #endregion

        #region private MatchText

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool MatchText(in char[] txt, in TempMultiWordsResult result, in KeywordInfo item)
        {
            var matchType = item.GetMatchType();
            if (matchType == IllegalWordsMatchType.PartMatch) {
                return true;
            } else if (matchType == IllegalWordsMatchType.MatchTextStart) {
                return IsSymbolStart(in txt, in result.KeywordIndexs[0].Start);
            } else if (matchType == IllegalWordsMatchType.MatchTextEnd) {
                var end = result.KeywordIndexs[result.KeywordIndexs.Length - 1].End;
                return IsSymbolEnd(in txt, in end);
            } else if (matchType == IllegalWordsMatchType.MatchTextStartOrEnd) {
                if (IsSymbolStart(in txt, in result.KeywordIndexs[0].Start)) {
                    return true;
                }
                var end = result.KeywordIndexs[result.KeywordIndexs.Length - 1].End;
                return IsSymbolEnd(in txt, in end);
            } else {
                var start = result.KeywordIndexs[0].Start;
                var end = result.KeywordIndexs[result.KeywordIndexs.Length - 1].End;
                return IsSymbolStart(in txt, in start) && IsSymbolEnd(in txt, in end);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool MatchText(in char[] txt, in TempWordsResultItem item)
        {
            if (item.MatchType == IllegalWordsMatchType.PartMatch) {
                return true;
            } else if (item.MatchType == IllegalWordsMatchType.MatchTextStart) {
                return IsSymbolStart(in txt, in item.Start);
            } else if (item.MatchType == IllegalWordsMatchType.MatchTextEnd) {
                return IsSymbolEnd(in txt, in item.End);
            } else if (item.MatchType == IllegalWordsMatchType.MatchTextStartOrEnd) {
                if (IsSymbolStart(in txt, in item.Start)) { return true; }
                return IsSymbolEnd(txt, in item.End);
            }
            return IsSymbolStart(in txt, in item.Start) && IsSymbolEnd(txt, in item.End);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSymbolStart(in char[] txt, in int start)
        {
            if (start == 0) { return true; }
            return MemoryCache.Instance.TxtEndChars[txt[start - 1]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSymbolEnd(in char[] txt, in int end)
        {
            if (end == txt.Length - 1) { return true; }
            return MemoryCache.Instance.TxtEndChars[txt[end + 1]];
        }

        #endregion



    }
}
