using System;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 子网掩码工具类
    /// </summary>
    public static class MaskBit
    {
        /// <summary>
        /// 将CIDR表示的数字转换为点分十进制格式
        /// </summary>
        /// <param name="maskBit">CIDR表示的数字 (0-32)</param>
        /// <returns>点分十进制格式的掩码</returns>
        public static string GetMask(string maskBit)
        {
            if (!int.TryParse(maskBit, out int bits))
            {
                throw new ArgumentException("Invalid mask bit value");
            }
            return GetMask(bits);
        }

        /// <summary>
        /// 将CIDR表示的数字转换为点分十进制格式
        /// </summary>
        /// <param name="maskBit">CIDR表示的数字 (0-32)</param>
        /// <returns>点分十进制格式的掩码</returns>
        public static string GetMask(int maskBit)
        {
            if (maskBit < 0 || maskBit > 32)
            {
                throw new ArgumentException("Mask bit must be between 0 and 32");
            }

            if (maskBit == 0)
            {
                return "0.0.0.0";
            }

            uint mask = uint.MaxValue << (32 - maskBit);
            return $"{(mask >> 24) & 0xFF}.{(mask >> 16) & 0xFF}.{(mask >> 8) & 0xFF}.{mask & 0xFF}";
        }

        /// <summary>
        /// 将点分十进制格式的掩码转换为CIDR表示的数字
        /// </summary>
        /// <param name="mask">点分十进制格式的掩码</param>
        /// <returns>CIDR表示的数字</returns>
        public static int GetMaskBit(string mask)
        {
            if (string.IsNullOrWhiteSpace(mask))
            {
                throw new ArgumentException("Invalid mask format");
            }

            var parts = mask.Split('.');
            if (parts.Length != 4)
            {
                throw new ArgumentException("Invalid mask format");
            }

            uint maskValue = 0;
            for (int i = 0; i < 4; i++)
            {
                if (!byte.TryParse(parts[i], out byte b))
                {
                    throw new ArgumentException("Invalid mask format");
                }
                maskValue = (maskValue << 8) | b;
            }

            return GetMaskBit(maskValue);
        }

        /// <summary>
        /// 将uint格式的掩码转换为CIDR表示的数字
        /// </summary>
        /// <param name="mask">uint格式的掩码</param>
        /// <returns>CIDR表示的数字</returns>
        public static int GetMaskBit(uint mask)
        {
            int bits = 0;
            while ((mask & 0x80000000) != 0)
            {
                bits++;
                mask <<= 1;
            }
            return bits;
        }

        /// <summary>
        /// 检查IP是否在指定网段内
        /// </summary>
        /// <param name="ip">要检查的IP</param>
        /// <param name="network">网络地址</param>
        /// <param name="maskBit">子网掩码位数</param>
        /// <returns>是否在网段内</returns>
        public static bool IsInRange(string ip, string network, int maskBit)
        {
            var ipValue = ParseIpToUint(ip);
            var networkValue = ParseIpToUint(network);
            var mask = uint.MaxValue << (32 - maskBit);

            return (ipValue & mask) == (networkValue & mask);
        }

        /// <summary>
        /// 解析IP为uint
        /// </summary>
        private static uint ParseIpToUint(string ip)
        {
            var parts = ip.Split('.');
            if (parts.Length != 4)
            {
                throw new ArgumentException("Invalid IP format");
            }

            uint result = 0;
            for (int i = 0; i < 4; i++)
            {
                if (!byte.TryParse(parts[i], out byte b))
                {
                    throw new ArgumentException("Invalid IP format");
                }
                result = (result << 8) | b;
            }

            return result;
        }
    }
}
