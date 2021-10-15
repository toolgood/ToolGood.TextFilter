namespace ToolGood.ReadyGo3
{
    /// <summary>
    /// SQL 类型
    /// </summary>
    public enum SqlType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None = 0,
        /// <summary>
        /// SQLite 数据库
        /// </summary>
        SQLite = 1,
        /// <summary>
        /// Access 数据库
        /// </summary>
        MsAccessDb,
        /// <summary>
        /// Firebird 数据库
        /// </summary>
        FirebirdDb,


        /// <summary>
        /// Sql Server 数据库
        /// </summary>
        SqlServer = 10,
        /// <summary>
        /// Sql Server CE 数据库
        /// </summary>
        SqlServerCE,

        /// <summary>
        /// Sql Server 2012 数据库
        /// </summary>
        SqlServer2012,


        /// <summary>
        /// MySql 数据库
        /// </summary>
        MySql = 20,
        /// <summary>
        /// Maria 数据库
        /// </summary>
        MariaDb,



        /// <summary>
        /// Oracle 数据库
        /// </summary>
        Oracle = 30,
        /// <summary>
        /// PostgreSQL 数据库
        /// </summary>
        PostgreSQL = 40,




    }
}
