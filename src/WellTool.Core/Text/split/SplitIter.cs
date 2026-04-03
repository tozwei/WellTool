using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Text.Split
{
    /// <summary>
    /// 字符串切分迭代器
    /// </summary>
    public class SplitIter : IEnumerable<string>, IEnumerator<string>
    {
        private readonly string _text;
        private readonly char _separator;
        private int _index = -1;
        private string _current;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitIter(string text, char separator)
        {
            _text = text ?? "";
            _separator = separator;
        }

        /// <summary>
        /// 当前元素
        /// </summary>
        public string Current => _current;

        object IEnumerator.Current => Current;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 移动到下一个
        /// </summary>
        public bool MoveNext()
        {
            if (_index >= _text.Length)
            {
                return false;
            }

            int nextIndex = _text.IndexOf(_separator, _index + 1);
            if (nextIndex < 0)
            {
                _current = _text.Substring(_index + 1);
                _index = _text.Length;
            }
            else
            {
                _current = _text.Substring(_index + 1, nextIndex - _index - 1);
                _index = nextIndex;
            }
            return true;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _index = -1;
            _current = null;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            // 无需释放
        }
    }

    /// <summary>
    /// 切分迭代器扩展
    /// </summary>
    public static class SplitIterExtensions
    {
        /// <summary>
        /// 按字符切分
        /// </summary>
        public static SplitIter Split(this string text, char separator)
        {
            return new SplitIter(text, separator);
        }

        /// <summary>
        /// 按字符串切分
        /// </summary>
        public static IEnumerable<string> Split(this string text, string separator)
        {
            if (string.IsNullOrEmpty(text)) yield break;
            if (string.IsNullOrEmpty(separator))
            {
                yield return text;
                yield break;
            }

            int index = 0;
            int next;
            while ((next = text.IndexOf(separator, index, StringComparison.Ordinal)) >= 0)
            {
                yield return text.Substring(index, next - index);
                index = next + separator.Length;
            }
            yield return text.Substring(index);
        }
    }
}
