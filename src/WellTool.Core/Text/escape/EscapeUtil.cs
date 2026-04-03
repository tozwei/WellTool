using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Core.Text.Escape
{
    /// <summary>
    /// HTML转义工具类
    /// </summary>
    public static class HtmlEscapeUtil
    {
        private static readonly Regex HtmlPattern = new Regex(@"[<>&""']", RegexOptions.Compiled);

        private static readonly string[] HtmlCodes = new[]
        {
            "&lt;", "&gt;", "&amp;", "&quot;", "&#39;"
        };

        /// <summary>
        /// HTML转义
        /// </summary>
        public static string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return HtmlPattern.Replace(str, match =>
            {
                int index = Array.IndexOf(new[] { '<', '>', '&', '"', '\'' }, match.Value[0]);
                return index >= 0 ? HtmlCodes[index] : match.Value;
            });
        }

        /// <summary>
        /// HTML反转义
        /// </summary>
        public static string Unescape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Replace("&lt;", "<")
                      .Replace("&gt;", ">")
                      .Replace("&amp;", "&")
                      .Replace("&quot;", "\"")
                      .Replace("&#39;", "'")
                      .Replace("&apos;", "'");
        }

        /// <summary>
        /// 移除HTML标签
        /// </summary>
        public static string RemoveTags(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return Regex.Replace(str, "<[^>]*>", "");
        }
    }

    /// <summary>
    /// XML转义工具类
    /// </summary>
    public static class XmlEscapeUtil
    {
        /// <summary>
        /// XML转义
        /// </summary>
        public static string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Replace("&", "&amp;")
                      .Replace("<", "&lt;")
                      .Replace(">", "&gt;")
                      .Replace("\"", "&quot;")
                      .Replace("'", "&apos;");
        }

        /// <summary>
        /// XML反转义
        /// </summary>
        public static string Unescape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Replace("&amp;", "&")
                      .Replace("&lt;", "<")
                      .Replace("&gt;", ">")
                      .Replace("&quot;", "\"")
                      .Replace("&apos;", "'");
        }
    }

    /// <summary>
    /// 转义工具类入口
    /// </summary>
    public static class EscapeUtil
    {
        /// <summary>
        /// HTML转义
        /// </summary>
        public static string HtmlEscape(string str) => HtmlEscapeUtil.Escape(str);

        /// <summary>
        /// HTML反转义
        /// </summary>
        public static string HtmlUnescape(string str) => HtmlEscapeUtil.Unescape(str);

        /// <summary>
        /// XML转义
        /// </summary>
        public static string XmlEscape(string str) => XmlEscapeUtil.Escape(str);

        /// <summary>
        /// XML反转义
        /// </summary>
        public static string XmlUnescape(string str) => XmlEscapeUtil.Unescape(str);
    }
}
