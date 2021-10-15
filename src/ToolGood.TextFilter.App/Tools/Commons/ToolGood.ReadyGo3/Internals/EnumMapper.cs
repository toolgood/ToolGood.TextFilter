using System;
using System.Collections.Generic;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.ReadyGo3.Internals
{
    internal static class EnumHelper
    {
        private static readonly Cache<Type, Dictionary<string, object>> _types = new Cache<Type, Dictionary<string, object>>();
        private static readonly Cache<Type, bool> _useString = new Cache<Type, bool>();

        public static object EnumFromString(Type enumType, string value)
        {
            Dictionary<string, object> map = _types.Get(enumType, () => {
                var values = Enum.GetValues(enumType);
                var newmap = new Dictionary<string, object>(values.Length, StringComparer.InvariantCultureIgnoreCase);
                foreach (var v in values) {
                    newmap.Add(v.ToString(), v);
                }
                return newmap;
            });
            return map[value];
        }

        public static bool UseEnumString(Type enumType)
        {
            return _useString.Get(enumType, () => {
                var atts = enumType.GetCustomAttributes(typeof(EnumStringAttribute), true);
                return atts.Length > 0;
            });
        }



    }
}