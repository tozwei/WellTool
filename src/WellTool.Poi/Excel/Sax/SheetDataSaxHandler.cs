using NPOI.SS.UserModel;
using System.Collections.Generic;
using WellTool.Poi.Excel.Sax.Handler;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// Sheet数据SAX处理器
/// </summary>
public class SheetDataSaxHandler
{
	/// <summary>
	/// 行处理器
	/// </summary>
	public IRowHandler RowHandler { get; set; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="rowHandler">行处理器</param>
	public SheetDataSaxHandler(IRowHandler rowHandler)
	{
		RowHandler = rowHandler;
	}
}
