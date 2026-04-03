namespace WellTool.Core.Text.Finder;

/// <summary>
/// 字符匹配查找器，查找满足指定匹配器匹配的字符所在位置，此类长用于查找某一类字符，如数字等
/// </summary>
public class CharMatcherFinder : TextFinder
{
    private readonly WellTool.Core.Lang.Matcher.Matcher<char> _matcher;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="matcher">被查找的字符匹配器</param>
    public CharMatcherFinder(WellTool.Core.Lang.Matcher.Matcher<char> matcher)
    {
        _matcher = matcher;
    }

    public override int Start(int from)
    {
        Assert.NotNull(Text, "Text to find must be not null!");
        int limit = GetValidEndIndex();
        if (Negative)
        {
            for (int i = from; i > limit; i--)
            {
                if (_matcher.Match(Text[i]))
                {
                    return i;
                }
            }
        }
        else
        {
            for (int i = from; i < limit; i++)
            {
                if (_matcher.Match(Text[i]))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public override int End(int start)
    {
        if (start < 0)
        {
            return -1;
        }
        return start + 1;
    }
}
