namespace WellTool.DB.Meta
{
    /// <summary>
    /// 表类型
    /// </summary>
    public enum TableType
    {
        /// <summary>表</summary>
        TABLE,
        
        /// <summary>视图</summary>
        VIEW,
        
        /// <summary>系统表</summary>
        SYSTEM_TABLE,
        
        /// <summary>全局临时表</summary>
        GLOBAL_TEMPORARY,
        
        /// <summary>本地临时表</summary>
        LOCAL_TEMPORARY,
        
        /// <summary>同义词</summary>
        SYNONYM,
        
        /// <summary>其他</summary>
        OTHER
    }
}