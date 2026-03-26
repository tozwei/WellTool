using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 对象
    /// </summary>
    public class JSONObject : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _map;

        /// <summary>
        /// 构造函数
        /// </summary>
        public JSONObject()
        {
            _map = new Dictionary<string, object>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="map">字典</param>
        public JSONObject(Dictionary<string, object> map)
        {
            _map = map ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        public JSONObject(string json)
        {
            _map = new Dictionary<string, object>();
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
            tokener.Match('{');
            tokener.SkipWhitespace();

            while (tokener.NextCharacter() != '}')
            {
                tokener.SkipWhitespace();
                var key = tokener.NextString();
                tokener.SkipWhitespace();
                tokener.Match(':');
                tokener.SkipWhitespace();
                var value = ParseValue(tokener);
                _map[key] = value;
                tokener.SkipWhitespace();
                if (tokener.NextCharacter() == ',')
                {
                    tokener.Next();
                    tokener.SkipWhitespace();
                }
                else if (tokener.NextCharacter() != '}')
                {
                    throw new JSONException($"Expected ',' or '}}' at position {tokener.EndOfFile}");
                }
            }

            tokener.Next(); // 跳过 '}'
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
        public JSONObject(JSONTokener tokener)
        {
            _map = new Dictionary<string, object>();
            Parse(tokener);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object this[string key]
        {
            get { return _map.TryGetValue(key, out var value) ? value : null; }
            set { _map[key] = value; }
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Put(string key, object value)
        {
            _map[key] = value;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串值</returns>
        public string GetString(string key)
        {
            return (string)this[key];
        }

        /// <summary>
        /// 获取整数���
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>整数值</returns>
        public long GetLong(string key)
        {
            return Convert.ToInt64(this[key]);
        }

        /// <summary>
        /// 获取双精度值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>双精度值</returns>
        public double GetDouble(string key)
        {
            return Convert.ToDouble(this[key]);
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>布尔值</returns>
        public bool GetBoolean(string key)
        {
            return Convert.ToBoolean(this[key]);
        }

        /// <summary>
        /// 获取 JSONObject
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>JSONObject</returns>
        public JSONObject GetJSONObject(string key)
        {
            return (JSONObject)this[key];
        }

        /// <summary>
        /// 获取 JSONArray
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>JSONArray</returns>
        public JSONArray GetJSONArray(string key)
        {
            return (JSONArray)this[key];
        }

        /// <summary>
        /// 检查是否包含键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(string key)
        {
            return _map.ContainsKey(key);
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public ICollection<string> Keys
        {
            get { return _map.Keys; }
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        public ICollection<object> Values
        {
            get { return _map.Values; }
        }

        /// <summary>
        /// 获取键值对数量
        /// </summary>
        public int Count
        {
            get { return _map.Count; }
        }

        /// <summary>
        /// 转换为字典
        /// </summary>
        /// <returns>字典</returns>
        public Dictionary<string, object> ToDictionary()
        {
            return _map.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>JSON 字符串</returns>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append('{');
            var first = true;

            foreach (var kv in _map)
            {
                if (!first)
                {
                    sb.Append(',');
                }
                first = false;

                sb.Append('"');
                sb.Append(EscapeString(kv.Key));
                sb.Append('"');
                sb.Append(':');
                sb.Append(ToString(kv.Value));
            }

            sb.Append('}');
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
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _map.GetEnumerator();
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