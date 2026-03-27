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

namespace WellTool.Script.Tests;

public class ScriptUtilTests
{
    [Fact]
    public void TestExecuteJavaScript()
    {
        // 测试执行简单的 JavaScript 代码
        // JavaScript 中的数字都是 double 类型
        var result = WellTool.Script.ScriptUtil.Instance.ExecuteJavaScript("1 + 1");
        Assert.Equal(2.0, result);
    }

    [Fact]
    public void TestExecuteJavaScriptWithType()
    {
        // 测试执行 JavaScript 代码并返回指定类型的结果
        // JavaScript 中的数字都是 double 类型
        var result = WellTool.Script.ScriptUtil.Instance.ExecuteJavaScript<double>("1 + 1");
        Assert.Equal(2.0, result);
    }

    [Fact]
    public void TestCreateJavaScriptEngine()
    {
        // 测试创建 JavaScript 引擎
        var engine = WellTool.Script.ScriptUtil.Instance.CreateJavaScriptEngine();
        Assert.NotNull(engine);
    }
}
