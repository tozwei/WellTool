using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto
{
    /// <summary>
    /// 安全工具类
    /// </summary>
    public static class SecureUtil
    {


        /// <summary>
        /// 生成安全的随机字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>MD5摘要</returns>
        public static byte[] Md5(byte[] data)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            return md5.ComputeHash(data);
        }

        /// <summary>
        /// 创建MD5摘要
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>MD5摘要</returns>
        public static byte[] Md5(string str)
        {
            return Md5(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 创建SHA1摘要
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>SHA1摘要</returns>
        public static byte[] Sha1(byte[] data)
        {
            using var sha1 = System.Security.Cryptography.SHA1.Create();
            return sha1.ComputeHash(data);
        }

        /// <summary>
        /// 创建SHA256摘要
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>SHA256摘要</returns>
        public static byte[] Sha256(byte[] data)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            return sha256.ComputeHash(data);
        }

        /// <summary>
        /// 创建AES加密器
        /// </summary>
        /// <param name="key">密钥</param>
        /// <returns>AES加密器</returns>
        public static Symmetric.AES Aes(byte[] key)
        {
            return new Symmetric.AES(key);
        }

        /// <summary>
        /// 创建RSA加密器
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>RSA加密器</returns>
        public static Asymmetric.RSA Rsa(byte[] privateKey)
        {
            return new Asymmetric.RSA(privateKey);
        }
    }
}