using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        ///     The column name.
        /// </summary>
        public string PrimaryKey;

        /// <summary>
        ///     A flag which specifies if the primary key is auto incrementing.
        /// </summary>
        public bool AutoIncrement;

        /// <summary>
        ///     The sequence name.
        /// </summary>
        public string SequenceName;

        /// <summary>
        ///     Constructs a new instance of the <seealso cref="PrimaryKeyAttribute" />.
        /// </summary>
        /// <param name="primaryKey">The name of the primary key column.</param>
        public PrimaryKeyAttribute(string primaryKey)
        {
            PrimaryKey = primaryKey;
            AutoIncrement = true;
        }
        /// <summary>
        /// 主键
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="autoIncrement"></param>
        public PrimaryKeyAttribute(string primaryKey, bool autoIncrement)
        {
            PrimaryKey = primaryKey.Trim();
            AutoIncrement = autoIncrement;
        }
        /// <summary>
        /// 主键
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="autoIncrement"></param>
        /// <param name="sequenceName"></param>
        public PrimaryKeyAttribute(string primaryKey, bool autoIncrement, string sequenceName)
        {
            PrimaryKey = primaryKey.Trim();
            AutoIncrement = autoIncrement;
            SequenceName = sequenceName.Trim();
        }
    }
}