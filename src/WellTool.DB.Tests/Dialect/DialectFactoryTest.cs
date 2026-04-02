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

using System.Data.SqlClient;
using WellTool.DB.Dialect;
using WellTool.DB.Dialect.Impl;
using Xunit;

namespace WellTool.DB.Tests.Dialect;

/// <summary>
/// DialectFactory 类测试
/// </summary>
public class DialectFactoryTest
{
    /// <summary>
    /// 测试根据提供程序名称获取方言
    /// </summary>
    [Fact]
    public void TestGetDialectByProviderName()
    {
        var sqlServerDialect = DialectFactory.GetDialect("System.Data.SqlClient");
        Assert.IsType<SqlServerDialect>(sqlServerDialect);
        Assert.Equal("SQL Server", sqlServerDialect.Name);

        var mySqlDialect = DialectFactory.GetDialect("MySql.Data.MySqlClient");
        Assert.IsType<MySqlDialect>(mySqlDialect);
        Assert.Equal("MySQL", mySqlDialect.Name);

        var postgreSqlDialect = DialectFactory.GetDialect("Npgsql");
        Assert.IsType<PostgreSqlDialect>(postgreSqlDialect);
        Assert.Equal("PostgreSQL", postgreSqlDialect.Name);

        var sqliteDialect = DialectFactory.GetDialect("System.Data.SQLite");
        Assert.IsType<Sqlite3Dialect>(sqliteDialect);
        Assert.Equal("SQLite3", sqliteDialect.Name);

        var h2Dialect = DialectFactory.GetDialect("org.h2.Driver");
        Assert.IsType<H2Dialect>(h2Dialect);
        Assert.Equal("H2", h2Dialect.Name);

        var oracleDialect = DialectFactory.GetDialect("oracle.jdbc.OracleDriver");
        Assert.IsType<OracleDialect>(oracleDialect);
        Assert.Equal("Oracle", oracleDialect.Name);

        var hanaDialect = DialectFactory.GetDialect("com.sap.db.jdbc.Driver");
        Assert.IsType<HanaDialect>(hanaDialect);
        Assert.Equal("HANA", hanaDialect.Name);

        var dmDialect = DialectFactory.GetDialect("dm.jdbc.driver.DmDriver");
        Assert.IsType<DmDialect>(dmDialect);
        Assert.Equal("DM", dmDialect.Name);

        // 测试默认方言
        var defaultDialect = DialectFactory.GetDialect("unknown");
        Assert.IsType<AnsiSqlDialect>(defaultDialect);
        Assert.Equal("ANSI", defaultDialect.Name);
    }

    /// <summary>
    /// 测试根据连接获取方言
    /// </summary>
    [Fact]
    public void TestGetDialectByConnection()
    {
        using var connection = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
        var dialect = DialectFactory.GetDialect(connection);
        Assert.IsType<SqlServerDialect>(dialect);
        Assert.Equal("SQL Server", dialect.Name);
    }

    /// <summary>
    /// 测试注册方言
    /// </summary>
    [Fact]
    public void TestRegisterDialect()
    {
        var customDialect = new AnsiSqlDialect();
        DialectFactory.RegisterDialect("custom", customDialect);
        var dialect = DialectFactory.GetDialect("custom");
        Assert.Same(customDialect, dialect);
    }
}
