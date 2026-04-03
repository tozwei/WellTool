using System.Text.RegularExpressions;

namespace WellTool.Core.Text.Finder;

/// <summary>
/// 正则查找器，通过传入正则表达式，查找指定字符串中匹配正则的开始和结束位置
/// </summary>
public class PatternFinder : TextFinder
{
    private readonly Regex _pattern;
    private Match _matcher;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="regex">被查找的正则表达式</param>
    /// <param name="caseInsensitive">是否忽略大小写</param>
    public PatternFinder(string regex, bool caseInsensitive)
    {
        var options = caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None;
        _pattern = new Regex(regex, options);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="pattern">被查找的正则</param>
    public PatternFinder(Regex pattern)
    {
        _pattern = pattern;
    }

    public override TextFinder SetText(string text)
    {
        _matcher = _pattern.Match(text);
        return base.SetText(text);
    }

    public override TextFinder SetNegative(bool negative)
    {
        throw new NotSupportedException("Negative is invalid for Pattern!");
    }

    public override int Start(int from)
    {
        if (_matcher.Success)
        {
            int end = _matcher.Index + _matcher.Length;
            // 只有匹配到的字符串结尾在limit范围内，才算找到
            if (end <= GetValidEndIndex())
            {
                int start = _matcher.Index;
                if (start == end)
                {
                    // 如果匹配空串，按照未匹配对待，避免死循环
                    return INDEX_NOT_FOUND;
                }
                return start;
            }
        }
        return INDEX_NOT_FOUND;
    }

    public override int End(int start)
    {
        int end = _matcher.Index + _matcher.Length;
        int limit;
        if (EndIndex < 0)
        {
            limit = Text.Length;
        }
        else
        {
            limit = Math.Min(EndIndex, Text.Length);
        }
        return end <= limit ? end : INDEX_NOT_FOUND;
    }

    public PatternFinder Reset()
    {
        _matcher = _matcher.NextMatch();
        return this;
    }
}
