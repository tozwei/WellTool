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

using System;

namespace WellTool.Core.Text;

/// <summary>
/// 文本相似度计算<br>
/// 提供多种文本相似度计算方法
/// </summary>
public static class TextSimilarity
{
    /// <summary>
    /// 计算两个字符串的编辑距离（Levenshtein距离）
    /// </summary>
    /// <param name="s1">第一个字符串</param>
    /// <param name="s2">第二个字符串</param>
    /// <returns>编辑距离</returns>
    public static int EditDistance(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1))
        {
            return string.IsNullOrEmpty(s2) ? 0 : s2.Length;
        }

        if (string.IsNullOrEmpty(s2))
        {
            return s1.Length;
        }

        var matrix = new int[s1.Length + 1, s2.Length + 1];

        for (var i = 0; i <= s1.Length; i++)
        {
            matrix[i, 0] = i;
        }

        for (var j = 0; j <= s2.Length; j++)
        {
            matrix[0, j] = j;
        }

        for (var i = 1; i <= s1.Length; i++)
        {
            for (var j = 1; j <= s2.Length; j++)
            {
                var cost = s1[i - 1] == s2[j - 1] ? 0 : 1;
                matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
            }
        }

        return matrix[s1.Length, s2.Length];
    }

    /// <summary>
    /// 计算两个字符串的相似度（基于编辑距离）
    /// </summary>
    /// <param name="s1">第一个字符串</param>
    /// <param name="s2">第二个字符串</param>
    /// <returns>相似度（0-1之间，越接近1越相似）</returns>
    public static double Similarity(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
        {
            return 1.0;
        }

        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
        {
            return 0.0;
        }

        var distance = EditDistance(s1, s2);
        var maxLength = Math.Max(s1.Length, s2.Length);
        return 1.0 - (double)distance / maxLength;
    }

    /// <summary>
    /// 计算两个字符串的余弦相似度
    /// </summary>
    /// <param name="s1">第一个字符串</param>
    /// <param name="s2">第二个字符串</param>
    /// <returns>余弦相似度（0-1之间，越接近1越相似）</returns>
    public static double CosineSimilarity(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
        {
            return 1.0;
        }

        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
        {
            return 0.0;
        }

        var words1 = s1.Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '[', ']', '{', '}', '\'', '"' }, StringSplitOptions.RemoveEmptyEntries);
        var words2 = s2.Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '[', ']', '{', '}', '\'', '"' }, StringSplitOptions.RemoveEmptyEntries);

        var wordSet = new HashSet<string>(words1);
        wordSet.UnionWith(words2);

        var vector1 = new Dictionary<string, int>();
        var vector2 = new Dictionary<string, int>();

        foreach (var word in wordSet)
        {
            vector1[word] = 0;
            vector2[word] = 0;
        }

        foreach (var word in words1)
        {
            vector1[word]++;
        }

        foreach (var word in words2)
        {
            vector2[word]++;
        }

        var dotProduct = 0;
        var norm1 = 0;
        var norm2 = 0;

        foreach (var word in wordSet)
        {
            dotProduct += vector1[word] * vector2[word];
            norm1 += vector1[word] * vector1[word];
            norm2 += vector2[word] * vector2[word];
        }

        if (norm1 == 0 || norm2 == 0)
        {
            return 0.0;
        }

        return dotProduct / (Math.Sqrt(norm1) * Math.Sqrt(norm2));
    }
}
