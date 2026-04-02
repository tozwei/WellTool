using Xunit;

namespace WellTool.Socket.Tests
{
    /// <summary>
    /// AIO 客户端测试
    /// </summary>
    public class AioClientTest
    {
        [Fact]
        public void TestAioClient()
        {
            var client = new Aio.AioClient("127.0.0.1", 8080);
            Assert.NotNull(client);
        }
    }
}
