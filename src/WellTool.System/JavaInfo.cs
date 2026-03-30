//using System.Text;
//using System.Text.RegularExpressions;

//namespace WellTool.System;

///// <summary>
///// .NET Runtime 信息（对应Java的JavaInfo）
///// </summary>
//public class JavaInfo
//{
//    private static readonly Lazy<JavaInfo> _instance = new(() => new JavaInfo());
//    public static JavaInfo Instance => _instance.Value;

//    private static readonly Regex _versionRegex = new(@"^[0-9]{1,2}(\.[0-9]{1,2})?", RegexOptions.Compiled);
//    private static readonly Regex _versionIntRegex = new(@"^[0-9]{1,2}(\.[0-9]{1,2}){0,2}", RegexOptions.Compiled);

//    private readonly string _version;
//    private readonly float _versionFloat;
//    private readonly int _versionInt;
//    private readonly string _vendor;
//    private readonly string _vendorUrl;

//    private readonly bool _isJava1_8;
//    private readonly bool _isJava9;
//    private readonly bool _isJava10;
//    private readonly bool _isJava11;
//    private readonly bool _isJava12;
//    private readonly bool _isJava13;
//    private readonly bool _isJava14;
//    private readonly bool _isJava15;
//    private readonly bool _isJava16;
//    private readonly bool _isJava17;
//    private readonly bool _isJava18;

//    private JavaInfo()
//    {
//        _version = Environment.Version.ToString();
//        _versionFloat = GetJavaVersionAsFloat();
//        _versionInt = GetJavaVersionAsInt();
//        _vendor = ".NET Runtime";
//        _vendorUrl = "https://dotnet.microsoft.com/";

//        _isJava1_8 = GetJavaVersionMatches("1.8");
//        _isJava9 = GetJavaVersionMatches("9");
//        _isJava10 = GetJavaVersionMatches("10");
//        _isJava11 = GetJavaVersionMatches("11");
//        _isJava12 = GetJavaVersionMatches("12");
//        _isJava13 = GetJavaVersionMatches("13");
//        _isJava14 = GetJavaVersionMatches("14");
//        _isJava15 = GetJavaVersionMatches("15");
//        _isJava16 = GetJavaVersionMatches("16");
//        _isJava17 = GetJavaVersionMatches("17");
//        _isJava18 = GetJavaVersionMatches("18");
//    }

//    /// <summary>
//    /// 取得当前.NET impl.的版本
//    /// </summary>
//    /// <returns>版本号</returns>
//    public string Version => _version;

//    /// <summary>
//    /// 取得当前.NET impl.的版本（float）
//    /// </summary>
//    /// <returns>版本float值</returns>
//    public float VersionFloat => _versionFloat;

//    /// <summary>
//    /// 取得当前.NET impl.的版本（int）
//    /// </summary>
//    /// <returns>版本int值</returns>
//    public int VersionInt => _versionInt;

//    /// <summary>
//    /// 取得当前.NET impl.的厂商
//    /// </summary>
//    /// <returns>厂商</returns>
//    public string Vendor => _vendor;

//    /// <summary>
//    /// 取得当前.NET impl.的厂商网站的URL
//    /// </summary>
//    /// <returns>厂商URL</returns>
//    public string VendorUrl => _vendorUrl;

//    /// <summary>
//    /// 判断当前.NET的版本是否为1.8
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava1_8 => _isJava1_8;

//    /// <summary>
//    /// 判断当前.NET的版本是否为9
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava9 => _isJava9;

//    /// <summary>
//    /// 判断当前.NET的版本是否为10
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava10 => _isJava10;

//    /// <summary>
//    /// 判断当前.NET的版本是否为11
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava11 => _isJava11;

//    /// <summary>
//    /// 判断当前.NET的版本是否为12
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava12 => _isJava12;

//    /// <summary>
//    /// 判断当前.NET的版本是否为13
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava13 => _isJava13;

//    /// <summary>
//    /// 判断当前.NET的版本是否为14
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava14 => _isJava14;

//    /// <summary>
//    /// 判断当前.NET的版本是否为15
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava15 => _isJava15;

//    /// <summary>
//    /// 判断当前.NET的版本是否为16
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava16 => _isJava16;

//    /// <summary>
//    /// 判断当前.NET的版本是否为17
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava17 => _isJava17;

//    /// <summary>
//    /// 判断当前.NET的版本是否为18
//    /// </summary>
//    /// <returns>如果是返回true</returns>
//    public bool IsJava18 => _isJava18;

//    /// <summary>
//    /// 判定当前.NET的版本是否大于等于指定的版本号
//    /// </summary>
//    /// <param name="requiredVersion">需要的版本</param>
//    /// <returns>如果当前.NET版本大于或等于指定的版本，则返回true</returns>
//    public bool IsJavaVersionAtLeast(float requiredVersion)
//    {
//        return VersionFloat >= requiredVersion;
//    }

//    /// <summary>
//    /// 判定当前.NET的版本是否大于等于指定的版本号
//    /// </summary>
//    /// <param name="requiredVersion">需要的版本</param>
//    /// <returns>如果当前.NET版本大于或等于指定的版本，则返回true</returns>
//    public bool IsJavaVersionAtLeast(int requiredVersion)
//    {
//        return VersionInt >= requiredVersion;
//    }

//    private float GetJavaVersionAsFloat()
//    {
//        if (string.IsNullOrEmpty(_version))
//        {
//            return 0f;
//        }

//        var match = _versionRegex.Match(_version);
//        if (!match.Success)
//        {
//            return 0f;
//        }

//        return float.TryParse(match.Value, out var result) ? result : 0f;
//    }

//    private int GetJavaVersionAsInt()
//    {
//        if (string.IsNullOrEmpty(_version))
//        {
//            return 0;
//        }

//        var match = _versionIntRegex.Match(_version);
//        if (!match.Success)
//        {
//            return 0;
//        }

//        var split = match.Value.Split('.');
//        var result = string.Concat(split);

//        // 保证java10及其之后的版本返回的值为4位
//        if (split[0].Length > 1)
//        {
//            result = (result + "0000").Substring(0, 4);
//        }

//        return int.TryParse(result, out var intResult) ? intResult : 0;
//    }

//    private bool GetJavaVersionMatches(string versionPrefix)
//    {
//        if (string.IsNullOrEmpty(_version))
//        {
//            return false;
//        }

//        return _version.StartsWith(versionPrefix);
//    }

//    /// <summary>
//    /// 将.NET Implementation的信息转换成字符串
//    /// </summary>
//    /// <returns>字符串表示</returns>
//    public override string ToString()
//    {
//        var builder = new StringBuilder();

//        SystemUtil.Append(builder, "Java Version:    ", Version);
//        SystemUtil.Append(builder, "Java Vendor:     ", Vendor);
//        SystemUtil.Append(builder, "Java Vendor URL: ", VendorUrl);

//        return builder.ToString();
//    }
//}