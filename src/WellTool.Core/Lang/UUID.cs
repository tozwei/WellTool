using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// UUID 工具类
    /// </summary>
    public class UUID
    {
        /// <summary>
        /// 生成 UUID
        /// </summary>
        /// <returns>UUID 字符串</returns>
        public static string RandomUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 生成无连字符的 UUID
        /// </summary>
        /// <returns>无连字符的 UUID 字符串</returns>
        public static string RandomUUIDWithoutHyphen()
        {
            return System.Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// 生成 UUID（基于时间戳）
        /// </summary>
        /// <returns>基于时间戳的 UUID 字符串</returns>
        public static string TimeBasedUUID()
        {
            var guid = System.Guid.NewGuid();
            var bytes = guid.ToByteArray();
            var timestamp = DateTime.UtcNow.Ticks;
            var timestampBytes = BitConverter.GetBytes(timestamp);
            
            // 替换前8个字节为时间戳
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }
            Array.Copy(timestampBytes, 0, bytes, 0, 8);
            
            return new System.Guid(bytes).ToString();
        }

        /// <summary>
        /// 从字符串解析 UUID
        /// </summary>
        /// <param name="uuid">UUID 字符串</param>
        /// <returns>Guid 对象</returns>
        public static System.Guid FromString(string uuid)
        {
            return System.Guid.Parse(uuid);
        }

        /// <summary>
        /// 检查字符串是否是有效的 UUID
        /// </summary>
        /// <param name="uuid">UUID 字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsValid(string uuid)
        {
            return System.Guid.TryParse(uuid, out _);
        }

        /// <summary>
        /// 获取 UUID 的字节数组
        /// </summary>
        /// <param name="uuid">UUID 字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] ToByteArray(string uuid)
        {
            return FromString(uuid).ToByteArray();
        }

        /// <summary>
        /// 从字节数组创建 UUID
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>UUID 字符串</returns>
        public static string FromByteArray(byte[] bytes)
        {
            return new System.Guid(bytes).ToString();
        }
    }
}