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

using WellTool.AI.Core;
using WellTool.AI.Model.OpenAI;

namespace WellTool.AI.Tests
{
    public class AIConfigRegistryTests
    {
        [Fact]
        public void TestGetConfigClass_WithValidModelName()
        {
            // 测试获取已知的配置类
            var configType = AIConfigRegistry.GetConfigClass("OpenAI");
            
            // 验证返回的类型不为null
            Assert.NotNull(configType);
            
            // 验证返回的类型是OpenAIConfig
            Assert.Equal(typeof(OpenAIConfig), configType);
        }

        [Fact]
        public void TestGetConfigClass_WithInvalidModelName()
        {
            // 测试获取不存在的配置类
            var configType = AIConfigRegistry.GetConfigClass("NonExistentModel");
            
            // 验证返回的类型为null
            Assert.Null(configType);
        }

        [Fact]
        public void TestGetConfigClass_CaseInsensitive()
        {
            // 测试不同大小写的模型名称
            var configType1 = AIConfigRegistry.GetConfigClass("openai");
            var configType2 = AIConfigRegistry.GetConfigClass("OPENAI");
            var configType3 = AIConfigRegistry.GetConfigClass("OpenAi");
            
            // 验证所有情况都返回相同的类型
            Assert.NotNull(configType1);
            Assert.Equal(configType1, configType2);
            Assert.Equal(configType1, configType3);
            Assert.Equal(typeof(OpenAIConfig), configType1);
        }
    }
}