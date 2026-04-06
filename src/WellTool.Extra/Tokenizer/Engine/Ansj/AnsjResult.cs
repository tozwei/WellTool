using System.Collections.Generic;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Ansj
{
    /// <summary>
    /// Ansj分词结果
    /// </summary>
    public class AnsjResult : AbstractResult
    {
        private readonly List<WellTool.Extra.Tokenizer.Word> _words;
        private int _index;

        public AnsjResult(string text)
        {
            _words = new List<WellTool.Extra.Tokenizer.Word>();
            _index = 0;
            // TODO: 需要集成 Ansj 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var (c, i) in text.Select((ch, index) => (ch, index)))
            {
                _words.Add(new AnsjWord(c.ToString(), i));
            }
        }

        protected override WellTool.Extra.Tokenizer.Word NextWord()
        {
            if (_index < _words.Count)
            {
                return _words[_index++];
            }
            return null;
        }
    }
}
