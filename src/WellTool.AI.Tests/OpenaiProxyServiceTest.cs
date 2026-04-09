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
using WellTool.AI.OpenAI;

namespace WellTool.AI.Tests
{
    /// <summary>
    /// OpenaiProxyServiceTest
    /// </summary>
    public class OpenaiProxyServiceTest
    {
        [Fact]
        public void TestOpenAIProxyServiceCreation()
        {
            // 测试 OpenAI 代理服务的创建
            var apiKey = "test-api-key"; // 实际使用时需要替换为真实的 API 密钥
            var proxyUrl = "https://api.openai.com/v1"; // 实际使用时需要替换为真实的代理 URL
            var service = OpenAIProxyService.Create(apiKey, proxyUrl);
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOpenAIProxyServiceWithHeaders()
        {
            // 测试带有自定义头部的 OpenAI 代理服务创建
            var apiKey = "test-api-key"; // 实际使用时需要替换为真实的 API 密钥
            var proxyUrl = "https://api.openai.com/v1"; // 实际使用时需要替换为真实的代理 URL
            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "X-Custom-Header", "custom-value" }
            };
            var service = OpenAIProxyService.Create(apiKey, proxyUrl, headers);
            Assert.NotNull(service);
        }
    }
}

