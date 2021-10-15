//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ToolGood.ReadyGo3.PetaPoco.Core;

//namespace ToolGood.ReadyGo3
//{
//      partial class SqlHelper
//    {

//        #region FirstOrDefault PK

//        /// <summary>
//        /// 根据条件查询第一个
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public T FirstOrDefault_Table<T>(string table, int condition) where T : class
//        {
//            return SingleOrDefaultById_Table<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public T FirstOrDefault_Table<T>(string table, uint condition) where T : class
//        {
//            return SingleOrDefaultById_Table<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public T FirstOrDefault_Table<T>(string table, long condition) where T : class
//        {
//            return SingleOrDefaultById_Table<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public T FirstOrDefault_Table<T>(string table, ulong condition) where T : class
//        {
//            return SingleOrDefaultById_Table<T>(table, condition);
//        }

//        #endregion

//        /// <summary>
//        /// 根据条件查询第一个
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public T FirstOrDefault_Table<T>(string table, object condition) where T : class
//        {
//            return FirstOrDefault_Table<T>(table, ConditionObjectToWhere(condition));
//        }


//        /// <summary>
//        /// 根据条件查询
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="limit">个数</param>
//        /// <param name="offset">位移</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public List<T> Select_Table<T>(string table, int limit, int offset, object condition) where T : class
//        {
//            return Select_Table<T>(table, limit, offset, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件查询
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="limit">个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public List<T> Select_Table<T>(string table, int limit, object condition) where T : class
//        {
//            return Select_Table<T>(table, limit, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件查询
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public List<T> Select_Table<T>(string table, object condition) where T : class
//        {
//            return Select_Table<T>(table, ConditionObjectToWhere(condition));
//        }

  

//        /// <summary>
//        /// 根据条件查询页
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="page">页数</param>
//        /// <param name="itemsPerPage">每页个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public List<T> SelectPage_Table<T>(string table, int page, int itemsPerPage, object condition)
//            where T : class
//        {
//            return SelectPage_Table<T>(table, page, itemsPerPage, ConditionObjectToWhere(condition));
//        }
 

//        /// <summary>
//        /// 根据条件查询页
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="page">页数</param>
//        /// <param name="itemsPerPage">每页个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Page<T> Page_Table<T>(string table, int page, int itemsPerPage, object condition)
//            where T : class
//        {
//            return Page_Table<T>(table, page, itemsPerPage, ConditionObjectToWhere(condition));
//        }


//        /// <summary>
//        /// 更新表
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="set"></param>
//        /// <param name="condition"></param>
//        /// <param name="ignoreFields"></param>
//        /// <returns></returns>
//        public int Update_Table(string table, object set, object condition, IEnumerable<string> ignoreFields = null)
//        {
//            var tbn = _provider.GetTableName(table);
//            return Update("UPDATE " + tbn + " " + ConditionObjectToUpdateSetWhere(set, condition, ignoreFields));
//        }

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition"></param>
//        /// <returns></returns>
//        public int Delete_Table(string table, object condition)
//        {
//            if (null == condition) {
//                throw new ArgumentNullException(nameof(condition));
//            }
//            var tbn = _provider.GetTableName(table);
//            return Delete("DELETE FROM " + tbn + " " + ConditionObjectToWhere(condition));
//        }


//        /// <summary>
//        /// 根据条件查询个数
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public int Count_Table(string table, object condition)
//        {
//            return Count_Table(table, ConditionObjectToWhere(condition));
//        }


//        /// <summary>
//        /// 根据条件判断是否存在
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public bool Exists_Table(string table, object condition)
//        {
//            return Count_Table(table, ConditionObjectToWhere(condition)) > 0;
//        }





//#if !NET40


//        #region FirstOrDefault_Async PK

//        /// <summary>
//        /// 根据条件查询第一个，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<T> FirstOrDefault_Table_Async<T>(string table, int condition) where T : class
//        {
//            return SingleOrDefaultById_Table_Async<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<T> FirstOrDefault_Table_Async<T>(string table, uint condition) where T : class
//        {
//            return SingleOrDefaultById_Table_Async<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<T> FirstOrDefault_Table_Async<T>(string table, long condition) where T : class
//        {
//            return SingleOrDefaultById_Table_Async<T>(table, condition);
//        }

//        /// <summary>
//        /// 根据条件查询第一个，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<T> FirstOrDefault_Table_Async<T>(string table, ulong condition) where T : class
//        {
//            return SingleOrDefaultById_Table_Async<T>(table, condition);
//        }

//        #endregion

//        /// <summary>
//        /// 根据条件查询第一个，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<T> FirstOrDefault_Table_Async<T>(string table, object condition) where T : class
//        {
//            return FirstOrDefault_Table_Async<T>(table, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件查询，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="limit">个数</param>
//        /// <param name="offset">位移</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<List<T>> Select_Table_Async<T>(string table, int limit, int offset, object condition) where T : class
//        {
//            return Select_Table_Async<T>(table, limit, offset, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件查询
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="limit">个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<List<T>> Select_Table_Async<T>(string table, int limit, object condition) where T : class
//        {
//            return Select_Table_Async<T>(table, limit, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件查询，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<List<T>> Select_Table_Async<T>(string table, object condition) where T : class
//        {
//            return Select_Table_Async<T>(table, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        ///  根据条件查询页，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="page">页数</param>
//        /// <param name="itemsPerPage">每页个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<List<T>> SelectPage_Table_Async<T>(string table, int page, int itemsPerPage, object condition)
//            where T : class
//        {
//            return SelectPage_Table_Async<T>(table, page, itemsPerPage, ConditionObjectToWhere(condition));
//        }

 
//        /// <summary>
//        ///  根据条件查询页，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="page">页数</param>
//        /// <param name="itemsPerPage">每页个数</param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<Page<T>> Page_Table_Async<T>(string table, int page, int itemsPerPage, object condition)
//            where T : class
//        {
//            return Page_Table_Async<T>(table, page, itemsPerPage, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 更新表
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="set"></param>
//        /// <param name="condition"></param>
//        /// <param name="ignoreFields"></param>
//        /// <returns></returns>
//        public Task<int> Update_Table_Async(string table, object set, object condition, IEnumerable<string> ignoreFields = null)
//        {
//            var tbn = _provider.GetTableName(table);
//            return Update_Async("UPDATE " + tbn + " " + ConditionObjectToUpdateSetWhere(set, condition, ignoreFields));
//        }

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition"></param>
//        /// <returns></returns>
//        public Task<int> Delete_Table_Async(string table, object condition)
//        {
//            if (null == condition) {
//                throw new ArgumentNullException(nameof(condition));
//            }
//            var tbn = _provider.GetTableName(table);
//            return Delete_Async("DELETE FROM " + tbn + " " + ConditionObjectToWhere(condition));
//        }


//        /// <summary>
//        /// 根据条件查询个数，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public Task<int> Count_Table_Async(string table, object condition)
//        {
//            return Count_Table_Async(table, ConditionObjectToWhere(condition));
//        }

//        /// <summary>
//        /// 根据条件是判断否存在，异步操作
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="condition">条件</param>
//        /// <returns></returns>
//        public async Task<bool> Exists_Table_Async(string table, object condition)
//        {
//            return await Count_Table_Async(table, ConditionObjectToWhere(condition)) > 0;

//        }

//#endif




//    }
//}
