using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 计算迭代器，用于流式计算
    /// </summary>
    public class ComputeIter<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;
        private readonly Func<T, T> _computer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComputeIter(IEnumerable<T> source, Func<T, T> computer)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _source)
            {
                yield return _computer(item);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 链式计算
        /// </summary>
        public ComputeIter<T> Then(Func<T, T> nextComputer)
        {
            var original = _computer;
            return new ComputeIter<T>(_source, x => nextComputer(original(x)));
        }

        /// <summary>
        /// 计算并返回结果列表
        /// </summary>
        public List<T> ToList()
        {
            return this.ToList();
        }

        /// <summary>
        /// 计算并返回结果数组
        /// </summary>
        public T[] ToArray()
        {
            return this.ToArray();
        }
    }

    /// <summary>
    /// 计算迭代器扩展
    /// </summary>
    public static class ComputeIterExtensions
    {
        /// <summary>
        /// 创建计算迭代器
        /// </summary>
        public static ComputeIter<T> Compute<T>(this IEnumerable<T> source, Func<T, T> computer)
        {
            return new ComputeIter<T>(source, computer);
        }

        /// <summary>
        /// 创建带索引的计算迭代器
        /// </summary>
        public static ComputeIter<T> Compute<T>(this IEnumerable<T> source, Func<T, int, T> computer)
        {
            return new ComputeIter<T>(source.Select((item, index) => computer(item, index)));
        }
    }
}
