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
/// 连接池数据源工厂
/// </summary>
public class PooledDSFactory : AbstractDSFactory
{
    private PooledDataSource _dataSource;

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <returns>连接</returns>
    public override IDbConnection GetConnection()
    {
        return _dataSource?.GetConnection() ?? throw new System.Exception("DataSource not initialized");
    }

    /// <summary>
    /// 获取连接
    /// </summary>
    /// <param name="connStr">连接字符串</param>
    /// <returns>连接</returns>
    public override IDbConnection GetConnection(string connStr)
    {
        var config = new DbConfig { Url = connStr };
        _dataSource = new PooledDataSource(config);
        return _dataSource.GetConnection();
    }

    /// <summary>
    /// 获取数据适配器
    /// </summary>
    /// <returns>数据适配器</returns>
    public override IDbDataAdapter GetAdapter()
    {
        return null;
    }

    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <param name="url">连接字符串</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>数据源</returns>
    public override IDbConnection CreateDataSource(string url, string username, string password)
    {
        var config = new DbConfig
        {
            Url = url,
            Username = username,
            Password = password
        };
        _dataSource = new PooledDataSource(config);
        return _dataSource;
    }

    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <param name="config">数据库配置</param>
    /// <returns>数据源</returns>
    public IDbConnection CreateDataSource(DbConfig config)
    {
        _dataSource = new PooledDataSource(config);
        return _dataSource;
    }

    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <param name="setting">数据库配置设置</param>
    /// <returns>数据源</returns>
    public IDbConnection CreateDataSource(DbSetting setting)
    {
        var config = new DbConfig
        {
            DriverClass = setting.Get("driver"),
            Url = setting.Get("url"),
            Username = setting.Get("username"),
            Password = setting.Get("password"),
            InitialSize = setting.GetInt("initialSize", 10),
            MaxActive = setting.GetInt("maxActive", 50),
            MaxIdle = setting.GetInt("maxIdle", 10),
            MinIdle = setting.GetInt("minIdle", 5),
            MaxWait = setting.GetInt("maxWait", 60000),
            ConnectionTimeout = setting.GetInt("connectionTimeout", 30000),
            ValidationQuery = setting.Get("validationQuery", "SELECT 1"),
            ValidationTimeout = setting.GetInt("validationTimeout", 5000),
            TestOnBorrow = setting.GetBool("testOnBorrow", true),
            TestOnReturn = setting.GetBool("testOnReturn", false),
            TestWhileIdle = setting.GetBool("testWhileIdle", true),
            TimeBetweenEvictionRunsMillis = setting.GetInt("timeBetweenEvictionRunsMillis", 60000),
            MaxLifetime = setting.GetInt("maxLifetime", 1800000)
        };
        _dataSource = new PooledDataSource(config);
        return _dataSource;
    }
}
