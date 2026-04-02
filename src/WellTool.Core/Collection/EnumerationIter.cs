using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 枚举迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class EnumerationIter<T> : IEnumerator<T>
    {
        private readonly IEnumerator _enumerator;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumerator">枚举器</param>
        public EnumerationIter(IEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public T Current
        {
            get
            {
                return (T)_enumerator.Current;
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
