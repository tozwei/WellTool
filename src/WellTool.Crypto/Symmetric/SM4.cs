// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// 国密对称加密算法SM4实现
    /// </summary>
    public class SM4 : SymmetricCrypto
    {
        /// <summary>
        /// SM4算法名称
        /// </summary>
        public const string ALGORITHM_NAME = "SM4";
        
        /// <summary>
        /// 密码模式
        /// </summary>
        private readonly CipherMode _mode;
        
        /// <summary>
        /// 填充模式
        /// </summary>
        private readonly Padding _padding;

        /// <summary>
        /// 构造函数，使用随机密钥，默认ECB模式和PKCS5Padding
        /// </summary>
        public SM4()
            : base(SymmetricAlgorithmType.SM4, GenerateKey(), null)
        {
            _mode = CipherMode.ECB;
            _padding = Padding.PKCS5Padding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16字节）</param>
        public SM4(byte[] key)
            : base(SymmetricAlgorithmType.SM4, key, null)
        {
            _mode = CipherMode.ECB;
            _padding = Padding.PKCS5Padding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16字节）</param>
        /// <param name="iv">初始化向量</param>
        public SM4(byte[] key, byte[]? iv)
            : base(SymmetricAlgorithmType.SM4, key, iv)
        {
            _mode = CipherMode.ECB;
            _padding = Padding.PKCS5Padding;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="padding">填充方式</param>
        public SM4(CipherMode mode, Padding padding)
            : base(SymmetricAlgorithmType.SM4, GenerateKey(), null)
        {
            _mode = mode;
            _padding = padding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="padding">填充方式</param>
        /// <param name="key">密钥</param>
        public SM4(CipherMode mode, Padding padding, byte[] key)
            : base(SymmetricAlgorithmType.SM4, key, null)
        {
            _mode = mode;
            _padding = padding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="padding">填充方式</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        public SM4(CipherMode mode, Padding padding, byte[] key, byte[]? iv)
            : base(SymmetricAlgorithmType.SM4, key, iv)
        {
            _mode = mode;
            _padding = padding;
        }
        
        /// <summary>
        /// 生成随机密钥
        /// </summary>
        private static byte[] GenerateKey()
        {
            var key = new byte[16];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(key);
            return key;
        }
        
        /// <summary>
        /// 创建SM4加密实例
        /// </summary>
        public static SM4 Create()
        {
            return new SM4();
        }

        /// <summary>
        /// 创建PaddedBufferedBlockCipher
        /// </summary>
        private PaddedBufferedBlockCipher CreateCipher(bool forEncryption)
        {
            var engine = new SM4Engine();
            IBlockCipherMode mode;
            
            switch (_mode)
            {
                case CipherMode.CBC:
                    mode = new CbcBlockCipher(engine);
                    break;
                case CipherMode.CFB:
                    mode = new CfbBlockCipher(engine, 128);
                    break;
                case CipherMode.OFB:
                    mode = new OfbBlockCipher(engine, 128);
                    break;
                case CipherMode.ECB:
                default:
                    mode = new EcbBlockCipher(engine);
                    break;
            }
            
            IBlockCipherPadding padding = _padding switch
            {
                Padding.NoPadding => new ZeroBytePadding(),
                Padding.ZeroPadding => new ZeroBytePadding(),
                _ => new Pkcs7Padding()
            };
            
            var cipher = new PaddedBufferedBlockCipher(mode, padding);
            
            var keyParam = new KeyParameter(Key);
            ICipherParameters parameters = keyParam;
            
            // ECB 模式不需要 IV
            if (_mode != CipherMode.ECB && IV != null && IV.Length > 0)
            {
                parameters = new ParametersWithIV(keyParam, IV);
            }
            
            cipher.Init(forEncryption, parameters);
            return cipher;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public override byte[] Encrypt(byte[] data)
        {
            var cipher = CreateCipher(true);
            
            try
            {
                return cipher.DoFinal(data);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("SM4 encryption failed", ex);
            }
        }

        /// <summary>
        /// 加密为十六进制字符串
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的十六进制字符串</returns>
        public string EncryptHex(byte[] data)
        {
            byte[] encrypted = Encrypt(data);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="data">待加密字符串</param>
        /// <param name="encoding">编码，默认UTF-8</param>
        /// <returns>加密后的十六进制字符串</returns>
        public string EncryptHex(string data, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return EncryptHex(encoding.GetBytes(data));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <returns>解密后的数据</returns>
        public override byte[] Decrypt(byte[] data)
        {
            var cipher = CreateCipher(false);
            
            try
            {
                return cipher.DoFinal(data);
            }
            catch (System.Exception ex)
            {
                throw new CryptoException("SM4 decryption failed", ex);
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="data">待解密的十六进制字符串</param>
        /// <param name="encoding">编码，默认UTF-8</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptStr(string data, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            byte[] dataBytes = HexToBytes(data);
            byte[] decrypted = Decrypt(dataBytes);
            return encoding.GetString(decrypted);
        }

        /// <summary>
        /// 十六进制字符串转字节数组
        /// </summary>
        private static byte[] HexToBytes(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0)
                throw new ArgumentException("Invalid hex string");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        
        /// <summary>
        /// 获取加密算法名称
        /// </summary>
        public string GetAlgorithm()
        {
            return $"{ALGORITHM_NAME}/{_mode}/{_padding}";
        }
    }
}
