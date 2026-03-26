using System;
using System.Security.Cryptography;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base58工具类，提供Base58的编码和解码方案
    /// 规范见：https://en.bitcoin.it/wiki/Base58Check_encoding
    /// </summary>
    public static class Base58
    {
        private const int CHECKSUM_SIZE = 4;

        // -------------------------------------------------------------------- encode

        /// <summary>
        /// Base58编码
        /// 包含版本位和校验位
        /// </summary>
        /// <param name="version">编码版本，{@code null}表示不包含版本位</param>
        /// <param name="data">被编码的数组，添加校验和。</param>
        /// <returns>编码后的字符串</returns>
        public static string EncodeChecked(int? version, byte[] data)
        {
            return Encode(AddChecksum(version, data));
        }

        /// <summary>
        /// Base58编码
        /// </summary>
        /// <param name="data">被编码的数据，不带校验和。</param>
        /// <returns>编码后的字符串</returns>
        public static string Encode(byte[] data)
        {
            return Base58Codec.INSTANCE.Encode(data);
        }
        // -------------------------------------------------------------------- decode

        /// <summary>
        /// Base58解码
        /// 解码包含标志位验证和版本呢位去除
        /// </summary>
        /// <param name="encoded">被解码的base58字符串</param>
        /// <returns>解码后的bytes</returns>
        /// <exception cref="ArgumentException">标志位验证错误抛出此异常</exception>
        public static byte[] DecodeChecked(string encoded)
        {
            try
            {
                return DecodeChecked(encoded, true);
            }
            catch (ArgumentException)
            {
                return DecodeChecked(encoded, false);
            }
        }

        /// <summary>
        /// Base58解码
        /// 解码包含标志位验证和版本呢位去除
        /// </summary>
        /// <param name="encoded">被解码的base58字符串</param>
        /// <param name="withVersion">是否包含版本位</param>
        /// <returns>解码后的bytes</returns>
        /// <exception cref="ArgumentException">标志位验证错误抛出此异常</exception>
        public static byte[] DecodeChecked(string encoded, bool withVersion)
        {
            byte[] valueWithChecksum = Decode(encoded);
            return VerifyAndRemoveChecksum(valueWithChecksum, withVersion);
        }

        /// <summary>
        /// Base58解码
        /// </summary>
        /// <param name="encoded">被编码的base58字符串</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(string encoded)
        {
            return Base58Codec.INSTANCE.Decode(encoded);
        }

        /// <summary>
        /// 验证并去除验证位和版本位
        /// </summary>
        /// <param name="data">编码的数据</param>
        /// <param name="withVersion">是否包含版本位</param>
        /// <returns>载荷数据</returns>
        private static byte[] VerifyAndRemoveChecksum(byte[] data, bool withVersion)
        {
            int startIndex = withVersion ? 1 : 0;
            byte[] payload = new byte[data.Length - startIndex - CHECKSUM_SIZE];
            Array.Copy(data, startIndex, payload, 0, payload.Length);
            
            byte[] checksum = new byte[CHECKSUM_SIZE];
            Array.Copy(data, data.Length - CHECKSUM_SIZE, checksum, 0, CHECKSUM_SIZE);
            
            byte[] expectedChecksum = Checksum(payload);
            if (!ArraysEqual(checksum, expectedChecksum))
            {
                throw new ArgumentException("Base58 checksum is invalid");
            }
            return payload;
        }

        /// <summary>
        /// 数据 + 校验码
        /// </summary>
        /// <param name="version">版本，{@code null}表示不添加版本位</param>
        /// <param name="payload">Base58数据（不含校验码）</param>
        /// <returns>Base58数据</returns>
        private static byte[] AddChecksum(int? version, byte[] payload)
        {
            byte[] addressBytes;
            if (version.HasValue)
            {
                addressBytes = new byte[1 + payload.Length + CHECKSUM_SIZE];
                addressBytes[0] = (byte)version.Value;
                Array.Copy(payload, 0, addressBytes, 1, payload.Length);
            }
            else
            {
                addressBytes = new byte[payload.Length + CHECKSUM_SIZE];
                Array.Copy(payload, 0, addressBytes, 0, payload.Length);
            }
            byte[] checksum = Checksum(payload);
            Array.Copy(checksum, 0, addressBytes, addressBytes.Length - CHECKSUM_SIZE, CHECKSUM_SIZE);
            return addressBytes;
        }

        /// <summary>
        /// 获取校验码
        /// 计算规则为对数据进行两次sha256计算，然后取{@link #CHECKSUM_SIZE}长度
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>校验码</returns>
        private static byte[] Checksum(byte[] data)
        {
            byte[] hash = Hash256(Hash256(data));
            byte[] result = new byte[CHECKSUM_SIZE];
            Array.Copy(hash, 0, result, 0, CHECKSUM_SIZE);
            return result;
        }

        /// <summary>
        /// 计算数据的SHA-256值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>sha-256值</returns>
        private static byte[] Hash256(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }

        /// <summary>
        /// 比较两个字节数组是否相等
        /// </summary>
        /// <param name="a">第一个字节数组</param>
        /// <param name="b">第二个字节数组</param>
        /// <returns>是否相等</returns>
        private static bool ArraysEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}