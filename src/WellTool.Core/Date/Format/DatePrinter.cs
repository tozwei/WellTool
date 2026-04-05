namespace WellTool.Core.Date.Format;

using System;
using System.Globalization;
using System.Text;
using SystemTimeZone = System.TimeZone;
using Calendar = WellTool.Core.Date.Calendar;

/// <summary>
/// 日期格式化输出接口
/// </summary>
public interface DatePrinter : DateBasic
{
	/// <summary>
	/// 格式化日期表示的毫秒数
	/// </summary>
	/// <param name="millis">毫秒数</param>
	/// <returns>格式化后的字符串</returns>
	string Format(long millis);

	/// <summary>
	/// 格式化日期
	/// </summary>
	/// <param name="date">{DateTime}</param>
	/// <returns>格式化后的字符串</returns>
	string Format(DateTime date);

	/// <summary>
	/// 格式化日历
	/// </summary>
	/// <param name="calendar">{Calendar}</param>
	/// <returns>格式化后的字符串</returns>
	string Format(Calendar calendar);

	/// <summary>
	/// 格式化毫秒数到缓冲区
	/// </summary>
	/// <param name="millis">毫秒数</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	StringBuilder Format(long millis, StringBuilder buf);

	/// <summary>
	/// 格式化日期到缓冲区
	/// </summary>
	/// <param name="date">{DateTime}</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	StringBuilder Format(DateTime date, StringBuilder buf);

	/// <summary>
	/// 格式化日历到缓冲区
	/// </summary>
	/// <param name="calendar">{Calendar}</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	StringBuilder Format(Calendar calendar, StringBuilder buf);
}
