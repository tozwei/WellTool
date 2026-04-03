using System;

namespace WellTool.Extra.Tokenizer.Engine.Mmseg
{
    /// <summary>
    /// Mmseg分词引擎实现
    /// 
    /// 需要安装 Mmseg4Net 或类似 NuGet 包
    /// 项目地址：https://github.com/chenlb/mmseg4j-core
    /// </summary>
    public class MmsegEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public MmsegEngine()
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
            return new MmsegResult(text);
        }
    }
}
