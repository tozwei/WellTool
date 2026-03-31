using System.Collections.Generic;

namespace WellTool.Dfa
{
    /// <summary>
    /// 敏感词处理器
    /// </summary>
    public class SensitiveProcessor
    {
        /// <summary>
        /// 敏感词树
        /// </summary>
        private readonly WordTree _wordTree;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="wordTree">敏感词树</param>
        public SensitiveProcessor(WordTree wordTree)
        {
            _wordTree = wordTree;
        }

        /// <summary>
        /// 处理文本中的敏感词
        /// </summary>
        /// <param name="text">待处理的文本</param>
        /// <param name="replaceChar">替换字符</param>
        /// <returns>处理后的文本</returns>
        public string Process(string text, char replaceChar = '*')
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var result = new char[text.Length];
            text.CopyTo(0, result, 0, text.Length);

            var foundWords = _wordTree.FindAll(text);
            foreach (var foundWord in foundWords)
            {
                for (int i = foundWord.StartIndex; i < foundWord.EndIndex; i++)
                {
                    result[i] = replaceChar;
                }
            }

            return new string(result);
        }

        /// <summary>
        /// 检查文本是否包含敏感词
        /// </summary>
        /// <param name="text">待检查的文本</param>
        /// <returns>是否包含敏感词</returns>
        public bool ContainsSensitive(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return _wordTree.FindFirst(text) != null;
        }

        /// <summary>
        /// 查找文本中的所有敏感词
        /// </summary>
        /// <param name="text">待查找的文本</param>
        /// <returns>敏感词列表</returns>
        public List<FoundWord> FindAllSensitive(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new List<FoundWord>();
            }

            return _wordTree.FindAll(text);
        }
    }
}