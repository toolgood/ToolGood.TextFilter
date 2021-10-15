namespace ToolGood.ReadyGo3.Gadget.Events
{
    /// <summary>
    /// 
    /// </summary>
    class SqlOperationEventArgs : System.EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <param name="sqlWithArgs"></param>
        /// <param name="sqlHelper"></param>
        public SqlOperationEventArgs(string sql, object[] args, string sqlWithArgs, SqlHelper sqlHelper)
        {
            Sql = sql;
            Args = args;
            SqlWithArgs = sqlWithArgs;
            SqlHelper = sqlHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlHelper SqlHelper;

        /// <summary>
        /// Sql语句
        /// </summary>
        public string Sql;

        /// <summary>
        /// 参数 
        /// </summary>
        public object[] Args;

        /// <summary>
        /// Sql语句 带 参数
        /// </summary>
        public string SqlWithArgs;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void SqlOperationEventHandler(object sender, SqlOperationEventArgs args);
}