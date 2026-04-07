using System;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 令牌解析器
    /// </summary>
    public class JSONTokener
    {
        private readonly string _input;
        private int _position;
        private readonly int _length;

        /// <summary>
        /// 创建 JSONTokener
        /// </summary>
        /// <param name="input">输入字符串</param>
        public JSONTokener(string input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _length = _input.Length;
            _position = 0;
        }

        /// <summary>
        /// 创建 JSONTokener
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="config">JSON配置</param>
        public JSONTokener(string input, JSONConfig config)
            : this(input)
        {
            // 配置暂时未使用
        }

        /// <summary>
        /// 当前读取位置
        /// </summary>
        public int Position => _position;

        /// <summary>
        /// 是否到达末尾
        /// </summary>
        public bool EndOfFile => _position >= _length;

        /// <summary>
        /// 是否到达末尾
        /// </summary>
        /// <returns>是否到达末尾</returns>
        public bool End()
        {
            return EndOfFile;
        }

        /// <summary>
        /// 跳过空白字符
        /// </summary>
        public void SkipWhitespace()
        {
            while (_position < _length && char.IsWhiteSpace(_input[_position]))
            {
                _position++;
            }
        }

        /// <summary>
        /// 前进一个字符
        /// </summary>
        /// <returns>下一个字符，到达末尾返回 '\0'</returns>
        public char Next()
        {
            if (_position >= _length)
            {
                return '\0';
            }
            return _input[_position++];
        }

        /// <summary>
        /// 查看下一个字符（不移动位置）
        /// </summary>
        /// <returns>下一个字符，到达末尾返回 '\0'</returns>
        public char NextCharacter()
        {
            if (_position >= _length)
            {
                return '\0';
            }
            return _input[_position];
        }

        /// <summary>
        /// 回退一个字符
        /// </summary>
        public void Back()
        {
            if (_position > 0)
            {
                _position--;
            }
        }

        /// <summary>
        /// 匹配并消费指定字符
        /// </summary>
        /// <param name="c">期望的字符</param>
        public void Match(char c)
        {
            var next = Next();
            if (next != c)
            {
                throw new JSONException($"Expected '{c}' but found '{next}' at position {_position}");
            }
        }

        /// <summary>
        /// 匹配并消费指定字符串
        /// </summary>
        /// <param name="s">期望的字符串</param>
        public void Match(string s)
        {
            foreach (var c in s)
            {
                Match(c);
            }
        }

        /// <summary>
        /// 查看下一个非空白字符
        /// </summary>
        /// <returns>下一个非空白字符</returns>
        public char NextClean()
        {
            SkipWhitespace();
            return NextCharacter();
        }

        /// <summary>
        /// 读取下一个字符串
        /// </summary>
        /// <returns>字符串值（不包含引号）</returns>
        public string NextString()
        {
            // 消费开始的引号
            var quoteChar = Next();
            if (quoteChar != '"' && quoteChar != '\'')
            {
                throw new JSONException($"Expected quote character but found '{quoteChar}'");
            }

            var sb = new StringBuilder();
            while (_position < _length)
            {
                var c = Next();
                if (c == quoteChar)
                {
                    return sb.ToString();
                }
                if (c == '\\')
                {
                    if (_position >= _length)
                    {
                        throw new JSONException("Unterminated string");
                    }
                    c = Next();
                    switch (c)
                    {
                        case '"': sb.Append('"'); break;
                        case '\'': sb.Append('\''); break;
                        case '\\': sb.Append('\\'); break;
                        case '/': sb.Append('/'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'n': sb.Append('\n'); break;
                        case 'r': sb.Append('\r'); break;
                        case 't': sb.Append('\t'); break;
                        case 'u':
                            var hex = "";
                            for (int i = 0; i < 4; i++)
                            {
                                if (_position < _length)
                                {
                                    hex += Next();
                                }
                                else
                                {
                                    throw new JSONException("Invalid unicode escape");
                                }
                            }
                            sb.Append((char)Convert.ToInt32(hex, 16));
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            throw new JSONException("Unterminated string");
        }

        /// <summary>
        /// 读取下一个数字
        /// </summary>
        /// <returns>数字</returns>
        public object NextNumber()
        {
            var sb = new StringBuilder();
            var c = NextCharacter();

            // 处理负号
            if (c == '-')
            {
                sb.Append(c);
                _position++;
                c = NextCharacter();
            }

            // 处理整数部分
            if (!char.IsDigit(c))
            {
                throw new JSONException($"Invalid number: expected digit at position {_position}");
            }

            while (_position < _length && char.IsDigit(c))
            {
                sb.Append(c);
                _position++;
                c = NextCharacter();
            }

            // 处理小数部分
            if (c == '.')
            {
                sb.Append(c);
                _position++;
                c = NextCharacter();

                if (!char.IsDigit(c))
                {
                    throw new JSONException($"Invalid number: expected digit after decimal point at position {_position}");
                }

                while (_position < _length && char.IsDigit(c))
                {
                    sb.Append(c);
                    _position++;
                    c = NextCharacter();
                }
            }

            // 处理指数部分
            if (c == 'e' || c == 'E')
            {
                sb.Append(c);
                _position++;
                c = NextCharacter();

                // 处理指数符号
                if (c == '+' || c == '-')
                {
                    sb.Append(c);
                    _position++;
                    c = NextCharacter();
                }

                if (!char.IsDigit(c))
                {
                    throw new JSONException($"Invalid number: expected digit in exponent at position {_position}");
                }

                while (_position < _length && char.IsDigit(c))
                {
                    sb.Append(c);
                    _position++;
                    c = NextCharacter();
                }
            }

            var numStr = sb.ToString();

            // 尝试解析为整数
            if (numStr.IndexOf('.') < 0 && numStr.IndexOf('e') < 0 && numStr.IndexOf('E') < 0)
            {
                if (long.TryParse(numStr, out var longVal))
                {
                    if (longVal >= int.MinValue && longVal <= int.MaxValue)
                    {
                        return (int)longVal;
                    }
                    return longVal;
                }
            }

            // 解析为双精度
            if (double.TryParse(numStr, out var doubleVal))
            {
                return doubleVal;
            }

            throw new JSONException($"Invalid number: {numStr}");
        }

        /// <summary>
        /// 读取下一个布尔值
        /// </summary>
        /// <returns>布尔值</returns>
        public bool NextBoolean()
        {
            var start = _position;
            var remaining = _length - _position;

            if (remaining >= 4 && _input.Substring(_position, 4).ToLower() == "true")
            {
                _position += 4;
                return true;
            }

            if (remaining >= 5 && _input.Substring(_position, 5).ToLower() == "false")
            {
                _position += 5;
                return false;
            }

            throw new JSONException($"Expected 'true' or 'false' at position {_position}");
        }

        /// <summary>
        /// 读取下一个 null 值
        /// </summary>
        /// <returns>JSONNull</returns>
        public JSONNull NextNull()
        {
            var remaining = _length - _position;

            if (remaining >= 4 && _input.Substring(_position, 4).ToLower() == "null")
            {
                _position += 4;
                return JSONNull.NULL;
            }

            throw new JSONException($"Expected 'null' at position {_position}");
        }

        /// <summary>
        /// 取到下一个值，跳过空白
        /// </summary>
        /// <returns>JSON支持的类型的值</returns>
        public virtual object NextValue()
        {
            SkipWhitespace();
            if (End())
            {
                throw new JSONException("Unexpected end of JSON input");
            }

            char c = Next();
            return NextValue(c);
        }

        /// <summary>
        /// 取到下一个值，跳过空白
        /// </summary>
        /// <param name="c">当前字符</param>
        /// <returns>JSON支持的类型的值</returns>
        public virtual object NextValue(char c)
        {
            switch (c)
            {
                case '"':
                    return NextString();
                case '[':
                    return ReadArray();
                case '{':
                    return ReadObject();
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    Back();
                    return NextNumber();
                case 't':
                    return ReadTrue();
                case 'f':
                    return ReadFalse();
                case 'n':
                    return ReadNull();
                default:
                    throw new JSONException($"Unexpected character: {c}");
            }
        }

        /// <summary>
        /// 读取数组
        /// </summary>
        /// <returns>JSONArray</returns>
        private object ReadArray()
        {
            var array = new JSONArray();
            SkipWhitespace();
            if (NextClean() == ']')
            {
                return array;
            }
            Back();
            while (true)
            {
                SkipWhitespace();
                var value = NextValue();
                array.Add(value);
                SkipWhitespace();
                var c = NextClean();
                if (c == ']')
                {
                    return array;
                }
                if (c != ',')
                {
                    throw SyntaxError("Expected ',' or ']'");
                }
            }
        }

        /// <summary>
        /// 读取对象
        /// </summary>
        /// <returns>JSONObject</returns>
        private object ReadObject()
        {
            var obj = new JSONObject();
            SkipWhitespace();
            if (NextClean() == '}')
            {
                return obj;
            }
            Back();
            while (true)
            {
                SkipWhitespace();
                var key = NextString();
                SkipWhitespace();
                if (NextClean() != ':')
                {
                    throw SyntaxError("Expected ':' after key");
                }
                var value = NextValue();
                obj.Set(key, value);
                SkipWhitespace();
                var c = NextClean();
                if (c == '}')
                {
                    return obj;
                }
                if (c != ',')
                {
                    throw SyntaxError("Expected ',' or '}'");
                }
            }
        }

        /// <summary>
        /// 读取数字
        /// </summary>
        /// <param name="c">当前字符</param>
        /// <returns>数字</returns>
        private object ReadNumber(char c)
        {
            Back();
            return NextNumber();
        }

        /// <summary>
        /// 读取true
        /// </summary>
        /// <returns>true</returns>
        private object ReadTrue()
        {
            if (_position + 3 < _length && _input.Substring(_position, 4).ToLower() == "true")
            {
                _position += 4;
                return true;
            }
            throw SyntaxError("Expected 'true'");
        }

        /// <summary>
        /// 读取false
        /// </summary>
        /// <returns>false</returns>
        private object ReadFalse()
        {
            if (_position + 4 < _length && _input.Substring(_position, 5).ToLower() == "false")
            {
                _position += 5;
                return false;
            }
            throw SyntaxError("Expected 'false'");
        }

        /// <summary>
        /// 读取null
        /// </summary>
        /// <returns>JSONNull</returns>
        private object ReadNull()
        {
            if (_position + 3 < _length && _input.Substring(_position, 4).ToLower() == "null")
            {
                _position += 4;
                return JSONNull.NULL;
            }
            throw SyntaxError("Expected 'null'");
        }

        /// <summary>
        /// 读取到指定字符
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <returns>字符串</returns>
        public string NextValueString(char separator)
        {
            var sb = new StringBuilder();
            SkipWhitespace();

            while (_position < _length)
            {
                var c = NextCharacter();
                if (c == separator || c == '\0')
                {
                    break;
                }
                sb.Append(c);
                _position++;
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 剩余内容
        /// </summary>
        /// <returns>剩余字符串</returns>
        public string Rest()
        {
            return _input.Substring(_position);
        }

        /// <summary>
        /// 跳过指定字符
        /// </summary>
        /// <param name="c">要跳过的字符</param>
        public void Skip(char c)
        {
            if (NextCharacter() == c)
            {
                _position++;
            }
        }

        /// <summary>
        /// 抛出语法错误异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常</returns>
        public JSONException SyntaxError(string message)
        {
            throw new JSONException($"Syntax error: {message} at position {_position}");
        }

        /// <summary>
        /// 获取前一个字符
        /// </summary>
        /// <returns>前一个字符</returns>
        public char GetPrevious()
        {
            return _position > 0 ? _input[_position - 1] : '\0';
        }

        /// <summary>
        /// 读取下一个字符串值
        /// </summary>
        /// <returns>字符串值</returns>
        public string NextStringValue()
        {
            return NextString();
        }
    }
}
