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
    }
}