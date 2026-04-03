using System;
using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer.Engine
{
    /// <summary>
    /// 分词引擎工厂
    /// </summary>
    public static class TokenizerFactory
    {
        private static TokenizerEngine _instance;

        /// <summary>
        /// 获取单例的分词引擎
        /// </summary>
        /// <returns>分词引擎</returns>
        public static TokenizerEngine Get()
        {
            if (_instance == null)
            {
                _instance = Create();
            }
            return _instance;
        }

        /// <summary>
        /// 创建分词引擎
        /// </summary>
        /// <returns>分词引擎</returns>
        public static TokenizerEngine Create()
        {
            var engine = DoCreate();
            return engine;
        }

        /// <summary>
        /// 执行创建分词引擎
        /// </summary>
        /// <returns>分词引擎</returns>
        private static TokenizerEngine DoCreate()
        {
            // 尝试加载可用的分词引擎
            // 默认返回简单的空格分词引擎
            try
            {
                // 可以在这里添加更多的引擎检测逻辑
                return new SimpleTokenizerEngine();
            }
            catch
            {
                throw new TokenizerException("No tokenizer engine found! Please add a tokenizer package to your project!");
            }
        }
    }

    /// <summary>
    /// 简单分词引擎实现（基于空格分割）
    /// </summary>
    public class SimpleTokenizerEngine : TokenizerEngine
    {
        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>分词结果</returns>
        public Result Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new SimpleResult();
            }

            var words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new SimpleResult(words);
        }
    }

    /// <summary>
    /// 简单分词结果
    /// </summary>
    public class SimpleResult : Result
    {
        private readonly string[] _words;
        private int _index = -1;

        public SimpleResult()
        {
            _words = Array.Empty<string>();
        }

        public SimpleResult(string[] words)
        {
            _words = words;
        }

        public bool HasNext()
        {
            return _index < _words.Length - 1;
        }

        public Word Next()
        {
            if (HasNext())
            {
                _index++;
                return new Word(_words[_index]);
            }
            return null;
        }

        public void Reset()
        {
            _index = -1;
        }
    }

    /// <summary>
    /// 分词结果单词
    /// </summary>
    public class Word
    {
        public string Text { get; }
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        public Word(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
