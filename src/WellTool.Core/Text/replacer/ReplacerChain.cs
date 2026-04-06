using System.Collections.Generic;
using WellTool.Core.Text;

namespace WellTool.Core.Text.Replacer;

/// <summary>
/// 字符串替换链，用于组合多个字符串替换逻辑
/// </summary>
public class ReplacerChain : StrReplacer
{
    private readonly List<StrReplacer> _replacers = new List<StrReplacer>();

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="strReplacers">字符串替换器</param>
    public ReplacerChain(params StrReplacer[] strReplacers)
    {
        foreach (var strReplacer in strReplacers)
        {
            AddChain(strReplacer);
        }
    }

    /// <summary>
    /// 添加替换器到链中
    /// </summary>
    /// <param name="element">字符串替换器</param>
    /// <returns>this</returns>
    public ReplacerChain AddChain(StrReplacer element)
    {
        _replacers.Add(element);
        return this;
    }

    /// <summary>
    /// 获取所有替换器
    /// </summary>
    public IEnumerable<StrReplacer> Replacers => _replacers;

    public override int Replace(string str, int pos, StrBuilder outBuilder)
    {
        int consumed = 0;
        foreach (var strReplacer in _replacers)
        {
            consumed = strReplacer.Replace(str, pos, outBuilder);
            if (consumed != 0)
            {
                return consumed;
            }
        }

        return 0;
    }
}
