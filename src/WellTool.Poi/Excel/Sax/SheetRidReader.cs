using System.Collections.Generic;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// Sheet RID读取器
/// </summary>
public class SheetRidReader
{
	private readonly Dictionary<string, int> _nameToRid = new Dictionary<string, int>();
	private readonly Dictionary<int, int> _indexToRid = new Dictionary<int, int>();

	/// <summary>
	/// 根据名称获取RID
	/// </summary>
	public int? GetRidByName(string sheetName)
	{
		return _nameToRid.TryGetValue(sheetName, out var rid) ? rid : null;
	}

	/// <summary>
	/// 根据名称获取RID（从0开始）
	/// </summary>
	public int? GetRidByNameBase0(string sheetName)
	{
		var rid = GetRidByName(sheetName);
		return rid.HasValue ? rid.Value - 1 : null;
	}

	/// <summary>
	/// 根据索引获取RID
	/// </summary>
	public int? GetRidByIndex(int index)
	{
		return _indexToRid.TryGetValue(index, out var rid) ? rid : null;
	}

	/// <summary>
	/// 根据索引获取RID（从0开始）
	/// </summary>
	public int? GetRidByIndexBase0(int index)
	{
		var rid = GetRidByIndex(index);
		return rid.HasValue ? rid.Value - 1 : null;
	}

	/// <summary>
	/// 获取所有Sheet名称
	/// </summary>
	public List<string> GetSheetNames()
	{
		return new List<string>(_nameToRid.Keys);
	}
}
