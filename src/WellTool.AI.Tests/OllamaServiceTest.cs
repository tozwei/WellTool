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
using WellTool.AI.Ollama;

namespace WellTool.AI.Tests
{
    /// <summary>
    /// OllamaServiceTest
    /// </summary>
    public class OllamaServiceTest
    {
        [Fact]
        public void TestOllamaServiceCreation()
        {
            // 测试 Ollama 服务的创建
            var service = OllamaService.Create();
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOllamaServiceWithCustomBaseUrl()
        {
            // 测试使用自定义基础 URL 创建 Ollama 服务
            var baseUrl = "http://localhost:11434/api";
            var service = OllamaService.Create(baseUrl);
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOllamaServiceWithModel()
        {
            // 测试使用指定模型创建 Ollama 服务
            var model = "llama3";
            var service = OllamaService.Create(model);
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOllamaServiceWithCustomBaseUrlAndModel()
        {
            // 测试使用自定义基础 URL 和指定模型创建 Ollama 服务
            var baseUrl = "http://localhost:11434/api";
            var model = "llama3";
            var service = OllamaService.Create(baseUrl, model);
            Assert.NotNull(service);
        }
    }
}

