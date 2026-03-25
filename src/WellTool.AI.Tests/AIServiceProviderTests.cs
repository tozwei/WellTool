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
    public class AIServiceProviderTests
    {
        [Fact]
        public void TestOpenAIServiceProvider()
        {
            // 创建OpenAI服务提供者
            var provider = new OpenAIServiceProvider();

            // 验证服务名称
            Assert.Equal("OpenAI", provider.GetServiceName());

            // 创建配置
            var config = new OpenAIConfig();
            config.SetApiKey("test-api-key");

            // 创建服务
            var service = provider.Create(config);

            // 验证服务不为null
            Assert.NotNull(service);
            Assert.IsType<OpenAIService>(service);
        }

        [Fact]
        public void TestOpenAIServiceProvider_CaseInsensitive()
        {
            // 创建OpenAI服务提供者
            var provider = new OpenAIServiceProvider();

            // 验证服务名称（大小写不敏感）
            Assert.Equal("OpenAI", provider.GetServiceName());
            Assert.Equal("openai", provider.GetServiceName().ToLower());
        }
    }
}