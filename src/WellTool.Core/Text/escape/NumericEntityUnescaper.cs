using WellTool.Core.Text;
using WellTool.Core.Util;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// 形如&amp;#39;的反转义器
/// </summary>
public class NumericEntityUnescaper : StrReplacer
{
    protected override int Replace(string str, int pos, StrBuilder outBuilder)
    {
        var len = str.Length;
        // 检查以确保以&#开头
        if (str[pos] == '&' && pos < len - 2 && str[pos + 1] == '#')
        {
            int start = pos + 2;
            bool isHex = false;
            char firstChar = str[start];
            if (firstChar == 'x' || firstChar == 'X')
            {
                start++;
                isHex = true;
            }

            // 确保&#后还有数字
            if (start == len)
            {
                return 0;
            }

            int end = start;
            while (end < len && CharUtil.IsHexChar(str[end]))
            {
                end++;
            }
            bool isSemiNext = end != len && str[end] == ';';
            if (isSemiNext)
            {
                int entityValue;
                try
                {
                    if (isHex)
                    {
                        entityValue = Convert.ToInt32(str.Substring(start, end - start), 16);
                    }
                    else
                    {
                        entityValue = int.Parse(str.Substring(start, end - start));
                    }
                }
                catch (FormatException)
                {
                    return 0;
                }
                outBuilder.Append((char)entityValue);
                return 2 + (end - start) + (isHex ? 1 : 0) + 1;
            }
        }
        return 0;
    }
}
