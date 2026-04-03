using System;

namespace WellTool.Extra.Tokenizer.Engine.MyNlp
{
    /// <summary>
    /// MyNlp分词引擎实现
    /// 
    /// 需要安装 MyNlp.Net 或类似 NuGet 包
    /// </summary>
    public class MyNlpEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public MyNlpEngine()
        {
        }

        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>分词结果</returns>
        public override Result Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            return new MyNlpResult(text);
        }
    }
}
