using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 复制迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class CopiedIter<T> : IEnumerator<T>
    {
        private readonly List<T> _copy;
        private int _index = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">原始迭代器</param>
        public CopiedIter(IEnumerator<T> iterator)
        {
            _copy = new List<T>();
            while (iterator.MoveNext())
            {
                _copy.Add(iterator.Current);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="collection">集合</param>
        public CopiedIter(IEnumerable<T> collection)
        {
            _copy = new List<T>(collection);
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _copy.Count)
                {
                    throw new System.InvalidOperationException();
                }
                return _copy[_index];
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
        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        /// <returns>是否成功移动</returns>
        public bool MoveNext()
        {
            _index++;
            return _index < _copy.Count;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _index = -1;
        }
    }
}
