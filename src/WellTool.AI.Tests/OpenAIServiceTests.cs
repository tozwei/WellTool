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

using WellTool.AI.Model.OpenAI;

namespace WellTool.AI.Tests
{
    public class OpenAIServiceTests
    {
        [Fact]
        public void TestOpenAIServiceCreation()
        {
            // 创建配置
            var config = new OpenAIConfig();
            config.SetApiKey("test-api-key");

            // 创建服务
            var service = new OpenAIService(config);

            // 验证服务不为null
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOpenAIConfig()
        {
            // 创建配置
            var config = new OpenAIConfig();

            // 验证默认值
            Assert.Equal("https://api.openai.com/v1", config.GetApiUrl());
            Assert.Equal("gpt-3.5-turbo", config.GetModel());

            // 测试带API密钥的构造函数
            var configWithApiKey = new OpenAIConfig("test-api-key");
            Assert.Equal("test-api-key", configWithApiKey.GetApiKey());
        }
    }
}
