using System;
using System.Net;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 网络工具单元测试
    /// </summary>
    public class NetTests
    {
        #region 基础网络测试

        [Fact]
        public void BasicNetworkTest()
        {
            // 测试基本网络功能
            var hostName = Dns.GetHostName();
            Assert.NotNull(hostName);
        }

        [Fact]
        public void UriTest()
        {
            // 测试URI解析
            var uri = new Uri("https://www.example.com/path?query=value");
            Assert.Equal("https", uri.Scheme);
            Assert.Equal("www.example.com", uri.Host);
            Assert.Equal("/path", uri.AbsolutePath);
            Assert.Equal("?query=value", uri.Query);
        }

        #endregion
    }
}