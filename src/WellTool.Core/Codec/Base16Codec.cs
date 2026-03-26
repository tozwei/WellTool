using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base16（Hex）编码解码器
    /// 十六进制（简写为hex或下标16）在数学中是一种逢16进1的进位制，一般用数字0到9和字母A到F表示（其中:A~F即10~15）。
    /// 例如十进制数57，在二进制写作111001，在16进制写作39。
    /// </summary>
    public class Base16Codec : Encoder<byte[], char[]>, Decoder<string, byte[]>
    {
        public static readonly Base16Codec CODEC_LOWER = new Base16Codec(true);
        public static readonly Base16Codec CODEC_UPPER = new Base16Codec(false);

        private readonly char[] _alphabets;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="lowerCase">是否小写</param>
        public Base16Codec(bool lowerCase)
        {
            _alphabets = (lowerCase ? "0123456789abcdef" : "0123456789ABCDEF").ToCharArray();
        }

        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">被编码的数据</param>
        /// <returns>编码后的数据</returns>
        public char[] Encode(byte[] data)
        {
            int len = data.Length;
            char[] output = new char[len << 1]; // len*2
            
            // two characters from the hex value.
            for (int i = 0, j = 0; i < len; i++)
            {
                output[j++] = _alphabets[(0xF0 & data[i]) >> 4]; // 高位
                output[j++] = _alphabets[0x0F & data[i]]; // 低位
            }
            return output;
        }

        /// <summary>
        /// 执行解码
        /// </summary>
        /// <param name="encoded">被解码的数据</param>
        /// <returns>解码后的数据</returns>
        public byte[] Decode(string encoded)
        {
            if (string.IsNullOrEmpty(encoded))
            {
                return null;
            }

            encoded = encoded.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
            int len = encoded.Length;

            if ((len & 0x01) != 0)
            {
                // 如果提供的数据是奇数长度，则前面补0凑偶数
                encoded = "0" + encoded;
                len = encoded.Length;
            }

            byte[] output = new byte[len >> 1];

            // two characters form the hex value.
            for (int i = 0, j = 0; j < len; i++)
            {
                int f = ToDigit(encoded[j], j) << 4;
                j++;
                f = f | ToDigit(encoded[j], j);
                j++;
                output[i] = (byte)(f & 0xFF);
            }

            return output;
        }

        /// <summary>
        /// 将指定char值转换为Unicode字符串形式，常用于特殊字符（例如汉字）转Unicode形式
        /// 转换的字符串如果u后不足4位，则前面用0填充，例如：
        /// <pre>
        /// '你' =》'\u4f60'
        /// </pre>
        /// </summary>
        /// <param name="ch">char值</param>
        /// <returns>Unicode表现形式</returns>
        public string ToUnicodeHex(char ch)
        {
            return "\\u" +
                _alphabets[(ch >> 12) & 15] +
                _alphabets[(ch >> 8) & 15] +
                _alphabets[(ch >> 4) & 15] +
                _alphabets[(ch) & 15];
        }

        /// <summary>
        /// 将byte值转为16进制并添加到{@link StringBuilder}中
        /// </summary>
        /// <param name="builder">{@link StringBuilder}</param>
        /// <param name="b">byte</param>
        public void AppendHex(StringBuilder builder, byte b)
        {
            int high = (b & 0xf0) >> 4; // 高位
            int low = b & 0x0f; // 低位
            builder.Append(_alphabets[high]);
            builder.Append(_alphabets[low]);
        }

        /// <summary>
        /// 将十六进制字符转换成一个整数
        /// </summary>
        /// <param name="ch">十六进制char</param>
        /// <param name="index">十六进制字符在字符数组中的位置</param>
        /// <returns>一个整数</returns>
        /// <exception cref="ArgumentException">当ch不是一个合法的十六进制字符时，抛出异常</exception>
        private static int ToDigit(char ch, int index)
        {
            int digit = System.Convert.ToInt32(ch.ToString(), 16);
            if (digit < 0)
            {
                throw new ArgumentException($"Illegal hexadecimal character {ch} at index {index}");
            }
            return digit;
        }
    }
}