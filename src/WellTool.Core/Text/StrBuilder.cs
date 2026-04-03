using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符串构建器，类似Java的StringBuilder但提供更多便捷方法
    /// </summary>
    public class StrBuilder
    {
        private readonly StringBuilder _sb;
        private readonly int _initialCapacity;

        /// <summary>
        /// 构造函数
        /// </summary>
        public StrBuilder()
        {
            _sb = new StringBuilder();
            _initialCapacity = 16;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public StrBuilder(int capacity)
        {
            _sb = new StringBuilder(capacity);
            _initialCapacity = capacity;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str">初始字符串</param>
        public StrBuilder(string str)
        {
            _sb = new StringBuilder(str ?? "");
            _initialCapacity = _sb.Capacity;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str">初始字符串</param>
        /// <param name="capacity">初始容量</param>
        public StrBuilder(string str, int capacity)
        {
            _sb = new StringBuilder(str ?? "", capacity);
            _initialCapacity = capacity;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        public int Length => _sb.Length;

        /// <summary>
        /// 设置长度
        /// </summary>
        public int Capacity => _sb.Capacity;

        /// <summary>
        /// 追加对象
        /// </summary>
        public StrBuilder Append(object obj)
        {
            _sb.Append(obj?.ToString());
            return this;
        }

        /// <summary>
        /// 追加字符串
        /// </summary>
        public StrBuilder Append(string str)
        {
            _sb.Append(str);
            return this;
        }

        /// <summary>
        /// 追加字符
        /// </summary>
        public StrBuilder Append(char c)
        {
            _sb.Append(c);
            return this;
        }

        /// <summary>
        /// 追加多行文本
        /// </summary>
        public StrBuilder AppendLine(string str = null)
        {
            _sb.AppendLine(str);
            return this;
        }

        /// <summary>
        /// 追加指定次数的字符
        /// </summary>
        public StrBuilder Append(char c, int repeatCount)
        {
            _sb.Append(c, repeatCount);
            return this;
        }

        /// <summary>
        /// 条件追加
        /// </summary>
        public StrBuilder AppendIf(bool condition, string str)
        {
            if (condition)
            {
                _sb.Append(str);
            }
            return this;
        }

        /// <summary>
        /// 格式化追加
        /// </summary>
        public StrBuilder AppendFormat(string format, params object[] args)
        {
            _sb.AppendFormat(format, args);
            return this;
        }

        /// <summary>
        /// 在指定位置插入
        /// </summary>
        public StrBuilder Insert(int index, string str)
        {
            _sb.Insert(index, str);
            return this;
        }

        /// <summary>
        /// 删除指定范围
        /// </summary>
        public StrBuilder Delete(int startIndex, int endIndex)
        {
            _sb.Remove(startIndex, endIndex - startIndex);
            return this;
        }

        /// <summary>
        /// 替换
        /// </summary>
        public StrBuilder Replace(string oldValue, string newValue)
        {
            _sb.Replace(oldValue, newValue);
            return this;
        }

        /// <summary>
        /// 反转
        /// </summary>
        public StrBuilder Reverse()
        {
            _sb.Reverse();
            return this;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public StrBuilder Clear()
        {
            _sb.Clear();
            return this;
        }

        /// <summary>
        /// 确保容量
        /// </summary>
        public StrBuilder EnsureCapacity(int capacity)
        {
            _sb.EnsureCapacity(capacity);
            return this;
        }

        /// <summary>
        /// 获取子字符串
        /// </summary>
        public string Substring(int startIndex, int length = -1)
        {
            if (length < 0)
            {
                return _sb.ToString(startIndex);
            }
            return _sb.ToString(startIndex, length);
        }

        /// <summary>
        /// 获取字符
        /// </summary>
        public char this[int index] => _sb[index];

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            return _sb.ToString();
        }

        /// <summary>
        /// 隐式转换为字符串
        /// </summary>
        public static implicit operator string(StrBuilder sb)
        {
            return sb?.ToString();
        }

        /// <summary>
        /// 重置并复用
        /// </summary>
        public StrBuilder Reset()
        {
            _sb.Clear();
            _sb.EnsureCapacity(_initialCapacity);
            return this;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => _sb.Length == 0;

        /// <summary>
        /// 是否不为空
        /// </summary>
        public bool IsNotEmpty => _sb.Length > 0;

        /// <summary>
        /// 去除首尾空白
        /// </summary>
        public StrBuilder Trim()
        {
            var str = _sb.ToString().Trim();
            _sb.Clear();
            _sb.Append(str);
            return this;
        }

        /// <summary>
        /// 转为StringBuilder
        /// </summary>
        public StringBuilder ToStringBuilder()
        {
            return new StringBuilder(_sb.ToString());
        }
    }
}
