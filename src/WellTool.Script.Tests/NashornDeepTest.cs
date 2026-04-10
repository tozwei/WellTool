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
using WellTool.Script;

namespace WellTool.Script.Tests
{
    /// <summary>
    /// NashornDeepTest
    /// </summary>
    public class NashornDeepTest
    {
        [Fact]
        public void TestJavaScriptEngine()
        {
            // 测试 JavaScript 引擎的基本功能
            var engine = (WellTool.Script.FullSupportScriptEngine)ScriptUtil.CreateEngine("javascript");
            Assert.NotNull(engine);

            // 测试执行复杂的 JavaScript 表达式
            var result = engine.Execute("function add(a, b) { return a + b; } add(2, 3);");
            Assert.NotNull(result);
            Assert.Equal(5, result);
        }

        [Fact]
        public void TestJavaScriptObjects()
        {
            // 测试 JavaScript 对象的操作
            var engine = (WellTool.Script.FullSupportScriptEngine)ScriptUtil.CreateEngine("javascript");
            Assert.NotNull(engine);

            // 测试创建和操作 JavaScript 对象
            engine.Execute("var obj = { name: 'WellTool', version: 1.0 };");
            var name = engine.Execute("obj.name");
            var version = engine.Execute("obj.version");

            Assert.NotNull(name);
            Assert.Equal("WellTool", name);
            Assert.NotNull(version);
            Assert.Equal(1.0, version);
        }

        [Fact]
        public void TestJavaScriptArrays()
        {
            // 测试 JavaScript 数组的操作
            var engine = (WellTool.Script.FullSupportScriptEngine)ScriptUtil.CreateEngine("javascript");
            Assert.NotNull(engine);

            // 测试创建和操作 JavaScript 数组
            engine.Execute("var arr = [1, 2, 3, 4, 5];");
            var length = engine.Execute("arr.length");
            var firstElement = engine.Execute("arr[0]");
            var lastElement = engine.Execute("arr[arr.length - 1]");

            Assert.NotNull(length);
            Assert.Equal(5, length);
            Assert.NotNull(firstElement);
            Assert.Equal(1, firstElement);
            Assert.NotNull(lastElement);
            Assert.Equal(5, lastElement);
        }
    }
}

