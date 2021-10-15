using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 忽略特征
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class IgnoreAttribute : Attribute
    {
    }
}