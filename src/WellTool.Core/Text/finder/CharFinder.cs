using WellTool.Core.Util;

namespace WellTool.Core.Text.Finder;

/// <summary>
/// 字符查找器，查找指定字符在字符串中的位置信息
/// </summary>
public class CharFinder : TextFinder
{
    private readonly char _c;
    private readonly bool _caseInsensitive;

    /// <summary>
    /// 构造，不忽略字符大小写
    /// </summary>
    /// <param name="c">被查找的字符</param>
    public CharFinder(char c)
    {
        this(c, false);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="c">被查找的字符</param>
    /// <param name="caseInsensitive">是否忽略大小写</param>
    public CharFinder(char c, bool caseInsensitive)
    {
        _c = c;
        _caseInsensitive = caseInsensitive;
    }

    public override int Start(int from)
    {
        Assert.NotNull(Text, "Text to find must be not null!");
        int limit = GetValidEndIndex();
        if (Negative)
        {
            for (int i = from; i > limit; i--)
            {
                if (NumberUtil.Equals(_c, Text[i], _caseInsensitive))
                {
                    return i;
                }
            }
        }
        else
        {
            for (int i = from; i < limit; i++)
            {
                if (NumberUtil.Equals(_c, Text[i], _caseInsensitive))
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
