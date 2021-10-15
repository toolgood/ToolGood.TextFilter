using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 返回
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class ResultColumnAttribute : ColumnAttribute
    {
        /// <summary>
        ///
        /// </summary>
        public ResultColumnAttribute() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public ResultColumnAttribute(string name) : base(name) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="definition"></param>
        public ResultColumnAttribute(string name, string definition) : base(name)
        {
            if (string.IsNullOrEmpty(definition) == false) {
                definition = definition.Replace("{0}.", "{0}").Trim();
                if (definition.StartsWith("(") == false) {
                    definition = "(" + definition + ")";
                }
                Definition = definition;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Definition;
    }
}