using System.Collections;
using System.Collections.Generic;

namespace WellTool.Extra.Tokenizer
{
    /// <summary>
    /// 对于未实现IEnumerator接口的普通结果类，装饰为Result
    /// 普通的结果类只需实现NextWord() 即可
    /// </summary>
    public abstract class AbstractResult : Result
    {
        /// <summary>
        /// 下一个单词，通过实现此方法获取下一个单词，null表示无下一个结果。
        /// </summary>
        /// <returns>下一个单词或null</returns>
        protected abstract Word NextWord();

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<Word> GetEnumerator()
        {
            Word word;
            while ((word = NextWord()) != null)
            {
                yield return word;
            }
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}