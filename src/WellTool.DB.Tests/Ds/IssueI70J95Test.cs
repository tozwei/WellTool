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

using Xunit;
using WellTool.DB.DS;

namespace WellTool.DB.Tests.DS;

/// <summary>
/// Issue I70J95 测试
/// </summary>
public class IssueI70J95Test
{
    /// <summary>
    /// 测试 Issue I70J95
    /// </summary>
    [Fact]
    public void TestIssueI70J95()
    {
        // 测试 Issue I70J95 - 测试数据源包装器的创建和使用
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
        
        // 验证包装器创建成功
        Assert.NotNull(wrapper);
        Assert.Equal("MockDriver", wrapper.GetDriver());
        Assert.Equal(dataSource, wrapper.GetRaw());

        // 测试获取连接
        using var connection = wrapper.GetConnection();
        Assert.NotNull(connection);
        Assert.Equal("Server=localhost;Database=test;", connection.ConnectionString);

        // 测试使用指定连接字符串获取连接
        var customConnectionString = "Server=remote;Database=other;";
        using var customConnection = wrapper.GetConnection(customConnectionString);
        Assert.NotNull(customConnection);
        Assert.Equal(customConnectionString, customConnection.ConnectionString);
    }

    /// <summary>
    /// 测试数据源包装器的资源管理
    /// </summary>
    [Fact]
    public void TestDataSourceWrapperResourceManagement()
    {
        // 测试数据源包装器的资源管理
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
        
        // 测试释放资源
        wrapper.Dispose();
        
        // 验证测试通过（没有异常抛出）
        Assert.True(true);//
    }

    /// <summary>
    /// 测试不同类型的驱动
    /// </summary>
    [Fact]
    public void TestDataSourceWrapperWithDifferentDrivers()
    {
        // 测试不同类型的驱动
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "SqlClientDriver");
        var wrapper = DataSourceWrapper.Wrap(dataSource, "SqlClientDriver");
        
        // 验证驱动名称正确
        Assert.Equal("SqlClientDriver", wrapper.GetDriver());
        
        // 测试获取连接
        using var connection = wrapper.GetConnection();
        Assert.NotNull(connection);
    }
}
