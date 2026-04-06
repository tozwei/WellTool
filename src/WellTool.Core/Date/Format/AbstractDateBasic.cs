namespace WellTool.Core.Date.Format;

using System;
using System.Globalization;
using System.IO;
using SystemTimeZone = System.TimeZone;

/// <summary>
/// 日期格式化基类
/// </summary>
[Serializable]
public abstract class AbstractDateBasic : DateBasic
{
	private const long SerialVersionUid = 6333136319870641818L;

	/// <summary>
	/// 格式
	/// </summary>
	protected readonly string Pattern;

	/// <summary>
	/// 时区
	/// </summary>
	protected readonly SystemTimeZone TimeZone;

	/// <summary>
	/// 区域
	/// </summary>
	protected readonly CultureInfo Locale;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	protected AbstractDateBasic(string pattern, SystemTimeZone timeZone, CultureInfo locale)
	{
		Pattern = pattern;
		TimeZone = timeZone;
		Locale = locale;
	}

	/// <summary>
	/// 获得日期格式化或者转换的格式
	/// </summary>
	/// <returns>{System.String}</returns>
	public string GetPattern() => Pattern;

	/// <summary>
	/// 获得时区
	/// </summary>
	/// <returns>{System.TimeZone}</returns>
	public SystemTimeZone GetTimeZone() => TimeZone;

	/// <summary>
	/// 获得 日期地理位置
	/// </summary>
	/// <returns>{CultureInfo}</returns>
	public CultureInfo GetLocale() => Locale;

	/// <summary>
	/// 是否相等
	/// </summary>
	public override bool Equals(object obj)
	{
		if (obj is not FastDatePrinter)
			return false;
		var other = (AbstractDateBasic)obj;
		return Pattern == other.Pattern && TimeZone == other.TimeZone && Locale.Equals(other.Locale);
	}

	/// <summary>
	/// 获取哈希码
	/// </summary>
	public override int GetHashCode() => Pattern.GetHashCode() + 13 * (TimeZone.GetHashCode() + 13 * Locale.GetHashCode());

	/// <summary>
	/// 返回字符串
	/// </summary>
	public override string ToString() => $"FastDatePrinter[{Pattern},{Locale},{TimeZone.StandardName}]";
}
