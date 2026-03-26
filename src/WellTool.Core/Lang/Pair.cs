using System;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 键值对
    /// </summary>
    /// <typeparam name="T">键类型</typeparam>
    /// <typeparam name="U">值类型</typeparam>
    public class Pair<T, U>
    {
        /// <summary>
        /// 键
        /// </summary>
        public T Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public U Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public Pair(T key, U value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 重写 Equals 方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            if (obj is Pair<T, U> other)
            {
                return Equals(Key, other.Key) && Equals(Value, other.Value);
            }
            return false;
        }

        /// <summary>
        /// 重写 GetHashCode 方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return (Key?.GetHashCode() ?? 0) ^ (Value?.GetHashCode() ?? 0);
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"({Key}, {Value})";
        }
    }
}
