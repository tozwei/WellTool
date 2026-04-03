using System;
using System.IO;

namespace WellDone.Compiled;
using WellDone.Converter;

using WellDone.Core.Converter;
using WellDone.Core.Date.Chinese;
using WellDone.Core.Math;
using WellDone.Core.Util;

using SystemTimeZone = System.TimeZoneInfo;

using java.time.LocalDate = System.DateTime;
using Date = System.DateTime;

/// <summary>
/// 农历日期工具，最大支持到2099年，支持：
/// - 通过公历日期构造获取对应农历
/// - 通过农历日期直接构造
/// </summary>
/// <author>zjw, looly</author>
public class ChineseDate
{
	// 农历年
	private readonly int year;
	// 农历月，润N月这个值就是N+1，其他月按照显示月份赋值
	private readonly int month;
	// 当前月份是否闰月
	private readonly bool isLeapMonth;
	// 农历日
	private readonly int day;

	// 公历年
	private readonly int gyear;
	// 公历月，从1开始计数
	private readonly int gmonthBase1;
	// 公历日
	private readonly int gday;

	/// <summary>
	/// 通过公历日期构造
	/// </summary>
	/// <param name="date">公历日期</param>
	public ChineseDate(Date date)
	{
		var localDate = date.Date;
		// 公历
		gyear = localDate.Year;
		gmonthBase1 = localDate.Month;
		gday = localDate.Day;

		// 求出和1900年1月31日相差的天数
		var baseDay = new DateTime(1900, 1, 31);
		int offset = (int)(localDate - baseDay).TotalDays;

		// 计算农历年份
		// 用offset减去每农历年的天数，计算当天是农历第几天
		int daysOfYear;
		int iYear;
		for (iYear = LunarInfo.BASE_YEAR; iYear <= LunarInfo.MAX_YEAR; iYear++)
		{
			daysOfYear = LunarInfo.YearDays(iYear);
			if (offset < daysOfYear)
			{
				break;
			}
			offset -= daysOfYear;
		}

		year = iYear;
		// 计算农历月份
		int leapMonth = LunarInfo.LeapMonth(iYear);
		int month;
		int daysOfMonth;
		bool hasLeapMonth = false;
		for (month = 1; month < 13; month++)
		{
			if (leapMonth > 0 && month == (leapMonth + 1))
			{
				daysOfMonth = LunarInfo.LeapDays(year);
				hasLeapMonth = true;
			}
			else
			{
				daysOfMonth = LunarInfo.MonthDays(year, hasLeapMonth ? month - 1 : month);
			}

			if (offset < daysOfMonth)
			{
				break;
			}
			offset -= daysOfMonth;
		}

		isLeapMonth = leapMonth > 0 && (month == (leapMonth + 1));
		if (hasLeapMonth && !isLeapMonth)
		{
			month--;
		}
		this.month = month;
		this.day = offset + 1;
	}

	/// <summary>
	/// 构造方法传入日期
	/// </summary>
	/// <param name="chineseYear">农历年</param>
	/// <param name="chineseMonth">农历月，1表示一月（正月）</param>
	/// <param name="chineseDay">农历日，1表示初一</param>
	public ChineseDate(int chineseYear, int chineseMonth, int chineseDay)
		: this(chineseYear, chineseMonth, chineseDay, chineseMonth == LunarInfo.LeapMonth(chineseYear))
	{
	}

	/// <summary>
	/// 构造方法传入日期
	/// </summary>
	/// <param name="chineseYear">农历年</param>
	/// <param name="chineseMonth">农历月，1表示一月（正月）</param>
	/// <param name="chineseDay">农历日，1表示初一</param>
	/// <param name="isLeapMonth">当前月份是否闰月</param>
	public ChineseDate(int chineseYear, int chineseMonth, int chineseDay, bool isLeapMonth)
	{
		if (chineseMonth != LunarInfo.LeapMonth(chineseYear))
		{
			isLeapMonth = false;
		}

		this.day = chineseDay;
		this.isLeapMonth = isLeapMonth;
		this.month = isLeapMonth ? chineseMonth + 1 : chineseMonth;
		this.year = chineseYear;

		var dateTime = Lunar2solar(chineseYear, chineseMonth, chineseDay, isLeapMonth);
		if (dateTime.HasValue)
		{
			this.gday = dateTime.Value.Day;
			this.gmonthBase1 = dateTime.Value.Month;
			this.gyear = dateTime.Value.Year;
		}
		else
		{
			this.gday = -1;
			this.gmonthBase1 = -1;
			this.gyear = -1;
		}
	}

	/// <summary>获得农历年份</summary>
	public int ChineseYear => year;

	/// <summary>获取公历的年</summary>
	public int GregorianYear => gyear;

	/// <summary>获取农历的月，从1开始计数</summary>
	public int Month => month;

	/// <summary>获取公历的月，从1开始计数</summary>
	public int GregorianMonthBase1 => gmonthBase1;

	/// <summary>获取公历的月，从0开始计数</summary>
	public int GregorianMonth => gmonthBase1 - 1;

	/// <summary>当前农历月份是否为闰月</summary>
	public bool IsLeapMonth => isLeapMonth;

	/// <summary>获取农历的日，从1开始计数</summary>
	public int Day => day;

	/// <summary>获取公历的日</summary>
	public int GregorianDay => gday;

	/// <summary>获得农历日</summary>
	public string ChineseDay
	{
		get
		{
			string[] chineseTen = { "初", "十", "廿", "卅" };
			int n = (day % 10 == 0) ? 9 : (day % 10 - 1);
			if (day > 30)
			{
				return "";
			}
			switch (day)
			{
				case 10:
					return "初十";
				case 20:
					return "二十";
				case 30:
					return "三十";
				default:
					return chineseTen[day / 10] + NumberChineseFormatter.Format(n + 1, false);
			}
		}
	}

	/// <summary>获得农历月份（中文，例如二月，十二月，或者润一月）</summary>
	public string ChineseMonthName => ChineseMonth.GetChineseMonthName(IsLeapMonth, IsLeapMonth() ? Month - 1 : Month, false);

	/// <summary>获得农历月份（中文，例如二月，十二月，或者润一月）</summary>
	/// <param name="isTraditional">是否传统表示，例如一月传统表示为正月</param>
	public string GetChineseMonth(bool isTraditional) => ChineseMonth.GetChineseMonthName(IsLeapMonth, IsLeapMonth() ? Month - 1 : Month, isTraditional);

	/// <summary>获得农历月份称呼</summary>
	public string GetChineseMonthName() => ChineseMonth.GetChineseMonthName(IsLeapMonth, IsLeapMonth() ? Month - 1 : Month, false);

	private bool IsLeapMonth() => isLeapMonth;

	/// <summary>获取公历的Date</summary>
	public Date GregorianDate => new Date(gyear, GetGregorianMonth(), gday);

	/// <summary>获得年份生肖</summary>
	public string ChineseZodiac => Zodiac.GetChineseZodiac(year);

	/// <summary>获得年的天干地支</summary>
	public string Cyclical => GanZhi.GetGanzhiOfYear(year);

	/// <summary>干支纪年信息</summary>
	public string CyclicalYMD()
	{
		if (gyear >= LunarInfo.BASE_YEAR && gmonthBase1 > 0 && gday > 0)
		{
			return Cyclicalm(gyear, gmonthBase1, gday);
		}
		return null;
	}

	/// <summary>获得节气</summary>
	public string Term => SolarTerms.GetTerm(gyear, gmonthBase1, gday);

	/// <summary>转换为标准的日期格式来表示农历日期，例如2020-01-13</summary>
	public string ToStringNormal() => $"{year:0000}-{(IsLeapMonth() ? month - 1 : month):00}-{day:00}";

	/// <summary>通过农历年月日信息 返回公历信息 提供给构造函数</summary>
	private DateTime? Lunar2solar(int chineseYear, int chineseMonth, int chineseDay, bool isLeapMonth)
	{
		// 超出最大极限值
		if ((chineseYear == 2100 && chineseMonth == 12 && chineseDay > 1) ||
				(chineseYear == LunarInfo.BASE_YEAR && chineseMonth == 1 && chineseDay < 31))
		{
			return null;
		}
		int day = LunarInfo.MonthDays(chineseYear, chineseMonth);
		int _day = day;
		if (isLeapMonth)
		{
			_day = LunarInfo.LeapDays(chineseYear);
		}
		// 参数合法性效验
		if (chineseYear < LunarInfo.BASE_YEAR || chineseYear > 2100 || chineseDay > _day)
		{
			return null;
		}
		// 计算农历的时间差
		int offset = 0;
		for (int i = LunarInfo.BASE_YEAR; i < chineseYear; i++)
		{
			offset += LunarInfo.YearDays(i);
		}
		int leap;
		bool isAdd = false;
		for (int i = 1; i < chineseMonth; i++)
		{
			leap = LunarInfo.LeapMonth(chineseYear);
			if (!isAdd)
			{
				if (leap <= i && leap > 0)
				{
					offset += LunarInfo.LeapDays(chineseYear);
					isAdd = true;
				}
			}
			offset += LunarInfo.MonthDays(chineseYear, i);
		}
		// 转换闰月农历 需补充该年闰月的前一个月的时差
		if (isLeapMonth)
		{
			offset += day;
		}
		// 1900年农历正月一日的公历时间为1900年1月30日0时0分0秒
		var baseDate = new DateTime(1900, 1, 31);
		return baseDate.AddDays(offset + chineseDay - 31);
	}

	private bool IsLeapMonth() => isLeapMonth;

	private string Cyclicalm(int year, int month, int day)
	{
		return $"{GanZhi.GetGanzhiOfYear(this.year)}年{GanZhi.GetGanzhiOfMonth(year, month, day)}";
	}

	private bool IsLeapMonth() => isLeapMonth;

	/// <summary>获得公历月份</summary>
	private int GetGregorianMonth() => gmonthBase1;

	private bool IsLeapMonth() => isLeapMonth;

	public override string ToString() => $"{Cyclical}{ChineseZodiac}年 {GetChineseMonthName()}{ChineseDay}";
}
