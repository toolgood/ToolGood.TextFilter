/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    public interface IKeywordsSearch 
    {
        void Set_GetMatchKeyword(Func<int, KeywordInfo> func);

        unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result);

        void Load(BinaryReader br);

    }
    class KeywordsSearch2 : IKeywordsSearch
    {
        private ushort _firstMaxChar; 
        private ushort _azNumMinChar;//_azNumMinChar和_azNumMaxChar 两值是两个特殊值， 缩减 IllegalWordsSearch类中判断是否为英文数字代码
        private ushort _azNumMaxChar; // 详见 CheckNextChar  CheckPreChar
        private ushort[] _dict; // 0 没有该值 1) 跳词, 2+)转成对应key

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _check;
        private Int32[] _failure; // 在 AC自动机内也有_failure，都是未找到匹配后，使用_failure来重新查找，AC自动机的_failure指向对象，而这里的_failure指向地址索引
        private Int32[][] _guides;

        #region GetMatchKeyword Set_GetMatchKeyword
        private Func<int, KeywordInfo> _getKeyword;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeywordInfo GetMatchKeyword(int resultIndex)
        {
            return _getKeyword(resultIndex);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set_GetMatchKeyword(Func<int, KeywordInfo> func)
        {
            _getKeyword = func;
        }
        #endregion

        #region unsafe FindAll
        public unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result)
        {
            fixed (ushort* _pdict = &_dict[0])// ★ 使用指针，提高性能
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;// 初始索引位置
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];// 获取字符

                    var t = _pdict[t1];// 转成映射值
                    if (t == 1) { continue; }//★ 映射值为1时，为跳词
                    if (t <= _firstMaxChar) { p = t; continue; }//★ 映射值<=_firstMaxChar 时，为特殊敏感词字符，字符只出现在敏感词的第一位

                    var next = _pnext[p] + t; // 下一个索引位置，只是可能，待验证
                    if (_pkey[next] == t) {// 验证 是否为下一个索引位置
                        var index = _pcheck[next];// 获取敏感词组索引位置，大于0为真实有效
                        if (index != 0 && CheckNextChar(_ptext, length, t, i, _pdict)) {//★ 检测下一字符，防误判英文数字
                            var guides = _guides[index]; // 获取敏感词组索引
                            var start = i;// 敏感词开始位置
                            var tempLen = 1;//★ 临时长度
                            var tc = t;
                            for (int ij = 0; ij < guides.Length; ij++) {
                                var item = guides[ij];// 敏感词信息索引

                                var keyInfo = GetMatchKeyword(item);// 获取敏感词信息
                                var len = keyInfo.KeywordLength;//★ 获取敏感词长度。敏感词信息不记录文本，只记录长度，可以缩小内存使用量
                                while (tempLen < len) {
                                    if ((tc = _pdict[_ptext[--start]]) != 1) { tempLen++; }//★ 跳过跳词，非跳词，临时长度+1
                                }
                                if (CheckPreChar(_ptext, tc, start, _pdict)) { //★ 检测 start 前面的字符，防误判英文数字 
                                    result.Add(new TempWordsResultItem(start, i, keyInfo));//添加到结果集
                                }
                            }
                        }
                        p = next; // 设置索引位置
                    } else {
                        while (p != 0) {
                            p = _pfailure[p];//★ 获取匹配失败后的索引位置
                            next = _pnext[p] + t;// 下一个索引位置，只是可能，待验证
                            if (_pkey[next] == t) {// 验证 是否为下一个索引位置
                                var index = _pcheck[next];// 获取敏感词组索引位置，大于0为真实有效
                                if (index != 0 && CheckNextChar(_ptext, length, t, i, _pdict)) {//★ 检测下一字符，防误判英文数字 
                                    var guides = _guides[index];// 获取敏感词组索引
                                    var start = i;// 敏感词开始位置
                                    var tempLen = 1;//★ 临时长度
                                    var tc = t;
                                    for (int ij = 0; ij < guides.Length; ij++) {
                                        var item = guides[ij];// 敏感词信息索引

                                        var keyInfo = GetMatchKeyword(item);// 获取敏感词信息
                                        var len = keyInfo.KeywordLength;// 获取敏感词长度。敏感词信息不记录文本，只记录长度，可以缩小内存使用量
                                        while (tempLen < len) {
                                            if ((tc = _pdict[_ptext[--start]]) != 1) { tempLen++; }//★ 跳过跳词，非跳词，临时长度+1
                                        }
                                        if (CheckPreChar(_ptext, tc, start, _pdict)) {//★ 检测 start 前面的字符，防误判英文数字 
                                            result.Add(new TempWordsResultItem(start, i, keyInfo));//添加到结果集
                                        }
                                    }
                                }
                                p = next;// 设置索引位置
                                break;
                            }
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 防止误杀英文数字敏感词，如敏感词【sm】可能错误匹配到【smile】。
        /// </summary>
        /// <param name="_ptext"></param>
        /// <param name="length"></param>
        /// <param name="t"></param>
        /// <param name="i"></param>
        /// <param name="_pdict"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckNextChar(char* _ptext, int length, ushort t, int i, ushort* _pdict)
        {
            if (t < _azNumMinChar || t > _azNumMaxChar || i == length) { return true; }

            var tt = _pdict[_ptext[i + 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }
        /// <summary>
        /// 防止误杀英文数字敏感词，如敏感词【sm】可能错误匹配到【smile】。
        /// </summary>
        /// <param name="_ptext"></param>
        /// <param name="t"></param>
        /// <param name="i"></param>
        /// <param name="_pdict"></param>
        /// <returns></returns>

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckPreChar(char* _ptext, ushort t, int i, ushort* _pdict)
        {
            if (t < _azNumMinChar || t > _azNumMaxChar || i == 0) { return true; }

            var tt = _pdict[_ptext[i - 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        #endregion

        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _azNumMinChar = br.ReadUInt16();
            _azNumMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
            _key = br.ReadUshortArray();

            _next = br.ReadIntArray();
            _check = br.ReadIntArray();
            _failure = br.ReadIntArray();
            _guides = br.ReadIntArray2();
        }


        #endregion
    }
}
