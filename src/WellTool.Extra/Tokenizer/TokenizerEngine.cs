namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 分词引擎接口定义，用户通过实现此接口完成特定分词引擎的适配
    /// </summary>
    public interface TokenizerEngine
    {
        /// <summary>
        /// 文本分词处理接口，通过实现此接口完成分词，产生分词结果
        /// </summary>
        /// <param name="text">需要分词的文本</param>
        /// <returns>分词结果实现</returns>
        Result Parse(string text);
    }
}