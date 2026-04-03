using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符串连接器
    /// </summary>
    public class StrJoiner
    {
        private readonly string _delimiter;
        private readonly string _prefix;
        private readonly string _suffix;
        private readonly StringBuilder _sb = new StringBuilder();
        private bool _first = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="delimiter">分隔符</param>
        public StrJoiner(string delimiter) : this(delimiter, "", "")
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="delimiter">分隔符</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        public StrJoiner(string delimiter, string prefix, string suffix)
        {
            _delimiter = delimiter ?? "";
            _prefix = prefix ?? "";
            _suffix = suffix ?? "";
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>this</returns>
        public StrJoiner Add(object element)
        {
            if (!_first)
            {
                _sb.Append(_delimiter);
            }
            else
            {
                _sb.Append(_prefix);
                _first = false;
            }
            _sb.Append(element?.ToString() ?? "");
            return this;
        }

        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <param name="elements">元素集合</param>
        /// <returns>this</returns>
        public StrJoiner Add(params object[] elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
            return this;
        }

        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <param name="elements">元素集合</param>
        /// <returns>this</returns>
        public StrJoiner Add(IEnumerable<object> elements)
        {
            foreach (var element in elements)
            {
                Add(element);
            }
            return this;
        }

        /// <summary>
        /// 获取字符串表示
        /// </summary>
        public override string ToString()
        {
            if (_first)
            {
                // 没有添加任何元素
                if (_prefix.Length > 0 || _suffix.Length > 0)
                {
                    return _prefix + _suffix;
                }
                return "";
            }
            return _sb.Append(_suffix).ToString();
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        public int Length => ToString().Length;

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _sb.Clear();
            _first = true;
        }

        /// <summary>
        /// 连接数组元素
        /// </summary>
        public static string Join(string delimiter, params object[] elements)
        {
            var joiner = new StrJoiner(delimiter);
            return joiner.Add(elements).ToString();
        }

        /// <summary>
        /// 连接集合元素
        /// </summary>
        public static string Join(string delimiter, IEnumerable<object> elements)
        {
            var joiner = new StrJoiner(delimiter);
            return joiner.Add(elements).ToString();
        }

        /// <summary>
        /// 连接数组元素，带前缀后缀
        /// </summary>
        public static string Join(string delimiter, string prefix, string suffix, params object[] elements)
        {
            var joiner = new StrJoiner(delimiter, prefix, suffix);
            return joiner.Add(elements).ToString();
        }
    }
}
