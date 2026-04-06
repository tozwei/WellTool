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

using System.Collections.Generic;
using System.Data;
using System.Reflection;
using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// Excel 读取器
/// </summary>
public class ExcelReader : IDisposable
{
    /// <summary>
    /// Excel 包
    /// </summary>
    private readonly ExcelPackage _package;

    /// <summary>
    /// 当前工作表
    /// </summary>
    private ExcelWorksheet _currentWorksheet;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    public ExcelReader(ExcelPackage package)
        : this(package, 0)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    /// <param name="sheetIndex">工作表索引</param>
    public ExcelReader(ExcelPackage package, int sheetIndex)
    {
        _package = package;
        _currentWorksheet = _package.Workbook.Worksheets[sheetIndex];
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelReader(ExcelPackage package, string sheetName)
    {
        _package = package;
        _currentWorksheet = _package.Workbook.Worksheets[sheetName];
    }

    /// <summary>
    /// 读取所有数据
    /// </summary>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadAll()
    {
        return Read();
    }

    /// <summary>
    /// 读取当前工作表的数据
    /// </summary>
    /// <returns>数据列表</returns>
    public List<List<object?>> Read()
    {
        return ReadSheet(_currentWorksheet);
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadSheet(int sheetIndex)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        return ReadSheet(worksheet);
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadSheet(string sheetName)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        return ReadSheet(worksheet);
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <returns>数据列表</returns>
    private List<List<object?>> ReadSheet(ExcelWorksheet worksheet)
    {
        try
        {
            var result = new List<List<object?>>();
            if (worksheet.Dimension == null)
            {
                return result;
            }
            for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
            {
                var rowData = new List<object?>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    rowData.Add(worksheet.Cells[row, col].Value);
                }
                result.Add(rowData);
            }
            return result;
        }
        catch (System.Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 读取当前工作表的数据为字典列表
    /// </summary>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(int headerRowIndex = 0)
    {
        return ReadAsDictionaryList(_currentWorksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为字典列表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(int sheetIndex, int headerRowIndex = 0)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        return ReadAsDictionaryList(worksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为字典列表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(string sheetName, int headerRowIndex = 0)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        return ReadAsDictionaryList(worksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为字典列表
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    private List<Dictionary<string, object?>> ReadAsDictionaryList(ExcelWorksheet worksheet, int headerRowIndex = 0)
    {
        try
        {
            var result = new List<Dictionary<string, object?>>();
            if (worksheet.Dimension == null)
            {
                return result;
            }

            // 读取表头
            var headers = new List<string>();
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                var headerValue = worksheet.Cells[headerRowIndex + 1, col].Value;
                headers.Add(headerValue?.ToString() ?? string.Empty);
            }

            // 读取数据行
            for (int row = headerRowIndex + 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var rowData = new Dictionary<string, object?>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    if (col <= headers.Count)
                    {
                        rowData[headers[col - 1]] = worksheet.Cells[row, col].Value;
                    }
                }
                result.Add(rowData);
            }
            return result;
        }
        catch (System.Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 读取当前工作表的数据为DataTable
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>DataTable</returns>
    public DataTable ReadAsDataTable(string tableName = "Sheet1", int headerRowIndex = 0)
    {
        return ReadAsDataTable(_currentWorksheet, tableName, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为DataTable
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="tableName">表名</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>DataTable</returns>
    public DataTable ReadAsDataTable(int sheetIndex, string tableName = "Sheet1", int headerRowIndex = 0)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        return ReadAsDataTable(worksheet, tableName, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为DataTable
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="tableName">表名</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>DataTable</returns>
    public DataTable ReadAsDataTable(string sheetName, string tableName = "Sheet1", int headerRowIndex = 0)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        return ReadAsDataTable(worksheet, tableName, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为DataTable
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="tableName">表名</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>DataTable</returns>
    private DataTable ReadAsDataTable(ExcelWorksheet worksheet, string tableName = "Sheet1", int headerRowIndex = 0)
    {
        try
        {
            var dataTable = new DataTable(tableName);
            if (worksheet.Dimension == null)
            {
                return dataTable;
            }

            // 读取表头
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                var headerValue = worksheet.Cells[headerRowIndex + 1, col].Value;
                dataTable.Columns.Add(headerValue?.ToString() ?? $"Column{col}");
            }

            // 读取数据行
            for (int row = headerRowIndex + 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var dataRow = dataTable.NewRow();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    if (col <= dataTable.Columns.Count)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Value;
                    }
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        catch (System.Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 读取当前工作表的数据为指定类型的列表
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>指定类型的列表</returns>
    public List<T> ReadAsList<T>(int headerRowIndex = 0) where T : new()
    {
        return ReadAsList<T>(_currentWorksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为指定类型的列表
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>指定类型的列表</returns>
    public List<T> ReadAsList<T>(int sheetIndex, int headerRowIndex = 0) where T : new()
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        return ReadAsList<T>(worksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为指定类型的列表
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>指定类型的列表</returns>
    public List<T> ReadAsList<T>(string sheetName, int headerRowIndex = 0) where T : new()
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        return ReadAsList<T>(worksheet, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为指定类型的列表
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="worksheet">工作表</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>指定类型的列表</returns>
    private List<T> ReadAsList<T>(ExcelWorksheet worksheet, int headerRowIndex = 0) where T : new()
    {
        try
        {
            var result = new List<T>();
            if (worksheet.Dimension == null)
            {
                return result;
            }

            // 读取表头
            var headers = new List<string>();
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                var headerValue = worksheet.Cells[headerRowIndex + 1, col].Value;
                headers.Add(headerValue?.ToString() ?? string.Empty);
            }

            // 获取类型的属性
            var properties = typeof(T).GetProperties();

            // 读取数据行
            for (int row = headerRowIndex + 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var item = new T();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    if (col <= headers.Count)
                    {
                        var header = headers[col - 1];
                        var property = properties.FirstOrDefault(p => p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));
                        if (property != null)
                        {
                            var cellValue = worksheet.Cells[row, col].Value;
                            if (cellValue != null)
                            {
                                try
                                {
                                    var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);
                                    property.SetValue(item, convertedValue);
                                }
                                catch
                                {
                                    // 类型转换失败，跳过
                                }
                            }
                        }
                    }
                }
                result.Add(item);
            }
            return result;
        }
        catch (System.Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _package.Dispose();
    }
}
