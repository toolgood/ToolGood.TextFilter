using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.PetaPoco.Core;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class FirebirdDbDatabaseProvider : DatabaseProvider
    {
        /// <summary>
        /// Firebird
        /// </summary>
        public FirebirdDbDatabaseProvider()
        {
            usedEscapeSql = true;
            escapeSql = '"';
        }
        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                "FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Culture=neutral, PublicKeyToken=3750abcc3150b00c",
                "FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient"
                );
        }

        public override string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            var sql = $"{parts.Sql}\nROWS @{args.Length} TO @{args.Length + 1}";
            args = args.Concat(new object[] { skip + 1, skip + take }).ToArray();
            return sql;
        }

        public override object ExecuteInsert(Database database, IDbCommand cmd, string primaryKeyName)
        {
            cmd.CommandText = cmd.CommandText.TrimEnd();

            if (cmd.CommandText.EndsWith(";"))
                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1);

            cmd.CommandText += " RETURNING " + EscapeSqlIdentifier(primaryKeyName) + ";";
            return database.ExecuteScalarHelper(cmd);
        }

        public override string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return $"\"{sqlIdentifier}\"";
        }
        public override string GetTableName(string databaseName, string schemaName, string tableName)
        {
            if (string.IsNullOrEmpty(databaseName) == false) {
                if (string.IsNullOrEmpty(schemaName) == false) {
                    return $"\"{databaseName}\".\"{schemaName}\".\"{tableName}\"";
                }
                return $"\"{databaseName}\".\"{tableName}\"";
            }
            if (string.IsNullOrEmpty(schemaName) == false) {
                return $"\"{schemaName}\".\"{tableName}\"";
            }
            return $"\"{tableName}\"";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="columnSql"></param>
        /// <param name="fromtable"></param>
        /// <param name="order"></param>
        /// <param name="where"></param>
        /// <returns></returns>
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
            sb.AppendFormat($" ROWS {offset + 1} TO {offset + limit}");
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string CreateFunction(SqlFunction function, params object[] args)
        {
            //  http://www.firebirdsql.org/refdocs/langrefupd21.html
            switch (function) {
                case SqlFunction.Fuction: break;
                case SqlFunction.Len: break;
                //case SqlFunction.Max: break;
                //case SqlFunction.Min: break;
                //case SqlFunction.Avg: break;
                //case SqlFunction.Sum: break;
                //case SqlFunction.Count: break;
                //case SqlFunction.CountDistinct: break;
                //case SqlFunction.DatePart: break;
                //case SqlFunction.DateDiff: return CreateFunction("DATEDIFF(day,{0},{1})", args);
                case SqlFunction.Year: return CreateFunction("EXTRACT(YEAR FROM {0})", args);
                case SqlFunction.Month: return CreateFunction("EXTRACT(MONTH FROM {0})", args);
                case SqlFunction.Day: return CreateFunction("EXTRACT(DAY FROM {0})", args);
                case SqlFunction.Hour: return CreateFunction("EXTRACT(HOUR FROM {0})", args);
                case SqlFunction.Minute: return CreateFunction("EXTRACT(MINUTE FROM {0})", args);
                case SqlFunction.Second: return CreateFunction("EXTRACT(SECOND FROM {0})", args);
                case SqlFunction.DayOfYear: return CreateFunction("EXTRACT(DAYOFYEAR FROM {0})", args);
                //case SqlFunction.Week: return CreateFunction("EXTRACT(WEEK FROM {0})", args);
                case SqlFunction.WeekDay: return CreateFunction("EXTRACT(WEEKDAY FROM {0})", args);
                case SqlFunction.SubString3: return CreateFunction("SUBSTRING({0} FROM {1} FOR {2})", args);
                case SqlFunction.SubString2: return CreateFunction("SUBSTRING({0} FROM {1})", args);
                //case SqlFunction.Left: return CreateFunction("SLEFT({0},{1})", args);
                //case SqlFunction.Right: return CreateFunction("SRIGHT({0},{1})", args);
                case SqlFunction.Lower: break;
                case SqlFunction.Upper: break;
                //case SqlFunction.Ascii: break;
                //case SqlFunction.Concat: break;
                case SqlFunction.IndexOf: return CreateFunction("(POSITION({1} IN {0})-1)", args);
                default: break;
            }
            return base.CreateFunction(function, args);
        }

        public override string ToString()
        {
            return "FirebirdDbDatabaseProvider";
        }
    }
}