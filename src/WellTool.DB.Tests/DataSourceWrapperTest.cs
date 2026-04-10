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
using System.Data;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// DataSourceWrapperTest
    /// </summary>
    public class DataSourceWrapperTest
    {
        [Fact]
        public void TestDataSourceCreation()
        {
            // 测试创建数据源包装器
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 验证包装器不为空
            Assert.NotNull(wrapper);
            
            // 验证驱动名正确
            Assert.Equal("MockDriver", wrapper.GetDriver());
            
            // 验证原始数据源正确
            Assert.Equal(dataSource, wrapper.GetRaw());
        }

        [Fact]
        public void TestDataSourceWithType()
        {
            // 测试指定类型创建数据源包装器
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "SqlClientDriver");
            var wrapper = new DataSourceWrapper(dataSource, "SqlClientDriver");
            
            // 验证包装器不为空
            Assert.NotNull(wrapper);
            
            // 验证驱动名正确
            Assert.Equal("SqlClientDriver", wrapper.GetDriver());
            
            // 验证可以获取连接
            using var connection = wrapper.GetConnection();
            Assert.NotNull(connection);
        }

        [Fact]
        public void TestDataSourceGetConnection()
        {
            // 测试从包装器获取连接
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 测试获取连接
            using var connection = wrapper.GetConnection();
            Assert.NotNull(connection);
            Assert.IsType<MockDbConnection>(connection);
        }

        [Fact]
        public void TestDataSourceGetConnectionWithConnectionString()
        {
            // 测试使用指定连接字符串获取连接
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 测试使用指定连接字符串获取连接
            var customConnectionString = "Server=remote;Database=other;";
            using var connection = wrapper.GetConnection(customConnectionString);
            Assert.NotNull(connection);
            Assert.Equal(customConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void TestDataSourceDispose()
        {
            // 测试数据源包装器的释放
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 测试释放资源
            wrapper.Dispose();
            
            // 验证测试通过（没有异常抛出）
            Assert.True(true);//
        }

        [Fact]
        public void TestDataSourceClose()
        {
            // 测试数据源包装器的关闭
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 测试关闭
            wrapper.Close();
            
            // 验证测试通过（没有异常抛出）
            Assert.True(true);//
        }
    }
}
