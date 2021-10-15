using System.Collections.Generic;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.PetaPoco.Internal
{
    internal class ExpandoColumn : PocoColumn
    {
        public override void SetValue(object target, object val)
        {
            (target as IDictionary<string, object>)[ColumnName] = val;
        }

        public override object GetValue(object target)
        {
            (target as IDictionary<string, object>).TryGetValue(ColumnName, out object val);
            return val;
        }

        public override object ChangeType(object val)
        {
            return val;
        }
    }
}