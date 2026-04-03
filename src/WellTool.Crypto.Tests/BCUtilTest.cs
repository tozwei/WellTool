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

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// BCUtil BouncyCastle工具类测试
    /// </summary>
    public class BCUtilTest
    {
        [Fact]
        public void CreateSecureRandomTest()
        {
            // 测试安全随机数生成器创建
            var random = BCUtil.CreateSecureRandom();
            Assert.NotNull(random);
            
            byte[] bytes = new byte[16];
            random.NextBytes(bytes);
            Assert.NotNull(bytes);
        }
    }
}
