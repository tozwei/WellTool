using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WellTool.Http;

/// <summary>
/// HTML 过滤器，用于去除 XSS (Cross Site Scripting) 漏洞隐患
/// </summary>
public sealed class HTMLFilter
{
    private static readonly Pattern P_COMMENTS = new Pattern("<!--(.*?)-->", RegexOptions.Singleline);
    private static readonly Pattern P_TAGS = new Pattern("<(.*?)>", RegexOptions.Singleline);
    private static readonly Pattern P_END_TAG = new Pattern("^/([a-z0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Pattern P_START_TAG = new Pattern("^([a-z0-9]+)(.*?)(/?)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Pattern P_QUOTED_ATTRIBUTES = new Pattern("([a-z0-9]+)=([\"'])(.*?)\\2", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Pattern P_UNQUOTED_ATTRIBUTES = new Pattern("([a-z0-9]+)(=)([^\"\\s']+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Pattern P_PROTOCOL = new Pattern("^([^:]+):", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Pattern P_ENTITY = new Pattern("&#(\\d+);?");
    private static readonly Pattern P_ENTITY_UNICODE = new Pattern("&#x([0-9a-f]+);?");
    private static readonly Pattern P_ENCODE = new Pattern("%([0-9a-f]{2});?");

    private readonly Dictionary<string, List<string>> vAllowed;
    private readonly string[] vSelfClosingTags;
    private readonly string[] vDisallowed;
    private readonly string[] vProtocolAtts;
    private readonly string[] vAllowedProtocols;
    private readonly string[] vRemoveBlanks;
    private readonly bool vAlwaysAddQuotes;

    public HTMLFilter()
    {
        vAllowed = new Dictionary<string, List<string>>();

        // 默认允许的标签和属性
        var a = new List<string>();
        a.AddRange(new[] { "href", "target" });
        vAllowed["a"] = a;

        var img = new List<string>();
        img.AddRange(new[] { "src", "width", "height", "alt" });
        vAllowed["img"] = img;

        var div = new List<string>();
        div.Add("align");
        vAllowed["div"] = div;

        var table = new List<string>();
        table.AddRange(new[] { "border", "cellpadding", "cellspacing" });
        vAllowed["table"] = table;

        var td = new List<string>();
        td.AddRange(new[] { "border", "colspan", "valign" });
        vAllowed["td"] = td;

        var tr = new List<string>();
        tr.AddRange(new[] { "border", "bgcolor" });
        vAllowed["tr"] = tr;

        var tbody = new List<string>();
        tbody.Add("align");
        vAllowed["tbody"] = tbody;

        var thead = new List<string>();
        thead.Add("align");
        vAllowed["thead"] = thead;

        var tfoot = new List<string>();
        tfoot.Add("align");
        vAllowed["tfoot"] = tfoot;

        var p = new List<string>();
        p.Add("align");
        vAllowed["p"] = p;

        var h = new List<string>();
        h.Add("align");
        vAllowed["h1"] = h;
        vAllowed["h2"] = h;
        vAllowed["h3"] = h;
        vAllowed["h4"] = h;
        vAllowed["h5"] = h;
        vAllowed["h6"] = h;

        var q = new List<string>();
        q.AddRange(new[] { "cite" });
        vAllowed["q"] = q;

        var br = new List<string>();
        vAllowed["br"] = br;

        var ol = new List<string>();
        ol.AddRange(new[] { "start", "type" });
        vAllowed["ol"] = ol;

        var ul = new List<string>();
        ul.Add("type");
        vAllowed["ul"] = ul;

        var li = new List<string>();
        vAllowed["li"] = li;

        var dl = new List<string>();
        vAllowed["dl"] = dl;

        var dt = new List<string>();
        vAllowed["dt"] = dt;

        var dd = new List<string>();
        vAllowed["dd"] = dd;

        var blockquote = new List<string>();
        blockquote.Add("cite");
        vAllowed["blockquote"] = blockquote;

        var cite = new List<string>();
        vAllowed["cite"] = cite;

        var i = new List<string>();
        vAllowed["i"] = i;

        var b = new List<string>();
        vAllowed["b"] = b;

        var u = new List<string>();
        vAllowed["u"] = u;

        var strong = new List<string>();
        vAllowed["strong"] = strong;

        var span = new List<string>();
        vAllowed["span"] = span;

        var font = new List<string>();
        font.AddRange(new[] { "color", "size", "face" });
        vAllowed["font"] = font;

        var small = new List<string>();
        vAllowed["small"] = small;

        var big = new List<string>();
        vAllowed["big"] = big;

        var em = new List<string>();
        vAllowed["em"] = em;

        var sub = new List<string>();
        vAllowed["sub"] = sub;

        var sup = new List<string>();
        vAllowed["sup"] = sup;

        var strike = new List<string>();
        vAllowed["strike"] = strike;

        var s = new List<string>();
        vAllowed["s"] = s;

        var pre = new List<string>();
        vAllowed["pre"] = pre;

        var code = new List<string>();
        vAllowed["code"] = code;

        var tt = new List<string>();
        vAllowed["tt"] = tt;

        var abbr = new List<string>();
        vAllowed["abbr"] = abbr;

        var acronym = new List<string>();
        vAllowed["acronym"] = acronym;

        var address = new List<string>();
        vAllowed["address"] = address;

        var var = new List<string>();
        vAllowed["var"] = var;

        vSelfClosingTags = new[] { "br" };
        vDisallowed = new string[0];
        vProtocolAtts = new[] { "src", "href" };
        vAllowedProtocols = new[] { "http", "https", "mailto", "ftp" };
        vRemoveBlanks = new[] { "a", "div", "table", "tr", "td", "th", "tbody", "thead", "tfoot", "p", "blockquote", "pre", "code" };
        vAlwaysAddQuotes = false;
    }

    /// <summary>
    /// 过滤 HTML 内容
    /// </summary>
    /// <param name="input">输入的 HTML</param>
    /// <returns>过滤后的 HTML</returns>
    public string Filter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // 移除注释
        var output = P_COMMENTS.ReplaceAll(input, "");

        // 处理标签
        output = process(output);

        return output;
    }

    private string process(string input)
    {
        var result = "";
        var pos = 0;
        var matcher = P_TAGS.Match(input);

        while (matcher.Success)
        {
            var match = matcher.Match;
            if (match.Index > pos)
            {
                result += input.Substring(pos, match.Index - pos);
            }

            var tag = match.Value.ReplaceFirst("<", "").ReplaceFirst(">", "");
            result += processTag(tag);

            pos = match.Index + match.Length;
            matcher = matcher.NextMatch();
        }

        if (pos < input.Length)
        {
            result += input.Substring(pos);
        }

        return result;
    }

    private string processTag(string tag)
    {
        // 处理结束标签
        var endTagMatcher = P_END_TAG.Match(tag);
        if (endTagMatcher.Success)
        {
            var tagName = endTagMatcher.Groups[1].Value.ToLower();
            if (vAllowed.ContainsKey(tagName))
            {
                return $"</{tagName}>";
            }
            return "";
        }

        // 处理开始标签
        var startTagMatcher = P_START_TAG.Match(tag);
        if (startTagMatcher.Success)
        {
            var tagName = startTagMatcher.Groups[1].Value.ToLower();
            var attributes = startTagMatcher.Groups[2].Value;
            var selfClosing = startTagMatcher.Groups[3].Value;

            if (!vAllowed.ContainsKey(tagName))
            {
                return "";
            }

            var processedAttrs = processAttributes(tagName, attributes);

            if (Array.IndexOf(vSelfClosingTags, tagName) >= 0 || !string.IsNullOrEmpty(selfClosing))
            {
                return $"<{tagName}{processedAttrs} />";
            }

            return $"<{tagName}{processedAttrs}>";
        }

        return "";
    }

    private string processAttributes(string tagName, string attributes)
    {
        var result = "";

        // 处理引号内的属性
        var quotedMatcher = P_QUOTED_ATTRIBUTES.Match(attributes);
        while (quotedMatcher.Success)
        {
            var attrName = quotedMatcher.Groups[1].Value.ToLower();
            var quote = quotedMatcher.Groups[2].Value;
            var attrValue = quotedMatcher.Groups[3].Value;

            if (vAllowed[tagName].Contains(attrName))
            {
                if (Array.IndexOf(vProtocolAtts, attrName) >= 0)
                {
                    attrValue = processProtocol(attrValue);
                }

                result += $" {attrName}={quote}{attrValue}{quote}";
            }

            quotedMatcher = quotedMatcher.NextMatch();
        }

        // 处理无引号的属性
        var unquotedMatcher = P_UNQUOTED_ATTRIBUTES.Match(attributes);
        while (unquotedMatcher.Success)
        {
            var attrName = unquotedMatcher.Groups[1].Value.ToLower();
            var attrValue = unquotedMatcher.Groups[3].Value;

            if (vAllowed[tagName].Contains(attrName))
            {
                if (Array.IndexOf(vProtocolAtts, attrName) >= 0)
                {
                    attrValue = processProtocol(attrValue);
                }

                result += $" {attrName}=\"{attrValue}\"";
            }

            unquotedMatcher = unquotedMatcher.NextMatch();
        }

        return result;
    }

    private string processProtocol(string value)
    {
        var protocolMatcher = P_PROTOCOL.Match(value);
        if (protocolMatcher.Success)
        {
            var protocol = protocolMatcher.Groups[1].Value.ToLower();
            if (Array.IndexOf(vAllowedProtocols, protocol) < 0)
            {
                return "invalid://";
            }
        }
        return value;
    }
}

/// <summary>
/// 正则表达式包装类
/// </summary>
internal class Pattern
{
    private readonly Regex _regex;
    private readonly string _pattern;

    public Pattern(string pattern, RegexOptions options = RegexOptions.None)
    {
        _pattern = pattern;
        _regex = new Regex(pattern, options);
    }

    public MatchResult Match(string input)
    {
        var match = _regex.Match(input);
        return new MatchResult(match);
    }

    public string ReplaceAll(string input, string replacement)
    {
        return _regex.Replace(input, replacement);
    }
}

/// <summary>
/// 匹配结果包装类
/// </summary>
internal class MatchResult
{
    private readonly Match _match;

    public MatchResult(Match match)
    {
        _match = match;
    }

    public bool Success => _match.Success;

    public Match Match => _match;

    public GroupCollection Groups => _match.Groups;

    public MatchResult NextMatch()
    {
        return new MatchResult(_match.NextMatch());
    }
}

/// <summary>
/// 字符串扩展方法
/// </summary>
internal static class StringExtensions
{
    public static string ReplaceFirst(this string text, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(oldValue))
        {
            return text;
        }

        var index = text.IndexOf(oldValue);
        if (index < 0)
        {
            return text;
        }

        return text.Substring(0, index) + newValue + text.Substring(index + oldValue.Length);
    }
}
