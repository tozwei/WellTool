using System.Net.Sockets;
using System.Text;

namespace WellTool.Socket.Nio
{
    public class NioUtil
    {
        public static byte[] ReadAll(System.Net.Sockets.Socket socket, int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            var bytesRead = socket.Receive(buffer);
            var data = new byte[bytesRead];
            System.Buffer.BlockCopy(buffer, 0, data, 0, bytesRead);
            return data;
        }
        
        public static void WriteAll(System.Net.Sockets.Socket socket, byte[] data)
        {
            socket.Send(data);
        }
        
        public static string GetRemoteAddress(System.Net.Sockets.Socket socket)
        {
            return socket.RemoteEndPoint.ToString();
        }
    }
}