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

namespace WellTool.AI.Tests
{
    public class MessageTests
    {
        [Fact]
        public void TestDefaultConstructor()
        {
            // 创建Message实例
            var message = new Message();

            // 验证默认值
            Assert.Null(message.Role);
            Assert.Null(message.Content);
        }

        [Fact]
        public void TestParameterizedConstructor()
        {
            // 创建Message实例
            var role = "user";
            var content = "Hello, AI!";
            var message = new Message(role, content);

            // 验证属性值
            Assert.Equal(role, message.Role);
            Assert.Equal(content, message.Content);
        }

        [Fact]
        public void TestPropertySetters()
        {
            // 创建Message实例
            var message = new Message();

            // 设置属性
            var role = "assistant";
            var content = "Hello, human!";
            message.Role = role;
            message.Content = content;

            // 验证属性值
            Assert.Equal(role, message.Role);
            Assert.Equal(content, message.Content);
        }

        [Fact]
        public void TestContentWithDifferentTypes()
        {
            // 测试不同类型的内容
            var message = new Message();

            // 字符串内容
            message.Content = "String content";
            Assert.Equal("String content", message.Content);

            // 数字内容
            message.Content = 42;
            Assert.Equal(42, message.Content);

            // 布尔内容
            message.Content = true;
            Assert.True((bool)message.Content!);
        }
    }
}