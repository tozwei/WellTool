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

using System;
using Xunit;
using WellTool.Crypto.Digest.Otp;

namespace WellTool.Crypto.Tests
{
    /// <summary>
    /// OTP一次性密码测试
    /// </summary>
    public class OTPTest
    {
        [Fact]
        public void HOTPTest()
        {
            // 测试HOTP
            var hotp = new HOTP("JBSWY3DPEHPK3PXP");
            string code = hotp.Generate(123456);
            
            Assert.NotNull(code);
            Assert.Equal(6, code.Length);
        }

        [Fact]
        public void TOTPTest()
        {
            // 测试TOTP
            var totp = new TOTP("JBSWY3DPEHPK3PXP");
            string code = totp.Generate();
            
            Assert.NotNull(code);
            Assert.Equal(6, code.Length);
        }
    }
}
