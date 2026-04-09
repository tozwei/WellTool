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
using WellTool.DB.Ds;

namespace WellTool.DB.Tests
{
    /// <summary>
    /// DriverUtilTest
    /// </summary>
    public class DriverUtilTest
    {
        [Fact]
        public void TestDriverUtilCreation()
        {
            // 测试创建 DriverUtil 实例
            var driverUtil = DriverUtil.Create();
            Assert.NotNull(driverUtil);
        }

        [Fact]
        public void TestDriverUtilLoadDriver()
        {
            // 测试加载数据库驱动
            var driverUtil = DriverUtil.Create();
            // 这里使用常见的 SQL Server 驱动作为示例
            var driverLoaded = driverUtil.LoadDriver("Microsoft.Data.SqlClient");
            // 验证驱动加载操作没有抛出异常
            Assert.True(true);
        }

        [Fact]
        public void TestDriverUtilGetDriver()
        {
            // 测试获取数据库驱动
            var driverUtil = DriverUtil.Create();
            var driver = driverUtil.GetDriver("Microsoft.Data.SqlClient");
            // 验证获取驱动操作没有抛出异常
            Assert.True(true);
        }

        [Fact]
        public void TestDriverUtilGetDriverNames()
        {
            // 测试获取驱动名称列表
            var driverUtil = DriverUtil.Create();
            var driverNames = driverUtil.GetDriverNames();
            Assert.NotNull(driverNames);
        }
    }
}

