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
using WellTool.AI;
using WellTool.AI.Model.DeepSeek;

namespace WellTool.AI.Tests
{
    /// <summary>
    /// DeepSeekServiceTest
    /// </summary>
    public class DeepSeekServiceTest
    {
        [Fact]
        public void TestDeepSeekServiceCreation()
        {
            // 测试 DeepSeek 服务的创建
            var config = new WellTool.AI.Model.DeepSeek.DeepSeekConfig();
            config.SetApiKey("test-api-key"); // 实际使用时需要替换为真实的 API 密钥
            var service = WellTool.AI.AIServiceFactory.GetAIService<WellTool.AI.Model.DeepSeek.DeepSeekService>(config, typeof(WellTool.AI.Model.DeepSeek.DeepSeekService));
            Assert.NotNull(service);
        }

        [Fact]
        public void TestDeepSeekServiceWithModel()
        {
            // 测试使用指定模型创建 DeepSeek 服务
            var config = new WellTool.AI.Model.DeepSeek.DeepSeekConfig();
            config.SetApiKey("test-api-key"); // 实际使用时需要替换为真实的 API 密钥
            config.SetModel("deepseek-chat");
            var service = WellTool.AI.AIServiceFactory.GetAIService<WellTool.AI.Model.DeepSeek.DeepSeekService>(config, typeof(WellTool.AI.Model.DeepSeek.DeepSeekService));
            Assert.NotNull(service);
        }

        [Fact]
        public void TestDeepSeekServiceWithCustomBaseUrl()
        {
            // 测试使用自定义基础 URL 创建 DeepSeek 服务
            var config = new WellTool.AI.Model.DeepSeek.DeepSeekConfig();
            config.SetApiKey("test-api-key"); // 实际使用时需要替换为真实的 API 密钥
            config.SetApiUrl("https://api.deepseek.com/v1");
            var service = WellTool.AI.AIServiceFactory.GetAIService<WellTool.AI.Model.DeepSeek.DeepSeekService>(config, typeof(WellTool.AI.Model.DeepSeek.DeepSeekService));
            Assert.NotNull(service);
        }
    }
}

