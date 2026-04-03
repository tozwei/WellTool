using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 转换集合，包装一个集合并在访问时进行转换
    /// </summary>
    public class TransCollection<TSource, TResult> : IEnumerable<TResult>
    {
        private readonly ICollection<TSource> _source;
        private readonly Func<TSource, TResult> _transformer;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TransCollection(ICollection<TSource> source, Func<TSource, TResult> transformer)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        /// <summary>
        /// 元素数量
        /// </summary>
        public int Count => _source.Count;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<TResult> GetEnumerator()
        {
            foreach (var item in _source)
            {
                yield return _transformer(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// 转换集合扩展
    /// </summary>
    public static class TransCollectionExtensions
    {
        /// <summary>
        /// 转换为转换集合
        /// </summary>
        public static TransCollection<TSource, TResult> AsTransCollection<TSource, TResult>(
            this ICollection<TSource> source,
            Func<TSource, TResult> transformer)
        {
            return new TransCollection<TSource, TResult>(source, transformer);
        }
    }
}
