using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base58编码器
    /// 此编码器不包括校验码、版本等信息
    /// </summary>
    public class Base58Codec : Encoder<byte[], string>, Decoder<string, byte[]>
    {
        public static Base58Codec INSTANCE = new Base58Codec();

        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">被编码的数据</param>
        /// <returns>编码后的数据</returns>
        public string Encode(byte[] data)
        {
            return Base58Encoder.ENCODER.Encode(data);
        }

        /// <summary>
        /// 执行解码
        /// </summary>
        /// <param name="encoded">被解码的数据</param>
        /// <returns>解码后的数据</returns>
        public byte[] Decode(string encoded)
        {
            return Base58Decoder.DECODER.Decode(encoded);
        }

        /// <summary>
        /// Base58编码器
        /// </summary>
        public class Base58Encoder : Encoder<byte[], string>
        {
            public const string DEFAULT_ALPHABET = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

            public static readonly Base58Encoder ENCODER = new Base58Encoder(DEFAULT_ALPHABET.ToCharArray());

            private readonly char[] _alphabet;
            private readonly char _alphabetZero;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="alphabet">编码字母表</param>
            public Base58Encoder(char[] alphabet)
            {
                _alphabet = alphabet;
                _alphabetZero = alphabet[0];
            }

            /// <summary>
            /// 执行编码
            /// </summary>
            /// <param name="data">被编码的数据</param>
            /// <returns>编码后的数据</returns>
            public string Encode(byte[] data)
            {
                if (data == null)
                {
                    return null;
                }
                if (data.Length == 0)
                {
                    return string.Empty;
                }
                
                // 计算开头0的个数
                int zeroCount = 0;
                while (zeroCount < data.Length && data[zeroCount] == 0)
                {
                    ++zeroCount;
                }
                
                // 将256位编码转换为58位编码
                byte[] copyData = new byte[data.Length];
                Array.Copy(data, copyData, data.Length); // since we modify it in-place
                
                char[] encoded = new char[data.Length * 2]; // upper bound
                int outputStart = encoded.Length;
                
                for (int inputStart = zeroCount; inputStart < copyData.Length; )
                {
                    encoded[--outputStart] = _alphabet[Divmod(copyData, inputStart, 256, 58)];
                    if (copyData[inputStart] == 0)
                    {
                        ++inputStart; // optimization - skip leading zeros
                    }
                }
                
                // Preserve exactly as many leading encoded zeros in output as there were leading zeros in input.
                while (outputStart < encoded.Length && encoded[outputStart] == _alphabetZero)
                {
                    ++outputStart;
                }
                
                while (--zeroCount >= 0)
                {
                    encoded[--outputStart] = _alphabetZero;
                }
                
                // Return encoded string (including encoded leading zeros).
                return new string(encoded, outputStart, encoded.Length - outputStart);
            }
        }

        /// <summary>
        /// Base58解码器
        /// </summary>
        public class Base58Decoder : Decoder<string, byte[]>
        {
            public static readonly Base58Decoder DECODER = new Base58Decoder(Base58Encoder.DEFAULT_ALPHABET);

            private readonly byte[] _lookupTable;

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="alphabet">编码字符表</param>
            public Base58Decoder(string alphabet)
            {
                byte[] lookupTable = new byte['z' + 1];
                for (int i = 0; i < lookupTable.Length; i++)
                {
                    lookupTable[i] = 255;
                }

                int length = alphabet.Length;
                for (int i = 0; i < length; i++)
                {
                    lookupTable[alphabet[i]] = (byte)i;
                }
                _lookupTable = lookupTable;
            }

            /// <summary>
            /// 执行解码
            /// </summary>
            /// <param name="encoded">被解码的数据</param>
            /// <returns>解码后的数据</returns>
            public byte[] Decode(string encoded)
            {
                if (encoded.Length == 0)
                {
                    return new byte[0];
                }
                
                // Convert the base58-encoded ASCII chars to a base58 byte sequence (base58 digits).
                byte[] input58 = new byte[encoded.Length];
                for (int i = 0; i < encoded.Length; ++i)
                {
                    char c = encoded[i];
                    int digit = c < 128 ? _lookupTable[c] : 255;
                    if (digit == 255)
                    {
                        throw new ArgumentException($"Invalid char '{c}' at [{i}]");
                    }
                    input58[i] = (byte)digit;
                }
                
                // Count leading zeros.
                int zeros = 0;
                while (zeros < input58.Length && input58[zeros] == 0)
                {
                    ++zeros;
                }
                
                // Convert base-58 digits to base-256 digits.
                byte[] decoded = new byte[encoded.Length];
                int outputStart = decoded.Length;
                
                for (int inputStart = zeros; inputStart < input58.Length; )
                {
                    decoded[--outputStart] = Divmod(input58, inputStart, 58, 256);
                    if (input58[inputStart] == 0)
                    {
                        ++inputStart; // optimization - skip leading zeros
                    }
                }
                
                // Ignore extra leading zeroes that were added during the calculation.
                while (outputStart < decoded.Length && decoded[outputStart] == 0)
                {
                    ++outputStart;
                }
                
                // Return decoded data (including original number of leading zeros).
                int resultLength = decoded.Length - (outputStart - zeros);
                byte[] result = new byte[resultLength];
                Array.Copy(decoded, outputStart - zeros, result, 0, resultLength);
                return result;
            }
        }

        /// <summary>
        /// Divides a number, represented as an array of bytes each containing a single digit
        /// in the specified base, by the given divisor. The given number is modified in-place
        /// to contain the quotient, and the return value is the remainder.
        /// </summary>
        /// <param name="number">the number to divide</param>
        /// <param name="firstDigit">the index within the array of the first non-zero digit
        /// (this is used for optimization by skipping the leading zeros)</param>
        /// <param name="baseValue">the base in which the number's digits are represented (up to 256)</param>
        /// <param name="divisor">the number to divide by (up to 256)</param>
        /// <returns>the remainder of the division operation</returns>
        private static byte Divmod(byte[] number, int firstDigit, int baseValue, int divisor)
        {
            // this is just long division which accounts for the base of the input digits
            int remainder = 0;
            for (int i = firstDigit; i < number.Length; i++)
            {
                int digit = (int)number[i] & 0xFF;
                int temp = remainder * baseValue + digit;
                number[i] = (byte)(temp / divisor);
                remainder = temp % divisor;
            }
            return (byte)remainder;
        }
    }
}