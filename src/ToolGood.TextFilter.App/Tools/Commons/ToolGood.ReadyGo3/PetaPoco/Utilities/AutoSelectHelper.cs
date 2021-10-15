using System.Text.RegularExpressions;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.PetaPoco.Internal
{
    internal static class AutoSelectHelper
    {
        private static readonly Regex rxSelect = new Regex(@"^(SELECT|SQLEXEC|EXEC|EXECUTE|CALL|WITH|SET|DECLARE|USE|GO|PRINT)\s",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //private static readonly Regex rxSelect = new Regex(@"\A\s*(SELECT|SQLEXEC|EXEC|EXECUTE|CALL|WITH|SET|DECLARE|USE|GO|PRINT)\s",
        //    RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static readonly Regex rxFrom = new Regex(@"FROM\s*([^,\s]+)(\s+AS)?(\s+?[^,\s]+)?",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static string AddSelectClause<T>(DatabaseProvider provider, string table, string sql)
        {
            sql = sql.Trim();
            //if (sql.StartsWith(";")) return sql.Substring(1);
            if (!rxSelect.IsMatch(sql)) {
                var pd = PocoData.ForType(typeof(T));
                var tableName = table;


                if (sql.StartsWith("FROM ", System.StringComparison.CurrentCultureIgnoreCase)) {
                    var m = rxFrom.Match(sql);
                    if (m.Success) {
                        if (string.IsNullOrEmpty(tableName)) {
                            tableName = m.Groups[3].Success ? m.Groups[3].Value.Trim() : m.Groups[1].Value.Trim();
                        }
                        var columns = CrudCache.GetSelectColumnsSql(provider, pd, tableName);
                        sql = $"SELECT {columns} {sql}";
                    } else {
                        var columns = CrudCache.GetSelectColumnsSql(provider, pd);
                        if (string.IsNullOrEmpty(tableName)) {
                            tableName = provider.GetTableName(pd.TableInfo.TableName);
                        }
                        sql = $"SELECT {columns} FROM {tableName} {sql}";
                    }
                } else {
                    var columns = CrudCache.GetSelectColumnsSql(provider, pd);
                    if (string.IsNullOrEmpty(tableName)) {
                        tableName = provider.GetTableName(pd.TableInfo.TableName);
                    }
                    sql = $"SELECT {columns} FROM {tableName} {sql}";
                }
            }
            return sql;
        }



    }
}