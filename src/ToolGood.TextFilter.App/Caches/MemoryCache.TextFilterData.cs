/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToolGood.RcxCrypto;
using ToolGood.TextFilter.App.Commons;
using ToolGood.TextFilter.App.Datas.TextFilters;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Commons
{
    public partial class MemoryCache
    {
        public bool LoadTextFilterSuccess { get; private set; }

        #region 基础数据
        /// <summary>
        /// 分词 Keywords
        /// </summary> 
        public FenciKeywordInfo[] FenciKeywords { get; private set; }
        /// <summary>
        /// 敏感词类型
        /// </summary>
        public KeywordTypeInfo[] KeywordTypeInfos { get; private set; }
        /// <summary>
        /// 单组敏感词
        /// </summary>
        public KeywordInfo[] KeywordInfos { get; private set; }


        /// <summary>
        ///  结尾符号
        /// </summary>
        public bool[] TxtEndChars { get; private set; }

        public int Keyword_34_Index_Start { get; private set; }
        #endregion

        #region 内置搜索 

        /// <summary>
        /// 转义
        /// </summary>
        public ITranslateSearch TranslateSearch { get; private set; }

        /// <summary>
        /// 分词
        /// </summary>
        public IFenciSearch FenciSearch { get; private set; }

        /// <summary>
        /// 使用 大量文本优化 
        /// </summary>
        public bool UseBig { get; private set; }

        /// <summary>
        /// 敏感词搜索，正常，触线、危险
        /// </summary>
        public IKeywordsSearch KeywordSearch_012 { get; private set; }

        /// <summary>
        /// 敏感词搜索 违规  小量文本
        /// </summary>
        public IACRegexSearch KeywordSearch_34 { get; private set; }

        /// <summary>
        /// 敏感词搜索 违规  大量文本
        /// </summary>
        public IACRegexSearch BigACTextFilterSearch_34 { get; private set; }

        /// <summary>
        /// 敏感词搜索 违规  大量文本
        /// </summary>
        public IACRegexSearch BigKeywordSearch_34 { get; private set; }


        /// <summary>
        /// 多组 敏感词
        /// </summary>
        public IMultiWordsSearch MultiWordsSearch { get; private set; }

        /// <summary>
        /// 联系方式 敏感词
        /// </summary>
        public IContactSearch ContactSearch { get; private set; }

        #endregion

        /// <summary>
        /// 自定义 类型
        /// </summary>
        public CustomKeywordType[] KeywordTypes { get; private set; }

        #region TextFilterData_Dispose
        private void TextFilterData_Dispose()
        {
            // 基础数据
            FenciKeywords = null;
            KeywordTypeInfos = null;
            KeywordInfos = null;
            TxtEndChars = null;

            // 内置数据
            TranslateSearch = null;
            FenciSearch = null;
            KeywordSearch_012 = null;
            KeywordSearch_34 = null;
            BigACTextFilterSearch_34 = null;
            BigKeywordSearch_34 = null;
            MultiWordsSearch = null;
            ContactSearch = null;

            KeywordTypes = null;

        }
        #endregion



        #region InitTextFilter
        private void InitTextFilter(LicenceInfo info)
        {
            if (LoadTextFilter(info)) {
                LoadDatabase();
            }
        }

        #region LoadTextFilter

        private bool LoadTextFilter(LicenceInfo info)
        {
            var files = Directory.GetFiles(AppContext.BaseDirectory, "TextFilter-*.data").OrderByDescending(q => q).ToList();

            LoadTextFilterSuccess = false;
            var errorMessages = "";

            foreach (var filePath in files) {
                string errorMessage;
                if (LoadTextFilter(filePath, info, out errorMessage)) {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> Load text filter file: {Path.GetFileName(filePath)}");
                    return true;
                }
                if (errorMessages.Length > 0) {
                    errorMessages += "\r\n";
                }
                errorMessages += $"[{filePath}]{errorMessage}";
            }
            if (files.Count == 0) {
                errorMessages = "未找到敏感词数据文件！";
            }

            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> Load text filter file error: {errorMessages}");
            return false;
        }

        private bool LoadTextFilter(string filePath, LicenceInfo info, out string errorMessage)
        {
            if (File.Exists(filePath) == false) {
                errorMessage = "数据文件不存在！";
                return false;
            }
            var bytes = File.ReadAllBytes(filePath);
            string pwd = "012345679";
            Rcy.Encrypt(bytes, pwd);

            MemoryStream ms = null;
            BinaryReader br = null;
            try {
                ms = CompressionUtil.GzipDecompress(bytes, 0);
                br = new BinaryReader(ms);

                #region CheckPhone
                var phone = br.ReadString();
                if (string.IsNullOrEmpty(phone) == false) {
                    if (info.Phone != phone) {
                        errorMessage = "数据文件异常！";
                        return false;
                    }
                }
                #endregion
                #region check version
                var version = br.ReadString();
                if (version != Version) {
                    errorMessage = "当前程序不支持版本数据，请升级！！！";
                    return false;
                }
                #endregion
                #region check year month day
                var year = br.ReadInt32();
                var month = br.ReadInt32();
                var day = br.ReadInt32();
                //if (LicenceInfo.ServiceEnd < new DateTime(year, month, day)) {
                //    errorMessage = "许可已过期，请续费！！！";
                //    return false;
                //}
                #endregion

                return LoadTextFilter(br, out errorMessage);
            } catch (Exception ex) {
                errorMessage = ex.Message;
                return false;
            } finally {
                if (br != null) { br.Close(); br = null; }
                if (ms != null) { ms.Close(); ms = null; }
                bytes = null;
            }
        }

        private bool LoadTextFilter(BinaryReader br, out string errorMessage)
        {
            // 解密
            var fenciKeywordInfos = FenciKeywordInfo.ReadList(br);
            var keywordTypeInfos = KeywordTypeInfo.ReadList(br);
            var keywordInfos = KeywordInfo.ReadList(br);
            var length = br.ReadInt32();
            var chs = br.ReadChars(length);
            var txtEndChars = new bool[0x10000];
            foreach (var item in chs) {
                txtEndChars[item] = true;
            }
            var keyword_34_Index_Start = br.ReadInt32();


            ITranslateSearch translateSearch = new TranslateSearch5();
            translateSearch.Load(br);

            IFenciSearch fenciSearch = new FenciSearch3();
            fenciSearch.Load(br);
            fenciSearch.Set_GetMatchKeyword((i) => { return fenciKeywordInfos[i]; });

            IKeywordsSearch keywordSearch_012 = new KeywordsSearch2();
            keywordSearch_012.Load(br);
            keywordSearch_012.Set_GetMatchKeyword((i) => { return keywordInfos[i]; });


            length = br.ReadInt32();
            var skipIndexs = br.ReadBytes(length);
            var useSkipOnce = br.ReadBoolArray();

            length = br.ReadInt32();
            var dicts1 = new ushort[length][];
            var dicts2 = new ushort[length][];
            for (int i = 0; i < length; i++) {
                dicts1[i] = br.ReadUshortArray();
                dicts2[i] = br.ReadUshortArray();
            }
            length = br.ReadInt32();
            ISkipwordsSearch[] skipwordsSearchs = new ISkipwordsSearch[length];
            for (int i = 0; i < length; i++) {
                var has = br.ReadByte();
                if (has == (byte)1) {
                    ISkipwordsSearch skipwordsSearch = new SkipwordsSearch();
                    skipwordsSearch.Load(br);
                    skipwordsSearchs[i] = skipwordsSearch;
                }
            }

            var dicts = br.ReadUshortArray();
            var d_34 = Build(dicts, dicts1, dicts2);
            IACRegexSearch keywordSearch_34 = new ACRegexSearch7();
            keywordSearch_34.Load(br);
            keywordSearch_34.SetDict(skipIndexs, d_34, skipwordsSearchs, useSkipOnce);
            keywordSearch_34.Set_GetMatchKeyword((i) => { return keywordInfos[i]; });



            var useBig = br.ReadBoolean();
            IACRegexSearch bigSearch_1 = null;
            IACRegexSearch bigSearch_2 = null;
            if (useBig) {
                dicts = br.ReadUshortArray();
                var d_34_big_1 = Build(dicts, dicts1, dicts2);
                bigSearch_1 = new ACTextFilterSearch();
                bigSearch_1.Load(br);
                bigSearch_1.SetDict(skipIndexs, d_34_big_1, skipwordsSearchs, useSkipOnce);
                bigSearch_1.Set_GetMatchKeyword((i) => { return keywordInfos[i]; });

                dicts = br.ReadUshortArray();
                var d_34_big_2 = Build(dicts, dicts1, dicts2);
                bigSearch_2 = new ACRegexSearch7();
                bigSearch_2.Load(br);
                bigSearch_2.SetDict(skipIndexs, d_34_big_2, skipwordsSearchs, useSkipOnce);
                bigSearch_2.Set_GetMatchKeyword((i) => { return keywordInfos[i]; });
            }
            dicts1 = null;
            dicts2 = null;

            IMultiWordsSearch multiWordsSearch = new MultiWordsSearch4();
            multiWordsSearch.Load(br);

            IContactSearch contactSearch = new ContactSearch2();
            contactSearch.Load(br);

            // 基础数据
            FenciKeywords = fenciKeywordInfos;
            KeywordTypeInfos = keywordTypeInfos;
            KeywordInfos = keywordInfos;
            TxtEndChars = txtEndChars;
            Keyword_34_Index_Start = keyword_34_Index_Start;

            // 内置数据
            TranslateSearch = translateSearch;
            FenciSearch = fenciSearch;
            KeywordSearch_012 = keywordSearch_012;
            KeywordSearch_34 = keywordSearch_34;
            UseBig = useBig;
            BigACTextFilterSearch_34 = bigSearch_1;
            BigKeywordSearch_34 = bigSearch_2;
            MultiWordsSearch = multiWordsSearch;
            ContactSearch = contactSearch;


            // 返回成功
            LoadTextFilterSuccess = true;
            errorMessage = null;
            return true;
        }
        private ushort[][] Build(ushort[] old, ushort[][] dicts1, ushort[][] dicts2)
        {
            ushort[][] result = new ushort[dicts1.Length][];
            for (int i = 0; i < dicts1.Length; i++) {
                var n = new ushort[old.Length];
                Array.Copy(old, n, old.Length);
                var d1 = dicts1[i];
                var d2 = dicts2[i];

                foreach (var item in d1) {
                    n[item] = 0xffff;
                }
                foreach (var item in d2) {
                    n[item] = 0xfffe;
                }
                result[i] = n;
            }
            return result;
        }


        #endregion

        #region LoadDatabase
        private void LoadDatabase()
        {
            try {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var types = helper.Select<DbKeywordType>();

                    var keywordTypes = CustomKeywordType.Build(KeywordTypeInfos, types);
                    var skipwordSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='skipword'");

                    KeywordTypes = keywordTypes;
                }
            } catch (Exception ex) {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> Load database error: {ex.Message}");
            }
        }
        #endregion


        #endregion


        #region GetRiskLevel
        public IllegalWordsRiskLevel GetRiskLevel(int typeId, IllegalWordsSrcRiskLevel srcRiskLevel)
        {
            if (typeId == 0) {
                return IllegalWordsRiskLevel.Pass;
            }
            if (srcRiskLevel == IllegalWordsSrcRiskLevel.Normal) { return IllegalWordsRiskLevel.Pass; }

            //var types=  KeywordTypes[typeId];
            if (KeywordTypes == null || KeywordTypes.Length <= typeId) {
                if (srcRiskLevel == IllegalWordsSrcRiskLevel.Sensitive) {
                    return IllegalWordsRiskLevel.Review;
                }
                return IllegalWordsRiskLevel.Reject;
            }
            var type = KeywordTypes[typeId];
            #region 判断时间 不在时间内 pass
            if (type.UseTime) {
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                if (type.StartTime != null) {
                    if (type.EndTime != null) {
                        if (type.StartTime <= type.EndTime) {
                            if (month < type.StartTime.Value.Month || (month == type.StartTime.Value.Month && day < type.StartTime.Value.Day)) {
                                return IllegalWordsRiskLevel.Pass;
                            } else if (month > type.EndTime.Value.Month || (month == type.EndTime.Value.Month && day > type.EndTime.Value.Day)) {
                                return IllegalWordsRiskLevel.Pass;
                            }
                        } else if (
                            (month < type.StartTime.Value.Month || (month == type.StartTime.Value.Month && day < type.StartTime.Value.Day))
                            && (month > type.EndTime.Value.Month || (month == type.EndTime.Value.Month && day > type.EndTime.Value.Day))
                            ) {
                            return IllegalWordsRiskLevel.Pass;
                        }
                    } else if (month < type.StartTime.Value.Month || (month == type.StartTime.Value.Month && day < type.StartTime.Value.Day)) {
                        return IllegalWordsRiskLevel.Pass;
                    }
                } else if (type.EndTime != null) {
                    if (month > type.EndTime.Value.Month || (month == type.EndTime.Value.Month && day > type.EndTime.Value.Day)) {
                        return IllegalWordsRiskLevel.Pass;
                    }
                }
            }
            #endregion

            if (srcRiskLevel == IllegalWordsSrcRiskLevel.Sensitive) {
                return type.RiskLevel_1 ?? IllegalWordsRiskLevel.Review;
            }
            if (srcRiskLevel == IllegalWordsSrcRiskLevel.Dangerous) {
                return type.RiskLevel_2 ?? IllegalWordsRiskLevel.Reject;
            }
            return type.RiskLevel_3 ?? IllegalWordsRiskLevel.Reject;
        }
        #endregion




    }
}
