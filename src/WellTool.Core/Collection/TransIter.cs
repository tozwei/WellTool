using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 转换迭代器
    /// </summary>
    /// <typeparam name="T">输入类型</typeparam>
    /// <typeparam name="R">输出类型</typeparam>
    public class TransIter<T, R> : IEnumerator<R>
    {
        private readonly IEnumerator<T> _iterator;
        private readonly System.Func<T, R> _func;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">原始迭代器</param>
        /// <param name="func">转换函数</param>
        public TransIter(IEnumerator<T> iterator, System.Func<T, R> func)
        {
            _iterator = iterator;
            _func = func;
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public R Current
        {
            get
            {
                return _func(_iterator.Current);
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
}
