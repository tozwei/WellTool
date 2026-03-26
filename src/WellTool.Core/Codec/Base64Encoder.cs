using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base64编码
    /// </summary>
    public static class Base64Encoder
    {
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
        
        /// <summary>
        /// 标准编码表
        /// </summary>
        private static readonly byte[] STANDARD_ENCODE_TABLE = {
            (byte)'A', (byte)'B', (byte)'C', (byte)'D', (byte)'E', (byte)'F', (byte)'G', (byte)'H',
            (byte)'I', (byte)'J', (byte)'K', (byte)'L', (byte)'M', (byte)'N', (byte)'O', (byte)'P',
            (byte)'Q', (byte)'R', (byte)'S', (byte)'T', (byte)'U', (byte)'V', (byte)'W', (byte)'X',
            (byte)'Y', (byte)'Z', (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f',
            (byte)'g', (byte)'h', (byte)'i', (byte)'j', (byte)'k', (byte)'l', (byte)'m', (byte)'n',
            (byte)'o', (byte)'p', (byte)'q', (byte)'r', (byte)'s', (byte)'t', (byte)'u', (byte)'v',
            (byte)'w', (byte)'x', (byte)'y', (byte)'z', (byte)'0', (byte)'1', (byte)'2', (byte)'3',
            (byte)'4', (byte)'5', (byte)'6', (byte)'7', (byte)'8', (byte)'9', (byte)'+', (byte)'/'
        };
        
        /// <summary>
        /// URL安全的编码表，将 + 和 / 替换为 - 和 _
        /// </summary>
        private static readonly byte[] URL_SAFE_ENCODE_TABLE = {
            (byte)'A', (byte)'B', (byte)'C', (byte)'D', (byte)'E', (byte)'F', (byte)'G', (byte)'H',
            (byte)'I', (byte)'J', (byte)'K', (byte)'L', (byte)'M', (byte)'N', (byte)'O', (byte)'P',
            (byte)'Q', (byte)'R', (byte)'S', (byte)'T', (byte)'U', (byte)'V', (byte)'W', (byte)'X',
            (byte)'Y', (byte)'Z', (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f',
            (byte)'g', (byte)'h', (byte)'i', (byte)'j', (byte)'k', (byte)'l', (byte)'m', (byte)'n',
            (byte)'o', (byte)'p', (byte)'q', (byte)'r', (byte)'s', (byte)'t', (byte)'u', (byte)'v',
            (byte)'w', (byte)'x', (byte)'y', (byte)'z', (byte)'0', (byte)'1', (byte)'2', (byte)'3',
            (byte)'4', (byte)'5', (byte)'6', (byte)'7', (byte)'8', (byte)'9', (byte)'-', (byte)'_'
        };

        // -------------------------------------------------------------------- encode

        /// <summary>
        /// 编码为Base64，非URL安全的
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="lineSep">在76个char之后是CRLF还是EOF</param>
        /// <returns>编码后的bytes</returns>
        public static byte[] Encode(byte[] arr, bool lineSep)
        {
            return Encode(arr, lineSep, false);
        }

        /// <summary>
        /// 编码为Base64，URL安全的
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="lineSep">在76个char之后是CRLF还是EOF</param>
        /// <returns>编码后的bytes</returns>
        public static byte[] EncodeUrlSafe(byte[] arr, bool lineSep)
        {
            return Encode(arr, lineSep, true);
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(string source)
        {
            return Encode(source, DEFAULT_ENCODING);
        }

        /// <summary>
        /// base64编码，URL安全
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(string source)
        {
            return EncodeUrlSafe(source, DEFAULT_ENCODING);
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(string source, Encoding encoding)
        {
            return Encode(encoding.GetBytes(source));
        }

        /// <summary>
        /// base64编码，URL安全的
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(string source, Encoding encoding)
        {
            return EncodeUrlSafe(encoding.GetBytes(source));
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(byte[] source)
        {
            return Encoding.UTF8.GetString(Encode(source, false));
        }

        /// <summary>
        /// base64编码,URL安全的
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(byte[] source)
        {
            return Encoding.UTF8.GetString(EncodeUrlSafe(source, false));
        }

        /// <summary>
        /// 编码为Base64字符串
        /// 如果isMultiLine为{@code true}，则每76个字符一个换行符，否则在一行显示
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="isMultiLine">在76个char之后是CRLF还是EOF</param>
        /// <param name="isUrlSafe">是否使用URL安全字符，在URL Safe模式下，=为URL中的关键字符，不需要补充。空余的byte位要去掉，一般为{@code false}</param>
        /// <returns>编码后的bytes</returns>
        public static string EncodeStr(byte[] arr, bool isMultiLine, bool isUrlSafe)
        {
            return Encoding.UTF8.GetString(Encode(arr, isMultiLine, isUrlSafe));
        }

        /// <summary>
        /// 编码为Base64
        /// 如果isMultiLine为{@code true}，则每76个字符一个换行符，否则在一行显示
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="isMultiLine">在76个char之后是CRLF还是EOF</param>
        /// <param name="isUrlSafe">是否使用URL安全字符，在URL Safe模式下，=为URL中的关键字符，不需要补充。空余的byte位要去掉，一般为{@code false}</param>
        /// <returns>编码后的bytes</returns>
        public static byte[] Encode(byte[] arr, bool isMultiLine, bool isUrlSafe)
        {
            if (arr == null)
            {
                return null;
            }

            int len = arr.Length;
            if (len == 0)
            {
                return new byte[0];
            }

            int evenlen = (len / 3) * 3;
            int cnt = ((len - 1) / 3 + 1) << 2;
            int destlen = cnt + (isMultiLine ? (cnt - 1) / 76 * 2 : 0);
            byte[] dest = new byte[destlen];

            byte[] encodeTable = isUrlSafe ? URL_SAFE_ENCODE_TABLE : STANDARD_ENCODE_TABLE;

            for (int s = 0, d = 0, cc = 0; s < evenlen; )
            {
                int i = (arr[s++] & 0xff) << 16 | (arr[s++] & 0xff) << 8 | (arr[s++] & 0xff);

                dest[d++] = encodeTable[(i >> 18) & 0x3f];
                dest[d++] = encodeTable[(i >> 12) & 0x3f];
                dest[d++] = encodeTable[(i >> 6) & 0x3f];
                dest[d++] = encodeTable[i & 0x3f];

                if (isMultiLine && ++cc == 19 && d < destlen - 2)
                {
                    dest[d++] = (byte) '\r';
                    dest[d++] = (byte) '\n';
                    cc = 0;
                }
            }

            int left = len - evenlen; // 剩余位数
            if (left > 0)
            {
                int i = ((arr[evenlen] & 0xff) << 10) | (left == 2 ? ((arr[len - 1] & 0xff) << 2) : 0);

                dest[destlen - 4] = encodeTable[i >> 12];
                dest[destlen - 3] = encodeTable[(i >> 6) & 0x3f];

                if (isUrlSafe)
                {
                    // 在URL Safe模式下，=为URL中的关键字符，不需要补充。空余的byte位要去掉。
                    int urlSafeLen = destlen - 2;
                    if (2 == left)
                    {
                        dest[destlen - 2] = encodeTable[i & 0x3f];
                        urlSafeLen += 1;
                    }
                    byte[] urlSafeDest = new byte[urlSafeLen];
                    Array.Copy(dest, 0, urlSafeDest, 0, urlSafeLen);
                    return urlSafeDest;
                }
                else
                {
                    dest[destlen - 2] = (left == 2) ? encodeTable[i & 0x3f] : (byte)'=';
                    dest[destlen - 1] = (byte)'=';
                }
            }
            return dest;
        }
    }
}