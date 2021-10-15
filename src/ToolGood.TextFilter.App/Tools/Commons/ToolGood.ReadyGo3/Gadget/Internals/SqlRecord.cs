using System.Text;

namespace ToolGood.ReadyGo3.Gadget.Internals
{
    /// <summary>
    /// SQL 配置信息
    /// </summary>
    public class SqlRecord
    {
        internal SqlRecord() { }


        /// <summary>
        /// 上次Sql语句
        /// </summary>
        public string LastSQL { get; internal set; }

        /// <summary>
        /// 上次Sql语句的参数
        /// </summary>
        public object[] LastArgs { get; internal set; }

        /// <summary>
        /// 上次错误信息
        /// </summary>
        public string LastErrorMessage { get; internal set; }

        /// <summary>
        /// 上次Sql语句 带参数 （格式化）
        /// </summary>
        public string LastCommand {
            get { return FormatCommand(LastSQL, LastArgs); }
        }

        internal static string FormatCommand(string sql, object[] args)
        {
            if (string.IsNullOrEmpty(sql)) return "";
            var sb = new StringBuilder();
            sb.Append(sql);
            if (args != null && args.Length > 0) {
                sb.Append("\n");
                for (int i = 0; i < args.Length; i++) {
                    sb.AppendFormat("\t -> ({0})[{1}] = \"{2}\"\n", i, args[i].GetType().Name, args[i]);
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}