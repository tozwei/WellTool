using System;
using System.Text;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// TEA 加密算法
    /// </summary>
    public class TEA
    {
        private static readonly uint DELTA = 0x9E3779B9;
        private const int BlockSize = 8;
        
        private readonly byte[] _key;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16字节）</param>
        public TEA(byte[] key)
        {
            if (key.Length != 16)
                throw new ArgumentException("TEA key must be 16 bytes");
            _key = (byte[])key.Clone();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] data)
        {
            return Encrypt(data, _key);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] data)
        {
            return Decrypt(data, _key);
        }

        /// <summary>
        /// 加密字符串并返回十六进制
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文（十六进制）</returns>
        public string EncryptHex(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var encrypted = Encrypt(bytes);
            return Convert.ToHexString(encrypted).ToLower();
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="hexData">密文（十六进制）</param>
        /// <returns>明文</returns>
        public string DecryptStr(string hexData)
        {
            var bytes = Convert.FromHexString(hexData);
            var decrypted = Decrypt(bytes);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// 加密任意长度数据（使用PKCS7填充）
        /// </summary>
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            // PKCS7 填充
            int padLen = BlockSize - (data.Length % BlockSize);
            byte[] padded = new byte[data.Length + padLen];
            Array.Copy(data, 0, padded, 0, data.Length);
            for (int i = data.Length; i < padded.Length; i++)
            {
                padded[i] = (byte)padLen;
            }

            // 分块加密
            byte[] result = new byte[padded.Length];
            for (int i = 0; i < padded.Length; i += BlockSize)
            {
                byte[] block = new byte[BlockSize];
                Array.Copy(padded, i, block, 0, BlockSize);
                byte[] encrypted = EncryptBlock(block, key);
                Array.Copy(encrypted, 0, result, i, BlockSize);
            }
            return result;
        }

        /// <summary>
        /// 解密任意长度数据
        /// </summary>
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length % BlockSize != 0)
                throw new ArgumentException("Ciphertext length must be multiple of block size");

            // 分块解密
            byte[] decrypted = new byte[data.Length];
            for (int i = 0; i < data.Length; i += BlockSize)
            {
                byte[] block = new byte[BlockSize];
                Array.Copy(data, i, block, 0, BlockSize);
                byte[] decryptedBlock = DecryptBlock(block, key);
                Array.Copy(decryptedBlock, 0, decrypted, i, BlockSize);
            }

            // 移除 PKCS7 填充
            int padLen = decrypted[decrypted.Length - 1];
            if (padLen > BlockSize || padLen > decrypted.Length)
                throw new ArgumentException("Invalid padding");
            byte[] result = new byte[decrypted.Length - padLen];
            Array.Copy(decrypted, 0, result, 0, result.Length);
            return result;
        }

        /// <summary>
        /// 加密单个8字节块
        /// </summary>
        private static byte[] EncryptBlock(byte[] data, byte[] key)
        {
            uint[] v = new uint[2];
            uint[] k = new uint[4];

            v[0] = BitConverter.ToUInt32(data, 0);
            v[1] = BitConverter.ToUInt32(data, 4);

            for (int i = 0; i < 4; i++)
            {
                k[i] = BitConverter.ToUInt32(key, i * 4);
            }

            uint sum = 0;
            for (int i = 0; i < 32; i++)
            {
                sum += DELTA;
                v[0] += ((v[1] << 4) + k[0]) ^ (v[1] + sum) ^ ((v[1] >> 5) + k[1]);
                v[1] += ((v[0] << 4) + k[2]) ^ (v[0] + sum) ^ ((v[0] >> 5) + k[3]);
            }

            byte[] result = new byte[BlockSize];
            BitConverter.GetBytes(v[0]).CopyTo(result, 0);
            BitConverter.GetBytes(v[1]).CopyTo(result, 4);
            return result;
        }

        /// <summary>
        /// 解密单个8字节块
        /// </summary>
        private static byte[] DecryptBlock(byte[] data, byte[] key)
        {
            uint[] v = new uint[2];
            uint[] k = new uint[4];

            v[0] = BitConverter.ToUInt32(data, 0);
            v[1] = BitConverter.ToUInt32(data, 4);

            for (int i = 0; i < 4; i++)
            {
                k[i] = BitConverter.ToUInt32(key, i * 4);
            }

            uint sum = DELTA * 32;
            for (int i = 0; i < 32; i++)
            {
                v[1] -= ((v[0] << 4) + k[2]) ^ (v[0] + sum) ^ ((v[0] >> 5) + k[3]);
                v[0] -= ((v[1] << 4) + k[0]) ^ (v[1] + sum) ^ ((v[1] >> 5) + k[1]);
                sum -= DELTA;
            }

            byte[] result = new byte[BlockSize];
            BitConverter.GetBytes(v[0]).CopyTo(result, 0);
            BitConverter.GetBytes(v[1]).CopyTo(result, 4);
            return result;
        }
    }
}