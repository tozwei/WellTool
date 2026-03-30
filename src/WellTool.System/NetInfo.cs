using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.System;

/// <summary>
/// .NET Runtime 信息
/// </summary>
public class NetInfo
{
    private static readonly Lazy<NetInfo> _instance = new(() => new NetInfo());
    public static NetInfo Instance => _instance.Value;

    private static readonly Regex _versionRegex = new(@"^[0-9]{1,2}(\.[0-9]{1,2})?", RegexOptions.Compiled);
    private static readonly Regex _versionIntRegex = new(@"^[0-9]{1,2}(\.[0-9]{1,2}){0,2}", RegexOptions.Compiled);

    private readonly string _version;
    private readonly float _versionFloat;
    private readonly int _versionInt;
    private readonly string _vendor;
    private readonly string _vendorUrl;

    private readonly bool _isNet6;
    private readonly bool _isNet7;
    private readonly bool _isNet8;
    private readonly bool _isNet9;

    private NetInfo()
    {
        _version = Environment.Version.ToString();
        _versionFloat = GetNetVersionAsFloat();
        _versionInt = GetNetVersionAsInt();
        _vendor = "Microsoft Corporation";
        _vendorUrl = "https://dotnet.microsoft.com/";

        _isNet6 = GetNetVersionMatches("6");
        _isNet7 = GetNetVersionMatches("7");
        _isNet8 = GetNetVersionMatches("8");
        _isNet9 = GetNetVersionMatches("9");
    }

    /// <summary>
    /// 取得当前.NET impl.的版本
    /// </summary>
    /// <returns>版本号</returns>
    public string Version => _version;

    /// <summary>
    /// 取得当前.NET impl.的版本（float）
    /// </summary>
    /// <returns>版本float值</returns>
    public float VersionFloat => _versionFloat;

    /// <summary>
    /// 取得当前.NET impl.的版本（int）
    /// </summary>
    /// <returns>版本int值</returns>
    public int VersionInt => _versionInt;

    /// <summary>
    /// 取得当前.NET impl.的厂商
    /// </summary>
    /// <returns>厂商</returns>
    public string Vendor => _vendor;

    /// <summary>
    /// 取得当前.NET impl.的厂商网站的URL
    /// </summary>
    /// <returns>厂商URL</returns>
    public string VendorUrl => _vendorUrl;

    /// <summary>
    /// 判断当前.NET的版本是否为6
    /// </summary>
    /// <returns>如果是返回true</returns>
    public bool IsNet6 => _isNet6;

    /// <summary>
    /// 判断当前.NET的版本是否为7
    /// </summary>
    /// <returns>如果是返回true</returns>
    public bool IsNet7 => _isNet7;

    /// <summary>
    /// 判断当前.NET的版本是否为8
    /// </summary>
    /// <returns>如果是返回true</returns>
    public bool IsNet8 => _isNet8;

    /// <summary>
    /// 判断当前.NET的版本是否为9
    /// </summary>
    /// <returns>如果是返回true</returns>
    public bool IsNet9 => _isNet9;

    /// <summary>
    /// 判定当前.NET的版本是否大于等于指定的版本号
    /// </summary>
    /// <param name="requiredVersion">需要的版本</param>
    /// <returns>如果当前.NET版本大于或等于指定的版本，则返回true</returns>
    public bool IsNetVersionAtLeast(float requiredVersion)
    {
        return VersionFloat >= requiredVersion;
    }

    /// <summary>
    /// 判定当前.NET的版本是否大于等于指定的版本号
    /// </summary>
    /// <param name="requiredVersion">需要的版本</param>
    /// <returns>如果当前.NET版本大于或等于指定的版本，则返回true</returns>
    public bool IsNetVersionAtLeast(int requiredVersion)
    {
        return VersionInt >= requiredVersion;
    }

    private float GetNetVersionAsFloat()
    {
        if (string.IsNullOrEmpty(_version))
        {
            return 0f;
        }

        var match = _versionRegex.Match(_version);
        if (!match.Success)
        {
            return 0f;
        }

        return float.TryParse(match.Value, out var result) ? result : 0f;
    }

    private int GetNetVersionAsInt()
    {
        if (string.IsNullOrEmpty(_version))
        {
            return 0;
        }

        var match = _versionIntRegex.Match(_version);
        if (!match.Success)
        {
            return 0;
        }

        var split = match.Value.Split('.');
        var result = string.Concat(split);

        // 保证版本号返回的值为4位
        if (split[0].Length > 1)
        {
            result = (result + "0000").Substring(0, 4);
        }

        return int.TryParse(result, out var intResult) ? intResult : 0;
    }

    private bool GetNetVersionMatches(string versionPrefix)
    {
        if (string.IsNullOrEmpty(_version))
        {
            return false;
        }

        return _version.StartsWith(versionPrefix);
    }

    /// <summary>
    /// 将.NET Implementation的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, ".NET Version:    ", Version);
        SystemUtil.Append(builder, ".NET Vendor:     ", Vendor);
        SystemUtil.Append(builder, ".NET Vendor URL: ", VendorUrl);

        return builder.ToString();
    }
}