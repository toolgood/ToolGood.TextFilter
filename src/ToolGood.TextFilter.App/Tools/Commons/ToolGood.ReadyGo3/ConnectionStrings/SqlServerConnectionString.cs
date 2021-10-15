using System.Collections.Generic;
using System.Text;

namespace ToolGood.ReadyGo3
{
    /// <summary>
    /// sqlserver连接字符串
    /// https://blog.csdn.net/usoa/article/details/43405163
    /// </summary>
    public class SqlServerConnectionString
    {
        /// <summary>
        /// Application Name（应用程序名称）：应用程序的名称。如果没有被指定的话，它的值为.NET SqlClient Data Provider（数据提供程序）.
        /// </summary>
        public string ApplicationName { set { SetValue("Application Name", value); } }

        /// <summary>
        ///     AttachDBFilename／extended properties（扩展属性）／Initial File Name（初始文件名）：可连接数据库的主要文件的名称，包括完整路径名称。数据库名称必须用关键字数据库指定。
        /// </summary>
        public string AttachDBFilename { set { SetValue("AttachDBFilename", value); } }

        /// <summary>
        /// Connect Timeout（连接超时）／Connection Timeout（连接超时）：一个到服务器的连接在终止之前等待的时间长度（以秒计），缺省值为15。
        /// </summary>
        public int? ConnectTimeout { set { SetValue("Connect Timeout", value); } }

        /// <summary>
        ///    Connection Lifetime（连接生存时间）：当一个连接被返回到连接池时，它的创建时间会与当前时间进行对比。如果这个时间跨度超过了连接的有效期的话，连接就被取消。其缺省值为0。
        /// </summary>
        public int? ConnectionLifetime { set { SetValue("Connection Lifetime", value); } }

        /// <summary>
        /// Connection Reset（连接重置）：表示一个连接在从连接池中被移除时是否被重置。一个伪的有效在获得一个连接的时候就无需再进行一个额外的服务器来回运作，其缺省值为真。
        /// </summary>
        public bool? ConnectionReset { set { SetValue("Connection Reset", value); } }

        /// <summary>
        /// Current Language（当前语言）：SQL Server语言记录的名称。
        /// </summary>
        public string CurrentLanguage { set { SetValue("Current Language", value); } }

        /// <summary>
        ///     Data Source（数据源）／Server（服务器）／Address（地址）／Addr（地址）／Network Address（网络地址）：SQL Server实例的名称或网络地址。
        /// </summary>
        public string Server { set { SetValue("Server", value); } }
        /// <summary>
        ///     Encrypt（加密）：当值为真时，如果服务器安装了授权证书，SQL Server就会对所有在客户和服务器之间传输的数据使用SSL加密。被接受的值有true（真）、false（伪）、yes（是）和no（否）。
        /// </summary>
        public bool? Encrypt { set { SetValue("Encrypt", value); } }
        /// <summary>
        /// Enlist（登记）：表示连接池程序是否会自动登记创建线程的当前事务语境中的连接，其缺省值为真。
        /// </summary>
        public bool? Enlist { set { SetValue("Enlist", value); } }
        /// <summary>
        /// Database（数据库）／Initial Catalog（初始编目）：数据库的名称。
        /// </summary>
        public string Database { set { SetValue("Database", value); } }
        /// <summary>
        ///    Integrated Security（集成安全）／Trusted Connection（受信连接）：表示Windows认证是否被用来连接数据库。它可以被设置成真、伪或者是和真对等的sspi，其缺省值为伪。
        /// </summary>
        public bool? IntegratedSecurity { set { SetValue("Integrated Security", value); } }

        /// <summary>
        /// Max Pool Size（连接池的最大容量）：连接池允许的连接数的最大值，其缺省值为100。
        /// </summary>
        public int? MaxPoolSize { set { SetValue("Max Pool Size", value); } }
        /// <summary>
        /// Min Pool Size（连接池的最小容量）：连接池允许的连接数的最小值，其缺省值为0。
        /// </summary>
        public int? MinPoolSize { set { SetValue("Min Pool Size", value); } }
        /// <summary>
        ///     Network Library（网络库）／Net（网络）：用来建立到一个SQL Server实例的连接的网络库。支持的值包括： dbnmpntw(Named Pipes)、dbmsrpcn(Multiprotocol／RPC)、dbmsvinn(Banyan Vines)、dbmsspxn(IPX／SPX)和dbmssocn(TCP／IP)。协议的动态链接库必须被安装到适当的连接，其缺省值为TCP／IP。
        /// </summary>
        public string NetworkLibrary { set { SetValue("Network Library", value); } }

        /// <summary>
        /// Packet Size（数据包大小）：用来和数据库通信的网络数据包的大小。其缺省值为8192。
        /// </summary>
        public int? PacketSize { set { SetValue("Packet Size", value); } }
        /// <summary>
        ///  Password（密码）／Pwd：与帐户名相对应的密码。
        /// </summary>
        public string Password { set { SetValue("Password", value); } }

        /// <summary>
        ///     Persist Security Info（保持安全信息）：用来确定一旦连接建立了以后安全信息是否可用。如果值为真的话，说明像用户名和密码这样对安全性比较敏感的数据可用，而如果值为伪则不可用。重置连接字符串将重新配置包括密码在内的所有连接字符串的值。其缺省值为伪。
        /// </summary>
        public bool? PersistSecurityInfo { set { SetValue("Persist Security Info", value); } }

        /// <summary>
        ///     Pooling（池）：确定是否使用连接池。如果值为真的话，连接就要从适当的连接池中获得，或者，如果需要的话，连接将被创建，然后被加入合适的连接池中。其缺省值为真。
        /// </summary>
        public bool? Pooling { set { SetValue("Pooling", value); } }
        /// <summary>
        /// User ID（用户ID）：用来登陆数据库的帐户名。
        /// </summary>
        public string UserID { set { SetValue("User ID", value); } }
        /// <summary>
        /// Workstation ID（工作站ID）：连接到SQL Server的工作站的名称。其缺省值为本地计算机的名称。 
        /// </summary>
        public string WorkstationID { set { SetValue("Workstation ID", value); } }


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
