using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 哈希工具类
    /// </summary>
    public class HashUtil
    {
        /// <summary>
        /// 计算 MD5 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5 哈希值</returns>
        public static string MD5(string input)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 SHA1 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA1 哈希值</returns>
        public static string SHA1(string input)
        {
            using var sha1 = System.Security.Cryptography.SHA1.Create();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 SHA256 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA256 哈希值</returns>
        public static string SHA256(string input)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 SHA384 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA384 哈希值</returns>
        public static string SHA384(string input)
        {
            using var sha384 = System.Security.Cryptography.SHA384.Create();
            var bytes = sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 SHA512 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA512 哈希值</returns>
        public static string SHA512(string input)
        {
            using var sha512 = System.Security.Cryptography.SHA512.Create();
            var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 HMAC-MD5 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>HMAC-MD5 哈希值</returns>
        public static string HMACMD5(string input, string key)
        {
            using var hmac = new HMACMD5(Encoding.UTF8.GetBytes(key));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 HMAC-SHA1 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>HMAC-SHA1 哈希值</returns>
        public static string HMACSHA1(string input, string key)
        {
            using var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 计算 HMAC-SHA256 哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>HMAC-SHA256 哈希值</returns>
        public static string HMACSHA256(string input, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BytesToHex(bytes);
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>十六进制字符串</returns>
        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}