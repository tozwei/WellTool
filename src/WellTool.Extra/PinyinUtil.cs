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

namespace WellTool.Extra;

/// <summary>
/// 拼音工具类
/// </summary>
public class PinyinUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static PinyinUtil Instance { get; } = new PinyinUtil();

    /// <summary>
    /// 中文字符范围
    /// </summary>
    private const int ChineseStart = 0x4E00;
    private const int ChineseEnd = 0x9FFF;

    /// <summary>
    /// 获取中文字符的拼音首字母
    /// </summary>
    /// <param name="c">中文字符</param>
    /// <returns>拼音首字母</returns>
    public char GetFirstLetter(char c)
    {
        if (c < ChineseStart || c > ChineseEnd)
        {
            return c;
        }

        // 简单实现，实际项目中可能需要更复杂的拼音库
        var pinyinTable = new Dictionary<int, char>
        {
            { 0x4E00, 'A' }, { 0x4E8C, 'B' }, { 0x4E09, 'C' }, { 0x56DB, 'D' }, { 0x4E94, 'E' },
            { 0x516D, 'F' }, { 0x4E03, 'G' }, { 0x516B, 'H' }, { 0x4E5D, 'J' }, { 0x5341, 'K' },
            { 0x516C, 'L' }, { 0x4E2D, 'M' }, { 0x56E2, 'N' }, { 0x53E3, 'O' }, { 0x53E4, 'P' },
            { 0x5730, 'Q' }, { 0x5934, 'R' }, { 0x53F8, 'S' }, { 0x58EB, 'T' }, { 0x5317, 'W' },
            { 0x56FD, 'X' }, { 0x4E1C, 'Y' }, { 0x5357, 'Z' }
        };

        foreach (var item in pinyinTable)
        {
            if (c >= item.Key)
            {
                return item.Value;
            }
        }

        return 'A';
    }

    /// <summary>
    /// 获取字符串的拼音首字母
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>拼音首字母</returns>
    public string GetFirstLetters(string str)
    {
        var result = new System.Text.StringBuilder();
        foreach (var c in str)
        {
            result.Append(GetFirstLetter(c));
        }
        return result.ToString();
    }

    /// <summary>
    /// 将中文字符串转换为拼音
    /// </summary>
    /// <param name="str">中文字符串</param>
    /// <returns>拼音字符串</returns>
    public string ToPinyin(string str)
    {
        // 简单实现，实际项目中可能需要更复杂的拼音库
        var result = new System.Text.StringBuilder();
        foreach (var c in str)
        {
            if (c >= ChineseStart && c <= ChineseEnd)
            {
                result.Append(GetFirstLetter(c));
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }
}

/// <summary>
/// 拼音异常
/// </summary>
public class PinyinException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public PinyinException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public PinyinException(string message, Exception innerException) : base(message, innerException)
    {}
}