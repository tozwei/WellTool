namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 分词工具类
    /// </summary>
    public class TokenizerUtil
    {
        /// <summary>
        /// 根据用户引入的分词引擎，自动创建对应的分词引擎对象
        /// </summary>
        /// <returns>分词引擎</returns>
        public static TokenizerEngine CreateEngine()
        {
            return TokenizerFactory.Create();
        }
    }
}