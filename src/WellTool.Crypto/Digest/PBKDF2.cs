using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    /// <summary>
    /// PBKDF2 密钥派生函数
    /// </summary>
    public class PBKDF2
    {
        /// <summary>
        /// 派生密钥
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt">盐</param>
        /// <param name="iterations">迭代次数</param>
        /// <param name="keyLength">密钥长度（字节）</param>
        /// <returns>派生的密钥</returns>
        public static byte[] DeriveKey(string password, string salt, int iterations, int keyLength)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), iterations))
            {
                return pbkdf2.GetBytes(keyLength);
            }
        }

        /// <summary>
        /// 派生密钥并返回十六进制字符串
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt">盐</param>
        /// <param name="iterations">迭代次数</param>
        /// <param name="keyLength">密钥长度（字节）</param>
        /// <returns>派生的密钥的十六进制字符串</returns>
        public static string DeriveKeyHex(string password, string salt, int iterations, int keyLength)
        {
            var key = DeriveKey(password, salt, iterations, keyLength);
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }
}