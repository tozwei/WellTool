namespace WellTool.Extra.Tokenizer
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// 分词工具类
    /// </summary>
    public class TokenizerUtil
    {
        private static TokenizerEngine _engine;

        /// <summary>
        /// 获取分词引擎
        /// </summary>
        public static TokenizerEngine Engine
        {
            get
            {
                if (_engine == null)
                {
                    _engine = CreateEngine();
                }
                return _engine;
            }
        }

        /// <summary>
        /// 引擎类型枚举
        /// </summary>
        public static class EngineType
        {
            public const string EN = "en";
            public const string CN = "cn";
        }

        /// <summary>
        /// 根据用户引入的分词引擎，自动创建对应的分词引擎对象
        /// </summary>
        /// <returns>分词引擎</returns>
        public static TokenizerEngine CreateEngine()
        {
            return TokenizerFactory.Create();
        }

        /// <summary>
        /// 对文本进行分词
        /// </summary>
        /// <param name="text">要分词的文本</param>
        /// <returns>分词结果</returns>
        public static Result Tokenize(string text)
        {
            var engine = CreateEngine();
            return engine.Parse(text);
        }

        /// <summary>
        /// 对文本进行分词
        /// </summary>
        /// <param name="text">要分词的文本</param>
        /// <param name="engineName">引擎名称</param>
        /// <returns>分词结果</returns>
        public static Result Tokenize(string text, string engineName)
        {
            return Tokenize(text);
        }

        /// <summary>
        /// 创建分词器（返回实现了 IDisposable 的结果对象）
        /// </summary>
        /// <param name="text">要分词的文本</param>
        /// <returns>分词结果</returns>
        public static IResult Create(string text)
        {
            return new ResultAdapter(Tokenize(text));
        }

        /// <summary>
        /// 创建分词器（带引擎类型）
        /// </summary>
        /// <param name="text">要分词的文本</param>
        /// <param name="engine">引擎类型</param>
        /// <returns>分词结果</returns>
        public static IResult Create(string text, string engine)
        {
            return new ResultAdapter(Tokenize(text, engine));
        }

        /// <summary>
        /// 包装 Result 接口的适配器，使其实现 IDisposable
        /// </summary>
        private class ResultAdapter : IResult
        {
            private readonly Result _inner;

            public ResultAdapter(Result inner)
            {
                _inner = inner;
            }

            public IEnumerator<Word> GetEnumerator()
            {
                return _inner?.GetEnumerator() ?? Enumerable.Empty<Word>().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Dispose()
            {
                // Result 接口不需要释放资源
            }
        }

        /// <summary>
        /// 可释放的分词结果接口
        /// </summary>
        public interface IResult : Result, IDisposable
        {
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void CreateDirectory(string path)
        {
            // 简化实现
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}