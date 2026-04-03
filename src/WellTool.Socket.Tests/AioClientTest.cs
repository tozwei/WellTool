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
            var endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8080);
            var client = new Aio.AioClient(endpoint, new TestIoAction());
            Assert.NotNull(client);
        }

        private class TestIoAction : Aio.IIoAction<byte[]>
        {
            public void Accept(Aio.AioSession session)
            {
            }

            public void DoAction(Aio.AioSession session, byte[] data)
            {
            }

            public void Failed(Exception exc, Aio.AioSession session)
            {
            }
        }
    }
}
