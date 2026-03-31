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
/// 表情符号工具类
/// </summary>
public class EmojiUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static EmojiUtil Instance { get; } = new EmojiUtil();

    /// <summary>
    /// 表情符号正则表达式
    /// </summary>
    private static readonly Regex EmojiRegex = new Regex(
        @"[\uD83C-\uDBFF\uDC00-\uDFFF]+|[\u2600-\u27BF]+|[\u1F600-\u1F64F]+|[\u1F300-\u1F5FF]+|[\u1F680-\u1F6FF]+|[\u1F700-\u1F77F]+|[\u1F780-\u1F7FF]+|[\u1F800-\u1F8FF]+|[\u1F900-\u1F9FF]+|[\u1FA00-\u1FA6F]+|[\u1FA70-\u1FAFF]+|[\u2000-\u206F]+|[\u2100-\u214F]+|[\u2150-\u218F]+|[\u2C00-\u2C5F]+|[\u2C60-\u2C7F]+|[\uA700-\uA7FF]+",
        RegexOptions.Compiled
    );

    /// <summary>
    /// 检查字符串是否包含表情符号
    /// </summary>
    /// <param name="str">要检查的字符串</param>
    /// <returns>是否包含表情符号</returns>
    public bool ContainsEmoji(string str)
    {
        return EmojiRegex.IsMatch(str);
    }

    /// <summary>
    /// 移除字符串中的表情符号
    /// </summary>
    /// <param name="str">要处理的字符串</param>
    /// <returns>移除表情符号后的字符串</returns>
    public string RemoveEmoji(string str)
    {
        return EmojiRegex.Replace(str, string.Empty);
    }

    /// <summary>
    /// 提取字符串中的表情符号
    /// </summary>
    /// <param name="str">要处理的字符串</param>
    /// <returns>提取的表情符号列表</returns>
    public List<string> ExtractEmoji(string str)
    {
        var matches = EmojiRegex.Matches(str);
        var result = new List<string>();
        foreach (Match match in matches)
        {
            result.Add(match.Value);
        }
        return result;
    }
}