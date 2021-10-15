using System;

namespace ToolGood.ReadyGo3.Gadget.TableManager
{
    class SqlTableHelper
    {
        private readonly SqlHelper _sqlHelper;

        public SqlTableHelper(SqlHelper sqlhelper)
        {
            _sqlHelper = sqlhelper;
        }

        public string GetTryCreateTable(Type type, bool withIndex = true)
        {
            var dp = DatabaseProvider.Resolve(_sqlHelper._sqlType);
            return dp.GetTryCreateTable(type, withIndex);
        }
        public string GetCreateTable(Type type, bool withIndex = true)
        {
            var dp = DatabaseProvider.Resolve(_sqlHelper._sqlType);
            return dp.GetCreateTable(type, withIndex);
        }

        public string GetCreateTableIndex(Type type)
        {
            var dp = DatabaseProvider.Resolve(_sqlHelper._sqlType);
            return dp.GetCreateIndex(type);
        }

        public string GetDropTable(Type type)
        {
            var dp = DatabaseProvider.Resolve(_sqlHelper._sqlType);
            return dp.GetDropTable(type);
        }
        public string GetTruncateTable(Type type)
        {
            var dp = DatabaseProvider.Resolve(_sqlHelper._sqlType);
            return dp.GetTruncateTable(type);
        }



        public void TryCreateTable(Type type, bool withIndex = true)
        {
            var sql = GetTryCreateTable(type, withIndex);
            _sqlHelper.Execute(sql);
        }

        public void CreateTable(Type type, bool withIndex = true)
        {
            var sql = GetCreateTable(type, withIndex);
            _sqlHelper.Execute(sql);
        }


        public void CreateTableIndex(Type type)
        {
            var sql = GetCreateTableIndex(type);
            if (string.IsNullOrEmpty(sql)) { return; }
            _sqlHelper.Execute(sql);
        }

        public void DropTable(Type type)
        {
            var sql = GetDropTable(type);
            _sqlHelper.Execute(sql);
        }

        public void TruncateTable(Type type)
        {
            var sql = GetTruncateTable(type);
            _sqlHelper.Execute(sql);
        }


    }
}
