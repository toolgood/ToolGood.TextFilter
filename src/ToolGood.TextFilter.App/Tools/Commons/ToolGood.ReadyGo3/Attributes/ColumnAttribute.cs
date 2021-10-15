using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 列标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name;

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment;

        /// <summary>
        /// 是否转成Utc
        /// </summary>
        public bool ForceToUtc;

        /// <summary>
        /// 列标签
        /// </summary>
        public ColumnAttribute() { }

        /// <summary>
        /// 列标签
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="comment">备注</param>
        public ColumnAttribute(string name, string comment = null)
        {
            this.Name = name.Trim();
            ForceToUtc = false;
            if (comment != null) {
                this.Comment = comment.Trim();
            }
        }
    }
}