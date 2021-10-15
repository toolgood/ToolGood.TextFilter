/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using ToolGood.TextFilter.App.Datas.Results;

namespace ToolGood.TextFilter
{
    public interface IContactSearch
    {
 
        int[] GetContactDict();

        List<ContactResult> FindAll(List<TempWordsResultItem> txt);

        void Load(BinaryReader br);
    }
}
