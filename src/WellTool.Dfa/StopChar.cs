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
/// 停止字符
/// </summary>
public static class StopChar
{
    /// <summary>
    /// 停止字符集合
    /// </summary>
    private static readonly HashSet<char> _stopChars = new()
    {
        ' ', '\t', '\n', '\r',
        ',', '.', '!', '?',
        ';', ':', '"', '\'',
        '(', ')', '[', ']',
        '{', '}', '<', '>',
        '&', '^', '%', '$',
        '#', '@', '!', '~',
        '`', '-', '_', '=',
        '+', '|', '\\', '/',
        '*', '"', '\'', '，',
        '。', '！', '？', '；',
        '：', '“', '”', '‘',
        '’', '（', '）', '【',
        '】', '｛', '｝', '《',
        '》', '＆', '＾', '％',
        '＄', '＃', '＠', '！',
        '～', '｀', '－', '＿',
        '＝', '＋', '｜', '＼',
        '／', '＊', '＂', '＇'
    };

    /// <summary>
    /// 判断字符是否为停止字符
    /// </summary>
    /// <param name="ch">字符</param>
    /// <returns>是否为停止字符</returns>
    public static bool IsStopChar(char ch)
    {
        return _stopChars.Contains(ch);
    }

    /// <summary>
    /// 添加停止字符
    /// </summary>
    /// <param name="ch">字符</param>
    public static void Add(char ch)
    {
        _stopChars.Add(ch);
    }

    /// <summary>
    /// 移除停止字符
    /// </summary>
    /// <param name="ch">字符</param>
    /// <returns>是否移除成功</returns>
    public static bool Remove(char ch)
    {
        return _stopChars.Remove(ch);
    }
}
