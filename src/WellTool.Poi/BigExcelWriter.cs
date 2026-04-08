using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace WellTool.Poi;

/// <summary>
/// 大数据量Excel写入器
/// </summary>
public class BigExcelWriter : ExcelWriter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public BigExcelWriter(string filePath)
        : base(filePath)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="sheetName">sheet名称</param>
    public BigExcelWriter(string filePath, string sheetName)
        : base(filePath)
    {
        SetCurrentSheet(sheetName);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    public BigExcelWriter(Stream stream)
        : base(stream)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="sheetName">sheet名称</param>
    public BigExcelWriter(Stream stream, string sheetName)
        : base(stream)
    {
        SetCurrentSheet(sheetName);
    }
}