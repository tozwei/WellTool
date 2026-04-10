using System.Data;

namespace WellTool.DB.DS;

/// <summary>
/// <see cref="IDbDataSource"/> 数据源实现包装，通过包装，提供基本功能外的额外功能和参数持有，包括：
/// 1. 提供驱动名的持有，用于确定数据库方言
/// </summary>
public class DataSourceWrapper : IDbDataSource
{
    private readonly IDbDataSource _ds;
    private readonly string _driver;

    /// <summary>
    /// 包装指定的DataSource
    /// </summary>
    /// <param name="ds">原始的DataSource</param>
    /// <param name="driver">数据库驱动类名</param>
    /// <returns>DataSourceWrapper</returns>
    public static DataSourceWrapper Wrap(IDbDataSource ds, string driver)
    {
        return new DataSourceWrapper(ds, driver);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="ds">原始的DataSource</param>
    /// <param name="driver">数据库驱动类名</param>
    public DataSourceWrapper(IDbDataSource ds, string driver)
    {
        _ds = ds;
        _driver = driver;
    }

    /// <summary>
    /// 获取驱动名
    /// </summary>
    /// <returns>驱动名</returns>
    public string GetDriver()
    {
        return _driver;
    }

    /// <summary>
    /// 获取原始的数据源
    /// </summary>
    /// <returns>原始数据源</returns>
    public IDbDataSource GetRaw()
    {
        return _ds;
    }

    /// <summary>
    /// 获取数据库连接
    /// </summary>
    /// <returns>数据库连接</returns>
    public IDbConnection GetConnection()
    {
        return _ds.GetConnection();
    }

    /// <summary>
    /// 获取数据库连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <returns>数据库连接</returns>
    public IDbConnection GetConnection(string connectionString)
    {
        return _ds.GetConnection(connectionString);
    }

    /// <summary>
    /// 关闭数据源
    /// </summary>
    public void Close()
    {
        if (_ds is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}
