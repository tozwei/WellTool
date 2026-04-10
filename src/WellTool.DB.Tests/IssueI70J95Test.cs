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

namespace WellTool.DB.Tests
{
    /// <summary>
    /// IssueI70J95Test
    /// </summary>
    public class IssueI70J95Test
    {
        [Fact]
        public void TestDataSourceWrapperCreation()
        {
            // 测试创建数据源包装器
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 验证包装器创建成功
            Assert.NotNull(wrapper);
            Assert.Equal("MockDriver", wrapper.GetDriver());
            Assert.Equal(dataSource, wrapper.GetRaw());
        }

        [Fact]
        public void TestDataSourceWrapperGetConnection()
        {
            // 测试获取数据库连接
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 测试获取连接
            using var connection = wrapper.GetConnection();
            Assert.NotNull(connection);
            Assert.Equal("Server=localhost;Database=test;", connection.ConnectionString);
        }

        [Fact]
        public void TestDataSourceWrapperWithOptions()
        {
            // 测试使用选项创建数据源包装器
            var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
            var wrapper = DataSourceWrapper.Wrap(dataSource, "MockDriver");
            
            // 验证包装器创建成功
            Assert.NotNull(wrapper);
            
            // 测试使用不同的连接字符串获取连接
            var customConnectionString = "Server=remote;Database=other;";
            using var connection = wrapper.GetConnection(customConnectionString);
            Assert.NotNull(connection);
            Assert.Equal(customConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void TestDataSourceWrapperDispose()
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
        public void TestDataSourceWrapperClose()
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
