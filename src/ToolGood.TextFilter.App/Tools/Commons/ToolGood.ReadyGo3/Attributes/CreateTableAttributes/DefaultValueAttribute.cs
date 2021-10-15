using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 默认值特征
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class DefaultValueAttribute : Attribute
    {
        /// <summary>
        /// 默认值特征
        /// </summary>
        /// <param name="defaultstring">默认SQL</param>
        public DefaultValueAttribute(string defaultstring)
        {
            DefaultValue = defaultstring.Trim();
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue;
    }
}