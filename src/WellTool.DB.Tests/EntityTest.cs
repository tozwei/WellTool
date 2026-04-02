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
/// Entity 类测试
/// </summary>
public class EntityTest
{
    /// <summary>
    /// 测试无参构造函数
    /// </summary>
    [Fact]
    public void TestConstructor()
    {
        var entity = new Entity();
        Assert.NotNull(entity);
        Assert.Null(entity.TableName);
        Assert.Equal(0, entity.Count);
    }

    /// <summary>
    /// 测试带表名的构造函数
    /// </summary>
    [Fact]
    public void TestConstructorWithTableName()
    {
        var entity = new Entity("user");
        Assert.NotNull(entity);
        Assert.Equal("user", entity.TableName);
        Assert.Equal(0, entity.Count);
    }

    /// <summary>
    /// 测试带表名和容量的构造函数
    /// </summary>
    [Fact]
    public void TestConstructorWithTableNameAndCapacity()
    {
        var entity = new Entity("user", 10);
        Assert.NotNull(entity);
        Assert.Equal("user", entity.TableName);
        Assert.Equal(0, entity.Count);
    }

    /// <summary>
    /// 测试 Set 方法
    /// </summary>
    [Fact]
    public void TestSet()
    {
        var entity = new Entity("user");
        var result = entity.Set("id", 1).Set("name", "test");
        Assert.Same(entity, result);
        Assert.Equal(2, entity.Count);
        Assert.Equal(1, entity["id"]);
        Assert.Equal("test", entity["name"]);
    }

    /// <summary>
    /// 测试 Get 方法
    /// </summary>
    [Fact]
    public void TestGet()
    {
        var entity = new Entity("user");
        entity.Set("id", 1).Set("name", "test");
        Assert.Equal(1, entity.Get<int>("id"));
        Assert.Equal("test", entity.Get<string>("name"));
        Assert.Equal(default(int), entity.Get<int>("non_existent"));
    }

    /// <summary>
    /// 测试 Remove 方法
    /// </summary>
    [Fact]
    public void TestRemove()
    {
        var entity = new Entity("user");
        entity.Set("id", 1).Set("name", "test");
        var result = entity.Remove("id");
        Assert.Same(entity, result);
        Assert.Equal(1, entity.Count);
        Assert.False(entity.ContainsKey("id"));
    }

    /// <summary>
    /// 测试 Clear 方法
    /// </summary>
    [Fact]
    public void TestClear()
    {
        var entity = new Entity("user");
        entity.Set("id", 1).Set("name", "test");
        var result = entity.Clear();
        Assert.Same(entity, result);
        Assert.Equal(0, entity.Count);
    }
}
