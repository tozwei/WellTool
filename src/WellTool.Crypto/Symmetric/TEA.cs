using System;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// TEA 加密算法
    /// </summary>
    public class TEA
    {
        private static readonly uint DELTA = 0x9E3779B9;

        private readonly byte[] _key;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">密钥（16字节）</param>
        public TEA(byte[] key)
        {
            _key = key;
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
        /// <param name="data">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            uint[] v = new uint[2];
            uint[] k = new uint[4];

            // 将明文转换为 uint 数组
            v[0] = BitConverter.ToUInt32(data, 0);
            v[1] = BitConverter.ToUInt32(data, 4);

            // 将密钥转换为 uint 数组
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

            // 将 uint 数组转换为 byte 数组
            byte[] result = new byte[8];
            BitConverter.GetBytes(v[0]).CopyTo(result, 0);
            BitConverter.GetBytes(v[1]).CopyTo(result, 4);

            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            uint[] v = new uint[2];
            uint[] k = new uint[4];

            // 将密文转换为 uint 数组
            v[0] = BitConverter.ToUInt32(data, 0);
            v[1] = BitConverter.ToUInt32(data, 4);

            // 将密钥转换为 uint 数组
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

            // 将 uint 数组转换为 byte 数组
            byte[] result = new byte[8];
            BitConverter.GetBytes(v[0]).CopyTo(result, 0);
            BitConverter.GetBytes(v[1]).CopyTo(result, 4);

            return result;
        }
    }
}