namespace WellTool.DB.Dialect;

/// <summary>
/// 方言名枚举
/// 列出了支持的数据库方言
/// </summary>
public enum DialectName
{
    /// <summary>
    /// ANSI标准SQL
    /// </summary>
    Ansi,
    
    /// <summary>
    /// MySQL
    /// </summary>
    MySQL,
    
    /// <summary>
    /// Oracle
    /// </summary>
    Oracle,
    
    /// <summary>
    /// PostgreSQL
    /// </summary>
    PostgreSQL,
    
    /// <summary>
    /// SQLite3
    /// </summary>
    Sqlite3,
    
    /// <summary>
    /// H2数据库
    /// </summary>
    H2,
    
    /// <summary>
    /// SQL Server
    /// </summary>
    SqlServer,
    
    /// <summary>
    /// SQL Server 2012+
    /// </summary>
    SqlServer2012,
    
    /// <summary>
    /// Phoenix
    /// </summary>
    Phoenix,
    
    /// <summary>
    /// 达梦数据库
    /// </summary>
    Dm,
    
    /// <summary>
    /// SAP HANA
    /// </summary>
    Hana,

    /// <summary>
    /// OpenGauss
    /// </summary>
    OpenGauss,

    /// <summary>
    /// OceanBase
    /// </summary>
    OceanBase
}
