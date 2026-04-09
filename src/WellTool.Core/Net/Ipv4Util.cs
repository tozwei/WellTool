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
            return (long)((uint)bytes[0] << 24) | (long)((uint)bytes[1] << 16) | (long)((uint)bytes[2] << 8) | (long)bytes[3];
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

        /// <summary>
        /// 将 IPv4 地址转换为整数（兼容方法）
        /// </summary>
        /// <param name="ip">IPv4 地址</param>
        /// <returns>整数形式的 IP 地址</returns>
        public static long Ipv4ToLong(string ip)
        {
            return IpToLong(ip);
        }

        /// <summary>
        /// 将整数转换为 IPv4 地址（兼容方法）
        /// </summary>
        /// <param name="ipLong">整数形式的 IP 地址</param>
        /// <returns>IPv4 地址</returns>
        public static string LongToIpv4(long ipLong)
        {
            return LongToIp(ipLong);
        }

        /// <summary>
        /// 获取子网掩码对应的网络位数
        /// </summary>
        /// <param name="subnetMask">子网掩码</param>
        /// <returns>网络位数</returns>
        public static int GetMaskBitByMask(string subnetMask)
        {
            var maskLong = IpToLong(subnetMask);
            int bits = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((maskLong & (1L << (31 - i))) != 0)
                {
                    bits++;
                }
                else
                {
                    break;
                }
            }
            return bits;
        }

        /// <summary>
        /// 根据网络位数获取子网掩码
        /// </summary>
        /// <param name="maskBit">网络位数</param>
        /// <returns>子网掩码</returns>
        public static string GetMaskByMaskBit(int maskBit)
        {
            if (maskBit < 0 || maskBit > 32)
            {
                throw new ArgumentException("Mask bit must be between 0 and 32");
            }
            long maskLong = maskBit == 0 ? 0 : 0xFFFFFFFF << (32 - maskBit);
            return LongToIp(maskLong);
        }

        /// <summary>
        /// 获取IP范围的结束地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="maskBit">网络位数</param>
        /// <returns>结束地址</returns>
        public static string GetEndIpStr(string ip, int maskBit)
        {
            var ipLong = IpToLong(ip);
            long maskLong = maskBit == 0 ? 0 : 0xFFFFFFFF << (32 - maskBit);
            long networkLong = ipLong & maskLong;
            long broadcastLong = networkLong | (~maskLong & 0xFFFFFFFF);
            return LongToIp(broadcastLong);
        }

        /// <summary>
        /// 获取IP范围列表
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="maskBit">网络位数</param>
        /// <returns>IP范围列表</returns>
        public static List<string> Ipv4RangeList(string ip, int maskBit)
        {
            var result = new List<string>();
            var startIp = GetNetworkAddress(ip, GetMaskByMaskBit(maskBit));
            var endIp = GetEndIpStr(ip, maskBit);
            var startLong = IpToLong(startIp);
            var endLong = IpToLong(endIp);

            // 跳过网络地址和广播地址
            for (long i = startLong + 1; i < endLong; i++)
            {
                result.Add(LongToIp(i));
            }

            return result;
        }

        /// <summary>
        /// 检查子网掩码是否有效
        /// </summary>
        /// <param name="mask">子网掩码</param>
        /// <returns>是否有效</returns>
        public static bool IsMaskValid(string mask)
        {
            try
            {
                var maskLong = IpToLong(mask);
                // 检查是否为连续的1后跟连续的0
                bool foundZero = false;
                for (int i = 0; i < 32; i++)
                {
                    bool bit = (maskLong & (1L << (31 - i))) != 0;
                    if (foundZero && bit)
                    {
                        return false;
                    }
                    if (!bit)
                    {
                        foundZero = true;
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
        /// 检查IP地址是否匹配模式
        /// </summary>
        /// <param name="pattern">模式</param>
        /// <param name="ip">IP地址</param>
        /// <returns>是否匹配</returns>
        public static bool Matches(string pattern, string ip)
        {
            var patternParts = pattern.Split('.');
            var ipParts = ip.Split('.');

            if (patternParts.Length != 4 || ipParts.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (patternParts[i] == "*")
                {
                    continue;
                }
                if (patternParts[i] != ipParts[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}