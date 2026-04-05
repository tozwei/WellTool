using System.Text;
using WellTool.Core.Util;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// 形如&amp;#39;的反转义器
/// </summary>
public class NumericEntityUnescaper : StrReplacer
{
    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="textBuilder">文本构建器</param>
    /// <returns>替换后的文本</returns>
    public override string Replace(string text, StringBuilder textBuilder)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        int pos = 0;
        int len = text.Length;
        
        while (pos < len)
        {
            // 检查以确保以&#开头
            if (text[pos] == '&' && pos < len - 2 && text[pos + 1] == '#')
            {
                int start = pos + 2;
                bool isHex = false;
                char firstChar = text[start];
                if (firstChar == 'x' || firstChar == 'X')
                {
                    start++;
                    isHex = true;
                }

                // 确保&#后还有数字
                if (start < len)
                {
                    int end = start;
                    while (end < len && CharUtil.IsHexChar(text[end]))
                    {
                        end++;
                    }
                    bool isSemiNext = end != len && text[end] == ';';
                    if (isSemiNext)
                    {
                        int entityValue;
                        try
                        {
                            if (isHex)
                            {
                                entityValue = System.Convert.ToInt32(text.Substring(start, end - start), 16);
                            }
                            else
                            {
                                entityValue = int.Parse(text.Substring(start, end - start));
                            }
                            textBuilder.Append((char)entityValue);
                            pos = end + 1;
                            continue;
                        }
                        catch (System.FormatException)
                        {
                            // 解析失败，直接添加原字符
                        }
                    }
                }
            }
            
            textBuilder.Append(text[pos]);
            pos++;
        }
        
        return textBuilder.ToString();
    }
}
