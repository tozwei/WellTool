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

namespace WellTool.Dfa;

/// <summary>
/// 敏感词树
/// </summary>
public class WordTree
{
    /// <summary>
    /// 根节点
    /// </summary>
    private readonly Node _root = new();

    /// <summary>
    /// 添加敏感词
    /// </summary>
    /// <param name="word">敏感词</param>
    public void AddWord(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return;
        }

        var current = _root;
        foreach (var ch in word)
        {
            current = current.GetOrAddChild(ch);
        }
        current.IsEnd = true;
    }

    /// <summary>
    /// 添加敏感词列表
    /// </summary>
    /// <param name="words">敏感词列表</param>
    public void AddWords(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            AddWord(word);
        }
    }

    /// <summary>
    /// 查找文本中的敏感词
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>敏感词列表</returns>
    public List<FoundWord> FindAll(string text)
    {
        var result = new List<FoundWord>();
        if (string.IsNullOrEmpty(text))
        {
            return result;
        }

        for (var i = 0; i < text.Length; i++)
        {
            var found = Find(text, i);
            if (found != null)
            {
                result.Add(found);
                i += found.Word.Length - 1;
            }
        }

        return result;
    }

    /// <summary>
    /// 从指定位置开始查找敏感词
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="start">开始位置</param>
    /// <returns>找到的敏感词</returns>
    private FoundWord? Find(string text, int start)
    {
        var current = _root;
        var end = start;
        FoundWord? foundWord = null;

        for (var i = start; i < text.Length; i++)
        {
            var ch = text[i];
            
            // 跳过停止字符
            if (StopChar.IsStopChar(ch))
            {
                continue;
            }

            var child = current.GetChild(ch);
            if (child == null)
            {
                break;
            }

            current = child;
            end = i;

            if (current.IsEnd)
            {
                foundWord = new FoundWord
                {
                    Word = text.Substring(start, end - start + 1),
                    StartIndex = start,
                    EndIndex = end
                };
            }
        }

        return foundWord;
    }

    /// <summary>
    /// 节点类
    /// </summary>
    private class Node
    {
        /// <summary>
        /// 子节点
        /// </summary>
        private readonly Dictionary<char, Node> _children = new();

        /// <summary>
        /// 是否为词的结束
        /// </summary>
        public bool IsEnd { get; set; }

        /// <summary>
        /// 获取或添加子节点
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>子节点</returns>
        public Node GetOrAddChild(char ch)
        {
            if (!_children.TryGetValue(ch, out var child))
            {
                child = new Node();
                _children[ch] = child;
            }
            return child;
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>子节点</returns>
        public Node? GetChild(char ch)
        {
            _children.TryGetValue(ch, out var child);
            return child;
        }
    }
}
