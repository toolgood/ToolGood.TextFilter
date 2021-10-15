using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.Gadget.Internals
{
    /// <summary>
    /// 默认值生成
    /// </summary>
    class DefaultValue
    {
        private static readonly Cache<Type, Delegate> _setDefault = new Cache<Type, Delegate>();

        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="setString"></param>
        /// <param name="setDateTime"></param>
        /// <param name="setGuid"></param>
        public static void SetDefaultValue<T>(T obj, bool setString, bool setDateTime, bool setGuid)
        {
            var action = _setDefault.Get(typeof(T), () => CreateDefaultFunction<T>());
            var a = (action as Action<T, bool, bool, bool>);
            a(obj, setString, setDateTime, setGuid);
        }


        private static Delegate CreateDefaultFunction<T>()
        {
            #region 初始时间
            var pd = PocoData.ForType(typeof(T));
            List<PropertyInfo> datetimes = new List<PropertyInfo>();
            List<PropertyInfo> datetimeoffsets = new List<PropertyInfo>();
            List<PropertyInfo> strings = new List<PropertyInfo>();
            List<PropertyInfo> ansiStrings = new List<PropertyInfo>();
            List<PropertyInfo> guids = new List<PropertyInfo>();
            foreach (var item in pd.Columns) {
                if (item.Value.ResultColumn) continue;
                var pi = item.Value.PropertyInfo;
                if (pi.PropertyType == typeof(DateTime)) {
                    datetimes.Add(pi);
                } else if (pi.PropertyType == typeof(DateTimeOffset)) {
                    datetimeoffsets.Add(pi);
                } else if (pi.PropertyType == typeof(string)) {
                    strings.Add(pi);
                } else if (pi.PropertyType == typeof(Guid)) {
                    guids.Add(pi);
                } else if (pi.PropertyType == typeof(AnsiString)) {
                    ansiStrings.Add(pi);
                }
            }
            #endregion

            #region dateTimeType dateTimeOffsetType AnsiString
            var dateTimeType = typeof(DateTime);
            //var getYear = dateTimeType.GetProperty("Year");
            var getNow = dateTimeType.GetProperty("Now", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getMinValue = dateTimeType.GetField("MinValue", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getop_Equality = dateTimeType.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);



            var dateTimeOffsetType = typeof(DateTimeOffset);
            var getNow2 = dateTimeOffsetType.GetProperty("Now", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getMinValue2 = dateTimeOffsetType.GetField("MinValue", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getop_Equality2 = dateTimeOffsetType.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);


            var guidType = typeof(Guid);
            var getEmpty = guidType.GetField("Empty", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getNewGuid = guidType.GetMethod("NewGuid", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            var getop_Equality3 = guidType.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);


            var asctor = typeof(AnsiString).GetConstructor(new Type[] { typeof(string) });

            #endregion

            var m = new DynamicMethod("tg_def_" + Guid.NewGuid().ToString().Replace("-", ""), typeof(void), new Type[] { typeof(T), typeof(bool), typeof(bool), typeof(bool) }, true);
            var il = m.GetILGenerator();

            #region string
            if (strings.Count > 0) {
                il.Emit(OpCodes.Ldarg_1);
                var lab1 = il.DefineLabel();
                if (strings.Count < 7) {
                    il.Emit(OpCodes.Brfalse_S, lab1);
                } else {
                    il.Emit(OpCodes.Brfalse, lab1);
                }
                for (int i = 0; i < strings.Count; i++) {
                    var item = strings[i];

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    var lab = il.DefineLabel();
                    il.Emit(OpCodes.Brtrue_S, lab);


                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, "");
                    il.Emit(OpCodes.Callvirt, item.GetSetMethod());
                    il.MarkLabel(lab);
                }
                il.MarkLabel(lab1);
            }
            #endregion

            #region AnsiString
            if (ansiStrings.Count > 0) {
                il.Emit(OpCodes.Ldarg_1);
                var lab1 = il.DefineLabel();
                if (ansiStrings.Count < 7) {
                    il.Emit(OpCodes.Brfalse_S, lab1);
                } else {
                    il.Emit(OpCodes.Brfalse, lab1);
                }

                for (int i = 0; i < ansiStrings.Count; i++) {
                    var item = ansiStrings[i];
                    var lab = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    il.Emit(OpCodes.Brtrue_S, lab);


                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, "");
                    il.Emit(OpCodes.Newobj, asctor);
                    il.Emit(OpCodes.Callvirt, item.GetSetMethod());
                    il.MarkLabel(lab);
                }
                il.MarkLabel(lab1);

            }


            #endregion

            #region date
            if (datetimes.Count + datetimeoffsets.Count > 0) {
                il.Emit(OpCodes.Ldarg_2);
                var lab2 = il.DefineLabel();
                if (datetimes.Count + datetimeoffsets.Count < 5) {
                    il.Emit(OpCodes.Brfalse_S, lab2);
                } else {
                    il.Emit(OpCodes.Brfalse, lab2);
                }

                #region datetimes
                foreach (var item in datetimes) {
                    var lab = il.DefineLabel();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    il.Emit(OpCodes.Ldsfld, getMinValue);
                    il.Emit(OpCodes.Call, getop_Equality);
                    il.Emit(OpCodes.Brfalse_S, lab);

                    il.Emit(OpCodes.Ldarg_0);
                    //il.Emit(OpCodes.Ldsfld, getMinValue);
                    il.Emit(OpCodes.Call, getNow.GetGetMethod());
                    il.Emit(OpCodes.Callvirt, item.GetSetMethod());
                    il.MarkLabel(lab);
                }
                #endregion

                #region datetimeoffsets
                foreach (var item in datetimeoffsets) {
                    var lab = il.DefineLabel();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    il.Emit(OpCodes.Ldsfld, getMinValue2);
                    il.Emit(OpCodes.Call, getop_Equality2);
                    il.Emit(OpCodes.Brfalse_S, lab);

                    il.Emit(OpCodes.Ldarg_0);
                    //il.Emit(OpCodes.Ldsfld, getMinValue);
                    il.Emit(OpCodes.Call, getNow2.GetGetMethod());
                    il.Emit(OpCodes.Callvirt, item.GetSetMethod());
                    il.MarkLabel(lab);
                }
                #endregion
                il.MarkLabel(lab2);
            }

            #endregion

            #region guid
            if (guids.Count > 0) {
                il.Emit(OpCodes.Ldarg_3);
                var lab3 = il.DefineLabel();
                if (guids.Count < 5) {
                    il.Emit(OpCodes.Brfalse_S, lab3);
                } else {
                    il.Emit(OpCodes.Brfalse, lab3);
                }

                foreach (var item in guids) {
                    var lab = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                    il.Emit(OpCodes.Ldsfld, getEmpty);
                    il.Emit(OpCodes.Call, getop_Equality3);
                    il.Emit(OpCodes.Brfalse_S, lab);

                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Call, getNewGuid);
                    il.Emit(OpCodes.Callvirt, item.GetSetMethod());
                    il.MarkLabel(lab);
                }
                il.MarkLabel(lab3);
            }
            #endregion

            il.Emit(OpCodes.Ret);
            return m.CreateDelegate(Expression.GetActionType(typeof(T), typeof(bool), typeof(bool), typeof(bool)));
        }

    }

}
