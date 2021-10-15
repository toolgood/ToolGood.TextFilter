using System.Collections.Generic;
using System.Data;

namespace ToolGood.ReadyGo3
{
    /// <summary>
    /// SQL 参数集合
    /// </summary>
    public class SqlParameterCollection : List<SqlParameter>
    {
        /// <summary>
        /// 添加SQL参数
        /// </summary>
        /// <param name="parameterName">不带前缀</param>
        /// <param name="value">值</param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="scale"></param>
        public void Add(string parameterName, object value, DbType? dbType = null, int? size = null, ParameterDirection? direction = null, byte? scale = null)
        {
            SqlParameter parameter = new SqlParameter {
                ParameterName = parameterName,
                Value = value,
                DbType = dbType,
                ParameterDirection = direction ?? ParameterDirection.Input,
                Size = size,
                Scale = scale
            };
            this.Add(parameter);
        }
    }


    /// <summary>
    /// SQL 参数
    /// </summary>
    public class SqlParameter
    {

        /// <summary>
        /// 参数名字
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterDirection ParameterDirection { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// 获取或设置所解析的 Value 的小数位数。
        /// </summary>
        public byte? Scale { get; set; }

    }
}
