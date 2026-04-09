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
using WellTool.DB.Dialect;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// DriverUtilTest
    /// </summary>
    public class DriverUtilTest
    {
        [Fact]
        public void TestDriverUtilIdentifyDriver()
        {
            // 测试识别数据库驱动
            var dialectName = DriverUtil.IdentifyDriver("Server=localhost;Database=test;User Id=sa;Password=password;");
            Assert.NotNull(dialectName);
        }

        [Fact]
        public void TestDriverUtilIdentifyFromConnection()
        {
            // 测试从连接识别数据库驱动
            using var connection = new System.Data.SqlClient.SqlConnection();
            var dialectName = DriverUtil.IdentifyFromConnection(connection);
            Assert.NotNull(dialectName);
        }
    }
}

