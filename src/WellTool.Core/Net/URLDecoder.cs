using System;
using System.Text;
using WellTool.Core.Util;

namespace WellTool.Core.Net
{
    /// <summary>
    /// URL解码，数据内容的类型是 application/x-www-form-urlencoded。
    /// 
    /// <pre>
    /// 1. 将%20转换为空格 ;
    /// 2. 将"%xy"转换为文本形式,xy是两位16进制的数值;
    /// 3. 跳过不符合规范的%形式，直接输出
    /// </pre>
    /// </summary>
    public class URLDecoder
    {
        private const char ESCAPE_CHAR = '%';

        /// <summary>
        /// 解码，不对+解码
        /// 
        /// <ol>
        ///     <li>将%20转换为空格</li>
        ///     <li>将 "%xy"转换为文本形式,xy是两位16进制的数值</li>
        ///     <li>跳过不符合规范的%形式，直接输出</li>
        /// </ol>
        /// </summary>
        /// <param name="str">包含URL编码后的字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>解码后的字符串</returns>
        public static string DecodeForPath(string str, Encoding charset)
        {
            return Decode(str, charset, false);
        }

        /// <summary>
        /// 解码
        /// 规则见：https://url.spec.whatwg.org/#urlencoded-parsing
        /// <pre>
        ///   1. 将+和%20转换为空格(" ");
        ///   2. 将"%xy"转换为文本形式,xy是两位16进制的数值;
        ///   3. 跳过不符合规范的%形式，直接输出
        /// </pre>
        /// </summary>
        /// <param name="str">包含URL编码后的字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>解码后的字符串</returns>
        public static string Decode(string str, Encoding charset)
        {
            return Decode(str, charset, true);
        }

        /// <summary>
        /// 解码
        /// <pre>
        ///   1. 将%20转换为空格 ;
        ///   2. 将"%xy"转换为文本形式,xy是两位16进制的数值;
        ///   3. 跳过不符合规范的%形式，直接输出
        /// </pre>
        /// </summary>
        /// <param name="str">包含URL编码后的字符串</param>
        /// <param name="isPlusToSpace">是否+转换为空格</param>
        /// <param name="charset">编码，null表示不做编码</param>
        /// <returns>解码后的字符串</returns>
        public static string Decode(string str, Encoding charset, bool isPlusToSpace)
        {
            if (str == null || charset == null)
            {
                return str;
            }

            int length = str.Length;
            if (length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder(length / 3);

            int begin = 0;
            char c;
            for (int i = 0; i < length; i++)
            {
                c = str[i];
                if (ESCAPE_CHAR == c || CharUtil.IsHexChar(c))
                {
                    continue;
                }

                // 遇到非需要处理的字符跳过
                // 处理之前的hex字符
                if (i > begin)
                {
                    result.Append(DecodeSub(str, begin, i, charset, isPlusToSpace));
                }

                // 非Hex字符，忽略本字符
                if ('+' == c && isPlusToSpace)
                {
                    c = ' ';
                }

                // 非Hex字符，忽略本字符
                result.Append(c);
                begin = i + 1;
            }

            // 处理剩余字符
            if (begin < length)
            {
                result.Append(DecodeSub(str, begin, length, charset, isPlusToSpace));
            }

            return result.ToString();
        }

        /// <summary>
        /// 解码
        /// <pre>
        ///   1. 将+和%20转换为空格 ;
        ///   2. 将"%xy"转换为文本形式,xy是两位16进制的数值;
        ///   3. 跳过不符合规范的%形式，直接输出
        /// </pre>
        /// </summary>
        /// <param name="bytes">url编码的bytes</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(byte[] bytes)
        {
            return Decode(bytes, true);
        }

        /// <summary>
        /// 解码
        /// <pre>
        ///   1. 将%20转换为空格 ;
        ///   2. 将"%xy"转换为文本形式,xy是两位16进制的数值;
        ///   3. 跳过不符合规范的%形式，直接输出
        /// </pre>
        /// </summary>
        /// <param name="bytes">url编码的bytes</param>
        /// <param name="isPlusToSpace">是否+转换为空格</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(byte[] bytes, bool isPlusToSpace)
        {
            if (bytes == null)
            {
                return null;
            }
            using (var buffer = new System.IO.MemoryStream(bytes.Length))
            {
                int b;
                for (int i = 0; i < bytes.Length; i++)
                {
                    b = bytes[i];
                    if (b == '+')
                    {
                        buffer.WriteByte((byte)(isPlusToSpace ? ' ' : b));
                    }
                    else if (b == ESCAPE_CHAR)
                    {
                        if (i + 1 < bytes.Length)
                        {
                            int u = CharUtil.Digit16((char)bytes[i + 1]);
                            if (u >= 0 && i + 2 < bytes.Length)
                            {
                                int l = CharUtil.Digit16((char)bytes[i + 2]);
                                if (l >= 0)
                                {
                                    buffer.WriteByte((byte)((u << 4) + l));
                                    i += 2;
                                    continue;
                                }
                            }
                        }
                        // 跳过不符合规范的%形式
                        buffer.WriteByte((byte)b);
                    }
                    else
                    {
                        buffer.WriteByte((byte)b);
                    }
                }
                return buffer.ToArray();
            }
        }

        /// <summary>
        /// 解码子串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="begin">开始位置（包含）</param>
        /// <param name="end">结束位置（不包含）</param>
        /// <param name="charset">编码</param>
        /// <param name="isPlusToSpace">是否+转换为空格</param>
        /// <returns>解码后的字符串</returns>
        private static string DecodeSub(string str, int begin, int end, Encoding charset, bool isPlusToSpace)
        {
            byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(str.Substring(begin, end - begin));
            byte[] decodedBytes = Decode(bytes, isPlusToSpace);
            return charset.GetString(decodedBytes);
        }
    }
}