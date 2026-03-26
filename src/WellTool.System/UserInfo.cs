using System.Text;

namespace WellTool.System;

/// <summary>
/// 用户信息（对应Java的UserInfo）
/// </summary>
public class UserInfo
{
    private static readonly Lazy<UserInfo> _instance = new(() => new UserInfo());
    public static UserInfo Instance => _instance.Value;

    private readonly string _userName;
    private readonly string _userHome;
    private readonly string _userDir;
    private readonly string _tempDir;
    private readonly string _userLanguage;
    private readonly string _userCountry;

    private UserInfo()
    {
        _userName = Environment.UserName;
        _userHome = FixPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        _userDir = FixPath(Environment.CurrentDirectory);
        _tempDir = FixPath(Path.GetTempPath());
        _userLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        _userCountry = CultureInfo.CurrentUICulture.Name.Length >= 2
            ? CultureInfo.CurrentUICulture.Name.Substring(CultureInfo.CurrentUICulture.Name.Length - 2)
            : CultureInfo.CurrentUICulture.Name;
    }

    /// <summary>
    /// 取得当前登录用户的名字
    /// </summary>
    public string Name => _userName;

    /// <summary>
    /// 取得当前登录用户的home目录
    /// </summary>
    public string HomeDir => _userHome;

    /// <summary>
    /// 取得当前目录
    /// </summary>
    public string CurrentDir => _userDir;

    /// <summary>
    /// 取得临时目录
    /// </summary>
    public string TempDir => _tempDir;

    /// <summary>
    /// 取得当前登录用户的语言设置
    /// </summary>
    public string Language => _userLanguage;

    /// <summary>
    /// 取得当前登录用户的国家或区域设置
    /// </summary>
    public string Country => _userCountry;

    private static string FixPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return path;
        }

        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            return path + Path.DirectorySeparatorChar;
        }

        return path;
    }

    /// <summary>
    /// 将当前用户的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, "User Name:        ", Name);
        SystemUtil.Append(builder, "User Home Dir:    ", HomeDir);
        SystemUtil.Append(builder, "User Current Dir: ", CurrentDir);
        SystemUtil.Append(builder, "User Temp Dir:    ", TempDir);
        SystemUtil.Append(builder, "User Language:    ", Language);
        SystemUtil.Append(builder, "User Country:     ", Country);

        return builder.ToString();
    }
}