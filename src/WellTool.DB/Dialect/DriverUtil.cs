using System.Data.Common;

namespace WellTool.DB.Dialect;

/// <summary>
/// 驱动相关工具类，包括自动获取驱动类名
/// </summary>
public static class DriverUtil
{
    /// <summary>
    /// 通过连接字符串或数据源名称等信息识别数据库类型
    /// </summary>
    /// <param name="connectionString">连接字符串或包含数据库标识的字符串</param>
    /// <returns>方言名称</returns>
    public static DialectName IdentifyDriver(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return DialectName.Ansi;

        var lower = connectionString.ToLower();

        // 检测MySQL
        if (lower.Contains("mysql") || lower.Contains("mariadb"))
            return DialectName.MySQL;

        // 检测PostgreSQL
        if (lower.Contains("postgresql") || lower.Contains("postgres"))
            return DialectName.PostgreSQL;

        // 检测SQL Server
        if (lower.Contains("microsoft.sqlserver") || lower.Contains("sql server"))
            return DialectName.SqlServer;

        // 检测Oracle
        if (lower.Contains("oracle"))
            return DialectName.Oracle;

        // 检测SQLite
        if (lower.Contains("sqlite") || lower.Contains("data source="))
            return DialectName.Sqlite3;

        // 检测H2
        if (lower.Contains("h2"))
            return DialectName.H2;

        // 检测达梦
        if (lower.Contains("dm"))
            return DialectName.Dm;

        // 检测SAP HANA
        if (lower.Contains("hana") || lower.Contains("sap"))
            return DialectName.Hana;

        return DialectName.Ansi;
    }

    /// <summary>
    /// 通过数据库连接识别数据库类型
    /// </summary>
    /// <param name="connection">数据库连接</param>
    /// <returns>方言名称</returns>
    public static DialectName IdentifyFromConnection(DbConnection connection)
    {
        try
        {
            var dbType = connection.GetType().Name.ToLower();

            if (dbType.Contains("mysql"))
                return DialectName.MySQL;
            if (dbType.Contains("npgsql") || dbType.Contains("postgres"))
                return DialectName.PostgreSQL;
            if (dbType.Contains("sqlclient") || dbType.Contains("sqlconnection"))
                return DialectName.SqlServer;
            if (dbType.Contains("oracle"))
                return DialectName.Oracle;
            if (dbType.Contains("sqlite"))
                return DialectName.Sqlite3;
        }
        catch
        {
            // 忽略异常
        }

        return DialectName.Ansi;
    }
}
