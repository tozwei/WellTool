using System;
using SystemTimeZone = System.TimeZoneInfo;

namespace WellDone.Core.Date;

/// <summary>
/// 星座工具类
/// </summary>
public static class Zodiac
{
	/// <summary>星座分隔时间日</summary>
	private static readonly int[] DAY_ARR = { 20, 19, 21, 20, 21, 22, 23, 23, 23, 24, 23, 22 };

	/// <summary>星座</summary>
	private static readonly string[] ZODIACS = { "摩羯座", "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座" };

	/// <summary>生肖</summary>
	private static readonly string[] CHINESE_ZODIACS = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

	/// <summary>
	/// 通过生日计算星座
	/// </summary>
	/// <param name="date">出生日期</param>
	/// <returns>星座名</returns>
	public static string GetZodiac(DateTime date) => GetZodiac(date.Month, date.Day);

	/// <summary>
	/// 通过生日计算星座
	/// </summary>
	/// <param name="month">月，从1开始计数</param>
	/// <param name="day">日</param>
	/// <returns>星座名</returns>
	public static string GetZodiac(int month, int day)
	{
		if (month < 1 || month > 12)
			throw new ArgumentOutOfRangeException(nameof(month), "Unsupported month value, must be [1,12]");
		// 在分隔日前为前一个星座，否则为后一个星座
		return day < DAY_ARR[month - 1] ? ZODIACS[month - 1] : ZODIACS[month];
	}

	/// <summary>
	/// 计算生肖，只计算1900年后出生的人
	/// </summary>
	/// <param name="year">农历年</param>
	/// <returns>生肖名</returns>
	public static string GetChineseZodiac(int year)
	{
		if (year < 1900)
			return null;
		return CHINESE_ZODIACS[(year - 1900) % CHINESE_ZODIACS.Length];
	}
}
