using System;
using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// ChaCha20 加密算法
    /// </summary>
    public class ChaCha20
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="nonce">随机数</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] plaintext, byte[] key, byte[] nonce)
        {
            using (var chacha20 = new ChaCha20Poly1305(key))
            {
                byte[] ciphertext = new byte[plaintext.Length];
                byte[] tag = new byte[16]; // 16 bytes for Poly1305 tag
                chacha20.Encrypt(nonce, plaintext, ciphertext, tag);
                return ciphertext;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="nonce">随机数</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce)
        {
            using (var chacha20 = new ChaCha20Poly1305(key))
            {
                byte[] plaintext = new byte[ciphertext.Length];
                byte[] tag = new byte[16]; // 16 bytes for Poly1305 tag
                chacha20.Decrypt(nonce, ciphertext, tag, plaintext);
                return plaintext;
            }
        }
    }
}