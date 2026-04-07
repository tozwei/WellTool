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
        private T _current;
        private bool _hasCurrent;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">原始迭代器</param>
        /// <param name="predicate">过滤条件</param>
        public FilterIter(IEnumerator<T> iterator, System.Func<T, bool> predicate)
        {
            _iterator = iterator;
            _predicate = predicate;
            _hasCurrent = false;
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                if (!_hasCurrent)
                {
                    throw new System.InvalidOperationException();
                }
                return _current;
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
            while (_iterator.MoveNext())
            {
                var current = _iterator.Current;
                if (_predicate == null || _predicate(current))
                {
                    _current = current;
                    _hasCurrent = true;
                    return true;
                }
            }
            _hasCurrent = false;
            return false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _iterator.Reset();
            _hasCurrent = false;
        }
    }
}
