using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 可重置的迭代器包装
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class ResettableIter<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly Func<IEnumerator<T>> _factory;
        private IEnumerator<T> _currentEnumerator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="enumerable">原始集合</param>
        public ResettableIter(IEnumerable<T> enumerable)
        {
            _factory = () => enumerable?.GetEnumerator();
            _currentEnumerator = _factory();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="factory">迭代器工厂</param>
        public ResettableIter(Func<IEnumerator<T>> factory)
        {
            _factory = factory;
            _currentEnumerator = _factory();
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 重置迭代器
        /// </summary>
        public void Reset()
        {
            _currentEnumerator = _factory();
        }

        /// <summary>
        /// 获取当前元素
        /// </summary>
        public T Current => _currentEnumerator.Current;

        object IEnumerator.Current => Current;

        /// <summary>
        /// 移动到下一个
        /// </summary>
        public bool MoveNext()
        {
            return _currentEnumerator.MoveNext();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _currentEnumerator?.Dispose();
        }
    }
}
