using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace WellTool.Core.Lang;

/// <summary>
/// 常用正则表达式集合
/// </summary>
public static class PatternPool
{
	/// <summary>
	/// Pattern缓存池
	/// </summary>
	private static readonly ConcurrentDictionary<string, Regex> Pool = new();

	/// <summary>
	/// 英文字母 、数字和下划线
	/// </summary>
	public static readonly Regex GENERAL = Get(RegexPool.GENERAL);

	/// <summary>
	/// 数字
	/// </summary>
	public static readonly Regex NUMBERS = Get(RegexPool.NUMBERS);

	/// <summary>
	/// 字母
	/// </summary>
	public static readonly Regex WORD = Get(RegexPool.WORD);

	/// <summary>
	/// 单个中文汉字
	/// </summary>
	public static readonly Regex CHINESE = Get(RegexPool.CHINESE);

	/// <summary>
	/// 中文汉字
	/// </summary>
	public static readonly Regex CHINESES = Get(RegexPool.CHINESES);

	/// <summary>
	/// 分组
	/// </summary>
	public static readonly Regex GROUP_VAR = Get(RegexPool.GROUP_VAR);

	/// <summary>
	/// IP v4
	/// </summary>
	public static readonly Regex IPV4 = Get(RegexPool.IPV4);

	/// <summary>
	/// IP v6
	/// </summary>
	public static readonly Regex IPV6 = Get(RegexPool.IPV6);

	/// <summary>
	/// 货币
	/// </summary>
	public static readonly Regex MONEY = Get(RegexPool.MONEY);

	/// <summary>
	/// 邮件
	/// </summary>
	public static readonly Regex EMAIL = Get(RegexPool.EMAIL, RegexOptions.IgnoreCase);

	/// <summary>
	/// 带中文支持的邮件
	/// </summary>
	public static readonly Regex EMAIL_WITH_CHINESE = Get(RegexPool.EMAIL_WITH_CHINESE, RegexOptions.IgnoreCase);

	/// <summary>
	/// 移动电话
	/// </summary>
	public static readonly Regex MOBILE = Get(RegexPool.MOBILE);

	/// <summary>
	/// 中国香港移动电话
	/// </summary>
	public static readonly Regex MOBILE_HK = Get(RegexPool.MOBILE_HK);

	/// <summary>
	/// 中国台湾移动电话
	/// </summary>
	public static readonly Regex MOBILE_TW = Get(RegexPool.MOBILE_TW);

	/// <summary>
	/// 中国澳门移动电话
	/// </summary>
	public static readonly Regex MOBILE_MO = Get(RegexPool.MOBILE_MO);

	/// <summary>
	/// 座机号码
	/// </summary>
	public static readonly Regex TEL = Get(RegexPool.TEL);

	/// <summary>
	/// 座机号码+400+800电话
	/// </summary>
	public static readonly Regex TEL_400_800 = Get(RegexPool.TEL_400_800);

	/// <summary>
	/// 18位身份证号码
	/// </summary>
	public static readonly Regex CITIZEN_ID = Get(RegexPool.CITIZEN_ID);

	/// <summary>
	/// 邮编
	/// </summary>
	public static readonly Regex ZIP_CODE = Get(RegexPool.ZIP_CODE);

	/// <summary>
	/// 生日
	/// </summary>
	public static readonly Regex BIRTHDAY = Get(RegexPool.BIRTHDAY);

	/// <summary>
	/// URL
	/// </summary>
	public static readonly Regex URL = Get(RegexPool.URL);

	/// <summary>
	/// Http URL
	/// </summary>
	public static readonly Regex URL_HTTP = Get(RegexPool.URL_HTTP, RegexOptions.IgnoreCase);

	/// <summary>
	/// 中文字、英文字母、数字和下划线
	/// </summary>
	public static readonly Regex GENERAL_WITH_CHINESE = Get(RegexPool.GENERAL_WITH_CHINESE);

	/// <summary>
	/// UUID
	/// </summary>
	public static readonly Regex UUID = Get(RegexPool.UUID, RegexOptions.IgnoreCase);

	/// <summary>
	/// 不带横线的UUID
	/// </summary>
	public static readonly Regex UUID_SIMPLE = Get(RegexPool.UUID_SIMPLE);

	/// <summary>
	/// MAC地址正则
	/// </summary>
	public static readonly Regex MAC_ADDRESS = Get(RegexPool.MAC_ADDRESS, RegexOptions.IgnoreCase);

	/// <summary>
	/// 16进制字符串
	/// </summary>
	public static readonly Regex HEX = Get(RegexPool.HEX);

	/// <summary>
	/// 时间正则
	/// </summary>
	public static readonly Regex TIME = Get(RegexPool.TIME);

	/// <summary>
	/// 中国车牌号码
	/// </summary>
	public static readonly Regex PLATE_NUMBER = Get(RegexPool.PLATE_NUMBER);

	/// <summary>
	/// 统一社会信用代码
	/// </summary>
	public static readonly Regex CREDIT_CODE = Get(RegexPool.CREDIT_CODE);

	/// <summary>
	/// 车架号
	/// </summary>
	public static readonly Regex CAR_VIN = Get(RegexPool.CAR_VIN);

	/// <summary>
	/// 驾驶证
	/// </summary>
	public static readonly Regex CAR_DRIVING_LICENCE = Get(RegexPool.CAR_DRIVING_LICENCE);

	/// <summary>
	/// 中文姓名
	/// </summary>
	public static readonly Regex CHINESE_NAME = Get(RegexPool.CHINESE_NAME);

	/// <summary>
	/// 先从Pattern池中查找正则对应的Regex，找不到则编译正则表达式并入池。
	/// </summary>
	/// <param name="regex">正则表达式</param>
	/// <param name="options">正则选项</param>
	/// <returns>Regex</returns>
	public static Regex Get(string regex, RegexOptions options = RegexOptions.None)
	{
		var key = GetKey(regex, options);
		return Pool.GetOrAdd(key, _ => new Regex(regex, options, TimeSpan.FromSeconds(1)));
	}

	/// <summary>
	/// 移除缓存
	/// </summary>
	/// <param name="regex">正则</param>
	/// <param name="options">选项</param>
	/// <returns>是否成功移除</returns>
	public static bool Remove(string regex, RegexOptions options = RegexOptions.None)
	{
		var key = GetKey(regex, options);
		return Pool.TryRemove(key, out _);
	}

	/// <summary>
	/// 清空缓存池
	/// </summary>
	public static void Clear()
	{
		Pool.Clear();
	}

	private static string GetKey(string regex, RegexOptions options)
	{
		return $"{regex}|{options}";
	}
}
