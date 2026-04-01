using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// ID 工具类
    /// </summary>
    public class IdUtil
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// 生成 UUID
        /// </summary>
        /// <returns>UUID</returns>
        public static string GenerateUUID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 生成无连字符的 UUID
        /// </summary>
        /// <returns>无连字符的 UUID</returns>
        public static string GenerateUUIDWithoutHyphen()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        /// <summary>
        /// 生成雪花 ID
        /// </summary>
        /// <returns>雪花 ID</returns>
        public static long GenerateSnowflakeId()
        {
            // 这里使用简单的实现，实际项目中可能需要更复杂的雪花 ID 生成器
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + _random.Next(1000);
        }

        /// <summary>
        /// 生成随机 ID
        /// </summary>
        /// <param name="length">ID 长度</param>
        /// <returns>随机 ID</returns>
        public static string GenerateRandomId(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成数字 ID
        /// </summary>
        /// <param name="length">ID 长度</param>
        /// <returns>数字 ID</returns>
        public static string GenerateNumericId(int length = 8)
        {
            const string chars = "0123456789";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// 生成基于时间的 ID
        /// </summary>
        /// <returns>基于时间的 ID</returns>
        public static string GenerateTimeBasedId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + GenerateNumericId(4);
        }

        /// <summary>
        /// 生成基于哈希的 ID
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>基于哈希的 ID</returns>
        public static string GenerateHashId(string input)
        {
            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成递增 ID
        /// </summary>
        /// <returns>递增 ID</returns>
        public static long GenerateIncrementId()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}