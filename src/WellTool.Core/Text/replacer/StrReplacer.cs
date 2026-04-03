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

using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Text.Replacer;

/// <summary>
/// 字符串替换器接口
/// </summary>
public interface IStrReplacer
{
    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="textBuilder">文本构建器</param>
    /// <returns>替换后的文本</returns>
    string Replace(string text, StringBuilder textBuilder);
}

/// <summary>
/// 查找替换器
/// </summary>
public class LookupReplacer : IStrReplacer
{
    private readonly Dictionary<string, string> _replaceMap;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="replaceMap">替换映射</param>
    public LookupReplacer(Dictionary<string, string> replaceMap)
    {
        _replaceMap = replaceMap;
    }

    /// <summary>
    /// 替换文本
    /// </summary>
    public virtual string Replace(string text, StringBuilder textBuilder)
    {
        if (_replaceMap.TryGetValue(text, out var replacement))
        {
            return replacement;
        }
        return text;
    }
}

/// <summary>
/// 替换器链
/// </summary>
public class ReplacerChain : IStrReplacer
{
    private readonly List<IStrReplacer> _replacers = new();

    /// <summary>
    /// 添加替换器
    /// </summary>
    /// <param name="replacer">替换器</param>
    public void AddReplace(IStrReplacer replacer)
    {
        _replacers.Add(replacer);
    }

    /// <summary>
    /// 替换文本
    /// </summary>
    public virtual string Replace(string text, StringBuilder textBuilder)
    {
        var result = text;
        foreach (var replacer in _replacers)
        {
            result = replacer.Replace(result, new StringBuilder());
        }
        return result;
    }
}

/// <summary>
/// 字符串替换工具类
/// </summary>
public static class StrReplacerUtil
{
    /// <summary>
    /// 使用替换器链替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="replacers">替换器链</param>
    /// <returns>替换后的文本</returns>
    public static string Replace(string text, params IStrReplacer[] replacers)
    {
        if (replacers == null || replacers.Length == 0)
        {
            return text;
        }

        var result = text;
        foreach (var replacer in replacers)
        {
            result = replacer.Replace(result, new StringBuilder());
        }
        return result;
    }
}