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

using System.Collections.Generic;
using Xunit;

namespace WellTool.DB.Tests;

/// <summary>
/// 分页结果测试
/// </summary>
public class PageResultTest
{
    /// <summary>
    /// 测试分页结果
    /// </summary>
    [Fact]
    public void TestPageResult()
    {
        // 测试分页结果基本功能
        // 创建测试数据
        var testData = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" };

        // 创建分页结果对象
        var pageResult = new WellTool.DB.PageResult<string>(testData, 25, 2, 5);
        Assert.NotNull(pageResult);
        Assert.Equal(testData, pageResult.List);
        Assert.Equal(25, pageResult.Total);
        Assert.Equal(2, pageResult.PageNumber);
        Assert.Equal(5, pageResult.PageSize);

        // 测试总页数计算
        Assert.Equal(5, pageResult.TotalPages); // (25 + 5 - 1) / 5 = 5

        // 测试是否有下一页和上一页
        Assert.True(pageResult.HasNext()); // 第2页，总页数5，有下一页
        Assert.True(pageResult.HasPrevious()); // 第2页，有上一页

        // 测试第一页
        var firstPageResult = new WellTool.DB.PageResult<string>(testData, 25, 1, 5);
        Assert.False(firstPageResult.HasPrevious()); // 第一页，没有上一页
        Assert.True(firstPageResult.HasNext()); // 第一页，有下一页

        // 测试最后一页
        var lastPageResult = new WellTool.DB.PageResult<string>(testData, 25, 5, 5);
        Assert.True(lastPageResult.HasPrevious()); // 最后一页，有上一页
        Assert.False(lastPageResult.HasNext()); // 最后一页，没有下一页

        // 测试空数据
        var emptyPageResult = new WellTool.DB.PageResult<string>();
        Assert.NotNull(emptyPageResult.List);
        Assert.Empty(emptyPageResult.List);
        Assert.Equal(0, emptyPageResult.Total);
        Assert.Equal(1, emptyPageResult.PageNumber);
        Assert.Equal(10, emptyPageResult.PageSize);
        Assert.Equal(0, emptyPageResult.TotalPages);
        Assert.False(emptyPageResult.HasNext());
        Assert.False(emptyPageResult.HasPrevious());
    }
}