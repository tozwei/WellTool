using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer.Engine.Jcseg
{
    /// <summary>
    /// Jcseg分词结果
    /// </summary>
    public class JcsegResult : Result
    {
        private readonly List<Word> _words;

        public JcsegResult(string text)
        {
            _words = new List<Word>();
            // TODO: 需要集成 Jcseg.Net 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var c in text)
            {
                _words.Add(new JcsegWord(c.ToString()));
            }
        }

        public override bool HasNext => _words.Count > 0;

        public override Word Next()
        {
            return _words.Count > 0 ? _words[0] : null;
        }
    }

    /// <summary>
    /// Jcseg词条
    /// </summary>
    public class JcsegWord : Word
    {
        private readonly string _text;

        public JcsegWord(string text)
        {
            _text = text;
        }

        public override string Text => _text;
    }
}
