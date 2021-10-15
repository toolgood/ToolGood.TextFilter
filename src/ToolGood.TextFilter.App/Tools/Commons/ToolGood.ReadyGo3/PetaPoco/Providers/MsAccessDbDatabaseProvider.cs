using System;
using System.Data;
using System.Data.Common;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.Exceptions;
using ToolGood.ReadyGo3.PetaPoco.Core;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Providers
{
    partial class MsAccessDbDatabaseProvider : DatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
#if NETSTANDARD2_0
            throw new DatabaseUnsupportException();
#else
            return DbProviderFactories.GetFactory("System.Data.OleDb");
#endif
        }

        public override object ExecuteInsert(Database database, IDbCommand cmd, string primaryKeyName)
        {
            database.ExecuteNonQueryHelper(cmd);
            cmd.CommandText = "SELECT @@IDENTITY AS NewID;";
            return database.ExecuteScalarHelper(cmd);
        }

        public override string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            throw new NotSupportedException("The Access provider does not support paging.");
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
            }
            throw new DatabaseUnsupportException();
        }

        /// <summary>
        /// 
        /// https://support.office.com/zh-cn/article/%E8%A1%A8%E8%BE%BE%E5%BC%8F%E8%AF%AD%E6%B3%95%E6%8C%87%E5%8D%97-ebc770bc-8486-4adc-a9ec-7427cce39a90#bm3
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override string CreateFunction(SqlFunction function, params object[] args)
        {
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
                //case SqlFunction.DateDiff: break;
                case SqlFunction.Year: break;
                case SqlFunction.Month: break;
                case SqlFunction.Day: break;
                case SqlFunction.Hour: break;
                case SqlFunction.Minute: break;
                case SqlFunction.Second: break;
                case SqlFunction.DayOfYear: break;
                //case SqlFunction.Week: break;
                case SqlFunction.WeekDay: break;
                case SqlFunction.SubString3: return CreateFunction("MID({0}, {1}, {2})", args);
                case SqlFunction.SubString2: return CreateFunction("MID({0}, {1})", args);
                //case SqlFunction.Left: break;
                //case SqlFunction.Right: break;
                case SqlFunction.Lower: return CreateFunction("LCASE({0})", args);
                case SqlFunction.Upper: return CreateFunction("UCASE({0})", args);
                //case SqlFunction.Ascii: return CreateFunction("ASC({0})", args);
                //case SqlFunction.Concat: break;
                case SqlFunction.IndexOf: break;
                default: break;
            }

            return base.CreateFunction(function, args);
        }

        public override string ToString()
        {
            return "MsAccessDbDatabaseProvider";
        }
    }
}