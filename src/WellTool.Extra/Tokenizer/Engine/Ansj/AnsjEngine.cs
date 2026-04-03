using System;

namespace WellTool.Extra.Tokenizer.Engine.Ansj
{
    /// <summary>
    /// Ansj分词引擎实现
    /// 
    /// 需要安装 Ansj.Net 或类似 NuGet 包
    /// 项目地址：https://github.com/NLPchina/ansj_seg
    /// </summary>
    public class AnsjEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AnsjEngine()
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
            return new AnsjResult(text);
        }
    }
}
