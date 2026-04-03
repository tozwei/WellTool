using System;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 字符集工具类
    /// </summary>
    public static class CharsetUtil
    {
        /// <summary>
        /// UTF-8字符集
        /// </summary>
        public static Encoding UTF8 => Encoding.UTF8;

        /// <summary>
        /// GBK字符集
        /// </summary>
        public static Encoding GBK => Encoding.GetEncoding("GBK");

        /// <summary>
        /// GB2312字符集
        /// </summary>
        public static Encoding GB2312 => Encoding.GetEncoding("GB2312");

        /// <summary>
        /// ASCII字符集
        /// </summary>
        public static Encoding ASCII => Encoding.ASCII;

        /// <summary>
        /// Unicode字符集
        /// </summary>
        public static Encoding UNICODE => Encoding.Unicode;

        /// <summary>
        /// UTF-32字符集
        /// </summary>
        public static Encoding UTF32 => Encoding.UTF32;

        /// <summary>
        /// 获取字符集
        /// </summary>
        public static Encoding Charset(string charsetName)
        {
            if (string.IsNullOrEmpty(charsetName))
            {
                return Encoding.UTF8;
            }

            charsetName = charsetName.Trim().ToUpper();
            
            // 常见字符集名称映射
            switch (charsetName)
            {
                case "UTF-8":
                case "UTF8":
                    return Encoding.UTF8;
                case "GBK":
                    return Encoding.GetEncoding("GBK");
                case "GB2312":
                    return Encoding.GetEncoding("GB2312");
                case "ASCII":
                    return Encoding.ASCII;
                case "UNICODE":
                case "UTF-16":
                    return Encoding.Unicode;
                case "UTF-32":
                case "UTF32":
                    return Encoding.UTF32;
                case "ISO-8859-1":
                case "LATIN1":
                    return Encoding.GetEncoding("ISO-8859-1");
                case "BIG5":
                    return Encoding.GetEncoding("Big5");
                case "SHIFT-JIS":
                case "SHIFT_JIS":
                    return Encoding.GetEncoding("Shift_JIS");
                default:
                    try
                    {
                        return Encoding.GetEncoding(charsetName);
                    }
                    catch
                    {
                        return Encoding.UTF8;
                    }
            }
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        public static byte[] ToBytes(string str, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return Array.Empty<byte>();
            }
            return (encoding ?? Encoding.UTF8).GetBytes(str);
        }

        /// <summary>
        /// 从字节数组转换
        /// </summary>
        public static string FromBytes(byte[] bytes, Encoding encoding = null)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }
            return (encoding ?? Encoding.UTF8).GetString(bytes);
        }

        /// <summary>
        /// 转换字符串的编码
        /// </summary>
        public static string Convert(string str, Encoding fromEncoding, Encoding toEncoding)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (fromEncoding == null || toEncoding == null)
            {
                throw new ArgumentNullException("Encoding cannot be null");
            }

            byte[] bytes = fromEncoding.GetBytes(str);
            return toEncoding.GetString(bytes);
        }

        /// <summary>
        /// 获取字符集名称
        /// </summary>
        public static string GetCharsetName(Encoding encoding)
        {
            return encoding?.WebName ?? "utf-8";
        }

        /// <summary>
        /// 检查是否支持指定字符集
        /// </summary>
        public static bool IsSupported(string charsetName)
        {
            try
            {
                Encoding.GetEncoding(charsetName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取所有可用字符集名称
        /// </summary>
        public static string[] GetAllCharsets()
        {
            return Encoding.GetEncodings().Select(e => e.Name).ToArray();
        }
    }
}
