using System;
using System.Text;
using Xunit;

namespace WellTool.Socket.Tests
{
    /// <summary>
    /// NIO 客户端测试
    /// </summary>
    public class NioClientTest
    {
        [Fact]
        public void TestNioClientCreation()
        {
            // 测试 NioClient 创建
            var client = new Nio.NioClient("127.0.0.1", 8080);
            Assert.NotNull(client);
        }

        [Fact]
        public void TestNioServerCreation()
        {
            // 测试 NioServer 创建
            var server = new Nio.NioServer(8080);
            Assert.NotNull(server);
        }

        [Fact]
        public void TestAioClientCreation()
        {
            // 测试 AioClient 创建
            var client = new Aio.AioClient("127.0.0.1", 8080);
            Assert.NotNull(client);
        }

        [Fact]
        public void TestAioServerCreation()
        {
            // 测试 AioServer 创建
            var server = new Aio.AioServer(8080);
            Assert.NotNull(server);
        }
    }
}
