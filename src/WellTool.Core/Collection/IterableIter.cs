using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 可迭代对象的迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class IterableIter<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterable">可迭代对象</param>
        public IterableIter(IEnumerable<T> iterable)
        {
            _enumerator = iterable.GetEnumerator();
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                return _enumerator.Current;
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
            _enumerator?.Dispose();
        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        /// <returns>是否成功移动</returns>
        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}
