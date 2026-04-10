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
    /// ScriptUtilTest
    /// </summary>
    public class ScriptUtilTest
    {
        [Fact]
        public void TestCreateScriptEngine()
        {
            // 测试创建 JavaScript 脚本引擎
            var jsEngine = ScriptUtil.CreateEngine("javascript");
            Assert.NotNull(jsEngine);
        }

        [Fact]
        public void TestEvalScript()
        {
            // 测试执行简单的 JavaScript 脚本
            var result = ScriptUtil.Eval("javascript", "1 + 1");
            Assert.NotNull(result);
            Assert.Equal(2, Convert.ToInt32(result));
        }

        [Fact]
        public void TestEvalScriptWithBindings()
        {
            // 测试执行带有绑定变量的 JavaScript 脚本
            var bindings = new System.Collections.Generic.Dictionary<string, object>
            {
                { "a", 1 },
                { "b", 2 }
            };
            var result = ScriptUtil.Eval("javascript", "a + b", bindings);
            Assert.NotNull(result);
            Assert.Equal(3, Convert.ToInt32(result));
        }
    }
}

