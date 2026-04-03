using System;
using System.Collections.Generic;

namespace WellTool.Core.Date.Chinese;

/// <summary>
/// 节假日（农历）封装
/// </summary>
public static class LunarFestival
{
	/// <summary>农历节日</summary>
	private static readonly Dictionary<(int month, int day), List<string>> L_FTV = new()
	{
		// 正月
		{ (1, 1), new List<string> { "春节" } },
		{ (1, 5), new List<string> { "牛日", "破五日" } },
		{ (1, 7), new List<string> { "人日", "人胜节" } },
		{ (1, 15), new List<string> { "元宵节", "上元节" } },

		// 二月
		{ (2, 2), new List<string> { "龙抬头" } },

		// 三月
		{ (3, 3), new List<string> { "上巳节" } },

		// 五月
		{ (5, 5), new List<string> { "端午节", "端阳节" } },

		// 六月
		{ (6, 6), new List<string> { "晒衣节", "姑姑节", "天贶节" } },

		// 七月
		{ (7, 7), new List<string> { "七夕" } },
		{ (7, 15), new List<string> { "盂兰盆节", "中元节" } },

		// 八月
		{ (8, 15), new List<string> { "中秋节" } },

		// 九月
		{ (9, 9), new List<string> { "重阳节" } },

		// 十月
		{ (10, 1), new List<string> { "祭祖节" } },
		{ (10, 15), new List<string> { "下元节" } },

		// 腊月
		{ (12, 8), new List<string> { "腊八节" } },
		{ (12, 23), new List<string> { "小年" } },
		{ (12, 30), new List<string> { "除夕" } }
	};

	/// <summary>
	/// 获得节日列表
	/// </summary>
	/// <param name="year">年</param>
	/// <param name="month">月</param>
	/// <param name="day">日</param>
	/// <returns>获得农历节日</returns>
	public static List<string> GetFestivals(int year, int month, int day)
	{
		// 春节判断，如果12月是小月，则29为除夕，否则30为除夕
		if (month == 12 && day == 29)
		{
			if (29 == LunarInfo.MonthDays(year, month))
			{
				day++;
			}
		}
		return GetFestivals(month, day);
	}

	/// <summary>
	/// 获得节日列表
	/// </summary>
	/// <param name="month">月</param>
	/// <param name="day">日</param>
	/// <returns>获得农历节日</returns>
	public static List<string> GetFestivals(int month, int day)
	{
		if (L_FTV.TryGetValue((month, day), out var festivals))
		{
			return festivals;
		}
		return new List<string>();
	}
}
