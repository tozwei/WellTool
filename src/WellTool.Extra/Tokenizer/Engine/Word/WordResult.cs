using System.Collections.Generic;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Word
{
    /// <summary>
    /// Word分词结果
    /// </summary>
    public class WordResult : AbstractResult
    {
        private readonly List<Word> _words;
        private int _index;

        public WordResult(string text)
        {
            _words = new List<Word>();
            _index = 0;
            // TODO: 需要集成 Word.Net 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var c in text)
            {
                _words.Add(new WordWord(c.ToString()));
            }
        }

        protected override Word NextWord()
        {
            if (_index < _words.Count)
            {
                return _words[_index++];
            }
            return null;
        }
    }
}
