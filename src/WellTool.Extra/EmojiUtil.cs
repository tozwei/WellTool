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

using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Extra;

/// <summary>
/// 表情符号工具类
/// </summary>
public class EmojiUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static EmojiUtil Instance { get; } = new EmojiUtil();

    // Emoji Unicode 范围：
    // Surrogate pairs 用于 Emoji (4 字节字符)
    // 范围: D83C-D83D (补充图形符号) + D83E (表情符号扩展) + DC00-DFFF (低代理)
    
    /// <summary>
    /// 检查字符串是否包含表情符号
    /// </summary>
    /// <param name="str">要检查的字符串</param>
    /// <returns>是否包含表情符号</returns>
    public bool ContainsEmoji(string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;
            
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            
            // 检查是否是 surrogate pair
            if (char.IsSurrogate(c))
            {
                if (i + 1 < str.Length && char.IsSurrogatePair(c, str[i + 1]))
                {
                    // 这是一个 surrogate pair，检查是否是 Emoji 范围
                    int codePoint = char.ConvertToUtf32(c, str[i + 1]);
                    if (IsEmojiCodePoint(codePoint))
                    {
                        return true;
                    }
                    i++; // 跳过下一个字符
                }
            }
            else
            {
                // 检查单字符 Emoji
                if (IsEmojiChar(c))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 移除字符串中的表情符号
    /// </summary>
    /// <param name="str">要处理的字符串</param>
    /// <returns>移除表情符号后的字符串</returns>
    public string RemoveEmoji(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
            
        var sb = new StringBuilder(str.Length);
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            
            if (char.IsSurrogate(c))
            {
                if (i + 1 < str.Length && char.IsSurrogatePair(c, str[i + 1]))
                {
                    int codePoint = char.ConvertToUtf32(c, str[i + 1]);
                    if (!IsEmojiCodePoint(codePoint))
                    {
                        sb.Append(c);
                        sb.Append(str[i + 1]);
                    }
                    i++; // 跳过下一个字符
                }
                else
                {
                    sb.Append(c);
                }
            }
            else if (!IsEmojiChar(c))
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 提取字符串中的表情符号
    /// </summary>
    /// <param name="str">要处理的字符串</param>
    /// <returns>提取的表情符号列表</returns>
    public List<string> ExtractEmoji(string str)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(str))
            return result;
            
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            
            if (char.IsSurrogate(c))
            {
                if (i + 1 < str.Length && char.IsSurrogatePair(c, str[i + 1]))
                {
                    int codePoint = char.ConvertToUtf32(c, str[i + 1]);
                    if (IsEmojiCodePoint(codePoint))
                    {
                        result.Add(new string(new[] { c, str[i + 1] }));
                    }
                    i++; // 跳过下一个字符
                }
            }
            else if (IsEmojiChar(c))
            {
                result.Add(c.ToString());
            }
        }
        return result;
    }
    
    /// <summary>
    /// 判断 Unicode 码点是否是 Emoji
    /// </summary>
    private bool IsEmojiCodePoint(int codePoint)
    {
        // Emoji 主要范围
        // 1F600-1F64F: Emoticons
        if (codePoint >= 0x1F600 && codePoint <= 0x1F64F)
            return true;
        // 1F300-1F5FF: Miscellaneous Symbols and Pictographs
        if (codePoint >= 0x1F300 && codePoint <= 0x1F5FF)
            return true;
        // 1F680-1F6FF: Transport and Map Symbols
        if (codePoint >= 0x1F680 && codePoint <= 0x1F6FF)
            return true;
        // 1F700-1F77F: Alchemical Symbols
        if (codePoint >= 0x1F700 && codePoint <= 0x1F77F)
            return true;
        // 1F780-1F9FF: Geometric Shapes Extended + Supplemental Symbols
        if (codePoint >= 0x1F780 && codePoint <= 0x1F9FF)
            return true;
        // 1FA00-1FA6F: Chess Symbols, Symbols and Pictographs Extended-A
        if (codePoint >= 0x1FA00 && codePoint <= 0x1FA6F)
            return true;
        // 1FA70-1FAFF: Symbols and Pictographs Extended-A
        if (codePoint >= 0x1FA70 && codePoint <= 0x1FAFF)
            return true;
        // 1F1E6-1F1FF: Regional Indicator Symbols (flag emojis)
        if (codePoint >= 0x1F1E6 && codePoint <= 0x1F1FF)
            return true;
            
        return false;
    }
    
    /// <summary>
    /// 判断单个字符是否是 Emoji (不包括 surrogate pairs)
    /// </summary>
    private bool IsEmojiChar(char c)
    {
        // 杂项符号和星星等: 2600-27BF
        if (c >= 0x2600 && c <= 0x27BF)
            return true;
        // 其他扩展符号
        if (c >= 0x1F300 && c <= 0x1F9FF) // 这个范围主要是 surrogate pairs，但有些字符可能在这里
            return true;
        if (c >= 0x1FA00 && c <= 0x1FAFF)
            return true;
        return false;
    }
}
