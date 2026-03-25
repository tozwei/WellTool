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
    public class BaseConfigTests
    {
        [Fact]
        public void TestBaseConfig()
        {
            // 创建配置
            var config = new OpenAIConfig();

            // 设置属性
            config.SetApiKey("test-api-key");
            config.SetApiUrl("https://api.openai.com/v1");
            config.SetModel("gpt-3.5-turbo");
            config.SetTimeout(30000);
            config.SetReadTimeout(60000);

            // 验证属性
            Assert.Equal("test-api-key", config.GetApiKey());
            Assert.Equal("https://api.openai.com/v1", config.GetApiUrl());
            Assert.Equal("gpt-3.5-turbo", config.GetModel());
            Assert.Equal(30000, config.GetTimeout());
            Assert.Equal(60000, config.GetReadTimeout());
        }

        [Fact]
        public void TestAdditionalConfig()
        {
            // 创建配置
            var config = new OpenAIConfig();

            // 添加额外配置
            config.PutAdditionalConfigByKey("temperature", 0.7);
            config.PutAdditionalConfigByKey("max_tokens", 1000);

            // 验证额外配置
            Assert.Equal(0.7, config.GetAdditionalConfigByKey("temperature"));
            Assert.Equal(1000, config.GetAdditionalConfigByKey("max_tokens"));

            // 验证配置映射
            var additionalConfig = config.GetAdditionalConfigMap();
            Assert.Contains("temperature", additionalConfig);
            Assert.Contains("max_tokens", additionalConfig);
        }
    }
}
