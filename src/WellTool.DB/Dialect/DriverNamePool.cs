namespace WellTool.DB.Dialect;

/// <summary>
/// 常用数据库驱动池
/// </summary>
public static class DriverNamePool
{
    /// <summary>
    /// MySQL 驱动
    /// </summary>
    public const string DriverMySQL = "MySql.Data.MySqlClient";

    /// <summary>
    /// MySQL 8.x 驱动
    /// </summary>
    public const string DriverMySQLV6 = "MySql.Data.MySqlClient";

    /// <summary>
    /// MariaDB 驱动
    /// </summary>
    public const string DriverMariaDB = "MySql.Data.MySqlClient";

    /// <summary>
    /// Oracle 驱动
    /// </summary>
    public const string DriverOracle = "Oracle.ManagedDataAccess";

    /// <summary>
    /// PostgreSQL 驱动
    /// </summary>
    public const string DriverPostgreSQL = "Npgsql";

    /// <summary>
    /// SQLite3 驱动
    /// </summary>
    public const string DriverSQLite3 = "Microsoft.Data.Sqlite";

    /// <summary>
    /// SQL Server 驱动
    /// </summary>
    public const string DriverSqlServer = "Microsoft.Data.SqlClient";

    /// <summary>
    /// H2 数据库驱动
    /// </summary>
    public const string DriverH2 = "NHibernate.Driver.H2Driver";

    /// <summary>
    /// 达梦数据库驱动
    /// </summary>
    public const string DriverDm7 = "Dm";

    /// <summary>
    /// 华为高斯数据库驱动
    /// </summary>
    public const string DriverGauss = "Gauss";

    /// <summary>
    /// SAP HANA 驱动
    /// </summary>
    public const string DriverHana = "Sap.Data.Hana";

    /// <summary>
    /// ClickHouse 驱动
    /// </summary>
    public const string DriverClickHouse = "ClickHouse.Client";

    /// <summary>
    /// 瀚高数据库驱动
    /// </summary>
    public const string DriverHighgo = "HighGo";

    /// <summary>
    /// DB2 驱动
    /// </summary>
    public const string DriverDB2 = "IBM.Data.DB2";

    /// <summary>
    /// Sybase 驱动
    /// </summary>
    public const string DriverSybase = "Sybase.Data.AseClient";

    /// <summary>
    /// Snowflake 驱动
    /// </summary>
    public const string DriverSnowflake = "Snowflake.Data";
}
