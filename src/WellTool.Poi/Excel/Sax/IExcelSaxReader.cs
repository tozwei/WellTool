using System.IO;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// SAX读取Excel接口
/// </summary>
public interface IExcelSaxReader
{
	/// <summary>
	/// Sheet RID前缀
	/// </summary>
	string RidPrefix { get; }

	/// <summary>
	/// 读取Excel
	/// </summary>
	/// <param name="file">文件</param>
	/// <param name="idOrRidOrSheetName">Sheet ID或名称</param>
	/// <returns>this</returns>
	IExcelSaxReader Read(FileInfo file, string idOrRidOrSheetName);

	/// <summary>
	/// 读取Excel
	/// </summary>
	/// <param name="inStream">流</param>
	/// <param name="idOrRidOrSheetName">Sheet ID或名称</param>
	/// <returns>this</returns>
	IExcelSaxReader Read(Stream inStream, string idOrRidOrSheetName);
}
