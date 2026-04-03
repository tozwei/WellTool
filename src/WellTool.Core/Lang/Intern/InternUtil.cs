namespace WellTool.Core.Lang.Intern;

/// <summary>
/// 驻留工具类
/// </summary>
public static class InternUtil
{
	private static readonly StringInterner _stringInterner = new();

	/// <summary>
	/// 驻留字符串
	/// </summary>
	/// <param name="str">原始字符串</param>
	/// <returns>驻留后的字符串</returns>
	public static string Intern(string str)
	{
		return _stringInterner.Intern(str);
	}

	/// <summary>
	/// 获取驻留器
	/// </summary>
	/// <typeparam name="T">类型</typeparam>
	/// <returns>驻留器</returns>
	public static IInterner<T> GetInterner<T>()
	{
		return new StringInterner() as IInterner<T>;
	}
}
