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

namespace WellTool.DB.Tests;

/// <summary>
/// 包装器测试
/// </summary>
public class WrapperTest
{
    /// <summary>
    /// 测试包装器
    /// </summary>
    [Fact]
    public void TestWrapper()
    {
        // 测试包装器基本功能
        // 这里测试数据库连接包装器的基本操作
        using var ds = new System.Data.SqlClient.SqlDataSourceBuilder()
            .ConnectionString("Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;")
            .Build();

        // 验证数据源不为空
        Assert.NotNull(ds);

        // 测试连接打开和关闭
        using var connection = ds.CreateConnection();
        Assert.NotNull(connection);

        // 测试连接状态
        Assert.Equal(System.Data.ConnectionState.Closed, connection.State);

        // 打开连接
        connection.Open();
        Assert.Equal(System.Data.ConnectionState.Open, connection.State);

        // 关闭连接
        connection.Close();
        Assert.Equal(System.Data.ConnectionState.Closed, connection.State);
    }
}