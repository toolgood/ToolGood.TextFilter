using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Enums;
using ToolGood.ReadyGo3.Gadget.Internals;
using ToolGood.ReadyGo3.PetaPoco.Internal;
using ToolGood.ReadyGo3.PetaPoco.Providers;
using ToolGood.ReadyGo3.PetaPoco.Utilities;

namespace ToolGood.ReadyGo3.PetaPoco.Core
{
    /// <summary>
    ///     Base class for DatabaseType handlers - provides default/common handling for different database engines
    /// </summary>
    abstract partial class DatabaseProvider
    {
        /// <summary>
        ///     Gets the DbProviderFactory for this database provider.
        /// </summary>
        /// <returns>The provider factory.</returns>
        public abstract DbProviderFactory GetFactory();

        /// <summary>
        ///     Gets a flag for whether the DB has native support for GUID/UUID.
        /// </summary>
        public virtual bool HasNativeGuidSupport {
            get { return false; }
        }

        /// <summary>
        ///     Gets the <seealso cref="PagingHelper" /> this provider supplies.
        /// </summary>
        public virtual PagingHelper PagingUtility {
            get { return PagingHelper.Instance; }
        }

        ///// <summary>
        /////     Escape a tablename into a suitable format for the associated database provider.
        ///// </summary>
        ///// <param name="tableName">
        /////     The name of the table (as specified by the client program, or as attributes on the associated
        /////     POCO class.
        ///// </param>
        ///// <returns>The escaped table name</returns>
        //public virtual string GetTableName(string tableName)
        //{
        //    // Assume table names with "dot" are already escaped
        //    return tableName.IndexOf('.') >= 0 ? tableName : EscapeSqlIdentifier(tableName);
        //}

        /// <summary>
        ///     Escape and arbitary SQL identifier into a format suitable for the associated database provider
        /// </summary>
        /// <param name="sqlIdentifier">The SQL identifier to be escaped</param>
        /// <returns>The escaped identifier</returns>
        public virtual string EscapeSqlIdentifier(string sqlIdentifier)
        {
            return $"[{sqlIdentifier}]";
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetTableName(PocoData data)
        {
            var ti = data.TableInfo;
            var databaseName = ti.DatabaseName;
            var schemaName = ti.SchemaName;
            var tableName = ti.TableName;
            return GetTableName(databaseName, schemaName, tableName);
        }

        ///// <summary>
        ///// 获取表名 ,不带databaseName ， schemaName
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public string GetMiniTableName(PocoData data)
        //{
        //    var ti = data.TableInfo;
        //    var tableName = ti.TableName;
        //    return GetTableName(null, null, tableName);
        //}

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual string GetTableName(string databaseName, string schemaName, string tableName)
        {
            if (string.IsNullOrEmpty(databaseName) == false) {
                if (string.IsNullOrEmpty(schemaName) == false) {
                    return $"[{databaseName}].[{schemaName}].[{tableName}]";
                }
                return $"[{databaseName}].[dbo].[{tableName}]";
            }
            if (string.IsNullOrEmpty(schemaName) == false) {
                return $"[{schemaName}].[{tableName}]";
            }
            return $"[{tableName}]";
        }

        public virtual string GetTableName(string tableName)
        {
            tableName = tableName.Replace("[", "").Replace("]", "").Replace("`", "").Replace("\"", "");
            var ts = tableName.Split('.');
            if (ts.Length >= 3) {
                return GetTableName(ts[0], ts[1], ts[2]);
            }
            if (ts.Length == 2) {
                return GetTableName(null, ts[0], ts[1]);
            }
            return GetTableName(null, null, ts[0]);
        }

        /// <summary>
        ///     Returns the prefix used to delimit parameters in SQL query strings.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The providers character for prefixing a query parameter.</returns>
        public virtual string GetParameterPrefix(string connectionString)
        {
            return "@";
        }

        /// <summary>
        ///     Converts a supplied C# object value into a value suitable for passing to the database
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted value</returns>
        public virtual object MapParameterValue(object value)
        {
            if (value is bool)
                return ((bool)value) ? 1 : 0;

            return value;
        }

        /// <summary>
        ///     Called immediately before a command is executed, allowing for modification of the IDbCommand before it's passed to
        ///     the database provider
        /// </summary>
        /// <param name="cmd"></param>
        public virtual void PreExecute(IDbCommand cmd)
        {
        }

        /// <summary>
        ///     Builds an SQL query suitable for performing page based queries to the database
        /// </summary>
        /// <param name="skip">The number of rows that should be skipped by the query</param>
        /// <param name="take">The number of rows that should be retruend by the query</param>
        /// <param name="parts">The original SQL query after being parsed into it's component parts</param>
        /// <param name="args">Arguments to any embedded parameters in the SQL query</param>
        /// <returns>The final SQL query that should be executed.</returns>
        public virtual string BuildPageQuery(int skip, int take, SQLParts parts, ref object[] args)
        {
            string sql;
            if (skip > 0) {
                sql = $"{parts.Sql}\nLIMIT @{args.Length} OFFSET @{args.Length + 1}";
                args = args.Concat(new object[] { take, skip }).ToArray();
            } else {
                sql = $"{parts.Sql}\nLIMIT @{args.Length}";
                args = args.Concat(new object[] { take }).ToArray();
            }
            return sql;
        }

        /// <summary>
        ///     Returns an SQL Statement that can check for the existence of a row in the database.
        /// </summary>
        /// <returns></returns>
        public virtual string GetExistsSql()
        {
            return "SELECT COUNT(*) FROM {0} WHERE {1}";
        }

        /// <summary>
        ///     Return an SQL expression that can be used to populate the primary key column of an auto-increment column.
        /// </summary>
        /// <param name="tableInfo">Table info describing the table</param>
        /// <returns>An SQL expressions</returns>
        /// <remarks>See the Oracle database type for an example of how this method is used.</remarks>
        public virtual string GetAutoIncrementExpression(TableInfo tableInfo)
        {
            return null;
        }

        /// <summary>
        ///     Returns an SQL expression that can be used to specify the return value of auto incremented columns.
        /// </summary>
        /// <param name="primaryKeyName">The primary key of the row being inserted.</param>
        /// <returns>An expression describing how to return the new primary key value</returns>
        /// <remarks>See the SQLServer database provider for an example of how this method is used.</remarks>
        public virtual string GetInsertOutputClause(string primaryKeyName)
        {
            return string.Empty;
        }

        /// <summary>
        ///     Performs an Insert operation
        /// </summary>
        /// <param name="database">The calling Database object</param>
        /// <param name="cmd">The insert command to be executed</param>
        /// <param name="primaryKeyName">The primary key of the table being inserted into</param>
        /// <returns>The ID of the newly inserted record</returns>
        public virtual object ExecuteInsert(Database database, IDbCommand cmd, string primaryKeyName)
        {
            cmd.CommandText += ";\nSELECT @@IDENTITY AS NewID;";
            return database.ExecuteScalarHelper(cmd);
        }


        private DbProviderFactory providerFactory = null;
        /// <summary>
        ///     Returns the .net standard conforming DbProviderFactory.
        /// </summary>
        /// <param name="assemblyQualifiedNames">The assembly qualified name of the provider factory.</param>
        /// <returns>The db provider factory.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="assemblyQualifiedNames" /> does not match a type.</exception>
        protected DbProviderFactory GetFactory(params string[] assemblyQualifiedNames)
        {
            if (providerFactory == null) {
                Type ft = null;
                foreach (var assemblyName in assemblyQualifiedNames) {
                    ft = Type.GetType(assemblyName);
                    if (ft != null) break;
                }

                if (ft == null) throw new ArgumentException("Could not load the " + GetType().Name + " DbProviderFactory.");

                providerFactory = (DbProviderFactory)ft.GetField("Instance").GetValue(null);
            }
            return providerFactory;
        }

        internal static DatabaseProvider Resolve(SqlType type)
        {
            switch (type) {
                case SqlType.SqlServer: return Singleton<SqlServerDatabaseProvider>.Instance;
                case SqlType.MySql: return Singleton<MySqlDatabaseProvider>.Instance;
                case SqlType.SQLite: return Singleton<SQLiteDatabaseProvider>.Instance;
                case SqlType.MsAccessDb: return Singleton<MsAccessDbDatabaseProvider>.Instance;
                case SqlType.Oracle: return Singleton<OracleDatabaseProvider>.Instance;
                case SqlType.PostgreSQL: return Singleton<PostgreSQLDatabaseProvider>.Instance;
                case SqlType.FirebirdDb: return Singleton<FirebirdDbDatabaseProvider>.Instance;
                case SqlType.MariaDb: return Singleton<MySqlDatabaseProvider>.Instance;
                case SqlType.SqlServerCE: return Singleton<SqlServerCEDatabaseProviders>.Instance;
                case SqlType.SqlServer2012: return Singleton<SqlServer2012DatabaseProvider>.Instance;
                default: break;
            }
            return Singleton<SqlServerDatabaseProvider>.Instance;
        }

        internal static SqlType GetSqlType(string providerNameOrTypeName, string connectionString)
        {
            if (providerNameOrTypeName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.MySql;
            if (providerNameOrTypeName.IndexOf("MariaDb", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.MariaDb;
            if (providerNameOrTypeName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                providerNameOrTypeName.IndexOf("SqlCeConnection", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                providerNameOrTypeName.IndexOf("SqlCe", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.SqlServerCE;
            if (providerNameOrTypeName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0
                || providerNameOrTypeName.IndexOf("pgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.PostgreSQL;
            if (providerNameOrTypeName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.Oracle;
            if (providerNameOrTypeName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.SQLite;
            if (providerNameOrTypeName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.Oracle;
            if (providerNameOrTypeName.IndexOf("Firebird", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                providerNameOrTypeName.IndexOf("FbConnection", StringComparison.InvariantCultureIgnoreCase) >= 0) return SqlType.FirebirdDb;
            if (providerNameOrTypeName.StartsWith("FbConnection") || providerNameOrTypeName.EndsWith("FirebirdClientFactory")) return SqlType.FirebirdDb;

            if (providerNameOrTypeName.IndexOf("OleDb", StringComparison.InvariantCultureIgnoreCase) >= 0
                && (connectionString.IndexOf("Jet.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0
                || connectionString.IndexOf("ACE.OLEDB", StringComparison.InvariantCultureIgnoreCase) > 0)) {
                return SqlType.MsAccessDb;
            }
            if (providerNameOrTypeName.IndexOf("SqlServer", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                providerNameOrTypeName.IndexOf("System.Data.SqlClient", StringComparison.InvariantCultureIgnoreCase) >= 0)
                return SqlType.SqlServer;
            if (providerNameOrTypeName.Equals("SqlConnection") || providerNameOrTypeName.Equals("SqlClientFactory")) return SqlType.SqlServer;

            // Assume SQL Server
            return SqlType.SqlServer;
        }

        ///// <summary>
        /////     Unwraps a wrapped <see cref="DbProviderFactory"/>.
        ///// </summary>
        ///// <param name="factory">The factory to unwrap.</param>
        ///// <returns>The unwrapped factory or the original factory if no wrapping occurred.</returns>
        //internal static DbProviderFactory Unwrap(DbProviderFactory factory)
        //{
        //    var sp = factory as IServiceProvider;

        //    if (sp == null)
        //        return factory;

        //    var unwrapped = sp.GetService(factory.GetType()) as DbProviderFactory;
        //    return unwrapped == null ? factory : Unwrap(unwrapped);
        //}


        public virtual string CreateSql(int limit, int offset, string columnSql, string fromtable, string order, string where)
        {
            throw new Exception("不支持！！！");
        }



        /// <summary>
        /// 创建生成SQL Function
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual string CreateFunction(SqlFunction function, params object[] args)
        {
            switch (function) {
                case SqlFunction.Len: return CreateFunction("LEN({0})", args);
                //case SqlFunction.Max: return CreateFunction("MAX({0})", args);
                //case SqlFunction.Min: return CreateFunction("MIN({0})", args);
                //case SqlFunction.Avg: return CreateFunction("AVG({0})", args);
                //case SqlFunction.Sum: return CreateFunction("SUM({0})", args);
                //case SqlFunction.Count: return CreateFunction("COUNT({0})", args);
                //case SqlFunction.CountDistinct: return CreateFunction("COUNT(DISTINCT {0})", args);
                //case SqlFunction.DatePart: return CreateFunction("DATEPART({0},{1})", args);
                //case SqlFunction.DateDiff: return CreateFunction("DATEDIFF({0},{1})", args);
                case SqlFunction.Year: return CreateFunction("YEAR({0})", args);
                case SqlFunction.Month: return CreateFunction("MONTH({0})", args);
                case SqlFunction.Day: return CreateFunction("DAY({0})", args);
                case SqlFunction.Hour: return CreateFunction("HOUR({0})", args);
                case SqlFunction.Minute: return CreateFunction("MINUTE({0})", args);
                case SqlFunction.Second: return CreateFunction("SECOND({0})", args);
                case SqlFunction.DayOfYear: return CreateFunction("DAYOFYEAR({0})", args);
                //case SqlFunction.Week: return CreateFunction("WEEK({0})", args);
                case SqlFunction.WeekDay: return CreateFunction("WEEKDAY({0})", args);
                case SqlFunction.SubString3: return CreateFunction("SUBSTRING({0},{1},{2})", args);
                case SqlFunction.SubString2: return CreateFunction("SUBSTRING({0},{1})", args);
                //case SqlFunction.Left: return CreateFunction("LEFT({0},{1})", args);
                //case SqlFunction.Right: return CreateFunction("RIGHT({0},{1})", args);
                case SqlFunction.Lower: return CreateFunction("LOWER({0})", args);
                case SqlFunction.Upper: return CreateFunction("UPPER({0})", args);
                //case SqlFunction.Ascii: return CreateFunction("ASCII({0})", args);
                case SqlFunction.Trim: return CreateFunction("LTRIM(RTRIM({0}))", args);
                case SqlFunction.LTrim: return CreateFunction("LTRIM({0})", args);
                case SqlFunction.RTrim: return CreateFunction("RTRIM({0})", args);
                case SqlFunction.Concat:
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("CONCAT(");
                    foreach (var item in args) {
                        stringBuilder.Append(EscapeParam(item));
                        stringBuilder.Append(',');
                    }
                    stringBuilder[stringBuilder.Length - 1] = ')';
                    return stringBuilder.ToString();
                case SqlFunction.IndexOf: return CreateFunction("(INSTR({0},{1})-1)", args);
                case SqlFunction.Replace: return CreateFunction("REPLACE({0},{1},{2})", args);
                default: break;
            }
            var objs = new object[args.Length - 1];
            for (int i = 0; i < objs.Length; i++) {
                objs[i] = args[i + 1];
            }
            return CreateFunction(args[0].ToString(), objs, 0);
        }
        /// <summary>
        /// 创建生成SQL Function
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string CreateFunction(string sql, object[] args, int index = 0)
        {
            List<string> list = new List<string>();
            for (int i = index; i < args.Length; i++) {
                if (i == 0) {
                    var value = args[0];
                    list.Add(value.ToString());
                } else {
                    list.Add(EscapeParam(args[i]));
                }
            }
            return string.Format(sql, list.ToArray());
        }

        protected bool usedEscapeSql = false;
        protected char escapeSql = '`';

        /// <summary>
        /// 格式SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string FormatSql(string sql, object[] args)
        {
            if (args == null || args.Length == 0) {
                return sql;
            } else if (sql.Contains("@") == false) {
                return sql;
            }

            StringBuilder _where = new StringBuilder();

            bool isInText = false, isStart = false, isInTableColumn = false;
            var c = 'a';
            var text = "";

            for (int i = 0; i < sql.Length; i++) {
                var t = sql[i];
                if (isInText) {
                    if (t == c) isInText = false;
                } else if (isInTableColumn) {
                    if (t == ']') {
                        isInTableColumn = false;
                        if (usedEscapeSql) {
                            _where.Append(escapeSql);
                            continue;
                        }
                    }
                } else if (t == '[') {
                    isInTableColumn = true;
                    if (usedEscapeSql) {
                        _where.Append(escapeSql);
                        continue;
                    }
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
                    WhereTranslate(_where, text, args);
                    isStart = false;
                }
                _where.Append(t);
            }
            if (isStart) WhereTranslate(_where, text, args);

            return _where.ToString();
        }
        private void WhereTranslate(StringBuilder where, string text, object[] args)
        {
            int p = int.Parse(text.Replace("@", ""));
            var value = args[p];
            if (value is ICollection) {
                if (((ICollection)value).Count == 0) {
                    where.Append("(Null)");
                    where.Append(" AND 1=2 ");
                } else {
                    where.Append("(");
                    foreach (var item in (ICollection)value) {
                        where.Append(EscapeParam(item));
                        where.Append(",");
                    }
                    where[where.Length - 1] = ')';
                }
            } else {
                where.Append(EscapeParam(value));
            }
        }



        /// <summary>
        /// 转化成SQL语言的片段，value不能为Null.
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string EscapeParam(object value)
        {
            if (object.Equals(value, null)) return "NULL";

            var fieldType = value.GetType();
            if (fieldType.IsEnum) {
                var isEnumFlags = fieldType.IsEnum;
                if (!isEnumFlags && Int64.TryParse(value.ToString(), out long enumValue)) {
                    value = Enum.ToObject(fieldType, enumValue).ToString();
                }
                var enumString = value.ToString();
                return !isEnumFlags ? "'" + enumString.Trim('"') + "'" : enumString;
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
                return "'" + txt + "'";
            }
            return "'" + value.ToString() + "'";
        }

    }
}
