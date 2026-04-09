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
/// 分页测试
/// </summary>
public class PageTest
{
    /// <summary>
    /// 测试分页
    /// </summary>
    [Fact]
    public void TestPage()
    {
        // 测试分页基本功能
        // 创建默认分页对象
        var page = new WellTool.DB.Page();
        Assert.NotNull(page);
        Assert.Equal(1, page.PageNumber);
        Assert.Equal(10, page.PageSize);

        // 测试起始位置和结束位置
        Assert.Equal(0, page.GetStartIndex());
        Assert.Equal(10, page.GetEndIndex());

        // 创建指定页码和每页大小的分页对象
        var customPage = new WellTool.DB.Page(3, 20);
        Assert.Equal(3, customPage.PageNumber);
        Assert.Equal(20, customPage.PageSize);
        Assert.Equal(40, customPage.GetStartIndex()); // (3-1)*20 = 40
        Assert.Equal(60, customPage.GetEndIndex()); // 3*20 = 60

        // 测试下一页
        var nextPage = customPage.NextPage();
        Assert.Equal(4, nextPage.PageNumber);
        Assert.Equal(20, nextPage.PageSize);

        // 测试上一页
        var previousPage = customPage.PreviousPage();
        Assert.Equal(2, previousPage.PageNumber);
        Assert.Equal(20, previousPage.PageSize);

        // 测试第一页的上一页
        var firstPage = new WellTool.DB.Page(1, 10);
        var previousPageFromFirst = firstPage.PreviousPage();
        Assert.Equal(1, previousPageFromFirst.PageNumber); // 保持在第一页
        Assert.Equal(10, previousPageFromFirst.PageSize);
    }
}