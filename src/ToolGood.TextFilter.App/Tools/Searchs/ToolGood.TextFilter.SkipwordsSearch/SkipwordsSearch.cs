/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;

namespace ToolGood.TextFilter
{
    public interface ISkipwordsSearch
    {
        void Load(BinaryReader br);

        unsafe bool FindAll(char* _ptext, int length, bool* result);

    }
    /// <summary>
    /// SkipwordsSearch类是缩小版的KeywordsSearch2。
    /// </summary>
    public class SkipwordsSearch : ISkipwordsSearch
    {
        private ushort _firstMaxChar;
        private ushort[] _dict;

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _checkLen;//跳词长度
        private Int32[] _failure;

        #region unsafe FindAll

        public unsafe bool FindAll(char* _ptext, int length, bool* result)
        {
            var _find = false;
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_checkLen[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i]; // 获取字符

                    var t = _pdict[t1];// 转成映射值
                    if (t <= _firstMaxChar) { p = t; continue; }// ★ 映射值 <= _firstMaxChar 时，为特殊敏感词字符，字符只出现在敏感词的第一位
                    if (t == 0xffff) { continue; }// 不存在跳过，注这里跳过类【空格】

                    var next = _pnext[p] + t;// 下一个索引位置，只是可能，待验证
                    if (_pkey[next] == t) {// 验证 是否为下一个索引位置
                        var len = _pcheck[next];// 获取敏感词组的长度
                        if (len > 0) {// 长度大于0，表示敏感词存在
                            _find = true;// 赋值，表示查找到了
                            var idx = i; // 索引
                            while (len != 0) {
                                if (_pdict[_ptext[idx]] != 0xffff) { len--; }// ★ 判断不是为空格， 长度减一
                                result[idx] = true;// 设置跳词标志
                                idx--;   // 索引减一
                            }
                        }
                        p = next;// 设置索引位置
                    } else {
                        while (p != 0) {
                            p = _pfailure[p];// 获取失败后指向的地址
                            next = _pnext[p] + t;// ★ 下一个索引位置，只是可能，待验证
                            if (_pkey[next] == t) { // 验证 是否为下一个索引位置
                                var len = _pcheck[next];// 获取敏感词组的长度
                                if (len > 0) {// 长度大于0，表示敏感词存在
                                    _find = true;// 赋值，表示查找到了
                                    var idx = i;// 索引
                                    while (len != 0) {
                                        if (_pdict[_ptext[idx]] != 0xffff) { len--; }// ★ 判断不是为空格， 长度减一
                                        result[idx] = true;// 设置跳词标志
                                        idx--; //索引减一
                                    }
                                }
                                p = next;// 设置索引位置
                                break;
                            }
                        }
                    }


                }
            }
            return _find;// 返回是不是查找到了
        }

        #endregion

        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
            _key = br.ReadUshortArray();
            _next = br.ReadIntArray();
            _checkLen = br.ReadIntArray();
            _failure = br.ReadIntArray();
        }
        #endregion

    }
}
