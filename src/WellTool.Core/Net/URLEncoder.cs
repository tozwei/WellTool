using System;
using System.Text;
using WellTool.Core.Util;

namespace WellTool.Core.Net
{
    /// <summary>
    /// URL编码，数据内容的类型是 application/x-www-form-urlencoded。
    /// <pre>
    /// 1.字符"a"-"z"，"A"-"Z"，"0"-"9"，"."，"-"，"*"，和"_" 都不会被编码;
    /// 2.将空格转换为%20 ;
    /// 3.将非文本内容转换成"%xy"的形式,xy是两位16进制的数值;
    /// </pre>
    /// </summary>
    [Obsolete("此类中的方法并不规范，请使用 RFC3986")]
    public class URLEncoder
    {
        // --------------------------------------------------------------------------------------------- Static method start
        /// <summary>
        /// 默认URLEncoder<br>
        /// 默认的编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// default = pchar / "/"
        /// pchar = unreserved（不处理） / pct-encoded / sub-delims（子分隔符） / ":" / "@"
        /// unreserved = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// </summary>
        public static readonly URLEncoder Default = CreateDefault();

        /// <summary>
        /// URL的Path的每一个Segment URLEncoder<br>
        /// 默认的编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// pchar = unreserved / pct-encoded / sub-delims / ":"（非空segment不包含:） / "@"
        /// unreserved = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// 
        /// 定义见：https://www.rfc-editor.org/rfc/rfc3986.html#section-3.3
        /// </summary>
        public static readonly URLEncoder PathSegment = CreatePathSegment();

        /// <summary>
        /// URL的Fragment URLEncoder<br>
        /// 默认的编码器针对Fragment，定义如下：
        /// 
        /// <pre>
        /// fragment    = *( pchar / "/" / "?" )
        /// pchar       = unreserved / pct-encoded / sub-delims / ":" / "@"
        /// unreserved  = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims  = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// 
        /// 具体见：https://datatracker.ietf.org/doc/html/rfc3986#section-3.5
        /// </summary>
        public static readonly URLEncoder Fragment = CreateFragment();

        /// <summary>
        /// 用于查询语句的URLEncoder<br>
        /// 编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// 0x20 ' ' =》 '+'
        /// 0x2A, 0x2D, 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A as-is
        /// '*', '-', '.', '0' to '9', 'A' to 'Z', '_', 'a' to 'z' Also '=' and '&' 不编码
        /// 其它编码为 %nn 形式
        /// </pre>
        /// <p>
        /// 详细见：https://www.w3.org/TR/html5/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        public static readonly URLEncoder Query = CreateQuery();

        /// <summary>
        /// 全编码的URLEncoder<br>
        /// <pre>
        ///  0x2A, 0x2D, 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A as-is
        ///  '*', '-', '.', '0' to '9', 'A' to 'Z', '_', 'a' to 'z' 不编码
        ///  其它编码为 %nn 形式
        /// </pre>
        /// </summary>
        public static readonly URLEncoder All = CreateAll();

        /// <summary>
        /// 创建默认URLEncoder<br>
        /// 默认的编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// default = pchar / "/"
        /// pchar = unreserved（不处理） / pct-encoded / sub-delims（子分隔符） / ":" / "@"
        /// unreserved = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// 
        /// @return URLEncoder
        /// </summary>
        public static URLEncoder CreateDefault()
        {
            var encoder = new URLEncoder();
            encoder.AddSafeCharacter('-');
            encoder.AddSafeCharacter('.');
            encoder.AddSafeCharacter('_');
            encoder.AddSafeCharacter('~');

            // Add the sub-delims
            AddSubDelims(encoder);

            // Add the remaining literals
            encoder.AddSafeCharacter(':');
            encoder.AddSafeCharacter('@');

            // Add '/' so it isn't encoded when we encode a path
            encoder.AddSafeCharacter('/');

            return encoder;
        }

        /// <summary>
        /// URL的Path的每一个Segment URLEncoder<br>
        /// 默认的编码器针对URI路径的每一段编码，定义如下：
        /// 
        /// <pre>
        /// pchar = unreserved / pct-encoded / sub-delims / ":"（非空segment不包含:） / "@"
        /// unreserved = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// 
        /// 定义见：https://www.rfc-editor.org/rfc/rfc3986.html#section-3.3
        /// 
        /// @return URLEncoder
        /// </summary>
        public static URLEncoder CreatePathSegment()
        {
            var encoder = new URLEncoder();

            // unreserved
            encoder.AddSafeCharacter('-');
            encoder.AddSafeCharacter('.');
            encoder.AddSafeCharacter('_');
            encoder.AddSafeCharacter('~');

            // Add the sub-delims
            AddSubDelims(encoder);

            // Add the remaining literals
            //non-zero-length segment without any colon ":"
            //encoder.AddSafeCharacter(':');
            encoder.AddSafeCharacter('@');

            return encoder;
        }

        /// <summary>
        /// URL的Fragment URLEncoder<br>
        /// 默认的编码器针对Fragment，定义如下：
        /// 
        /// <pre>
        /// fragment    = *( pchar / "/" / "?" )
        /// pchar       = unreserved / pct-encoded / sub-delims / ":" / "@"
        /// unreserved  = ALPHA / DIGIT / "-" / "." / "_" / "~"
        /// sub-delims  = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// </pre>
        /// 
        /// 具体见：https://datatracker.ietf.org/doc/html/rfc3986#section-3.5
        /// 
        /// @return URLEncoder
        /// </summary>
        public static URLEncoder CreateFragment()
        {
            var encoder = new URLEncoder();
            encoder.AddSafeCharacter('-');
            encoder.AddSafeCharacter('.');
            encoder.AddSafeCharacter('_');
            encoder.AddSafeCharacter('~');

            // Add the sub-delims
            AddSubDelims(encoder);

            // Add the remaining literals
            encoder.AddSafeCharacter(':');
            encoder.AddSafeCharacter('@');

            encoder.AddSafeCharacter('/');
            encoder.AddSafeCharacter('?');

            return encoder;
        }

        /// <summary>
        /// 创建用于查询语句的URLEncoder<br>
        /// 编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// 0x20 ' ' =》 '+'
        /// 0x2A, 0x2D, 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A as-is
        /// '*', '-', '.', '0' to '9', 'A' to 'Z', '_', 'a' to 'z' Also '=' and '&' 不编码
        /// 其它编码为 %nn 形式
        /// </pre>
        /// <p>
        /// 详细见：https://www.w3.org/TR/html5/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// 
        /// @return URLEncoder
        /// </summary>
        public static URLEncoder CreateQuery()
        {
            var encoder = new URLEncoder();
            // Special encoding for space
            encoder.SetEncodeSpaceAsPlus(true);
            // Alpha and digit are safe by default
            // Add the other permitted characters
            encoder.AddSafeCharacter('*');
            encoder.AddSafeCharacter('-');
            encoder.AddSafeCharacter('.');
            encoder.AddSafeCharacter('_');

            encoder.AddSafeCharacter('=');
            encoder.AddSafeCharacter('&');

            return encoder;
        }

        /// <summary>
        /// 创建URLEncoder<br>
        /// 编码器针对URI路径编码，定义如下：
        /// 
        /// <pre>
        /// 0x2A, 0x2D, 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A as-is
        /// '*', '-', '.', '0' to '9', 'A' to 'Z', '_', 'a' to 'z' 不编码
        /// 其它编码为 %nn 形式
        /// </pre>
        /// <p>
        /// 详细见：https://www.w3.org/TR/html5/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// 
        /// @return URLEncoder
        /// </summary>
        public static URLEncoder CreateAll()
        {
            var encoder = new URLEncoder();
            encoder.AddSafeCharacter('*');
            encoder.AddSafeCharacter('-');
            encoder.AddSafeCharacter('.');
            encoder.AddSafeCharacter('_');

            return encoder;
        }
        // --------------------------------------------------------------------------------------------- Static method end

        /// <summary>
        /// 存放安全编码
        /// </summary>
        private readonly bool[] _safeCharacters = new bool[256];
        /// <summary>
        /// 是否编码空格为+
        /// </summary>
        private bool _encodeSpaceAsPlus = false;

        /// <summary>
        /// 构造<br>
        /// [a-zA-Z0-9]默认不被编码
        /// </summary>
        public URLEncoder()
        {
            // unreserved
            AddAlpha();
            AddDigit();
        }

        /// <summary>
        /// 增加安全字符<br>
        /// 安全字符不被编码
        /// </summary>
        /// <param name="c">字符</param>
        public void AddSafeCharacter(char c)
        {
            if (c < 256)
            {
                _safeCharacters[c] = true;
            }
        }

        /// <summary>
        /// 移除安全字符<br>
        /// 安全字符不被编码
        /// </summary>
        /// <param name="c">字符</param>
        public void RemoveSafeCharacter(char c)
        {
            if (c < 256)
            {
                _safeCharacters[c] = false;
            }
        }

        /// <summary>
        /// 是否将空格编码为+
        /// </summary>
        /// <param name="encodeSpaceAsPlus">是否将空格编码为+</param>
        public void SetEncodeSpaceAsPlus(bool encodeSpaceAsPlus)
        {
            _encodeSpaceAsPlus = encodeSpaceAsPlus;
        }

        /// <summary>
        /// 将URL中的字符串编码为%形式
        /// </summary>
        /// <param name="path">需要编码的字符串</param>
        /// <param name="charset">编码, null返回原字符串，表示不编码</param>
        /// <returns>编码后的字符串</returns>
        public string Encode(string path, Encoding charset)
        {
            if (charset == null || StrUtil.IsEmpty(path))
            {
                return path;
            }

            StringBuilder rewrittenPath = new StringBuilder(path.Length);

            foreach (char c in path)
            {
                if (c < 256 && _safeCharacters[c])
                {
                    rewrittenPath.Append(c);
                }
                else if (_encodeSpaceAsPlus && c == ' ')
                {
                    // 对于空格单独处理
                    rewrittenPath.Append('+');
                }
                else
                {
                    // convert to external encoding before hex conversion
                    byte[] ba = charset.GetBytes(new char[] { c });
                    foreach (byte toEncode in ba)
                    {
                        // Converting each byte in the buffer
                        rewrittenPath.Append('%');
                        rewrittenPath.Append(HexUtil.ToHex(new byte[] { toEncode }, false));
                    }
                }
            }
            return rewrittenPath.ToString();
        }

        /// <summary>
        /// 增加安全字符[a-z][A-Z]
        /// </summary>
        private void AddAlpha()
        {
            for (char i = 'a'; i <= 'z'; i++)
            {
                AddSafeCharacter(i);
            }
            for (char i = 'A'; i <= 'Z'; i++)
            {
                AddSafeCharacter(i);
            }
        }

        /// <summary>
        /// 增加数字1-9
        /// </summary>
        private void AddDigit()
        {
            for (char i = '0'; i <= '9'; i++)
            {
                AddSafeCharacter(i);
            }
        }

        /// <summary>
        /// 增加sub-delims<br>
        /// sub-delims  = "!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="
        /// 定义见：https://datatracker.ietf.org/doc/html/rfc3986#section-2.2
        /// </summary>
        /// <param name="encoder">编码器</param>
        private static void AddSubDelims(URLEncoder encoder)
        {
            // Add the sub-delims
            encoder.AddSafeCharacter('!');
            encoder.AddSafeCharacter('$');
            encoder.AddSafeCharacter('&');
            encoder.AddSafeCharacter('\'');
            encoder.AddSafeCharacter('(');
            encoder.AddSafeCharacter(')');
            encoder.AddSafeCharacter('*');
            encoder.AddSafeCharacter('+');
            encoder.AddSafeCharacter(',');
            encoder.AddSafeCharacter(';');
            encoder.AddSafeCharacter('=');
        }
    }
}