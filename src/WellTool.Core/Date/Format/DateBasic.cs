namespace WellTool.Core.Date.Format;

using System.Globalization;
using SystemTimeZone = System.TimeZone;

/// <summary>
/// 日期基本信息获取接口
/// </summary>
public interface DateBasic
{
	/// <summary>
	/// 获得日期格式化或者转换的格式
	/// </summary>
	/// <returns>格式字符串</returns>
	string GetPattern();

	/// <summary>
	/// 获得时区
	/// </summary>
	/// <returns>{System.TimeZone}</returns>
	SystemTimeZone GetTimeZone();

	/// <summary>
	/// 获得 日期地理位置
	/// </summary>
	/// <returns>{CultureInfo}</returns>
	CultureInfo GetLocale();
}
