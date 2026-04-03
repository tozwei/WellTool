using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 键值对条目抽象基类
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public abstract class AbsEntry<K, V> : IKeyValuePair<K, V>
    {
        /// <summary>
        /// 键
        /// </summary>
        public abstract K Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public abstract V Value { get; set; }

        /// <summary>
        /// 获取键
        /// </summary>
        public K GetKey() => Key;

        /// <summary>
        /// 获取值
        /// </summary>
        public V GetValue() => Value;

        /// <summary>
        /// 设置值
        /// </summary>
        public V SetValue(V value)
        {
            var oldValue = Value;
            Value = value;
            return oldValue;
        }

        /// <summary>
        /// 比较是否相等
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is IKeyValuePair<K, V> other)
            {
                return Equals(Key, other.Key);
            }
            return false;
        }

        /// <summary>
        /// 获取哈希码
        /// </summary>
        public override int GetHashCode()
        {
            return Key?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            return $"{Key}={Value}";
        }
    }

    /// <summary>
    /// 键值对接口
    /// </summary>
    public interface IKeyValuePair<K, V>
    {
        K Key { get; set; }
        V Value { get; set; }
        K GetKey();
        V GetValue();
        V SetValue(V value);
    }
}
