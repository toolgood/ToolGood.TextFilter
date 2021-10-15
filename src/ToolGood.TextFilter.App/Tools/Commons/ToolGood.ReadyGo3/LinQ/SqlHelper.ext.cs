using System;
using System.Linq.Expressions;
using ToolGood.ReadyGo3.LinQ;

namespace ToolGood.ReadyGo3
{
    partial class SqlHelper
    {
        #region Where
        /// <summary>
        /// 动态Sql拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public WhereHelper<T> Where<T>() where T : class
        {
            var where = new WhereHelper<T>(this);
            return where;
        }


        /// <summary>
        /// 动态Sql拼接， 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T> Where<T>(string where) where T : class
        {
            var whereHelper = new WhereHelper<T>(this);
            whereHelper.Where(where);

            return whereHelper;
        }

        /// <summary>
        /// 动态Sql拼接， 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T> Where<T>(string where, params object[] args) where T : class
        {
            var whereHelper = new WhereHelper<T>(this);
            if (string.IsNullOrEmpty(where)) throw new ArgumentNullException("where");
            whereHelper.where(where, args);
            return whereHelper;
        }

        /// <summary>
        /// 动态Sql拼接
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T> Where<T>(Expression<Func<T, bool>> where) where T : class
        {
            var whereHelper = new WhereHelper<T>(this);
            whereHelper.Where(where);
            return whereHelper;
        }

        #endregion

        #region WhereTable
        /// <summary>
        /// 动态Sql拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public WhereHelper<T> Where_Table<T>(string table) where T : class
        {
            var where = new WhereHelper<T>(this, table);
            return where;
        }


        /// <summary>
        /// 动态Sql拼接， 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T> Where_Table<T>(string table, string where) where T : class
        {
            var whereHelper = new WhereHelper<T>(this, table);
            whereHelper.Where(where);

            return whereHelper;
        }

        /// <summary>
        /// 动态Sql拼接， 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T> Where_Table<T>(string table, string where, params object[] args) where T : class
        {
            var whereHelper = new WhereHelper<T>(this, table);
            if (string.IsNullOrEmpty(where)) throw new ArgumentNullException("where");
            whereHelper.where(where, args);
            return whereHelper;
        }

        /// <summary>
        /// 动态Sql拼接
        /// </summary>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T> Where_Table<T>(string table, Expression<Func<T, bool>> where) where T : class
        {
            var whereHelper = new WhereHelper<T>(this, table);
            whereHelper.Where(where);
            return whereHelper;
        } 
        #endregion

    }

}
