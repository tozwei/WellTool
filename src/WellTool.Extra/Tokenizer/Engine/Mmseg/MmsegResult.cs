using System.Collections.Generic;
using WellTool.Extra.Tokenizer;

namespace WellTool.Extra.Tokenizer.Engine.Mmseg
{
    /// <summary>
    /// Mmseg分词结果
    /// </summary>
    public class MmsegResult : AbstractResult
    {
        private readonly List<WellTool.Extra.Tokenizer.Word> _words;
        private int _index;

        public MmsegResult(string text)
        {
            _words = new List<WellTool.Extra.Tokenizer.Word>();
            _index = 0;
            // TODO: 需要集成 MMSeg 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var (c, i) in text.Select((ch, index) => (ch, index)))
            {
                _words.Add(new MmsegWord(c.ToString(), i, i + 1));
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
