using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 数组
    /// </summary>
    public class JSONArray : IEnumerable<object>
    {
        private readonly List<object> _list;

        /// <summary>
        /// 构造函数
        /// </summary>
        public JSONArray()
        {
            _list = new List<object>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="list">列表</param>
        public JSONArray(List<object> list)
        {
            _list = list ?? new List<object>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        public JSONArray(string json)
        {
            _list = new List<object>();
            Parse(json);
        }

        /// <summary>
        /// 解析 JSON 字符串
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        private void Parse(string json)
        {
            var tokener = new JSONTokener(json);
            Parse(tokener);
        }

        /// <summary>
        /// 解析 JSON 字符串
        /// </summary>
        /// <param name="tokener">JSON 解析器</param>
        private void Parse(JSONTokener tokener)
        {
            tokener.SkipWhitespace();
            tokener.Match('[');
            tokener.SkipWhitespace();

            while (tokener.NextCharacter() != ']')
            {
                tokener.SkipWhitespace();
                var value = ParseValue(tokener);
                _list.Add(value);
                tokener.SkipWhitespace();
                if (tokener.NextCharacter() == ',')
                {
                    tokener.Next();
                    tokener.SkipWhitespace();
                }
                else if (tokener.NextCharacter() != ']')
                {
                    throw new JSONException($"Expected ',' or ']' at position {tokener.EndOfFile}");
                }
            }

            tokener.Next(); // 跳过 ']'
        }

        /// <summary>
        /// 解析值
        /// </summary>
        /// <param name="tokener">JSON 解析器</param>
        /// <returns>值</returns>
        private object ParseValue(JSONTokener tokener)
        {
            tokener.SkipWhitespace();
            var c = tokener.NextCharacter();

            switch (c)
            {
                case '{':
                    return new JSONObject(tokener);
                case '[':
                    return new JSONArray(tokener);
                case '"':
                    return tokener.NextString();
                case 't':
                    return tokener.NextBoolean();
                case 'f':
                    return tokener.NextBoolean();
                case 'n':
                    return tokener.NextNull();
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
                    return tokener.NextNumber();
                default:
                    throw new JSONException($"Unexpected character at position {tokener.EndOfFile}");
            }
        }

        /// <summary>
        /// 解析 JSON 字符串
        /// </summary>
        /// <param name="tokener">JSON 解析器</param>
        public JSONArray(JSONTokener tokener)
        {
            _list = new List<object>();
            Parse(tokener);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>值</returns>
        public object this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="value">值</param>
        public void Add(object value)
        {
            _list.Add(value);
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>字符串值</returns>
        public string GetString(int index)
        {
            return (string)this[index];
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>整数值</returns>
        public long GetLong(int index)
        {
            return Convert.ToInt64(this[index]);
        }

        /// <summary>
        /// 获取双精度值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>双精度值</returns>
        public double GetDouble(int index)
        {
            return Convert.ToDouble(this[index]);
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>布尔值</returns>
        public bool GetBoolean(int index)
        {
            return Convert.ToBoolean(this[index]);
        }

        /// <summary>
        /// 获取 JSONObject
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>JSONObject</returns>
        public JSONObject GetJSONObject(int index)
        {
            return (JSONObject)this[index];
        }

        /// <summary>
        /// 获取 JSONArray
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>JSONArray</returns>
        public JSONArray GetJSONArray(int index)
        {
            return (JSONArray)this[index];
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <returns>列表</returns>
        public List<object> ToList()
        {
            return new List<object>(_list);
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>JSON 字符串</returns>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append('[');
            var first = true;

            foreach (var value in _list)
            {
                if (!first)
                {
                    sb.Append(',');
                }
                first = false;

                sb.Append(ToString(value));
            }

            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// 将对象转换为字符串
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns>字符串</returns>
        private string ToString(object value)
        {
            if (value == null || value is JSONNull)
            {
                return "null";
            }
            else if (value is string s)
            {
                return '"' + EscapeString(s) + '"';
            }
            else if (value is bool b)
            {
                return b.ToString().ToLower();
            }
            else if (value is JSONObject obj)
            {
                return obj.ToString();
            }
            else if (value is JSONArray arr)
            {
                return arr.ToString();
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// 转义字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>转义后的字符串</returns>
        private string EscapeString(string s)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var c in s)
            {
                switch (c)
                {
                    case '"': sb.Append('\\'); sb.Append('"'); break;
                    case '\\': sb.Append('\\'); sb.Append('\\'); break;
                    case '\b': sb.Append('\\'); sb.Append('b'); break;
                    case '\f': sb.Append('\\'); sb.Append('f'); break;
                    case '\n': sb.Append('\\'); sb.Append('n'); break;
                    case '\r': sb.Append('\\'); sb.Append('r'); break;
                    case '\t': sb.Append('\\'); sb.Append('t'); break;
                    default:
                        if (c < 32)
                        {
                            sb.Append($"\\u{(int)c:X4}");
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<object> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}