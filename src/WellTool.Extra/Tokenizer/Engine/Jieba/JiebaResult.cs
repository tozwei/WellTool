using System.Collections.Generic;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Jieba
{
    /// <summary>
    /// Jieba分词结果
    /// </summary>
    public class JiebaResult : AbstractResult
    {
        private readonly List<WellTool.Extra.Tokenizer.Word> _words;
        private int _index;

        public JiebaResult(string text)
        {
            _words = new List<WellTool.Extra.Tokenizer.Word>();
            _index = 0;
            // TODO: 需要集成 Jieba.Net 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var (c, i) in text.Select((ch, index) => (ch, index)))
            {
                _words.Add(new JiebaWord(c.ToString(), i, i + 1));
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
