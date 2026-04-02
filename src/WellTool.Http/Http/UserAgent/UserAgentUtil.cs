using System.Text.RegularExpressions;

namespace WellTool.Http.UserAgent;

/// <summary>
/// User-Agent 解析工具类
/// </summary>
public static partial class UserAgentUtil
{
    private static readonly Regex MobilePattern = new(@"Mobile|Android|iPhone|iPod", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex TabletPattern = new(@"Tablet|iPad", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex BrowserPattern = new(@"(?<browser>Chrome|Firefox|Safari|Edge|MSIE|Opera)[/\s](?<version>[\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex OSPattern = new(@"(Windows NT|Mac OS X|Linux|Android|iOS)[\s/]?(?<version>[\d._]+)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// 解析 User-Agent 字符串
    /// </summary>
    /// <param name="userAgentString">User-Agent 字符串</param>
    /// <returns>UserAgent 对象</returns>
    public static UserAgent Parse(string userAgentString)
    {
        if (string.IsNullOrWhiteSpace(userAgentString))
        {
            return new UserAgent
            {
                Browser = new Browser { Name = "Unknown", Type = "Unknown", Vendor = "Unknown" },
                OS = new OS { Name = "Unknown", Version = string.Empty, Vendor = "Unknown" },
                Engine = new Engine { Name = "Unknown", Version = string.Empty },
                Platform = Platform.UNKNOWN
            };
        }

        var userAgent = new UserAgent
        {
            Browser = new Browser { Name = "Unknown", Type = "Unknown", Vendor = "Unknown" },
            OS = new OS { Name = "Unknown", Version = string.Empty, Vendor = "Unknown" },
            Engine = new Engine { Name = "Unknown", Version = string.Empty },
            Platform = Platform.UNKNOWN
        };

        // 判断是否为移动设备
        userAgent.Mobile = MobilePattern.IsMatch(userAgentString) && !TabletPattern.IsMatch(userAgentString);

        // 解析浏览器信息
        var browserMatch = BrowserPattern.Match(userAgentString);
        if (browserMatch.Success)
        {
            userAgent.Browser = new Browser
            {
                Name = browserMatch.Groups["browser"].Value,
                Type = browserMatch.Groups["browser"].Value,
                Vendor = GetBrowserVendor(browserMatch.Groups["browser"].Value)
            };
            userAgent.Version = browserMatch.Groups["version"].Value;
        }

        // 解析操作系统
        var osMatch = OSPattern.Match(userAgentString);
        if (osMatch.Success)
        {
            var osName = osMatch.Groups[1].Value;
            userAgent.OS = new OS
            {
                Name = osName,
                Version = osMatch.Groups["version"].Success ? osMatch.Groups["version"].Value : string.Empty,
                Vendor = GetOSVendor(osName)
            };
            userAgent.OsVersion = osMatch.Groups["version"].Success ? osMatch.Groups["version"].Value : string.Empty;
        }

        // 判断平台类型
        if (TabletPattern.IsMatch(userAgentString))
        {
            userAgent.Platform = Platform.TABLET;
        }
        else if (userAgent.Mobile)
        {
            userAgent.Platform = Platform.MOBILE;
        }
        else
        {
            userAgent.Platform = Platform.DESKTOP;
        }

        return userAgent;
    }

    /// <summary>
    /// 获取浏览器厂商
    /// </summary>
    /// <param name="browserName">浏览器名称</param>
    /// <returns>厂商</returns>
    private static string GetBrowserVendor(string browserName)
    {
        return browserName.ToLowerInvariant() switch
        {
            "chrome" => "Google",
            "firefox" => "Mozilla",
            "safari" => "Apple",
            "edge" => "Microsoft",
            "msie" => "Microsoft",
            "opera" => "Opera Software",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// 获取操作系统厂商
    /// </summary>
    /// <param name="osName">操作系统名称</param>
    /// <returns>厂商</returns>
    private static string GetOSVendor(string osName)
    {
        if (osName.Contains("Windows", StringComparison.OrdinalIgnoreCase))
            return "Microsoft";
        if (osName.Contains("Mac", StringComparison.OrdinalIgnoreCase) ||
            osName.Contains("iOS", StringComparison.OrdinalIgnoreCase))
            return "Apple";
        if (osName.Contains("Android", StringComparison.OrdinalIgnoreCase))
            return "Google";
        if (osName.Contains("Linux", StringComparison.OrdinalIgnoreCase))
            return "Linux Foundation";

        return "Unknown";
    }
}
