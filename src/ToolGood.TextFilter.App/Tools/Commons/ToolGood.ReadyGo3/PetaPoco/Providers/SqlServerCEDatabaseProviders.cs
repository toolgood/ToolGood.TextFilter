using System.Data.Common;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.PetaPoco.Core;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class SqlServerCEDatabaseProviders : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                //"System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe, Culture=neutral, PublicKeyToken=89845dcd8080cc91",
                "System.Data.SqlServerCe.SqlCeProviderFactory, System.Data.SqlServerCe"
                );
        }

        public override string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            if (string.IsNullOrEmpty(parts.SqlOrderBy))
                parts.Sql += " ORDER BY ABS(1)";
            var sqlPage = $"{parts.Sql}\nOFFSET @{args.Length} ROWS FETCH NEXT @{args.Length + 1} ROWS ONLY";
            args = args.Concat(new object[] { skip, take }).ToArray();
            return sqlPage;
        }

        public override object ExecuteInsert(Database db, System.Data.IDbCommand cmd, string primaryKeyName)
        {
            db.ExecuteNonQueryHelper(cmd);
            return db.ExecuteScalar<object>("SELECT @@IDENTITY AS NewID;", new object[0]);
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
            } else {
                sb.Append(" ORDER BY ABS(1)");
            }
            sb.AppendFormat($" OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY");
            return sb.ToString();
        }


        public override string ToString()
        {
            return "SqlServerCEDatabaseProviders";
        }
    }
}