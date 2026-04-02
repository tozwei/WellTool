namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 分词引擎工厂
    /// </summary>
    public static class TokenizerFactory
    {
        /// <summary>
        /// 创建分词引擎
        /// </summary>
        /// <returns>分词引擎</returns>
        public static TokenizerEngine Create()
        {
            // 简化实现，实际项目中可以根据配置或依赖自动检测并创建对应的分词引擎
            return new DefaultTokenizerEngine();
        }
    }

    /// <summary>
    /// 默认分词引擎实现
    /// </summary>
    internal class DefaultTokenizerEngine : TokenizerEngine
    {
        /// <summary>
        /// 文本分词处理接口，通过实现此接口完成分词，产生分词结果
        /// </summary>
        /// <param name="text">需要分词的文本</param>
        /// <returns>分词结果实现</returns>
        public Result Parse(string text)
        {
            return new DefaultResult(text);
        }
    }

    /// <summary>
    /// 默认分词结果实现
    /// </summary>
    internal class DefaultResult : AbstractResult
    {
        private string[] _words;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        public DefaultResult(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _words = Array.Empty<string>();
            }
            else
            {
                _words = text.Split(' ');
            }
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public override IEnumerator<Word> GetEnumerator()
        {
            int index = 0;
            while (index < _words.Length)
            {
                string wordText = _words[index];
                int startOffset = index;
                int endOffset = startOffset + wordText.Length;
                index++;
                yield return new DefaultWord(wordText, startOffset, endOffset);
            }
        }

        /// <summary>
        /// 下一个单词，通过实现此方法获取下一个单词，null表示无下一个结果。
        /// </summary>
        /// <returns>下一个单词或null</returns>
        protected override Word NextWord()
        {
            // 此方法不再使用，因为我们重写了GetEnumerator()方法
            return null;
        }
    }

    /// <summary>
    /// 默认单词实现
    /// </summary>
    internal class DefaultWord : Word
    {
        private string _text;
        private int _startOffset;
        private int _endOffset;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">单词文本</param>
        /// <param name="startOffset">起始位置</param>
        /// <param name="endOffset">结束位置</param>
        public DefaultWord(string text, int startOffset, int endOffset)
        {
            _text = text;
            _startOffset = startOffset;
            _endOffset = endOffset;
        }

        /// <summary>
        /// 获取单词文本
        /// </summary>
        /// <returns>单词文本</returns>
        public string GetText()
        {
            return _text;
        }

        /// <summary>
        /// 获取本词的起始位置
        /// </summary>
        /// <returns>起始位置</returns>
        public int GetStartOffset()
        {
            return _startOffset;
        }

        /// <summary>
        /// 获取本词的结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        public int GetEndOffset()
        {
            return _endOffset;
        }

        /// <summary>
        /// 比较当前对象与另一个对象
        /// </summary>
        /// <param name="obj">另一个对象</param>
        /// <returns>比较结果</returns>
        public int CompareTo(object obj)
        {
            if (obj is DefaultWord other)
            {
                return _text.CompareTo(other._text);
            }
            return 0;
        }
    }
}