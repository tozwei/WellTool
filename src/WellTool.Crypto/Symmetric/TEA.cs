using System;

namespace WellTool.Crypto.Symmetric
{
    /// <summary>
    /// TEA 加密算法
    /// </summary>
    public class TEA
    {
        private static readonly uint DELTA = 0x9E3779B9;

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