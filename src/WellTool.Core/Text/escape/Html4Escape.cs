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

namespace WellTool.Core.Text.Escape;

/// <summary>
/// HTML4转义处理
/// </summary>
public class Html4Escape : ReplacerChain
{
    private static readonly string[][] BASIC_ESCAPE = {
        new[] { "&", "&amp;" },
        new[] { "<", "&lt;" },
        new[] { ">", "&gt;" }
    };

    private static readonly string[][] ISO8859_1_ESCAPE = {
        new[] { "\u00C0", "&Agrave;" }, new[] { "\u00E0", "&agrave;" },
        new[] { "\u00C1", "&Aacute;" }, new[] { "\u00E1", "&aacute;" },
        new[] { "\u00C2", "&Acirc;" }, new[] { "\u00E2", "&acirc;" },
        new[] { "\u00C3", "&Atilde;" }, new[] { "\u00E3", "&atilde;" },
        new[] { "\u00C4", "&Auml;" }, new[] { "\u00E4", "&auml;" },
        new[] { "\u00C5", "&Aring;" }, new[] { "\u00E5", "&aring;" },
        new[] { "\u00C6", "&AElig;" }, new[] { "\u00E6", "&aelig;" },
        new[] { "\u00C7", "&Ccedil;" }, new[] { "\u00E7", "&ccedil;" },
        new[] { "\u00C8", "&Egrave;" }, new[] { "\u00E8", "&egrave;" },
        new[] { "\u00C9", "&Eacute;" }, new[] { "\u00E9", "&eacute;" },
        new[] { "\u00CA", "&Ecirc;" }, new[] { "\u00EA", "&ecirc;" },
        new[] { "\u00CB", "&Euml;" }, new[] { "\u00EB", "&euml;" },
        new[] { "\u00CC", "&Igrave;" }, new[] { "\u00EC", "&igrave;" },
        new[] { "\u00CD", "&Iacute;" }, new[] { "\u00ED", "&iacute;" },
        new[] { "\u00CE", "&Icirc;" }, new[] { "\u00EE", "&icirc;" },
        new[] { "\u00CF", "&Iuml;" }, new[] { "\u00EF", "&iuml;" },
        new[] { "\u00D0", "&ETH;" }, new[] { "\u00F0", "&eth;" },
        new[] { "\u00D1", "&Ntilde;" }, new[] { "\u00F1", "&ntilde;" },
        new[] { "\u00D2", "&Ograve;" }, new[] { "\u00F2", "&ograve;" },
        new[] { "\u00D3", "&Oacute;" }, new[] { "\u00F3", "&oacute;" },
        new[] { "\u00D4", "&Ocirc;" }, new[] { "\u00F4", "&ocirc;" },
        new[] { "\u00D5", "&Otilde;" }, new[] { "\u00F5", "&otilde;" },
        new[] { "\u00D6", "&Ouml;" }, new[] { "\u00F6", "&ouml;" },
        new[] { "\u00D8", "&Oslash;" }, new[] { "\u00F8", "&oslash;" },
        new[] { "\u00D9", "&Ugrave;" }, new[] { "\u00F9", "&ugrave;" },
        new[] { "\u00DA", "&Uacute;" }, new[] { "\u00FA", "&uacute;" },
        new[] { "\u00DB", "&Ucirc;" }, new[] { "\u00FB", "&ucirc;" },
        new[] { "\u00DC", "&Uuml;" }, new[] { "\u00FC", "&uuml;" },
        new[] { "\u00DD", "&Yacute;" }, new[] { "\u00FD", "&yacute;" },
        new[] { "\u00DE", "&THORN;" }, new[] { "\u00FE", "&thorn;" },
        new[] { "\u00DF", "&szlig;" }, new[] { "\u00FF", "&yuml;" }
    };

    private static readonly string[][] HTML40_EXTENDED_ESCAPE = {
        new[] { "\u0192", "&fnof;" }, new[] { "\u0391", "&Alpha;" }, new[] { "\u03B1", "&alpha;" },
        new[] { "\u0392", "&Beta;" }, new[] { "\u03B2", "&beta;" }, new[] { "\u0393", "&Gamma;" },
        new[] { "\u03B3", "&gamma;" }, new[] { "\u0394", "&Delta;" }, new[] { "\u03B4", "&delta;" },
        new[] { "\u0395", "&Epsilon;" }, new[] { "\u03B5", "&epsilon;" }, new[] { "\u0396", "&Zeta;" },
        new[] { "\u03B6", "&zeta;" }, new[] { "\u0397", "&Eta;" }, new[] { "\u03B7", "&eta;" },
        new[] { "\u0398", "&Theta;" }, new[] { "\u03B8", "&theta;" }, new[] { "\u0399", "&Iota;" },
        new[] { "\u03B9", "&iota;" }, new[] { "\u039A", "&Kappa;" }, new[] { "\u03BA", "&kappa;" },
        new[] { "\u039B", "&Lambda;" }, new[] { "\u03BB", "&lambda;" }, new[] { "\u039C", "&Mu;" },
        new[] { "\u03BC", "&mu;" }, new[] { "\u039D", "&Nu;" }, new[] { "\u03BD", "&nu;" },
        new[] { "\u039E", "&Xi;" }, new[] { "\u03BE", "&xi;" }, new[] { "\u039F", "&Omicron;" },
        new[] { "\u03BF", "&omicron;" }, new[] { "\u03A0", "&Pi;" }, new[] { "\u03C0", "&pi;" },
        new[] { "\u03A1", "&Rho;" }, new[] { "\u03C1", "&rho;" }, new[] { "\u03A3", "&Sigma;" },
        new[] { "\u03C3", "&sigma;" }, new[] { "\u03A4", "&Tau;" }, new[] { "\u03C4", "&tau;" },
        new[] { "\u03A5", "&Upsilon;" }, new[] { "\u03C5", "&upsilon;" }, new[] { "\u03A6", "&Phi;" },
        new[] { "\u03C6", "&phi;" }, new[] { "\u03A7", "&Chi;" }, new[] { "\u03C7", "&chi;" },
        new[] { "\u03A8", "&Psi;" }, new[] { "\u03C8", "&psi;" }, new[] { "\u03A9", "&Omega;" },
        new[] { "\u03C9", "&omega;" }, new[] { "\u03B5", "&infin;" }, new[] { "\u03C2", "&sigmaf;" },
        new[] { "\u03D0", "&exist;" }, new[] { "\u03D1", "&ni;" }, new[] { "\u03D2", "&equiv;" },
        new[] { "\u03D5", "&le;" }, new[] { "\u03D6", "&ge;" }, new[] { "\u2022", "&bull;" },
        new[] { "\u2026", "&hellip;" }, new[] { "\u2032", "&prime;" }, new[] { "\u2033", "&Prime;" },
        new[] { "\u203E", "&oline;" }, new[] { "\u2044", "&frasl;" }, new[] { "\u200E", "&lrm;" },
        new[] { "\u200F", "&rlm;" }, new[] { "\u2013", "&ndash;" }, new[] { "\u2014", "&mdash;" },
        new[] { "\u2018", "&lsquo;" }, new[] { "\u2019", "&rsquo;" }, new[] { "\u201C", "&ldquo;" },
        new[] { "\u201D", "&rdquo;" }, new[] { "\u2022", "&bull;" }, new[] { "\u20AC", "&euro;" },
        new[] { "\u2122", "&trade;" }, new[] { "\u2030", "&permil;" }, new[] { "\u2265", "&ge;" },
        new[] { "\u2264", "&le;" }, new[] { "\u221E", "&infin;" }
    };

    /// <summary>
    /// 构造
    /// </summary>
    public Html4Escape()
    {
        AddReplace(new LookupReplacer(BASIC_ESCAPE));
        AddReplace(new LookupReplacer(ISO8859_1_ESCAPE));
        AddReplace(new LookupReplacer(HTML40_EXTENDED_ESCAPE));
    }
}

/// <summary>
/// 查找替换器
/// </summary>
public class LookupReplacer : StrReplacer
{
    private readonly Dictionary<string, string> _replaceMap;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="replaceMap">替换映射</param>
    public LookupReplacer(string[][] replaceMap)
    {
        _replaceMap = new Dictionary<string, string>();
        foreach (var pair in replaceMap)
        {
            _replaceMap[pair[0]] = pair[1];
        }
    }

    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="textBuilder">文本构建器</param>
    /// <returns>替换后的文本</returns>
    public override string Replace(string text, StringBuilder textBuilder)
    {
        if (_replaceMap.TryGetValue(text, out var replacement))
        {
            return replacement;
        }
        return text;
    }
}

/// <summary>
/// 文本替换器基类
/// </summary>
public abstract class StrReplacer
{
    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <param name="textBuilder">文本构建器</param>
    /// <returns>替换后的文本</returns>
    public virtual string Replace(string text, StringBuilder textBuilder)
    {
        return text;
    }
}

/// <summary>
/// 替换器链
/// </summary>
public class ReplacerChain
{
    private readonly List<StrReplacer> _replacers = new();

    /// <summary>
    /// 添加替换器
    /// </summary>
    /// <param name="replacer">替换器</param>
    public void AddReplace(StrReplacer replacer)
    {
        _replacers.Add(replacer);
    }

    /// <summary>
    /// 替换文本
    /// </summary>
    /// <param name="text">文本</param>
    /// <returns>替换后的文本</returns>
    public virtual string Replace(string text)
    {
        var result = text;
        foreach (var replacer in _replacers)
        {
            result = replacer.Replace(result, new StringBuilder());
        }
        return result;
    }
}