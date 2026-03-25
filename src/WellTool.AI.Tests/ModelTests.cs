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
using WellTool.AI.Model.DeepSeek;
using WellTool.AI.Model.Doubao;
using WellTool.AI.Model.Gemini;
using WellTool.AI.Model.Grok;
using WellTool.AI.Model.Hutool;
using WellTool.AI.Model.Ollama;

namespace WellTool.AI.Tests
{
    public class ModelTests
    {
        [Fact]
        public void TestDeepSeekConfig()
        {
            // 创建DeepSeek配置
            var config = new DeepSeekConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestDeepSeekService()
        {
            // 创建DeepSeek配置
            var config = new DeepSeekConfig();
            config.SetApiKey("test-api-key");

            // 创建DeepSeek服务
            var service = new DeepSeekServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }

        [Fact]
        public void TestDoubaoConfig()
        {
            // 创建Doubao配置
            var config = new DoubaoConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestDoubaoService()
        {
            // 创建Doubao配置
            var config = new DoubaoConfig();
            config.SetApiKey("test-api-key");

            // 创建Doubao服务
            var service = new DoubaoServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }

        [Fact]
        public void TestGeminiConfig()
        {
            // 创建Gemini配置
            var config = new GeminiConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestGeminiService()
        {
            // 创建Gemini配置
            var config = new GeminiConfig();
            config.SetApiKey("test-api-key");

            // 创建Gemini服务
            var service = new GeminiServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }

        [Fact]
        public void TestGrokConfig()
        {
            // 创建Grok配置
            var config = new GrokConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestGrokService()
        {
            // 创建Grok配置
            var config = new GrokConfig();
            config.SetApiKey("test-api-key");

            // 创建Grok服务
            var service = new GrokServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }

        [Fact]
        public void TestHutoolConfig()
        {
            // 创建Hutool配置
            var config = new HutoolConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestHutoolService()
        {
            // 创建Hutool配置
            var config = new HutoolConfig();
            config.SetApiKey("test-api-key");

            // 创建Hutool服务
            var service = new HutoolServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }

        [Fact]
        public void TestOllamaConfig()
        {
            // 创建Ollama配置
            var config = new OllamaConfig();
            config.SetApiKey("test-api-key");

            // 验证配置
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestOllamaService()
        {
            // 创建Ollama配置
            var config = new OllamaConfig();
            config.SetApiKey("test-api-key");

            // 创建Ollama服务
            var service = new OllamaServiceImpl(config);

            // 验证服务
            Assert.NotNull(service);
        }
    }
}