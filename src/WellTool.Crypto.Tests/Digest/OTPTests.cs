using System;
using System.Text;
using Xunit;
using WellTool.Crypto.Digest.Otp;

namespace WellTool.Crypto.Tests.Digest
{
    public class OTPTests
    {
        [Fact]
        public void HOTPTest()
        {
            var key = Encoding.UTF8.GetBytes("secret");
            var hotp = new HOTP(key, 6, "HmacSHA1", 0);

            var code1 = hotp.Generate();
            var code2 = hotp.Generate();

            Assert.NotNull(code1);
            Assert.NotNull(code2);
            Assert.NotEqual(code1, code2);
            Assert.Equal(6, code1.Length);
            Assert.Equal(6, code2.Length);
        }

        [Fact]
        public void HOTPVerifyTest()
        {
            var key = Encoding.UTF8.GetBytes("secret");
            var hotp = new HOTP(key, 6, "HmacSHA1", 0);

            var code = hotp.Generate(0);
            var result = hotp.Verify(code, 0);

            Assert.True(result);
        }

        [Fact]
        public void TOTPTest()
        {
            var key = Encoding.UTF8.GetBytes("secret");
            var totp = new TOTP(key, 6, "HmacSHA1", 30);

            var code = totp.Generate();

            Assert.NotNull(code);
            Assert.Equal(6, code.Length);
        }

        [Fact]
        public void TOTPVerifyTest()
        {
            var key = Encoding.UTF8.GetBytes("secret");
            var totp = new TOTP(key, 6, "HmacSHA1", 30);

            var code = totp.Generate();
            var result = totp.Verify(code);

            Assert.True(result);
        }
    }
}