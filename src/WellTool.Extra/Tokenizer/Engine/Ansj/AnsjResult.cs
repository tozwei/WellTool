using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer.Engine.Ansj
{
    /// <summary>
    /// Ansj分词结果
    /// </summary>
    public class AnsjResult : Result
    {
        private readonly List<Word> _words;

        public AnsjResult(string text)
        {
            _words = new List<Word>();
            // TODO: 需要集成 Ansj.Net 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var c in text)
            {
                _words.Add(new AnsjWord(c.ToString()));
            }
        }

        public override bool HasNext => _words.Count > 0;

        public override Word Next()
        {
            return _words.Count > 0 ? _words[0] : null;
        }
    }

    /// <summary>
    /// Ansj词条
    /// </summary>
    public class AnsjWord : Word
    {
        private readonly string _text;

        public AnsjWord(string text)
        {
            _text = text;
        }

        public override string Text => _text;
    }
}
