using System;

namespace ToolGood.ReadyGo3.Gadget.Events
{
    /// <summary>
    /// sql错误事件事件参数 
    /// </summary>
    public class SqlErrorEventArgs : System.EventArgs
    {
        /// <summary>
        /// sql错误事件事件参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <param name="sqlWithArgs"></param>
        /// <param name="exception"></param>
        public SqlErrorEventArgs(string sql, object[] args, string sqlWithArgs, Exception exception)
        {
            SqlWithArgs = sqlWithArgs;
            Exception = exception;
            ErrorMsg = exception.Message;
            Sql = sql;
            Args = args;
            Handle = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception;

        /// <summary>
        /// Sql语句
        /// </summary>
        public string Sql;
        /// <summary>
        /// 参数
        /// </summary>
        public object[] Args;
        /// <summary>
        /// Sql语句+参数
        /// </summary>
        public string SqlWithArgs;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg;
        /// <summary>
        /// 是否处理
        /// </summary>
        public bool Handle;
    }
    /// <summary>
    /// sql错误事件事件处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void SqlErrorEventHandler(object sender, SqlErrorEventArgs args);
}