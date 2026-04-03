using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变键值对
    /// </summary>
    public class MutablePair<TKey, TValue>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutablePair()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutablePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 创建键值对
        /// </summary>
        public static MutablePair<TKey, TValue> Of(TKey key, TValue value)
        {
            return new MutablePair<TKey, TValue>(key, value);
        }

        /// <summary>
        /// 创建键值对
        /// </summary>
        public static MutablePair<TKey, TValue>[] OfArray(TKey[] keys, TValue[] values)
        {
            if (keys == null || values == null || keys.Length != values.Length)
            {
                throw new ArgumentException("Keys and values must have the same length");
            }

            var result = new MutablePair<TKey, TValue>[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                result[i] = new MutablePair<TKey, TValue>(keys[i], values[i]);
            }
            return result;
        }

        public override string ToString()
        {
            return $"({Key}, {Value})";
        }

        public override bool Equals(object obj)
        {
            if (obj is MutablePair<TKey, TValue> other)
            {
                return Equals(Key, other.Key) && Equals(Value, other.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }
    }
}
