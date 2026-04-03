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
/// XML特殊字符转义
/// &amp; (ampersand) 替换为 &amp;amp;
/// &lt; (less than) 替换为 &amp;lt;
/// &gt; (greater than) 替换为 &amp;gt;
/// " (double quote) 替换为 &amp;quot;
/// ' (single quote / apostrophe) 替换为 &amp;apos;
/// </summary>
public class XmlEscape : ReplacerChain
{
    private static readonly long serialVersionUID = 1L;

    private static readonly string[][] BASIC_ESCAPE = {
        new[] { "'", "&apos;" },
        new[] { "\"", "&quot;" },
        new[] { "&", "&amp;" },
        new[] { "<", "&lt;" },
        new[] { ">", "&gt;" }
    };

    /// <summary>
    /// 构造
    /// </summary>
    public XmlEscape()
    {
        AddReplace(new LookupReplacer(BASIC_ESCAPE));
    }
}

/// <summary>
/// XML反转义
/// </summary>
public class XmlUnescape : ReplacerChain
{
    private static readonly long serialVersionUID = 1L;

    private static readonly string[][] BASIC_UNESCAPE = {
        new[] { "&apos;", "'" },
        new[] { "&quot;", "\"" },
        new[] { "&amp;", "&" },
        new[] { "&lt;", "<" },
        new[] { "&gt;", ">" },
        new[] { "&nbsp;", " " },
        new[] { "&copy;", "©" },
        new[] { "&reg;", "®" },
        new[] { "&trade;", "™" }
    };

    /// <summary>
    /// 构造
    /// </summary>
    public XmlUnescape()
    {
        AddReplace(new LookupReplacer(BASIC_UNESCAPE));
    }
}

/// <summary>
/// HTML4反转义
/// </summary>
public class Html4Unescape : ReplacerChain
{
    private static readonly long serialVersionUID = 1L;

    private static readonly string[][] BASIC_UNESCAPE = {
        new[] { "&amp;", "&" },
        new[] { "&lt;", "<" },
        new[] { "&gt;", ">" },
        new[] { "&quot;", "\"" },
        new[] { "&apos;", "'" },
        new[] { "&nbsp;", " " },
        new[] { "&Agrave;", "À" }, new[] { "&agrave;", "à" },
        new[] { "&Aacute;", "Á" }, new[] { "&aacute;", "á" },
        new[] { "&Acirc;", "Â" }, new[] { "&acirc;", "â" },
        new[] { "&Atilde;", "Ã" }, new[] { "&atilde;", "ã" },
        new[] { "&Auml;", "Ä" }, new[] { "&auml;", "ä" },
        new[] { "&Aring;", "Å" }, new[] { "&aring;", "å" },
        new[] { "&AElig;", "Æ" }, new[] { "&aelig;", "æ" },
        new[] { "&Ccedil;", "Ç" }, new[] { "&ccedil;", "ç" },
        new[] { "&Egrave;", "È" }, new[] { "&egrave;", "è" },
        new[] { "&Eacute;", "É" }, new[] { "&eacute;", "é" },
        new[] { "&Ecirc;", "Ê" }, new[] { "&ecirc;", "ê" },
        new[] { "&Euml;", "Ë" }, new[] { "&euml;", "ë" },
        new[] { "&Igrave;", "Ì" }, new[] { "&igrave;", "ì" },
        new[] { "&Iacute;", "Í" }, new[] { "&iacute;", "í" },
        new[] { "&Icirc;", "Î" }, new[] { "&icirc;", "î" },
        new[] { "&Iuml;", "Ï" }, new[] { "&iuml;", "ï" },
        new[] { "&Ntilde;", "Ñ" }, new[] { "&ntilde;", "ñ" },
        new[] { "&Ograve;", "Ò" }, new[] { "&ograve;", "ò" },
        new[] { "&Oacute;", "Ó" }, new[] { "&oacute;", "ó" },
        new[] { "&Ocirc;", "Ô" }, new[] { "&ocirc;", "ô" },
        new[] { "&Otilde;", "Õ" }, new[] { "&otilde;", "õ" },
        new[] { "&Ouml;", "Ö" }, new[] { "&ouml;", "ö" },
        new[] { "&Oslash;", "Ø" }, new[] { "&oslash;", "ø" },
        new[] { "&Ugrave;", "Ù" }, new[] { "&ugrave;", "ù" },
        new[] { "&Uacute;", "Ú" }, new[] { "&uacute;", "ú" },
        new[] { "&Ucirc;", "Û" }, new[] { "&ucirc;", "û" },
        new[] { "&Uuml;", "Ü" }, new[] { "&uuml;", "ü" },
        new[] { "&Yacute;", "Ý" }, new[] { "&yacute;", "ý" },
        new[] { "&szlig;", "ß" }
    };

    /// <summary>
    /// 构造
    /// </summary>
    public Html4Unescape()
    {
        AddReplace(new LookupReplacer(BASIC_UNESCAPE));
    }
}