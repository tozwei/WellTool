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

using System.Text.RegularExpressions;

namespace WellTool.Extra;

/// <summary>
/// 分词工具类
/// </summary>
public class TokenizerUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static TokenizerUtil Instance { get; } = new TokenizerUtil();

    /// <summary>
    /// 分词
    /// </summary>
    /// <param name="text">要分词的文本</param>
    /// <returns>分词结果</returns>
    public List<string> Tokenize(string text)
    {
        try
        {
            // 简单实现，实际项目中可能需要使用更专业的分词库
            // 这里使用正则表达式进行简单分词
            var result = new List<string>();
            
            // 匹配中文词
            var chineseMatches = Regex.Matches(text, @"[\u4e00-\u9fa5]+");
            foreach (Match match in chineseMatches)
            {
                result.Add(match.Value);
            }
            
            // 匹配英文词
            var englishMatches = Regex.Matches(text, @"[a-zA-Z]+");
            foreach (Match match in englishMatches)
            {
                result.Add(match.Value);
            }
            
            // 匹配数字
            var numberMatches = Regex.Matches(text, @"\d+");
            foreach (Match match in numberMatches)
            {
                result.Add(match.Value);
            }
            
            return result;
        }
        catch (Exception ex)
        {
            throw new TokenizerException("分词失败", ex);
        }
    }

    /// <summary>
    /// 分词并统计词频
    /// </summary>
    /// <param name="text">要分词的文本</param>
    /// <returns>词频统计结果</returns>
    public Dictionary<string, int> TokenizeWithFrequency(string text)
    {
        try
        {
            var tokens = Tokenize(text);
            var frequency = new Dictionary<string, int>();
            
            foreach (var token in tokens)
            {
                if (frequency.ContainsKey(token))
                {
                    frequency[token]++;
                }
                else
                {
                    frequency[token] = 1;
                }
            }
            
            return frequency;
        }
        catch (Exception ex)
        {
            throw new TokenizerException("分词并统计词频失败", ex);
        }
    }
}

/// <summary>
/// 分词异常
/// </summary>
public class TokenizerException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public TokenizerException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public TokenizerException(string message, Exception innerException) : base(message, innerException)
    {}
}