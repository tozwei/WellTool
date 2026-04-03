namespace WellTool.Extra.Tokenizer.Engine.Word
{
    /// <summary>
    /// Word分词中的一个单词包装
    /// </summary>
    public class WordWord : Word
    {
        /// <summary>
        /// 词文本
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">词文本</param>
        public WordWord(string text)
        {
            Text = text;
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
        public int GetStartOffset() => -1;

        /// <summary>
        /// 获取本词的结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        public int GetEndOffset() => -1;

        /// <summary>
        /// 返回文本表示
        /// </summary>
        public override string ToString() => GetText();
    }
}
