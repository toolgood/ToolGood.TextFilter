using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 非空标签 
    /// </summary>
    class RequiredAttribute : Attribute
    {
        /// <summary>
        /// 是否非空
        /// </summary>
        public bool Required;
        /// <summary>
        /// 非空标签
        /// </summary>
        /// <param name="required"></param>
        public RequiredAttribute(bool required = true)
        {
            Required = required;
        }
    }
}
