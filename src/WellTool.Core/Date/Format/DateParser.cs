namespace WellTool.Core.Date.Format;

using System;
using System.Globalization;
using Calendar = WellTool.Core.Date.Calendar;

/// <summary>
/// 日期解析接口，用于解析日期字符串为 {DateTime} 对象
/// </summary>
public interface DateParser : DateBasic
{
	/// <summary>
	/// 将日期字符串解析并转换为 {DateTime} 对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <returns>{DateTime}</returns>
	/// <exception cref="FormatException">转换异常，被转换的字符串格式错误</exception>
	DateTime Parse(string source);

	/// <summary>
	/// 将日期字符串解析并转换为 {DateTime} 对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <returns>{DateTime}</returns>
	DateTime Parse(string source, ref int pos);

	/// <summary>
	/// 根据给定格式更新日历
	/// </summary>
	/// <param name="source">被转换的日期字符串</param>
	/// <param name="pos">解析位置，转换结束后更新转换到的位置</param>
	/// <param name="calendar">日历对象</param>
	/// <returns>是否解析成功</returns>
	bool Parse(string source, ref int pos, Calendar calendar);

	/// <summary>
	/// 将日期字符串解析并转换为 {DateTime} 对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <returns>{DateTime}</returns>
	object ParseObject(string source);

	/// <summary>
	/// 将日期字符串解析并转换为 {DateTime} 对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <returns>{DateTime}</returns>
	object ParseObject(string source, ref int pos);
}
