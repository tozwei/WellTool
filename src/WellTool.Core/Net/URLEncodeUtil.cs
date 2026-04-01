using System;
using System.Text;

namespace WellTool.Core.Net
{
    /// <summary>
    /// URL 编码工具类
    /// </summary>
    public class URLEncodeUtil
    {
        /// <summary>
        /// URL 编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string Encode(string str)
        {
            return Uri.EscapeDataString(str);
        }

        /// <summary>
        /// URL 编码（使用指定编码）
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>编码后的字符串</returns>
        public static string Encode(string str, Encoding encoding)
        {
            return Uri.EscapeDataString(str);
        }

        /// <summary>
        /// URL 解码
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string Decode(string str)
        {
            return Uri.UnescapeDataString(str);
        }

        /// <summary>
        /// URL 解码（使用指定编码）
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解码后的字符串</returns>
        public static string Decode(string str, Encoding encoding)
        {
            return Uri.UnescapeDataString(str);
        }

        /// <summary>
        /// 编码 URL 路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>编码后的路径</returns>
        public static string EncodePath(string path)
        {
            return Uri.EscapeUriString(path);
        }

        /// <summary>
        /// 编码 URL 查询参数
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns>编码后的查询参数</returns>
        public static string EncodeQueryParam(string param)
        {
            return Uri.EscapeDataString(param);
        }

        /// <summary>
        /// 编码 URL 片段
        /// </summary>
        /// <param name="fragment">片段</param>
        /// <returns>编码后的片段</returns>
        public static string EncodeFragment(string fragment)
        {
            return Uri.EscapeDataString(fragment);
        }
    }
}