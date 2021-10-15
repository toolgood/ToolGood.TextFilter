namespace ToolGood.ReadyGo3
{


    /// <summary>
    /// Sqlite 
    /// </summary>
    public enum JournalMode
    {
        /// <summary>
        /// 不启用
        /// </summary>
        None,

        /// <summary>
        /// 在此模式下，每次事务终止的时候，journal文件会被删除，它会导致事务提交。
        /// </summary>
        Delete,

        /// <summary>
        /// 通过将回滚journal截短成0，而不是删除它。
        /// </summary>
        Truncate,

        /// <summary>
        /// 每次事务结束时，并不删除rollback journal，而只是在journal的头部填充0，
        /// 这样会阻止别的数据库连接来rollback. 该模式在某些平台下，是一种优化，
        /// 特别是删除或者truncate一个文件比覆盖文件的第一块代价高的时候。
        /// </summary>
        Persist,

        /// <summary>
        /// 只将rollback日志存储到RAM中，节省了磁盘I/O，但带来的代价是稳定性和完整性上的损失。
        /// 如果中间crash掉了，数据库有可能损坏。
        /// </summary>
        Memory,

        /// <summary>
        /// 也就是write-ahead　log取代rollback journal。
        /// 该模式是持久化的，跨多个数据为连接，在重新打开数据库以后，仍然有效。该模式只在3.7.0以后才有效。
        /// </summary>
        Wal,

        /// <summary>
        /// 这样就没有事务支持了
        /// </summary>
        Off
    }
}
