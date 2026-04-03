using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
        /// XML分析器，继承自JSONTokener，提供XML的语法分析
        /// </summary>
        public class XMLTokener : JSONTokener
        {
            /// <summary>
            /// XML字符常量
            /// </summary>
            public const char AMP = '&';
            public const char APOS = '\'';
            public const char GT = '>';
            public const char LT = '<';
            public const char QUOT = '"';
            public const char SLASH = '/';
            public const char EQ = '=';
            public const char BANG = '!';
            public const char QUEST = '?';

            /// <summary>
            /// 实体值表
            /// </summary>
            private static readonly Dictionary<string, char> _entity = new Dictionary<string, char>
            {
                { "amp", AMP },
                { "apos", APOS },
                { "gt", GT },
                { "lt", LT },
                { "quot", QUOT }
            };

            /// <summary>
            /// Construct an XMLTokener from a string
            /// </summary>
            /// <param name="s">A source string</param>
            public XMLTokener(string s) : base(s)
            {
            }

            /// <summary>
            /// 抛出语法错误异常
            /// </summary>
            /// <param name="message">错误消息</param>
            /// <returns>异常</returns>
            protected JSONException SyntaxError(string message)
            {
                throw new JSONException(message);
            }

            /// <summary>
            /// 检查是否到达末尾
            /// </summary>
            /// <returns>是否到达末尾</returns>
            protected bool End()
            {
                return EndOfFile;
            }

        /// <summary>
        /// Get the text in the CDATA block
        /// </summary>
        /// <returns>The string up to the ]]&gt;</returns>
        public string NextCDATA()
        {
            var sb = new StringBuilder();
            while (true)
            {
                char c = Next();
                if (End())
                {
                    throw SyntaxError("Unclosed CDATA");
                }
                sb.Append(c);
                int i = sb.Length - 3;
                if (i >= 0 && sb[i] == ']' && sb[i + 1] == ']' && sb[i + 2] == '>')
                {
                    sb.Length = i;
                    return sb.ToString();
                }
            }
        }

        /// <summary>
        /// Get the next XML outer token, trimming whitespace
        /// </summary>
        /// <returns>A string, or a '&gt;' Character, or null if there is no more source text</returns>
        public object NextContent()
        {
            char c;
            do
            {
                c = Next();
            } while (char.IsWhiteSpace(c));

            if (c == '\0')
            {
                return null;
            }
            if (c == '<')
            {
                return LT;
            }
            var sb = new StringBuilder();
            while (true)
            {
                if (c == '<' || c == '\0')
                {
                    Back();
                    return sb.ToString().Trim();
                }
                if (c == '&')
                {
                    sb.Append(NextEntity(c));
                }
                else
                {
                    sb.Append(c);
                }
                c = Next();
            }
        }

        /// <summary>
        /// Return the next entity
        /// </summary>
        /// <param name="ampersand">An ampersand character</param>
        /// <returns>A Character or an entity String if the entity is not recognized</returns>
        public object NextEntity(char ampersand)
        {
            var sb = new StringBuilder();
            while (true)
            {
                char c = Next();
                if (char.IsLetterOrDigit(c) || c == '#')
                {
                    sb.Append(char.ToLower(c));
                }
                else if (c == ';')
                {
                    break;
                }
                else
                {
                    throw SyntaxError("Missing ';' in XML entity: &" + sb);
                }
            }
            return UnescapeEntity(sb.ToString());
        }

        /// <summary>
        /// Unescape an XML entity encoding
        /// </summary>
        /// <param name="e">entity (only the actual entity value, not the preceding &amp; or ending ;</param>
        /// <returns>Unescape str</returns>
        private static string UnescapeEntity(string e)
        {
            if (string.IsNullOrEmpty(e))
            {
                return "";
            }
            // if our entity is an encoded unicode point, parse it
            if (e[0] == '#')
            {
                int cp;
                if (e[1] == 'x' || e[1] == 'X')
                {
                    // hex encoded unicode
                    cp = int.Parse(e.Substring(2), System.Globalization.NumberStyles.HexNumber);
                }
                else
                {
                    // decimal encoded unicode
                    cp = int.Parse(e.Substring(1));
                }
                return char.ConvertFromUtf32(cp);
            }
            if (_entity.TryGetValue(e, out char knownEntity))
            {
                return knownEntity.ToString();
            }
            // we don't know the entity so keep it encoded
            return '&' + e + ';';
        }

        /// <summary>
        /// Returns the next XML meta token
        /// </summary>
        /// <returns>Syntax characters are returned as Character, and strings and names are returned as Boolean</returns>
        public object NextMeta()
        {
            char c;
            char q;
            do
            {
                c = Next();
            } while (char.IsWhiteSpace(c));

            switch (c)
            {
                case (char)0:
                    throw SyntaxError("Misshaped meta tag");
                case '<':
                    return LT;
                case '>':
                    return GT;
                case '/':
                    return SLASH;
                case '=':
                    return EQ;
                case '!':
                    return BANG;
                case '?':
                    return QUEST;
                case '"':
                case '\'':
                    q = c;
                    while (true)
                    {
                        c = Next();
                        if (c == 0)
                        {
                            throw SyntaxError("Unterminated string");
                        }
                        if (c == q)
                        {
                            return true;
                        }
                    }
                default:
                    while (true)
                    {
                        c = Next();
                        if (char.IsWhiteSpace(c))
                        {
                            return true;
                        }
                        switch (c)
                        {
                            case '\0':
                            case '<':
                            case '>':
                            case '/':
                            case '=':
                            case '!':
                            case '?':
                            case '"':
                            case '\'':
                                Back();
                                return true;
                        }
                    }
            }
        }

        /// <summary>
        /// Get the next XML Token
        /// </summary>
        /// <returns>a String or a Character</returns>
        public object NextToken()
        {
            char c;
            char q;
            StringBuilder sb;
            do
            {
                c = Next();
            } while (char.IsWhiteSpace(c));

            switch (c)
            {
                case '\0':
                    throw SyntaxError("Misshaped element");
                case '<':
                    throw SyntaxError("Misplaced '<'");
                case '>':
                    return GT;
                case '/':
                    return SLASH;
                case '=':
                    return EQ;
                case '!':
                    return BANG;
                case '?':
                    return QUEST;

                // Quoted string
                case '"':
                case '\'':
                    q = c;
                    sb = new StringBuilder();
                    while (true)
                    {
                        c = Next();
                        if (c == '\0')
                        {
                            throw SyntaxError("Unterminated string");
                        }
                        if (c == q)
                        {
                            return sb.ToString();
                        }
                        if (c == '&')
                        {
                            sb.Append(NextEntity(c));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                default:
                    // Name
                    sb = new StringBuilder();
                    while (true)
                    {
                        sb.Append(c);
                        c = Next();
                        if (char.IsWhiteSpace(c))
                        {
                            return sb.ToString();
                        }
                        switch (c)
                        {
                            case '\0':
                                return sb.ToString();
                            case '>':
                            case '/':
                            case '=':
                            case '!':
                            case '?':
                            case '[':
                            case ']':
                                Back();
                                return sb.ToString();
                            case '<':
                            case '"':
                            case '\'':
                                throw SyntaxError("Bad character in a name");
                        }
                    }
            }
        }
    }
}
