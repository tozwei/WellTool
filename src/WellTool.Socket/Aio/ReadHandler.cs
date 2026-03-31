using System.Net.Sockets;
using System.Text;

namespace WellTool.Socket.Aio
{
    public class ReadHandler
    {
        private AioSession _session;
        
        public ReadHandler(AioSession session)
        {
            _session = session;
        }
        
        public void HandleRead(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success && e.BytesTransferred > 0)
            {
                var data = new byte[e.BytesTransferred];
                System.Buffer.BlockCopy(e.Buffer, e.Offset, data, 0, e.BytesTransferred);
                
                // 处理接收到的数据
                _session.GetIoAction()?.DoAction(_session, data);
                
                // 继续接收数据
                _session.Read();
            }
            else
            {
                // 连接关闭
                _session.Close();
            }
        }
    }
}