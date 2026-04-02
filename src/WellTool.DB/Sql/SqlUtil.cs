using System.Data;
using System.Text;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// 移除SQL外层的ORDER BY子句
        /// 用于分页查询时去除不必要的排序
        /// </summary>
        /// <param name="sql">原始SQL</param>
        /// <returns>移除ORDER BY后的SQL</returns>
        public static string RemoveOuterOrderBy(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return sql;
            }

            var pattern = @"\s+ORDER\s+BY\s+[\w\s,\.\-\(\)]+(\s+(ASC|DESC))?(\s+(NULLS\s+(FIRST|LAST)))?";
            return Regex.Replace(sql, pattern, "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 检查SQL片段是否在IN子句中
        /// </summary>
        /// <param name="sql">SQL片段</param>
        /// <returns>是否在IN子句中</returns>
        public static bool IsInClause(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return false;
            }

            var trimmed = sql.TrimEnd();
            var upper = trimmed.ToUpperInvariant();

            if (!upper.EndsWith("IN ("))
            {
                return false;
            }

            int paramCount = 0;
            int parenCount = 1;
            for (int i = trimmed.Length - 1; i >= 0; i--)
            {
                char c = trimmed[i];
                if (c == '(')
                {
                    parenCount--;
                    if (parenCount == 0)
                    {
                        return paramCount > 0;
                    }
                }
                else if (c == ')')
                {
                    parenCount++;
                }
                else if (c == '?' && parenCount == 1)
                {
                    paramCount++;
                }
            }

            return false;
        }

        /// <summary>
        /// 构建LIKE值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="likeType">LIKE类型</param>
        /// <param name="isNot">是否取反</param>
        /// <returns>LIKE值</returns>
        public static string BuildLikeValue(string value, Condition.LikeType likeType, bool isNot = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return likeType switch
            {
                Condition.LikeType.StartWith => value + "%",
                Condition.LikeType.EndWith => "%" + value,
                Condition.LikeType.Contains => "%" + value + "%",
                _ => value
            };
        }
    }
}