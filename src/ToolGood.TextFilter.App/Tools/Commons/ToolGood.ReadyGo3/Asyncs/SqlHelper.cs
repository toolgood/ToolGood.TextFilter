using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Gadget.Internals;
using ToolGood.ReadyGo3.Internals;
using ToolGood.ReadyGo3.PetaPoco.Core;

#if !NET40

namespace ToolGood.ReadyGo3
{
      partial class SqlHelper
    {

        #region Execute ExecuteScalar ExecuteDataTable ExecuteDataSet Exists

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns>返回受影响的行数</returns>
        public Task<int> Execute_Async(string sql, params object[] args)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().Execute_Async(sql, args);
        }

        /// <summary>
        /// 执行SQL 查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns>返回查询所返回的结果集中第一行的第一列。忽略额外的列或行</returns>
        public Task<T> ExecuteScalar_Async<T>(string sql = "", params object[] args)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().ExecuteScalar_Async<T>(sql, args);
        }

        /// <summary>
        /// 执行SQL 查询,返回 DataTable
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns>返回 Data_Table</returns>
        public Task<DataTable> ExecuteDataTable_Async(string sql, params object[] args)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().ExecuteDataTable_Async(sql, args);

        }



        /// <summary>
        /// 执行SQL 查询,判断是否存在，返回bool类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<bool> Exists_Async<T>(string sql, params object[] args)
        {
            sql = FormatSql(sql);
            return await Count_Async<T>(sql, args) > 0;
        }
        /// <summary>
        /// 执行SQL 查询,判断是否存在，返回bool类型
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<bool> Exists_Table_Async(string table, string sql, params object[] args)
        {
            sql = FormatSql(sql);
            return await Count_Table_Async(table, sql, args) > 0;
        }

        /// <summary>
        ///  执行SQL 查询,返回数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Count_Async<T>(string sql = "", params object[] args)
        {
            sql = sql.Trim();
            if (sql.StartsWith("SELECT ", StringComparison.CurrentCultureIgnoreCase) == false) {
                var pd = PocoData.ForType(typeof(T));
                var table = _provider.GetTableName(pd);
                sql = FormatSql(sql);
                sql = $"SELECT COUNT(*) FROM {table} {sql}";
            }
            return GetDatabase().ExecuteScalar_Async<int>(sql, args);
        }

        /// <summary>
        /// 执行SQL 查询,返回数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<int> Count_Async(string sql, params object[] args)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            return GetDatabase().ExecuteScalar_Async<int>(sql, args);
        }
        /// <summary>
        /// 执行SQL 查询,返回数量
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Count_Table_Async(string table, string sql = "", params object[] args)
        {
            sql = sql.Trim();
            if (sql.StartsWith("SELECT ", StringComparison.CurrentCultureIgnoreCase) == false) {
                sql = FormatSql(sql);
                sql = $"SELECT COUNT(*) FROM {table} {sql}";
            }
            return GetDatabase().ExecuteScalar_Async<int>(sql, args);
        }

        #endregion Execute ExecuteScalar ExecuteDataTable ExecuteDataSet Exists

        #region Select Page Select
        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Async<T>(string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(sql, args)).ToList();
        }
        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Table_Async<T>(string table, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(table, sql, args)).ToList();
        }

        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="limit">获取个数</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Async<T>(int limit, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(0, limit, sql, args)).ToList();
        }
        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="limit">获取个数</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Table_Async<T>(string table, int limit, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(table, 0, limit, sql, args)).ToList();
        }

        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">跳过</param>
        /// <param name="limit">获取个数</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Async<T>(int limit, int offset, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(offset, limit, sql, args)).ToList();
        }
        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="offset">跳过</param>
        /// <param name="limit">获取个数</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> Select_Table_Async<T>(string table, int limit, int offset, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(table, offset, limit, sql, args)).ToList();
        }

        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> SelectPage_Async<T>(int page, int itemsPerPage, string sql = "", params object[] args)
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>((page - 1) * itemsPerPage, itemsPerPage, sql, args)).ToList();
        }
        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> SelectPage_Table_Async<T>(string table, int page, int itemsPerPage, string sql = "", params object[] args)
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(table, (page - 1) * itemsPerPage, itemsPerPage, sql, args)).ToList();
        }


        /// <summary>
        /// 执行SQL 查询,返回Page类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<Page<T>> Page_Async<T>(int page, int itemsPerPage, string sql = "", params object[] args)
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            sql = FormatSql(sql);
            return GetDatabase().Page_Async<T>(page, itemsPerPage, sql, args);
        }
        /// <summary>
        /// 执行SQL 查询,返回Page类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<Page<T>> Page_Table_Async<T>(string table, int page, int itemsPerPage, string sql = "", params object[] args)
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            sql = FormatSql(sql);
            return GetDatabase().Page_Table_Async<T>(table, page, itemsPerPage, sql, args);
        }

        /// <summary>
        /// 执行SQL 查询,返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="columnSql">查询列 SQL语句</param>
        /// <param name="tableSql">TABLE SQL语句</param>
        /// <param name="orderSql">ORDER BY SQL语句</param>
        /// <param name="whereSql">WHERE SQL语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<List<T>> SQL_Select_Async<T>(int page, int itemsPerPage, string columnSql, string tableSql, string orderSql, string whereSql, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(columnSql)) { throw new ArgumentNullException("columnSql is null."); }
            if (string.IsNullOrWhiteSpace(tableSql)) { throw new ArgumentNullException("tableSql is null."); }
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            columnSql = RemoveStart(columnSql, "SELECT ");
            tableSql = RemoveStart(tableSql, "FROM ");
            orderSql = RemoveStart(orderSql, "ORDER BY ");
            whereSql = RemoveStart(whereSql, "WHERE ");

            var sql = _provider.CreateSql((int)itemsPerPage, (int)((Math.Max(0, page - 1)) * itemsPerPage), columnSql, tableSql, orderSql, whereSql);
            sql = FormatSql(sql);
            return (await GetDatabase().Query_Async<T>(sql, args)).ToList();
        }
        /// <summary>
        /// 执行SQL 查询,返回Page类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page">页数</param>
        /// <param name="itemsPerPage">每页数量</param>
        /// <param name="columnSql">查询列 SQL语句</param>
        /// <param name="tableSql">TABLE SQL语句</param>
        /// <param name="orderSql">ORDER BY SQL语句</param>
        /// <param name="whereSql">WHERE SQL语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<Page<T>> SQL_Page_Async<T>(int page, int itemsPerPage, string columnSql, string tableSql, string orderSql, string whereSql, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(columnSql)) { throw new ArgumentNullException("columnSql is null."); }
            if (string.IsNullOrWhiteSpace(tableSql)) { throw new ArgumentNullException("tableSql is null."); }
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            columnSql = RemoveStart(columnSql, "SELECT ");
            tableSql = RemoveStart(tableSql, "FROM ");
            orderSql = RemoveStart(orderSql, "ORDER BY ");
            whereSql = RemoveStart(whereSql, "WHERE ");

            string countSql = string.IsNullOrEmpty(whereSql) ? $"SELECT COUNT(1) FROM {tableSql}" : $"SELECT COUNT(1) FROM {tableSql} WHERE {whereSql}";
            countSql = FormatSql(countSql);

            var sql = _provider.CreateSql((int)itemsPerPage, (int)((Math.Max(0, page - 1)) * itemsPerPage), columnSql, tableSql, orderSql, whereSql);
            sql = FormatSql(sql);

            return GetDatabase().PageSql_Async<T>(page, itemsPerPage, sql, countSql, args);
        }

        #endregion Select Page Select

        #region Single SingleOrDefault First FirstOrDefault

        /// <summary>
        /// 获取唯一一个类型，若数量大于1，则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey">主键名</param>
        /// <returns></returns>
        private async Task<T> SingleOrDefaultById_Async<T>(object primaryKey)
        {
            var pd = PocoData.ForType(typeof(T));
            var pk = _provider.EscapeSqlIdentifier(pd.TableInfo.PrimaryKey);
            var sql = $"WHERE {pk}=@0";
            return (await GetDatabase().Query_Async<T>(sql, new object[] { primaryKey })).FirstOrDefault();
        }
        /// <summary>
        /// 获取唯一一个类型，若数量大于1，则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="primaryKey">主键名</param>
        /// <returns></returns>
        private async Task<T> SingleOrDefaultById_Table_Async<T>(string table, object primaryKey)
        {
            var pd = PocoData.ForType(typeof(T));
            var pk = _provider.EscapeSqlIdentifier(pd.TableInfo.PrimaryKey);
            var sql = $"WHERE {pk}=@0";
            return (await GetDatabase().Query_Async<T>(table, sql, new object[] { primaryKey })).FirstOrDefault();
        }

        /// <summary>
        /// 获取第一个类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<T> FirstOrDefault_Async<T>(string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            if (_sql_firstWithLimit1 == false) {
                return (await GetDatabase().Query_Async<T>(sql, args)).FirstOrDefault();
            }
            return (await GetDatabase().Query_Async<T>(0, 1, sql, args)).FirstOrDefault();
        }
        /// <summary>
        /// 获取第一个类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public async Task<T> FirstOrDefault_Table_Async<T>(string table, string sql = "", params object[] args)
        {
            sql = FormatSql(sql);
            if (_sql_firstWithLimit1 == false) {
                return (await GetDatabase().Query_Async<T>(table, sql, args)).FirstOrDefault();
            }
            return (await GetDatabase().Query_Async<T>(table, 0, 1, sql, args)).FirstOrDefault();
        }

        #endregion Single SingleOrDefault First FirstOrDefault

        #region Object  Insert Update Delete DeleteById Save

        /// <summary>
        /// 插入集合，不返回主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public async Task InsertList_Async<T>(List<T> list) where T : class
        {
            if (list == null) throw new ArgumentNullException("list is null.");
            if (list.Count == 0) return;
            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                foreach (var item in list) {
                    DefaultValue.SetDefaultValue<T>(item, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
                }
            }
            if (_Events.OnBeforeInsert(list)) return;

            await GetDatabase().Insert_Async(list);
            _Events.OnAfterInsert(list);
        }
        /// <summary>
        /// 插入集合，不返回主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task InsertList_Table_Async<T>(string table, List<T> list) where T : class
        {
            if (list == null) throw new ArgumentNullException("list is null.");
            if (list.Count == 0) return;
            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                foreach (var item in list) {
                    DefaultValue.SetDefaultValue<T>(item, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
                }
            }
            if (_Events.OnBeforeInsert(list)) return;

            await GetDatabase().Insert_Table_Async(table, list);
            _Events.OnAfterInsert(list);
        }


        /// <summary>
        /// 插入，支持主键自动获取。
        /// </summary>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<object> Insert_Async<T>(T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (poco is IList) throw new ArgumentException("poco is a list type, use InsertList methon .");

            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                DefaultValue.SetDefaultValue<T>(poco, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
            }
            if (_Events.OnBeforeInsert(poco)) return null;

            var obj = await GetDatabase().Insert_Async(poco);
            _Events.OnAfterInsert(poco);
            return obj;
        }

        /// <summary>
        /// 插入，支持主键自动获取。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<object> Insert_Table_Async<T>(string table, T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (poco is IList) throw new ArgumentException("poco is a list type, use InsertList methon .");

            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                DefaultValue.SetDefaultValue<T>(poco, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
            }
            if (_Events.OnBeforeInsert(poco)) return null;

            var obj = await GetDatabase().Insert_Table_Async(table, poco);
            _Events.OnAfterInsert(poco);
            return obj;
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="poco">对象</param>
        /// <param name="autoIncrement">是否自增，是，返回自增ID</param>
        /// <returns></returns>
        public async Task<object> Insert_Table_Async(string table, object poco, bool autoIncrement)
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (poco is IList) throw new ArgumentException("poco is a list type, use InsertList methon .");

            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                DefaultValue.SetDefaultValue(poco, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
            }
            if (_Events.OnBeforeInsert(poco)) return null;

            var obj = await GetDatabase().Insert_Async(table, poco, autoIncrement, null);
            _Events.OnAfterInsert(poco);
            return obj;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="table"></param>
        /// <param name="poco"></param>
        /// <param name="ignoreFields">忽略字段，这里填类中的属性名</param>
        /// <returns></returns>
        public async Task<object> Insert_Table_Async(string table, object poco, IEnumerable<string> ignoreFields)
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (poco is IList) throw new ArgumentException("poco is a list type, use InsertList methon .");

            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                DefaultValue.SetDefaultValue(poco, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
            }
            if (_Events.OnBeforeInsert(poco)) return null;

            var obj = await GetDatabase().Insert_Async(table, poco, false, ignoreFields);
            _Events.OnAfterInsert(poco);
            return obj;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="poco"></param>
        /// <param name="autoIncrement">是否自增，是，返回自增ID</param>
        /// <param name="ignoreFields">忽略字段，这里填类中的属性名</param>
        /// <returns></returns>
        public async Task<object> Insert_Table_Async(string table, object poco, bool autoIncrement, IEnumerable<string> ignoreFields)
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (poco is IList) throw new ArgumentException("poco is a list type, use InsertList methon .");

            if (_setDateTimeDefaultNow || _setStringDefaultNotNull || _setGuidDefaultNew) {
                DefaultValue.SetDefaultValue(poco, _setStringDefaultNotNull, _setDateTimeDefaultNow, _setGuidDefaultNew);
            }
            if (_Events.OnBeforeInsert(poco)) return null;

            var obj = await GetDatabase().Insert_Async(table, poco, autoIncrement, ignoreFields);
            _Events.OnAfterInsert(poco);
            return obj;
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<int> Update_Async<T>(T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (_Events.OnBeforeUpdate(poco)) return -1;

            int r = await GetDatabase().Update_Async(poco);

            _Events.OnAfterUpdate(poco);
            return r;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<int> Update_Table_Async<T>(string table, T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (_Events.OnBeforeUpdate(poco)) return -1;

            int r = await GetDatabase().Update_Table_Async(table, poco);

            _Events.OnAfterUpdate(poco);
            return r;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<int> Delete_Async<T>(T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (_Events.OnBeforeDelete(poco)) return -1;

            var t = await GetDatabase().Delete_Async(poco);

            _Events.OnAfterDelete(poco);
            return t;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="poco">对象</param>
        /// <returns></returns>
        public async Task<int> Delete_Table_Async<T>(string table, T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            if (_Events.OnBeforeDelete(poco)) return -1;

            var t = await GetDatabase().Delete_Table_Async(table, poco);

            _Events.OnAfterDelete(poco);
            return t;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Delete_Async<T>(string sql, params object[] args) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().Delete_Async<T>(sql, args);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Delete_Table_Async(string table, string sql, params object[] args)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().Delete_Table_Async(table, sql, args);
        }


        /// <summary>
        /// 根据ID 删除表数据, 注： 单独从delete方法，防止出错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public Task<int> DeleteById_Async<T>(object primaryKey) where T : class
        {
            return GetDatabase().Delete_Async<T>(primaryKey);
        }

        /// <summary>
        /// 根据ID 删除表数据, 注： 单独从delete方法，防止出错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        public Task<int> DeleteById_Table_Async<T>(string table, object primaryKey) where T : class
        {
            return GetDatabase().Delete_Table_Async<T>(table, primaryKey);
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="poco"></param>
        public async Task Save_Async<T>(T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            await GetDatabase().Save_Async(poco);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="poco"></param>
        /// <returns></returns>
        public async Task Save_Table_Async<T>(string table, T poco) where T : class
        {
            if (poco == null) throw new ArgumentNullException("poco is null");
            await GetDatabase().Save_Table_Async(table, poco);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Update_Async<T>(string sql, params object[] args) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().Update_Async<T>(sql, args);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="args">SQL 参数</param>
        /// <returns></returns>
        public Task<int> Update_Table_Async<T>(string table, string sql, params object[] args) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql is empty.");
            sql = FormatSql(sql);
            return GetDatabase().Update_Table_Async<T>(table, sql, args);
        }

        #endregion Object  Insert Update Delete DeleteById Save

    }
}
#endif