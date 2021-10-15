using System.Reflection;
using ToolGood.ReadyGo3.Attributes;
using ToolGood.ReadyGo3.Internals;

namespace ToolGood.ReadyGo3.PetaPoco
{
    /// <summary>
    /// 使用PocoData.ForObject或PocoData.ForType来解析
    /// </summary>
    public class ColumnInfo
    {
        private ColumnInfo() { }

        /// <summary>
        ///     列名
        /// </summary>
        public string ColumnName;

        /// <summary>
        ///     属性名
        /// </summary>
        public string PropertyName;

        /// <summary>
        ///     是否为返回列，即没有真实的列
        /// </summary>
        public bool ResultColumn;

        /// <summary>
        ///     返回列的sql语句，注，{0} 为当前列的别名.
        ///     Select UserName from User as u where u.UserId={0}UserId
        /// </summary>
        public string ResultSql;

        /// <summary>
        /// DateTime为Utc
        /// </summary>
        public bool ForceToUtc;




        /// <summary>
        ///     Creates and populates a ColumnInfo from the attributes of a POCO property.
        /// </summary>
        /// <param name="pi">The property whose column info is required</param>
        /// <returns>A ColumnInfo instance</returns>
        internal static ColumnInfo FromProperty(PropertyInfo pi)
        {
            if (pi.CanRead == false || pi.CanWrite == false) return null;
            if (Types.IsAllowType(pi.PropertyType) == false) return null;

            var explicitColumns = pi.DeclaringType.GetCustomAttributes(typeof(ExplicitColumnsAttribute), true).Length > 0;
            // Check for [Column]/[Ignore] Attributes
            var colAttrs = pi.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (explicitColumns) {
                if (colAttrs.Length == 0)
                    return null;
            } else {
                if (pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Length != 0)
                    return null;
            }

            if (pi.GetCustomAttributes(typeof(IgnoreAttribute), true).Length > 0) return null;

            var ci = new ColumnInfo {
                PropertyName = pi.Name
            };

            if (colAttrs.Length > 0) {
                var colattr = (ColumnAttribute)colAttrs[0];

                ci.ColumnName = colattr.Name ?? pi.Name;
                if (string.IsNullOrEmpty(ci.ColumnName)) ci.ColumnName = pi.Name;

                ci.ForceToUtc = colattr.ForceToUtc;
                if (colattr is ResultColumnAttribute) {
                    ci.ResultColumn = true;
                    ci.ResultSql = (colattr as ResultColumnAttribute).Definition;
                }
            } else {
                ci.ColumnName = pi.Name;
                ci.ForceToUtc = false;
                ci.ResultColumn = false;
            }
            return ci;
        }

    }
}