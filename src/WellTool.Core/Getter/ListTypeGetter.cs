using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Getter
{
    public interface IListTypeGetter
    {
        List<bool> GetBoolList(string key, List<bool> defaultValue = null);
        List<char> GetCharList(string key, List<char> defaultValue = null);
        List<byte> GetByteList(string key, List<byte> defaultValue = null);
        List<short> GetShortList(string key, List<short> defaultValue = null);
        List<int> GetIntList(string key, List<int> defaultValue = null);
        List<long> GetLongList(string key, List<long> defaultValue = null);
        List<float> GetFloatList(string key, List<float> defaultValue = null);
        List<double> GetDoubleList(string key, List<double> defaultValue = null);
        List<string> GetStringList(string key, List<string> defaultValue = null);
        List<T> GetEnumList<T>(string key, List<T> defaultValue = null) where T : Enum;
    }

    public abstract class ListTypeGetter : IListTypeGetter
    {
        public abstract object GetObject(string key);

        public List<bool> GetBoolList(string key, List<bool> defaultValue = null)
        {
            return GetList<bool>(key, defaultValue);
        }

        public List<char> GetCharList(string key, List<char> defaultValue = null)
        {
            return GetList<char>(key, defaultValue);
        }

        public List<byte> GetByteList(string key, List<byte> defaultValue = null)
        {
            return GetList<byte>(key, defaultValue);
        }

        public List<short> GetShortList(string key, List<short> defaultValue = null)
        {
            return GetList<short>(key, defaultValue);
        }

        public List<int> GetIntList(string key, List<int> defaultValue = null)
        {
            return GetList<int>(key, defaultValue);
        }

        public List<long> GetLongList(string key, List<long> defaultValue = null)
        {
            return GetList<long>(key, defaultValue);
        }

        public List<float> GetFloatList(string key, List<float> defaultValue = null)
        {
            return GetList<float>(key, defaultValue);
        }

        public List<double> GetDoubleList(string key, List<double> defaultValue = null)
        {
            return GetList<double>(key, defaultValue);
        }

        public List<string> GetStringList(string key, List<string> defaultValue = null)
        {
            return GetList<string>(key, defaultValue);
        }

        public List<T> GetEnumList<T>(string key, List<T> defaultValue = null) where T : Enum
        {
            return GetList<T>(key, defaultValue);
        }

        protected virtual List<T> GetList<T>(string key, List<T> defaultValue = null)
        {
            var obj = GetObject(key);
            if (obj == null)
            {
                return defaultValue;
            }

            if (obj is List<T> list)
            {
                return list;
            }

            if (obj is IEnumerable<T> enumerable)
            {
                return enumerable.ToList();
            }

            if (obj is string str)
            {
                // 尝试按逗号分割字符串
                return str.Split(',')
                    .Select(item => {
                        try
                        {
                            if (typeof(T).IsEnum)
                            {
                                return (T)Enum.Parse(typeof(T), item.Trim());
                            }
                            return (T)System.Convert.ChangeType(item.Trim(), typeof(T));
                        }
                        catch
                        {
                            return default;
                        }
                    })
                    .ToList();
            }

            return defaultValue;
        }
    }
}
