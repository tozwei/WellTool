using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 数组迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class ArrayIter<T> : IEnumerator<T>
    {
        private readonly T[] _array;
        private int _index = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="array">数组</param>
        public ArrayIter(T[] array)
        {
            _array = array;
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _array.Length)
                {
                    throw new System.InvalidOperationException();
                }
                return _array[_index];
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
            return _index < _array.Length;
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
