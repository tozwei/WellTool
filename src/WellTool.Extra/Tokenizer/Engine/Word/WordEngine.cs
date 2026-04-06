using System;

namespace WellTool.Extra.Tokenizer.Engine.Word
{
    /// <summary>
    /// Word分词引擎实现
    /// 
    /// 需要安装 Word.Net 或类似 NuGet 包
    /// </summary>
    public class WordEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public WordEngine()
        {
        }

        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>分词结果</returns>
        public Result Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            return new WordResult(text);
        }
    }
}
