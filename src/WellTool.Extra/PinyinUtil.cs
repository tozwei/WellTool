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
        var pinyinTable = new (int start, int end, char letter)[]
        {
            (0x4E00, 0x4E8B, 'A'),  // 一
            (0x4E8C, 0x4E09, 'B'),  // 二
            (0x4E09, 0x56DB, 'C'),  // 三
            (0x56DB, 0x4E94, 'D'),  // 四
            (0x4E94, 0x516D, 'E'),  // 五
            (0x516D, 0x4E03, 'F'),  // 六
            (0x4E03, 0x516B, 'G'),  // 七
            (0x516B, 0x4E5D, 'H'),  // 八
            (0x4E5D, 0x5341, 'J'),  // 九
            (0x5341, 0x516C, 'K'),  // 十
            (0x516C, 0x4E2D, 'L'),  // 公
            (0x4E2D, 0x56E2, 'M'),  // 中
            (0x56E2, 0x53E3, 'N'),  // 团
            (0x53E3, 0x53E4, 'O'),  // 口
            (0x53E4, 0x5730, 'P'),  // 古
            (0x5730, 0x5934, 'Q'),  // 地
            (0x5934, 0x53F8, 'R'),  // 头
            (0x53F8, 0x58EB, 'S'),  // 司
            (0x58EB, 0x5317, 'T'),  // 士
            (0x5317, 0x56FD, 'W'),  // 北
            (0x56FD, 0x4E1C, 'X'),  // 国
            (0x4E1C, 0x5357, 'Y'),  // 东
            (0x5357, 0x9FA5, 'Z')   // 南
        };

        foreach (var item in pinyinTable)
        {
            if (c >= item.start && c < item.end)
            {
                return item.letter;
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