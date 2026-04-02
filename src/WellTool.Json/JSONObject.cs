using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 对象
    /// </summary>
    public class JSONObject : JSONBase, IJSONGetter<string>, IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// 内部存储的字典
        /// </summary>
        private readonly Dictionary<string, object> _map;

        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        private bool _ignoreCase;

        /// <summary>
        /// 创建空的 JSONObject
        /// </summary>
        public JSONObject() : this(16)
        {
        }

        /// <summary>
        /// 创建 JSONObject
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public JSONObject(int capacity)
        {
            _map = new Dictionary<string, object>(capacity);
            Config = JSONConfig.Create();
        }

        /// <summary>
        /// 创建 JSONObject
        /// </summary>
        /// <param name="config">JSON 配置</param>
        public JSONObject(JSONConfig config)
        {
            _map = new Dictionary<string, object>();
            Config = config ?? JSONConfig.Create();
            _ignoreCase = Config.IsIgnoreCase();
        }

        /// <summary>
        /// 创建 JSONObject
        /// </summary>
        /// <param name="map">初始字典</param>
        public JSONObject(Dictionary<string, object> map)
        {
            _map = map ?? new Dictionary<string, object>();
            Config = JSONConfig.Create();
        }

        /// <summary>
        /// 从 JSON 字符串解析
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        public JSONObject(string json)
        {
            _map = new Dictionary<string, object>();
            Config = JSONConfig.Create();
            Parse(json);
        }

        /// <summary>
        /// 从对象创建
        /// </summary>
        /// <param name="source">源对象</param>
        public JSONObject(object source) : this(source, JSONConfig.Create())
        {
        }

        /// <summary>
        /// 从对象创建
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="ignoreNullValue">是否忽略 null 值</param>
        public JSONObject(object source, bool ignoreNullValue)
        {
            _map = new Dictionary<string, object>();
            Config = JSONConfig.Create().SetIgnoreNullValue(ignoreNullValue);
            ParseFromObject(source);
        }

        /// <summary>
        /// 从对象创建
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="config">JSON 配置</param>
        public JSONObject(object source, JSONConfig config)
        {
            _map = new Dictionary<string, object>();
            Config = config ?? JSONConfig.Create();
            _ignoreCase = Config.IsIgnoreCase();
            ParseFromObject(source);
        }

        /// <summary>
        /// 从 JSONTokener 解析
        /// </summary>
        /// <param name="tokener">JSON 解析器</param>
        public JSONObject(JSONTokener tokener)
        {
            _map = new Dictionary<string, object>();
            Config = JSONConfig.Create();
            Parse(tokener);
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
        /// 从 JSONTokener 解析
        /// </summary>
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
                    throw new JSONException($"Expected ',' or '}}' at position {tokener.Position}");
                }
            }

            tokener.Next(); // 跳过 '}'
        }

        /// <summary>
        /// 解析值
        /// </summary>
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
                    throw new JSONException($"Unexpected character at position {tokener.Position}");
            }
        }

        /// <summary>
        /// 从对象解析
        /// </summary>
        private void ParseFromObject(object source)
        {
            if (source == null)
            {
                return;
            }

            // 如果是字典或 IDictionary
            if (source is IDictionary dict)
            {
                foreach (DictionaryEntry entry in dict)
                {
                    var key = entry.Key?.ToString();
                    if (key != null)
                    {
                        var value = WrapValue(entry.Value);
                        if (value != null || !Config.IsIgnoreNullValue())
                        {
                            _map[key] = value;
                        }
                    }
                }
                return;
            }

            // 如果是 IEnumerable 但不是字符串
            if (source is IEnumerable enumerable && !(source is string))
            {
                throw new JSONException($"Cannot convert IEnumerable to JSONObject");
            }

            // 使用反射获取属性
            var type = source.GetType();
            var properties = type.GetProperties();
            foreach (var prop in properties)
            {
                if (!prop.CanRead)
                {
                    continue;
                }

                // 跳过索引属性
                if (prop.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                try
                {
                    var value = prop.GetValue(source);
                    var jsonValue = WrapValue(value);

                    if (jsonValue != null || !Config.IsIgnoreNullValue())
                    {
                        _map[prop.Name] = jsonValue;
                    }
                }
                catch
                {
                    // 忽略属性读取异常
                }
            }

            // 获取字段
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                try
                {
                    var value = field.GetValue(source);
                    var jsonValue = WrapValue(value);

                    if (jsonValue != null || !Config.IsIgnoreNullValue())
                    {
                        _map[field.Name] = jsonValue;
                    }
                }
                catch
                {
                    // 忽略字段读取异常
                }
            }
        }

        /// <summary>
        /// 包装值
        /// </summary>
        private object WrapValue(object value)
        {
            if (value == null)
            {
                return Config.IsIgnoreNullValue() ? null : JSONNull.NULL;
            }

            if (value is string || value is bool || value is int || value is long ||
                value is double || value is float || value is decimal || value is JSONNull ||
                value is JSONObject || value is JSONArray)
            {
                return value;
            }

            if (value is DateTime dt)
            {
                if (!string.IsNullOrEmpty(Config.GetDateFormat()))
                {
                    return dt.ToString(Config.GetDateFormat());
                }
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (value is IDictionary dict)
            {
                return new JSONObject(dict);
            }

            if (value is IEnumerable enumerable && !(value is string))
            {
                return new JSONArray(enumerable);
            }

            // 枚举使用字符串形式
            if (value is Enum)
            {
                return value.ToString();
            }

            // 基本类型包装
            if (value is sbyte || value is byte || value is short || value is ushort ||
                value is uint || value is ulong || value is char)
            {
                return Convert.ToInt64(value);
            }

            // 其他对象包装为 JSONObject
            return new JSONObject(value, Config);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object this[string key]
        {
            get
            {
                var actualKey = GetActualKey(key);
                return actualKey != null ? _map[actualKey] : null;
            }
            set
            {
                Set(key, value);
            }
        }

        /// <summary>
        /// 获取实际的键（考虑大小写）
        /// </summary>
        private string GetActualKey(string key)
        {
            if (_ignoreCase)
            {
                return _map.Keys.FirstOrDefault(k => k.Equals(key, StringComparison.OrdinalIgnoreCase));
            }
            return _map.ContainsKey(key) ? key : null;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONObject Set(string key, object value)
        {
            if (key == null)
            {
                return this;
            }

            var wrappedValue = WrapValue(value);

            if (wrappedValue == null && Config.IsIgnoreNullValue())
            {
                Remove(key);
                return this;
            }

            if (Config.IsCheckDuplicate() && _map.ContainsKey(key))
            {
                throw new JSONException($"Duplicate key \"{key}\"");
            }

            _map[key] = wrappedValue;
            return this;
        }

        /// <summary>
        /// 添加键值对，如果键已存在则抛出异常
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONObject PutOnce(string key, object value)
        {
            if (key != null && ContainsKey(key))
            {
                throw new JSONException($"Duplicate key \"{key}\"");
            }
            return Set(key, value);
        }

        /// <summary>
        /// 仅在键和值都不为空时添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONObject PutOpt(string key, object value)
        {
            if (key != null && value != null)
            {
                Set(key, value);
            }
            return this;
        }

        /// <summary>
        /// 累加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONObject Accumulate(string key, object value)
        {
            var currentValue = this[key];
            if (currentValue == null)
            {
                Set(key, value);
            }
            else if (currentValue is JSONArray arr)
            {
                arr.Set(value);
            }
            else
            {
                Set(key, new JSONArray(new[] { currentValue, value }));
            }
            return this;
        }

        /// <summary>
        /// 追加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONObject Append(string key, object value)
        {
            var currentValue = this[key];
            if (currentValue == null)
            {
                Set(key, new JSONArray(new[] { value }));
            }
            else if (currentValue is JSONArray arr)
            {
                arr.Set(value);
            }
            else
            {
                throw new JSONException($"JSONObject [{key}] is not a JSONArray.");
            }
            return this;
        }

        /// <summary>
        /// 数值加一
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>this</returns>
        public JSONObject Increment(string key)
        {
            var value = this[key];
            if (value == null)
            {
                Set(key, 1);
            }
            else if (value is int intVal)
            {
                Set(key, intVal + 1);
            }
            else if (value is long longVal)
            {
                Set(key, longVal + 1);
            }
            else if (value is double doubleVal)
            {
                Set(key, doubleVal + 1);
            }
            else if (value is float floatVal)
            {
                Set(key, floatVal + 1);
            }
            else if (value is decimal decimalVal)
            {
                Set(key, decimalVal + 1);
            }
            else
            {
                throw new JSONException($"Unable to increment [{key}].");
            }
            return this;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string GetStr(string key)
        {
            var value = this[key];
            return value as string;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string GetString(string key)
        {
            return GetStr(key);
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        public int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        public int GetInt(string key, int defaultValue)
        {
            var value = this[key];
            if (value == null) return defaultValue;
            if (value is int intVal) return intVal;
            if (value is long longVal) return (int)longVal;
            if (value is double doubleVal) return (int)doubleVal;
            if (int.TryParse(value.ToString(), out var result)) return result;
            return defaultValue;
        }

        /// <summary>
        /// 获取长整型值
        /// </summary>
        public long GetLong(string key)
        {
            return GetLong(key, 0);
        }

        /// <summary>
        /// 获取长整型值
        /// </summary>
        public long GetLong(string key, long defaultValue)
        {
            var value = this[key];
            if (value == null) return defaultValue;
            if (value is long longVal) return longVal;
            if (value is int intVal) return intVal;
            if (value is double doubleVal) return (long)doubleVal;
            if (long.TryParse(value.ToString(), out var result)) return result;
            return defaultValue;
        }

        /// <summary>
        /// 获取双精度值
        /// </summary>
        public double GetDouble(string key)
        {
            return GetDouble(key, 0.0);
        }

        /// <summary>
        /// 获取双精度值
        /// </summary>
        public double GetDouble(string key, double defaultValue)
        {
            var value = this[key];
            if (value == null) return defaultValue;
            if (value is double doubleVal) return doubleVal;
            if (value is int intVal) return intVal;
            if (value is long longVal) return longVal;
            if (double.TryParse(value.ToString(), out var result)) return result;
            return defaultValue;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBool(string key)
        {
            return GetBool(key, false);
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBool(string key, bool defaultValue)
        {
            var value = this[key];
            if (value == null) return defaultValue;
            if (value is bool boolVal) return boolVal;
            if (bool.TryParse(value.ToString(), out var result)) return result;
            return defaultValue;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBoolean(string key)
        {
            return GetBool(key);
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBoolean(string key, bool defaultValue)
        {
            return GetBool(key, defaultValue);
        }

        /// <summary>
        /// 获取 JSON 对象
        /// </summary>
        public JSONObject GetJSONObject(string key)
        {
            var value = this[key];
            return value as JSONObject;
        }

        /// <summary>
        /// 获取 JSON 数组
        /// </summary>
        public JSONArray GetJSONArray(string key)
        {
            var value = this[key];
            return value as JSONArray;
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        public object GetObj(string key)
        {
            return this[key];
        }

        /// <summary>
        /// 获取对象值，带默认值
        /// </summary>
        public object GetObj(string key, object defaultValue)
        {
            var value = this[key];
            return value ?? defaultValue;
        }

        /// <summary>
        /// 检查是否包含键
        /// </summary>
        public bool ContainsKey(string key)
        {
            if (_ignoreCase)
            {
                return _map.Keys.Any(k => k.Equals(key, StringComparison.OrdinalIgnoreCase));
            }
            return _map.ContainsKey(key);
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public ICollection<string> Keys => _map.Keys;

        /// <summary>
        /// 获取所有值
        /// </summary>
        public ICollection<object> Values => _map.Values;

        /// <summary>
        /// 获取键值对数量
        /// </summary>
        public int Count => _map.Count;

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _map.Count == 0;

        /// <summary>
        /// 移除键
        /// </summary>
        public object Remove(string key)
        {
            if (_map.TryGetValue(key, out var value))
            {
                _map.Remove(key);
                return value;
            }
            return null;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _map.Clear();
        }

        /// <summary>
        /// 将指定 KEY 列表的值组成新的 JSONArray
        /// </summary>
        /// <param name="names">KEY 列表</param>
        /// <returns>JSONArray</returns>
        public JSONArray ToJSONArray(IEnumerable<string> names)
        {
            var array = new JSONArray();
            foreach (var name in names)
            {
                var value = this[name];
                if (value != null)
                {
                    array.Set(value);
                }
            }
            return array;
        }

        /// <summary>
        /// 通过表达式获取 JSON 中嵌套的对象
        /// </summary>
        public override object GetByPath(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return this;
            }

            var parts = expression.Split('.');
            object current = this;

            foreach (var part in parts)
            {
                if (current == null)
                {
                    return null;
                }

                // 处理数组索引
                if (part.Contains("["))
                {
                    var namePart = part.Substring(0, part.IndexOf("["));
                    var indexPart = part.Substring(part.IndexOf("[") + 1);
                    var index = int.Parse(indexPart.TrimEnd(']'));

                    if (!string.IsNullOrEmpty(namePart))
                    {
                        current = GetNestedValue(current, namePart);
                    }

                    if (current is JSONArray arr)
                    {
                        current = arr[index];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    current = GetNestedValue(current, part);
                }
            }

            return current;
        }

        /// <summary>
        /// 获取嵌套值
        /// </summary>
        private object GetNestedValue(object obj, string key)
        {
            if (obj is JSONObject obj2)
            {
                return obj2[key];
            }
            if (obj is IDictionary dict)
            {
                return dict[key];
            }
            return null;
        }

        /// <summary>
        /// 设置表达式指定位置的值
        /// </summary>
        public override void PutByPath(string expression, object value)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return;
            }

            var parts = expression.Split('.');
            object current = this;

            for (int i = 0; i < parts.Length - 1; i++)
            {
                var part = parts[i];
                if (part.Contains("["))
                {
                    var namePart = part.Substring(0, part.IndexOf("["));
                    var indexPart = part.Substring(part.IndexOf("[") + 1);
                    var index = int.Parse(indexPart.TrimEnd(']'));

                    if (!string.IsNullOrEmpty(namePart))
                    {
                        current = GetNestedValue(current, namePart);
                    }

                    if (current is JSONArray arr)
                    {
                        // 确保数组有足够长度
                        while (arr.Count <= index)
                        {
                            arr.Set(JSONNull.NULL);
                        }
                        current = arr[index];
                    }
                }
                else
                {
                    var nested = GetNestedValue(current, part);
                    if (nested == null)
                    {
                        nested = new JSONObject();
                        if (current is JSONObject obj)
                        {
                            obj[part] = nested;
                        }
                    }
                    current = nested;
                }
            }

            var lastPart = parts[parts.Length - 1];
            if (current is JSONObject obj2)
            {
                obj2[lastPart] = value;
            }
            else if (current is JSONArray arr)
            {
                var index = int.Parse(lastPart.TrimEnd(']'));
                while (arr.Count <= index)
                {
                    arr.Set(JSONNull.NULL);
                }
                arr[index] = value;
            }
        }

        /// <summary>
        /// 转换为字典
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>(_map);
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        public override T ToBean<T>()
        {
            return (T)ToBean(typeof(T));
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        public override object ToBean(Type type)
        {
            return JSONConverter.JsonConvert(type, this, Config);
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        public override string ToString()
        {
            return ToJSONString(0);
        }

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        public override string ToJSONString(int indentFactor)
        {
            using var writer = new StringWriter();
            Write(writer, indentFactor, 0);
            return writer.ToString();
        }

        /// <summary>
        /// 写入到 Writer
        /// </summary>
        public override void Write(TextWriter writer, int indentFactor, int indent)
        {
            writer.Write('{');
            var first = true;

            var keys = _map.Keys.ToList();
            if (Config.GetKeyComparator() != null)
            {
                keys.Sort(Config.GetKeyComparator());
            }

            foreach (var key in keys)
            {
                if (!first)
                {
                    writer.Write(',');
                }
                first = false;

                if (indentFactor > 0)
                {
                    writer.WriteLine();
                    writer.Write(new string(' ', indent + indentFactor));
                }

                writer.Write('"');
                writer.Write(EscapeString(key));
                writer.Write('"');
                writer.Write(':');

                if (indentFactor > 0)
                {
                    writer.Write(' ');
                }

                WriteValue(writer, _map[key], indentFactor, indent);
            }

            if (indentFactor > 0)
            {
                writer.WriteLine();
                writer.Write(new string(' ', indent));
            }

            writer.Write('}');
        }

        /// <summary>
        /// 写入值
        /// </summary>
        private void WriteValue(TextWriter writer, object value, int indentFactor, int indent)
        {
            if (value == null || value == JSONNull.NULL)
            {
                writer.Write("null");
            }
            else if (value is string str)
            {
                writer.Write('"');
                writer.Write(EscapeString(str));
                writer.Write('"');
            }
            else if (value is bool b)
            {
                writer.Write(b ? "true" : "false");
            }
            else if (value is int || value is long || value is double || value is float || value is decimal)
            {
                writer.Write(FormatNumber(value));
            }
            else if (value is JSONObject obj)
            {
                obj.Write(writer, indentFactor, indent + indentFactor);
            }
            else if (value is JSONArray arr)
            {
                arr.Write(writer, indentFactor, indent + indentFactor);
            }
            else
            {
                writer.Write('"');
                writer.Write(EscapeString(value.ToString()));
                writer.Write('"');
            }
        }

        /// <summary>
        /// 格式化数字
        /// </summary>
        private string FormatNumber(object value)
        {
            if (Config.IsStripTrailingZeros() && value is double d)
            {
                if (d == Math.Floor(d) && !double.IsInfinity(d))
                {
                    return ((long)d).ToString();
                }
            }
            return value.ToString();
        }

        /// <summary>
        /// 转义字符串
        /// </summary>
        private static string EscapeString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var sb = new StringBuilder();
            foreach (var c in s)
            {
                switch (c)
                {
                    case '"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\b': sb.Append("\\b"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
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
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 克隆
        /// </summary>
        public JSONObject Clone()
        {
            var clone = new JSONObject(Config.Copy());
            foreach (var kvp in _map)
            {
                clone._map[kvp.Key] = CloneValue(kvp.Value);
            }
            return clone;
        }

        /// <summary>
        /// 克隆值
        /// </summary>
        private object CloneValue(object value)
        {
            if (value == null || value == JSONNull.NULL)
            {
                return value;
            }
            if (value is JSONObject obj)
            {
                return obj.Clone();
            }
            if (value is JSONArray arr)
            {
                return arr.Clone();
            }
            return value;
        }

        /// <summary>
        /// 添加多个键值对
        /// </summary>
        public void AddAll(IDictionary<string, object> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Set(kvp.Key, kvp.Value);
            }
        }
    }
}
