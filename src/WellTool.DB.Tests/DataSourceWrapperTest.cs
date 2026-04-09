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
    /// DataSourceWrapperTest
    /// </summary>
    public class DataSourceWrapperTest
    {
        [Fact]
        public void TestDataSourceCreation()
        {
            // 测试创建数据源
            var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
            var connection = GlobalDSFactory.CreateDataSource(connectionString, "sa", "password");
            Assert.NotNull(connection);
        }

        [Fact]
        public void TestDataSourceWithType()
        {
            // 测试指定类型创建数据源
            var connectionString = "Server=localhost;Database=test;User Id=sa;Password=password;";
            var connection = GlobalDSFactory.CreateDataSource("simple", connectionString, "sa", "password");
            Assert.NotNull(connection);
        }
    }
}

