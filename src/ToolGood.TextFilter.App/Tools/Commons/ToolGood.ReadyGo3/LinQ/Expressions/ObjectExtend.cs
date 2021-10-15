using System.Collections.Generic;

namespace ToolGood.ReadyGo3.LinQ
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ObjectExtend
    {
        /// <summary>
        /// 在……之中，支持Where方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T o, params T[] c)
            where T : struct
        {
            return IsIn(o, (ICollection<T>)c);
        }
        /// <summary>
        /// 在……之中，支持Where方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T o, ICollection<T> c)
            where T : struct
        {
            foreach (T i in c)
                if (i.Equals(o)) return true;
            return false;
        }
        /// <summary>
        /// 不在……之中，支持Where方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNotIn<T>(this T o, params T[] c)
            where T : struct
        {
            return IsNotIn(o, (ICollection<T>)c);
        }
        /// <summary>
        /// 不在……之中，支持Where方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNotIn<T>(this T o, ICollection<T> c)
            where T : struct
        {
            foreach (T i in c)
                if (i.Equals(o)) return false;
            return true;
        }
        /// <summary>
        /// 在……之中，支持Where方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIn(this string o, params string[] c)
        {
            return IsIn(o, (ICollection<string>)c);
        }
        /// <summary>
        /// 在……之中，支持Where方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsIn(this string o, ICollection<string> c)
        {
            foreach (string i in c)
                if (i.Equals(o)) return true;
            return false;
        }
        /// <summary>
        /// 不在……之中，支持Where方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNotIn(this string o, params string[] c)
        {
            return IsNotIn(o, (ICollection<string>)c);
        }
        /// <summary>
        /// 不在……之中，支持Where方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNotIn(this string o, ICollection<string> c)
        {
            foreach (string i in c)
                if (i.Equals(o)) return false;
            return true;
        }
    }
}
