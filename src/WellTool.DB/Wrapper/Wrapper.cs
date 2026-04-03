using System;

namespace WellTool.DB
{
    /// <summary>
    /// SQL语句包装器，用于包装表名、字段名等
    /// </summary>
    public class Wrapper
    {
        /// <summary>
        /// 包装字符，例如 "`"、"\"" 等
        /// </summary>
        private readonly char? _wrapperChar;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="wrapperChar">包装字符</param>
        public Wrapper(char? wrapperChar)
        {
            _wrapperChar = wrapperChar;
        }

        /// <summary>
        /// 包装字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>包装后的字段名</returns>
        public string Wrap(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || _wrapperChar == null)
            {
                return fieldName;
            }
            return $"{_wrapperChar}{fieldName}{_wrapperChar}";
        }

        /// <summary>
        /// 包装表名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>包装后的表名</returns>
        public string WrapTableName(string tableName)
        {
            if (string.IsNullOrEmpty(tableName) || _wrapperChar == null)
            {
                return tableName;
            }
            return $"{_wrapperChar}{tableName}{_wrapperChar}";
        }

        /// <summary>
        /// 获取包装字符
        /// </summary>
        public char? WrapperChar => _wrapperChar;

        /// <summary>
        /// 默认包装器（无包装）
        /// </summary>
        public static Wrapper None => new Wrapper(null);

        /// <summary>
        /// MySQL包装器（反引号）
        /// </summary>
        public static Wrapper MySQL => new Wrapper('`');

        /// <summary>
        /// SQL Server包装器（方括号）
        /// </summary>
        public static Wrapper SqlServer => new Wrapper('[');

        /// <summary>
        /// Oracle包装器（双引号）
        /// </summary>
        public static Wrapper Oracle => new Wrapper('"');
    }
}
