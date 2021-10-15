using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.LinQ.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    class SqlExpression
    {
        private const string sep = " ";
        private readonly DatabaseProvider provider;

        /// <summary>
        /// SqlExpression
        /// </summary>
        /// <param name="type"></param>
        public SqlExpression(SqlType type)
        {
            this.provider = DatabaseProvider.Resolve(type);
        }
        public SqlExpression(DatabaseProvider provider)
        {
            this.provider = provider;
        }


        #region 可重写的方法
        private string GetQuotedValue(string paramValue)
        {
            var txt = (paramValue.ToString()).ToEscapeParam();
            return "'" + txt + "'";
        }
        private string GetQuotedValue(object value, Type fieldType)
        {
            if (value == null) return "NULL";

            if (fieldType.IsEnum) {
                if (EnumHelper.UseEnumString(fieldType)) {
                    var txt = (value.ToString()).ToEscapeParam();
                    return "'" + txt + "'";
                }
                return $"'{Convert.ToInt64(value)}'";
                //var isEnumFlags = fieldType.IsEnum;
                //long enumValue;
                //if (!isEnumFlags && Int64.TryParse(value.ToString(), out enumValue)) {
                //    value = Enum.ToObject(fieldType, enumValue).ToString();
                //}
                //var enumString = value.ToString();

                //return !isEnumFlags
                //    ? GetQuotedValue(enumString.Trim('"'))
                //    : enumString;
            }

            var typeCode = Type.GetTypeCode(fieldType);
            switch (typeCode) {
                case TypeCode.Boolean: return (bool)value ? "1" : "0";
                case TypeCode.Single: return ((float)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Double: return ((double)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Decimal: return ((decimal)value).ToString(CultureInfo.InvariantCulture);
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    //if (IsNumericType(fieldType))
                    return Convert.ChangeType(value, fieldType).ToString();
                    //break;
            }
            if (fieldType == typeof(DateTime)) return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (fieldType == typeof(TimeSpan)) return ((TimeSpan)value).Ticks.ToString(CultureInfo.InvariantCulture);
            if (fieldType == typeof(byte[])) {
                var txt = BitConverter.ToString((byte[])value).Replace("-","");
                return "X'" + txt + "'";
            }
            // TO： add 用于sqlite

            return GetQuotedValue(value.ToString());
        }
        private object GetQuotedTrueValue()
        {
            return new PartialSqlString("1");
        }
        private object GetQuotedFalseValue()
        {
            return new PartialSqlString("0");
        }

        private object VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(ObjectExtend))
                return VisitObjectExtendMethodCall(m);

            if (IsStaticArrayMethod(m))
                return VisitStaticArrayMethodCall(m);

            if (IsEnumerableMethod(m))
                return VisitEnumerableMethodCall(m);

            if (IsColumnAccess(m))
                return VisitColumnAccessMethod(m);

            return Expression.Lambda(m).Compile().DynamicInvoke();
        }
        private string VisitSqlMethodCall(MethodCallExpression call)
        {
            var callName = call.Method.Name;

            List<Object> args = new List<object>();
            var original = call.Arguments;
            string quotedColName = null;

            for (int i = 0, n = original.Count; i < n; i++) {
                var o = original[i];
                if (o.NodeType == ExpressionType.MemberAccess) {
                    quotedColName = getColumnName(o as MemberExpression);
                } else {
                    args.Add((o as ConstantExpression).Value);
                }
            }

            return string.Format("{0}({1}{2})",
                callName.ToUpper(), quotedColName,
                args.Count == 1 ? string.Format(",'{0}'", args[0]) : ""
            );
        }

        // String 类方法调用
        private object VisitColumnAccessMethod(MethodCallExpression m)
        {
            List<Object> _args = this.VisitExpressionList(m.Arguments);
            var quotedColName = Visit(m.Object);
            var wildcardArg = _args.Count > 0 ? _args[0] != null ? _args[0].ToString() : "" : "";
            string statement;
            switch (m.Method.Name) {
                case "Trim": statement = provider.CreateFunction(SqlFunction.Trim, quotedColName); break;
                case "TrimStart": statement = provider.CreateFunction(SqlFunction.LTrim, quotedColName); break;
                case "TrimEnd": statement = provider.CreateFunction(SqlFunction.RTrim, quotedColName); break;
                case "ToUpper": statement = provider.CreateFunction(SqlFunction.Upper, quotedColName); break;
                case "ToLower": statement = provider.CreateFunction(SqlFunction.Lower, quotedColName); break;
                case "StartsWith": statement = provider.CreateFunction(SqlFunction.Fuction, "{0} LIKE {1}", quotedColName, wildcardArg.ToEscapeLikeParam() + "%"); break;
                case "EndsWith": statement = provider.CreateFunction(SqlFunction.Fuction, "{0} LIKE {1}", quotedColName, "%" + wildcardArg.ToEscapeLikeParam()); break;
                case "Contains": statement = provider.CreateFunction(SqlFunction.Fuction, "{0} LIKE {1}", quotedColName, "%" + wildcardArg.ToEscapeLikeParam() + "%"); break;
                case "Substring":
                    var startIndex = Int32.Parse(_args[0].ToString()) + 1;
                    if (_args.Count == 1) {
                        statement = provider.CreateFunction(SqlFunction.SubString2, quotedColName, startIndex);
                        break;
                    }
                    var length = Int32.Parse(_args[1].ToString());
                    statement = provider.CreateFunction(SqlFunction.SubString3, quotedColName, startIndex, length);
                    break;
                case "Equals":
                    wildcardArg = GetQuotedValue(wildcardArg);
                    statement = $"({quotedColName} = {wildcardArg})"; break;
                case "Concat":
                    var args = new List<object>();
                    args.Add(quotedColName);
                    args.AddRange(_args);
                    statement = provider.CreateFunction(SqlFunction.Concat, args.ToArray()); break;
                case "ToString": statement = quotedColName.ToString(); break;
                case "IndexOf": statement = provider.CreateFunction(SqlFunction.IndexOf, quotedColName, _args[0]); break;
                case "Replace": statement = provider.CreateFunction(SqlFunction.Replace, quotedColName, _args[0], _args[1]); break;
                default: throw new NotSupportedException();
            }
            return new PartialSqlString(statement);
        }


        #endregion


        #region Analysis getColumnName
        /// <summary>
        /// 获取所有列
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="sql"></param>
        public void GetColumns(LambdaExpression exp, out string sql)
        {
            if (exp.Body.NodeType == ExpressionType.New) {
                sql = Visit(exp).ToString();
            } else if (exp.Body.NodeType == ExpressionType.MemberAccess) {
                sql = getColumnName(exp.Body as MemberExpression);

            } else if (exp.Body.NodeType != ExpressionType.Call) {
                sql = getColumnName((exp.Body as UnaryExpression).Operand.Reduce() as MemberExpression);

            } else {
                var call = exp.Body as MethodCallExpression;
                sql = VisitSqlMethodCall(call);
            }
        }
        /// <summary>
        /// 分析LambdaExpression
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="sql"></param>
        public void Analysis(LambdaExpression exp, out string sql)
        {
            sql = Visit(exp).ToString();
        }
        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetColumnName(LambdaExpression exp)
        {
            if (exp.Body.NodeType == ExpressionType.MemberAccess) {
                return getColumnName(exp.Body as MemberExpression);
            }
            if (exp.Body.NodeType != ExpressionType.Call) {
                return getColumnName((exp.Body as UnaryExpression).Operand.Reduce() as MemberExpression);
            }

            var call = exp.Body as MethodCallExpression;
            return VisitSqlMethodCall(call);
        }

        private string getColumnName(MemberExpression m)
        {
            var colName = m.Member.Name;
            var p = m.Expression as ParameterExpression;
            var tableDef = PocoData.ForType(p.Type);

            var col = tableDef.Columns.Where(q => q.Value.PropertyName == colName).Select(q => q.Value).FirstOrDefault();
            if (col != null) {
                colName = col.ColumnName;
            }
            return provider.EscapeSqlIdentifier(colName);
        }

        #endregion getColumnName

        #region GetSelectSql

        private object GetTrueExpression()
        {
            return new PartialSqlString($"({GetQuotedTrueValue().ToString()}={GetQuotedTrueValue().ToString()})");
        }

        private object GetFalseExpression()
        {
            return new PartialSqlString($"({GetQuotedTrueValue().ToString()}={GetQuotedFalseValue().ToString()})");
        }



        #endregion GetSelectSql

        #region Expression Visit

        private object Visit(Expression exp)
        {
            if (exp == null) return string.Empty;
            switch (exp.NodeType) {
                case ExpressionType.Lambda: return VisitLambda(exp as LambdaExpression);
                case ExpressionType.MemberAccess: return VisitMemberAccess(exp as MemberExpression);
                case ExpressionType.Constant: return VisitConstant(exp as ConstantExpression);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr: return VisitBinary(exp as BinaryExpression);
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs: return VisitUnary(exp as UnaryExpression);
                case ExpressionType.Parameter: return VisitParameter(exp as ParameterExpression);
                case ExpressionType.Call: return VisitMethodCall(exp as MethodCallExpression);
                case ExpressionType.New: return VisitNew(exp as NewExpression);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds: return VisitNewArray(exp as NewArrayExpression);
                case ExpressionType.MemberInit: return VisitMemberInit(exp as MemberInitExpression);
                case ExpressionType.Conditional:
                    return VisitConditional(exp as ConditionalExpression);
                default: return exp.ToString();
            }
        }
        private object VisitConditional(ConditionalExpression conditional)
        {
            var test = Visit(conditional.Test);
            var trueSql = Visit(conditional.IfTrue);
            var falseSql = Visit(conditional.IfFalse);

            return new PartialSqlString($"(case when {test} then {trueSql} else {falseSql} end)");
        }
        private object VisitLambda(LambdaExpression lambda)
        {
            if (lambda.Body.NodeType == ExpressionType.MemberAccess) {
                MemberExpression m = lambda.Body as MemberExpression;

                if (m.Expression != null) {
                    string r = VisitMemberAccess(m).ToString();
                    //if (m.Member.ReflectedType == typeof(DateTime?) && m.Member.Name== "HasValue") {
                    //    return r;
                    //}
                    return $"{r}={GetQuotedTrueValue()}";
                }
            }
            return Visit(lambda.Body);
        }

        private object VisitBinary(BinaryExpression b)
        {
            var operand = BindOperant(b.NodeType);   //sep= " " ??
            if (operand == "AND" || operand == "OR") {
                return VisitBinary_And_Or(b, operand);
            }
            object left = Visit(b.Left);
            object right = Visit(b.Right);

            if (left as PartialSqlString == null && right as PartialSqlString == null) {
                var result = Expression.Lambda(b).Compile().DynamicInvoke();
                return result;
            } else {
                if (left as PartialSqlString == null) {
                    left = GetQuotedValue(left, left?.GetType());
                    return CreatePartialSqlString(left, operand, right);
                } else if (right as PartialSqlString == null) {
                    if (right == null) {
                        right = GetQuotedValue(right, null);
                        if (operand == "=") operand = "IS";
                        else if (operand == "<>") operand = "IS NOT";
                        return new PartialSqlString(left + sep + operand + sep + right);
                    } else {
                        right = GetQuotedValue(right, right?.GetType());
                        return CreatePartialSqlString(left, operand, right);
                    }
                }
            }

            if (operand == "=" && right.ToString().Equals("null", StringComparison.OrdinalIgnoreCase)) operand = "IS";
            else if (operand == "<>" && right.ToString().Equals("null", StringComparison.OrdinalIgnoreCase)) operand = "IS NOT";

            return CreatePartialSqlString(left, operand, right);
        }

        private PartialSqlString CreatePartialSqlString(object left, string operand, object right)
        {
            if (operand == "MOD" || operand == "COALESCE") {
                return new PartialSqlString($"{operand}({left},{right})");
            }
            return new PartialSqlString(left + sep + operand + sep + right);
        }

        private object VisitBinary_And_Or(BinaryExpression b, string operand)
        {
            object left, right;
            var m = b.Left as MemberExpression;
            if (m != null && m.Expression != null
                && m.Expression.NodeType == ExpressionType.Parameter)
                left = new PartialSqlString(string.Format("{0}={1}", VisitMemberAccess(m), GetQuotedTrueValue()));
            else
                left = Visit(b.Left);

            m = b.Right as MemberExpression;
            if (m != null && m.Expression != null
                && m.Expression.NodeType == ExpressionType.Parameter)
                right = new PartialSqlString(string.Format("{0}={1}", VisitMemberAccess(m), GetQuotedTrueValue()));
            else
                right = Visit(b.Right);

            if (left as PartialSqlString == null && right as PartialSqlString == null) {
                var result = Expression.Lambda(b).Compile().DynamicInvoke();
                return new PartialSqlString(GetQuotedValue(result, result.GetType()));
            }

            if (left as PartialSqlString == null)
                left = ((bool)left) ? GetTrueExpression() : GetFalseExpression();
            if (right as PartialSqlString == null)
                right = ((bool)right) ? GetTrueExpression() : GetFalseExpression();
            if (operand == "OR") {
                return new PartialSqlString("(" + left + sep + operand + sep + right + ")");
            }
            return new PartialSqlString(left + sep + operand + sep + right);
        }

        private object VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null
                && (m.Expression.NodeType == ExpressionType.Parameter || m.Expression.NodeType == ExpressionType.Convert)) {
                var me = m;
                if (m.Expression.NodeType == ExpressionType.Convert) {
                    me = (m.Expression as UnaryExpression).Operand.Reduce() as MemberExpression;
                }

                var p = me.Expression as ParameterExpression;
                var tableDef = PocoData.ForType(p.Type);
                var colName = m.Member.Name;

                var col = tableDef.Columns.Where(q => q.Value.PropertyName == colName).Select(q => q.Value).FirstOrDefault();
                if (col != null) {
                    colName = col.ColumnName;
                }
                return new PartialSqlString(colName);
            }
            if (m.Member.DeclaringType == typeof(DateTime) || m.Member.DeclaringType == typeof(DateTime?)) {
                var m1 = m.Expression as MemberExpression;
                if (m1 != null) {
                    var p = Expression.Convert(m1, typeof(object));
                    if (p.NodeType == ExpressionType.Convert) {
                        var pp = m1.Expression as ParameterExpression;
                        if (pp == null) {
                            m1 = m1.Expression as MemberExpression;
                            if (m1 != null) { pp = m1.Expression as ParameterExpression; }
                        }

                        if (pp != null) {
                            string sql = null;
                            switch (m.Member.Name) {
                                case "Year": sql = provider.CreateFunction(SqlFunction.Year, Visit(m1)); break;
                                case "Month": sql = provider.CreateFunction(SqlFunction.Month, Visit(m1)); break;
                                case "Day": sql = provider.CreateFunction(SqlFunction.Day, Visit(m1)); break;
                                case "Hour": sql = provider.CreateFunction(SqlFunction.Hour, Visit(m1)); break;
                                case "Minute": sql = provider.CreateFunction(SqlFunction.Minute, Visit(m1)); break;
                                case "Second": sql = provider.CreateFunction(SqlFunction.Second, Visit(m1)); break;
                                case "Value": return Visit(m1);
                                case "DayOfWeek": sql = provider.CreateFunction(SqlFunction.WeekDay, Visit(m1)); break;
                                case "DayOfYear": sql = provider.CreateFunction(SqlFunction.DayOfYear, Visit(m1)); break;

                                //case "HasValue": sql = Visit(m1).ToString() + " IS NOT NULL "; break;
                                default: throw new NotSupportedException("Not Supported " + m.Member.Name);
                            }
                            return new PartialSqlString(sql);
                        }
                    }
                }
            }

            var member = Expression.Convert(m, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            return getter();
        }

        private object VisitMemberInit(MemberInitExpression exp)
        {
            return Expression.Lambda(exp).Compile().DynamicInvoke();
        }

        private object VisitNew(NewExpression nex)
        {
            var member = Expression.Convert(nex, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            try {
                return lambda.Compile()();
            } catch (System.InvalidOperationException) {
                List<PartialSqlString> exprs = VisitExpressionList(nex.Arguments).OfType<PartialSqlString>().ToList();
                StringBuilder r = new StringBuilder();
                for (int i = 0; i < exprs.Count; i++) {
                    if (i != 0) r.Append(", ");
                    r.Append(exprs[i]);
                    r.Append(" AS ");
                    r.Append(nex.Members[i].Name);
                }
                return r.ToString();
            }
        }

        private object VisitParameter(ParameterExpression p)
        {
            return "";
        }

        private object VisitConstant(ConstantExpression c)
        {
            if (c.Value == null) return new PartialSqlString("NULL");
            return c.Value;
        }

        private object VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType) {
                case ExpressionType.Not:
                    var o = Visit(u.Operand);
                    if (o as PartialSqlString == null) return !((bool)o);
                    if (u.Operand.NodeType == ExpressionType.MemberAccess) {
                        o = o + "=" + GetQuotedTrueValue();
                    }
                    return new PartialSqlString("NOT (" + o + ")");

                case ExpressionType.Convert:
                    if (u.Method != null)
                        return Expression.Lambda(u).Compile().DynamicInvoke();
                    break;
            }
            return Visit(u.Operand);
        }


        private string BindOperant(ExpressionType e)
        {
            switch (e) {
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.AndAlso: return "AND";
                case ExpressionType.OrElse: return "OR";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "MOD";
                case ExpressionType.Coalesce: return "COALESCE";
                default: return e.ToString();
            }
        }


        #endregion Expression Visit

        #region IsNumericType IsOrHasGenericInterfaceTypeOf

        private bool IsOrHasGenericInterfaceTypeOf(Type type, Type genericTypeDefinition)
        {
            if (GetTypeWithGenericTypeDefinitionOf(type, genericTypeDefinition) == null) {
                return (type == genericTypeDefinition);
            }
            return true;
        }

        private Type GetTypeWithGenericTypeDefinitionOf(Type type, Type genericTypeDefinition)
        {
            foreach (Type t in type.GetInterfaces()) {
                if (t.IsGenericType && (t.GetGenericTypeDefinition() == genericTypeDefinition)) {
                    return t;
                }
            }
            Type genericType = GetGenericType(type);
            if ((genericType != null) && (genericType.GetGenericTypeDefinition() == genericTypeDefinition)) {
                return genericType;
            }
            return null;
        }

        private Type GetGenericType(Type type)
        {
            while (type != null) {
                if (type.IsGenericType) {
                    return type;
                }
                type = type.BaseType;
            }
            return null;
        }

        #endregion IsNumericType IsOrHasGenericInterfaceTypeOf

        #region 分析方法调用


        private bool IsStaticArrayMethod(MethodCallExpression m)
        {
            if (m.Object == null && m.Method.Name == "Contains") {
                return m.Arguments.Count == 2;
            }
            return false;
        }

        private object VisitStaticArrayMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name) {
                case "Contains":
                    List<Object> _args = this.VisitExpressionList(m.Arguments);
                    object quotedColName = _args[1];

                    Expression memberExpr = m.Arguments[0];
                    if (memberExpr.NodeType == ExpressionType.MemberAccess)
                        memberExpr = (m.Arguments[0] as MemberExpression);

                    return ToInPartialString(memberExpr, quotedColName, "IN");

                default:
                    throw new NotSupportedException();
            }
        }

        private bool IsEnumerableMethod(MethodCallExpression m)
        {
            if (m.Object != null
                && IsOrHasGenericInterfaceTypeOf(m.Object.Type, typeof(IEnumerable<>))
                && m.Object.Type != typeof(string)
                && m.Method.Name == "Contains") {
                return m.Arguments.Count == 1;
            }
            return false;
        }

        private object VisitEnumerableMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name) {
                case "Contains":
                    List<Object> _args = this.VisitExpressionList(m.Arguments);
                    object quotedColName = _args[0];
                    return ToInPartialString(m.Object, quotedColName, "IN");

                default:
                    throw new NotSupportedException();
            }
        }

        private object ToInPartialString(Expression memberExpr, object quotedColName, string option)
        {
            var member = Expression.Convert(memberExpr, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            List<object> inArgs = new List<object>();
            foreach (var item in getter() as IEnumerable) {
                inArgs.Add(item);
            }
            if (inArgs.Count == 0) {
                if (option == "IN") {
                    return new PartialSqlString("1 <> 1");
                } else {
                    return new PartialSqlString("1 == 1");
                }
            }
            if (inArgs.Count == 1) {
                if (option == "IN") {
                    return new PartialSqlString(string.Format("{0}={1}", quotedColName, GetQuotedValue(inArgs[0], inArgs[0].GetType())));
                } else {
                    return new PartialSqlString(string.Format("{0}<>{1}", quotedColName, GetQuotedValue(inArgs[0], inArgs[0].GetType())));
                }
            }

            var sIn = new StringBuilder();
            if (inArgs.Count > 0) {
                foreach (object e in inArgs) {
                    if (sIn.Length > 0)
                        sIn.Append(",");
                    sIn.Append(GetQuotedValue(e, e.GetType()));
                }
            }

            var statement = $"{quotedColName} {option} ({sIn})";
            return new PartialSqlString(statement);
        }

        private bool IsColumnAccess(MethodCallExpression m)
        {
            if (m.Object != null && m.Object as MethodCallExpression != null)
                return IsColumnAccess(m.Object as MethodCallExpression);

            var exp = m.Object as MemberExpression;
            return exp != null
                   && exp.Expression != null
                   && exp.Expression.NodeType == ExpressionType.Parameter;
        }


        private object VisitObjectExtendMethodCall(MethodCallExpression m)
        {
            if (m.Arguments[0].NodeType != ExpressionType.MemberAccess) {
                return Expression.Lambda(m).Compile().DynamicInvoke();
            }
            var quotedColName = VisitMemberAccess((MemberExpression)m.Arguments[0]);
            var option = m.Method.Name == "IsIn" ? "IN" : "NOT IN";
            return ToInPartialString(m.Arguments[1], quotedColName, option);
        }

        #endregion 分析方法调用

        #region 获取 集合

        private List<Object> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            List<Object> list = new List<Object>();
            for (int i = 0, n = original.Count; i < n; i++) {
                if (original[i].NodeType == ExpressionType.NewArrayInit ||
                    original[i].NodeType == ExpressionType.NewArrayBounds)
                    list.AddRange(VisitNewArrayFromExpressionList(original[i] as NewArrayExpression));
                else
                    list.Add(Visit(original[i]));
            }
            return list;
        }

        private object VisitNewArray(NewArrayExpression na)
        {
            return string.Join(",", VisitExpressionList(na.Expressions));
        }

        private List<Object> VisitNewArrayFromExpressionList(NewArrayExpression na)
        {
            return VisitExpressionList(na.Expressions);
        }

        #endregion 获取 集合

    }
}
