// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Text;

namespace WellTool.Dfa;

/// <summary>
/// 敏感词工具类
/// </summary>
public static class SensitiveUtil
{
    /// <summary>
    /// 敏感词树
    /// </summary>
    private static readonly WordTree _wordTree = new();

    /// <summary>
    /// 初始化敏感词树
    /// </summary>
    static SensitiveUtil()
    {
        // 可以在这里添加默认的敏感词
    }

    /// <summary>
    /// 添加敏感词
    /// </summary>
    /// <param name="word">敏感词</param>
    public static void AddWord(string word)
    {
        _wordTree.AddWord(word);
    }

    /// <summary>
    /// 添加敏感词列表
    /// </summary>
    /// <param name="words">敏感词列表</param>
    public static void AddWords(IEnumerable<string> words)
    {
        _wordTree.AddWords(words);
    }

    /// <summary>
    /// 查找文本中的敏感词
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>敏感词列表</returns>
    public static List<FoundWord> FindAll(string text)
    {
        return _wordTree.FindAll(text);
    }

    /// <summary>
    /// 替换文本中的敏感词
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="replacement">替换字符</param>
    /// <returns>替换后的文本</returns>
    public static string Replace(string text, char replacement = '*')
    {
        var foundWords = FindAll(text);
        if (foundWords.Count == 0)
        {
            return text;
        }

        var sb = new StringBuilder(text);
        foreach (var foundWord in foundWords)
        {
            for (var i = foundWord.StartIndex; i <= foundWord.EndIndex; i++)
            {
                sb[i] = replacement;
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// 判断文本是否包含敏感词
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>是否包含敏感词</returns>
    public static bool Contains(string text)
    {
        return FindAll(text).Count > 0;
    }
}
