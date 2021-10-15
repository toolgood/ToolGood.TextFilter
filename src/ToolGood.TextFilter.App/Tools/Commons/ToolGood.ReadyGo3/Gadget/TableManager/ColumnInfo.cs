using System;
using System.Reflection;
using ToolGood.ReadyGo3.Attributes;
using ToolGood.ReadyGo3.Internals;

namespace ToolGood.ReadyGo3.Gadget.TableManager
{
    public class ColumnInfo
    {
        private ColumnInfo() { }

        public string ColumnName;
        public string Comment;

        public string DefaultValue;
        public bool Required;


        public Type PropertyType;

        public string FieldLength;
        public bool IsText;

        internal static ColumnInfo FromProperty(PropertyInfo pi)
        {
            if (pi.CanRead == false || pi.CanWrite == false) return null;
            if (Types.IsAllowType(pi.PropertyType) == false) return null;
            var a = pi.GetCustomAttributes(typeof(IgnoreAttribute), true);
            if (a.Length > 0) return null;
            a = pi.GetCustomAttributes(typeof(ResultColumnAttribute), true);
            if (a.Length > 0) return null;

            ColumnInfo ci = new ColumnInfo {
                PropertyType = pi.PropertyType
            };

            a = pi.GetCustomAttributes(typeof(ColumnAttribute), true);
            ci.ColumnName = a.Length == 0 ? pi.Name : (a[0] as ColumnAttribute).Name;
            if (string.IsNullOrEmpty(ci.ColumnName)) ci.ColumnName = pi.Name;
            ci.Comment = a.Length == 0 ? null : (a[0] as ColumnAttribute).Comment;

            a = pi.GetCustomAttributes(typeof(DefaultValueAttribute), true);
            ci.DefaultValue = a.Length == 0 ? null : (a[0] as DefaultValueAttribute).DefaultValue;


            a = pi.GetCustomAttributes(typeof(FieldLengthAttribute), true);
            if (a.Length > 0) {
                ci.IsText = (a[0] as FieldLengthAttribute).IsText;
                ci.FieldLength = (a[0] as FieldLengthAttribute).FieldLength;
            }
            var atts = pi.GetCustomAttributes(typeof(RequiredAttribute), true);
            if (atts.Length > 0) {
                ci.Required = (atts[0] as RequiredAttribute).Required;
            } else {
                if (pi.PropertyType == typeof(string) || pi.PropertyType == typeof(AnsiString)) {
                    ci.Required = false;
                } else {
                    ci.Required = Types.IsNullType(ci.PropertyType) == false;
                }
            }
            ci.PropertyType = Types.GetBaseType(ci.PropertyType);
            return ci;
        }

    }
}
