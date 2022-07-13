using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.DataBuilder2.Enums
{
    public enum EmMatchType : byte
    {
        普通匹配 = 0,
        匹配开头 = 1,
        匹配全文 = 2,
        匹配结尾 = 3,
        匹配句子开始 = 4,
        匹配整句话 = 5,
        匹配句子结尾 = 6,

    }
}
