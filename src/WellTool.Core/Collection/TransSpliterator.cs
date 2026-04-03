using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 可转换的 Spliterator
    /// </summary>
    /// <typeparam name="T">源元素类型</typeparam>
    /// <typeparam name="R">目标元素类型</typeparam>
    public class TransSpliterator<T, R> : ISpliterator<R>
    {
        private readonly ISpliterator<T> _source;
        private readonly Func<T, R> _converter;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">源 Spliterator</param>
        /// <param name="converter">转换函数</param>
        public TransSpliterator(ISpliterator<T> source, Func<T, R> converter)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// 尝试分割
        /// </summary>
        public ISpliterator<R> TrySplit()
        {
            var split = _source.TrySplit();
            return split != null ? new TransSpliterator<T, R>(split, _converter) : null;
        }

        /// <summary>
        /// 估算剩余元素数量
        /// </summary>
        public long EstimateSize() => _source.EstimateSize();

        /// <summary>
        /// 前进
        /// </summary>
        public bool TryAdvance(Action<R> action)
        {
            return _source.TryAdvance(item => action(_converter(item)));
        }

        /// <summary>
        /// 遍历剩余元素
        /// </summary>
        public void ForEachRemaining(Action<R> action)
        {
            _source.ForEachRemaining(item => action(_converter(item)));
        }
    }
}
