using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 转换迭代器，将一种类型的迭代器转换为另一种类型
    /// </summary>
    /// <typeparam name="TSource">源元素类型</typeparam>
    /// <typeparam name=" TResult">目标元素类型</typeparam>
    public class TransIter<TSource, TResult> : IEnumerable<TResult>, IDisposable
    {
        private readonly IEnumerable<TSource> _source;
        private readonly Func<TSource, TResult> _converter;
        private bool _disposed = false;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">源迭代器</param>
        /// <param name="converter">转换函数</param>
        public TransIter(IEnumerable<TSource> source, Func<TSource, TResult> converter)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<TResult> GetEnumerator()
        {
            return new TransEnumerator(_source.GetEnumerator(), _converter);
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
            if (!_disposed)
            {
                _disposed = true;
            }
        }

        private class TransEnumerator : IEnumerator<TResult>
        {
            private readonly IEnumerator<TSource> _sourceEnum;
            private readonly Func<TSource, TResult> _converter;

            public TransEnumerator(IEnumerator<TSource> source, Func<TSource, TResult> converter)
            {
                _sourceEnum = source;
                _converter = converter;
            }

            public TResult Current => _converter(_sourceEnum.Current);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return _sourceEnum.MoveNext();
            }

            public void Reset()
            {
                _sourceEnum.Reset();
            }
        }
    }
}
