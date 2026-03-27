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

namespace WellTool.Poi.Tests;

public class ExcelUtilTests
{
    [Fact]
    public void TestGetReader()
    {
        // 测试 GetReader 方法，由于未实现，应该抛出 NotImplementedException
        var exception = Assert.Throws<NotImplementedException>(() => WellTool.Poi.ExcelUtil.Instance.GetReader("test.xlsx"));
        Assert.Contains("ExcelUtil 需要添加 EPPlus 包引用并实现", exception.Message);
    }

    [Fact]
    public void TestGetWriter()
    {
        // 测试 GetWriter 方法，由于未实现，应该抛出 NotImplementedException
        var exception = Assert.Throws<NotImplementedException>(() => WellTool.Poi.ExcelUtil.Instance.GetWriter("test.xlsx"));
        Assert.Contains("ExcelUtil 需要添加 EPPlus 包引用并实现", exception.Message);
    }
}