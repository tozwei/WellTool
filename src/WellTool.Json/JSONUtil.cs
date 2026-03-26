using System;
using System.IO;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 工具类
    /// </summary>
    public static class JSONUtil
    {
        /// <summary>
        /// 解析 JSON 字符串为 JSONObject
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ParseObject(string json)
        {
            return new JSONObject(json);
        }

        /// <summary>
        /// 解析 JSON 字符串为 JSONArray
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        /// <returns>JSONArray</returns>
        public static JSONArray ParseArray(string json)
        {
            return new JSONArray(json);
        }

        /// <summary>
        /// 将对象转换为 JSON 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON 字符串</returns>
        public static string ToJson(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            else if (obj is JSONObject jsonObj)
            {
                return jsonObj.ToString();
            }
            else if (obj is JSONArray jsonArr)
            {
                return jsonArr.ToString();
            }
            else if (obj is string s)
            {
                return '"' + EscapeString(s) + '"';
            }
            else if (obj is bool b)
            {
                return b.ToString().ToLower();
            }
            else if (obj is int i)
            {
                return i.ToString();
            }
            else if (obj is long l)
            {
                return l.ToString();
            }
            else if (obj is double d)
            {
                return d.ToString();
            }
            else if (obj is float f)
            {
                return f.ToString();
            }
            else if (obj is decimal m)
            {
                return m.ToString();
            }
            else if (obj is DateTime dt)
            {
                return '"' + dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + '"';
            }
            else
            {
                throw new JSONException($"Unsupported type: {obj.GetType()}");
            }
        }

        /// <summary>
        /// 转义字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>转义后的字符串</returns>
        private static string EscapeString(string s)
        {
            var sb = new StringBuilder();
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
        /// 从文件中读取 JSON 并解析为 JSONObject
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>JSONObject</returns>
        public static JSONObject ReadFromFile(string path)
        {
            var json = File.ReadAllText(path, Encoding.UTF8);
            return ParseObject(json);
        }

        /// <summary>
        /// 将 JSONObject 写入文件
        /// </summary>
        /// <param name="obj">JSONObject</param>
        /// <param name="path">文件路径</param>
        public static void WriteToFile(JSONObject obj, string path)
        {
            File.WriteAllText(path, obj.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 将 JSONArray 写入文件
        /// </summary>
        /// <param name="arr">JSONArray</param>
        /// <param name="path">文件路径</param>
        public static void WriteToFile(JSONArray arr, string path)
        {
            File.WriteAllText(path, arr.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 格式化 JSON 字符串
        /// </summary>
        /// <param name="json">JSON 字符串</param>
        /// <returns>格式化后的 JSON 字符串</returns>
        public static string Format(string json)
        {
            var obj = ParseObject(json);
            return Format(obj);
        }

        /// <summary>
        /// 格式化 JSONObject
        /// </summary>
        /// <param name="obj">JSONObject</param>
        /// <returns>格式化后的 JSON 字符串</returns>
        public static string Format(JSONObject obj)
        {
            return Format(obj, 0);
        }

        /// <summary>
        /// 格式化 JSONArray
        /// </summary>
        /// <param name="arr">JSONArray</param>
        /// <returns>格式化后的 JSON 字符串</returns>
        public static string Format(JSONArray arr)
        {
            return Format(arr, 0);
        }

        /// <summary>
        /// 格式化 JSONObject
        /// </summary>
        /// <param name="obj">JSONObject</param>
        /// <param name="indent">缩进级别</param>
        /// <returns>格式化后的 JSON 字符串</returns>
        private static string Format(JSONObject obj, int indent)
        {
            var sb = new StringBuilder();
            var indentStr = new string(' ', indent * 2);
            sb.Append('{');
            sb.AppendLine();

            var first = true;
            foreach (var kv in obj)
            {
                if (!first)
                {
                    sb.Append(',');
                    sb.AppendLine();
                }
                first = false;

                sb.Append(indentStr);
                sb.Append(' ');
                sb.Append('"');
                sb.Append(EscapeString(kv.Key));
                sb.Append('"');
                sb.Append(':');
                sb.Append(' ');
                sb.Append(Format(kv.Value, indent + 1));
            }

            sb.AppendLine();
            sb.Append(indentStr);
            sb.Append('}');
            return sb.ToString();
        }

        /// <summary>
        /// 格式化 JSONArray
        /// </summary>
        /// <param name="arr">JSONArray</param>
        /// <param name="indent">缩进级别</param>
        /// <returns>格式化后的 JSON 字符串</returns>
        private static string Format(JSONArray arr, int indent)
        {
            var sb = new StringBuilder();
            var indentStr = new string(' ', indent * 2);
            sb.Append('[');
            sb.AppendLine();

            var first = true;
            foreach (var value in arr)
            {
                if (!first)
                {
                    sb.Append(',');
                    sb.AppendLine();
                }
                first = false;

                sb.Append(indentStr);
                sb.Append(' ');
                sb.Append(Format(value, indent + 1));
            }

            sb.AppendLine();
            sb.Append(indentStr);
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// 格式化值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="indent">缩进级别</param>
        /// <returns>格式化后的字符串</returns>
        private static string Format(object value, int indent)
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
                return Format(obj, indent);
            }
            else if (value is JSONArray arr)
            {
                return Format(arr, indent);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}