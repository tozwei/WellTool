namespace WellTool.DB.Meta
{
    /// <summary>
    /// 索引信息
    /// </summary>
    public class IndexInfo
    {
        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 索引类型
        /// </summary>
        public string IndexType { get; set; }

        /// <summary>
        /// 是否唯一索引
        /// </summary>
        public bool NonUnique { get; set; }

        /// <summary>
        /// 所属表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列在索引中的顺序
        /// </summary>
        public int OrdinalPosition { get; set; }
    }
}
