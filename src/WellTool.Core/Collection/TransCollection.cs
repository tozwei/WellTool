using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 可转换的集合包装器
    /// </summary>
    /// <typeparam name="T">源元素类型</typeparam>
    /// <typeparam name="R">目标元素类型</typeparam>
    public class TransCollection<T, R> : IEnumerable<R>
    {
        private readonly IEnumerable<T> _source;
        private readonly Func<T, R> _converter;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">源集合</param>
        /// <param name="converter">转换函数</param>
        public TransCollection(IEnumerable<T> source, Func<T, R> converter)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<R> GetEnumerator()
        {
            foreach (var item in _source)
            {
                yield return _converter(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
