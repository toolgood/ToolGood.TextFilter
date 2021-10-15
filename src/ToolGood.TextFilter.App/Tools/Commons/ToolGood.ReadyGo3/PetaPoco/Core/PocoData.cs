using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Internal;

namespace ToolGood.ReadyGo3.PetaPoco.Core
{
    /// <summary>
    /// PocoData
    /// </summary>
    public class PocoData
    {
        private static readonly Cache<Type, PocoData> _pocoDatas = new Cache<Type, PocoData>();
        public static readonly List<Func<object, object>> _converters = new List<Func<object, object>>();
        private static readonly object _converterLock = new object();
        private static readonly MethodInfo fnGetValue = typeof(IDataRecord).GetMethod("GetValue", new Type[] { typeof(int) });
        private static readonly MethodInfo fnIsDBNull = typeof(IDataRecord).GetMethod("IsDBNull");
        private static readonly FieldInfo fldConverters = typeof(PocoData).GetField("_converters", BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public);
        private static readonly MethodInfo fnListGetItem = typeof(List<Func<object, object>>).GetProperty("Item").GetGetMethod();
        private static readonly MethodInfo fnInvoke = typeof(Func<object, object>).GetMethod("Invoke");
        private readonly Cache<string, Delegate> PocoFactories = new Cache<string, Delegate>();
        private readonly Type Type;

        /// <summary>
        /// 表信息
        /// </summary>
        public TableInfo TableInfo;
        /// <summary>
        /// 列信息
        /// </summary>
        public Dictionary<string, PocoColumn> Columns;
        internal Dictionary<string, PocoColumn> SelectColumns;

        internal PocoData()
        {
        }

        internal PocoData(Type type)
        {
            Type = type;

            // Get the mapper for this type
            //var mapper = Singleton<StandardMapper>.Instance;

            // Get the table info
            TableInfo = TableInfo.FromPoco(type);

            // Work out bound properties
            Columns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
            foreach (var pi in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)) {
                ColumnInfo ci = ColumnInfo.FromProperty(pi);// mapper.GetColumnInfo(pi);
                if (ci == null)
                    continue;

                var pc = new PocoColumn {
                    PropertyInfo = pi,
                    PropertyName = pi.Name,
                    ColumnName = ci.ColumnName,
                    ResultColumn = ci.ResultColumn,
                    ForceToUtc = ci.ForceToUtc,
                    ResultSql = ci.ResultSql
                };
                // Store it
                Columns.Add(pc.ColumnName, pc);
            }
            //排除 主键为String
            if (TableInfo.AutoIncrement) {
                PocoColumn pc;
                if (Columns.TryGetValue(TableInfo.PrimaryKey, out pc)) {
                    TableInfo.AutoIncrement = pc.PropertyInfo.PropertyType.IsValueType;
                } else {
                    TableInfo.PrimaryKey = null;
                    TableInfo.AutoIncrement = false;
                }
            }


            SelectColumns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in Columns) {
                SelectColumns[item.Key] = item.Value;
            }

            // 支持 is_system_object 匹配 IsSystemObject
            var names = Columns.Keys.ToList();
            foreach (var name in names) {
                StringBuilder sb = new StringBuilder();
                foreach (var c in name) {
                    if (c >= 'A' && c <= 'Z') { sb.Append("_"); }
                    sb.Append(c);
                }
                if (sb[0] == '_') { sb.Remove(0, 1); }
                var newName = sb.ToString();
                if (SelectColumns.ContainsKey(newName) == false) {
                    SelectColumns.Add(newName, Columns[name]);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="primaryKeyName"></param>
        /// <returns></returns>
        public static PocoData ForObject(object obj, string primaryKeyName)
        {
            var t = obj.GetType();
            if (t == typeof(System.Dynamic.ExpandoObject)) {
                var pd = new PocoData {
                    TableInfo = new TableInfo(),
                    Columns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase)
                };
                pd.Columns.Add(primaryKeyName, new ExpandoColumn() { ColumnName = primaryKeyName });
                pd.TableInfo.PrimaryKey = primaryKeyName;
                pd.TableInfo.AutoIncrement = true;
                foreach (var col in (obj as IDictionary<string, object>).Keys) {
                    if (col != primaryKeyName)
                        pd.Columns.Add(col, new ExpandoColumn() { ColumnName = col });
                }
                pd.SelectColumns = new Dictionary<string, PocoColumn>(StringComparer.OrdinalIgnoreCase);
                foreach (var item in pd.Columns) {
                    pd.SelectColumns[item.Key] = item.Value;
                }
                return pd;
            }
            return ForType(t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PocoData ForType(Type type)
        {
            if (type == typeof(System.Dynamic.ExpandoObject))
                throw new InvalidOperationException("Can't use dynamic types with this method");

            return _pocoDatas.Get(type, () => new PocoData(type));
        }

        private static bool IsIntegralType(Type type)
        {
            var tc = Type.GetTypeCode(type);
            return tc >= TypeCode.SByte && tc <= TypeCode.UInt64;
        }

        /// <summary>
        /// Create factory function that can convert a IDataReader record into a POCO
        /// </summary>
        /// <param name="firstColumn"></param>
        /// <param name="countColumns"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Delegate GetFactory(int firstColumn, int countColumns, IDataReader reader)
        {
            #region 创建Key
            StringBuilder sb = new StringBuilder();
            sb.Append(reader.GetType().FullName);
            for (int i = 0; i < countColumns; i++) {
                sb.AppendFormat("|{0}-{1}", reader.GetName(i), reader.GetFieldType(i).FullName);
            }
            //sb.Append("|" + usedProxy.ToString());
            var key = sb.ToString();
            #endregion 创建Key


            return PocoFactories.Get(key, () => {
                var type = /*usedProxy ? UpdateData.GetProxyType(Type) : */Type;
                // Create the method
                var m = new DynamicMethod("tg_mi_" + Guid.NewGuid().ToString().Replace("-", ""), type, new Type[] { typeof(IDataReader) }, true);
                var il = m.GetILGenerator();
                //var mapper = Singleton<StandardMapper>.Instance;

                if (type == typeof(object)) {
                    // var poco=new T()
                    il.Emit(OpCodes.Newobj, typeof(System.Dynamic.ExpandoObject).GetConstructor(Type.EmptyTypes));          // obj

                    MethodInfo fnAdd = typeof(IDictionary<string, object>).GetMethod("Add");

                    // Enumerate all fields generating a set assignment for the column
                    for (int i = firstColumn; i < firstColumn + countColumns; i++) {
                        var srcType = reader.GetFieldType(i);

                        il.Emit(OpCodes.Dup); // obj, obj
                        il.Emit(OpCodes.Ldstr, reader.GetName(i)); // obj, obj, fieldname

                        //// Get the converter
                        //Func<object, object> converter = mapper.GetFromDbConverter((PropertyInfo)null, srcType);

                        /*
						if (ForceDateTimesToUtc && converter == null && srcType == typeof(DateTime))
							converter = delegate(object src) { return new DateTime(((DateTime)src).Ticks, DateTimeKind.Utc); };
						 */

                        //// Setup stack for call to converter
                        //AddConverterToStack(il, converter);

                        // r[i]
                        il.Emit(OpCodes.Ldarg_0); // obj, obj, fieldname, converter?,    rdr
                        il.Emit(OpCodes.Ldc_I4, i); // obj, obj, fieldname, converter?,  rdr,i
                        il.Emit(OpCodes.Callvirt, fnGetValue); // obj, obj, fieldname, converter?,  value

                        // Convert DBNull to null
                        il.Emit(OpCodes.Dup); // obj, obj, fieldname, converter?,  value, value
                        il.Emit(OpCodes.Isinst, typeof(DBNull)); // obj, obj, fieldname, converter?,  value, (value or null)
                        var lblNotNull = il.DefineLabel();
                        il.Emit(OpCodes.Brfalse_S, lblNotNull); // obj, obj, fieldname, converter?,  value
                        il.Emit(OpCodes.Pop); // obj, obj, fieldname, converter?
                        //if (converter != null)
                        //    il.Emit(OpCodes.Pop); // obj, obj, fieldname, 
                        il.Emit(OpCodes.Ldnull); // obj, obj, fieldname, null

                        //if (converter != null) {
                        //    var lblReady = il.DefineLabel();
                        //    il.Emit(OpCodes.Br_S, lblReady);
                        //    il.MarkLabel(lblNotNull);
                        //    il.Emit(OpCodes.Callvirt, fnInvoke);
                        //    il.MarkLabel(lblReady);
                        //} else {
                        //    il.MarkLabel(lblNotNull);
                        //}
                        il.MarkLabel(lblNotNull);

                        il.Emit(OpCodes.Callvirt, fnAdd);
                    }
                } else if (type.IsValueType || type == typeof(string) || type == typeof(byte[])) {
                    // Do we need to install a converter?
                    var srcType = reader.GetFieldType(0);
                    var converter = GetConverter(null, srcType, type);

                    // "if (!rdr.IsDBNull(i))"
                    il.Emit(OpCodes.Ldarg_0); // rdr
                    il.Emit(OpCodes.Ldc_I4_0); // rdr,0
                    il.Emit(OpCodes.Callvirt, fnIsDBNull); // bool
                    var lblCont = il.DefineLabel();
                    il.Emit(OpCodes.Brfalse_S, lblCont);
                    il.Emit(OpCodes.Ldnull); // null
                    var lblFin = il.DefineLabel();
                    il.Emit(OpCodes.Br_S, lblFin);

                    il.MarkLabel(lblCont);

                    // Setup stack for call to converter
                    AddConverterToStack(il, converter);

                    il.Emit(OpCodes.Ldarg_0); // rdr
                    il.Emit(OpCodes.Ldc_I4_0); // rdr,0
                    il.Emit(OpCodes.Callvirt, fnGetValue); // value

                    // Call the converter
                    if (converter != null)
                        il.Emit(OpCodes.Callvirt, fnInvoke);

                    il.MarkLabel(lblFin);
                    il.Emit(OpCodes.Unbox_Any, type); // value converted
                } else {
                    // var poco=new T()
                    var ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
                    if (ctor == null)
                        throw new InvalidOperationException($"Type [{type.FullName}] should have default public or non-public constructor");

                    il.Emit(OpCodes.Newobj, ctor);

                    // Enumerate all fields generating a set assignment for the column
                    for (int i = firstColumn; i < firstColumn + countColumns; i++) {
                        // Get the PocoColumn for this db column, ignore if not known
                        if (!SelectColumns.TryGetValue(reader.GetName(i), out PocoColumn pc))
                            continue;

                        // Get the source type for this column
                        var srcType = reader.GetFieldType(i);
                        var dstType = pc.PropertyInfo.PropertyType;

                        // "if (!rdr.IsDBNull(i))"
                        il.Emit(OpCodes.Ldarg_0); // poco,rdr
                        il.Emit(OpCodes.Ldc_I4, i); // poco,rdr,i
                        il.Emit(OpCodes.Callvirt, fnIsDBNull); // poco,bool
                        var lblNext = il.DefineLabel();
                        il.Emit(OpCodes.Brtrue_S, lblNext); // poco

                        il.Emit(OpCodes.Dup); // poco,poco

                        // Do we need to install a converter?
                        var converter = GetConverter(pc, srcType, dstType);

                        // Fast
                        bool Handled = false;
                        if (converter == null) {
                            var valuegetter = typeof(IDataRecord).GetMethod("Get" + srcType.Name, new Type[] { typeof(int) });
                            if (valuegetter != null
                                && valuegetter.ReturnType == srcType
                                && (valuegetter.ReturnType == dstType || valuegetter.ReturnType == Nullable.GetUnderlyingType(dstType))) {
                                il.Emit(OpCodes.Ldarg_0); // *,rdr
                                il.Emit(OpCodes.Ldc_I4, i); // *,rdr,i
                                il.Emit(OpCodes.Callvirt, valuegetter); // *,value

                                // Convert to Nullable
                                if (Nullable.GetUnderlyingType(dstType) != null) {
                                    il.Emit(OpCodes.Newobj, dstType.GetConstructor(new Type[] { Nullable.GetUnderlyingType(dstType) }));
                                }

                                il.Emit(OpCodes.Callvirt, pc.PropertyInfo.GetSetMethod(true)); // poco
                                Handled = true;
                            }
                        }

                        // Not so fast
                        if (!Handled) {
                            // Setup stack for call to converter
                            AddConverterToStack(il, converter);

                            // "value = rdr.GetValue(i)"
                            il.Emit(OpCodes.Ldarg_0); // *,rdr
                            il.Emit(OpCodes.Ldc_I4, i); // *,rdr,i
                            il.Emit(OpCodes.Callvirt, fnGetValue); // *,value

                            // Call the converter
                            if (converter != null)
                                il.Emit(OpCodes.Callvirt, fnInvoke);

                            // Assign it
                            il.Emit(OpCodes.Unbox_Any, pc.PropertyInfo.PropertyType); // poco,poco,value
                            il.Emit(OpCodes.Callvirt, pc.PropertyInfo.GetSetMethod(true)); // poco
                        }

                        il.MarkLabel(lblNext);
                    }

                    var fnOnLoaded = RecurseInheritedTypes<MethodInfo>(type, (x) => x.GetMethod("OnLoaded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null));
                    if (fnOnLoaded != null) {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, fnOnLoaded);
                    }
                    var clearMethod = RecurseInheritedTypes<MethodInfo>(type, (x) => x.GetMethod("__ClearChanges__", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null));
                    if (clearMethod != null) {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Callvirt, clearMethod);
                    }
                }

                il.Emit(OpCodes.Ret);

                // Cache it, return it
                return m.CreateDelegate(Expression.GetFuncType(typeof(IDataReader), type));
            }
                );
        }
        private static T RecurseInheritedTypes<T>(Type t, Func<Type, T> cb)
        {
            while (t != null) {
                T info = cb(t);
                if (info != null) return info;
                t = t.BaseType;
            }
            return default(T);
        }

        private static void AddConverterToStack(ILGenerator il, Func<object, object> converter)
        {
            if (converter != null) {
                // Add the converter
                int converterIndex;

                lock (_converterLock) {
                    converterIndex = _converters.Count;
                    _converters.Add(converter);
                }

                // Generate IL to push the converter onto the stack
                il.Emit(OpCodes.Ldsfld, fldConverters);
                il.Emit(OpCodes.Ldc_I4, converterIndex);
                il.Emit(OpCodes.Callvirt, fnListGetItem); // Converter
            }
        }

        private static Func<object, object> GetConverter(PocoColumn pc, Type srcType, Type dstType)
        {
            //Func<object, object> converter = null;

            //// Get converter from the mapper
            //if (pc != null) {
            //    converter = mapper.GetFromDbConverter(pc.PropertyInfo, srcType);
            //    if (converter != null)
            //        return converter;
            //}

            // Standard DateTime->Utc mapper
            if (pc != null && pc.ForceToUtc && srcType == typeof(DateTime) && (dstType == typeof(DateTime) || dstType == typeof(DateTime?))) {
                return delegate (object src) { return new DateTime(((DateTime)src).Ticks, DateTimeKind.Utc); };
            }

            // unwrap nullable types
            Type underlyingDstType = Nullable.GetUnderlyingType(dstType);
            if (underlyingDstType != null) {
                dstType = underlyingDstType;
            }

            // Forced type conversion including integral types -> enum
            if (dstType.IsEnum && IsIntegralType(srcType)) {
                var backingDstType = Enum.GetUnderlyingType(dstType);
                if (underlyingDstType != null) {
                    // if dstType is Nullable<Enum>, convert to enum value
                    return delegate (object src) { return Enum.ToObject(dstType, src); };
                } else if (srcType != backingDstType) {
                    return delegate (object src) { return Convert.ChangeType(src, backingDstType, null); };
                }
            } else if (!dstType.IsAssignableFrom(srcType)) {
                if (dstType.IsEnum && srcType == typeof(string)) {
                    return delegate (object src) { return EnumHelper.EnumFromString(dstType, (string)src); };
                } else if (dstType == typeof(Guid) && srcType == typeof(string)) {
                    return delegate (object src) { return Guid.Parse((string)src); };
                } else {
                    return delegate (object src) { return Convert.ChangeType(src, dstType, null); };
                }
            }

            return null;
        }

        internal static void FlushCaches()
        {
            _pocoDatas.Flush();
        }

        public override string ToString()
        {
            return Type.FullName;
        }
    }
}
