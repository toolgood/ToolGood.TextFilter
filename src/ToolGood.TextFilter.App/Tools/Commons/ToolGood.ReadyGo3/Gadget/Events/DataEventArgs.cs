using System;

namespace ToolGood.ReadyGo3.Gadget.Events
{
    /// <summary>
    /// 数据事件参数
    /// </summary>
    class DataEventArgs : System.EventArgs
    {
        /// <summary>
        /// 数据事件参数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sqlHelper"></param>
        public DataEventArgs(object obj, SqlHelper sqlHelper)
        {
            Obj = obj;
            Cancel = false;
            SqlHelper = sqlHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        public SqlHelper SqlHelper;

        /// <summary>
        /// 是否取消操作
        /// </summary>
        public bool Cancel;

        /// <summary>
        /// 操作对象
        /// </summary>
        public object Obj;
    }
    /// <summary>
    /// 插入之前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void BeforeInsertEventHandler(object sender, DataEventArgs args);
    /// <summary>
    /// 更新之前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void BeforeUpdateEventHandler(object sender, DataEventArgs args);
    /// <summary>
    /// 删除之前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void BeforeDeleteEventHandler(object sender, DataEventArgs args);
    /// <summary>
    /// 数据事件参数
    /// </summary>
    class Data2EventArgs : System.EventArgs
    {
        /// <summary>
        /// 数据事件参数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sqlHelper"></param>
        public Data2EventArgs(object obj, SqlHelper sqlHelper)
        {
            Obj = obj;
            SqlHelper = sqlHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        public SqlHelper SqlHelper;
        /// <summary>
        /// 操作对象
        /// </summary>
        public object Obj;
    }
    /// <summary>
    /// 插入之后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void AfterInsertEventHandler(object sender, Data2EventArgs args);
    /// <summary>
    /// 更新之后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void AfterUpdateEventHandler(object sender, Data2EventArgs args);
    /// <summary>
    /// 删除之后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    delegate void AfterDeleteEventHandler(object sender, Data2EventArgs args);

    /// <summary>
    /// 事务之前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void BeforeTransactionEventHandler(object sender, EventArgs args);

    /// <summary>
    /// 事务之后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void AfterTransactionEventHandler(object sender, EventArgs args);

}