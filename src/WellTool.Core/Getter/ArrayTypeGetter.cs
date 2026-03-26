using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Getter
{
    public interface IArrayTypeGetter
    {
        bool[] GetBoolArray(string key, bool[] defaultValue = null);
        char[] GetCharArray(string key, char[] defaultValue = null);
        byte[] GetByteArray(string key, byte[] defaultValue = null);
        short[] GetShortArray(string key, short[] defaultValue = null);
        int[] GetIntArray(string key, int[] defaultValue = null);
        long[] GetLongArray(string key, long[] defaultValue = null);
        float[] GetFloatArray(string key, float[] defaultValue = null);
        double[] GetDoubleArray(string key, double[] defaultValue = null);
        string[] GetStringArray(string key, string[] defaultValue = null);
        T[] GetEnumArray<T>(string key, T[] defaultValue = null) where T : Enum;
    }

    public abstract class ArrayTypeGetter : IArrayTypeGetter
    {
        public abstract object GetObject(string key);

        public bool[] GetBoolArray(string key, bool[] defaultValue = null)
        {
            return GetArray<bool>(key, defaultValue);
        }

        public char[] GetCharArray(string key, char[] defaultValue = null)
        {
            return GetArray<char>(key, defaultValue);
        }

        public byte[] GetByteArray(string key, byte[] defaultValue = null)
        {
            return GetArray<byte>(key, defaultValue);
        }

        public short[] GetShortArray(string key, short[] defaultValue = null)
        {
            return GetArray<short>(key, defaultValue);
        }

        public int[] GetIntArray(string key, int[] defaultValue = null)
        {
            return GetArray<int>(key, defaultValue);
        }

        public long[] GetLongArray(string key, long[] defaultValue = null)
        {
            return GetArray<long>(key, defaultValue);
        }

        public float[] GetFloatArray(string key, float[] defaultValue = null)
        {
            return GetArray<float>(key, defaultValue);
        }

        public double[] GetDoubleArray(string key, double[] defaultValue = null)
        {
            return GetArray<double>(key, defaultValue);
        }

        public string[] GetStringArray(string key, string[] defaultValue = null)
        {
            return GetArray<string>(key, defaultValue);
        }

        public T[] GetEnumArray<T>(string key, T[] defaultValue = null) where T : Enum
        {
            return GetArray<T>(key, defaultValue);
        }

        protected virtual T[] GetArray<T>(string key, T[] defaultValue = null)
        {
            var obj = GetObject(key);
            if (obj == null)
            {
                return defaultValue;
            }

            if (obj is T[] array)
            {
                return array;
            }

            if (obj is IEnumerable<T> enumerable)
            {
                return enumerable.ToArray();
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
                    .ToArray();
            }

            return defaultValue;
        }
    }
}
