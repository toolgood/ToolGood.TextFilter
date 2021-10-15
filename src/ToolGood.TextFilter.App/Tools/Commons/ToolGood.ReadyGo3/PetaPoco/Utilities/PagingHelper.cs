using System.Text.RegularExpressions;

namespace ToolGood.ReadyGo3.PetaPoco.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingHelper
    {
        internal Regex RegexColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        internal Regex RegexDistinct = new Regex(@"\ADISTINCT\s", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        internal Regex RegexOrderBy =
            new Regex(
                @"\bORDER\s+BY\s+(?!.*?(?:\)|\s+)AS\s)(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\[\]`""\w\(\)\.])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\[\]`""\w\(\)\.])+(?:\s+(?:ASC|DESC))?)*",
                RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        internal Regex SimpleRegexOrderBy = new Regex(@"\bORDER\s+BY\s+",
            RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        internal Regex RegexGroupBy = new Regex(@"\bGROUP\s+BY\s+(?!.*?(?:\)|\s+)AS\s)(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\[\]`""\w\(\)\.])+?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\[\]`""\w\(\)\.])+?)*",
                                              RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        internal Regex SimpleRegexGroupBy = new Regex(@"\bGROUP\s+BY\s+",
                                                    RegexOptions.RightToLeft | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);


        internal static PagingHelper Instance { get; private set; }

        static PagingHelper()
        {
            Instance = new PagingHelper();
        }

        ///// <summary>
        /////     Splits the given <paramref name="sql" /> into <paramref name="parts" />;
        ///// </summary>
        ///// <param name="sql">The SQL to split.</param>
        ///// <param name="parts">The SQL parts.</param>
        ///// <returns><c>True</c> if the SQL could be split; else, <c>False</c>.</returns>
        //public bool SplitSQL(string sql, out SQLParts parts)
        //{
        //    parts.Sql = sql;
        //    parts.SqlSelectRemoved = null;
        //    parts.SqlCount = null;
        //    parts.SqlOrderBy = null;

        //    // Extract the columns from "SELECT <whatever> FROM"
        //    var columnsMatch = RegexColumns.Match(sql);
        //    if (!columnsMatch.Success)
        //        return false;

        //    // Look for the last "ORDER BY <whatever>" clause not part of a ROW_NUMBER expression
        //    var orderByMatch = RegexOrderBy.Match(sql);
        //    if (orderByMatch.Success) {
        //        parts.SqlOrderBy = orderByMatch.Value;
        //        parts.SqlCount = sql.Replace(orderByMatch.Value, string.Empty);
        //    }

        //    // Save column list and replace with COUNT(*)
        //    var columnsGroup = columnsMatch.Groups[1];
        //    parts.SqlSelectRemoved = sql.Substring(columnsGroup.Index);

        //    if (RegexDistinct.IsMatch(parts.SqlSelectRemoved)) {
        //        var txt = columnsMatch.Groups[1].ToString().Trim();
        //        if (txt.StartsWith("Distinct", System.StringComparison.CurrentCultureIgnoreCase) && txt.Contains(",") == false) {
        //            parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(" + columnsMatch.Groups[1].ToString().Trim() + ") " + sql.Substring(columnsGroup.Index + columnsGroup.Length);
        //        } else if (txt.Contains(",")) {
        //            columnsMatch = SimpleRegexOrderBy.Match(sql);
        //            if (columnsMatch.Success) {
        //                columnsGroup = columnsMatch.Groups[0];
        //                parts.SqlOrderBy = columnsGroup + sql.Substring(columnsGroup.Index + columnsGroup.Length);
        //                parts.SqlCount = $"SELECT COUNT(*) FROM ({sql.Substring(0, columnsGroup.Index)}) __toolgood__ ";
        //                return true;
        //            } else {
        //                parts.SqlCount = $"SELECT COUNT(*) FROM ({sql}) __toolgood__ ";
        //            }
        //        } else {
        //            parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(" + columnsMatch.Groups[1].ToString().Trim() + ") " + sql.Substring(columnsGroup.Index + columnsGroup.Length);
        //        }
        //    } else if(RegexGroupBy.IsMatch(parts.SqlSelectRemoved))
        //        parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(*) FROM (" + parts.SqlCount + ") __toolgood__";
        //    else
        //        parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(*) " + sql.Substring(columnsGroup.Index + columnsGroup.Length);

        //    // Look for the last "ORDER BY <whatever>" clause not part of a ROW_NUMBER expression
        //    columnsMatch = SimpleRegexOrderBy.Match(parts.SqlCount);
        //    if (columnsMatch.Success) {
        //        columnsGroup = columnsMatch.Groups[0];
        //        parts.SqlOrderBy = columnsGroup + parts.SqlCount.Substring(columnsGroup.Index + columnsGroup.Length);
        //        parts.SqlCount = parts.SqlCount.Substring(0, columnsGroup.Index);
        //    }

        //    return true;
        //}



        /// <summary>
        ///     Splits the given <paramref name="sql" /> into <paramref name="parts" />;
        /// </summary>
        /// <param name="sql">The SQL to split.</param>
        /// <param name="parts">The SQL parts.</param>
        /// <returns><c>True</c> if the SQL could be split; else, <c>False</c>.</returns>
        public bool SplitSQL(string sql, out SQLParts parts)
        {
            parts.Sql = sql;
            parts.SqlSelectRemoved = null;
            parts.SqlCount = sql;
            parts.SqlOrderBy = null;

            // Extract the columns from "SELECT <whatever> FROM"
            var columnsMatch = RegexColumns.Match(sql);
            if (!columnsMatch.Success)
                return false;

            // Look for the last "ORDER BY <whatever>" clause not part of a ROW_NUMBER expression
            var orderByMatch = RegexOrderBy.Match(sql);
            if (orderByMatch.Success) {
                parts.SqlOrderBy = orderByMatch.Value;
                parts.SqlCount = sql.Replace(orderByMatch.Value, string.Empty);
            }

            // Save column list and replace with COUNT(*)
            var columnsGroup = columnsMatch.Groups[1];
            parts.SqlSelectRemoved = sql.Substring(columnsGroup.Index);

            if (RegexDistinct.IsMatch(parts.SqlSelectRemoved) || SimpleRegexGroupBy.IsMatch(parts.SqlSelectRemoved)) {
                parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(*) FROM (" + parts.SqlCount + ") countAlias";
            } else {
                parts.SqlCount = sql.Substring(0, columnsGroup.Index) + "COUNT(*) " + parts.SqlCount.Substring(columnsGroup.Index + columnsGroup.Length);
            }

            return true;
        }

    }
}