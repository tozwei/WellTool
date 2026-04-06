namespace WellTool.Extra.Tokenizer.Engine.MyNlp
{
    /// <summary>
    /// MyNlp分词中的一个单词包装
    /// </summary>
    public class MyNlpWord : WellTool.Extra.Tokenizer.Word
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
        public MyNlpWord(string text, int startOffset)
        {
            Text = text;
            StartOffset = startOffset;
            EndOffset = startOffset + text.Length;
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

        /// <summary>
        /// 比较两个单词
        /// </summary>
        /// <param name="obj">要比较的对象</param>
        /// <returns>比较结果</returns>
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj is WellTool.Extra.Tokenizer.Word otherWord)
            {
                return string.Compare(this.GetText(), otherWord.GetText(), StringComparison.Ordinal);
            }
            throw new ArgumentException("Object is not a Word");
        }
    }
}
