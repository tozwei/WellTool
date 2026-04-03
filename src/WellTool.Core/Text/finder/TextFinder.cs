// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace WellTool.Core.Text.Finder;

/// <summary>
/// 文本查找器接口
/// </summary>
public interface TextFinder
{
    /// <summary>
    /// 查找目标字符串的起始位置
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="fromIndex">起始位置</param>
    /// <returns>起始位置，未找到返回-1</returns>
    int Find(string text, int fromIndex);
}

/// <summary>
/// 字符查找器
/// </summary>
public class CharFinder : TextFinder
{
    private readonly char _char;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="c">要查找的字符</param>
    public CharFinder(char c)
    {
        _char = c;
    }

    /// <summary>
    /// 查找目标字符串的起始位置
    /// </summary>
    public int Find(string text, int fromIndex)
    {
        return text.IndexOf(_char, fromIndex);
    }
}

/// <summary>
/// 字符串查找器
/// </summary>
public class StrFinder : TextFinder
{
    private readonly string _str;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="str">要查找的字符串</param>
    public StrFinder(string str)
    {
        _str = str;
    }

    /// <summary>
    /// 查找目标字符串的起始位置
    /// </summary>
    public int Find(string text, int fromIndex)
    {
        return text.IndexOf(_str, fromIndex, StringComparison.Ordinal);
    }
}

/// <summary>
/// 正则查找器
/// </summary>
public class PatternFinder : TextFinder
{
    private readonly System.Text.RegularExpressions.Regex _pattern;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="pattern">正则表达式</param>
    public PatternFinder(string pattern)
    {
        _pattern = new System.Text.RegularExpressions.Regex(pattern);
    }

    /// <summary>
    /// 查找目标字符串的起始位置
    /// </summary>
    public int Find(string text, int fromIndex)
    {
        var match = _pattern.Match(text, fromIndex);
        return match.Success ? match.Index : -1;
    }
}

/// <summary>
/// 长度查找器
/// 查找从起始位置开始，指定长度的位置
/// </summary>
public class LengthFinder : TextFinder
{
    private readonly int _length;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="length">长度</param>
    public LengthFinder(int length)
    {
        _length = length;
    }

    /// <summary>
    /// 查找目标字符串的起始位置
    /// </summary>
    public int Find(string text, int fromIndex)
    {
        var index = fromIndex + _length;
        return index <= text.Length ? index : -1;
    }
}