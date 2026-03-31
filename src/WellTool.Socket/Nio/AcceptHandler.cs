using System.Net.Sockets;

namespace WellTool.Socket.Nio
{
    public class AcceptHandler
    {
        private NioServer _server;
        
        public AcceptHandler(NioServer server)
        {
            _server = server;
        }
        
        public void HandleAccept(System.Net.Sockets.Socket socket)
        {
            // NioServer 使用 handler 处理新连接，这里不需要额外操作
        }
    }
}