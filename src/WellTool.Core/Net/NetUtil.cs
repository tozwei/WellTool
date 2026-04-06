using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 网络工具类
    /// </summary>
    public class NetUtil
    {
        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns>本地IP地址</returns>
        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return IPAddress.Loopback;
        }

        /// <summary>
        /// 获取本地IP地址字符串
        /// </summary>
        /// <returns>本地IP地址字符串</returns>
        public static string GetLocalIP()
        {
            return GetLocalIPAddress().ToString();
        }

        /// <summary>
        /// 检查端口是否可用
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>是否可用</returns>
        public static bool IsPortAvailable(int port)
        {
            try
            {
                using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Any, port));
                socket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取可用端口
        /// </summary>
        /// <returns>可用端口</returns>
        public static int GetAvailablePort()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
            var port = ((IPEndPoint)socket.LocalEndPoint).Port;
            socket.Close();
            return port;
        }

        /// <summary>
        /// 检查IP地址是否有效
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否有效</returns>
        public static bool IsValidIP(string ip)
        {
            return IPAddress.TryParse(ip, out _);
        }

        /// <summary>
        /// 检查端口是否有效
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>是否有效</returns>
        public static bool IsValidPort(int port)
        {
            return port >= 0 && port <= 65535;
        }

        /// <summary>
        /// 检查域名是否有效
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>是否有效</returns>
        public static bool IsValidDomain(string domain)
        {
            try
            {
                Dns.GetHostEntry(domain);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取域名的IP地址
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>IP地址列表</returns>
        public static IPAddress[] GetDomainIPs(string domain)
        {
            try
            {
                var host = Dns.GetHostEntry(domain);
                return host.AddressList;
            }
            catch
            {
                return Array.Empty<IPAddress>();
            }
        }

        /// <summary>
        /// 检查网络连接是否可用
        /// </summary>
        /// <returns>是否可用</returns>
        public static bool IsNetworkAvailable()
        {
            try
            {
                using var client = new TcpClient();
                var result = client.BeginConnect("www.baidu.com", 80, null, null);
                var success = result.AsyncWaitHandle.WaitOne(1000);
                client.EndConnect(result);
                client.Close();
                return success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取网络类型
        /// </summary>
        /// <returns>网络类型</returns>
        public static string GetNetworkType()
        {
            try
            {
                var ip = GetLocalIPAddress();
                if (IPAddress.IsLoopback(ip))
                {
                    return "Localhost";
                }
                else if (ip.ToString().StartsWith("192.168."))
                {
                    return "LAN";
                }
                else if (ip.ToString().StartsWith("10."))
                {
                    return "LAN";
                }
                else if (ip.ToString().StartsWith("172.16."))
                {
                    return "LAN";
                }
                else
                {
                    return "WAN";
                }
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}