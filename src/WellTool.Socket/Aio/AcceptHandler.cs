using System.Net.Sockets;

namespace WellTool.Socket.Aio
{
    public class AcceptHandler
    {
        private AioServer _server;
        
        public AcceptHandler(AioServer server)
        {
            _server = server;
        }
        
        public void HandleAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                var socket = e.AcceptSocket;
                if (socket != null && _server.GetIoAction() != null)
                {
                    var session = new AioSession(socket, _server.GetIoAction()!, _server.Config);
                    
                    // 处理请求接入
                    _server.GetIoAction()?.Accept(session);
                    
                    // 开始接收数据
                    session.Read();
                }
            }
            
            // 继续接受新的连接
            _server.Accept();
        }
    }
}