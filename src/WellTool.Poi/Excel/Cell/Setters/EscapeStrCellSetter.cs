using System.Text.RegularExpressions;

namespace WellTool.Poi.Excel.Cell.Setters;

/// <summary>
/// 字符串转义单元格设置器
/// 使用 _x005F前缀转义_xXXXX_，避免被decode的问题
/// </summary>
public class EscapeStrCellSetter : CharSequenceCellSetter
{
	private static readonly Regex UtfPattern = new Regex("_x[0-9A-Fa-f]{4}_", RegexOptions.Compiled);

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">值</param>
	public EscapeStrCellSetter(string value)
		: base(Escape(value))
	{
	}

	/// <summary>
	/// 使用 _x005F前缀转义_xXXXX_，避免被decode的问题
	/// </summary>
	/// <param name="value">被转义的字符串</param>
	/// <returns>转义后的字符串</returns>
	private static string Escape(string value)
	{
		if (string.IsNullOrEmpty(value) || !value.Contains("_x"))
		{
			return value;
		}

		return UtfPattern.Replace(value, "_x005F$0");
	}
}
