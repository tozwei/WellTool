using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WellTool.Json.Serialize;

namespace WellTool.Json
{
    /// <summary>
    /// 对象和JSON映射器，用于转换对象为JSON
    /// </summary>
    public class ObjectMapper
    {
        private readonly object _source;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">来源对象</param>
        public ObjectMapper(object source)
        {
            this._source = source;
        }

        /// <summary>
        /// 创建ObjectMapper
        /// </summary>
        /// <param name="source">来源对象</param>
        /// <returns>ObjectMapper</returns>
        public static ObjectMapper Of(object source)
        {
            return new ObjectMapper(source);
        }

        /// <summary>
        /// 将给定对象转换为JSONObject
        /// </summary>
        /// <param name="jsonObject">目标JSONObject</param>
        /// <param name="filter">键值对过滤编辑器</param>
        public void Map(JSONObject jsonObject, Func<string, object, bool> filter = null)
        {
            object source = this._source;
            if (null == source)
            {
                return;
            }

            // 自定义序列化
            var serializer = GlobalSerializeMapping.GetObjectSerializer(source.GetType());
            if (serializer != null)
            {
                serializer.Serialize(jsonObject, source);
                return;
            }

            if (source is JSONArray)
            {
                throw new JSONException("Unsupported type to JSONObject!");
            }

            if (source is Dictionary<string, object> dict)
            {
                foreach (var e in dict)
                {
                    bool include = filter == null || filter(e.Key, e.Value);
                    jsonObject.Set(e.Key, e.Value, include, jsonObject.Config.IsCheckDuplicate());
                }
            }
            else if (source is IDictionary entry)
            {
                foreach (DictionaryEntry e in entry)
                {
                    jsonObject.Set(e.Key?.ToString(), e.Value, true, jsonObject.Config.IsCheckDuplicate());
                }
            }
            else if (source is string str)
            {
                MapFromStr(str, jsonObject, filter);
            }
            else if (source is Stream stream)
            {
                using var reader = new StreamReader(stream);
                MapFromTokener(new JSONTokener(reader.ReadToEnd(), jsonObject.Config), jsonObject, filter);
            }
            else if (source is byte[] bytes)
            {
                MapFromTokener(new JSONTokener(Encoding.UTF8.GetString(bytes), jsonObject.Config), jsonObject, filter);
            }
            else if (source is TextReader textReader)
            {
                MapFromTokener(new JSONTokener(textReader.ReadToEnd(), jsonObject.Config), jsonObject, filter);
            }
            else
            {
                // 普通Bean
                MapFromBean(source, jsonObject, filter);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="jsonArray">目标JSONArray</param>
        /// <param name="filter">值过滤编辑器</param>
        public void Map(JSONArray jsonArray, Func<object, bool> filter = null)
        {
            object source = this._source;
            if (null == source)
            {
                return;
            }

            if (source is string str)
            {
                MapFromStr(str, jsonArray, filter);
            }
            else if (source is Stream stream)
            {
                using var reader = new StreamReader(stream);
                MapFromTokener(new JSONTokener(reader.ReadToEnd(), jsonArray.Config), jsonArray, filter);
            }
            else if (source is byte[] bytes)
            {
                if (bytes.Length > 1 && bytes[0] == (byte)'[' && bytes[bytes.Length - 1] == (byte)']')
                {
                    MapFromTokener(new JSONTokener(Encoding.UTF8.GetString(bytes), jsonArray.Config), jsonArray, filter);
                }
                else
                {
                    foreach (byte b in bytes)
                    {
                        jsonArray.Add(b);
                    }
                }
            }
            else if (source is TextReader textReader)
            {
                MapFromTokener(new JSONTokener(textReader.ReadToEnd(), jsonArray.Config), jsonArray, filter);
            }
            else
            {
                IEnumerable<object> iter;
                if (source is Array arr)
                {
                    iter = arr.Cast<object>();
                }
                else if (source is IEnumerable<object> enumerable)
                {
                    iter = enumerable;
                }
                else if (source is IEnumerable ienumerable)
                {
                    iter = ienumerable.Cast<object>();
                }
                else
                {
                    if (!jsonArray.Config.IsIgnoreError())
                    {
                        throw new JSONException("JSONArray initial value should be a string or collection or array.");
                    }
                    return;
                }

                JSONConfig config = jsonArray.Config;
                foreach (object next in iter)
                {
                    if (next != source)
                    {
                        jsonArray.AddRaw(JSONUtil.Wrap(next, config), filter);
                    }
                }
            }
        }

        /// <summary>
        /// 从字符串转换
        /// </summary>
        private void MapFromStr(string source, JSONObject jsonObject, Func<string, object, bool> filter)
        {
            string jsonStr = source.Trim();
            if (jsonStr.StartsWith("<"))
            {
                Xml.XML.ToJSONObject(jsonObject, jsonStr, false);
                return;
            }
            MapFromTokener(new JSONTokener(jsonStr, jsonObject.Config), jsonObject, filter);
        }

        /// <summary>
        /// 从字符串转换
        /// </summary>
        private void MapFromStr(string source, JSONArray jsonArray, Func<object, bool> filter)
        {
            if (!string.IsNullOrEmpty(source))
            {
                MapFromTokener(new JSONTokener(source.Trim(), jsonArray.Config), jsonArray, filter);
            }
        }

        /// <summary>
        /// 从JSONTokener转换
        /// </summary>
        private static void MapFromTokener(JSONTokener x, JSONObject jsonObject, Func<string, object, bool> filter)
        {
            JSONParser.Of(x).ParseTo(jsonObject, filter);
        }

        /// <summary>
        /// 从JSONTokener转换
        /// </summary>
        private static void MapFromTokener(JSONTokener x, JSONArray jsonArray, Func<object, bool> filter)
        {
            JSONParser.Of(x).ParseTo(jsonArray, filter);
        }

        /// <summary>
        /// 从Bean转换
        /// </summary>
        private static void MapFromBean(object bean, JSONObject jsonObject, Func<string, object, bool> filter)
        {
            // 简单的Bean转Map实现
            var type = bean.GetType();
            var properties = type.GetProperties();
            foreach (var prop in properties)
            {
                try
                {
                    var value = prop.GetValue(bean);
                    bool include = filter == null || filter(prop.Name, value);
                    if (include)
                    {
                        jsonObject.Set(prop.Name, value, jsonObject.Config.IsCheckDuplicate());
                    }
                }
                catch
                {
                    // 忽略无法读取的属性
                }
            }
        }
    }
}
