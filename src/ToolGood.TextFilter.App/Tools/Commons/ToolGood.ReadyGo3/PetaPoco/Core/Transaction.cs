using System;

namespace ToolGood.ReadyGo3.PetaPoco
{
    /// <summary>
    ///     Transaction object helps maintain transaction depth counts
    /// </summary>
    public class Transaction : IDisposable
    {
        private Database _db;
        /// <summary>
        /// Transaction
        /// </summary>
        /// <param name="db"></param>
        internal Transaction(Database db)
        {
            _db = db;
            _db.BeginTransaction();
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Complete()
        {
            if (_db != null) {
                _db.CompleteTransaction();
                _db = null;
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_db != null) {
                _db.AbortTransaction();
                _db = null;
            }
        }
    }
}