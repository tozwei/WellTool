using System.IO;
using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// 大数据量Excel写入器
/// </summary>
public class BigExcelWriter : ExcelWriter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel包</param>
    public BigExcelWriter(ExcelPackage package)
        : base(package)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel包</param>
    /// <param name="sheetName">sheet名称</param>
    public BigExcelWriter(ExcelPackage package, string sheetName)
        : base(package, sheetName)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public BigExcelWriter(string filePath)
        : base(new ExcelPackage(new FileInfo(filePath)))
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="sheetName">sheet名称</param>
    public BigExcelWriter(string filePath, string sheetName)
        : base(new ExcelPackage(new FileInfo(filePath)), sheetName)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    public BigExcelWriter(Stream stream)
        : base(new ExcelPackage(stream))
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="sheetName">sheet名称</param>
    public BigExcelWriter(Stream stream, string sheetName)
        : base(new ExcelPackage(stream), sheetName)
    {
    }
}