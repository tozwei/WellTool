using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 分词结果接口定义
    /// 实现此接口包装分词器的分词结果，通过实现IEnumerable相应方法获取分词中的单词
    /// </summary>
    public interface Result : IEnumerable<Word>
    {
    }
}