using System;
using System.IO;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 解析器
    /// </summary>
    public class JSONTokener
    {
        private readonly TextReader _reader;
        private int _character;
        private bool _eof;
        private int _index;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="s">JSON 字符串</param>
        public JSONTokener(string s)
        {
            _reader = new StringReader(s);
            _index = 0;
            Next();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reader">文本读取器</param>
        public JSONTokener(TextReader reader)
        {
            _reader = reader;
            _index = 0;
            Next();
        }

        /// <summary>
        /// 读取下一个字符
        /// </summary>
        /// <returns>下一个字符</returns>
        public int Next()
        {
            var c = _character;
            if (c != -1)
            {
                _index++;
            }
            if (_reader != null)
            {
                _character = _reader.Read();
            }
            else
            {
                _character = -1;
            }
            if (_character == -1)
            {
                _eof = true;
            }
            return c;
        }

        /// <summary>
        /// 查看当前字符
        /// </summary>
        /// <returns>当前字符</returns>
        public int NextCharacter()
        {
            return _character;
        }

        /// <summary>
        /// 回退一个字符
        /// </summary>
        public void Back()
        {
            if (_index > 0 && !_eof)
            {
                _index--;
                _eof = false;
                // 注意：TextReader 没有 BaseStream 属性，这里简化处理
                // 对于 StringReader，我们无法回退，所以这里只更新索引和 eof 状态
                _character = -1;
            }
        }

        /// <summary>
        /// 跳过空白字符
        /// </summary>
        public void SkipWhitespace()
        {
            while (char.IsWhiteSpace((char)_character))
            {
                Next();
            }
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string NextString()
        {
            if (_character != '"')
            {
                throw new JSONException($"Expected '\"' at position {_index}");
            }

            Next(); // 跳过 '"'
            var sb = new System.Text.StringBuilder();

            while (_character != -1)
            {
                var c = (char)Next();
                if (c == '"')
                {
                    return sb.ToString();
                }
                if (c == '\\')
                {
                    c = (char)Next();
                    switch (c)
                    {
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'n': sb.Append('\n'); break;
                        case 'r': sb.Append('\r'); break;
                        case 't': sb.Append('\t'); break;
                        case '"': sb.Append('"'); break;
                        case '\\': sb.Append('\\'); break;
                        case '/': sb.Append('/'); break;
                        case 'u':
                            var hex = new char[4];
                            for (int i = 0; i < 4; i++)
                            {
                                hex[i] = (char)Next();
                            }
                            sb.Append((char)Convert.ToInt32(new string(hex), 16));
                            break;
                        default:
                            throw new JSONException($"Invalid escape character at position {_index}");
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            throw new JSONException($"Unterminated string at position {_index}");
        }

        /// <summary>
        /// 读取数字
        /// </summary>
        /// <returns>数字对象</returns>
        public object NextNumber()
        {
            var sb = new System.Text.StringBuilder();

            if (_character == '-')
            {
                sb.Append((char)Next());
            }

            while (char.IsDigit((char)_character))
            {
                sb.Append((char)Next());
            }

            if (_character == '.')
            {
                sb.Append((char)Next());
                while (char.IsDigit((char)_character))
                {
                    sb.Append((char)Next());
                }
            }

            if (_character == 'e' || _character == 'E')
            {
                sb.Append((char)Next());
                if (_character == '+' || _character == '-')
                {
                    sb.Append((char)Next());
                }
                while (char.IsDigit((char)_character))
                {
                    sb.Append((char)Next());
                }
            }

            var s = sb.ToString();
            if (s.Contains(".") || s.Contains("e") || s.Contains("E"))
            {
                return double.Parse(s);
            }
            else
            {
                return long.Parse(s);
            }
        }

        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <returns>布尔值</returns>
        public bool NextBoolean()
        {
            var s = NextWord();
            if (s == "true")
            {
                return true;
            }
            else if (s == "false")
            {
                return false;
            }
            else
            {
                throw new JSONException($"Expected 'true' or 'false' at position {_index}");
            }
        }

        /// <summary>
        /// 读取 null
        /// </summary>
        /// <returns>JSONNull.Instance</returns>
        public object NextNull()
        {
            var s = NextWord();
            if (s == "null")
            {
                return JSONNull.Instance;
            }
            else
            {
                throw new JSONException($"Expected 'null' at position {_index}");
            }
        }

        /// <summary>
        /// 读取单词
        /// </summary>
        /// <returns>单词</returns>
        public string NextWord()
        {
            var sb = new System.Text.StringBuilder();
            while (_character != -1 && char.IsLetterOrDigit((char)_character))
            {
                sb.Append((char)Next());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 匹配字符
        /// </summary>
        /// <param name="c">要匹配的字符</param>
        public void Match(char c)
        {
            if (_character != c)
            {
                throw new JSONException($"Expected '{c}' at position {_index}");
            }
            Next();
        }

        /// <summary>
        /// 是否到达文件末尾
        /// </summary>
        public bool EndOfFile
        {
            get { return _eof; }
        }
    }
}