using System;
using System.Linq;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class SqlServer2012DatabaseProvider : SqlServerDatabaseProvider
    {

        //private static readonly Regex SelectTopRegex = new Regex(@"^SELECT +TOP(\d+)", RegexOptions.IgnoreCase);

        public override string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            if (parts.Sql.IndexOf("order by",0, StringComparison.OrdinalIgnoreCase)==-1) {
                return base.BuildPageQuery(skip, take, parts, ref args);
            }

            if (SelectTopRegex.IsMatch(parts.Sql)) return parts.Sql;
            if (skip == 0) {
                if (parts.Sql.StartsWith("SELECT ", StringComparison.InvariantCultureIgnoreCase)) {
                    var sql = $"SELECT TOP(@{args.Length}) " + parts.Sql.Substring(7/*"SELECT ".Length*/);
                    args = args.Concat(new object[] { take }).ToArray();
                    return sql;
                }
            }

            var sqlPage = $"{parts.Sql}\nOFFSET @{args.Length} ROWS FETCH NEXT @{args.Length + 1} ROWS ONLY";
            args = args.Concat(new object[] { skip, take }).ToArray();
            return sqlPage;
        }
        public override string GetAutoIncrementExpression(TableInfo tableInfo)
        {
            if (!string.IsNullOrEmpty(tableInfo.SequenceName))
                return $"NEXT VALUE FOR {tableInfo.SequenceName}";
            return null;
        }

        public override string ToString()
        {
            return "SqlServer2012DatabaseProvider";
        }

    }
}
