using System.Globalization;

namespace WellTool.Core.Lang;

/// <summary>
/// Locale工具类
/// </summary>
public static class LocaleUtil
{
    /// <summary>
    /// 获取默认的Locale
    /// </summary>
    /// <returns>默认的Locale</returns>
    public static CultureInfo GetDefault()
    {
        return CultureInfo.CurrentCulture;
    }

    /// <summary>
    /// 获取可用的Locales
    /// </summary>
    /// <returns>可用的Locales</returns>
    public static CultureInfo[] GetAvailableLocales()
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures);
    }

    /// <summary>
    /// 解析字符串为Locale
    /// </summary>
    /// <param name="localeStr">Locale字符串</param>
    /// <returns>Locale</returns>
    public static CultureInfo Parse(string localeStr)
    {
        try
        {
            return new CultureInfo(localeStr);
        }
        catch
        {
            return CultureInfo.InvariantCulture;
        }
    }

    /// <summary>
    /// 从字符串中获取语言
    /// </summary>
    /// <param name="localeStr">Locale字符串</param>
    /// <returns>语言</returns>
    public static string GetLanguage(string localeStr)
    {
        try
        {
            var locale = Parse(localeStr);
            return locale.TwoLetterISOLanguageName;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 从字符串中获取国家
    /// </summary>
    /// <param name="localeStr">Locale字符串</param>
    /// <returns>国家</returns>
    public static string GetCountry(string localeStr)
    {
        try
        {
            var locale = Parse(localeStr);
            return locale.Name.Split('-')[1];
        }
        catch
        {
            return string.Empty;
        }
    }
}