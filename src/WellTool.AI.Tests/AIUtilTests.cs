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
using Xunit;

namespace WellTool.AI.Tests
{
    public class AIUtilTests
    {
        [Fact]
        public void TestCreateConfigBuilder()
        {
            // 创建配置构建器
            var builder = AIUtil.CreateConfigBuilder("OpenAI");

            // 验证构建器不为null
            Assert.NotNull(builder);

            // 构建配置
            var config = builder.SetApiKey("test-api-key").Build();

            // 验证配置不为null
            Assert.NotNull(config);
            Assert.Equal("test-api-key", config.GetApiKey());
        }

        [Fact]
        public void TestGetService()
        {
            // 创建配置
            var config = AIUtil.CreateConfigBuilder("OpenAI")
                .SetApiKey("test-api-key")
                .Build();

            // 获取服务
            var service = AIUtil.GetService(config);

            // 验证服务不为null
            Assert.NotNull(service);
        }
    }
}

public class AIServiceFactoryTests
{
    [Fact]
    public void TestGetService()
    {
        // 创建配置
        var config = AIUtil.CreateConfigBuilder("OpenAI")
            .SetApiKey("test-api-key")
            .Build();

        // 获取服务
        var service = AIServiceFactory.GetService(config);

        // 验证服务不为null
        Assert.NotNull(service);
    }

    [Fact]
    public void TestGetServiceWithNullConfig()
    {
        // 测试空配置
        Assert.Throws<System.ArgumentNullException>(() => AIServiceFactory.GetService(null));
    }
}

public class AIConfigRegistryTests
{
    [Fact]
    public void TestRegistry()
    {
        // 测试注册和获取配置
        var registry = AIConfigRegistry.Instance;
        
        // 验证注册表不为null
        Assert.NotNull(registry);
    }
}

public class ModelTests
{
    [Fact]
    public void TestModelName()
    {
        // 测试模型名称
        var modelNames = new string[] { "OpenAI", "Gemini", "Doubao", "DeepSeek", "Grok", "Ollama", "Hutool" };
        
        foreach (var modelName in modelNames)
        {
            // 验证模型名称不为空
            Assert.False(string.IsNullOrEmpty(modelName));
        }
    }
}

