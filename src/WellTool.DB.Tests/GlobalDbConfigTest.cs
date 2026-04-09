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

namespace WellTool.DB.Tests;

/// <summary>
/// 全局数据库配置测试
/// </summary>
public class GlobalDbConfigTest
{
    /// <summary>
    /// 测试全局数据库配置
    /// </summary>
    [Fact]
    public void TestGlobalDbConfig()
    {
        // 测试全局数据库配置
        // 保存原始配置值
        var originalConnectionTimeout = WellTool.DB.GlobalDbConfig.ConnectionTimeout;
        var originalCommandTimeout = WellTool.DB.GlobalDbConfig.CommandTimeout;
        var originalTransactionIsolationLevel = WellTool.DB.GlobalDbConfig.DefaultTransactionIsolationLevel;
        var originalShowSql = WellTool.DB.GlobalDbConfig.ShowSql;
        var originalDbType = WellTool.DB.GlobalDbConfig.DbType;
        var originalCaseInsensitive = WellTool.DB.GlobalDbConfig.CaseInsensitive;

        try
        {
            // 测试默认值
            Assert.Equal(30000, WellTool.DB.GlobalDbConfig.ConnectionTimeout);
            Assert.Equal(30, WellTool.DB.GlobalDbConfig.CommandTimeout);
            Assert.Equal(System.Data.IsolationLevel.ReadCommitted, WellTool.DB.GlobalDbConfig.DefaultTransactionIsolationLevel);
            Assert.True(WellTool.DB.GlobalDbConfig.ShowSql);
            Assert.Equal("mysql", WellTool.DB.GlobalDbConfig.DbType);
            Assert.True(WellTool.DB.GlobalDbConfig.CaseInsensitive);

            // 测试修改配置
            WellTool.DB.GlobalDbConfig.ConnectionTimeout = 60000;
            WellTool.DB.GlobalDbConfig.CommandTimeout = 60;
            WellTool.DB.GlobalDbConfig.DefaultTransactionIsolationLevel = System.Data.IsolationLevel.Serializable;
            WellTool.DB.GlobalDbConfig.ShowSql = false;
            WellTool.DB.GlobalDbConfig.DbType = "sqlserver";
            WellTool.DB.GlobalDbConfig.CaseInsensitive = false;

            // 验证修改后的配置
            Assert.Equal(60000, WellTool.DB.GlobalDbConfig.ConnectionTimeout);
            Assert.Equal(60, WellTool.DB.GlobalDbConfig.CommandTimeout);
            Assert.Equal(System.Data.IsolationLevel.Serializable, WellTool.DB.GlobalDbConfig.DefaultTransactionIsolationLevel);
            Assert.False(WellTool.DB.GlobalDbConfig.ShowSql);
            Assert.Equal("sqlserver", WellTool.DB.GlobalDbConfig.DbType);
            Assert.False(WellTool.DB.GlobalDbConfig.CaseInsensitive);
        }
        finally
        {
            // 恢复原始配置
            WellTool.DB.GlobalDbConfig.ConnectionTimeout = originalConnectionTimeout;
            WellTool.DB.GlobalDbConfig.CommandTimeout = originalCommandTimeout;
            WellTool.DB.GlobalDbConfig.DefaultTransactionIsolationLevel = originalTransactionIsolationLevel;
            WellTool.DB.GlobalDbConfig.ShowSql = originalShowSql;
            WellTool.DB.GlobalDbConfig.DbType = originalDbType;
            WellTool.DB.GlobalDbConfig.CaseInsensitive = originalCaseInsensitive;
        }
    }
}