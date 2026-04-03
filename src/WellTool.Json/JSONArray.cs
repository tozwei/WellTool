using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 数组
    /// </summary>
    public class JSONArray : JSONBase, IJSONGetter<int>, IEnumerable<object>
    {
        /// <summary>
        /// 默认初始容量
        /// </summary>
        public const int DEFAULT_CAPACITY = 10;

        /// <summary>
        /// 内部存储的列表
        /// </summary>
        private readonly List<object> _list;

        /// <summary>
        /// 创建空的 JSONArray
        /// </summary>
        public JSONArray() : this(DEFAULT_CAPACITY)
        {
        }

        /// <summary>
        /// 创建 JSONArray
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public JSONArray(int capacity)
        {
            _list = new List<object>(capacity);
            Config = JSONConfig.Create();
        }

        /// <summary>
        /// 创建 JSONArray
        /// </summary>
        /// <param name="config">JSON 配置</param>
        public JSONArray(JSONConfig config)
        {
            _list = new List<object>();
            Config = config ?? JSONConfig.Create();
        }

        /// <summary>
        /// 创建 JSONArray
        /// </summary>
        /// <param name="list">初始列表</param>
        public JSONArray(List<object> list)
        {
            _list = list ?? new List<object>();
            Config = JSONConfig.Create();
        }

        /// <summary>
        /// 从 JSON 字符串解析
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        public JSONArray(string json) : this()
        {
            Parse(json);
        }

        /// <summary>
        /// 从数组或集合创建
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合</param>
        public JSONArray(object arrayOrCollection) : this()
        {
            ParseFromEnumerable(arrayOrCollection);
        }

        /// <summary>
        /// 从数组或集合创建
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合</param>
        /// <param name="ignoreNullValue">是否忽略 null 值</param>
        public JSONArray(object arrayOrCollection, bool ignoreNullValue) : this()
        {
            Config.SetIgnoreNullValue(ignoreNullValue);
            ParseFromEnumerable(arrayOrCollection);
        }

        /// <summary>
        /// 从数组或集合创建
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合</param>
        /// <param name="config">JSON 配置</param>
        public JSONArray(object arrayOrCollection, JSONConfig config) : this()
        {
            Config = config ?? JSONConfig.Create();
            ParseFromEnumerable(arrayOrCollection);
        }

        /// <summary>
        /// 从 JSONTokener 解析
        /// </summary>
        /// <param name="tokener">JSON 解析器</param>
        public JSONArray(JSONTokener tokener)
        {
            _list = new List<object>();
            Config = JSONConfig.Create();
            Parse(tokener);
        }

        /// <summary>
        /// 解析 JSON 字符串
        /// </summary>
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
                    throw new JSONException($"Expected ',' or ']' at position {tokener.Position}");
                }
            }

            tokener.Next(); // 跳过 ']'
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
        /// 从可枚举对象解析
        /// </summary>
        private void ParseFromEnumerable(object arrayOrCollection)
        {
            if (arrayOrCollection == null)
            {
                return;
            }

            // 如果是 JSON 字符串
            if (arrayOrCollection is string str)
            {
                Parse(str);
                return;
            }

            // 如果是 IEnumerable
            if (arrayOrCollection is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    var value = WrapValue(item);
                    if (value != null || !Config.IsIgnoreNullValue())
                    {
                        _list.Add(value);
                    }
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

            // 其他对象包装为 JSONObject
            return new JSONObject(value, Config);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>值</returns>
        public object this[int index]
        {
            get => index >= 0 && index < _list.Count ? _list[index] : null;
            set => _list[index] = value;
        }

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONArray Set(object value)
        {
            var wrappedValue = WrapValue(value);
            if (wrappedValue != null || !Config.IsIgnoreNullValue())
            {
                _list.Add(wrappedValue);
            }
            return this;
        }

        /// <summary>
        /// 添加原始值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public JSONArray AddRaw(object value)
        {
            _list.Add(value);
            return this;
        }

        /// <summary>
        /// 添加原始值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="filter">键值对过滤编辑器</param>
        /// <returns>this</returns>
        public JSONArray AddRaw(object value, Func<string, object, bool> filter)
        {
            _list.Add(value);
            return this;
        }

        /// <summary>
        /// 添加原始值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="filter">值过滤编辑器</param>
        /// <returns>this</returns>
        public JSONArray AddRaw(object value, Func<object, bool> filter)
        {
            _list.Add(value);
            return this;
        }

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        public bool Add(object value)
        {
            var wrappedValue = WrapValue(value);
            if (wrappedValue == null && Config.IsIgnoreNullValue())
            {
                return false;
            }
            _list.Add(wrappedValue);
            return true;
        }

        /// <summary>
        /// 在指定位置插入值
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="value">值</param>
        public void Insert(int index, object value)
        {
            var wrappedValue = WrapValue(value);
            if (wrappedValue == null && Config.IsIgnoreNullValue())
            {
                return;
            }
            _list.Insert(index, wrappedValue);
        }

        /// <summary>
        /// 设置指定位置的值
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="value">值</param>
        /// <returns>之前的值</returns>
        public object Set(int index, object value)
        {
            if (index < 0)
            {
                throw new JSONException($"Index cannot be negative: {index}");
            }

            if (index >= _list.Count)
            {
                // 填充 null 值
                while (_list.Count < index)
                {
                    _list.Add(JSONNull.NULL);
                }
                Add(value);
                return null;
            }

            var oldValue = _list[index];
            _list[index] = WrapValue(value);
            return oldValue;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string GetStr(int index)
        {
            var value = this[index];
            return value as string;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string GetString(int index)
        {
            return GetStr(index);
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        public int GetInt(int index)
        {
            var value = this[index];
            if (value == null) return 0;
            if (value is int intVal) return intVal;
            if (value is long longVal) return (int)longVal;
            if (value is double doubleVal) return (int)doubleVal;
            if (int.TryParse(value.ToString(), out var result)) return result;
            return 0;
        }

        /// <summary>
        /// 获取整数值，带默认值
        /// </summary>
        public int GetInt(int index, int defaultValue)
        {
            var value = this[index];
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
        public long GetLong(int index)
        {
            var value = this[index];
            if (value == null) return 0;
            if (value is long longVal) return longVal;
            if (value is int intVal) return intVal;
            if (value is double doubleVal) return (long)doubleVal;
            if (long.TryParse(value.ToString(), out var result)) return result;
            return 0;
        }

        /// <summary>
        /// 获取长整型值，带默认值
        /// </summary>
        public long GetLong(int index, long defaultValue)
        {
            var value = this[index];
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
        public double GetDouble(int index)
        {
            var value = this[index];
            if (value == null) return 0.0;
            if (value is double doubleVal) return doubleVal;
            if (value is int intVal) return intVal;
            if (value is long longVal) return longVal;
            if (double.TryParse(value.ToString(), out var result)) return result;
            return 0.0;
        }

        /// <summary>
        /// 获取双精度值，带默认值
        /// </summary>
        public double GetDouble(int index, double defaultValue)
        {
            var value = this[index];
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
        public bool GetBool(int index)
        {
            var value = this[index];
            if (value == null) return false;
            if (value is bool boolVal) return boolVal;
            if (bool.TryParse(value.ToString(), out var result)) return result;
            return false;
        }

        /// <summary>
        /// 获取布尔值，带默认值
        /// </summary>
        public bool GetBool(int index, bool defaultValue)
        {
            var value = this[index];
            if (value == null) return defaultValue;
            if (value is bool boolVal) return boolVal;
            if (bool.TryParse(value.ToString(), out var result)) return result;
            return defaultValue;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBoolean(int index)
        {
            return GetBool(index);
        }

        /// <summary>
        /// 获取布尔值，带默认值
        /// </summary>
        public bool GetBoolean(int index, bool defaultValue)
        {
            return GetBool(index, defaultValue);
        }

        /// <summary>
        /// 获取 JSON 对象
        /// </summary>
        public JSONObject GetJSONObject(int index)
        {
            var value = this[index];
            return value as JSONObject;
        }

        /// <summary>
        /// 获取 JSON 数组
        /// </summary>
        public JSONArray GetJSONArray(int index)
        {
            var value = this[index];
            return value as JSONArray;
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        public object GetObj(int index)
        {
            return this[index];
        }

        /// <summary>
        /// 获取对象值，带默认值
        /// </summary>
        public object GetObj(int index, object defaultValue)
        {
            var value = this[index];
            return value ?? defaultValue;
        }

        /// <summary>
        /// 根据给定名列表，与其位置对应的值组成 JSONObject
        /// </summary>
        /// <param name="names">名列表</param>
        /// <returns>JSONObject</returns>
        public JSONObject ToJSONObject(JSONArray names)
        {
            if (names == null || names.Count == 0 || Count == 0)
            {
                return null;
            }

            var jo = new JSONObject(Config);
            for (int i = 0; i < names.Count; i++)
            {
                var name = names.GetStr(i);
                if (name != null)
                {
                    jo[name] = GetObj(i);
                }
            }
            return jo;
        }

        /// <summary>
        /// JSONArray 转为以 separator 为分界符的字符串
        /// </summary>
        /// <param name="separator">分界符</param>
        /// <returns>字符串</returns>
        public string Join(string separator)
        {
            var sb = new StringBuilder();
            var first = true;

            foreach (var item in _list)
            {
                if (!first)
                {
                    sb.Append(separator);
                }
                first = false;

                if (item == null || item == JSONNull.NULL)
                {
                    sb.Append("null");
                }
                else if (item is string str)
                {
                    sb.Append('"');
                    sb.Append(str);
                    sb.Append('"');
                }
                else
                {
                    sb.Append(item.ToString());
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _list.Count == 0;

        /// <summary>
        /// 检查是否包含值
        /// </summary>
        public bool Contains(object value)
        {
            return _list.Contains(value);
        }

        /// <summary>
        /// 移除指定位置的值
        /// </summary>
        public object Remove(int index)
        {
            if (index >= 0 && index < _list.Count)
            {
                var value = _list[index];
                _list.RemoveAt(index);
                return value;
            }
            return null;
        }

        /// <summary>
        /// 移除值
        /// </summary>
        public bool Remove(object value)
        {
            return _list.Remove(value);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// 转为列表
        /// </summary>
        public List<object> ToList()
        {
            return new List<object>(_list);
        }

        /// <summary>
        /// 转为指定类型的列表
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <returns>List</returns>
        public List<T> ToList<T>()
        {
            var list = new List<T>();
            foreach (var item in _list)
            {
                if (item == null || item == JSONNull.NULL)
                {
                    list.Add(default(T));
                }
                else if (item is T t)
                {
                    list.Add(t);
                }
                else
                {
                    list.Add(JSONConverter.JsonConvert<T>(item, Config));
                }
            }
            return list;
        }

        /// <summary>
        /// 转为数组
        /// </summary>
        /// <param name="arrayClass">数组元素类型</param>
        /// <returns>数组</returns>
        public object ToArray(Type arrayClass)
        {
            return JSONConverter.ToArray(this, arrayClass);
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

            // 表达式应该是 [index] 或 [index].xxx 格式
            if (expression.StartsWith("["))
            {
                var endBracket = expression.IndexOf(']');
                if (endBracket > 0)
                {
                    var indexStr = expression.Substring(1, endBracket - 1);
                    var index = int.Parse(indexStr);
                    var value = this[index];

                    if (endBracket < expression.Length - 1)
                    {
                        var nextExpr = expression.Substring(endBracket + 2); // 跳过 ]. 或 ].
                        if (value is JSONObject obj)
                        {
                            return obj.GetByPath(nextExpr);
                        }
                        if (value is JSONArray arr)
                        {
                            return arr.GetByPath(nextExpr);
                        }
                    }

                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// 设置表达式指定位置的值
        /// </summary>
        public override void PutByPath(string expression, object value)
        {
            if (string.IsNullOrEmpty(expression) || !expression.StartsWith("["))
            {
                return;
            }

            var endBracket = expression.IndexOf(']');
            if (endBracket > 0)
            {
                var indexStr = expression.Substring(1, endBracket - 1);
                var index = int.Parse(indexStr);

                if (endBracket < expression.Length - 1)
                {
                    var nextExpr = expression.Substring(endBracket + 2);
                    var currentValue = this[index];

                    if (currentValue is JSONObject obj)
                    {
                        obj.PutByPath(nextExpr, value);
                    }
                    else if (currentValue is JSONArray arr)
                    {
                        arr.PutByPath(nextExpr, value);
                    }
                }
                else
                {
                    this[index] = value;
                }
            }
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        public override T ToBean<T>()
        {
            return (T)ToBean(typeof(T));
        }

        /// <summary>
        /// 转换为指定类型的列表
        /// </summary>
        public List<T> ToBeanList<T>()
        {
            return ToList<T>();
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        public override object ToBean(Type type)
        {
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var list = ToList();
                var array = Array.CreateInstance(elementType, list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    array.SetValue(JSONConverter.JsonConvert(elementType, list[i], Config), i);
                }
                return array;
            }

            if (type.IsGenericType)
            {
                var genericType = type.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(genericType);
                var list = (IList)Activator.CreateInstance(listType);
                foreach (var item in _list)
                {
                    list.Add(JSONConverter.JsonConvert(genericType, item, Config));
                }
                return list;
            }

            return ToList();
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
            writer.Write('[');
            var first = true;

            foreach (var item in _list)
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

                WriteValue(writer, item, indentFactor, indent);
            }

            if (indentFactor > 0)
            {
                writer.WriteLine();
                writer.Write(new string(' ', indent));
            }

            writer.Write(']');
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
        public IEnumerator<object> GetEnumerator()
        {
            return _list.GetEnumerator();
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
        public JSONArray Clone()
        {
            var clone = new JSONArray(Config.Copy());
            foreach (var item in _list)
            {
                clone._list.Add(CloneValue(item));
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
        /// 返回索引位置
        /// </summary>
        public int IndexOf(object value)
        {
            return _list.IndexOf(value);
        }

        /// <summary>
        /// 返回最后索引位置
        /// </summary>
        public int LastIndexOf(object value)
        {
            return _list.LastIndexOf(value);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        public void AddRange(IEnumerable<object> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// JSON 迭代器
        /// </summary>
        public IEnumerable<JSONObject> JsonIter()
        {
            foreach (var item in _list)
            {
                if (item is JSONObject obj)
                {
                    yield return obj;
                }
            }
        }
    }
}
