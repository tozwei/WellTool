namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// 单元格数据类型
/// </summary>
public enum CellDataType
{
	/// <summary>
	/// 空值
	/// </summary>
	Null,
	/// <summary>
	/// 布尔
	/// </summary>
	Bool,
	/// <summary>
	/// 错误
	/// </summary>
	Error,
	/// <summary>
	/// 公式
	/// </summary>
	Formula,
	/// <summary>
	/// 内联字符串
	/// </summary>
	InlineStr,
	/// <summary>
	/// 共享字符串索引
	/// </summary>
	SSTIndex,
	/// <summary>
	/// 数字
	/// </summary>
	Number,
	/// <summary>
	/// 日期
	/// </summary>
	Date
}

/// <summary>
/// 单元格数据类型扩展
/// </summary>
public static class CellDataTypeExtensions
{
	/// <summary>
	/// 根据类型字符串获取单元格数据类型
	/// </summary>
	public static CellDataType Of(string type)
	{
		return type switch
		{
			"b" => CellDataType.Bool,
			"e" => CellDataType.Error,
			"f" => CellDataType.Formula,
			"inlineStr" => CellDataType.InlineStr,
			"s" => CellDataType.SSTIndex,
			"str" => CellDataType.Formula,
			_ => CellDataType.Number
		};
	}
}
