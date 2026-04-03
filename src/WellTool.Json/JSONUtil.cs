using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 工具类
    /// </summary>
    public static class JSONUtil
    {
        #region 创建方法

        /// <summary>
        /// 创建 JSONObject
        /// </summary>
        /// <returns>JSONObject</returns>
        public static JSONObject CreateObj()
        {
            return new JSONObject();
        }

        /// <summary>
        /// 创建 JSONObject
        /// </summary>
        /// <param name="config">JSON 配置</param>
        /// <returns>JSONObject</returns>
        public static JSONObject CreateObj(JSONConfig config)
        {
            return new JSONObject(config);
        }

        /// <summary>
        /// 创建 JSONArray
        /// </summary>
        /// <returns>JSONArray</returns>
        public static JSONArray CreateArray()
        {
            return new JSONArray();
        }

        /// <summary>
        /// 创建 JSONArray
        /// </summary>
        /// <param name="config">JSON 配置</param>
        /// <returns>JSONArray</returns>
        public static JSONArray CreateArray(JSONConfig config)
        {
            return new JSONArray(config);
        }

        #endregion

        #region 解析方法

        /// <summary>
        /// JSON 字符串转 JSONObject 对象
        /// </summary>
        /// <param name="jsonStr">JSON 字符串</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObj(string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                return null;
            }
            return new JSONObject(jsonStr);
        }

        /// <summary>
        /// JSON 字符串转 JSONObject 对象
        /// </summary>
        /// <param name="obj">Bean 对象或者 Map</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObj(object obj)
        {
            return ParseObj(obj, (JSONConfig)null);
        }

        /// <summary>
        /// JSON 字符串转 JSONObject 对象
        /// </summary>
        /// <param name="obj">Bean 对象或者 Map</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObject(object obj)
        {
            return ParseObj(obj);
        }

        /// <summary>
        /// 格式化 JSON 字符串
        /// </summary>
        /// <param name="jsonStr">JSON 字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string jsonStr)
        {
            return FormatJsonStr(jsonStr);
        }

        /// <summary>
        /// 格式化 JSON 对象
        /// </summary>
        /// <param name="json">JSON 对象</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(JSONBase json)
        {
            if (json == null)
            {
                return null;
            }
            return json.ToJSONString(4);
        }

        /// <summary>
        /// 转换为 JSON 字符串
        /// </summary>
        /// <param name="obj">被转为 JSON 的对象</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJson(object obj)
        {
            return ToJsonStr(obj);
        }

        /// <summary>
        /// JSON 字符串转 JSONObject 对象
        /// </summary>
        /// <param name="obj">Bean 对象或者 Map</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObj(object obj, JSONConfig config)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return null;
                }
                var jsonObj = new JSONObject(str);
                if (config != null)
                {
                    // 复制配置到新创建的 JSONObject
                    jsonObj.Config = config.Copy();
                }
                return jsonObj;
            }

            if (obj is JSONObject jsonObj)
            {
                return jsonObj;
            }

            return new JSONObject(obj, config ?? JSONConfig.Create());
        }

        /// <summary>
        /// JSON 字符串转 JSONObject 对象
        /// </summary>
        /// <param name="obj">Bean 对象或者 Map</param>
        /// <param name="ignoreNullValue">是否忽略空值</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObj(object obj, bool ignoreNullValue)
        {
            return new JSONObject(obj, ignoreNullValue);
        }

        /// <summary>
        /// JSON 字符串转 JSONArray
        /// </summary>
        /// <param name="jsonStr">JSON 字符串</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ParseArray(string jsonStr)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                return null;
            }
            return new JSONArray(jsonStr);
        }

        /// <summary>
        /// JSON 字符串转 JSONArray
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合对象</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ParseArray(object arrayOrCollection)
        {
            return ParseArray(arrayOrCollection, (JSONConfig)null);
        }

        /// <summary>
        /// JSON 字符串转 JSONArray
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合对象</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ParseArray(object arrayOrCollection, JSONConfig config)
        {
            if (arrayOrCollection == null)
            {
                return null;
            }

            if (arrayOrCollection is string str)
            {
                return string.IsNullOrWhiteSpace(str) ? null : new JSONArray(str);
            }

            if (arrayOrCollection is JSONArray jsonArray)
            {
                return jsonArray;
            }

            return new JSONArray(arrayOrCollection, config ?? JSONConfig.Create());
        }

        /// <summary>
        /// JSON 字符串转 JSONArray
        /// </summary>
        /// <param name="arrayOrCollection">数组或集合对象</param>
        /// <param name="ignoreNullValue">是否忽略空值</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ParseArray(object arrayOrCollection, bool ignoreNullValue)
        {
            return new JSONArray(arrayOrCollection, ignoreNullValue);
        }

        /// <summary>
        /// 转换对象为 JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON</returns>
        public static JSON Parse(object obj)
        {
            return Parse(obj, (JSONConfig)null);
        }

        /// <summary>
        /// 转换对象为 JSON
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>JSON</returns>
        public static JSON Parse(object obj, JSONConfig config)
        {
            if (obj == null)
            {
                return null;
            }

            config = config ?? JSONConfig.Create();

            if (obj is JSON json)
            {
                return json;
            }

            if (obj is string str)
            {
                str = str.Trim();
                if (str.StartsWith("{"))
                {
                    return new JSONObject(str);
                }
                if (str.StartsWith("["))
                {
                    return new JSONArray(str);
                }
            }

            if (obj is IDictionary)
            {
                return new JSONObject(obj, config);
            }

            if (obj is IEnumerable enumerable && !(obj is string))
            {
                return new JSONArray(enumerable, config);
            }

            return new JSONObject(obj, config);
        }

        /// <summary>
        /// XML 字符串转为 JSONObject
        /// </summary>
        /// <param name="xmlStr">XML 字符串</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseFromXml(string xmlStr)
        {
            // XML 功能暂未实现
            return null;
        }

        #endregion

        #region 文件读取方法

        /// <summary>
        /// 读取 JSON 文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>JSON</returns>
        public static JSONBase ReadJSON(string filePath, System.Text.Encoding encoding = null)
        {
            encoding = encoding ?? System.Text.Encoding.UTF8;
            var content = File.ReadAllText(filePath, encoding);
            return (JSONBase)Parse(content);
        }

        /// <summary>
        /// 读取 JSONObject 文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ReadJSONObject(string filePath, System.Text.Encoding encoding = null)
        {
            encoding = encoding ?? System.Text.Encoding.UTF8;
            var content = File.ReadAllText(filePath, encoding);
            return ParseObj(content);
        }

        /// <summary>
        /// 读取 JSONArray 文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ReadJSONArray(string filePath, System.Text.Encoding encoding = null)
        {
            encoding = encoding ?? System.Text.Encoding.UTF8;
            var content = File.ReadAllText(filePath, encoding);
            return ParseArray(content);
        }

        #endregion

        #region ToString 方法

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="json">JSON</param>
        /// <param name="indentFactor">缩进因子</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonStr(JSONBase json, int indentFactor)
        {
            if (json == null)
            {
                return null;
            }
            return json.ToJSONString(indentFactor);
        }

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonStr(JSONBase json)
        {
            return ToJsonStr(json, 0);
        }

        /// <summary>
        /// 转为 JSON 字符串
        /// </summary>
        /// <param name="json">JSON</param>
        /// <param name="writer">Writer</param>
        public static void ToJsonStr(JSONBase json, TextWriter writer)
        {
            if (json != null && json is JSONBase jsonBase)
            {
                jsonBase.Write(writer, 0, 0);
            }
        }

        /// <summary>
        /// 转为格式化的 JSON 字符串
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonPrettyStr(JSONBase json)
        {
            return ToJsonStr(json, 4);
        }

        /// <summary>
        /// 转换为 JSON 字符串
        /// </summary>
        /// <param name="obj">被转为 JSON 的对象</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonStr(object obj)
        {
            return ToJsonStr(obj, (JSONConfig)null);
        }

        /// <summary>
        /// 转换为 JSON 字符串
        /// </summary>
        /// <param name="obj">被转为 JSON 的对象</param>
        /// <param name="jsonConfig">JSON 配置</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonStr(object obj, JSONConfig jsonConfig)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj is string str)
            {
                return str;
            }

            if (obj is bool || obj is char || obj is int || obj is long || 
                obj is double || obj is float || obj is decimal)
            {
                return obj.ToString();
            }

            var json = Parse(obj, jsonConfig);
            return json?.ToJSONString(0);
        }

        /// <summary>
        /// 转换为格式化 JSON 字符串
        /// </summary>
        /// <param name="obj">Bean 对象</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJsonPrettyStr(object obj)
        {
            var json = Parse(obj);
            return ToJsonPrettyStr(json);
        }

        /// <summary>
        /// 转换为 XML 字符串
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>XML 字符串</returns>
        public static string ToXmlStr(JSONBase json)
        {
            // XML 功能暂未实现
            return null;
        }

        #endregion

        #region ToBean 方法

        /// <summary>
        /// JSON 字符串转为实体类对象
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="jsonString">JSON 字符串</param>
        /// <returns>实体类对象</returns>
        public static T ToBean<T>(string jsonString)
        {
            return ToBean<T>(ParseObj(jsonString));
        }

        /// <summary>
        /// JSON 字符串转为实体类对象
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="jsonString">JSON 字符串</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>实体类对象</returns>
        public static T ToBean<T>(string jsonString, JSONConfig config)
        {
            return ToBean<T>(ParseObj(jsonString, config));
        }

        /// <summary>
        /// JSONObject 转为实体类对象
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="json">JSONObject</param>
        /// <returns>实体类对象</returns>
        public static T ToBean<T>(JSONObject json)
        {
            if (json == null)
            {
                return default;
            }
            return json.ToBean<T>();
        }

        /// <summary>
        /// JSON 字符串转为实体类对象
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="jsonString">JSON 字符串</param>
        /// <param name="type">实体类类型</param>
        /// <param name="ignoreError">是否忽略错误</param>
        /// <returns>实体类对象</returns>
        public static T ToBean<T>(string jsonString, Type type, bool ignoreError)
        {
            var json = Parse(jsonString, JSONConfig.Create().SetIgnoreError(ignoreError));
            if (json == null)
            {
                return default;
            }
            if (json is JSONBase jsonBase)
            {
                return (T)jsonBase.ToBean(type);
            }
            return default;
        }

        /// <summary>
        /// JSON 转为实体类对象
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="json">JSON</param>
        /// <param name="type">类型</param>
        /// <param name="ignoreError">是否忽略错误</param>
        /// <returns>实体类对象</returns>
        public static T ToBean<T>(JSONBase json, Type type, bool ignoreError)
        {
            if (json == null)
            {
                return default;
            }
            if (json is JSONBase jsonBase)
            {
                return (T)jsonBase.ToBean(type);
            }
            return default;
        }

        /// <summary>
        /// JSON 转为实体类对象
        /// </summary>
        /// <param name="json">JSON</param>
        /// <param name="type">类型</param>
        /// <param name="ignoreError">是否忽略错误</param>
        /// <returns>实体类对象</returns>
        public static object ToBean(JSONBase json, Type type, bool ignoreError)
        {
            if (json == null)
            {
                return null;
            }
            if (json is JSONBase jsonBase)
            {
                return jsonBase.ToBean(type);
            }
            return null;
        }

        #endregion

        #region ToList 方法

        /// <summary>
        /// JSONArray 字符串转换为 Bean 的 List
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="jsonArray">JSONArray 字符串</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(string jsonArray)
        {
            return ToList<T>(ParseArray(jsonArray), typeof(T));
        }

        /// <summary>
        /// JSONArray 转换为 Bean 的 List
        /// </summary>
        /// <typeparam name="T">Bean 类型</typeparam>
        /// <param name="jsonArray">JSONArray</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(JSONArray jsonArray)
        {
            return ToList<T>(jsonArray, typeof(T));
        }

        /// <summary>
        /// JSONArray 转换为 Bean 的 List
        /// </summary>
        /// <param name="jsonArray">JSONArray</param>
        /// <param name="elementType">List 中元素类型</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(JSONArray jsonArray, Type elementType)
        {
            if (jsonArray == null)
            {
                return null;
            }
            return JSONConverter.ToList<T>(jsonArray);
        }

        #endregion

        #region 路径操作方法

        /// <summary>
        /// 通过表达式获取 JSON 中嵌套的对象
        /// </summary>
        /// <param name="json">JSON</param>
        /// <param name="expression">表达式</param>
        /// <returns>对象</returns>
        public static object GetByPath(JSONBase json, string expression)
        {
            if (json == null || string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }
            return json.GetByPath(expression);
        }

        /// <summary>
        /// 通过表达式获取 JSON 中嵌套的对象
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="json">JSON</param>
        /// <param name="expression">表达式</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>对象</returns>
        public static T GetByPath<T>(JSONBase json, string expression, T defaultValue)
        {
            if (json == null || string.IsNullOrWhiteSpace(expression))
            {
                return defaultValue;
            }

            var value = json.GetByPath(expression);
            if (value == null)
            {
                return defaultValue;
            }

            return JSONConverter.JsonConvert<T>(value, null);
        }

        /// <summary>
        /// 设置表达式指定位置的值
        /// </summary>
        /// <param name="json">JSON</param>
        /// <param name="expression">表达式</param>
        /// <param name="value">值</param>
        public static void PutByPath(JSONBase json, string expression, object value)
        {
            json?.PutByPath(expression, value);
        }

        #endregion

        #region 字符串转义方法

        /// <summary>
        /// 对所有双引号做转义处理
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string Quote(string str)
        {
            return Quote(str, true);
        }

        /// <summary>
        /// 对所有双引号做转义处理
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="isWrap">是否使用双引号包装字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string Quote(string str, bool isWrap)
        {
            if (string.IsNullOrEmpty(str))
            {
                return isWrap ? "\"\"" : string.Empty;
            }

            var sb = new StringBuilder();
            if (isWrap)
            {
                sb.Append('"');
            }

            foreach (var c in str)
            {
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    default:
                        sb.Append(Escape(c));
                        break;
                }
            }

            if (isWrap)
            {
                sb.Append('"');
            }

            return sb.ToString();
        }

        /// <summary>
        /// 转义显示不可见字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string Escape(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var sb = new StringBuilder();
            foreach (var c in str)
            {
                sb.Append(Escape(c));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转义字符
        /// </summary>
        private static string Escape(char c)
        {
            switch (c)
            {
                case '\b':
                    return "\\b";
                case '\t':
                    return "\\t";
                case '\n':
                    return "\\n";
                case '\f':
                    return "\\f";
                case '\r':
                    return "\\r";
                case '"':
                    return "\\\"";
                case '\\':
                    return "\\\\";
                default:
                    return c.ToString();
            }
        }

        #endregion

        #region Wrap 方法

        /// <summary>
        /// 在需要的时候包装对象
        /// </summary>
        /// <param name="obj">被包装的对象</param>
        /// <param name="config">JSON 选项</param>
        /// <returns>包装后的值</returns>
        public static object Wrap(object obj, JSONConfig config)
        {
            config = config ?? JSONConfig.Create();

            if (obj == null)
            {
                return config.IsIgnoreNullValue() ? null : JSONNull.NULL;
            }

            if (obj is string || obj is bool || obj is int || obj is long ||
                obj is double || obj is float || obj is decimal ||
                obj is JSON || obj is JSONNull)
            {
                return obj;
            }

            if (obj is IDictionary)
            {
                return new JSONObject(obj, config);
            }

            if (obj is IEnumerable enumerable && !(obj is string))
            {
                return new JSONArray(enumerable, config);
            }

            if (obj is DateTime || obj is char)
            {
                return obj;
            }

            if (obj is Enum)
            {
                return obj.ToString();
            }

            return new JSONObject(obj, config);
        }

        #endregion

        #region 类型判断方法

        /// <summary>
        /// 格式化 JSON 字符串
        /// </summary>
        /// <param name="jsonStr">JSON 字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatJsonStr(string jsonStr)
        {
            var json = Parse(jsonStr);
            if (json == null)
            {
                return jsonStr;
            }
            return json.ToJSONString(4);
        }

        /// <summary>
        /// 是否为 JSON 类型字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为 JSON 类型字符串</returns>
        public static bool IsTypeJSON(string str)
        {
            return IsTypeJSONObject(str) || IsTypeJSONArray(str);
        }

        /// <summary>
        /// 是否为 JSONObject 字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为 JSONObject 字符串</returns>
        public static bool IsTypeJSONObject(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            str = str.Trim();
            return str.StartsWith("{") && str.EndsWith("}");
        }

        /// <summary>
        /// 是否为 JSONArray 字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为 JSONArray 字符串</returns>
        public static bool IsTypeJSONArray(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            str = str.Trim();
            return str.StartsWith("[") && str.EndsWith("]");
        }

        /// <summary>
        /// 是否为 null 对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否为 null</returns>
        public static bool IsNull(object obj)
        {
            return obj == null || obj is JSONNull;
        }

        /// <summary>
        /// XML 转 JSONObject
        /// </summary>
        /// <param name="xml">XML 字符串</param>
        /// <returns>JSONObject</returns>
        public static JSONObject XmlToJson(string xml)
        {
            // XML 功能暂未实现
            return null;
        }

        #endregion
    }
}
