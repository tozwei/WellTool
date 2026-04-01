using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 迭代器链
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class IterChain<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="source">源集合</param>
        public IterChain(IEnumerable<T> source)
        {
            _source = source;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Filter(Func<T, bool> predicate)
        {
            return new IterChain<T>(_source.Where(predicate));
        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="func">映射函数</param>
        /// <returns>新的 IterChain</returns>
        public IterChain<R> Map<R>(Func<T, R> func)
        {
            return new IterChain<R>(_source.Select(func));
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">遍历操作</param>
        /// <returns>当前实例</returns>
        public IterChain<T> ForEach(Action<T> action)
        {
            foreach (var item in _source)
            {
                action(item);
            }
            return this;
        }

        /// <summary>
        /// 聚合
        /// </summary>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="seed">初始值</param>
        /// <param name="func">聚合函数</param>
        /// <returns>聚合结果</returns>
        public R Reduce<R>(R seed, Func<R, T, R> func)
        {
            return _source.Aggregate(seed, func);
        }

        /// <summary>
        /// 收集到列表
        /// </summary>
        /// <returns>列表</returns>
        public List<T> ToList()
        {
            return _source.ToList();
        }

        /// <summary>
        /// 收集到数组
        /// </summary>
        /// <returns>数组</returns>
        public T[] ToArray()
        {
            return _source.ToArray();
        }

        /// <summary>
        /// 收集到集合
        /// </summary>
        /// <returns>集合</returns>
        public HashSet<T> ToSet()
        {
            return new HashSet<T>(_source);
        }

        /// <summary>
        /// 获取第一个元素
        /// </summary>
        /// <returns>第一个元素</returns>
        public T First()
        {
            return _source.First();
        }

        /// <summary>
        /// 获取第一个元素（带默认值）
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>第一个元素或默认值</returns>
        public T FirstOrDefault(T defaultValue = default)
        {
            return _source.FirstOrDefault() ?? defaultValue;
        }

        /// <summary>
        /// 获取最后一个元素
        /// </summary>
        /// <returns>最后一个元素</returns>
        public T Last()
        {
            return _source.Last();
        }

        /// <summary>
        /// 获取最后一个元素（带默认值）
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>最后一个元素或默认值</returns>
        public T LastOrDefault(T defaultValue = default)
        {
            return _source.LastOrDefault() ?? defaultValue;
        }

        /// <summary>
        /// 获取唯一元素
        /// </summary>
        /// <returns>唯一元素</returns>
        public T Single()
        {
            return _source.Single();
        }

        /// <summary>
        /// 获取唯一元素（带默认值）
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <returns>唯一元素或默认值</returns>
        public T SingleOrDefault(T defaultValue = default)
        {
            return _source.SingleOrDefault() ?? defaultValue;
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        /// <returns>元素数量</returns>
        public int Count()
        {
            return _source.Count();
        }

        /// <summary>
        /// 检查是否包含指定元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <returns>是否包含</returns>
        public bool Contains(T item)
        {
            return _source.Contains(item);
        }

        /// <summary>
        /// 检查是否所有元素都满足条件
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>是否所有元素都满足条件</returns>
        public bool All(Func<T, bool> predicate)
        {
            return _source.All(predicate);
        }

        /// <summary>
        /// 检查是否任一元素满足条件
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>是否任一元素满足条件</returns>
        public bool Any(Func<T, bool> predicate)
        {
            return _source.Any(predicate);
        }

        /// <summary>
        /// 检查是否为空
        /// </summary>
        /// <returns>是否为空</returns>
        public bool IsEmpty()
        {
            return !_source.Any();
        }

        /// <summary>
        /// 跳过指定数量的元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Skip(int count)
        {
            return new IterChain<T>(_source.Skip(count));
        }

        /// <summary>
        /// 取指定数量的元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Take(int count)
        {
            return new IterChain<T>(_source.Take(count));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns>当前实例</returns>
        public IterChain<T> Sort()
        {
            return new IterChain<T>(_source.OrderBy(x => x));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="comparer">比较器</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Sort(IComparer<T> comparer)
        {
            return new IterChain<T>(_source.OrderBy(x => x, comparer));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="R">排序键类型</typeparam>
        /// <param name="keySelector">键选择器</param>
        /// <returns>当前实例</returns>
        public IterChain<T> SortBy<R>(Func<T, R> keySelector)
        {
            return new IterChain<T>(_source.OrderBy(keySelector));
        }

        /// <summary>
        /// 倒序排序
        /// </summary>
        /// <returns>当前实例</returns>
        public IterChain<T> Reverse()
        {
            return new IterChain<T>(_source.Reverse());
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <returns>当前实例</returns>
        public IterChain<T> Distinct()
        {
            return new IterChain<T>(_source.Distinct());
        }

        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="comparer">比较器</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Distinct(IEqualityComparer<T> comparer)
        {
            return new IterChain<T>(_source.Distinct(comparer));
        }

        /// <summary>
        /// 连接两个集合
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Concat(IEnumerable<T> other)
        {
            return new IterChain<T>(_source.Concat(other));
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Intersect(IEnumerable<T> other)
        {
            return new IterChain<T>(_source.Intersect(other));
        }

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="other">另一个集合</param>
        /// <returns>当前实例</returns>
        public IterChain<T> Except(IEnumerable<T> other)
        {
            return new IterChain<T>(_source.Except(other));
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// IterChain 扩展方法
    /// </summary>
    public static class IterChainExtensions
    {
        /// <summary>
        /// 创建 IterChain
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源集合</param>
        /// <returns>IterChain</returns>
        public static IterChain<T> AsIterChain<T>(this IEnumerable<T> source)
        {
            return new IterChain<T>(source);
        }
    }
}