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

using System;
using Xunit;
using WellTool.DB.Handler;
using System.Data;

namespace WellTool.DB.Tests;

/// <summary>
/// 测试 Bean 类
/// </summary>
public class TestBean
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}

/// <summary>
/// FindBean 测试
/// </summary>
public class FindBeanTest
{
    /// <summary>
    /// 测试 BeanHandler 的基本功能
    /// </summary>
    [Fact]
    public void TestFindBean()
    {
        // 准备测试数据
        string[] columnNames = { "Id", "Name", "Age", "Address" };
        object[] values = { 1, "Test Name", 25, "Test Address" };
        var reader = new MockDataReader(columnNames, values);

        // 创建 BeanHandler 并处理结果集
        var handler = new BeanHandler<TestBean>();
        var result = handler.Handle(reader);

        // 验证结果
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Name", result.Name);
        Assert.Equal(25, result.Age);
        Assert.Equal("Test Address", result.Address);
    }

    /// <summary>
    /// 测试空结果集
    /// </summary>
    [Fact]
    public void TestEmptyResultSet()
    {
        // 准备空的测试数据
        string[] columnNames = { "Id", "Name" };
        object[] values = { 1, "Test" };
        var reader = new MockDataReader(columnNames, values);

        // 先读取一次，模拟空结果集
        reader.Read();

        // 创建 BeanHandler 并处理结果集
        var handler = new BeanHandler<TestBean>();
        var result = handler.Handle(reader);

        // 验证结果
        Assert.Equal(default(TestBean), result);
    }

    /// <summary>
    /// 测试列名与属性名不匹配的情况
    /// </summary>
    [Fact]
    public void TestMismatchedColumnNames()
    {
        // 准备测试数据，列名与属性名不完全匹配
        string[] columnNames = { "ID", "NAME", "AGE", "ADDRESS" };
        object[] values = { 1, "Test Name", 25, "Test Address" };
        var reader = new MockDataReader(columnNames, values);

        // 创建 BeanHandler 并处理结果集
        var handler = new BeanHandler<TestBean>();
        var result = handler.Handle(reader);

        // 验证结果（应该忽略大小写差异）
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Name", result.Name);
        Assert.Equal(25, result.Age);
        Assert.Equal("Test Address", result.Address);
    }

    /// <summary>
    /// 测试包含 DBNull 值的情况
    /// </summary>
    [Fact]
    public void TestDBNullValues()
    {
        // 准备测试数据，包含 DBNull 值
        string[] columnNames = { "Id", "Name", "Age", "Address" };
        object[] values = { 1, "Test Name", DBNull.Value, "Test Address" };
        var reader = new MockDataReader(columnNames, values);

        // 创建 BeanHandler 并处理结果集
        var handler = new BeanHandler<TestBean>();
        var result = handler.Handle(reader);

        // 验证结果（DBNull 值应该被忽略）
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Name", result.Name);
        Assert.Equal(0, result.Age); // 应该保持默认值
        Assert.Equal("Test Address", result.Address);
    }
}