using System.Text;
using WellTool.Core.Util;
using WellTool.Core.Text.Replacer;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// 形如&amp;#39;的反转义器
/// </summary>
public class NumericEntityUnescaper : StrReplacer
{
    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="pos">位置</param>
    /// <param name="outBuilder">输出构建器</param>
    /// <returns>替换的字符数</returns>
    public override int Replace(string str, int pos, StrBuilder outBuilder)
    {
        int len = str.Length;
        
        // 检查以确保以&#开头
        if (str[pos] == '&' && pos < len - 2 && str[pos + 1] == '#')
        {
            int start = pos + 2;
            bool isHex = false;
            if (start < len)
            {
                char firstChar = str[start];
                if (firstChar == 'x' || firstChar == 'X')
                {
                    start++;
                    isHex = true;
                }

                // 确保&#后还有数字
                if (start < len)
                {
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
                                entityValue = System.Convert.ToInt32(str.Substring(start, end - start), 16);
                            }
                            else
                            {
                                entityValue = int.Parse(str.Substring(start, end - start));
                            }
                            outBuilder.Append((char)entityValue);
                            return end + 1 - pos;
                        }
                        catch (System.FormatException)
                        {
                            // 解析失败，直接返回0
                            return 0;
                        }
                    }
                }
            }
        }
        
        return 0;
    }

    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>替换后的文本</returns>
    public new string Replace(string text)
    {
        return base.Replace(text);
    }
}
