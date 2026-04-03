using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer.Engine.HanLP
{
    /// <summary>
    /// HanLP分词结果
    /// </summary>
    public class HanLPResult : Result
    {
        private readonly List<Word> _words;

        public HanLPResult(string text)
        {
            _words = new List<Word>();
            // TODO: 需要集成 HanLP.NET 或类似库进行实际分词
            // 临时实现：按字符分割
            foreach (var c in text)
            {
                _words.Add(new HanLPWord(c.ToString()));
            }
        }

        public override bool HasNext => _words.Count > 0;

        public override Word Next()
        {
            return _words.Count > 0 ? _words[0] : null;
        }
    }

    /// <summary>
    /// HanLP词条
    /// </summary>
    public class HanLPWord : Word
    {
        private readonly string _text;

        public HanLPWord(string text)
        {
            _text = text;
        }

        public override string Text => _text;
    }
}
