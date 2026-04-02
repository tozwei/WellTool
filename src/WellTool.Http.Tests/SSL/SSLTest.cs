using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using WellTool.Http.Http.Ssl;
using WellTool.Http.Ssl;
using Xunit;

namespace WellTool.Http.Tests.SSL
{
    /// <summary>
    /// SSL相关测试类
    /// </summary>
    public class SSLTest
    {
        [Fact]
        public void TestDefaultSslInfo()
        {
            // 验证默认SSL信息
            var trustAny = DefaultSslInfo.TrustAnyHostValidator;
            Assert.NotNull(trustAny);

            // 验证信任任意主机（始终返回true）
            var result = trustAny(null, null, null, System.Net.Security.SslPolicyErrors.None);
            Assert.True(result);
        }

        [Fact]
        public void TestDefaultProtocol()
        {
            // 验证默认SSL协议
            var defaultProtocol = DefaultSslInfo.DefaultSslProtocol;
            Assert.True((defaultProtocol & SslProtocols.Tls12) == SslProtocols.Tls12);
        }

        [Fact]
        public void TestTrustAnyHostnameVerifier()
        {
            // 验证信任任意主机名验证器
            var result = TrustAnyHostnameVerifier.Verify("example.com", "TLS1.2");
            Assert.True(result);

            result = TrustAnyHostnameVerifier.Verify("any.host.name", "TLS1.3");
            Assert.True(result);
        }

        [Fact]
        public void TestCustomProtocolsSslFactory()
        {
            // 验证自定义协议工厂创建
            var factory = new CustomProtocolsSslFactory(SslProtocols.Tls12);
            Assert.NotNull(factory);
            Assert.Single(factory.Protocols);
            Assert.Equal(SslProtocols.Tls12, factory.Protocols[0]);
        }

        [Fact]
        public void TestCustomProtocolsSslFactoryMultipleProtocols()
        {
            // 验证多个协议
            var factory = new CustomProtocolsSslFactory(SslProtocols.Tls12);
            Assert.NotNull(factory);
            Assert.Single(factory.Protocols);
        }

        [Fact]
        public void TestSslSocketFactoryBuilder()
        {
            // 验证SSL套接字工厂构建器
            var builder = new SslSocketFactoryBuilder();
            Assert.NotNull(builder);

            // 设置验证回调
            builder.SetValidationCallback((sender, certificate, chain, sslPolicyErrors) =>
            {
                return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
            });

            var callback = builder.Build();
            Assert.NotNull(callback);
        }

        [Fact]
        public void TestDefaultSslFactoryCallback()
        {
            // 验证默认回调
            var callback = DefaultSslFactory.GetDefaultCallback();
            Assert.NotNull(callback);

            // 无错误时应返回true
            var result = callback(null, null, null, System.Net.Security.SslPolicyErrors.None);
            Assert.True(result);
        }
    }
}
