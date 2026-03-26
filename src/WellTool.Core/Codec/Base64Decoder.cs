using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base64解码实现
    /// </summary>
    public static class Base64Decoder
    {
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
        private const byte PADDING = 254;

        /// <summary>Base64解码表，共128位，255表示非base64字符，254表示padding</summary>
        private static readonly byte[] DECODE_TABLE = {
            // 0 1 2 3 4 5 6 7 8 9 A B C D E F
            255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, // 00-0f
            255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, // 10-1f
            255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 62, 255, 62, 255, 63, // 20-2f + - /
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 255, 255, 255, 254, 255, 255, // 30-3f 0-9，254的位置是'='
            255, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, // 40-4f A-O
            15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 255, 255, 255, 255, 63, // 50-5f P-Z _
            255, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, // 60-6f a-o
            41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51 // 70-7a p-z
        };

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStr(string source)
        {
            return DecodeStr(source, DEFAULT_ENCODING);
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStr(string source, Encoding encoding)
        {
            return encoding.GetString(Decode(source));
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static byte[] Decode(string source)
        {
            return Decode(DEFAULT_ENCODING.GetBytes(source));
        }

        /// <summary>
        /// 解码Base64
        /// </summary>
        /// <param name="inBytes">输入</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(byte[] inBytes)
        {
            if (inBytes == null || inBytes.Length == 0)
            {
                return inBytes;
            }
            return Decode(inBytes, 0, inBytes.Length);
        }

        /// <summary>
        /// 解码Base64
        /// </summary>
        /// <param name="inBytes">输入</param>
        /// <param name="pos">开始位置</param>
        /// <param name="length">长度</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(byte[] inBytes, int pos, int length)
        {
            if (inBytes == null || inBytes.Length == 0)
            {
                return inBytes;
            }

            int offset = pos;

            byte sestet0;
            byte sestet1;
            byte sestet2;
            byte sestet3;
            int maxPos = pos + length - 1;
            int octetId = 0;
            byte[] octet = new byte[length * 3 / 4]; // over-estimated if non-base64 characters present
            while (offset <= maxPos)
            {
                sestet0 = GetNextValidDecodeByte(inBytes, ref offset, maxPos);
                sestet1 = GetNextValidDecodeByte(inBytes, ref offset, maxPos);
                sestet2 = GetNextValidDecodeByte(inBytes, ref offset, maxPos);
                sestet3 = GetNextValidDecodeByte(inBytes, ref offset, maxPos);

                if (PADDING != sestet1)
                {
                    octet[octetId++] = (byte)((sestet0 << 2) | (sestet1 >> 4));
                }
                if (PADDING != sestet2)
                {
                    octet[octetId++] = (byte)(((sestet1 & 0xf) << 4) | (sestet2 >> 2));
                }
                if (PADDING != sestet3)
                {
                    octet[octetId++] = (byte)(((sestet2 & 3) << 6) | sestet3);
                }
            }

            if (octetId == octet.Length)
            {
                return octet;
            }
            else
            {
                // 如果有非Base64字符混入，则实际结果比解析的要短，截取之
                byte[] result = new byte[octetId];
                Array.Copy(octet, result, octetId);
                return result;
            }
        }

        /// <summary>
        /// 给定的字符是否为Base64字符
        /// </summary>
        /// <param name="octet">被检查的字符</param>
        /// <returns>是否为Base64字符</returns>
        public static bool IsBase64Code(byte octet)
        {
            return octet == '=' || (octet >= 0 && octet < DECODE_TABLE.Length && DECODE_TABLE[octet] != 255);
        }

        // ----------------------------------------------------------------------------------------------- Private start
        /// <summary>
        /// 获取下一个有效的byte字符
        /// </summary>
        /// <param name="inBytes">输入</param>
        /// <param name="pos">当前位置，调用此方法后此位置保持在有效字符的下一个位置</param>
        /// <param name="maxPos">最大位置</param>
        /// <returns>有效字符，如果达到末尾返回</returns>
        private static byte GetNextValidDecodeByte(byte[] inBytes, ref int pos, int maxPos)
        {
            byte base64Byte;
            byte decodeByte;
            while (pos <= maxPos)
            {
                base64Byte = inBytes[pos];
                pos++;
                if (base64Byte >= 0 && base64Byte < DECODE_TABLE.Length)
                {
                    decodeByte = DECODE_TABLE[base64Byte];
                    if (decodeByte != 255)
                    {
                        return decodeByte;
                    }
                }
            }
            // padding if reached max position
            return PADDING;
        }
        // ----------------------------------------------------------------------------------------------- Private end
    }
}