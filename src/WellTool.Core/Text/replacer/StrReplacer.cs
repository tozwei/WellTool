using System;
using System.Text;
using WellTool.Core.Text;

namespace WellTool.Core.Text.Replacer
{
    /// <summary>
    /// 查找替换器接口
    /// </summary>
    public interface IReplacer
    {
        /// <summary>
        /// 替换
        /// </summary>
        string Replace(string text);
    }

    /// <summary>
    /// 字符串替换器抽象基类
    /// </summary>
    public abstract class StrReplacer : IReplacer
    {
        /// <summary>
        /// 替换文本
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="textBuilder">文本构建器</param>
        /// <returns>替换后的文本</returns>
        public virtual string Replace(string text, StringBuilder textBuilder)
        {
            return text;
        }

        /// <summary>
        /// 替换文本
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="pos">位置</param>
        /// <param name="outBuilder">输出构建器</param>
        /// <returns>替换的字符数</returns>
        public abstract int Replace(string str, int pos, StrBuilder outBuilder);

        /// <summary>
        /// 替换文本
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>替换后的文本</returns>
        public string Replace(string text)
        {
            var outBuilder = new StrBuilder();
            int pos = 0;
            int consumed;
            while (pos < text.Length)
            {
                consumed = Replace(text, pos, outBuilder);
                if (consumed == 0)
                {
                    outBuilder.Append(text[pos]);
                    pos++;
                }
                else
                {
                    pos += consumed;
                }
            }
            return outBuilder.ToString();
        }
    }
}
