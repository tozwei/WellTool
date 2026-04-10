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

using System.Linq;
using Xunit;
using WellTool.DB.Meta;

namespace WellTool.DB.Tests.Meta;

/// <summary>
/// MetaUtil 测试
/// </summary>
public class MetaUtilTest
{
    /// <summary>
    /// 测试元数据工具
    /// </summary>
    [Fact]
    public void TestMetaUtil()
    {
        // 测试元数据工具的基本功能
        // 创建测试用的Table对象
        var table = new Table("test_table");
        table.Comment = "Test table for unit testing";
        table.TableType = TableType.TABLE;

        // 添加测试列
        var column1 = new Column
        {
            Name = "id",
            Type = "int",
            Size = 4,
            IsNullable = false,
            IsPrimaryKey = true
        };
        table.AddColumn(column1);

        var column2 = new Column
        {
            Name = "name",
            Type = "varchar",
            Size = 100,
            IsNullable = true,
            IsPrimaryKey = false
        };
        table.AddColumn(column2);

        // 验证表信息
        Assert.Equal("test_table", table.Name);
        Assert.Equal("Test table for unit testing", table.Comment);
        Assert.Equal(TableType.TABLE, table.TableType);
        Assert.Equal(2, table.Columns.Count);

        // 验证列信息
        var idColumn = table.GetColumn("id");
        Assert.NotNull(idColumn);
        Assert.Equal("int", idColumn.Type);
        Assert.True(idColumn.IsPrimaryKey);

        var nameColumn = table.GetColumn("name");
        Assert.NotNull(nameColumn);
        Assert.Equal("varchar", nameColumn.Type);
        Assert.False(nameColumn.IsPrimaryKey);

        // 验证主键
        var pk = table.GetPrimaryKey();
        Assert.NotNull(pk);
        Assert.Equal("id", pk.Name);
    }

    /// <summary>
    /// 测试Table类的构造函数
    /// </summary>
    [Fact]
    public void TestTableConstructor()
    {
        // 测试默认构造函数
        var table1 = new Table();
        Assert.NotNull(table1.Columns);
        Assert.Empty(table1.Columns);

        // 测试带表名的构造函数
        var table2 = new Table("users");
        Assert.Equal("users", table2.Name);
        Assert.NotNull(table2.Columns);

        // 测试完整构造函数
        var columns = new List<Column>
        {
            new Column { Name = "id", Type = "int" },
            new Column { Name = "name", Type = "varchar" }
        };
        var table3 = new Table("products", TableType.TABLE, "Product table", columns);
        Assert.Equal("products", table3.Name);
        Assert.Equal(TableType.TABLE, table3.TableType);
        Assert.Equal("Product table", table3.Comment);
        Assert.Equal(2, table3.Columns.Count);
    }

    /// <summary>
    /// 测试Table的AddColumn方法
    /// </summary>
    [Fact]
    public void TestTableAddColumn()
    {
        var table = new Table("test");
        
        // 测试链式调用
        var result = table
            .AddColumn(new Column { Name = "col1", Type = "int" })
            .AddColumn(new Column { Name = "col2", Type = "varchar" })
            .AddColumn(new Column { Name = "col3", Type = "datetime" });

        // 验证返回的是同一个对象（链式调用）
        Assert.Equal(table, result);
        
        // 验证列已添加
        Assert.Equal(3, table.Columns.Count);
        Assert.NotNull(table.GetColumn("col1"));
        Assert.NotNull(table.GetColumn("col2"));
        Assert.NotNull(table.GetColumn("col3"));
    }

    /// <summary>
    /// 测试Column类的属性
    /// </summary>
    [Fact]
    public void TestColumnProperties()
    {
        var column = new Column
        {
            Name = "price",
            Type = "decimal",
            Size = 18,
            DecimalDigits = 2,
            IsNullable = false,
            IsPrimaryKey = false,
            DefaultValue = "0.00",
            Comment = "Product price"
        };

        Assert.Equal("price", column.Name);
        Assert.Equal("decimal", column.Type);
        Assert.Equal(18, column.Size);
        Assert.Equal(2, column.DecimalDigits);
        Assert.False(column.IsNullable);
        Assert.False(column.IsPrimaryKey);
        Assert.Equal("0.00", column.DefaultValue);
        Assert.Equal("Product price", column.Comment);
    }

    /// <summary>
    /// 测试Table的ToString方法
    /// </summary>
    [Fact]
    public void TestTableToString()
    {
        var table = new Table("users", TableType.TABLE, "User table", new List<Column>());
        var str = table.ToString();

        Assert.Contains("users", str);
        Assert.Contains("TABLE", str);
        Assert.Contains("User table", str);
        Assert.Contains("0", str); // 列数为0
    }
}
