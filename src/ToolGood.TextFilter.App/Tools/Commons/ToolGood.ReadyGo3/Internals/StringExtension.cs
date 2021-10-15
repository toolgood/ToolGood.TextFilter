namespace ToolGood.ReadyGo3
{
    internal static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string ToEscapeParam(this string stringValue)
        {
            return stringValue.Replace(@"\", @"\\").Replace("'", "''")
                                  .Replace("\0", "\\0").Replace("\a", "\\a").Replace("\b", "\\b")
                                  .Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r")
                                  .Replace("\t", "\\t").Replace("\v", "\\v");
        }
        public static string ToEscapeLikeParam(this string param)
        {
            return param.ToEscapeParam()
                .Replace("_", @"\_")
                .Replace("%", @"\%")
                .Replace("'", @"\'")
                .Replace("[", @"\[")
                .Replace("]", @"\]");
        }




    }
}
