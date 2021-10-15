using System;
using System.Reflection;

namespace ToolGood.ReadyGo3.PetaPoco.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class PocoColumn
    {
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
        ///    当前属性的PropertyInfo
        /// </summary>
        public PropertyInfo PropertyInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="val"></param>
        public virtual void SetValue(object target, object val)
        {
            PropertyInfo.SetValue(target, val, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual object GetValue(object target)
        {
            return PropertyInfo.GetValue(target, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public virtual object ChangeType(object val)
        {
            var t = PropertyInfo.PropertyType;
            if (val.GetType().IsValueType && PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                t = t.GetGenericArguments()[0];

            return Convert.ChangeType(val, t);
        }
    }
}