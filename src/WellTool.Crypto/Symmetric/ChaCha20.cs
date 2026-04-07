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
        /// <returns>密文（包含认证标签）</returns>
        public static byte[] Encrypt(byte[] plaintext, byte[] key, byte[] nonce)
        {
#if NET6_0_OR_GREATER
            using (var chacha20 = new ChaCha20Poly1305(key))
            {
                byte[] ciphertext = new byte[plaintext.Length];
                byte[] tag = new byte[16]; // 16 bytes for Poly1305 tag
                chacha20.Encrypt(nonce, plaintext, ciphertext, tag);
                
                // 将密文和标签拼接返回
                byte[] result = new byte[ciphertext.Length + tag.Length];
                Buffer.BlockCopy(ciphertext, 0, result, 0, ciphertext.Length);
                Buffer.BlockCopy(tag, 0, result, ciphertext.Length, tag.Length);
                return result;
            }
#else
            // .NET 6.0 以下版本的实现
            // 使用 Bouncy Castle 库实现 ChaCha20 加密
            try
            {
                // 这里使用 Bouncy Castle 库的实现
                // 注意：需要添加 BouncyCastle.Cryptography NuGet 包
                var engine = new Org.BouncyCastle.Crypto.Engines.ChaChaEngine(20); // ChaCha20 引擎
                var parameters = new Org.BouncyCastle.Crypto.Parameters.ParametersWithIV(
                    new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key),
                    nonce);
                engine.Init(true, parameters);
                
                byte[] ciphertext = new byte[plaintext.Length];
                engine.ProcessBytes(plaintext, 0, plaintext.Length, ciphertext, 0);
                
                return ciphertext;
            }
            catch (System.Exception ex)
            {
                throw new NotSupportedException(
                    "ChaCha20 encryption is not supported in this framework version. " +
                    "Please install BouncyCastle.Cryptography NuGet package.", ex);
            }
#endif
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文（包含认证标签）</param>
        /// <param name="key">密钥</param>
        /// <param name="nonce">随机数</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] nonce)
        {
#if NET6_0_OR_GREATER
            using (var chacha20 = new ChaCha20Poly1305(key))
            {
                // 分离密文和标签
                int tagLength = 16;
                int cipherLength = ciphertext.Length - tagLength;
                byte[] cipherText = new byte[cipherLength];
                byte[] tag = new byte[tagLength];
                Buffer.BlockCopy(ciphertext, 0, cipherText, 0, cipherLength);
                Buffer.BlockCopy(ciphertext, cipherLength, tag, 0, tagLength);
                
                byte[] plaintext = new byte[cipherLength];
                chacha20.Decrypt(nonce, cipherText, tag, plaintext);
                return plaintext;
            }
#else
            // .NET 6.0 以下版本的实现
            // 使用 Bouncy Castle 库实现 ChaCha20 解密
            try
            {
                // 这里使用 Bouncy Castle 库的实现
                // 注意：需要添加 BouncyCastle.Cryptography NuGet 包
                var engine = new Org.BouncyCastle.Crypto.Engines.ChaChaEngine(20); // ChaCha20 引擎
                var parameters = new Org.BouncyCastle.Crypto.Parameters.ParametersWithIV(
                    new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key),
                    nonce);
                engine.Init(false, parameters);
                
                byte[] plaintext = new byte[ciphertext.Length];
                engine.ProcessBytes(ciphertext, 0, ciphertext.Length, plaintext, 0);
                
                return plaintext;
            }
            catch (System.Exception ex)
            {
                throw new NotSupportedException(
                    "ChaCha20 decryption is not supported in this framework version. " +
                    "Please install BouncyCastle.Cryptography NuGet package.", ex);
            }
#endif
        }
    }
}