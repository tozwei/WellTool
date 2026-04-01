using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 唯一键集合
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class UniqueKeySet<T> : ICollection<T>, IEnumerable<T>
    {
        private readonly HashSet<T> _set;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UniqueKeySet()
        {
            _set = new HashSet<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">比较器</param>
        public UniqueKeySet(IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(comparer);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">集合</param>
        public UniqueKeySet(IEnumerable<T> collection)
        {
            _set = new HashSet<T>(collection);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="comparer">比较器</param>
        public UniqueKeySet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(collection, comparer);
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否添加成功</returns>
        public bool Add(T item)
        {
            return _set.Add(item);
        }

        /// <summary>
        /// 添加元素（显式实现 ICollection<T> 接口）
        /// </summary>
        /// <param name="item">元素</param>
        void ICollection<T>.Add(T item)
        {
            _set.Add(item);
        }

        /// <summary>
        /// 批量添加元素
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>添加的元素数量</returns>
        public int AddRange(IEnumerable<T> collection)
        {
            int count = 0;
            foreach (var item in collection)
            {
                if (_set.Add(item))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(T item)
        {
            return _set.Remove(item);
        }

        /// <summary>
        /// 批量移除元素
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>移除的元素数量</returns>
        public int RemoveRange(IEnumerable<T> collection)
        {
            int count = 0;
            foreach (var item in collection)
            {
                if (_set.Remove(item))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            _set.Clear();
        }

        /// <summary>
        /// 检查是否包含元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否包含</returns>
        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        /// <summary>
        /// 检查是否包含所有元素
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>是否包含所有元素</returns>
        public bool ContainsAll(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                if (!_set.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 复制到数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="arrayIndex">数组索引</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        public int Count => _set.Count;

        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray()
        {
            var array = new T[_set.Count];
            _set.CopyTo(array);
            return array;
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <returns>列表</returns>
        public List<T> ToList()
        {
            return new List<T>(_set);
        }

        /// <summary>
        /// 转换为哈希集合
        /// </summary>
        /// <returns>哈希集合</returns>
        public HashSet<T> ToHashSet()
        {
            return new HashSet<T>(_set);
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>交集</returns>
        public UniqueKeySet<T> Intersect(IEnumerable<T> other)
        {
            var result = new UniqueKeySet<T>();
            foreach (var item in other)
            {
                if (_set.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>并集</returns>
        public UniqueKeySet<T> Union(IEnumerable<T> other)
        {
            var result = new UniqueKeySet<T>(_set);
            result.AddRange(other);
            return result;
        }

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>差集</returns>
        public UniqueKeySet<T> Except(IEnumerable<T> other)
        {
            var result = new UniqueKeySet<T>(_set);
            result.RemoveRange(other);
            return result;
        }

        /// <summary>
        /// 对称差集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>对称差集</returns>
        public UniqueKeySet<T> SymmetricExcept(IEnumerable<T> other)
        {
            var result = new UniqueKeySet<T>();
            var otherSet = new HashSet<T>(other);

            foreach (var item in _set)
            {
                if (!otherSet.Contains(item))
                {
                    result.Add(item);
                }
            }

            foreach (var item in otherSet)
            {
                if (!_set.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }

    /// <summary>
    /// UniqueKeySet 扩展方法
    /// </summary>
    public static class UniqueKeySetExtensions
    {
        /// <summary>
        /// 创建 UniqueKeySet
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">源集合</param>
        /// <returns>UniqueKeySet</returns>
        public static UniqueKeySet<T> AsUniqueKeySet<T>(this IEnumerable<T> collection)
        {
            return new UniqueKeySet<T>(collection);
        }
    }
}