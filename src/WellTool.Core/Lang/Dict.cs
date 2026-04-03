using System;
using System.Collections.Generic;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 字典类型，简化字典操作
    /// </summary>
    public class Dict : Dictionary<string, object>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Dict()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Dict(IEnumerable<KeyValuePair<string, object>> collection) : base()
        {
            if (collection != null)
            {
                foreach (var kvp in collection)
                {
                    Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// 创建字典
        /// </summary>
        public static Dict Create()
        {
            return new Dict();
        }

        /// <summary>
        /// 创建字典
        /// </summary>
        public static Dict Of(string key, object value)
        {
            return new Dict { { key, value } };
        }

        /// <summary>
        /// 创建字典
        /// </summary>
        public static Dict Of(params object[] keysAndValues)
        {
            var dict = new Dict();
            for (int i = 0; i < keysAndValues.Length - 1; i += 2)
            {
                dict[keysAndValues[i].ToString()] = keysAndValues[i + 1];
            }
            return dict;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        public Dict Set(string key, object value)
        {
            this[key] = value;
            return this;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T Get<T>(string key)
        {
            if (TryGetValue(key, out var value) && value != null)
            {
                if (value is T typedValue)
                {
                    return typedValue;
                }
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return default;
        }

        /// <summary>
        /// 获取值，为空时返回默认值
        /// </summary>
        public T GetOrDefault<T>(string key, T defaultValue = default)
        {
            if (TryGetValue(key, out var value) && value != null)
            {
                if (value is T typedValue)
                {
                    return typedValue;
                }
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        public string GetStr(string key, string defaultValue = null)
        {
            if (TryGetValue(key, out var value) && value != null)
            {
                return value.ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取整数值
        /// </summary>
        public int GetInt(string key, int defaultValue = 0)
        {
            if (TryGetValue(key, out var value))
            {
                if (value is int i) return i;
                if (int.TryParse(value.ToString(), out int result)) return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        public bool GetBool(string key, bool defaultValue = false)
        {
            if (TryGetValue(key, out var value))
            {
                if (value is bool b) return b;
                if (bool.TryParse(value.ToString(), out bool result)) return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 获取日期值
        /// </summary>
        public DateTime? GetDate(string key)
        {
            if (TryGetValue(key, out var value))
            {
                if (value is DateTime dt) return dt;
                if (DateTime.TryParse(value.ToString(), out DateTime result)) return result;
            }
            return null;
        }

        /// <summary>
        /// 链式调用
        /// </summary>
        public Dict Put(string key, object value)
        {
            this[key] = value;
            return this;
        }
    }
}
