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

public class DbUtilTests
{
    [Fact]
    public void TestQuote()
    {
        // 测试引号包裹
        var result = DbUtil.Quote("test");
        Assert.Equal("'test'", result);
    }
    
    [Fact]
    public void TestQuoteIfNotQuoted()
    {
        // 测试已经被引号包裹的情况
        var result1 = DbUtil.QuoteIfNotQuoted("'test'");
        Assert.Equal("'test'", result1);
        
        // 测试未被引号包裹的情况
        var result2 = DbUtil.QuoteIfNotQuoted("test");
        Assert.Equal("'test'", result2);
    }
    
    [Fact]
    public void TestUnquote()
    {
        // 测试去除引号
        var result = DbUtil.Unquote("'test'");
        Assert.Equal("test", result);
    }
    
    [Fact]
    public void TestIsQuoted()
    {
        // 测试是否被引号包裹
        var result1 = DbUtil.IsQuoted("'test'");
        Assert.True(result1);
        
        var result2 = DbUtil.IsQuoted("test");
        Assert.False(result2);
    }
}