namespace WellTool.Core.Lang;

/// <summary>
/// 常用正则表达式字符串常量池
/// </summary>
public static class RegexPool
{
	/// <summary>
	/// 英文字母 、数字和下划线
	/// </summary>
	public const string GENERAL = @"^\w+$";

	/// <summary>
	/// 数字
	/// </summary>
	public const string NUMBERS = @"\d+";

	/// <summary>
	/// 字母
	/// </summary>
	public const string WORD = @"[a-zA-Z]+";

	/// <summary>
	/// 单个中文汉字
	/// </summary>
	public const string CHINESE = @"[\u2E80-\u2EFF\u2F00-\u2FDF\u31C0-\u31EF\u3400-\u4DBF\u4E00-\u9FFF\uF900-\uFAFF]";

	/// <summary>
	/// 中文汉字
	/// </summary>
	public const string CHINESES = CHINESE + "+";

	/// <summary>
	/// 分组
	/// </summary>
	public const string GROUP_VAR = @"\$(\d+)";

	/// <summary>
	/// IP v4
	/// </summary>
	public const string IPV4 = @"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)$";

	/// <summary>
	/// IP v6
	/// </summary>
	public const string IPV6 = @"(([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]+|::(ffff(:0{1,4})?:)?((25[0-5]|(2[0-4]|1?[0-9])?[0-9])\.){3}(25[0-5]|(2[0-4]|1?[0-9])?[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1?[0-9])?[0-9])\.){3}(25[0-5]|(2[0-4]|1?[0-9])?[0-9]))";

	/// <summary>
	/// 货币
	/// </summary>
	public const string MONEY = @"^(\d+(?:\.\d+)?)$";

	/// <summary>
	/// 邮件，符合RFC 5322规范
	/// </summary>
	public const string EMAIL = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\])";

	/// <summary>
	/// 规则同EMAIL，添加了对中文的支持
	/// </summary>
	public const string EMAIL_WITH_CHINESE = @"(?:[a-z0-9\u4e00-\u9fa5!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9\u4e00-\u9fa5!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9\u4e00-\u9fa5](?:[a-z0-9\u4e00-\u9fa5-]*[a-z0-9\u4e00-\u9fa5])?\.)+[a-z0-9\u4e00-\u9fa5](?:[a-z0-9\u4e00-\u9fa5-]*[a-z0-9\u4e00-\u9fa5])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\])";

	/// <summary>
	/// 移动电话
	/// </summary>
	public const string MOBILE = @"(?:0|86|\+86)?1[3-9]\d{9}";

	/// <summary>
	/// 中国香港移动电话
	/// </summary>
	public const string MOBILE_HK = @"(?:0|852|\+852)?\d{8}";

	/// <summary>
	/// 中国台湾移动电话
	/// </summary>
	public const string MOBILE_TW = @"(?:0|886|\+886)?(?:-)09\d{8}";

	/// <summary>
	/// 中国澳门移动电话
	/// </summary>
	public const string MOBILE_MO = @"(?:0|853|\+853)?(?:-)6\d{7}";

	/// <summary>
	/// 座机号码
	/// </summary>
	public const string TEL = @"(010|02\d|0[3-9]\d{2})-?(\d{6,8})";

	/// <summary>
	/// 座机号码+400+800电话
	/// </summary>
	public const string TEL_400_800 = @"0\d{2,3}[\- ]?[0-9]\d{6,7}|[48]00[\- ]?[0-9]\d{2}[\- ]?\d{4}";

	/// <summary>
	/// 18位身份证号码
	/// </summary>
	public const string CITIZEN_ID = @"[1-9]\d{5}[1-2]\d{3}((0\d)|(1[0-2]))(([012]\d)|3[0-1])\d{3}(\d|X|x)";

	/// <summary>
	/// 邮编，兼容港澳台
	/// </summary>
	public const string ZIP_CODE = @"^(0[1-7]|1[0-356]|2[0-7]|3[0-6]|4[0-7]|5[0-7]|6[0-7]|7[0-5]|8[0-9]|9[0-8])\d{4}|99907[78]$";

	/// <summary>
	/// 生日
	/// </summary>
	public const string BIRTHDAY = @"^(\d{2,4})([/\-.年]?)(\d{1,2})([/\-.月]?)(\d{1,2})日?$";

	/// <summary>
	/// URL
	/// </summary>
	public const string URL = @"[a-zA-Z]+://[\w-+&@#/%?=~_|!:,.;]*[\w-+&@#/%=~_|]";

	/// <summary>
	/// Http URL
	/// </summary>
	public const string URL_HTTP = @"(https?|ftp|file)://[\w-+&@#/%?=~_|!:,.;]*[\w-+&@#/%=~_|]";

	/// <summary>
	/// 中文字、英文字母、数字和下划线
	/// </summary>
	public const string GENERAL_WITH_CHINESE = @"^[\u4E00-\u9FFF\w]+$";

	/// <summary>
	/// UUID
	/// </summary>
	public const string UUID = @"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$";

	/// <summary>
	/// 不带横线的UUID
	/// </summary>
	public const string UUID_SIMPLE = @"^[0-9a-fA-F]{32}$";

	/// <summary>
	/// MAC地址正则
	/// </summary>
	public const string MAC_ADDRESS = @"((?:[a-fA-F0-9]{1,2}[:-]){5}[a-fA-F0-9]{1,2})|((?:[a-fA-F0-9]{1,4}[.]){2}[a-fA-F0-9]{1,4})|[a-fA-F0-9]{12}|0x(\d{12}).+ETHER";

	/// <summary>
	/// 16进制字符串
	/// </summary>
	public const string HEX = @"^[a-fA-F0-9]+$";

	/// <summary>
	/// 时间正则
	/// </summary>
	public const string TIME = @"\d{1,2}[:时]\d{1,2}([:分]\d{1,2})?秒?";

	/// <summary>
	/// 中国车牌号码（兼容新能源车牌）
	/// </summary>
	public const string PLATE_NUMBER = @"^(([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z](([0-9]{5}[ABCDEFGHJK])|([ABCDEFGHJK]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领]\d{3}\d{1,3}[领])|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z][A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳使领]))$";

	/// <summary>
	/// 统一社会信用代码
	/// </summary>
	public const string CREDIT_CODE = @"^[0-9A-HJ-NPQRTUWXY]{2}\d{6}[0-9A-HJ-NPQRTUWXY]{10}$";

	/// <summary>
	/// 车架号（车辆识别代号）
	/// </summary>
	public const string CAR_VIN = @"^[A-HJ-NPR-Z0-9]{8}[X0-9]([A-HJ-NPR-Z0-9]{3}\d{5}|[A-HJ-NPR-Z0-9]{5}\d{3})$";

	/// <summary>
	/// 驾驶证
	/// </summary>
	public const string CAR_DRIVING_LICENCE = @"^[0-9]{12}$";

	/// <summary>
	/// 中文姓名
	/// </summary>
	public const string CHINESE_NAME = @"^[\u3400-\u9FFF·]{2,60}$";
}
