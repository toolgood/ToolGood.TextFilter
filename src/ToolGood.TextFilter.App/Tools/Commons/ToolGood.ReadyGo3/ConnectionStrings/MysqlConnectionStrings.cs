using System.Collections.Generic;
using System.Text;

namespace ToolGood.ReadyGo3
{
    /// <summary>
    /// mysql连接字符串
    /// https://blog.csdn.net/config_man/article/details/8254986
    /// </summary>
    public class MysqlConnectionString
    {
        /// <summary>
        /// 数据库位置(以上任何关键字均可)
        /// </summary>
        public string Server { set { SetValue("Server", value); } }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string Database { set { SetValue("Database", value); } }

        /// <summary>
        ///  socket 端口，默认 3306
        /// </summary>
        public int? Port { set { SetValue("Port", value); } }

        /// <summary>
        /// 连接协议，默认 Sockets
        /// </summary>
        public string ConnectionProtocol { set { SetValue("ConnectionProtocol", value); } }

        /// <summary>
        /// 连接管道，默认 MYSQL
        /// </summary>
        public string PipeName { set { SetValue("PipeName", value); } }

        /// <summary>
        /// 连接是否压缩，默认 false
        /// </summary>
        public bool? UseCompression { set { SetValue("UseCompression", value); } }

        /// <summary>
        /// 是否允许一次执行多条SQL语句，默认 true
        /// </summary>
        public bool? AllowBatch { set { SetValue("AllowBatch", value); } }

        /// <summary>
        /// 是否启用日志，默认 false
        /// </summary>
        public bool? Logging { set { SetValue("Logging", value); } }

        /// <summary>
        /// 内存共享的名称，默认 MYSQL
        /// </summary>
        public string SharedMemoryName { set { SetValue("SharedMemoryName", value); } }

        /// <summary>
        /// 是否兼容旧版的语法，默认 false
        /// </summary>
        public bool? UseOldSyntax { set { SetValue("UseOldSyntax", value); } }

        /// <summary>
        /// 连接超时等待时间，默认15s
        /// </summary>
        public int? ConnectionTimeout { set { SetValue("ConnectionTimeout", value); } }

        /// <summary>
        /// MySqlCommand 超时时间，默认 30s
        /// </summary>
        public int? DefaultCommandTimeout { set { SetValue("DefaultCommandTimeout", value); } }

        /// <summary>
        /// 数据库登录帐号
        /// </summary>
        public string UserID { set { SetValue("UserID", value); } }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { set { SetValue("Password", value); } }

        /// <summary>
        /// 是否保持敏感信息，默认 false
        /// </summary>
        public bool? PersistSecurityInfo { set { SetValue("PersistSecurityInfo", value); } }

        /// <summary>
        /// 已经用 SSL 替代了，默认 false
        /// </summary>
        public bool? Encrypt { set { SetValue("Encrypt", value); } }

        /// <summary>
        /// 证书文件(.pfx)格式
        /// </summary>
        public string CertificateFile { set { SetValue("CertificateFile", value); } }

        /// <summary>
        /// 证书的密码
        /// </summary>
        public string CertificatePassword { set { SetValue("CertificatePassword", value); } }

        /// <summary>
        /// 证书的存储位置
        /// </summary>
        public string CertificateStoreLocation { set { SetValue("CertificateStoreLocation", value); } }

        /// <summary>
        /// 证书指纹
        /// </summary>
        public string CertificateThumbprint { set { SetValue("CertificateThumbprint", value); } }

        /// <summary>
        /// 日期时间能否为零，默认 false
        /// </summary>
        public bool? AllowZeroDateTime { set { SetValue("AllowZeroDateTime", value); } }
        /// <summary>
        /// 为零的日期时间是否转化为 DateTime.MinValue，默认 false
        /// </summary>
        public bool? ConvertZeroDateTime { set { SetValue("ConvertZeroDateTime", value); } }
        /// <summary>
        /// 是否启用助手，会影响数据库性能，默认 false
        /// </summary>
        public bool? UseUsageAdvisor { set { SetValue("UseUsageAdvisor", value); } }
        /// <summary>
        /// 同一时间能缓存几条存储过程，0为禁止，默认 25
        /// </summary>
        public int? ProcedureCacheSize { set { SetValue("ProcedureCacheSize", value); } }
        /// <summary>
        /// 是否启用性能监视，默认 false
        /// </summary>
        public bool? UsePerformanceMonitor { set { SetValue("UsePerformanceMonitor", value); } }
        /// <summary>
        /// 是否忽略 Prepare() 调用，默认 true
        /// </summary>
        public bool? IgnorePrepare { set { SetValue("IgnorePrepare", value); } }

        /// <summary>
        /// 是否检查存储过程体、参数的有效性，默认 true
        /// </summary>
        public bool? UseProcedureBodies { set { SetValue("UseProcedureBodies", value); } }
        /// <summary>
        ///  是否自动使用活动的连接，默认 true
        /// </summary>
        public bool? AutoEnlist { set { SetValue("AutoEnlist", value); } }

        /// <summary>
        /// 是否响应列上元数据的二进制标志，默认 true
        /// </summary>
        public bool? RespectBinaryFlags { set { SetValue("RespectBinaryFlags", value); } }

        /// <summary>
        /// 是否将 TINYINT(1) 列视为布尔型，默认 true
        /// </summary>
        public bool? TreatTinyAsBoolean { set { SetValue("TreatTinyAsBoolean", value); } }

        /// <summary>
        /// 是否允许 SQL 中出现用户变量，默认 false
        /// </summary>
        public bool? AllowUserVariables { set { SetValue("AllowUserVariables", value); } }

        /// <summary>
        /// 会话是否允许交互，默认 false
        /// </summary>
        public bool? InteractiveSession { set { SetValue("InteractiveSession", value); } }
        /// <summary>
        /// 所有服务器函数是否按返回字符串处理，默认 false
        /// </summary>
        public bool? FunctionsReturnString { set { SetValue("FunctionsReturnString", value); } }
        /// <summary>
        /// 是否用受影响的行数替代查找到的行数来返回数据，默认 false
        /// </summary>
        public bool? UseAffectedRows { set { SetValue("UseAffectedRows", value); } }
        /// <summary>
        /// 是否将 binary(16) 列作为 Guids，默认 false
        /// </summary>
        public bool? OldGuids { set { SetValue("OldGuids", value); } }
        /// <summary>
        /// 保持 TCP 连接的秒数，默认0，不保持。
        /// </summary>
        public int? Keepalive { set { SetValue("Keepalive", value); } }
        /// <summary>
        /// 连接被销毁前在连接池中保持的最少时间（秒）。默认 0
        /// </summary>
        public int? ConnectionLifeTime { set { SetValue("ConnectionLifeTime", value); } }
        /// <summary>
        ///  是否使用线程池，默认 true
        /// </summary>
        public bool? Pooling { set { SetValue("Pooling", value); } }

        /// <summary>
        /// 线程池中允许的最少线程数，默认 0
        /// </summary>
        public int? MinimumPoolSize { set { SetValue("MinimumPoolSize", value); } }
        /// <summary>
        /// 线程池中允许的最多线程数，默认 100
        /// </summary>
        public int? MaximumPoolSize { set { SetValue("MaximumPoolSize", value); } }

        /// <summary>
        /// 连接过期后是否自动复位，默认 false
        /// </summary>
        public bool? ConnectionReset { set { SetValue("ConnectionReset", value); } }
        /// <summary>
        /// 向服务器请求连接所使用的字符集，默认：无
        /// </summary>
        public string CharacterSet { set { SetValue("CharacterSet", value); } }

        /// <summary>
        /// 列的匹配模式，一旦匹配将按 utf8 处理，默认：无
        /// </summary>
        public string TreatBlobsAsUTF8 { set { SetValue("TreatBlobsAsUTF8", value); } }

        /// <summary>
        /// 是否启用 SSL 连接模式，默认： None
        /// </summary>
        public string SslMode { set { SetValue("SslMode", value); } }



        private readonly Dictionary<string, string> _dict = new Dictionary<string, string>();
        /// <summary>
        /// 设置值, value为null时删除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, object value)
        {
            if (value == null) {
                _dict.Remove(key);
            } else if (value is bool?) {
                var b = (value as bool?);
                _dict[key] = b.Value.ToString().ToLower();
            } else {
                _dict[key] = value.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in _dict) {
                stringBuilder.AppendFormat("{0}={1};", item.Key, item.Value);
            }
            return stringBuilder.ToString();
        }
    }
}
