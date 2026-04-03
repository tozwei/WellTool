using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 类型转换器
    /// </summary>
    public static class JSONConverter
    {
        /// <summary>
        /// JSON 转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">JSON 值</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>目标类型实例</returns>
        public static T JsonConvert<T>(object value, JSONConfig config = null)
        {
            return (T)JsonConvert(typeof(T), value, config);
        }

        /// <summary>
        /// JSON 转换为指定类型
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="value">JSON 值</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>目标类型实例</returns>
        public static object JsonConvert(Type type, object value, JSONConfig config = null)
        {
            if (value == null || value == JSONNull.NULL)
            {
                return GetDefaultValue(type);
            }

            config = config ?? JSONConfig.Create();

            // 处理字符串
            if (value is string strValue)
            {
                return ConvertFromString(type, strValue, config);
            }

            // 处理数字
            if (value is int intValue && type == typeof(long))
            {
                return (long)intValue;
            }
            if (value is long longValue && type == typeof(int))
            {
                return (int)longValue;
            }

            // 处理基本类型直接转换
            if (type.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // 处理 JSON 对象
            if (value is JSONObject jsonObj)
            {
                return ConvertFromJSONObject(type, jsonObj, config);
            }

            // 处理 JSON 数组
            if (value is JSONArray jsonArray)
            {
                return ConvertFromJSONArray(type, jsonArray, config);
            }

            // 处理 IEnumerable
            if (value is IEnumerable enumerable && !type.IsArray)
            {
                return ConvertFromEnumerable(type, enumerable, config);
            }

            // 处理其他情况
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch
            {
                return config.IsIgnoreError() ? GetDefaultValue(type) : throw new JSONException($"Cannot convert {value.GetType()} to {type}");
            }
        }

        /// <summary>
        /// JSON 转换为目标类型
        /// </summary>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="targetType">TypeReference</param>
        /// <param name="value">JSON 值</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>目标类型实例</returns>
        public static TTarget JsonConvert<TTarget>(TypeReference<TTarget> targetType, object value, JSONConfig config = null)
        {
            return JsonConvert<TTarget>(value, config);
        }

        /// <summary>
        /// 将 JSON 值转换为目标类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">JSON 值</param>
        /// <param name="resultType">结果类型</param>
        /// <param name="config">JSON 配置</param>
        /// <returns>目标类型实例</returns>
        public static T JsonConvert<T>(object value, Type resultType, JSONConfig config = null)
        {
            return (T)JsonConvert(resultType, value, config);
        }

        /// <summary>
        /// 从字符串转换
        /// </summary>
        private static object ConvertFromString(Type type, string strValue, JSONConfig config)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return GetDefaultValue(type);
            }

            // 字符串类型直接返回
            if (type == typeof(string))
            {
                return strValue;
            }

            // 对象类型直接返回字符串
            if (type == typeof(object))
            {
                return strValue;
            }

            // 日期时间处理
            if (type == typeof(DateTime))
            {
                if (DateTime.TryParse(strValue, out var dateTime))
                {
                    return dateTime;
                }
                if (long.TryParse(strValue, out var timestamp))
                {
                    return new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
                }
                return config.IsIgnoreError() ? GetDefaultValue(type) : throw new JSONException($"Cannot parse date: {strValue}");
            }

            // 数字类型处理
            if (type == typeof(int) || type == typeof(int?))
            {
                return int.TryParse(strValue, out var result) ? result : 0;
            }
            if (type == typeof(long) || type == typeof(long?))
            {
                return long.TryParse(strValue, out var result) ? result : 0L;
            }
            if (type == typeof(double) || type == typeof(double?))
            {
                return double.TryParse(strValue, out var result) ? result : 0.0;
            }
            if (type == typeof(float) || type == typeof(float?))
            {
                return float.TryParse(strValue, out var result) ? result : 0f;
            }
            if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return decimal.TryParse(strValue, out var result) ? result : 0m;
            }

            // 布尔类型处理
            if (type == typeof(bool) || type == typeof(bool?))
            {
                var lower = strValue.ToLower();
                if (bool.TryParse(lower, out var result))
                {
                    return result;
                }
                return lower == "1" || lower == "yes" || lower == "true";
            }

            // Guid
            if (type == typeof(Guid))
            {
                return Guid.TryParse(strValue, out var result) ? result : Guid.Empty;
            }

            // Uri
            if (type == typeof(Uri))
            {
                return new Uri(strValue);
            }

            return config.IsIgnoreError() ? GetDefaultValue(type) : throw new JSONException($"Cannot convert string to {type}");
        }

        /// <summary>
        /// 从 JSONObject 转换
        /// </summary>
        private static object ConvertFromJSONObject(Type type, JSONObject jsonObj, JSONConfig config)
        {
            try
            {
                // 处理基本类型
                if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTimeOffset))
                {
                    return GetDefaultValue(type);
                }

                // 处理字典类型
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    var keyType = type.GetGenericArguments()[0];
                    var valueType = type.GetGenericArguments()[1];
                    var dict = Activator.CreateInstance(type) as IDictionary;
                    if (dict != null)
                    {
                        foreach (var kvp in jsonObj)
                        {
                            var key = Convert.ChangeType(kvp.Key, keyType);
                            var value = JsonConvert(valueType, kvp.Value, config);
                            dict.Add(key, value);
                        }
                    }
                    return dict;
                }

                // 处理对象类型
                var instance = Activator.CreateInstance(type);
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.CanWrite)
                    {
                        var value = jsonObj[property.Name];
                        if (value != null && value != JSONNull.NULL)
                        {
                            var propertyType = property.PropertyType;
                            var convertedValue = JsonConvert(propertyType, value, config);
                            property.SetValue(instance, convertedValue);
                        }
                    }
                }
                return instance;
            }
            catch
            {
                if (config.IsIgnoreError())
                {
                    return GetDefaultValue(type);
                }
                throw;
            }
        }

        /// <summary>
        /// 从 JSONArray 转换
        /// </summary>
        private static object ConvertFromJSONArray(Type type, JSONArray jsonArray, JSONConfig config)
        {
            try
            {
                // 如果目标类型是数组
                if (type.IsArray)
                {
                    var elementType = type.GetElementType();
                    var list = ToList(jsonArray, elementType);
                    var array = Array.CreateInstance(elementType, list.Count);
                    for (int i = 0; i < list.Count; i++)
                    {
                        array.SetValue(list[i], i);
                    }
                    return array;
                }

                // 如果目标类型是 List
                if (type.IsGenericType)
                {
                    var genericType = type.GetGenericArguments()[0];
                    var listType = typeof(List<>).MakeGenericType(genericType);
                    var list = (IList)Activator.CreateInstance(listType);
                    foreach (var item in jsonArray)
                    {
                        list.Add(JsonConvert(genericType, item, config));
                    }
                    return list;
                }

                // 如果目标类型实现了 IEnumerable
                if (typeof(IEnumerable).IsAssignableFrom(type))
                {
                    var list = new List<object>();
                    foreach (var item in jsonArray)
                    {
                        list.Add(item);
                    }
                    return list;
                }

                return jsonArray.ToList();
            }
            catch
            {
                if (config.IsIgnoreError())
                {
                    return GetDefaultValue(type);
                }
                throw;
            }
        }

        /// <summary>
        /// 从 IEnumerable 转换
        /// </summary>
        private static object ConvertFromEnumerable(Type type, IEnumerable enumerable, JSONConfig config)
        {
            var list = new List<object>();
            foreach (var item in enumerable)
            {
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 将 JSONArray 转换为指定类型的 List
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="jsonArray">JSON 数组</param>
        /// <returns>List</returns>
        public static List<T> ToList<T>(JSONArray jsonArray)
        {
            if (jsonArray == null)
            {
                return null;
            }

            var list = new List<T>();
            foreach (var item in jsonArray)
            {
                if (item == null || item == JSONNull.NULL)
                {
                    list.Add(default(T));
                }
                else if (item is JSONArray nestedArray)
                {
                    // 处理嵌套数组
                    if (typeof(T).IsArray)
                    {
                        var elementType = typeof(T).GetElementType();
                        var nestedList = ToList(nestedArray, elementType);
                        var array = Array.CreateInstance(elementType, nestedList.Count);
                        for (int i = 0; i < nestedList.Count; i++)
                        {
                            array.SetValue(nestedList[i], i);
                        }
                        list.Add((T)(object)array);
                    }
                    else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var elementType = typeof(T).GetGenericArguments()[0];
                        var nestedList = ToList(nestedArray, elementType);
                        var genericList = Activator.CreateInstance(typeof(T)) as IList;
                        if (genericList != null)
                        {
                            foreach (var nestedItem in nestedList)
                            {
                                genericList.Add(nestedItem);
                            }
                            list.Add((T)genericList);
                        }
                    }
                    else
                    {
                        // 如果 T 不是数组或列表类型，尝试直接转换
                        list.Add(JsonConvert<T>(item));
                    }
                }
                else if (item is JSONObject nestedObj)
                {
                    list.Add((T)nestedObj.ToBean(typeof(T)));
                }
                else
                {
                    list.Add(JsonConvert<T>(item));
                }
            }
            return list;
        }

        /// <summary>
        /// 将 JSONArray 转换为指定类型的 List
        /// </summary>
        /// <param name="jsonArray">JSON 数组</param>
        /// <param name="elementType">元素类型</param>
        /// <returns>List</returns>
        public static List<object> ToList(JSONArray jsonArray, Type elementType)
        {
            if (jsonArray == null)
            {
                return null;
            }

            var list = new List<object>();
            foreach (var item in jsonArray)
            {
                if (item == null || item == JSONNull.NULL)
                {
                    list.Add(null);
                }
                else if (item is JSONArray nestedArray)
                {
                    list.Add(ToList(nestedArray, elementType));
                }
                else if (item is JSONObject nestedObj)
                {
                    list.Add(nestedObj.ToBean(elementType));
                }
                else
                {
                    list.Add(JsonConvert(elementType, item));
                }
            }
            return list;
        }

        /// <summary>
        /// 将 JSONArray 转换为数组
        /// </summary>
        /// <param name="jsonArray">JSON 数组</param>
        /// <param name="arrayClass">数组元素类型</param>
        /// <returns>数组</returns>
        public static object ToArray(JSONArray jsonArray, Type arrayClass)
        {
            if (jsonArray == null)
            {
                return null;
            }

            var list = ToList(jsonArray, arrayClass);
            var array = Array.CreateInstance(arrayClass, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                array.SetValue(list[i], i);
            }
            return array;
        }
    }

    /// <summary>
    /// 类型引用，用于获取泛型参数类型
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class TypeReference<T>
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        public Type Type => typeof(T);
    }
}
