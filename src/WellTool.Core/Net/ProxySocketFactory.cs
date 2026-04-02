using System;
using System.Net;
using System.Net.Sockets;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 代理Socket工厂，用于创建代理Socket
    /// </summary>
    public class ProxySocketFactory
    {
        private readonly IWebProxy _proxy;

        /// <summary>
        /// 创建代理SocketFactory
        /// </summary>
        /// <param name="proxy">代理对象</param>
        /// <returns>ProxySocketFactory</returns>
        public static ProxySocketFactory Of(IWebProxy proxy)
        {
            return new ProxySocketFactory(proxy);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="proxy">Socket代理</param>
        public ProxySocketFactory(IWebProxy proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// 创建Socket
        /// </summary>
        /// <returns>Socket</returns>
        public Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 创建Socket并连接到指定地址和端口
        /// </summary>
        /// <param name="address">目标地址</param>
        /// <param name="port">目标端口</param>
        /// <returns>Socket</returns>
        public Socket CreateSocket(IPAddress address, int port)
        {
            var socket = CreateSocket();
            socket.Connect(new IPEndPoint(address, port));
            return socket;
        }

        /// <summary>
        /// 创建Socket并连接到指定地址和端口，同时绑定本地地址和端口
        /// </summary>
        /// <param name="address">目标地址</param>
        /// <param name="port">目标端口</param>
        /// <param name="localAddr">本地地址</param>
        /// <param name="localPort">本地端口</param>
        /// <returns>Socket</returns>
        public Socket CreateSocket(IPAddress address, int port, IPAddress localAddr, int localPort)
        {
            var socket = CreateSocket();
            socket.Bind(new IPEndPoint(localAddr, localPort));
            socket.Connect(new IPEndPoint(address, port));
            return socket;
        }

        /// <summary>
        /// 创建Socket并连接到指定主机和端口
        /// </summary>
        /// <param name="host">目标主机</param>
        /// <param name="port">目标端口</param>
        /// <returns>Socket</returns>
        public Socket CreateSocket(string host, int port)
        {
            var socket = CreateSocket();
            socket.Connect(host, port);
            return socket;
        }

        /// <summary>
        /// 创建Socket并连接到指定主机和端口，同时绑定本地地址和端口
        /// </summary>
        /// <param name="host">目标主机</param>
        /// <param name="port">目标端口</param>
        /// <param name="localAddr">本地地址</param>
        /// <param name="localPort">本地端口</param>
        /// <returns>Socket</returns>
        public Socket CreateSocket(string host, int port, IPAddress localAddr, int localPort)
        {
            var socket = CreateSocket();
            socket.Bind(new IPEndPoint(localAddr, localPort));
            socket.Connect(host, port);
            return socket;
        }
    }
}