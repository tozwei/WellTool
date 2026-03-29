using System;
using System.IO;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base64工具类，提供Base64的编码和解码方案
    /// base64编码是用64（2的6次方）个ASCII字符来表示256（2的8次方）个ASCII字符，
    /// 也就是三位二进制数组经过编码后变为四位的ASCII字符显示，长度比原来增加1/3。
    /// </summary>
    public static class Base64
    {
        static Base64()
        {
            // 注册编码提供程序，支持GBK编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        // -------------------------------------------------------------------- encode

        /// <summary>
        /// 编码为Base64，非URL安全的
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="lineSep">在76个char之后是CRLF还是EOF</param>
        /// <returns>编码后的bytes</returns>
        public static byte[] Encode(byte[] arr, bool lineSep)
        {
            if (arr == null)
            {
                return null;
            }
            string base64String = System.Convert.ToBase64String(arr, lineSep ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None);
            return DEFAULT_ENCODING.GetBytes(base64String);
        }

        /// <summary>
        /// 编码为Base64，URL安全的
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="lineSep">在76个char之后是CRLF还是EOF</param>
        /// <returns>编码后的bytes</returns>
        [Obsolete("按照RFC2045规范，URL安全的Base64无需换行")]
        public static byte[] EncodeUrlSafe(byte[] arr, bool lineSep)
        {
            return Base64Encoder.EncodeUrlSafe(arr, lineSep);
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
        /// <param name="charset">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(string source, string charset)
        {
            return Encode(source, Encoding.GetEncoding(charset));
        }

        /// <summary>
        /// base64编码，不进行padding(末尾不会填充'=')
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <param name="charset">编码</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeWithoutPadding(string source, string charset)
        {
            return EncodeWithoutPadding(Encoding.GetEncoding(charset).GetBytes(source));
        }

        /// <summary>
        /// base64编码,URL安全
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <param name="charset">字符集</param>
        /// <returns>被加密后的字符串</returns>
        [Obsolete("请使用 EncodeUrlSafe(string, Encoding)")]
        public static string EncodeUrlSafe(string source, string charset)
        {
            return EncodeUrlSafe(source, Encoding.GetEncoding(charset));
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被编码后的字符串</returns>
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
            if (source == null)
            {
                return null;
            }
            return System.Convert.ToBase64String(source);
        }

        /// <summary>
        /// base64编码，不进行padding(末尾不会填充'=')
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeWithoutPadding(byte[] source)
        {
            if (source == null)
            {
                return null;
            }
            string base64 = System.Convert.ToBase64String(source);
            return base64.TrimEnd('=');
        }

        /// <summary>
        /// base64编码,URL安全的
        /// </summary>
        /// <param name="source">被编码的base64字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(byte[] source)
        {
            if (source == null)
            {
                return null;
            }
            string base64 = System.Convert.ToBase64String(source);
            return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="stream">被编码base64的流（一般为图片流或者文件流）</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return Encode(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// base64编码,URL安全的
        /// </summary>
        /// <param name="stream">被编码base64的流（一般为图片流或者文件流）</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return EncodeUrlSafe(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="file">被编码base64的文件</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(FileInfo file)
        {
            return Encode(File.ReadAllBytes(file.FullName));
        }

        /// <summary>
        /// base64编码,URL安全的
        /// </summary>
        /// <param name="file">被编码base64的文件</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeUrlSafe(FileInfo file)
        {
            return EncodeUrlSafe(File.ReadAllBytes(file.FullName));
        }

        /// <summary>
        /// 编码为Base64字符串
        /// 如果isMultiLine为{@code true}，则每76个字符一个换行符，否则在一行显示
        /// </summary>
        /// <param name="arr">被编码的数组</param>
        /// <param name="isMultiLine">在76个char之后是CRLF还是EOF</param>
        /// <param name="isUrlSafe">是否使用URL安全字符，一般为{@code false}</param>
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
        /// <param name="isUrlSafe">是否使用URL安全字符，一般为{@code false}</param>
        /// <returns>编码后的bytes</returns>
        public static byte[] Encode(byte[] arr, bool isMultiLine, bool isUrlSafe)
        {
            return Base64Encoder.Encode(arr, isMultiLine, isUrlSafe);
        }

        // -------------------------------------------------------------------- decode

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <returns>密文解密的结果</returns>
        public static string DecodeStrGbk(string source)
        {
            return Base64Decoder.DecodeStr(source, Encoding.GetEncoding("GBK"));
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <returns>密文解密的结果</returns>
        public static string DecodeStr(string source)
        {
            return Base64Decoder.DecodeStr(source);
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <param name="charset">字符集</param>
        /// <returns>密文解密的结果</returns>
        public static string DecodeStr(string source, string charset)
        {
            return DecodeStr(source, Encoding.GetEncoding(charset));
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="source">被解码的base64字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>密文解密的结果</returns>
        public static string DecodeStr(string source, Encoding encoding)
        {
            return Base64Decoder.DecodeStr(source, encoding);
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="base64">被解码的base64字符串</param>
        /// <param name="destFile">目标文件</param>
        /// <returns>目标文件</returns>
        public static FileInfo DecodeToFile(string base64, FileInfo destFile)
        {
            File.WriteAllBytes(destFile.FullName, Base64Decoder.Decode(base64));
            return destFile;
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="base64">被解码的base64字符串</param>
        /// <param name="outStream">写出到的流</param>
        /// <param name="isCloseOut">是否关闭输出流</param>
        public static void DecodeToStream(string base64, Stream outStream, bool isCloseOut)
        {
            byte[] bytes = Base64Decoder.Decode(base64);
            outStream.Write(bytes, 0, bytes.Length);
            if (isCloseOut)
            {
                outStream.Dispose();
            }
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="base64">被解码的base64字符串</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(string base64)
        {
            return Base64Decoder.Decode(base64);
        }

        /// <summary>
        /// 解码Base64
        /// </summary>
        /// <param name="inBytes">输入</param>
        /// <returns>解码后的bytes</returns>
        public static byte[] Decode(byte[] inBytes)
        {
            return Base64Decoder.Decode(inBytes);
        }

        /// <summary>
        /// 检查是否为Base64
        /// </summary>
        /// <param name="base64">Base64的bytes</param>
        /// <returns>是否为Base64</returns>
        public static bool IsBase64(string base64)
        {
            if (string.IsNullOrEmpty(base64) || base64.Length < 2)
            {
                return false;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(base64);

            if (bytes.Length != base64.Length)
            {
                // 如果长度不相等，说明存在双字节字符，肯定不是Base64，直接返回false
                return false;
            }

            return IsBase64(bytes);
        }

        /// <summary>
        /// 检查是否为Base64
        /// </summary>
        /// <param name="base64Bytes">Base64的bytes</param>
        /// <returns>是否为Base64</returns>
        public static bool IsBase64(byte[] base64Bytes)
        {
            if (base64Bytes == null || base64Bytes.Length < 3)
            {
                return false;
            }
            bool hasPadding = false;
            foreach (byte base64Byte in base64Bytes)
            {
                if (hasPadding)
                {
                    if ('=' != base64Byte)
                    {
                        // 前一个字符是'='，则后边的字符都必须是'='，即'='只能都位于结尾
                        return false;
                    }
                }
                else if ('=' == base64Byte)
                {
                    // 发现'=' 标记之
                    hasPadding = true;
                }
                else if (!Base64Decoder.IsBase64Code(base64Byte) && !IsWhiteSpace(base64Byte))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsWhiteSpace(byte byteToCheck)
        {
            switch (byteToCheck)
            {
                case (byte)' ':
                case (byte) '\n':
                case (byte) '\r':
                case (byte) '\t':
                    return true;
                default:
                    return false;
            }
        }
    }
}