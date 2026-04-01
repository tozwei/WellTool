using System;
using System.Net;

namespace WellTool.Core.Net
{
    /// <summary>
    /// IPv4 工具类
    /// </summary>
    public class Ipv4Util
    {
        /// <summary>
        /// 将 IPv4 地址转换为整数
        /// </summary>
        /// <param name="ip">IPv4 地址</param>
        /// <returns>整数形式的 IP 地址</returns>
        public static long IpToLong(string ip)
        {
            if (!IPAddress.TryParse(ip, out var ipAddress))
            {
                throw new ArgumentException("Invalid IP address");
            }
            var bytes = ipAddress.GetAddressBytes();
            return (long)(bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
        }

        /// <summary>
        /// 将整数转换为 IPv4 地址
        /// </summary>
        /// <param name="ipLong">整数形式的 IP 地址</param>
        /// <returns>IPv4 地址</returns>
        public static string LongToIp(long ipLong)
        {
            var bytes = new byte[4];
            bytes[0] = (byte)((ipLong >> 24) & 0xFF);
            bytes[1] = (byte)((ipLong >> 16) & 0xFF);
            bytes[2] = (byte)((ipLong >> 8) & 0xFF);
            bytes[3] = (byte)(ipLong & 0xFF);
            return new IPAddress(bytes).ToString();
        }

        /// <summary>
        /// 检查 IP 地址是否在指定范围内
        /// </summary>
        /// <param name="ip">要检查的 IP 地址</param>
        /// <param name="startIp">起始 IP 地址</param>
        /// <param name="endIp">结束 IP 地址</param>
        /// <returns>是否在范围内</returns>
        public static bool IsIpInRange(string ip, string startIp, string endIp)
        {
            var ipLong = IpToLong(ip);
            var startLong = IpToLong(startIp);
            var endLong = IpToLong(endIp);
            return ipLong >= startLong && ipLong <= endLong;
        }

        /// <summary>
        /// 检查 IP 地址是否为私有地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <returns>是否为私有地址</returns>
        public static bool IsPrivateIp(string ip)
        {
            var ipLong = IpToLong(ip);
            var aClassStart = IpToLong("10.0.0.0");
            var aClassEnd = IpToLong("10.255.255.255");
            var bClassStart = IpToLong("172.16.0.0");
            var bClassEnd = IpToLong("172.31.255.255");
            var cClassStart = IpToLong("192.168.0.0");
            var cClassEnd = IpToLong("192.168.255.255");

            return (ipLong >= aClassStart && ipLong <= aClassEnd) ||
                   (ipLong >= bClassStart && ipLong <= bClassEnd) ||
                   (ipLong >= cClassStart && ipLong <= cClassEnd);
        }

        /// <summary>
        /// 检查 IP 地址是否为回环地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <returns>是否为回环地址</returns>
        public static bool IsLoopbackIp(string ip)
        {
            return ip.StartsWith("127.");
        }

        /// <summary>
        /// 检查 IP 地址是否为网络地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>是否为网络地址</returns>
        public static bool IsNetworkAddress(string ip, string subnetMask)
        {
            var ipLong = IpToLong(ip);
            var maskLong = IpToLong(subnetMask);
            var networkLong = ipLong & maskLong;
            return ipLong == networkLong;
        }

        /// <summary>
        /// 检查 IP 地址是否为广播地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>是否为广播地址</returns>
        public static bool IsBroadcastAddress(string ip, string subnetMask)
        {
            var ipLong = IpToLong(ip);
            var maskLong = IpToLong(subnetMask);
            var networkLong = ipLong & maskLong;
            var broadcastLong = networkLong | (~maskLong & 0xFFFFFFFF);
            return ipLong == broadcastLong;
        }

        /// <summary>
        /// 获取子网掩码对应的网络位数
        /// </summary>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>网络位数</returns>
        public static int GetNetworkLength(string subnetMask)
        {
            var maskLong = IpToLong(subnetMask);
            var bits = 0;
            while (maskLong > 0)
            {
                bits++;
                maskLong <<= 1;
            }
            return bits;
        }

        /// <summary>
        /// 获取网络地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>网络地址</returns>
        public static string GetNetworkAddress(string ip, string subnetMask)
        {
            var ipLong = IpToLong(ip);
            var maskLong = IpToLong(subnetMask);
            var networkLong = ipLong & maskLong;
            return LongToIp(networkLong);
        }

        /// <summary>
        /// 获取广播地址
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>广播地址</returns>
        public static string GetBroadcastAddress(string ip, string subnetMask)
        {
            var ipLong = IpToLong(ip);
            var maskLong = IpToLong(subnetMask);
            var networkLong = ipLong & maskLong;
            var broadcastLong = networkLong | (~maskLong & 0xFFFFFFFF);
            return LongToIp(broadcastLong);
        }
    }
}