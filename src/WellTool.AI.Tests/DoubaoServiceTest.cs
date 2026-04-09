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
using WellTool.AI.Doubao;

namespace WellTool.AI.Tests
{
    /// <summary>
    /// DoubaoServiceTest
    /// </summary>
    public class DoubaoServiceTest
    {
        [Fact]
        public void TestDoubaoServiceCreation()
        {
            // 测试 Doubao 服务的创建
            var apiKey = "test-api-key"; // 实际使用时需要替换为真实的 API 密钥
            var service = DoubaoService.Create(apiKey);
            Assert.NotNull(service);
        }

        [Fact]
        public void TestDoubaoServiceWithModel()
        {
            // 测试使用指定模型创建 Doubao 服务
            var apiKey = "test-api-key"; // 实际使用时需要替换为真实的 API 密钥
            var model = "ep-20250409140935-662ww";
            var service = DoubaoService.Create(apiKey, model);
            Assert.NotNull(service);
        }

        [Fact]
        public void TestDoubaoServiceWithCustomBaseUrl()
        {
            // 测试使用自定义基础 URL 创建 Doubao 服务
            var apiKey = "test-api-key"; // 实际使用时需要替换为真实的 API 密钥
            var baseUrl = "https://ark.cn-beijing.volces.com/api/v3";
            var service = DoubaoService.Create(apiKey, baseUrl);
            Assert.NotNull(service);
        }
    }
}

