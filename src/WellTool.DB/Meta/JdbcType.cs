using System.Data;
using System;

namespace WellTool.DB.Meta
{
    /// <summary>
    /// JDBC类型
    /// </summary>
    public enum JdbcType
    {
        // 整数类型
        [DbType(DbType.Int32)]
        INTEGER = 4,
        
        [DbType(DbType.Int64)]
        BIGINT = -5,
        
        [DbType(DbType.Int16)]
        SMALLINT = 5,
        
        [DbType(DbType.Byte)]
        TINYINT = -6,
        
        // 浮点数类型
        [DbType(DbType.Double)]
        DOUBLE = 8,
        
        [DbType(DbType.Single)]
        FLOAT = 6,
        
        [DbType(DbType.Decimal)]
        DECIMAL = 3,
        
        // 字符串类型
        [DbType(DbType.String)]
        VARCHAR = 12,
        
        [DbType(DbType.StringFixedLength)]
        CHAR = 1,
        
        [DbType(DbType.String)]
        TEXT = -1,
        
        // 日期时间类型
        [DbType(DbType.Date)]
        DATE = 91,
        
        [DbType(DbType.Time)]
        TIME = 92,
        
        [DbType(DbType.DateTime)]
        TIMESTAMP = 93,
        
        // 布尔类型
        [DbType(DbType.Boolean)]
        BOOLEAN = 16,
        
        // 二进制类型
        [DbType(DbType.Binary)]
        BINARY = -2,
        
        [DbType(DbType.Binary)]
        VARBINARY = -3,
        
        [DbType(DbType.Binary)]
        BLOB = 2004,
        
        [DbType(DbType.String)]
        CLOB = 2005
    }

    /// <summary>
    /// DbType 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DbTypeAttribute : Attribute
    {
        /// <summary>
        /// DbType
        /// </summary>
        public DbType DbType { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbType">DbType</param>
        public DbTypeAttribute(DbType dbType)
        {
            DbType = dbType;
        }
    }
}