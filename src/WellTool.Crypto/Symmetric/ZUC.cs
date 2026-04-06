using System;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// ZUC 加密算法
    /// </summary>
    public class ZUC
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        public ZUC(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] plaintext)
        {
            return Encrypt(plaintext, _key, _iv);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] ciphertext)
        {
            return Decrypt(ciphertext, _key, _iv);
        }

        /// <summary>
        /// 加密字符串并返回十六进制
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文（十六进制）</returns>
        public string EncryptHex(string data)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            var encrypted = Encrypt(bytes);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="hexData">密文（十六进制）</param>
        /// <returns>明文</returns>
        public string DecryptStr(string hexData)
        {
            var bytes = new byte[hexData.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexData.Substring(i * 2, 2), 16);
            }
            var decrypted = Decrypt(bytes);
            return System.Text.Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] plaintext, byte[] key, byte[] iv)
        {
            // 这里只是一个占位符，具体实现需要根据 ZUC 算法的标准来编写
            byte[] ciphertext = new byte[plaintext.Length];
            // 简单的异或操作，实际实现需要使用 ZUC 算法
            for (int i = 0; i < plaintext.Length; i++)
            {
                ciphertext[i] = (byte)(plaintext[i] ^ key[i % key.Length]);
            }
            return ciphertext;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            // 这里只是一个占位符，具体实现需要根据 ZUC 算法的标准来编写
            byte[] plaintext = new byte[ciphertext.Length];
            // 简单的异或操作，实际实现需要使用 ZUC 算法
            for (int i = 0; i < ciphertext.Length; i++)
            {
                plaintext[i] = (byte)(ciphertext[i] ^ key[i % key.Length]);
            }
            return plaintext;
        }
    }
}