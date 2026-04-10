using System.Data;
using System.Data.Common;

namespace WellTool.Core.DB.Sql;

/// <summary>
/// <see cref="IDbCommand"/> 包装类，用于添加拦截方法功能<br>
/// 拦截方法包括：
/// <pre>
/// 1. 提供参数注入
/// 2. 提供SQL打印日志拦截
/// </pre>
/// </summary>
public class StatementWrapper : IDbCommand
{
    private readonly IDbCommand _rawStatement;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="rawStatement"><see cref="IDbCommand"/></param>
    public StatementWrapper(IDbCommand rawStatement)
    {
        _rawStatement = rawStatement;
    }

    /// <summary>
    /// 获取或设置命令文本
    /// </summary>
    public string CommandText
    {
        get => _rawStatement.CommandText;
        set => _rawStatement.CommandText = value;
    }

    /// <summary>
    /// 获取或设置命令超时时间
    /// </summary>
    public int CommandTimeout
    {
        get => _rawStatement.CommandTimeout;
        set => _rawStatement.CommandTimeout = value;
    }

    /// <summary>
    /// 获取或设置命令类型
    /// </summary>
    public CommandType CommandType
    {
        get => _rawStatement.CommandType;
        set => _rawStatement.CommandType = value;
    }

    /// <summary>
    /// 获取或设置连接
    /// </summary>
    public IDbConnection Connection
    {
        get => _rawStatement.Connection;
        set => _rawStatement.Connection = value;
    }

    /// <summary>
    /// 获取参数集合
    /// </summary>
    public IDataParameterCollection Parameters => _rawStatement.Parameters;

    /// <summary>
    /// 获取或设置事务
    /// </summary>
    public IDbTransaction Transaction
    {
        get => _rawStatement.Transaction;
        set => _rawStatement.Transaction = value;
    }

    /// <summary>
    /// 获取或设置更新行数影响
    /// </summary>
    public UpdateRowSource UpdatedRowSource
    {
        get => _rawStatement.UpdatedRowSource;
        set => _rawStatement.UpdatedRowSource = value;
    }

    /// <summary>
    /// 取消命令
    /// </summary>
    public void Cancel()
    {
        _rawStatement.Cancel();
    }

    /// <summary>
    /// 创建参数
    /// </summary>
    /// <returns>参数</returns>
    public IDbDataParameter CreateParameter()
    {
        return _rawStatement.CreateParameter();
    }

    /// <summary>
    /// 执行查询
    /// </summary>
    /// <returns>结果集</returns>
    public IDataReader ExecuteReader()
    {
        return _rawStatement.ExecuteReader();
    }

    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="behavior">行为</param>
    /// <returns>结果集</returns>
    public IDataReader ExecuteReader(CommandBehavior behavior)
    {
        return _rawStatement.ExecuteReader(behavior);
    }

    /// <summary>
    /// 执行非查询
    /// </summary>
    /// <returns>影响行数</returns>
    public int ExecuteNonQuery()
    {
        return _rawStatement.ExecuteNonQuery();
    }

    /// <summary>
    /// 执行标量
    /// </summary>
    /// <returns>标量值</returns>
    public object ExecuteScalar()
    {
        return _rawStatement.ExecuteScalar();
    }

    /// <summary>
    /// 准备命令
    /// </summary>
    public void Prepare()
    {
        _rawStatement.Prepare();
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _rawStatement.Dispose();
    }
}
