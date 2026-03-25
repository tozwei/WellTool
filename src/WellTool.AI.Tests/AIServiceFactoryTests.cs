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

using WellTool.AI;
using WellTool.AI.Core;
using WellTool.AI.Model.OpenAI;

namespace WellTool.AI.Tests
{
    public class AIServiceFactoryTests
    {
        [Fact]
        public void TestGetAIService()
        {
            // 创建OpenAI配置
            var config = new OpenAIConfig();
            config.SetApiKey("test-api-key");

            // 获取AI服务
            var service = AIServiceFactory.GetAIService(config);

            // 验证服务不为null
            Assert.NotNull(service);
        }

        [Fact]
        public void TestGetAIServiceWithGenericType()
        {
            // 创建OpenAI配置
            var config = new OpenAIConfig();
            config.SetApiKey("test-api-key");

            // 获取AI服务（泛型版本）
            var service = AIServiceFactory.GetAIService<AIService>(config, typeof(AIService));

            // 验证服务不为null
            Assert.NotNull(service);
        }

        [Fact]
        public void TestGetAIServiceWithUnsupportedModel()
        {
            // 创建一个自定义配置类，模拟不支持的模型
            var config = new TestConfig();
            config.SetApiKey("test-api-key");

            // 验证会抛出异常
            Assert.Throws<ArgumentException>(() => AIServiceFactory.GetAIService(config));
        }
    }

    // 用于测试的自定义配置类
    public class TestConfig : AIConfig
    {
        // 实现所有必需的接口方法
        public string GetModelName()
        {
            return "TestModel";
        }

        public string GetApiKey() => string.Empty;
        public string GetApiUrl() => string.Empty;
        public string GetModel() => string.Empty;
        public object GetAdditionalConfigByKey(string key) => null!;
        public Dictionary<string, object> GetAdditionalConfigMap() => new Dictionary<string, object>();
        public int GetTimeout() => 0;
        public int GetReadTimeout() => 0;
        public bool GetHasProxy() => false;
        public System.Net.IWebProxy GetProxy() => null!;

        public void SetApiKey(string apiKey) { }
        public void SetApiUrl(string apiUrl) { }
        public void SetModel(string model) { }
        public void PutAdditionalConfigByKey(string key, object value) { }
        public void SetTimeout(int timeout) { }
        public void SetReadTimeout(int readTimeout) { }
        public void SetHasProxy(bool hasProxy) { }
        public void SetProxy(System.Net.IWebProxy proxy) { }
    }
}