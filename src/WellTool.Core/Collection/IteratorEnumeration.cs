using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 迭代器转枚举器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class IteratorEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _iterator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="iterator">迭代器</param>
        public IteratorEnumerator(IEnumerator<T> iterator)
        {
            _iterator = iterator;
        }

        /// <summary>
        /// 获取当前元素
        /// </summary>
        public T Current => _iterator.Current;

        /// <summary>
        /// 获取当前元素
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 迭代器不需要释放
        }

        /// <summary>
        /// 移动到下一个
        /// </summary>
        public bool MoveNext()
        {
            return _iterator.MoveNext();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _iterator.Reset();
        }
    }

    /// <summary>
    /// 枚举器转迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class EnumerationIterator<T> : IEnumerable<T>, IDisposable
    {
        private readonly Func<IEnumerator<T>> _factory;
        private IEnumerator<T> _currentEnumerator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="enumerable">原始集合</param>
        public EnumerationIterator(IEnumerable<T> enumerable)
        {
            _factory = () => enumerable?.GetEnumerator();
            _currentEnumerator = _factory();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="factory">迭代器工厂</param>
        public EnumerationIterator(Func<IEnumerator<T>> factory)
        {
            _factory = factory;
            _currentEnumerator = _factory();
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            _currentEnumerator = _factory();
            return new Enumerator(_currentEnumerator);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _currentEnumerator?.Dispose();
        }

        private class Enumerator : IEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public Enumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public T Current => _inner.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return _inner.MoveNext();
            }

            public void Reset()
            {
                _inner.Reset();
            }
        }
    }
}
