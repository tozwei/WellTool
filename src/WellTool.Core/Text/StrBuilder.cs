using System;
using System.Text;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符串构建器工具类
    /// </summary>
    public class StrBuilder
    {
        private readonly StringBuilder _builder;

        /// <summary>
        /// 构造函数
        /// </summary>
        public StrBuilder()
        {
            _builder = new StringBuilder();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capacity">初始容量</param>
        public StrBuilder(int capacity)
        {
            _builder = new StringBuilder(capacity);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str">初始字符串</param>
        public StrBuilder(string str)
        {
            _builder = new StringBuilder(str);
        }

        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(string str)
        {
            _builder.Append(str);
            return this;
        }

        /// <summary>
        /// 追加字符
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(char c)
        {
            _builder.Append(c);
            return this;
        }

        /// <summary>
        /// 追加整数
        /// </summary>
        /// <param name="i">整数</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(int i)
        {
            _builder.Append(i);
            return this;
        }

        /// <summary>
        /// 追加长整数
        /// </summary>
        /// <param name="l">长整数</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(long l)
        {
            _builder.Append(l);
            return this;
        }

        /// <summary>
        /// 追加浮点数
        /// </summary>
        /// <param name="d">浮点数</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(double d)
        {
            _builder.Append(d);
            return this;
        }

        /// <summary>
        /// 追加对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>当前实例</returns>
        public StrBuilder Append(object obj)
        {
            _builder.Append(obj);
            return this;
        }

        /// <summary>
        /// 追加换行符
        /// </summary>
        /// <returns>当前实例</returns>
        public StrBuilder AppendLine()
        {
            _builder.AppendLine();
            return this;
        }

        /// <summary>
        /// 追加带换行符的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>当前实例</returns>
        public StrBuilder AppendLine(string str)
        {
            _builder.AppendLine(str);
            return this;
        }

        /// <summary>
        /// 插入字符串
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="str">字符串</param>
        /// <returns>当前实例</returns>
        public StrBuilder Insert(int index, string str)
        {
            _builder.Insert(index, str);
            return this;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <returns>当前实例</returns>
        public StrBuilder Replace(string oldValue, string newValue)
        {
            _builder.Replace(oldValue, newValue);
            return this;
        }

        /// <summary>
        /// 删除字符串
        /// </summary>
        /// <param name="startIndex">开始位置</param>
        /// <param name="length">长度</param>
        /// <returns>当前实例</returns>
        public StrBuilder Remove(int startIndex, int length)
        {
            _builder.Remove(startIndex, length);
            return this;
        }

        /// <summary>
        /// 清空字符串
        /// </summary>
        /// <returns>当前实例</returns>
        public StrBuilder Clear()
        {
            _builder.Clear();
            return this;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        public int Length => _builder.Length;

        /// <summary>
        /// 设置长度
        /// </summary>
        /// <param name="length">长度</param>
        public void SetLength(int length)
        {
            _builder.Length = length;
        }

        /// <summary>
        /// 获取字符
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>字符</returns>
        public char this[int index]
        {
            get => _builder[index];
            set => _builder[index] = value;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return _builder.ToString();
        }

        /// <summary>
        /// 转换为 StringBuilder
        /// </summary>
        /// <returns>StringBuilder</returns>
        public StringBuilder ToStringBuilder()
        {
            return _builder;
        }
    }
}