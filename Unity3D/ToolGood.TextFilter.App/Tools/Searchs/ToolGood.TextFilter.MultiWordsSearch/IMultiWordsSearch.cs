/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;

namespace ToolGood.TextFilter
{
    public interface IMultiWordsSearch 
    {
        List<TempMultiWordsResult> FindAll(List<TempWordsResultItem> txt);

        void Load(BinaryReader br);
    }


}
