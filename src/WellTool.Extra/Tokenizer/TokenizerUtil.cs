namespace WellTool.Extra.Tokenizer
{
    using System.Collections.Generic;

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
            if (string.IsNullOrEmpty(text))
            {
                return new Result();
            }

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
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void Create(string path)
        {
            // 简化实现
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}