using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 过滤迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class FilterIter<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _iterator;
        private readonly System.Func<T, bool> _predicate;
        private T _next;
        private bool _hasNext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">原始迭代器</param>
        /// <param name="predicate">过滤条件</param>
        public FilterIter(IEnumerator<T> iterator, System.Func<T, bool> predicate)
        {
            _iterator = iterator;
            _predicate = predicate;
            _hasNext = MoveNextInternal();
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                if (!_hasNext)
                {
                    throw new System.InvalidOperationException();
                }
                return _next;
            }
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _iterator?.Dispose();
        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        /// <returns>是否成功移动</returns>
        public bool MoveNext()
        {
            var result = _hasNext;
            _hasNext = MoveNextInternal();
            return result;
        }

        /// <summary>
        /// 内部移动到下一个元素
        /// </summary>
        /// <returns>是否成功移动</returns>
        private bool MoveNextInternal()
        {
            while (_iterator.MoveNext())
            {
                var current = _iterator.Current;
                if (_predicate(current))
                {
                    _next = current;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _iterator.Reset();
            _hasNext = MoveNextInternal();
        }
    }
}
