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
using Xunit;
using WellTool.DB;

namespace WellTool.DB.Tests;

/// <summary>
/// 数据源测试
/// </summary>
public class DsTest
{
    /// <summary>
    /// 测试简单数据源的基本功能
    /// </summary>
    [Fact]
    public void TestDataSource()
    {
        // 创建模拟的数据源
        var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
        var driverClass = typeof(MockDbConnection).AssemblyQualifiedName;
        
        var dataSource = new TestDataSource(connectionString, driverClass);
        
        // 测试获取连接
        using var connection = dataSource.GetConnection();
        
        // 验证连接不为空
        Assert.NotNull(connection);
        Assert.IsType<MockDbConnection>(connection);
        Assert.Equal(connectionString, connection.ConnectionString);
    }

    /// <summary>
    /// 测试使用连接字符串获取连接
    /// </summary>
    [Fact]
    public void TestGetConnectionWithConnectionString()
    {
        // 创建模拟的数据源
        var defaultConnectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
        var driverClass = typeof(MockDbConnection).AssemblyQualifiedName;
        
        var dataSource = new TestDataSource(defaultConnectionString, driverClass);
        
        // 使用不同的连接字符串获取连接
        var customConnectionString = "Server=localhost;Database=custom;User Id=sa;Password=password;";
        using var connection = dataSource.GetConnection(customConnectionString);
        
        // 验证连接不为空且使用了指定的连接字符串
        Assert.NotNull(connection);
        Assert.IsType<MockDbConnection>(connection);
        Assert.Equal(customConnectionString, connection.ConnectionString);
    }

    /// <summary>
    /// 测试数据源接口的实现
    /// </summary>
    [Fact]
    public void TestIDbDataSourceInterface()
    {
        // 创建模拟的数据源
        var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
        var driverClass = typeof(MockDbConnection).AssemblyQualifiedName;
        
        IDbDataSource dataSource = new TestDataSource(connectionString, driverClass);
        
        // 测试接口方法
        using var connection1 = dataSource.GetConnection();
        using var connection2 = dataSource.GetConnection(connectionString);
        
        Assert.NotNull(connection1);
        Assert.NotNull(connection2);
    }
}