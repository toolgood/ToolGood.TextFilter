using System.Data.Common;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class SQLiteDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                "System.Data.SQLite.SQLiteFactory, System.Data.SQLite, Culture=neutral, PublicKeyToken=db937bc2d44ff139",
                "System.Data.SQLite.SQLiteFactory, System.Data.SQLite",
                "Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite, Culture=neutral, PublicKeyToken=adb9793829ddae60",
                "Microsoft.Data.Sqlite.SqliteFactory, Microsoft.Data.Sqlite"
                );
        }

        public override object MapParameterValue(object value)
        {
            if (value is uint)
                return (long)((uint)value);

            return base.MapParameterValue(value);
        }

        public override object ExecuteInsert(Database db, System.Data.IDbCommand cmd, string primaryKeyName)
        {
            if (primaryKeyName != null) {
                cmd.CommandText += ";\nSELECT last_insert_rowid();";
                return db.ExecuteScalarHelper(cmd);
            } else {
                db.ExecuteNonQueryHelper(cmd);
                return -1;
            }
        }

        public override string GetExistsSql()
        {
            return "SELECT EXISTS (SELECT 1 FROM {0} WHERE {1})";
        }

        public override string GetTableName(string databaseName, string schemaName, string tableName)
        {
            return $"[{tableName}]";
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
            // http://www.sqlite.org/lang_corefunc.html
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
                //case SqlFunction.DateDiff: return CreateFunction("FLOOR(CAST(STRFTIME('%J',{0}) AS INT) - CAST(STRFTIME('%J',{1}) AS INT))", args);
                case SqlFunction.Year: return CreateFunction("CAST(STRFTIME('%Y',{0}) AS INT)", args);
                case SqlFunction.Month: return CreateFunction("CAST(STRFTIME('%m',{0}) AS INT)", args);
                case SqlFunction.Day: return CreateFunction("CAST(STRFTIME('%d',{0}) AS INT)", args);
                case SqlFunction.Hour: return CreateFunction("CAST(STRFTIME('%H',{0}) AS INT)", args);
                case SqlFunction.Minute: return CreateFunction("CAST(STRFTIME('%M',{0}) AS INT)", args);
                case SqlFunction.Second: return CreateFunction("CAST(STRFTIME('%S',{0}) AS INT)", args);
                case SqlFunction.DayOfYear: return CreateFunction("CAST(STRFTIME('%j',{0}) AS INT)", args);
                //case SqlFunction.Week: return CreateFunction("CAST(STRFTIME('%W',{0}) AS INT)", args);
                case SqlFunction.WeekDay: return CreateFunction("CAST(STRFTIME('%w',{0}) AS INT)", args);
                case SqlFunction.SubString3: return CreateFunction("SUBSTR({0},{1},{2})", args);
                case SqlFunction.SubString2: return CreateFunction("SUBSTR({0},{1})", args);
                //case SqlFunction.Left: return CreateFunction("SUBSTR({0},1,{1})", args);
                //case SqlFunction.Right: return CreateFunction("SUBSTR({0},-{1})", args);
                case SqlFunction.Lower: break;
                case SqlFunction.Upper: break;
                //case SqlFunction.Ascii: break;
                //case SqlFunction.Concat: break;
                default: break;
            }

            return base.CreateFunction(function, args);
        }

        public override string ToString()
        {
            return "SQLiteDatabaseProvider";
        }
    }
}