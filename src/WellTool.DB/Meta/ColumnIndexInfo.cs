namespace WellTool.DB.Meta
{
    /// <summary>
    /// 列索引信息
    /// </summary>
    public class ColumnIndexInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 索引名称
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// 非唯一索引
        /// </summary>
        public bool NonUnique { get; set; }

        /// <summary>
        /// 索引序号
        /// </summary>
        public int IndexOrdinal { get; set; }
    }
}
