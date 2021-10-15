using System.Data.Common;
using System.Text;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class MySqlDatabaseProvider : DatabaseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public MySqlDatabaseProvider()
        {
            usedEscapeSql = true;
            escapeSql = '`';
        }
        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                "MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data, Culture=neutral, PublicKeyToken=c5687fc88969c44d",
                "MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data"
                );
        }

        public override string GetParameterPrefix(string connectionString)
        {
            if (connectionString != null && connectionString.IndexOf("Allow User Variables=true") >= 0)
                return "?";
            else
                return "@";
        }

        public override string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return $"`{sqlIdentifier}`";
        }

        public override string GetTableName(string databaseName, string schemaName, string tableName)
        {
            if (string.IsNullOrEmpty(databaseName) == false) {
                return $"`{databaseName}`.`{tableName}`";
            }
            if (string.IsNullOrEmpty(schemaName) == false) {
                return $"`{schemaName}`.`{tableName}`";
            }
            return $"`{tableName}`";
        }

        public override string GetExistsSql()
        {
            return "SELECT EXISTS (SELECT 1 FROM {0} WHERE {1})";
        }
        public override string CreateSql(int limit, int offset, string columnSql, string fromtable, string order, string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(columnSql);
            sb.Append(" FROM ");
            sb.Append(fromtable);
            if (string.IsNullOrEmpty(where) == false) {
                sb.Append(" WHERE ");
                sb.Append(where);
            }
            if (string.IsNullOrEmpty(order) == false) {
                sb.Append(" ORDER BY ");
                sb.Append(order);
            }
            if (limit > 0) {
                sb.Append(" LIMIT ");
                if (offset > 0) {
                    sb.Append(offset);
                    sb.Append(",");
                }
                sb.Append(limit);
            }
            return sb.ToString();
        }


        public override string ToString()
        {
            return "MySqlDatabaseProvider";
        }
    }
}