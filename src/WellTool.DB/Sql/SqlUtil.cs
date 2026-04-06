namespace WellTool.DB.Sql
{
    /// <summary>
    /// SQL 工具类
    /// </summary>
    public static class SqlUtil
    {
        /// <summary>
        /// 构建条件
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>条件字符串</returns>
        public static string BuildConditions(object entity)
        {
            // 默认实现，子类可重写
            return string.Empty;
        }

        /// <summary>
        /// 判断是否为 IN 子句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <returns>是否为 IN 子句</returns>
        public static bool IsInClause(string sql)
        {
            return !string.IsNullOrEmpty(sql) && sql.Contains(" IN ");
        }

        /// <summary>
        /// 移除外层 ORDER BY 子句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <returns>移除 ORDER BY 后的 SQL 语句</returns>
        public static string RemoveOuterOrderBy(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return sql;
            }

            int orderByIndex = sql.LastIndexOf(" ORDER BY ", System.StringComparison.OrdinalIgnoreCase);
            if (orderByIndex > 0)
            {
                return sql.Substring(0, orderByIndex);
            }

            return sql;
        }
    }
}