using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel基础类，用于抽象ExcelWriter和ExcelReader中共用部分的对象和方法
/// </summary>
/// <typeparam name="T">子类类型，用于返回this</typeparam>
public abstract class ExcelBase<T> : IDisposable where T : ExcelBase<T>
{
	/// <summary>
	/// 是否被关闭
	/// </summary>
	protected bool _isClosed;
	/// <summary>
	/// 目标文件
	/// </summary>
	protected FileInfo? _destFile;
	/// <summary>
	/// 工作簿
	/// </summary>
	protected IWorkbook? _workbook;
	/// <summary>
	/// Excel中对应的Sheet
	/// </summary>
	protected ISheet? _sheet;
	/// <summary>
	/// 标题行别名
	/// </summary>
	protected Dictionary<string, string>? _headerAlias;

	/// <summary>
	/// 获取Workbook
	/// </summary>
	/// <returns>Workbook</returns>
	public IWorkbook? GetWorkbook() => _workbook;

	/// <summary>
	/// 返回工作簿表格数
	/// </summary>
	/// <returns>工作簿表格数</returns>
	public int GetSheetCount() => _workbook?.NumberOfSheets ?? 0;

	/// <summary>
	/// 获取此工作簿所有Sheet表
	/// </summary>
	/// <returns>sheet表列表</returns>
	public List<ISheet> GetSheets()
	{
		var result = new List<ISheet>();
		if (_workbook == null) return result;

		var totalSheet = GetSheetCount();
		for (int i = 0; i < totalSheet; i++)
		{
			result.Add(_workbook.GetSheetAt(i));
		}
		return result;
	}

	/// <summary>
	/// 获取表名列表
	/// </summary>
	/// <returns>表名列表</returns>
	public List<string> GetSheetNames()
	{
		var result = new List<string>();
		if (_workbook == null) return result;

		var totalSheet = _workbook.NumberOfSheets;
		for (int i = 0; i < totalSheet; i++)
		{
			result.Add(_workbook.GetSheetAt(i).SheetName);
		}
		return result;
	}

	/// <summary>
	/// 获取当前Sheet
	/// </summary>
	/// <returns>Sheet</returns>
	public ISheet? GetSheet() => _sheet;

	/// <summary>
	/// 重命名当前sheet
	/// </summary>
	/// <param name="newName">新名字</param>
	/// <returns>this</returns>
	public T RenameSheet(string newName)
	{
		if (_workbook != null && _sheet != null)
		{
			_workbook.SetSheetName(_workbook.GetSheetIndex(_sheet), newName);
		}
		return (T)this;
	}

	/// <summary>
	/// 自定义需要读取或写出的Sheet，如果给定的sheet不存在，创建之
	/// </summary>
	/// <param name="sheetName">sheet名</param>
	/// <returns>this</returns>
	public T SetSheet(string sheetName)
	{
		return SetSheet(WorkbookUtil.GetOrCreateSheet(_workbook!, sheetName));
	}

	/// <summary>
	/// 自定义需要读取或写出的Sheet，如果给定的sheet不存在，创建之
	/// </summary>
	/// <param name="sheetIndex">sheet序号，从0开始计数</param>
	/// <returns>this</returns>
	public T SetSheet(int sheetIndex)
	{
		return SetSheet(_workbook!.GetSheetAt(sheetIndex));
	}

	/// <summary>
	/// 设置自定义Sheet
	/// </summary>
	/// <param name="sheet">自定义sheet</param>
	/// <returns>this</returns>
	public T SetSheet(ISheet sheet)
	{
		_sheet = sheet;
		return (T)this;
	}

	/// <summary>
	/// 获取指定坐标单元格，单元格不存在时返回null
	/// </summary>
	/// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
	/// <returns>Cell</returns>
	public ICell? GetCell(string locationRef)
	{
		var cellLocation = ExcelUtil.ToLocation(locationRef);
		return GetCell(cellLocation.X, cellLocation.Y);
	}

	/// <summary>
	/// 获取指定坐标单元格，单元格不存在时返回null
	/// </summary>
	/// <param name="x">X坐标，从0计数，即列号</param>
	/// <param name="y">Y坐标，从0计数，即行号</param>
	/// <returns>Cell</returns>
	public ICell? GetCell(int x, int y)
	{
		return GetCell(x, y, false);
	}

	/// <summary>
	/// 获取或创建指定坐标单元格
	/// </summary>
	/// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
	/// <returns>Cell</returns>
	public ICell? GetOrCreateCell(string locationRef)
	{
		var cellLocation = ExcelUtil.ToLocation(locationRef);
		return GetOrCreateCell(cellLocation.X, cellLocation.Y);
	}

	/// <summary>
	/// 获取或创建指定坐标单元格
	/// </summary>
	/// <param name="x">X坐标，从0计数，即列号</param>
	/// <param name="y">Y坐标，从0计数，即行号</param>
	/// <returns>Cell</returns>
	public ICell? GetOrCreateCell(int x, int y)
	{
		return GetCell(x, y, true);
	}

	/// <summary>
	/// 获取指定坐标单元格，如果isCreateIfNotExist为false，则在单元格不存在时返回null
	/// </summary>
	/// <param name="locationRef">单元格地址标识符</param>
	/// <param name="isCreateIfNotExist">单元格不存在时是否创建</param>
	/// <returns>Cell</returns>
	public ICell? GetCell(string locationRef, bool isCreateIfNotExist)
	{
		var cellLocation = ExcelUtil.ToLocation(locationRef);
		return GetCell(cellLocation.X, cellLocation.Y, isCreateIfNotExist);
	}

	/// <summary>
	/// 获取指定坐标单元格，如果isCreateIfNotExist为false，则在单元格不存在时返回null
	/// </summary>
	/// <param name="x">X坐标，从0计数</param>
	/// <param name="y">Y坐标，从0计数</param>
	/// <param name="isCreateIfNotExist">单元格不存在时是否创建</param>
	/// <returns>Cell</returns>
	public ICell? GetCell(int x, int y, bool isCreateIfNotExist)
	{
		if (_sheet == null) return null;

		var row = isCreateIfNotExist ? RowUtil.GetOrCreateRow(_sheet, y) : _sheet.GetRow(y);
		if (row != null)
		{
			return isCreateIfNotExist ? row.CreateCell(x) : row.GetCell(x);
		}
		return null;
	}

	/// <summary>
	/// 获取或者创建行
	/// </summary>
	/// <param name="y">Y坐标，从0计数，即行号</param>
	/// <returns>Row</returns>
	public IRow? GetOrCreateRow(int y)
	{
		return _sheet != null ? RowUtil.GetOrCreateRow(_sheet, y) : null;
	}

	/// <summary>
	/// 获取总行数
	/// </summary>
	/// <returns>行数</returns>
	public int GetRowCount()
	{
		return _sheet?.LastRowNum + 1 ?? 0;
	}

	/// <summary>
	/// 获取有记录的行数
	/// </summary>
	/// <returns>行数</returns>
	public int GetPhysicalRowCount()
	{
		return _sheet?.PhysicalNumberOfRows ?? 0;
	}

	/// <summary>
	/// 获取第一行总列数
	/// </summary>
	/// <returns>列数</returns>
	public int GetColumnCount()
	{
		return GetColumnCount(0);
	}

	/// <summary>
	/// 获取总列数
	/// </summary>
	/// <param name="rowNum">行号</param>
	/// <returns>列数，-1表示获取失败</returns>
	public int GetColumnCount(int rowNum)
	{
		var row = _sheet?.GetRow(rowNum);
		if (row != null)
		{
			return row.LastCellNum;
		}
		return -1;
	}

	/// <summary>
	/// 判断是否为xlsx格式的Excel表（Excel07格式）
	/// </summary>
	/// <returns>是否为xlsx格式</returns>
	public bool IsXlsx()
	{
		return _workbook is NPOI.XSSF.UserModel.XSSFWorkbook;
	}

	/// <summary>
	/// 关闭工作簿
	/// </summary>
	public virtual void Close()
	{
		_workbook?.Close();
		_sheet = null!;
		_workbook = null!;
		_isClosed = true;
	}

	/// <summary>
	/// 获得标题行的别名Map
	/// </summary>
	/// <returns>别名Map</returns>
	public Dictionary<string, string>? GetHeaderAlias() => _headerAlias;

	/// <summary>
	/// 设置标题行的别名Map
	/// </summary>
	/// <param name="headerAlias">别名Map</param>
	/// <returns>this</returns>
	public T SetHeaderAlias(Dictionary<string, string> headerAlias)
	{
		_headerAlias = headerAlias;
		return (T)this;
	}

	/// <summary>
	/// 增加标题别名
	/// </summary>
	/// <param name="header">标题</param>
	/// <param name="alias">别名</param>
	/// <returns>this</returns>
	public T AddHeaderAlias(string header, string alias)
	{
		_headerAlias ??= new Dictionary<string, string>();
		_headerAlias[header] = alias;
		return (T)this;
	}

	/// <summary>
	/// 去除标题别名
	/// </summary>
	/// <param name="header">标题</param>
	/// <returns>this</returns>
	public T RemoveHeaderAlias(string header)
	{
		_headerAlias?.Remove(header);
		return (T)this;
	}

	/// <summary>
	/// 清空标题别名
	/// </summary>
	/// <returns>this</returns>
	public T ClearHeaderAlias()
	{
		_headerAlias = null!;
		return (T)this;
	}

	/// <summary>
	/// 执行与释放或重置非托管资源关联的应用程序定义的任务
	/// </summary>
	public void Dispose()
	{
		Close();
	}
}
