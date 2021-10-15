using System.Data.Common;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class PostgreSQLDatabaseProvider : DatabaseProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public PostgreSQLDatabaseProvider()
        {
            usedEscapeSql = true;
            escapeSql = '"';
        }

        public override bool HasNativeGuidSupport {
            get { return true; }
        }

        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                "Npgsql.NpgsqlFactory, Npgsql, Culture=neutral, PublicKeyToken=5d8b90d52f46fda7",
                "Npgsql.NpgsqlFactory, Npgsql"
                );
        }

        public override string GetExistsSql()
        {
            return "SELECT CASE WHEN EXISTS(SELECT 1 FROM {0} WHERE {1}) THEN 1 ELSE 0 END";
        }

        public override object MapParameterValue(object value)
        {
            // Don't map bools to ints in PostgreSQL
            if (value is bool)
                return value;

            return base.MapParameterValue(value);
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

        public override object ExecuteInsert(Database db, System.Data.IDbCommand cmd, string primaryKeyName)
        {
            if (primaryKeyName != null) {
                cmd.CommandText += $"returning {EscapeSqlIdentifier(primaryKeyName)} as NewID";
                return db.ExecuteScalarHelper(cmd);
            } else {
                db.ExecuteNonQueryHelper(cmd);
                return -1;
            }
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string CreateFunction(SqlFunction function, params object[] args)
        {
            //  https://www.postgresql.org/docs/9.1/static/functions-math.html
            //  https://www.postgresql.org/docs/9.1/static/functions-string.html
            //  https://www.postgresql.org/docs/9.1/static/functions-datetime.html
            switch (function) {
                case SqlFunction.Fuction: break;
                case SqlFunction.Len: return CreateFunction("LENGTH({0})", args);
                //case SqlFunction.Max: break;
                //case SqlFunction.Min: break;
                //case SqlFunction.Avg: break;
                //case SqlFunction.Sum: break;
                //case SqlFunction.Count: break;
                //case SqlFunction.CountDistinct: break;
                //case SqlFunction.DatePart: break;
                //case SqlFunction.DateDiff: return CreateFunction("AGE(TIMESTAMP {0}, TIMESTAMP {1})", args);
                case SqlFunction.Year: return CreateFunction("EXTRACT(YEAR FROM TIMESTAMP {0})", args);
                case SqlFunction.Month: return CreateFunction("EXTRACT(MONTH FROM TIMESTAMP {0})", args);
                case SqlFunction.Day: return CreateFunction("EXTRACT(DAY FROM TIMESTAMP {0})", args);
                case SqlFunction.Hour: return CreateFunction("EXTRACT(HOUR FROM TIMESTAMP {0})", args);
                case SqlFunction.Minute: return CreateFunction("EXTRACT(MINUTE FROM TIMESTAMP {0})", args);
                case SqlFunction.Second: return CreateFunction("EXTRACT(SECOND FROM TIMESTAMP {0})", args);
                case SqlFunction.DayOfYear: return CreateFunction("EXTRACT(DAYOFYEAR FROM TIMESTAMP {0})", args);
                //case SqlFunction.Week: return CreateFunction("EXTRACT(WEEK FROM TIMESTAMP {0})", args);
                case SqlFunction.WeekDay: return CreateFunction("EXTRACT(WEEKDAY FROM TIMESTAMP {0})", args);
                case SqlFunction.SubString3: return CreateFunction("SUBSTR({0}, {1}, {2})", args);
                case SqlFunction.SubString2: return CreateFunction("SUBSTR({0}, {1})", args);
                //case SqlFunction.Left: break;
                //case SqlFunction.Right: break;
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
            return "PostgreSQLDatabaseProvider";
        }
    }
}