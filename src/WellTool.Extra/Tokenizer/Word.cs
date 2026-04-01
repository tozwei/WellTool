using System;

namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 表示分词中的一个词
    /// </summary>
    public interface Word : IComparable
    {
        /// <summary>
        /// 获取单词文本
        /// </summary>
        /// <returns>单词文本</returns>
        string GetText();

        /// <summary>
        /// 获取本词的起始位置
        /// </summary>
        /// <returns>起始位置</returns>
        int GetStartOffset();

        /// <summary>
        /// 获取本词的结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        int GetEndOffset();
    }
}