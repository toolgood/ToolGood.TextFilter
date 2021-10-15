using System;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.PetaPoco.Core;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class SqlServerDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory(
                "System.Data.SqlClient.SqlClientFactory, System.Data.SqlClient",
                "System.Data.SqlClient.SqlClientFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                "System.Data.SqlClient.SqlClientFactory, System.Data"
                );
        }
        protected static readonly Regex SelectTopRegex = new Regex(@"^SELECT +TOP(\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);


        public override string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            if (SelectTopRegex.IsMatch(parts.Sql)) return parts.Sql;
            if (skip == 0) {
                if (parts.Sql.StartsWith("SELECT ", StringComparison.InvariantCultureIgnoreCase)) {
                    var sql = $"SELECT TOP(@{args.Length}) " + parts.Sql.Substring(7/*"SELECT ".Length*/);
                    args = args.Concat(new object[] { take }).ToArray();
                    return sql;
                }
            }

            var helper = PagingUtility;
            // when the query does not contain an "order by", it is very slow
            if (helper.SimpleRegexOrderBy.IsMatch(parts.SqlSelectRemoved)) {
                var m = helper.SimpleRegexOrderBy.Match(parts.SqlSelectRemoved);
                if (m.Success) {
                    var g = m.Groups[0];
                    parts.SqlSelectRemoved = parts.SqlSelectRemoved.Substring(0, g.Index);
                }
            }
            if (helper.RegexDistinct.IsMatch(parts.SqlSelectRemoved)) {
                parts.SqlSelectRemoved = $"peta_inner.* FROM (SELECT {parts.SqlSelectRemoved}) peta_inner";
            }
            var sqlPage =
              $"SELECT * FROM (SELECT ROW_NUMBER() OVER ({parts.SqlOrderBy ?? "ORDER BY (SELECT NULL)"}) peta_rn, " +
              $"{parts.SqlSelectRemoved}) peta_paged " +
              $"WHERE peta_rn > @{args.Length} AND peta_rn <= @{args.Length + 1}";
            args = args.Concat(new object[] { skip, skip + take }).ToArray();
            return sqlPage;
        }

        public override object ExecuteInsert(Database db, System.Data.IDbCommand cmd, string primaryKeyName)
        {
            return db.ExecuteScalarHelper(cmd);
        }

        public override string GetExistsSql()
        {
            return "IF EXISTS (SELECT 1 FROM {0} WHERE {1}) SELECT 1 ELSE SELECT 0";
        }

        public override string GetInsertOutputClause(string primaryKeyName)
        {
            return $" OUTPUT INSERTED.[{primaryKeyName}]";
        }
        public override string CreateSql(int limit, int offset, string columnSql, string fromtable, string order, string where)
        {
            if (offset <= 0) {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ");
                sb.Append("TOP ");
                sb.Append(limit);
                sb.Append(" ");
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
                return sb.ToString();
            } else {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {0})", string.IsNullOrWhiteSpace(order) ? "(SELECT NULL)" : order);
                sb.AppendFormat(" peta_rn,{0} ", columnSql);
                sb.Append(" FROM ");
                sb.Append(fromtable);
                if (string.IsNullOrEmpty(where) == false) {
                    sb.Append(" WHERE ");
                    sb.Append(where);
                }
                sb.AppendFormat(")  peta_paged WHERE peta_rn>{0} AND peta_rn<={1}", offset, limit + offset);
                return sb.ToString();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string CreateFunction(SqlFunction function, params object[] args)
        {
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
                //case SqlFunction.DateDiff: return CreateFunction("DATEDIFF(DAY,{0},{1})", args);
                case SqlFunction.Year: return CreateFunction("DATEPART(YEAR,{0})", args);
                case SqlFunction.Month: return CreateFunction("DATEPART(MONTH,{0})", args);
                case SqlFunction.Day: return CreateFunction("DATEPART(DAY,{0})", args);
                case SqlFunction.Hour: return CreateFunction("DATEPART(HOUR,{0})", args);
                case SqlFunction.Minute: return CreateFunction("DATEPART(MINUTE,{0})", args);
                case SqlFunction.Second: return CreateFunction("DATEPART(SECOND,{0})", args);
                case SqlFunction.DayOfYear: return CreateFunction("DATEPART(DAYOFYEAR,{0})", args);
                //case SqlFunction.Week: return CreateFunction("DATEPART(WEEK,{0})", args);
                case SqlFunction.WeekDay: return CreateFunction("DATEPART(WEEKDAY,{0})", args);
                case SqlFunction.SubString3: break;
                case SqlFunction.SubString2: break;
                //case SqlFunction.Left: break;
                //case SqlFunction.Right: break;
                case SqlFunction.Lower: break;
                case SqlFunction.Upper: break;
                //case SqlFunction.Ascii: break;
                //case SqlFunction.Concat: break;
                case SqlFunction.IndexOf: return CreateFunction("(CHARINDEX({1},{0})-1)", args);
                default: break;
            }


            return base.CreateFunction(function, args);
        }


        public override string ToString()
        {
            return "SqlServerDatabaseProvider";
        }
    }
}