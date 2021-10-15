using System;
using System.Linq;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.ReadyGo3.PetaPoco
{
    /// <summary>
    /// 使用PocoData.ForObject或PocoData.ForType来解析
    /// </summary>
    public class TableInfo
    {
        internal TableInfo() { }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName;

        /// <summary>
        ///     模式名;
        /// </summary>
        public string SchemaName;

        /// <summary>
        ///     表名
        /// </summary>
        public string TableName;

        ///// <summary>
        ///// 配置名
        ///// </summary>
        //public string SettingName;

        /// <summary>
        ///     主键
        /// </summary>
        public string PrimaryKey;

        /// <summary>
        ///    Oracle sequence
        /// </summary>
        public string SequenceName;

        /// <summary>
        ///     自增长
        /// </summary>
        public bool AutoIncrement;


        /// <summary>
        ///     Creates and populates a TableInfo from the attributes of a POCO
        /// </summary>
        /// <param name="t">The POCO type</param>
        /// <returns>A TableInfo instance</returns>
        internal static TableInfo FromPoco(Type t)
        {
            TableInfo ti = new TableInfo();
            var a = t.GetCustomAttributes(typeof(TableAttribute), true);
            if (a.Length > 0) {
                var ta = (a[0] as TableAttribute);
                ti.DatabaseName = ta.DatabaseName;
                ti.SchemaName = ta.SchemaName;
                ti.TableName = ta.TableName;
                //ti.SettingName = ta.SettingName;
            } else {
                ti.TableName = t.Name;
            }

            a = t.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            ti.PrimaryKey = a.Length == 0 ? null : (a[0] as PrimaryKeyAttribute).PrimaryKey;
            ti.AutoIncrement = a.Length == 0 ? false : (a[0] as PrimaryKeyAttribute).AutoIncrement;
            ti.SequenceName = a.Length == 0 ? null : (a[0] as PrimaryKeyAttribute).SequenceName;

            if (string.IsNullOrEmpty(ti.PrimaryKey)) {
                var prop = t.GetProperties().FirstOrDefault(p => {
                    if (p.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (p.Name.Equals(t.Name + "id", StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (p.Name.Equals(t.Name + "_id", StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (p.Name.Equals(ti.TableName + "id", StringComparison.OrdinalIgnoreCase))
                        return true;
                    if (p.Name.Equals(ti.TableName + "_id", StringComparison.OrdinalIgnoreCase))
                        return true;
                    return false;
                });

                if (prop != null) {
                    ti.PrimaryKey = prop.Name;
                    ti.AutoIncrement = prop.PropertyType.IsValueType;
                }
            }


            return ti;
        }
    }
}