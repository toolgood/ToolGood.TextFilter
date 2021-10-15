using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3
{
      partial class SqlHelper
    {
        #region Select Update

        #region FirstOrDefault PK
        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(int condition) where T : class
        {
            return SingleOrDefaultById<T>(condition);
        }

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(uint condition) where T : class
        {
            return SingleOrDefaultById<T>(condition);
        }


        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(long condition) where T : class
        {
            return SingleOrDefaultById<T>(condition);
        }

        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(ulong condition) where T : class
        {
            return SingleOrDefaultById<T>(condition);
        }


        #endregion
        /// <summary>
        /// 根据条件查询第一个
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public T FirstOrDefault<T>(object condition) where T : class
        {
            return FirstOrDefault<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit">个数</param>
        /// <param name="offset">位移</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<T> Select<T>(int limit, int offset, object condition) where T : class
        {
            return Select<T>(limit, offset, ConditionObjectToWhere(condition));
        }


        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit">个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<T> Select<T>(int limit, object condition) where T : class
        {
            return Select<T>(limit, ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<T> Select<T>(object condition) where T : class
        {
            return Select<T>(ConditionObjectToWhere(condition));
        }
 

        /// <summary>
        /// 根据条件查询页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<T> SelectPage<T>(int page, int itemsPerPage, object condition)
            where T : class
        {
            return SelectPage<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Page<T> Page<T>(int page, int itemsPerPage, object condition)
            where T : class
        {
            return Page<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }


        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        public int Update<T>(object set, object condition, IEnumerable<string> ignoreFields = null) where T : class
        {
            return Update<T>(ConditionObjectToUpdateSetWhere(set, condition, ignoreFields));
        }



        /// <summary>
        /// 根据条件从数据库中删除对象
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int Delete<T>(object condition) where T : class
        {
            return Delete<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int Count<T>(object condition) where T : class
        {
            return Count<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public bool Exists<T>(object condition) where T : class
        {
            if (condition.GetType().IsClass) {
                return Exists<T>(ConditionObjectToWhere(condition));
            } else {
                var pd = PocoData.ForType(typeof(T));
                var table = _provider.GetTableName(pd);
                var pk = _provider.EscapeSqlIdentifier(pd.TableInfo.PrimaryKey);
                var sql = $"SELECT COUNT(*) FROM {table} WHERE {pk}=@0";

                var args = new object[] { condition };
                return GetDatabase().ExecuteScalar<int>(sql, args) > 0;
            }
        }


        #endregion

#if !NET40


        #region FirstOrDefault_Async PK
        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefault_Async<T>(int condition) where T : class
        {
            return SingleOrDefaultById_Async<T>(condition);
        }

        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefault_Async<T>(uint condition) where T : class
        {
            return SingleOrDefaultById_Async<T>(condition);
        }

        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefault_Async<T>(long condition) where T : class
        {
            return SingleOrDefaultById_Async<T>(condition);
        }

        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefault_Async<T>(ulong condition) where T : class
        {
            return SingleOrDefaultById_Async<T>(condition);
        }

        #endregion

        /// <summary>
        /// 根据条件查询第一个，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<T> FirstOrDefault_Async<T>(object condition) where T : class
        {
            return FirstOrDefault_Async<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="limit">个数</param>
        /// <param name="offset">位移</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<List<T>> Select_Async<T>(int limit, int offset, object condition) where T : class
        {
            return Select_Async<T>(limit, offset, ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="limit">个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<List<T>> Select_Async<T>(int limit, object condition) where T : class
        {
            return Select_Async<T>(limit, ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<List<T>> Select_Async<T>(object condition) where T : class
        {
            return Select_Async<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        ///  根据条件查询页，异步操作
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<List<T>> SelectPage_Async<T>(int page, int itemsPerPage, object condition)
            where T : class
        {
            return SelectPage_Async<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }

        /// <summary>
        ///  根据条件查询页，异步操作
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页个数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<Page<T>> Page_Async<T>(int page, int itemsPerPage, object condition)
            where T : class
        {
            return Page_Async<T>(page, itemsPerPage, ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件更新对象
        /// </summary>
        /// <param name="set"></param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        public Task<int> Update_Async<T>(object set, object condition, IEnumerable<string> ignoreFields = null) where T : class
        {
            return Update_Async<T>(ConditionObjectToUpdateSetWhere(set, condition, ignoreFields));
        }

        /// <summary>
        /// 根据条件从数据库中删除对象 
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<int> Delete_Async<T>(object condition) where T : class
        {
            return Delete_Async<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件查询个数，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Task<int> Count_Async<T>(object condition) where T : class
        {
            return Count_Async<T>(ConditionObjectToWhere(condition));
        }

        /// <summary>
        /// 根据条件是判断否存在，异步操作
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public async Task<bool> Exists_Async<T>(object condition) where T : class
        {
            if (condition.GetType().IsClass) {
                return await Exists_Async<T>(ConditionObjectToWhere(condition));
            } else {
                var pd = PocoData.ForType(typeof(T));
                var table = _provider.GetTableName(pd);
                var pk = _provider.EscapeSqlIdentifier(pd.TableInfo.PrimaryKey);
                var sql = $"SELECT COUNT(*) FROM {table} WHERE {pk}=@0";

                var args = new object[] { condition };
                return await GetDatabase().ExecuteScalar_Async<int>(sql, args) > 0;
            }
        }

#endif


        private string ConditionObjectToWhere(object condition)
        {
            if (condition == null) { return ""; }
            if (condition.GetType() == typeof(string)) { return (string)condition; }


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("WHERE ");
            ObjectToSql(stringBuilder, condition, " AND ", null);
            return stringBuilder.ToString();
        }

        private string ConditionObjectToUpdateSetWhere(object set, object condition, IEnumerable<string> ignoreFields)
        {
            if (set == null) { throw new ArgumentException("set is  null object!"); }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SET ");
            ObjectToSql(stringBuilder, set, ",", ignoreFields);
            if (condition != null) {
                if (condition.GetType() == typeof(string)) {
                    var str = ((string)condition).Trim();
                    if (str.StartsWith("WHERE ", StringComparison.CurrentCultureIgnoreCase) == false) {
                        stringBuilder.Append(" WHERE");
                    }
                    stringBuilder.Append(" ");
                    stringBuilder.Append(str);
                    return stringBuilder.ToString();
                }
                stringBuilder.Append(" WHERE ");
                ObjectToSql(stringBuilder, condition, " AND ", null);
            }
            return stringBuilder.ToString();
        }

        private void ObjectToSql(StringBuilder stringBuilder, object condition, string middelStr, IEnumerable<string> ignoreFields)
        {
            if (condition is IEnumerable) { throw new ArgumentException("condition is IEnumerable object!"); }
            bool hasColumn = false;

            var type = condition.GetType();
            var pis = type.GetProperties();
            for (int i = 0; i < pis.Length; i++) {
                var pi = pis[i];
                if (ignoreFields != null) {
                    if (ignoreFields.Any(q => string.Equals(q, pi.Name, StringComparison.CurrentCultureIgnoreCase))) {
                        continue;
                    }
                }
                if (hasColumn == false) {
                    hasColumn = true;
                } else {
                    stringBuilder.Append(middelStr);
                }

                var pt = pi.PropertyType;
                var value = pi.GetGetMethod().Invoke(condition, null);
                if (middelStr == " AND ") {
                    if (value == null) {
                        stringBuilder.Append($"[{pi.Name}] is Null");
                    } else {
                        if (value is IEnumerable && !(value is string)) {
                            List<object> objs = new List<object>();
                            foreach (var item in (IEnumerable)value) { objs.Add(item); }

                            if (objs.Count == 0) {
                                stringBuilder.Append($"1=2");
                            } else if (objs.Count == 1) {
                                stringBuilder.Append($"[{pi.Name}]=");
                                stringBuilder.Append(EscapeParam(objs[0]));
                            } else {
                                stringBuilder.Append($"[{pi.Name}] in (");
                                for (int j = 0; j < objs.Count; j++) {
                                    if (j > 0) { stringBuilder.Append(","); }
                                    stringBuilder.Append(EscapeParam(objs[j]));
                                }
                                stringBuilder.Append($")");
                            }
                        } else {
                            stringBuilder.Append($"[{pi.Name}]=");
                            stringBuilder.Append(EscapeParam(value));
                        }
                    }
                } else {
                    stringBuilder.Append($"[{pi.Name}]=");
                    stringBuilder.Append(EscapeParam(value));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string EscapeParam(object value)
        {
            if (object.Equals(value, null)) return "NULL";

            var fieldType = value.GetType();
            if (fieldType.IsEnum) {
                if (EnumHelper.UseEnumString(fieldType)) {
                    var txt = (value.ToString()).ToEscapeParam();
                    return "'" + txt + "'";
                }
                return $"'{Convert.ToInt64(value)}'";

                //var isEnumFlags = fieldType.IsEnum;
                //if (!isEnumFlags && Int64.TryParse(value.ToString(), out long enumValue)) {
                //    value = Enum.ToObject(fieldType, enumValue).ToString();
                //}
                //var enumString = value.ToString();
                //return !isEnumFlags ? "'" + enumString.Trim('"') + "'" : enumString;
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
                case TypeCode.UInt64: return value.ToString();
                default: break;
            }
            if (value is string || value is char) {
                var txt = (value.ToString()).ToEscapeParam();
                return "'" + txt + "'";
            }
            if (fieldType == typeof(DateTime)) return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (fieldType == typeof(TimeSpan)) return ((TimeSpan)value).Ticks.ToString(CultureInfo.InvariantCulture);
            if (fieldType == typeof(byte[])) {
                var txt = BitConverter.ToString((byte[])value).Replace("-", "");
                return "X'" + txt + "'";
            }
            return "'" + value.ToString() + "'";
        }

    }
}
