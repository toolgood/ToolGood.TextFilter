using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ToolGood.ReadyGo3.LinQ.Expressions;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.Internals
{
    /// <summary>
    /// 表名动态类
    /// </summary>
    class TableName : DynamicObject
    {
        internal string _asName;
        internal PocoData _pocoData;
        internal DatabaseProvider _provider;




        public TableName(Type type, DatabaseProvider provider, string asName)
        {
            _pocoData = PocoData.ForType(type);
            _provider = provider;
            _asName = asName;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var fieldName = binder.Name;
            if (_pocoData.Columns.ContainsKey(fieldName))
            {
                if (_provider != null)
                {
                    if (string.IsNullOrEmpty(_asName))
                    {
                        result = _provider.EscapeSqlIdentifier(_pocoData.Columns[fieldName].ColumnName);
                    }
                    else
                    {
                        result = _asName + "." + _provider.EscapeSqlIdentifier(_pocoData.Columns[fieldName].ColumnName);
                    }
                }
                else if (string.IsNullOrEmpty(_asName))
                {
                    result = _pocoData.Columns[fieldName].ColumnName;
                }
                else
                {
                    result = _asName + "." + _pocoData.Columns[fieldName].ColumnName;
                }
                return true;
            }
            fieldName = fieldName.Replace("_", "");
            foreach (var item in _pocoData.Columns)
            {
                if (item.Value.PropertyName.Replace("_", "").Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    if (_provider != null)
                    {
                        if (string.IsNullOrEmpty(_asName))
                        {
                            result = _provider.EscapeSqlIdentifier(item.Value.ColumnName);
                        }
                        else
                        {
                            result = _asName + "." + _provider.EscapeSqlIdentifier(item.Value.ColumnName);
                        }
                    }
                    else if (string.IsNullOrEmpty(_asName))
                    {
                        result = item.Value.ColumnName;
                    }
                    else
                    {
                        result = _asName + "." + item.Value.ColumnName;
                    }
                    return true;
                }
            }
            result = null;
            return false;
        }

        public override string ToString()
        {
            if (_provider != null)
            {
                if (string.IsNullOrEmpty(_asName))
                {
                    return _provider.EscapeSqlIdentifier(_pocoData.TableInfo.TableName);
                }
                else
                {
                    return _provider.EscapeSqlIdentifier(_pocoData.TableInfo.TableName) + " " + _asName;
                }
            }
            if (string.IsNullOrEmpty(_asName))
            {
                return _pocoData.TableInfo.TableName;
            }
            return _pocoData.TableInfo.TableName + " " + _asName;
        }

    }
    class TableName<T> : TableName
        where T : class, new()
    {
        private SqlExpression _sqlExpression;

        public TableName(Type type, DatabaseProvider provider, string asName) : base(type, provider, asName)
        {
            _sqlExpression = new SqlExpression(provider);
        }

        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public string F<T1>(Expression<Func<T, T1>> field)
        {
            var fieldName = _sqlExpression.GetColumnName(field);
            if (string.IsNullOrEmpty(_asName))
            {
                return fieldName;
            }
            return _asName + "." + fieldName;
        }


    }

}
