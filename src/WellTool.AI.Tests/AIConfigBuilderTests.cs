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

using System.Net;
using WellTool.AI.Core;

namespace WellTool.AI.Tests
{
    public class AIConfigBuilderTests
    {
        [Fact]
        public void TestCreateBuilderAndBuildConfig()
        {
            // 创建构建器
            var builder = new AIConfigBuilder("OpenAI");
            
            // 验证构建器不为null
            Assert.NotNull(builder);
            
            // 构建配置
            var config = builder.Build();
            
            // 验证配置不为null
            Assert.NotNull(config);
        }

        [Fact]
        public void TestChainedMethodCalls()
        {
            // 创建构建器并设置各种属性
            var builder = new AIConfigBuilder("OpenAI")
                .SetApiKey("test-api-key")
                .SetApiUrl("https://api.openai.com/v1")
                .SetModel("gpt-4")
                .PutAdditionalConfig("temperature", 0.7)
                .SetTimeout(30000)
                .SetReadTimeout(60000);
            
            // 构建配置
            var config = builder.Build();
            
            // 验证所有属性都已正确设置
            Assert.Equal("test-api-key", config.GetApiKey());
            Assert.Equal("https://api.openai.com/v1", config.GetApiUrl());
            Assert.Equal("gpt-4", config.GetModel());
            Assert.Equal(0.7, config.GetAdditionalConfigByKey("temperature"));
            Assert.Equal(30000, config.GetTimeout());
            Assert.Equal(60000, config.GetReadTimeout());
        }

        [Fact]
        public void TestInvalidModelName()
        {
            // 测试使用无效的模型名称
            Assert.Throws<ArgumentException>(() => new AIConfigBuilder("NonExistentModel"));
        }

        [Fact]
        public void TestSetProxy()
        {
            // 创建代理
            var proxy = new WebProxy("http://localhost:8080");
            
            // 创建构建器并设置代理
            var builder = new AIConfigBuilder("OpenAI")
                .SetProxy(proxy);
            
            // 构建配置
            var config = builder.Build();
            
            // 验证代理已设置
            Assert.True(config.GetHasProxy());
            Assert.Equal(proxy, config.GetProxy());
        }

        [Fact]
        public void TestNullValues()
        {
            // 创建构建器并设置null值
            var builder = new AIConfigBuilder("OpenAI")
                .SetApiKey(null!)
                .SetApiUrl(null!)
                .SetModel(null!)
                .PutAdditionalConfig("test", null!);
            
            // 构建配置
            var config = builder.Build();
            
            // 验证配置不为null（应该使用默认值）
            Assert.NotNull(config);
        }

        [Fact]
        public void TestNonPositiveTimeouts()
        {
            // 创建构建器并设置非正数的超时值
            var builder = new AIConfigBuilder("OpenAI")
                .SetTimeout(0)
                .SetReadTimeout(-1);
            
            // 构建配置
            var config = builder.Build();
            
            // 验证超时值应该使用默认值（不为0或负数）
            Assert.True(config.GetTimeout() > 0);
            Assert.True(config.GetReadTimeout() > 0);
        }
    }
}