using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ToolGood.ReadyGo3.LinQ.Expressions;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.LinQ
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public partial class WhereHelper<T1> where T1 : class
    {
        internal WhereHelper(SqlHelper helper)
        {
            this._sqlhelper = helper;
            this._paramPrefix = DatabaseProvider.Resolve(_sqlhelper._sqlType).GetParameterPrefix(_sqlhelper._connectionString);
            _sqlExpression = new Expressions.SqlExpression(_sqlhelper._sqlType);
        }
        internal WhereHelper(SqlHelper helper,string table)
        {
            this._sqlhelper = helper;
            this._paramPrefix = DatabaseProvider.Resolve(_sqlhelper._sqlType).GetParameterPrefix(_sqlhelper._connectionString);
            _sqlExpression = new Expressions.SqlExpression(_sqlhelper._sqlType);
            _table = table;
        }

        #region  01 私有变量
        private SqlExpression _sqlExpression;
        private readonly SqlHelper _sqlhelper;
        private List<object> _args = new List<object>();
        private StringBuilder _where = new StringBuilder();
        private string _joinOnString = "";
        private List<SelectHeader> _includeColumns = new List<SelectHeader>();
        private List<string> _excludeColumns = new List<string>();
        private readonly string _paramPrefix;

        private string _order = "";
        private string _groupby = "";
        private string _having = "";
        private bool _useDistinct = false;
        //private bool _useTableName = false;
        private string _tableName = "t1";

        private string _table;
        #endregion

        #region 02 SQL拼接 基础方法

        private bool _doNext = true;

        private bool jump()
        {
            if (_doNext == false) {
                _doNext = true;
                return true;
            }
            return false;
        }

        private void ifTrue(bool iftrue)
        {
            _doNext = iftrue;
        }

        private void whereNotIn(LambdaExpression field, ICollection args)
        {
            if (jump()) return;
            var column = _sqlExpression.GetColumnName(field);
            whereNotIn(column, args);
        }

        private void whereNotIn(string column, ICollection args)
        {
            if (jump()) return;

            if (_where.Length > 0) {
                _where.Append(" AND ");
            }
            if (args.Count == 0) {
                _where.Append("1=1");
                return;
            }
            _where.Append(column);
            if (args.Count == 1) {
                _where.Append(" <> ");
                _where.Append(_paramPrefix);
                _where.Append(_args.Count.ToString());
            } else {
                _where.Append(" NOT IN (");
                for (int i = 0; i < args.Count; i++) {
                    if (i > 0) {
                        _where.Append(",");
                    }
                    _where.Append(_paramPrefix);
                    _where.Append((_args.Count + i).ToString());
                }
                _where.Append(")");
            }
            foreach (var item in args) {
                _args.Add(item);
            }
        }

        private void whereIn(LambdaExpression field, ICollection args)
        {
            if (jump()) return;
            var column = _sqlExpression.GetColumnName(field);
            whereIn(column, args);
        }

        private void whereIn(string column, ICollection args)
        {
            if (jump()) return;

            if (_where.Length > 0) {
                _where.Append(" AND ");
            }
            if (args.Count == 0) {
                _where.Append("1=2");
                return;
            }
            _where.Append(column);
            if (args.Count == 1) {
                _where.Append(" = ");
                _where.Append(_paramPrefix);
                _where.Append(_args.Count.ToString());
            } else {
                _where.Append(" IN (");
                for (int i = 0; i < args.Count; i++) {
                    if (i > 0) {
                        _where.Append(",");
                    }
                    _where.Append(_paramPrefix);
                    _where.Append((_args.Count + i).ToString());
                }
                _where.Append(")");
            }
            foreach (var item in args) {
                _args.Add(item);
            }
        }


        internal void where(string where, ICollection args)
        {
            if (jump()) return;
            where = where.Trim();
            if (_where.Length > 0) _where.Append(" AND ");

            int start = 0;
            if (where.StartsWith("where ", StringComparison.CurrentCultureIgnoreCase)) start = 6;

            bool isInText = false, isStart = false;
            var c = 'a';
            var text = "";

            for (int i = start; i < where.Length; i++) {
                var t = where[i];
                if (isInText) {
                    if (t == c) isInText = false;
                } else if ("\"'`".Contains(t)) {
                    isInText = true;
                    c = t;
                    isStart = false;
                } else if (isStart == false) {
                    if (t == '@') {
                        isStart = true;
                        text = "@";
                        continue;
                    }
                } else if ("@1234567890".Contains(t)) {
                    text += t;
                    continue;
                } else {
                    whereTranslate(_where, text);
                    isStart = false;
                }
                _where.Append(t);
            }
            if (isStart) whereTranslate(_where, text);


            foreach (var item in args) {
                _args.Add(item);
            }
        }
        private void whereTranslate(StringBuilder where, string text)
        {
            if (text == "@@") {
                where.Append("@@");
            } else if (text.Length == 1) {
                where.Append(text);
            } else if (text.StartsWith("@")) {
                int p = this._args.Count + int.Parse(text.Replace("@", ""));
                where.Append(_paramPrefix);
                where.Append(p.ToString());
            } else {
                int p = int.Parse(text.Replace("@", ""));
                where.Append(_paramPrefix);
                where.Append(p.ToString());
            }
        }

        private void where(LambdaExpression where)
        {
            if (jump()) return;
            _sqlExpression.Analysis(where, out string sql);
            if (_where.Length > 0) {
                _where.Append(" AND ");
            }
            _where.Append(sql);
        }

        private void join(string join)
        {
            if (jump()) return;
            _joinOnString += " " + join;
        }

        private void includeColumn(LambdaExpression column, string asName)
        {
            if (jump()) return;
            var col = _sqlExpression.GetColumnName(column);
            includeColumn(col, asName);
        }
        private void includeColumn(string col, string asName)
        {
            if (jump()) return;
            var index = col.IndexOf('.');
            string table = null;
            if (index > -1) {
                table = col.Substring(0, index);
                col = col.Substring(index + 1);
            }
            if (string.IsNullOrWhiteSpace(asName)) {
                _includeColumns.Insert(0, new SelectHeader() {
                    AsName = col,
                    Table = table,
                    QuerySql = col
                });
            } else {
                _includeColumns.Insert(0, new SelectHeader() {
                    AsName = asName,
                    Table = table,
                    QuerySql = col,
                    UseAsName = true,
                });
            }

        }

        private void excludeColumn(LambdaExpression column)
        {
            if (jump()) return;
            var col = _sqlExpression.GetColumnName(column);
            excludeColumn(col);
        }
        private void excludeColumn(string col)
        {
            if (jump()) return;
            var index = col.IndexOf('.');
            if (index > -1) {
                col = col.Substring(index + 1);
            }
            _excludeColumns.Add(col);
        }


        private void orderBy(LambdaExpression order, OrderType type)
        {
            if (jump()) return;
            var column = _sqlExpression.GetColumnName(order);
            if (type == OrderType.Asc) {
                orderBySql(column + " ASC");
            } else {
                orderBySql(column + " DESC");
            }
        }

        private void orderBySql(string order)
        {
            if (jump()) return;
            if (_order.Length > 0) {
                _order += ",";
            }
            _order += order;
        }

        private void groupBy(LambdaExpression group)
        {
            if (jump()) return;
            var column = _sqlExpression.GetColumnName(group);
            this.groupBy(column);
        }

        private void groupBy(string groupby)
        {
            if (jump()) return;
            groupby = groupby.Trim().Trim(',');
            if (groupby.StartsWith("group by ", StringComparison.CurrentCultureIgnoreCase))
                groupby = groupby.Substring(9).Trim();
            if (string.IsNullOrWhiteSpace(groupby)) return;
            if (_groupby.Length > 0) {
                _groupby += ",";
            }
            _groupby += groupby;
        }
        private void having(LambdaExpression having)
        {
            if (jump()) return;
            _sqlExpression.Analysis(having, out string sql);
            this.having(sql);
        }
        private void having(string having)
        {
            if (jump()) return;
            having = having.Trim().Trim(',');
            if (having.StartsWith("having ", StringComparison.CurrentCultureIgnoreCase))
                having = having.Substring(7).Trim();

            if (string.IsNullOrWhiteSpace(having)) return;
            if (_having.Length > 0) {
                _having += ",";
            }
            _having += having;
        }
        #endregion SQL拼接方法

        #region 03 判断

        /// <summary>
        /// IfTrue 如果为假会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="ifTrue"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfTrue(bool ifTrue)
        {
            this.ifTrue(ifTrue);
            return this;
        }

        /// <summary>
        /// IfFalse 如果为真会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="ifTrue"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfFalse(bool ifTrue)
        {
            this.ifTrue(ifTrue == false);
            return this;
        }

        /// <summary>
        /// IfSet  如果字符串未设置，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfSet(string txt)
        {
            this.ifTrue(string.IsNullOrEmpty(txt) == false);
            return this;
        }

        /// <summary>
        /// IfNotSet  如果字符串已设置，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfNotSet(string txt)
        {
            this.ifTrue(string.IsNullOrEmpty(txt));
            return this;
        }

        /// <summary>
        /// IfNull  如果字符串不为空，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfNull(object txt)
        {
            this.ifTrue(object.Equals(txt, null));
            return this;
        }

        /// <summary>
        /// IfNotNull  如果字符串为空，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfNotNull(object txt)
        {
            this.ifTrue(object.Equals(txt, null) == false);
            return this;
        }

        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(int txt)
        {
            this.ifTrue(txt > 0);
            return this;
        }
        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(long txt)
        {
            this.ifTrue(txt > 0);
            return this;
        }

        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(uint txt)
        {
            this.ifTrue(txt > 0);
            return this;
        }
        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(ulong txt)
        {
            this.ifTrue(txt > 0);
            return this;
        }

        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(int? txt)
        {
            this.ifTrue(null != txt && txt > 0);
            return this;
        }
        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(long? txt)
        {
            this.ifTrue(null != txt && txt > 0);
            return this;
        }

        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(uint? txt)
        {
            this.ifTrue(null != txt && txt > 0);
            return this;
        }
        /// <summary>
        /// 如果是正整数，大于0，会影响 Where、OrderBy、AddSelect GroupBy Having On方法
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public WhereHelper<T1> IfPositiveInteger(ulong? txt)
        {
            this.ifTrue(null != txt && txt > 0);
            return this;
        }

        #endregion 判断

        #region 04 Sql 拼接
        /// <summary>
        /// 自动添加 “NOT EXISTS ” 也会自动添加 “SELECT * ”
        /// </summary>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereNotExists(string where, params object[] args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(where)) throw new ArgumentNullException("where");
            where = where.TrimStart();
            if (where.StartsWith("NOT EXISTS ", StringComparison.CurrentCultureIgnoreCase) == false) {
                if (where.StartsWith("SELECT ", StringComparison.CurrentCultureIgnoreCase) == false) {
                    where = $"NOT EXISTS(SELECT * {@where})";
                } else {
                    where = $"NOT EXISTS({@where})";
                }
            }
            this.where(where, args);
            return this;
        }
        /// <summary>
        /// 自动添加 “EXISTS ” 也会自动添加 “SELECT * ”
        /// </summary>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereExists(string where, params object[] args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(where)) throw new ArgumentNullException("where");
            where = where.TrimStart();
            if (where.StartsWith("EXISTS ", StringComparison.CurrentCultureIgnoreCase) == false) {
                if (where.StartsWith("SELECT ", StringComparison.CurrentCultureIgnoreCase) == false) {
                    where = $"EXISTS(SELECT * {@where})";
                } else {
                    where = $"EXISTS({@where})";
                }
            }
            this.where(where, args);
            return this;
        }
        /// <summary>
        /// 添加 Where
        /// </summary>
        /// <param name="where"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> Where(string where, params object[] args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(where)) throw new ArgumentNullException("where");
            this.where(where, args);
            return this;
        }
        /// <summary>
        /// 添加 Order By SQL语句
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public WhereHelper<T1> OrderBy(string order)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(order)) throw new ArgumentNullException("order");
            this.orderBySql(order);
            return this;
        }

        /// <summary>
        /// 添加 Order By SQL语句
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ascORdesc"></param>
        /// <returns></returns>
        public WhereHelper<T1> OrderBy(string order, string ascORdesc)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(order)) throw new ArgumentNullException("order");
            if (string.IsNullOrWhiteSpace(ascORdesc)) {
                this.orderBySql(order);
            } else {
                this.orderBySql(order + " " + ascORdesc);
            }
            return this;
        }

        /// <summary>
        /// 添加 Group By SQL语句
        /// </summary>
        /// <param name="groupby"></param>
        /// <returns></returns>
        public WhereHelper<T1> GroupBy(string groupby)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(groupby)) throw new ArgumentNullException("groupby");
            this.groupBy(groupby);
            return this;
        }
        /// <summary>
        /// 添加 Having SQL语句
        /// </summary>
        /// <param name="having"></param>
        /// <returns></returns>
        public WhereHelper<T1> Having(string having)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(having)) throw new ArgumentNullException("having");
            this.having(having);
            return this;
        }
        /// <summary>
        /// 添加 Join On SQL语句
        /// </summary>
        /// <param name="joinWithOn"></param>
        /// <returns></returns>
        public WhereHelper<T1> JoinOn(string joinWithOn)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(joinWithOn)) throw new ArgumentNullException("joinWithOn");
            this.join(joinWithOn);
            return this;
        }

        /// <summary>
        /// 添加 Where
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereNotIn(string field, ICollection args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(field)) throw new ArgumentNullException("field");
            this.whereNotIn(field, args);
            return this;
        }

        /// <summary>
        /// Where not In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereNotIn(string field, params object[] args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(field)) throw new ArgumentNullException("field");
            this.whereNotIn(field, args);
            return this;
        }

        /// <summary>
        /// 添加 Where
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereIn(string field, ICollection args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(field)) throw new ArgumentNullException("field");
            this.whereIn(field, args);
            return this;
        }

        /// <summary>
        /// Where not In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereIn(string field, params object[] args)
        {
            if (jump()) return this;
            
            if (string.IsNullOrEmpty(field)) throw new ArgumentNullException("field");
            this.whereIn(field, args);
            return this;
        }

        #endregion Sql 拼接

        #region 05 Sql拼接 LINQ WhereIn Where OrderBy Having
        /// <summary>
        /// Where not In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereNotIn<T>(Expression<Func<T1, T>> field, ICollection args)
        {
            this.whereNotIn(field, args);
            return this;
        }
        /// <summary>
        /// Where not In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereNotIn<T>(Expression<Func<T1, T>> field, params object[] args)
        {
            return WhereNotIn(field, (ICollection)args);
        }
        /// <summary>
        /// Where  In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereIn<T>(Expression<Func<T1, T>> field, ICollection args)
        {
            this.whereIn(field, args);
            return this;
        }
        /// <summary>
        /// Where  In
        /// </summary>
        /// <param name="field"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WhereHelper<T1> WhereIn<T>(Expression<Func<T1, T>> field, params object[] args)
        {
            return WhereIn(field, (ICollection)args);
        }
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public WhereHelper<T1> Where(Expression<Func<T1, bool>> where)
        {
            this.where(where);
            return this;
        }
        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="order"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public WhereHelper<T1> OrderBy<T>(Expression<Func<T1, T>> order, OrderType type = OrderType.Asc)
        {
            this.orderBy(order, type);
            return this;
        }
        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public WhereHelper<T1> GroupBy<T>(Expression<Func<T1, T>> group)
        {
            groupBy(group);
            return this;
        }
        /// <summary>
        /// Having
        /// </summary>
        /// <param name="having"></param>
        /// <returns></returns>
        public WhereHelper<T1> Having(Expression<Func<T1, bool>> having)
        {
            this.having(having);
            return this;
        }

        /// <summary>
        /// 非重复
        /// </summary>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public WhereHelper<T1> Distinct(bool distinct = true)
        {
            _useDistinct = distinct;
            return this;
        }

        #endregion WhereIn Where OrderBy Having

        #region 06 查询 Select Page SkipTake FirstOrDefault


        #region Select Page SkipTake First FirstOrDefault
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T1> Select(string selectSql = null)
        {
            return _sqlhelper.Select<T1>(GetFullSelectSql(selectSql), _args.ToArray());
        }
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T1> Select(int limit, string selectSql = null)
        {
            return _sqlhelper.Select<T1>(limit, 0, GetFullSelectSql(selectSql), _args.ToArray());
        }
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T1> Select(int skip, int take, string selectSql = null)
        {
            return _sqlhelper.Select<T1>(skip, take, GetFullSelectSql(selectSql), _args.ToArray());
        }

        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T1> SelectPage(int page, int itemsPerPage, string selectSql = null)
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            return _sqlhelper.Select<T1>((page - 1) * itemsPerPage, itemsPerPage, GetFullSelectSql(selectSql), _args.ToArray());
        }

        /// <summary>
        /// 查询 返回Page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public Page<T1> Page(int page, int itemsPerPage, string selectSql = null)
        {
            return _sqlhelper.Page<T1>(page, itemsPerPage, GetFullSelectSql(selectSql), _args.ToArray());
        }
  
        /// <summary>
        /// 返回第一列
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public T1 FirstOrDefault(string selectSql = null)
        {
            return _sqlhelper.FirstOrDefault<T1>(GetFullSelectSql(selectSql), _args.ToArray());
        }

        #endregion Select Page SkipTake FirstOrDefault

        #region Select Page SkipTake FirstOrDefault
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<T> Select<T>(Expression<Func<T1, T>> columns) where T : class
        {
            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.Select<T>(GetFullSelectSql(sql), _args.ToArray());
        }
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="limit"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<T> Select<T>(int limit, Expression<Func<T1, T>> columns) where T : class
        {
            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.Select<T>(limit, GetFullSelectSql(sql), _args.ToArray());
        }
        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<T> Select<T>(int skip, int take, Expression<Func<T1, T>> columns) where T : class
        {
            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.Select<T>(skip, take, GetFullSelectSql(sql), _args.ToArray());
        }

        /// <summary>
        /// 查询 返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public List<T> SelectPage<T>(int page, int itemsPerPage, Expression<Func<T1, T>> columns) where T : class
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.Select<T>((page - 1) * itemsPerPage, itemsPerPage, GetFullSelectSql(sql), _args.ToArray());
        }
         
        /// <summary>
        /// 返回第一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(Expression<Func<T1, T>> columns) where T : class
        {
            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.FirstOrDefault<T>(GetFullSelectSql(sql), _args.ToArray());
        }

        /// <summary>
        /// 查询 返回Page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public Page<T> Page<T>(int page, int itemsPerPage, Expression<Func<T1, T>> columns) where T : class
        {
            _sqlExpression.GetColumns(columns, out string sql);
            return _sqlhelper.Page<T>(page, itemsPerPage, GetFullSelectSql(sql), _args.ToArray());
        }

        #endregion
        #endregion

        #region 07 查询  Count ExecuteDataTable ExecuteDataSet Select Page FirstOrDefault
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public int SelectCount(string selectSql = null, bool distinct = false)
        {
            return this._sqlhelper.GetDatabase().ExecuteScalar<int>(this.GetCountSql(selectSql, distinct), this._args.ToArray());
        }
        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string selectSql = null)
        {
            return this._sqlhelper.ExecuteDataTable(this.GetFullSelectSql(selectSql), this._args.ToArray());
        }
        //#if !NETSTANDARD2_0
        /// <summary>
        /// 执行返回DataSet
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string selectSql = null)
        {
            return this._sqlhelper.ExecuteDataSet(this.GetFullSelectSql(selectSql), this._args.ToArray());
        }
        //#endif
        /// <summary>
        /// 执行返回集合
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T> Select<T>(string selectSql = null) where T : class
        {
            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.Select<T>(this.GetFullSelectSql(sql), this._args.ToArray());
        }
        /// <summary>
        /// 执行返回集合
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T> Select<T>(int limit, string selectSql = null) where T : class
        {
            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.Select<T>(limit, 0, this.GetFullSelectSql(sql), this._args.ToArray());
        }

        /// <summary>
        /// 执行返回集合
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T> Select<T>(int skip, int take, string selectSql = null) where T : class
        {
            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.Select<T>(skip, take, this.GetFullSelectSql(sql), this._args.ToArray());
        }

        /// <summary>
        /// 执行返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public List<T> SelectPage<T>(int page, int itemsPerPage, string selectSql = null) where T : class
        {
            if (page <= 0) { page = 1; }
            if (itemsPerPage <= 0) { itemsPerPage = 20; }

            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.Select<T>((page - 1) * itemsPerPage, itemsPerPage, this.GetFullSelectSql(sql), this._args.ToArray());
        }
         
        /// <summary>
        /// 返回第一列
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public T FirstOrDefault<T>(string selectSql = null) where T : class
        {
            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.FirstOrDefault<T>(this.GetFullSelectSql(sql), this._args.ToArray());
        }
        /// <summary>
        /// 返回页，page类
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public Page<T> Page<T>(int page, int itemsPerPage, string selectSql = null) where T : class
        {
            var sql = getSelect<T>(selectSql);
            return this._sqlhelper.Page<T>(page, itemsPerPage, this.GetFullSelectSql(sql), this._args.ToArray());
        }

        private string getSelect<T>(string selectSql)
        {
            if (string.IsNullOrEmpty(selectSql) == false) {
                if (selectSql.StartsWith("Select", StringComparison.CurrentCultureIgnoreCase)) {
                    return selectSql;
                }
                return "SELECT " + selectSql;
            }
            return CreateSelectHeader(typeof(T), this._includeColumns);
        }
        #endregion

        #region 08 包含列 排除列
        /// <summary>
        /// 排除列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <returns></returns>
        public WhereHelper<T1> RemoveColumn<T>(Expression<Func<T1, T>> column)
        {
            if (jump()) return this;
            excludeColumn(column);
            return this;
        }
        /// <summary>
        /// 排除列
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public WhereHelper<T1> RemoveColumn(string columns)
        {
            if (jump()) return this;
            var cols = columns.Split(',');
            foreach (var col in cols) {
                excludeColumn(col);
            }
            return this;
        }
        /// <summary>
        /// 包含列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="asName"></param>
        /// <returns></returns>
        public WhereHelper<T1> AddColumn<T>(Expression<Func<T1, T>> column, string asName = null)
        {
            if (jump()) return this;
            includeColumn(column, asName);
            return this;
        }

        /// <summary>
        /// 包含列
        /// </summary>
        /// <param name="columnSql"></param>
        /// <param name="asName"></param>
        /// <returns></returns>
        public WhereHelper<T1> AddColumn(string columnSql, string asName)
        {
            if (jump()) return this;
            if (string.IsNullOrEmpty(columnSql)) throw new ArgumentException(nameof(columnSql));
            if (string.IsNullOrEmpty(asName)) throw new ArgumentException(nameof(asName));
            if (jump()) { return this; }

            _includeColumns.Insert(0, new SelectHeader() {
                AsName = asName,
                Table = _tableName,
                QuerySql = columnSql,
                UseAsName = true
            });
            return this;
        }

        #endregion

        #region 09 获取Sql和args方法
        /// <summary>
        /// 获取 参数 数据
        /// </summary>
        /// <returns></returns>
        public object[] GetArgs()
        {
            return _args.ToArray();
        }

        /// <summary>
        /// 获取 Select SQL语句
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public string GetFullSelectSql(string select = null)
        {
            if (select == null) {
                select = CreateSelectHeader(_includeColumns);
            }

            StringBuilder sb = new StringBuilder();
            if (select.TrimStart().StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase)) {
                sb.Append(select);
            } else {
                sb.Append("SELECT ");
                if (_useDistinct) sb.Append(" DISTINCT ");
                sb.Append(select);
            }
            sb.Append(" ");
            sb.Append(GetFromAndJoinOn());

            if (_where.Length > 0) {
                sb.Append(" WHERE ");
                sb.Append(_where);
            }

            if (_groupby.Length > 0) {
                sb.Append(" GROUP BY ");
                sb.Append(_groupby);
                if (_having.Length > 0) {
                    sb.Append(" HAVING ");
                    sb.Append(_having);
                }
            }
            if (_order.Length > 0) {
                sb.Append(" ORDER BY ");
                sb.Append(_order);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取 Select Count SQL语句
        /// </summary>
        /// <param name="select"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetCountSql(string select = null, bool distinct = false)
        {
            if (select == null) {
                select = "SELECT Count(1) ";
            }
            if (select.TrimStart().StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase) == false) {
                select = distinct ? "SELECT DISTINCT " + select : "SELECT " + select;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(select);
            sb.Append(GetFromAndJoinOn());
            if (_where.Length > 0) {
                sb.Append(" WHERE ");
                sb.Append(_where);
            }

            if (_groupby.Length > 0) {
                sb.Append(" GROUP BY ");
                sb.Append(_groupby);
                if (_having.Length > 0) {
                    sb.Append(" HAVING ");
                    sb.Append(_having);
                }
            }
            return sb.ToString();
        }


        private string CreateSelectHeader(List<SelectHeader> defineHeader)
        {
            var headers = GetSelectHeader();


            if (defineHeader != null && defineHeader.Count > 0) {
                foreach (var header in defineHeader) {
                    SelectHeader h;
                    if (string.IsNullOrEmpty(header.Table)) {
                        h = defineHeader.FirstOrDefault(q => q.AsName == header.AsName);
                    } else {
                        h = defineHeader.FirstOrDefault(q => q.AsName == header.AsName && q.Table == header.Table);
                    }
                    if (h != null) {
                        h.QuerySql = header.QuerySql;
                    } else {
                        headers.Add(h);
                    }
                }
            }
            StringBuilder sb = new StringBuilder();

            foreach (var h in headers) {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(h.QuerySql);
                if (h.UseAsName) {
                    sb.Append(" As '");
                    if (h.Table != _tableName) {
                        sb.Append(h.Table);
                        sb.Append("_");
                    }
                    sb.Append(h.AsName);
                    sb.Append("'");
                }
            }
            if (_useDistinct) {
                sb.Insert(0, "SELECT DISTINCT ");
            } else {
                sb.Insert(0, "SELECT ");
            }
            return sb.ToString();
        }

        private string CreateSelectHeader(Type outType, List<SelectHeader> defineHeader)
        {
            if (defineHeader == null) defineHeader = new List<SelectHeader>();
            defineHeader.AddRange(GetSelectHeader());

            if (outType == typeof(object)) {
                List<string> asNames = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (var h in defineHeader) {
                    if (sb.Length > 0) { sb.Append(","); }
                    sb.Append(h.QuerySql);
                    if (h.UseAsName || asNames.Contains(h.AsName)) {
                        sb.Append(" As '");
                        sb.Append(h.AsName.ToEscapeParam());
                        if (asNames.Contains(h.AsName)) {
                            sb.Append("_");
                            sb.Append(asNames.Count(q => q == h.AsName).ToString());
                        }
                        sb.Append("'");
                    }
                    asNames.Add(h.AsName);
                }
                sb.Insert(0, "SELECT ");
                return sb.ToString();
            } else {
                var pd = PocoData.ForType(outType);
                StringBuilder sb = new StringBuilder();
                foreach (var item in pd.Columns) {
                    var h = defineHeader.FirstOrDefault(q => q.AsName == item.Value.ColumnName);
                    if (h != null) {
                        if (sb.Length > 0) { sb.Append(","); }
                        sb.Append(h.QuerySql);
                        if (h.UseAsName) {
                            sb.Append(" As '");
                            sb.Append(h.AsName.ToEscapeParam());
                            sb.Append("'");
                        }
                    }
                }
                sb.Insert(0, "SELECT ");
                return sb.ToString();
            }
        }

        private List<SelectHeader> GetSelectHeader()
        {
            List<SelectHeader> list = new List<SelectHeader>();
            var provider = DatabaseProvider.Resolve(_sqlhelper._sqlType);


            var pd = PocoData.ForType(typeof(T1));
            foreach (var col in pd.Columns) {
                if (_excludeColumns.Contains(col.Value.ColumnName)) continue;

                SelectHeader header = new SelectHeader {
                    Table = _tableName,
                    AsName = col.Value.ColumnName
                };

                if (col.Value.ResultColumn) {
                    if (string.IsNullOrEmpty(col.Value.ResultSql)) {
                        header.QuerySql = header.Table + "." + provider.EscapeSqlIdentifier(col.Value.ColumnName);
                    } else {
                        header.QuerySql = string.Format(col.Value.ResultSql, header.Table + ".");
                        header.UseAsName = true;
                    }
                } else {
                    header.QuerySql = header.Table + "." + provider.EscapeSqlIdentifier(col.Value.ColumnName);
                }
                list.Add(header);
            }
            foreach (var item in _includeColumns) {
                SelectHeader header = new SelectHeader {
                    AsName = item.AsName,
                    Table = item.Table,
                    QuerySql = item.QuerySql
                };
                list.Add(header);
            }
            return list;
        }

        private string GetFromAndJoinOn()
        {
            var pd1 = PocoData.ForType(typeof(T1));
            var dp = DatabaseProvider.Resolve(_sqlhelper._sqlType);
            StringBuilder sb = new StringBuilder();
            sb.Append("FROM ");
            if (string.IsNullOrEmpty(_table)) {
                sb.Append(dp.GetTableName(pd1));
            } else {
                sb.Append(_table);
            }

            sb.Append(" AS " + _tableName + " ");
            sb.Append(_joinOnString);
            return sb.ToString();
        }

        #endregion

        #region 10 Update
        /// <summary>
        /// 更新数据库,仅支持单一表格更新，WHERE条件为空报错
        /// </summary>
        /// <param name="setData"></param>
        /// <returns></returns>
        public int Update(object setData)
        {
            if (object.Equals(null, setData)) { throw new Exception("No setData Error!"); }
            if (_where.Length == 0) { throw new Exception("No Where Error!"); }

            var pis = setData.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append("SET ");
            int index = 0;
            List<object> args = new List<object>();
            foreach (var pi in pis) {
                if (pi.CanRead == false) continue;
                if (index > 0) { sb.Append(","); }
                sb.AppendFormat("{0}=@{1}", pi.Name, index++);
                args.Add(pi.GetValue(setData, null));
            }
            var sql = BuildUpdateSql(sb.ToString(), args);
            return _sqlhelper.Update<T1>(sql, _args.ToArray());
        }

        /// <summary>
        /// 更新数据库,仅支持单一表格更新，WHERE条件为空报错
        /// </summary>
        /// <param name="setData"></param>
        /// <returns></returns>
        public int Update(IDictionary<string, object> setData)
        {
            if (setData.Count == 0) { throw new Exception("No setData Error!"); }
            if (_where.Length == 0) { throw new Exception("No Where Error!"); }

            StringBuilder sb = new StringBuilder();
            sb.Append("SET ");
            int index = 0;
            List<object> args = new List<object>();
            foreach (var item in setData) {
                if (index > 0) { sb.Append(","); }
                sb.AppendFormat("{0}=@{1}", item.Key, index++);
                args.Add(item.Value);
            }
            var sql = BuildUpdateSql(sb.ToString(), args);
            return _sqlhelper.Update<T1>(sql, _args.ToArray());

        }

        /// <summary>
        /// 更新数据库,仅支持单一表格更新，WHERE条件为空报错
        /// </summary>
        /// <param name="setSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Update(string setSql, params object[] args)
        {
            if (string.IsNullOrEmpty(setSql)) { throw new Exception("No SET Error!"); }
            if (_where.Length == 0) { throw new Exception("No Where Error!"); }
            setSql = setSql.Trim();
            if (setSql.StartsWith("SET ", StringComparison.CurrentCultureIgnoreCase) == false) {
                setSql = "SET " + setSql;
            }
            var sql = BuildUpdateSql(setSql, args);

            return _sqlhelper.Update<T1>(sql, _args.ToArray());
        }

        private string BuildUpdateSql(string setSql, ICollection args)
        {
            StringBuilder sb = new StringBuilder();
            update(sb, setSql, args);
            sb.Append(" ");
            sb.Append(" WHERE ");
            sb.Append(_where);
            return sb.ToString();
        }

        private void update(StringBuilder updateSb, string setSql, ICollection args)
        {
            bool isInText = false, isStart = false;
            var c = 'a';
            var text = "";

            for (int i = 0; i < setSql.Length; i++) {
                var t = setSql[i];
                if (isInText) {
                    if (t == c) isInText = false;
                } else if ("\"'`".Contains(t)) {
                    isInText = true;
                    c = t;
                    isStart = false;
                } else if (isStart == false) {
                    if (t == '@') {
                        isStart = true;
                        text = "@";
                        continue;
                    }
                } else if ("@1234567890".Contains(t)) {
                    text += t;
                    continue;
                } else {
                    whereTranslate(updateSb, text);
                    isStart = false;
                }
                updateSb.Append(t);
            }
            if (isStart) whereTranslate(updateSb, text);


            foreach (var item in args) {
                _args.Add(item);
            }
        }
        #endregion

        #region 11 Delete
        /// <summary>
        /// 删除，只支持单一表格，WHERE条件为空报错
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (_where.Length == 0) { throw new Exception("No Where Error!"); }
            return _sqlhelper.Delete<T1>($"WHERE {_where.ToString()}", _args.ToArray());
        }

        #endregion

        #region 12 SelectInsert
        /// <summary>
        /// 查询插入
        /// </summary>
        /// <param name="insertTableName"></param>
        /// <param name="replaceSelect"></param>
        /// <param name="args"></param>
        public void SelectInsert(string insertTableName = null, string replaceSelect = null, params object[] args)
        {
            var sql = CreateSelectInsertSql(typeof(T1), insertTableName, replaceSelect, args);
            _sqlhelper.Execute(sql, _args.ToArray());
        }

        /// <summary>
        /// 查询插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertTableName"></param>
        /// <param name="replaceSelect"></param>
        /// <param name="args"></param>
        public void SelectInsert<T>(string insertTableName = null, string replaceSelect = null, params object[] args)
        {
            var sql = CreateSelectInsertSql(typeof(T), insertTableName, replaceSelect, args);
            _sqlhelper.Execute(sql, _args.ToArray());
        }

        private string CreateSelectInsertSql(Type type, string insertTableName, string replaceColumns, object[] args)
        {
            Dictionary<string, string> replaceCols = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var Provider = DatabaseProvider.Resolve(_sqlhelper._sqlType);
            if (string.IsNullOrEmpty(replaceColumns) == false) {
                var columnSqls = Provider.FormatSql(replaceColumns, args).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in columnSqls) {
                    var sp = item.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var header = sp[sp.Length - 1].Replace("'", "").Replace("\"", "");
                    replaceCols[header] = item;
                }
            }

            var pd = PetaPoco.Core.PocoData.ForType(type);
            var pocoData = PetaPoco.Core.PocoData.ForType(typeof(T1));

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            if (string.IsNullOrEmpty(insertTableName)) {
                sb.Append(Provider.GetTableName(pd));
            } else {
                sb.Append(insertTableName);
            }
            sb.Append("(");
            Dictionary<string, string> selectColumns = new Dictionary<string, string>();
            foreach (var item in pd.Columns) {
                var colName = item.Key;
                if (colName == pd.TableInfo.PrimaryKey) continue;
                if (item.Value.ResultColumn) continue;

                if (replaceCols.TryGetValue(colName, out string sql)) {
                    selectColumns[Provider.EscapeSqlIdentifier(colName)] = sql;
                } else if (pocoData.Columns.ContainsKey(colName)) {
                    var ci = pocoData.Columns[colName];
                    selectColumns[Provider.EscapeSqlIdentifier(colName)] = _tableName + "." + ci.ColumnName;
                }
            }
            sb.Append(string.Join(",", selectColumns.Keys));
            sb.Append(") SELECT ");
            if (_useDistinct) sb.Append("DISTINCT ");
            sb.Append(string.Join(",", selectColumns.Values));
            sb.Append(" ");
            sb.Append(GetFromAndJoinOn());

            if (_where.Length > 0) {
                sb.Append(" WHERE ");
                sb.Append(_where);
            }

            if (_groupby.Length > 0) {
                sb.Append(" GROUP BY ");
                sb.Append(_groupby);
                if (_having.Length > 0) {
                    sb.Append(" HAVING ");
                    sb.Append(_having);
                }
            }
            if (_order.Length > 0) {
                sb.Append(" ORDER BY ");
                sb.Append(_order);
            }
            return sb.ToString();
        }

        #endregion


        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _sqlExpression = null;
            _args = null;
            _where = null;
            _joinOnString = null;

            _order = null;
            _groupby = null;
            _having = null;

            _includeColumns = null;
            _excludeColumns = null;
        }



    }
}
