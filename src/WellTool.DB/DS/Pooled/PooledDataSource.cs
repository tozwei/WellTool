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

using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace WellTool.DB.DS.Pooled;

/// <summary>
/// 连接池数据源
/// </summary>
public class PooledDataSource : IDbConnection
{
    /// <summary>
    /// 连接池配置
    /// </summary>
    private readonly DbConfig _config;

    /// <summary>
    /// 连接列表
    /// </summary>
    private readonly List<PooledConnection> _connections;

    /// <summary>
    /// 锁对象
    /// </summary>
    private readonly object _lock = new();

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="config">连接池配置</param>
    public PooledDataSource(DbConfig config)
    {
        _config = config;
        _connections = new List<PooledConnection>();
        ConnectionString = config.Url;
        InitializeConnections();
    }

    /// <summary>
    /// 初始化连接
    /// </summary>
    private void InitializeConnections()
    {
        for (int i = 0; i < _config.InitialSize; i++)
        {
            var connection = CreateConnection();
            _connections.Add(connection);
        }
    }

    /// <summary>
    /// 创建连接
    /// </summary>
    /// <returns>连接</returns>
    private PooledConnection CreateConnection()
    {
        var connection = (IDbConnection)Activator.CreateInstance(Type.GetType(_config.DriverClass));
        connection.ConnectionString = _config.Url;
        connection.Open();
        return new PooledConnection(connection, this);
    }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns>连接</returns>
    public IDbConnection GetConnection()
    {
        lock (_lock)
        {
            // 查找空闲连接
            var freeConnection = _connections.FirstOrDefault(c => !c.InUse);
            if (freeConnection != null)
            {
                // 验证连接是否有效
                if (IsValidConnection(freeConnection))
                {
                    freeConnection.InUse = true;
                    return freeConnection;
                }
                else
                {
                    // 移除无效连接
                    _connections.Remove(freeConnection);
                    freeConnection.ReallyClose();
                }
            }

            // 如果没有空闲连接且未达到最大连接数，创建新连接
            if (_connections.Count < _config.MaxActive)
            {
                var connection = CreateConnection();
                connection.InUse = true;
                _connections.Add(connection);
                return connection;
            }

            // 等待空闲连接
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(_config.MaxWait))
            {
                Thread.Sleep(100);
                freeConnection = _connections.FirstOrDefault(c => !c.InUse);
                if (freeConnection != null)
                {
                    if (IsValidConnection(freeConnection))
                    {
                        freeConnection.InUse = true;
                        return freeConnection;
                    }
                    else
                    {
                        _connections.Remove(freeConnection);
                        freeConnection.ReallyClose();
                    }
                }
            }

            throw new TimeoutException("Timeout waiting for database connection");
        }
    }

    /// <summary>
    /// 验证连接是否有效
    /// </summary>
    /// <param name="connection">连接</param>
    /// <returns>是否有效</returns>
    private bool IsValidConnection(PooledConnection connection)
    {
        try
        {
            if (connection.State != ConnectionState.Open)
            {
                return false;
            }

            using var command = connection.CreateCommand();
            command.CommandText = _config.ValidationQuery;
            command.ExecuteScalar();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 返回连接
    /// </summary>
    /// <param name="connection">连接</param>
    public void ReturnConnection(PooledConnection connection)
    {
        lock (_lock)
        {
            connection.InUse = false;
            connection.LastUsedTime = DateTime.Now;
        }
    }

    /// <summary>
    /// 清理连接池
    /// </summary>
    public void Cleanup()
    {
        lock (_lock)
        {
            var now = DateTime.Now;
            var toRemove = new List<PooledConnection>();

            foreach (var connection in _connections)
            {
                if (!connection.InUse)
                {
                    // 清理超过最大生存时间的连接
                    if (now - connection.LastUsedTime > TimeSpan.FromMilliseconds(_config.MaxLifetime))
                    {
                        toRemove.Add(connection);
                    }
                }
            }

            foreach (var connection in toRemove)
            {
                _connections.Remove(connection);
                connection.ReallyClose();
            }

            // 确保连接数不低于最小空闲连接数
            while (_connections.Count < _config.MinIdle)
            {
                var connection = CreateConnection();
                _connections.Add(connection);
            }
        }
    }

    /// <summary>
    /// 打开连接
    /// </summary>
    public void Open()
    {
        // 连接池不需要打开，获取连接时会自动打开
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        // 清理所有连接
        lock (_lock)
        {
            foreach (var connection in _connections)
            {
                connection.ReallyClose();
            }
            _connections.Clear();
        }
    }

    /// <summary>
    /// 创建命令
    /// </summary>
    /// <returns>命令</returns>
    public IDbCommand CreateCommand()
    {
        var connection = GetConnection();
        return connection.CreateCommand();
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <returns>事务</returns>
    public IDbTransaction BeginTransaction()
    {
        var connection = GetConnection();
        return connection.BeginTransaction();
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <param name="il">隔离级别</param>
    /// <returns>事务</returns>
    public IDbTransaction BeginTransaction(IsolationLevel il)
    {
        var connection = GetConnection();
        return connection.BeginTransaction(il);
    }

    /// <summary>
    /// 更改数据库
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    public void ChangeDatabase(string databaseName)
    {
        throw new NotSupportedException("ChangeDatabase is not supported in pooled data source");
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public ConnectionState State => ConnectionState.Open;

    /// <summary>
    /// 数据库版本
    /// </summary>
    public string ServerVersion => string.Empty;

    /// <summary>
    /// 超时时间
    /// </summary>
    public int ConnectionTimeout => _config.ConnectionTimeout;

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string Database => string.Empty;

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Close();
    }
}
