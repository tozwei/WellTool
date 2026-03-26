using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base32 - encodes and decodes RFC4648 Base32
    /// base32就是用32（2的5次方）个特定ASCII码来表示256个ASCII码。
    /// 所以，5个ASCII字符经过base32编码后会变为8个字符（公约数为40），长度增加3/5.不足8n用"="补足。
    /// 根据RFC4648 Base32规范，支持两种模式：
    /// <ul>
    ///     <li>Base 32 Alphabet                 (ABCDEFGHIJKLMNOPQRSTUVWXYZ234567)</li>
    ///     <li>"Extended Hex" Base 32 Alphabet  (0123456789ABCDEFGHIJKLMNOPQRSTUV)</li>
    /// </ul>
    /// </summary>
    public class Base32Codec : Encoder<byte[], string>, Decoder<string, byte[]>
    {
        public static Base32Codec INSTANCE = new Base32Codec();

        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">被编码的数据</param>
        /// <returns>编码后的数据</returns>
        public string Encode(byte[] data)
        {
            return Encode(data, false);
        }

        /// <summary>
        /// 编码数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="useHex">是否使用Hex Alphabet</param>
        /// <returns>编码后的Base32字符串</returns>
        public string Encode(byte[] data, bool useHex)
        {
            Base32Encoder encoder = useHex ? Base32Encoder.HEX_ENCODER : Base32Encoder.ENCODER;
            return encoder.Encode(data);
        }

        /// <summary>
        /// 执行解码
        /// </summary>
        /// <param name="encoded">被解码的数据</param>
        /// <returns>解码后的数据</returns>
        public byte[] Decode(string encoded)
        {
            return Decode(encoded, false);
        }

        /// <summary>
        /// 解码数据
        /// </summary>
        /// <param name="encoded">base32字符串</param>
        /// <param name="useHex">是否使用Hex Alphabet</param>
        /// <returns>解码后的内容</returns>
        public byte[] Decode(string encoded, bool useHex)
        {
            Base32Decoder decoder = useHex ? Base32Decoder.HEX_DECODER : Base32Decoder.DECODER;
            return decoder.Decode(encoded);
        }

        /// <summary>
        /// Bas32编码器
        /// </summary>
        public class Base32Encoder : Encoder<byte[], string>
        {
            public const string DEFAULT_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            public const string HEX_ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
            private const char DEFAULT_PAD = '=';
            private static readonly int[] BASE32_FILL = { -1, 4, 1, 6, 3 };

            public static readonly Base32Encoder ENCODER = new Base32Encoder(DEFAULT_ALPHABET, DEFAULT_PAD);
            public static readonly Base32Encoder HEX_ENCODER = new Base32Encoder(HEX_ALPHABET, DEFAULT_PAD);

            private readonly char[] _alphabet;
            private readonly char _pad;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="alphabet">自定义编码字母表，见 {@link #DEFAULT_ALPHABET}和 {@link #HEX_ALPHABET}</param>
            /// <param name="pad">补位字符</param>
            public Base32Encoder(string alphabet, char pad)
            {
                _alphabet = alphabet.ToCharArray();
                _pad = pad;
            }

            /// <summary>
            /// 执行编码
            /// </summary>
            /// <param name="data">被编码的数据</param>
            /// <returns>编码后的数据</returns>
            public string Encode(byte[] data)
            {
                int i = 0;
                int index = 0;
                int digit;
                int currByte;
                int nextByte;

                int encodeLen = data.Length * 8 / 5;
                if (encodeLen != 0)
                {
                    encodeLen = encodeLen + 1 + BASE32_FILL[(data.Length * 8) % 5];
                }

                StringBuilder base32 = new StringBuilder(encodeLen);

                while (i < data.Length)
                {
                    // unsign
                    currByte = data[i] >= 0 ? data[i] : (data[i] + 256);

                    /* Is the current digit going to span a byte boundary? */
                    if (index > 3)
                    {
                        if ((i + 1) < data.Length)
                        {
                            nextByte = data[i + 1] >= 0 ? data[i + 1] : (data[i + 1] + 256);
                        }
                        else
                        {
                            nextByte = 0;
                        }

                        digit = currByte & (0xFF >> index);
                        index = (index + 5) % 8;
                        digit <<= index;
                        digit |= nextByte >> (8 - index);
                        i++;
                    }
                    else
                    {
                        digit = (currByte >> (8 - (index + 5))) & 0x1F;
                        index = (index + 5) % 8;
                        if (index == 0)
                        {
                            i++;
                        }
                    }
                    base32.Append(_alphabet[digit]);
                }

                if (_pad != '\0')
                {
                    // 末尾补充不足长度的
                    while (base32.Length < encodeLen)
                    {
                        base32.Append(_pad);
                    }
                }

                return base32.ToString();
            }
        }

        /// <summary>
        /// Base32解码器
        /// </summary>
        public class Base32Decoder : Decoder<string, byte[]>
        {
            private const char BASE_CHAR = '0';

            public static readonly Base32Decoder DECODER = new Base32Decoder(Base32Encoder.DEFAULT_ALPHABET);
            public static readonly Base32Decoder HEX_DECODER = new Base32Decoder(Base32Encoder.HEX_ALPHABET);

            private readonly byte[] _lookupTable;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="alphabet">编码字母表</param>
            public Base32Decoder(string alphabet)
            {
                _lookupTable = new byte[128];
                for (int i = 0; i < _lookupTable.Length; i++)
                {
                    _lookupTable[i] = 255;
                }

                int length = alphabet.Length;

                char c;
                for (int i = 0; i < length; i++)
                {
                    c = alphabet[i];
                    _lookupTable[c - BASE_CHAR] = (byte)i;
                    // 支持小写字母解码
                    if (c >= 'A' && c <= 'Z')
                    {
                        _lookupTable[char.ToLower(c) - BASE_CHAR] = (byte)i;
                    }
                }
            }

            /// <summary>
            /// 执行解码
            /// </summary>
            /// <param name="encoded">被解码的数据</param>
            /// <returns>解码后的数据</returns>
            public byte[] Decode(string encoded)
            {
                int i, index, lookup, offset, digit;
                string base32 = encoded;
                int len = base32.EndsWith("=") ? base32.IndexOf("=") * 5 / 8 : base32.Length * 5 / 8;
                byte[] bytes = new byte[len];

                for (i = 0, index = 0, offset = 0; i < base32.Length; i++)
                {
                    lookup = base32[i] - BASE_CHAR;

                    /* Skip chars outside the lookup table */
                    if (lookup < 0 || lookup >= _lookupTable.Length)
                    {
                        continue;
                    }

                    digit = _lookupTable[lookup];

                    /* If this digit is not in the table, ignore it */
                    if (digit == 255)
                    {
                        continue;
                    }

                    if (index <= 3)
                    {
                        index = (index + 5) % 8;
                        if (index == 0)
                        {
                            bytes[offset] |= (byte)digit;
                            offset++;
                            if (offset >= bytes.Length)
                            {
                                break;
                            }
                        }
                        else
                        {
                            bytes[offset] |= (byte)(digit << (8 - index));
                        }
                    }
                    else
                    {
                        index = (index + 5) % 8;
                        bytes[offset] |= (byte)(digit >> index);
                        offset++;

                        if (offset >= bytes.Length)
                        {
                            break;
                        }
                        bytes[offset] |= (byte)(digit << (8 - index));
                    }
                }
                return bytes;
            }
        }
    }
}