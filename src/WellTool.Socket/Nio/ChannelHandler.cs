using System.Net.Sockets;

namespace WellTool.Socket.Nio
{
    public interface ChannelHandler
    {
        void HandleRead(System.Net.Sockets.Socket socket, byte[] data);
        
        void HandleWrite(System.Net.Sockets.Socket socket, byte[] data);
        
        void HandleClose(System.Net.Sockets.Socket socket);
    }
}