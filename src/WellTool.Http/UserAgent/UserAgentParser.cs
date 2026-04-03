using System;
using System.Text.RegularExpressions;

namespace WellTool.Http.UserAgent
{
    /// <summary>
    /// User-Agent解析器
    /// </summary>
    public static class UserAgentParser
    {
        // 浏览器正则表达式
        private static readonly Regex ChromePattern = new Regex(@"Chrome/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex FirefoxPattern = new Regex(@"Firefox/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex EdgePattern = new Regex(@"Edg/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex IEBattern = new Regex(@"(?:MSIE |Trident/.*rv:)([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex SafariPattern = new Regex(@"Safari/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex OperaPattern = new Regex(@"(?:Opera|OPR)/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // 操作系统正则表达式
        private static readonly Regex WindowsPattern = new Regex(@"Windows NT ([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex MacOSPattern = new Regex(@"Mac OS X ([\d._]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex LinuxPattern = new Regex(@"(?:Linux|X11)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex AndroidPattern = new Regex(@"Android ([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex iOSPattern = new Regex(@"(?:iPhone|iPad|iPod) OS ([\d_]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // 平台正则表达式
        private static readonly Regex MobilePattern = new Regex(@"(?:Mobile|Android|iPhone|iPad|iPod)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // 引擎正则表达式
        private static readonly Regex WebkitPattern = new Regex(@"AppleWebKit/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex GeckoPattern = new Regex(@"Gecko/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex PrestoPattern = new Regex(@"Presto/([\d.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 解析User-Agent字符串
        /// </summary>
        /// <param name="userAgentString">User-Agent字符串</param>
        /// <returns>UserAgent对象</returns>
        public static UserAgent Parse(string userAgentString)
        {
            var userAgent = new UserAgent();

            if (string.IsNullOrEmpty(userAgentString))
            {
                return userAgent;
            }

            // 解析浏览器
            userAgent.Browser = ParseBrowser(userAgentString, userAgent);

            // 解析操作系统
            userAgent.Os = ParseOS(userAgentString, userAgent);

            // 解析平台
            userAgent.Platform = ParsePlatform(userAgentString, userAgent);

            // 解析引擎
            userAgent.Engine = ParseEngine(userAgentString, userAgent);

            // 解析是否为移动设备
            userAgent.IsMobile = MobilePattern.IsMatch(userAgentString);

            return userAgent;
        }

        private static Browser ParseBrowser(string userAgentString, UserAgent userAgent)
        {
            Match match;

            // Edge (基于Chromium)
            match = EdgePattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.Edge;
            }

            // Chrome
            match = ChromePattern.Match(userAgentString);
            if (match.Success && !userAgentString.Contains("OPR"))
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.Chrome;
            }

            // Firefox
            match = FirefoxPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.Firefox;
            }

            // Opera
            match = OperaPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.Opera;
            }

            // Safari
            match = SafariPattern.Match(userAgentString);
            if (match.Success && !userAgentString.Contains("Chrome"))
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.Safari;
            }

            // IE
            match = IEBattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.Version = match.Groups[1].Value;
                return Browser.IE;
            }

            return Browser.Unknown;
        }

        private static OS ParseOS(string userAgentString, UserAgent userAgent)
        {
            Match match;

            // Windows
            match = WindowsPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.OsVersion = match.Groups[1].Value;
                return OS.Windows;
            }

            // macOS
            match = MacOSPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.OsVersion = match.Groups[1].Value.Replace("_", ".");
                return OS.MacOS;
            }

            // Linux
            match = LinuxPattern.Match(userAgentString);
            if (match.Success)
            {
                return OS.Linux;
            }

            // Android
            match = AndroidPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.OsVersion = match.Groups[1].Value;
                return OS.Android;
            }

            // iOS
            match = iOSPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.OsVersion = match.Groups[1].Value.Replace("_", ".");
                return OS.iOS;
            }

            return OS.Unknown;
        }

        private static Platform ParsePlatform(string userAgentString, UserAgent userAgent)
        {
            if (Regex.IsMatch(userAgentString, @"Win64|x64|WOW64|amd64", RegexOptions.IgnoreCase))
                return Platform.Windows64;
            if (userAgentString.Contains("Windows"))
                return Platform.Windows;
            if (userAgentString.Contains("Mac"))
                return Platform.Mac;
            if (userAgentString.Contains("Linux"))
                return Platform.Linux;
            if (Regex.IsMatch(userAgentString, @"Android", RegexOptions.IgnoreCase))
                return Platform.Android;
            if (Regex.IsMatch(userAgentString, @"iPhone|iPad|iPod", RegexOptions.IgnoreCase))
                return Platform.iOS;

            return Platform.Unknown;
        }

        private static Engine ParseEngine(string userAgentString, UserAgent userAgent)
        {
            Match match;

            // WebKit
            match = WebkitPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.EngineVersion = match.Groups[1].Value;
                return Engine.WebKit;
            }

            // Gecko
            match = GeckoPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.EngineVersion = match.Groups[1].Value;
                return Engine.Gecko;
            }

            // Presto
            match = PrestoPattern.Match(userAgentString);
            if (match.Success)
            {
                userAgent.EngineVersion = match.Groups[1].Value;
                return Engine.Presto;
            }

            return Engine.Unknown;
        }
    }
}
