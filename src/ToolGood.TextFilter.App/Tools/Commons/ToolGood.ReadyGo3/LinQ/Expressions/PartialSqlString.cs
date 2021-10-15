namespace ToolGood.ReadyGo3.LinQ.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public class PartialSqlString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public PartialSqlString(string text)
        {
            Text = text;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Text;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
