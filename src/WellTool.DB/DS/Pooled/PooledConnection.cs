// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Data;

namespace WellTool.DB.DS.Pooled;

/// <summary>
/// 连接池中的连接
/// </summary>
public class PooledConnection : IDbConnection
{
    /// <summary>
    /// 原始连接
    /// </summary>
    private readonly IDbConnection _connection;

    /// <summary>
    /// 连接池
    /// </summary>
    private readonly PooledDataSource _dataSource;

    /// <summary>
    /// 是否在使用中
    /// </summary>
    private bool _inUse;

    /// <summary>
    /// 最后使用时间
    /// </summary>
    public DateTime LastUsedTime { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connection">原始连接</param>
    /// <param name="dataSource">连接池</param>
    public PooledConnection(IDbConnection connection, PooledDataSource dataSource)
    {
        _connection = connection;
        _dataSource = dataSource;
        _inUse = false;
        LastUsedTime = DateTime.Now;
    }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString
    {
        get => _connection.ConnectionString;
        set => _connection.ConnectionString = value;
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public ConnectionState State => _connection.State;

    /// <summary>
    /// 超时时间
    /// </summary>
    public int ConnectionTimeout => _connection.ConnectionTimeout;

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string Database => _connection.Database;

    /// <summary>
    /// 是否在使用中
    /// </summary>
    public bool InUse
    {
        get => _inUse;
        set => _inUse = value;
    }

    /// <summary>
    /// 打开连接
    /// </summary>
    public void Open()
    {
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        _inUse = true;
        LastUsedTime = DateTime.Now;
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        // 不真正关闭连接，而是返回连接池
        _inUse = false;
        LastUsedTime = DateTime.Now;
        _dataSource.ReturnConnection(this);
    }

    /// <summary>
    /// 创建命令
    /// </summary>
    /// <returns>命令</returns>
    public IDbCommand CreateCommand()
    {
        LastUsedTime = DateTime.Now;
        return _connection.CreateCommand();
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <returns>事务</returns>
    public IDbTransaction BeginTransaction()
    {
        LastUsedTime = DateTime.Now;
        return _connection.BeginTransaction();
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="il">隔离级别</param>
    /// <returns>事务</returns>
    public IDbTransaction BeginTransaction(IsolationLevel il)
    {
        LastUsedTime = DateTime.Now;
        return _connection.BeginTransaction(il);
    }

    /// <summary>
    /// 更改数据库
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    public void ChangeDatabase(string databaseName)
    {
        LastUsedTime = DateTime.Now;
        _connection.ChangeDatabase(databaseName);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }

    /// <summary>
    /// 真正关闭连接
    /// </summary>
    public void ReallyClose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}
