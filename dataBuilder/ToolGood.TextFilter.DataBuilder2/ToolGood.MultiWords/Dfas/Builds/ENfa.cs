using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.MultiWords.Dfas.Builds
{
    public class ENfa
    {
        public static int ItemIndex = 1;

        /// <summary>
        /// 获取或设置 NFA 的首状态。
        /// </summary>
        public ENfaState HeadState { get; set; }

        /// <summary>
        /// 获取或设置 NFA 的尾状态。
        /// </summary>
        public ENfaState TailState { get; set; }

        /// <summary>
        /// 在当前 NFA 中创建一个新状态。
        /// </summary>
        /// <returns>新创建的状态。</returns>
        public static ENfaState NewState()
        {
            ENfaState state = new ENfaState(ItemIndex++);
            return state;
        }



    }
}
