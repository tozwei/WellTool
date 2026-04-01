using System.Data;
using System.Text;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL工具类
    /// </summary>
    public static class SqlUtil
    {
        /// <summary>
        /// 转义SQL中的特殊字符
        /// </summary>
        /// <param name="str">需要转义的字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.Replace("'", "''");
        }

        /// <summary>
        /// 构建IN子句的参数占位符
        /// </summary>
        /// <param name="count">参数个数</param>
        /// <returns>占位符字符串，如 "?, ?, ?"</returns>
        public static string BuildInPlaceholders(int count)
        {
            if (count <= 0)
            {
                return "";
            }

            StringBuilder placeholders = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    placeholders.Append(", ");
                }
                placeholders.Append("?");
            }
            return placeholders.ToString();
        }

        /// <summary>
        /// 构建分页SQL
        /// </summary>
        /// <param name="sql">原始SQL</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>分页SQL</returns>
        public static string BuildPageSql(string sql, int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;
            return sql + string.Format(" LIMIT {0} OFFSET {1}", pageSize, offset);
        }

        /// <summary>
        /// 构建COUNT SQL
        /// </summary>
        /// <param name="sql">原始SQL</param>
        /// <returns>COUNT SQL</returns>
        public static string BuildCountSql(string sql)
        {
            return string.Format("SELECT COUNT(*) FROM ({0}) t", sql);
        }

        /// <summary>
        /// 检查SQL语句是否以指定的关键字开头
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="keywords">关键字列表</param>
        /// <returns>是否以指定关键字开头</returns>
        public static bool StartsWithKeywords(string sql, params string[] keywords)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return false;
            }

            string trimmedSql = sql.Trim().ToUpper();
            foreach (string keyword in keywords)
            {
                if (trimmedSql.StartsWith(keyword.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查SQL语句是否为SELECT语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否为SELECT语句</returns>
        public static bool IsSelect(string sql)
        {
            return StartsWithKeywords(sql, "SELECT");
        }

        /// <summary>
        /// 检查SQL语句是否为INSERT语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否为INSERT语句</returns>
        public static bool IsInsert(string sql)
        {
            return StartsWithKeywords(sql, "INSERT");
        }

        /// <summary>
        /// 检查SQL语句是否为UPDATE语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否为UPDATE语句</returns>
        public static bool IsUpdate(string sql)
        {
            return StartsWithKeywords(sql, "UPDATE");
        }

        /// <summary>
        /// 检查SQL语句是否为DELETE语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>是否为DELETE语句</returns>
        public static bool IsDelete(string sql)
        {
            return StartsWithKeywords(sql, "DELETE");
        }
    }
}