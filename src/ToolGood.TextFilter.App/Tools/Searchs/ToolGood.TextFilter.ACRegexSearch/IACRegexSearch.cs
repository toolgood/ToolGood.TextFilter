using System;
/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    public interface IACRegexSearch
    {
        void Set_GetMatchKeyword(Func<int, KeywordInfo> func);

        void SetDict(byte[] skipIndexs, ushort[][] dicts, ISkipwordsSearch[] skipwordsSearchs, bool[] useSkipOnce);

        unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result);

        void Load(BinaryReader br);

    }
}
