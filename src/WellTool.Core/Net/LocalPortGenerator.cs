using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 本地端口生成器，用于生成可用的本地端口
    /// </summary>
    public static class LocalPortGenerator
    {
        private static readonly Random _random = new Random();
        private static readonly HashSet<int> _usedPorts = new HashSet<int>();

        /// <summary>
        /// 生成一个随机的可用端口
        /// </summary>
        /// <returns>可用端口号</returns>
        public static int Generate()
        {
            return Generate(5000, 60000);
        }

        /// <summary>
        /// 在指定范围内生成一个随机的可用端口
        /// </summary>
        /// <param name="minPort">最小端口号</param>
        /// <param name="maxPort">最大端口号</param>
        /// <returns>可用端口号</returns>
        public static int Generate(int minPort, int maxPort)
        {
            int maxAttempts = 100;
            for (int i = 0; i < maxAttempts; i++)
            {
                int port = _random.Next(minPort, maxPort + 1);
                if (IsPortAvailable(port) && !_usedPorts.Contains(port))
                {
                    _usedPorts.Add(port);
                    return port;
                }
            }

            // 如果随机失败，尝试顺序查找
            for (int port = minPort; port <= maxPort; port++)
            {
                if (IsPortAvailable(port) && !_usedPorts.Contains(port))
                {
                    _usedPorts.Add(port);
                    return port;
                }
            }

            throw new InvalidOperationException("No available port found in the specified range");
        }

        /// <summary>
        /// 检查端口是否可用
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>是否可用</returns>
        public static bool IsPortAvailable(int port)
        {
            try
            {
                // 检查TCP端口
                var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                var tcpListeners = ipGlobalProperties.GetActiveTcpListeners();
                
                foreach (var endpoint in tcpListeners)
                {
                    if (endpoint.Port == port)
                    {
                        return false;
                    }
                }

                // 检查TCP连接
                var tcpConnections = ipGlobalProperties.GetActiveTcpConnections();
                foreach (var conn in tcpConnections)
                {
                    if (conn.LocalEndPoint.Port == port)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 标记端口为已使用
        /// </summary>
        /// <param name="port">端口号</param>
        public static void MarkAsUsed(int port)
        {
            _usedPorts.Add(port);
        }

        /// <summary>
        /// 标记端口为可用
        /// </summary>
        /// <param name="port">端口号</param>
        public static void MarkAsAvailable(int port)
        {
            _usedPorts.Remove(port);
        }

        /// <summary>
        /// 获取所有可用的端口
        /// </summary>
        /// <param name="minPort">最小端口号</param>
        /// <param name="maxPort">最大端口号</param>
        /// <returns>可用端口列表</returns>
        public static List<int> GetAvailablePorts(int minPort = 5000, int maxPort = 60000)
        {
            var result = new List<int>();
            for (int port = minPort; port <= maxPort; port++)
            {
                if (IsPortAvailable(port))
                {
                    result.Add(port);
                }
            }
            return result;
        }
    }
}
