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

using System.Numerics;

namespace WellTool.Core.Text;

/// <summary>
/// 文本相似度计算 - SimHash算法实现
/// </summary>
public class Simhash
{
    /// <summary>
    /// 计算文本的SimHash值
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns>SimHash值</returns>
    public static BigInteger Compute(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return BigInteger.Zero;
        }

        // 简单的分词处理
        var words = text.Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '[', ']', '{', '}', '\'', '"' }, StringSplitOptions.RemoveEmptyEntries);
        var featureCount = words.Length;

        // 初始化特征向量
        var vector = new int[64];

        // 计算特征向量
        foreach (var word in words)
        {
            // 计算每个词的哈希值
            var hash = word.GetHashCode();

            // 对每个比特位进行处理
            for (var i = 0; i < 64; i++)
            {
                if ((hash & (1L << i)) != 0)
                {
                    vector[i]++;
                }
                else
                {
                    vector[i]--;
                }
            }
        }

        // 生成SimHash值
        BigInteger simhash = BigInteger.Zero;
        for (var i = 0; i < 64; i++)
        {
            if (vector[i] > 0)
            {
                simhash |= BigInteger.One << i;
            }
        }

        return simhash;
    }

    /// <summary>
    /// 计算两个SimHash值的汉明距离
    /// </summary>
    /// <param name="hash1">第一个SimHash值</param>
    /// <param name="hash2">第二个SimHash值</param>
    /// <returns>汉明距离</returns>
    public static int HammingDistance(BigInteger hash1, BigInteger hash2)
    {
        var xor = hash1 ^ hash2;
        var distance = 0;

        while (xor > 0)
        {
            distance++;
            xor &= xor - 1;
        }

        return distance;
    }

    /// <summary>
    /// 计算两个文本的相似度
    /// </summary>
    /// <param name="text1">第一个文本</param>
    /// <param name="text2">第二个文本</param>
    /// <returns>相似度（0-1之间，越接近1越相似）</returns>
    public static double Similarity(string text1, string text2)
    {
        var hash1 = Compute(text1);
        var hash2 = Compute(text2);
        var distance = HammingDistance(hash1, hash2);

        // 相似度 = 1 - 汉明距离 / 64
        return 1.0 - (double)distance / 64;
    }
}
