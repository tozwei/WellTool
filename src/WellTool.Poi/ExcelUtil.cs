// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.IO;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// Excel 工具类
/// </summary>
public class ExcelUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ExcelUtil Instance { get; } = new ExcelUtil();

    /// <summary>
    /// xls的ContentType
    /// </summary>
    public static readonly string XLS_CONTENT_TYPE = "application/vnd.ms-excel";

    /// <summary>
    /// xlsx的ContentType
    /// </summary>
    public static readonly string XLSX_CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    // ------------------------------------------------------------------------------------------------ getReader

    /// <summary>
    /// 获取Excel读取器，默认读取第一个sheet
    /// </summary>
    /// <param name="filePath">Excel文件路径</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(string filePath)
    {
        return GetReader(filePath, 0);
    }

    /// <summary>
    /// 获取Excel读取器
    /// </summary>
    /// <param name="filePath">Excel文件路径</param>
    /// <param name="sheetIndex">sheet序号，0表示第一个sheet</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(string filePath, int sheetIndex)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelReader(package, sheetIndex);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 读取器失败", ex);
        }
    }

    /// <summary>
    /// 获取Excel读取器
    /// </summary>
    /// <param name="filePath">Excel文件路径</param>
    /// <param name="sheetName">sheet名</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(string filePath, string sheetName)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelReader(package, sheetName);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 读取器失败", ex);
        }
    }

    /// <summary>
    /// 获取Excel读取器，默认读取第一个sheet
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(Stream stream)
    {
        return GetReader(stream, 0);
    }

    /// <summary>
    /// 获取Excel读取器
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <param name="sheetIndex">sheet序号，0表示第一个sheet</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(Stream stream, int sheetIndex)
    {
        try
        {
            var package = new ExcelPackage(stream);
            return new ExcelReader(package, sheetIndex);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 读取器失败", ex);
        }
    }

    /// <summary>
    /// 获取Excel读取器
    /// </summary>
    /// <param name="stream">Excel文件流</param>
    /// <param name="sheetName">sheet名</param>
    /// <returns>Excel读取器</returns>
    public static ExcelReader GetReader(Stream stream, string sheetName)
    {
        try
        {
            var package = new ExcelPackage(stream);
            return new ExcelReader(package, sheetName);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 读取器失败", ex);
        }
    }

    // ------------------------------------------------------------------------------------------------ getWriter

    /// <summary>
    /// 获得ExcelWriter，默认写出到第一个sheet
    /// </summary>
    /// <returns>ExcelWriter</returns>
    public static ExcelWriter GetWriter()
    {
        try
        {
            var package = new ExcelPackage();
            return new ExcelWriter(package);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }

    /// <summary>
    /// 获得ExcelWriter，默认写出到第一个sheet
    /// </summary>
    /// <param name="filePath">目标文件路径</param>
    /// <returns>ExcelWriter</returns>
    public static ExcelWriter GetWriter(string filePath)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelWriter(package);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }

    /// <summary>
    /// 获得ExcelWriter
    /// </summary>
    /// <param name="filePath">目标文件路径</param>
    /// <param name="sheetName">sheet表名</param>
    /// <returns>ExcelWriter</returns>
    public static ExcelWriter GetWriter(string filePath, string sheetName)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelWriter(package, sheetName);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }

    /// <summary>
    /// 获得ExcelWriter，默认写出到第一个sheet
    /// </summary>
    /// <param name="stream">目标流</param>
    /// <returns>ExcelWriter</returns>
    public static ExcelWriter GetWriter(Stream stream)
    {
        try
        {
            var package = new ExcelPackage(stream);
            return new ExcelWriter(package);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }

    /// <summary>
    /// 获得ExcelWriter
    /// </summary>
    /// <param name="stream">目标流</param>
    /// <param name="sheetName">sheet表名</param>
    /// <returns>ExcelWriter</returns>
    public static ExcelWriter GetWriter(Stream stream, string sheetName)
    {
        try
        {
            var package = new ExcelPackage(stream);
            return new ExcelWriter(package, sheetName);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }

    // ------------------------------------------------------------------------------------------------ getBigWriter

    /// <summary>
    /// 获得BigExcelWriter，用于处理大数据量
    /// </summary>
    /// <returns>BigExcelWriter</returns>
    public static BigExcelWriter GetBigWriter()
    {
        try
        {
            var package = new ExcelPackage();
            return new BigExcelWriter(package);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 BigExcelWriter 失败", ex);
        }
    }

    /// <summary>
    /// 获得BigExcelWriter，用于处理大数据量
    /// </summary>
    /// <param name="filePath">目标文件路径</param>
    /// <returns>BigExcelWriter</returns>
    public static BigExcelWriter GetBigWriter(string filePath)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new BigExcelWriter(package);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 BigExcelWriter 失败", ex);
        }
    }

    /// <summary>
    /// 获得BigExcelWriter，用于处理大数据量
    /// </summary>
    /// <param name="filePath">目标文件路径</param>
    /// <param name="sheetName">sheet表名</param>
    /// <returns>BigExcelWriter</returns>
    public static BigExcelWriter GetBigWriter(string filePath, string sheetName)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new BigExcelWriter(package, sheetName);
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 BigExcelWriter 失败", ex);
        }
    }

    /// <summary>
    /// 将Sheet列号变为列名
    /// </summary>
    /// <param name="index">列号, 从0开始</param>
    /// <returns>0-》A; 1-》B...26-》AA</returns>
    public static string IndexToColName(int index)
    {
        if (index < 0)
        {
            return null;
        }
        var colName = new System.Text.StringBuilder();
        do
        {
            if (colName.Length > 0)
            {
                index--;
            }
            int remainder = index % 26;
            colName.Append((char)('A' + remainder));
            index = (index - remainder) / 26;
        } while (index > 0);
        return new string(colName.ToString().Reverse().ToArray());
    }

    /// <summary>
    /// 根据表元的列名转换为列号
    /// </summary>
    /// <param name="colName">列名, 从A开始</param>
    /// <returns>A1-》0; B1-》1...AA1-》26</returns>
    public static int ColNameToIndex(string colName)
    {
        int length = colName.Length;
        int index = -1;
        for (int i = 0; i < length; i++)
        {
            char c = char.ToUpper(colName[i]);
            if (char.IsDigit(c))
            {
                break;
            }
            index = (index + 1) * 26 + (c - 'A');
        }
        return index;
    }

    /// <summary>
    /// 将Excel中地址标识符（例如A11，B5）等转换为行列表示
    /// </summary>
    /// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
    /// <returns>坐标点，x表示列，从0开始，y表示行，从0开始</returns>
    public static CellLocation ToLocation(string locationRef)
    {
        int x = ColNameToIndex(locationRef);
        int y = int.Parse(Regex.Match(locationRef, @"\d+").Value) - 1;
        return new CellLocation(x, y);
    }
}
