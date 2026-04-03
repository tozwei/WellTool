using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 计算型迭代器基类，用于按需计算下一元素的迭代器
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public abstract class ComputeIter<T> : IEnumerable<T>, IEnumerator<T>
    {
        private T _current;
        private bool _finished = false;
        private bool _started = false;

        /// <summary>
        /// 获取当前元素
        /// </summary>
        public T Current => _current;

        object IEnumerator.Current => Current;

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 移动到下一个元素
        /// </summary>
        public bool MoveNext()
        {
            if (_finished)
            {
                return false;
            }

            if (!_started)
            {
                _started = true;
            }

            _current = ComputeNext();
            if (_current == null)
            {
                _finished = true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 计算下一个元素，返回 null 表示迭代结束
        /// </summary>
        /// <returns>下一个元素，null 表示迭代结束</returns>
        protected abstract T ComputeNext();

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _finished = false;
            _started = false;
            _current = default(T);
        }

        /// <summary>
        /// 标记迭代完成
        /// </summary>
        protected void Finish()
        {
            _finished = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Finish();
        }
    }
}
