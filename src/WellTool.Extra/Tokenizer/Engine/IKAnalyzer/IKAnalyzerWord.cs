namespace WellTool.Extra.Tokenizer.Engine.IKAnalyzer
{
    /// <summary>
    /// IKAnalyzer分词中的一个单词包装
    /// </summary>
    public class IKAnalyzerWord : Word
    {
        /// <summary>
        /// 词文本
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public int StartOffset { get; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int EndOffset { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">词文本</param>
        /// <param name="startOffset">起始位置</param>
        /// <param name="endOffset">结束位置</param>
        public IKAnalyzerWord(string text, int startOffset, int endOffset)
        {
            Text = text;
            StartOffset = startOffset;
            EndOffset = endOffset;
        }

        /// <summary>
        /// 获取单词文本
        /// </summary>
        /// <returns>单词文本</returns>
        public string GetText() => Text;

        /// <summary>
        /// 获取本词的起始位置
        /// </summary>
        /// <returns>起始位置</returns>
        public int GetStartOffset() => StartOffset;

        /// <summary>
        /// 获取本词的结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        public int GetEndOffset() => EndOffset;

        /// <summary>
        /// 返回文本表示
        /// </summary>
        public override string ToString() => GetText();
    }
}
