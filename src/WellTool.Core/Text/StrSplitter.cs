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
using System.Linq;

namespace WellTool.Core.Text;

/// <summary>
/// 字符串分割器<br>
/// 用于将字符串分割成多个部分
/// </summary>
public class StrSplitter
{
    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] Split(string text, string separator)
    {
        return text.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="options">分割选项</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] Split(string text, string separator, StringSplitOptions options)
    {
        return text.Split(new[] { separator }, options);
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separators">分隔符数组</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] Split(string text, params string[] separators)
    {
        return text.Split(separators, StringSplitOptions.None);
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separators">分隔符数组</param>
    /// <param name="options">分割选项</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] Split(string text, string[] separators, StringSplitOptions options)
    {
        return text.Split(separators, options);
    }

    /// <summary>
    /// 使用正则表达式分割字符串
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="pattern">正则表达式模式</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByRegex(string text, string pattern)
    {
        return Regex.Split(text, pattern);
    }

    /// <summary>
    /// 分割字符串为列表
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <returns>分割后的字符串列表</returns>
    public static List<string> SplitToList(string text, string separator)
    {
        return Split(text, separator).ToList();
    }

    /// <summary>
    /// 分割字符串为列表
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="options">分割选项</param>
    /// <returns>分割后的字符串列表</returns>
    public static List<string> SplitToList(string text, string separator, StringSplitOptions options)
    {
        return Split(text, separator, options).ToList();
    }

    /// <summary>
    /// 分割字符串为列表
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separators">分隔符数组</param>
    /// <returns>分割后的字符串列表</returns>
    public static List<string> SplitToList(string text, params string[] separators)
    {
        return Split(text, separators).ToList();
    }

    /// <summary>
    /// 分割字符串为列表
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separators">分隔符数组</param>
    /// <param name="options">分割选项</param>
    /// <returns>分割后的字符串列表</returns>
    public static List<string> SplitToList(string text, string[] separators, StringSplitOptions options)
    {
        return Split(text, separators, options).ToList();
    }

    /// <summary>
    /// 按字符串分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByString(string text, string separator)
    {
        return Split(text, separator);
    }

    /// <summary>
    /// 按字符串分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="limit">分割限制</param>
    /// <param name="trim">是否修剪每个分割后的字符串</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByString(string text, string separator, int limit, bool trim)
    {
        var result = Split(text, separator);
        if (limit > 0 && limit < result.Length)
        {
            var limitedResult = new string[limit];
            Array.Copy(result, limitedResult, limit - 1);
            limitedResult[limit - 1] = string.Join(separator, result.Skip(limit - 1));
            result = limitedResult;
        }
        if (trim)
        {
            result = result.Select(s => s.Trim()).ToArray();
        }
        return result;
    }

    /// <summary>
    /// 按字符分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByChar(string text, char separator)
    {
        return text.Split(separator);
    }

    /// <summary>
    /// 按字符分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="options">分割选项</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByChar(string text, char separator, StringSplitOptions options)
    {
        return text.Split(new[] { separator }, options);
    }

    /// <summary>
    /// 按字符分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="limit">分割限制</param>
    /// <param name="trim">是否修剪每个分割后的字符串</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByChar(string text, char separator, int limit, bool trim)
    {
        var result = text.Split(separator);
        if (limit > 0 && limit < result.Length)
        {
            var limitedResult = new string[limit];
            Array.Copy(result, limitedResult, limit - 1);
            limitedResult[limit - 1] = string.Join(separator.ToString(), result.Skip(limit - 1));
            result = limitedResult;
        }
        if (trim)
        {
            result = result.Select(s => s.Trim()).ToArray();
        }
        return result;
    }

    /// <summary>
    /// 按字符串分割
    /// </summary>
    /// <param name="text">要分割的文本</param>
    /// <param name="separator">分隔符</param>
    /// <param name="trim">是否修剪每个分割后的字符串</param>
    /// <returns>分割后的字符串数组</returns>
    public static string[] SplitByString(string text, string separator, bool trim)
    {
        var result = Split(text, separator);
        if (trim)
        {
            result = result.Select(s => s.Trim()).ToArray();
        }
        return result;
    }
}
