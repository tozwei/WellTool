using Lucene.Net.Analysis.TokenAttributes;

namespace WellTool.Extra.Tokenizer.Engine.Analysis
{
    /// <summary>
    /// Lucene-analysis分词中的一个单词包装
    /// </summary>
    public class AnalysisWord : WellTool.Extra.Tokenizer.Word
    {
        private readonly ICharTermAttribute _word;
        private readonly IOffsetAttribute _offsetAtt;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="word">词条属性</param>
        /// <param name="offsetAtt">偏移属性</param>
        public AnalysisWord(ICharTermAttribute word, IOffsetAttribute offsetAtt)
        {
            _word = word;
            _offsetAtt = offsetAtt;
        }

        /// <summary>
        /// 获取单词文本
        /// </summary>
        /// <returns>单词文本</returns>
        public string GetText()
        {
            return _word.ToString();
        }

        /// <summary>
        /// 获取本词的起始位置
        /// </summary>
        /// <returns>起始位置</returns>
        public int GetStartOffset()
        {
            return _offsetAtt?.StartOffset ?? -1;
        }

        /// <summary>
        /// 获取本词的结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        public int GetEndOffset()
        {
            return _offsetAtt?.EndOffset ?? -1;
        }

        /// <summary>
        /// 返回文本表示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetText();
        }
    }
}
