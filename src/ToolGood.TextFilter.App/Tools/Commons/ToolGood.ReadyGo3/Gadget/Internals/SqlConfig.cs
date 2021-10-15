using System.Data;
using ToolGood.ReadyGo3.PetaPoco.Core;

namespace ToolGood.ReadyGo3.Gadget.Internals
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class SqlConfig
    {
        internal SqlHelper _sqlHelper;

        internal SqlConfig(SqlHelper helper)
        {
            _sqlHelper = helper;
        }
        #region 属性
        /// <summary>
        /// 插入操作时，默认时间为现在时间
        /// </summary>
        public bool Insert_DateTime_Default_Now { get { return _sqlHelper._setDateTimeDefaultNow; } set { _sqlHelper._setDateTimeDefaultNow = value; } }
        /// <summary>
        /// 插入操作时，默认字符串非Null
        /// </summary>
        public bool Insert_String_Default_NotNull { get { return _sqlHelper._setStringDefaultNotNull; } set { _sqlHelper._setStringDefaultNotNull = value; } }

        /// <summary>
        /// 插入操作时，默认Grid自动生成
        /// </summary>
        public bool Insert_Guid_Default_New { get { return _sqlHelper._setGuidDefaultNew; } set { _sqlHelper._setGuidDefaultNew = value; } }

        /// <summary>
        /// 查询 First 使用 limit 1
        /// </summary>
        public bool Select_First_With_Limit_1 { get { return _sqlHelper._sql_firstWithLimit1; } set { _sqlHelper._sql_firstWithLimit1 = value; } }

        /// <summary>
        /// SQL语言类型
        /// </summary>
        public SqlType SqlType { get { return _sqlHelper._sqlType; } }

        /// <summary>
        /// 数据库链接字符串【写】
        /// </summary>
        public string ConnectionString { get { return _sqlHelper._connectionString; } }

        /// <summary>
        /// 数据库执行超出时间
        /// </summary>
        public int CommandTimeout { get { return _sqlHelper._commandTimeout; } set { _sqlHelper._commandTimeout = value; } }
        /// <summary>
        /// 事务级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get { return _sqlHelper._isolationLevel; } set { _sqlHelper._isolationLevel = value; } }

        #endregion

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void FlushFunctionCaches()
        {
            PocoData.FlushCaches();
        }

    }
}