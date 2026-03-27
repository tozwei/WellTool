using System;
using System.Security.Cryptography;

namespace WellTool.Crypto.Digest.Mac
{
    /// <summary>
    /// MAC（消息认证码）工具类
    /// </summary>
    public static class Mac
    {
        /// <summary>
        /// 创建HMAC-MD5
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="data">数据</param>
        /// <returns>MAC值</returns>
        public static byte[] HmacMd5(byte[] key, byte[] data)
        {
            using var hmac = new HMACMD5(key);
            return hmac.ComputeHash(data);
        }

        /// <summary>
        /// 创建HMAC-SHA1
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="data">数据</param>
        /// <returns>MAC值</returns>
        public static byte[] HmacSha1(byte[] key, byte[] data)
        {
            using var hmac = new HMACSHA1(key);
            return hmac.ComputeHash(data);
        }

        /// <summary>
        /// 创建HMAC-SHA256
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="data">数据</param>
        /// <returns>MAC值</returns>
        public static byte[] HmacSha256(byte[] key, byte[] data)
        {
            using var hmac = new HMACSHA256(key);
            return hmac.ComputeHash(data);
        }

        /// <summary>
        /// 创建HMAC-SHA384
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="data">数据</param>
        /// <returns>MAC值</returns>
        public static byte[] HmacSha384(byte[] key, byte[] data)
        {
            using var hmac = new HMACSHA384(key);
            return hmac.ComputeHash(data);
        }

        /// <summary>
        /// 创建HMAC-SHA512
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="data">数据</param>
        /// <returns>MAC值</returns>
        public static byte[] HmacSha512(byte[] key, byte[] data)
        {
            using var hmac = new HMACSHA512(key);
            return hmac.ComputeHash(data);
        }
    }
}