using System;

namespace WellTool.Extra.Tokenizer.Engine.Jieba
{
    /// <summary>
    /// Jieba分词引擎实现
    /// 
    /// 需要安装 JiebaNet 或类似 NuGet 包
    /// 项目地址：https://github.com/huaban/jieba-analysis
    /// </summary>
    public class JiebaEngine : TokenizerEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public JiebaEngine()
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
            return new JiebaResult(text);
        }
    }
}
