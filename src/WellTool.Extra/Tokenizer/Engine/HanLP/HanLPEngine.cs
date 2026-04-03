using System;

namespace WellTool.Extra.Tokenizer.Engine.HanLP
{
    /// <summary>
    /// HanLP分词引擎实现
    /// 
    /// 需要安装 HanLP.NET 或类似 NuGet 包
    /// 项目地址：https://github.com/hankcs/HanLP
    /// </summary>
    public class HanLPEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public HanLPEngine()
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
            return new HanLPResult(text);
        }
    }
}
