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
using System.IO;
using System.Reflection;
using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// Excel 写入器
/// </summary>
public class ExcelWriter : IDisposable
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
    public ExcelWriter(ExcelPackage package)
    {
        _package = package;
        // 如果没有工作表，创建一个默认的
        if (_package.Workbook.Worksheets.Count == 0)
        {
            _currentWorksheet = _package.Workbook.Worksheets.Add("Sheet1");
        }
        else
        {
            _currentWorksheet = _package.Workbook.Worksheets[0];
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelWriter(ExcelPackage package, string sheetName)
    {
        _package = package;
        // 尝试获取指定名称的工作表，如果不存在则创建
        _currentWorksheet = _package.Workbook.Worksheets[sheetName];
        if (_currentWorksheet == null)
        {
            _currentWorksheet = _package.Workbook.Worksheets.Add(sheetName);
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public ExcelWriter(string filePath)
        : this(new ExcelPackage(new FileInfo(filePath)))
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelWriter(string filePath, string sheetName)
        : this(new ExcelPackage(new FileInfo(filePath)), sheetName)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    public ExcelWriter(Stream stream)
        : this(new ExcelPackage(stream))
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelWriter(Stream stream, string sheetName)
        : this(new ExcelPackage(stream), sheetName)
    {
    }

    /// <summary>
    /// 创建工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>工作表索引</returns>
    public int CreateSheet(string sheetName)
    {
        try
        {
            // 检查工作表是否已存在
            var existingWorksheet = _package.Workbook.Worksheets[sheetName];
            if (existingWorksheet != null)
            {
                // 工作表已存在，返回其索引
                for (int i = 0; i < _package.Workbook.Worksheets.Count; i++)
                {
                    if (_package.Workbook.Worksheets[i].Name == sheetName)
                    {
                        return i;
                    }
                }
                return 0;
            }
            
            // 工作表不存在，创建新的
            var worksheet = _package.Workbook.Worksheets.Add(sheetName);
            // 遍历工作表集合，找到新添加的工作表的索引
            for (int i = 0; i < _package.Workbook.Worksheets.Count; i++)
            {
                if (_package.Workbook.Worksheets[i].Name == sheetName)
                {
                    return i;
                }
            }
            return 0;
        }
        catch (System.Exception ex)
        {
            throw new POIException("创建 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 切换到指定的工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    public void SetCurrentSheet(int sheetIndex)
    {
        _currentWorksheet = _package.Workbook.Worksheets[sheetIndex];
    }

    /// <summary>
    /// 切换到指定的工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    public void SetCurrentSheet(string sheetName)
    {
        _currentWorksheet = _package.Workbook.Worksheets[sheetName];
    }

    /// <summary>
    /// 写入数据到当前工作表
    /// </summary>
    /// <param name="data">数据</param>
    public void Write(List<List<object?>> data)
    {
        Write(_currentWorksheet, data);
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">数据</param>
    public void Write(int sheetIndex, List<List<object?>> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">数据</param>
    public void Write(string sheetName, List<List<object?>> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="data">数据</param>
    private void Write(ExcelWorksheet worksheet, List<List<object?>> data)
    {
        try
        {
            for (int row = 0; row < data.Count; row++)
            {
                var rowData = data[row];
                for (int col = 0; col < rowData.Count; col++)
                {
                    worksheet.Cells[row + 1, col + 1].Value = rowData[col];
                }
            }
        }
        catch (System.Exception ex)
        {
            throw new POIException("写入 Excel 数据失败", ex);
        }
    }

    /// <summary>
    /// 写入表头到当前工作表
    /// </summary>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(List<string> headers)
    {
        WriteHeader(_currentWorksheet, headers);
    }

    /// <summary>
    /// 写入表头到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(int sheetIndex, List<string> headers)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        WriteHeader(worksheet, headers);
    }

    /// <summary>
    /// 写入表头到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(string sheetName, List<string> headers)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        WriteHeader(worksheet, headers);
    }

    /// <summary>
    /// 写入表头到指定工作表
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="headers">表头数据</param>
    private void WriteHeader(ExcelWorksheet worksheet, List<string> headers)
    {
        try
        {
            for (int col = 0; col < headers.Count; col++)
            {
                worksheet.Cells[1, col + 1].Value = headers[col];
                // 设置表头样式
                using (var range = worksheet.Cells[1, col + 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }
            }
        }
        catch (System.Exception ex)
        {
            throw new POIException("写入 Excel 表头失败", ex);
        }
    }

    /// <summary>
    /// 写入字典列表到当前工作表
    /// </summary>
    /// <param name="data">字典列表</param>
    public void Write(List<Dictionary<string, object?>> data)
    {
        Write(_currentWorksheet, data);
    }

    /// <summary>
    /// 写入字典列表到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">字典列表</param>
    public void Write(int sheetIndex, List<Dictionary<string, object?>> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入字典列表到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">字典列表</param>
    public void Write(string sheetName, List<Dictionary<string, object?>> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入字典列表到指定工作表
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="data">字典列表</param>
    private void Write(ExcelWorksheet worksheet, List<Dictionary<string, object?>> data)
    {
        try
        {
            if (data.Count == 0)
            {
                return;
            }

            // 提取表头
            var headers = data[0].Keys.ToList();
            WriteHeader(worksheet, headers);

            // 写入数据
            for (int row = 0; row < data.Count; row++)
            {
                var rowData = data[row];
                for (int col = 0; col < headers.Count; col++)
                {
                    var key = headers[col];
                    if (rowData.TryGetValue(key, out var value))
                    {
                        worksheet.Cells[row + 2, col + 1].Value = value;
                    }
                }
            }

            // 自动调整列宽
            AutoFitColumns(worksheet, headers.Count);
        }
        catch (System.Exception ex)
        {
            throw new POIException("写入 Excel 数据失败", ex);
        }
    }

    /// <summary>
    /// 写入对象列表到当前工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="data">对象列表</param>
    public void Write<T>(List<T> data)
    {
        Write(_currentWorksheet, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">对象列表</param>
    public void Write<T>(int sheetIndex, List<T> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">对象列表</param>
    public void Write<T>(string sheetName, List<T> data)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        Write(worksheet, data);
    }

    /// <summary>
    /// 写入对象列表到当前工作表（别名方法）
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="data">对象列表</param>
    public void WriteBeans<T>(List<T> data)
    {
        Write(data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表（别名方法）
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">对象列表</param>
    public void WriteBeans<T>(int sheetIndex, List<T> data)
    {
        Write(sheetIndex, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表（别名方法）
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">对象列表</param>
    public void WriteBeans<T>(string sheetName, List<T> data)
    {
        Write(sheetName, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="worksheet">工作表</param>
    /// <param name="data">对象列表</param>
    private void Write<T>(ExcelWorksheet worksheet, List<T> data)
    {
        try
        {
            if (data.Count == 0)
            {
                return;
            }

            // 获取对象的属性
            var properties = typeof(T).GetProperties();
            var headers = properties.Select(p => p.Name).ToList();
            WriteHeader(worksheet, headers);

            // 写入数据
            for (int row = 0; row < data.Count; row++)
            {
                var item = data[row];
                for (int col = 0; col < properties.Length; col++)
                {
                    var property = properties[col];
                    var value = property.GetValue(item);
                    worksheet.Cells[row + 2, col + 1].Value = value;
                }
            }

            // 自动调整列宽
            AutoFitColumns(worksheet, headers.Count);
        }
        catch (System.Exception ex)
        {
            throw new POIException("写入 Excel 数据失败", ex);
        }
    }

    /// <summary>
    /// 写入DataTable到当前工作表
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    public void Write(DataTable dataTable)
    {
        Write(_currentWorksheet, dataTable);
    }

    /// <summary>
    /// 写入DataTable到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="dataTable">DataTable</param>
    public void Write(int sheetIndex, DataTable dataTable)
    {
        var worksheet = _package.Workbook.Worksheets[sheetIndex];
        Write(worksheet, dataTable);
    }

    /// <summary>
    /// 写入DataTable到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="dataTable">DataTable</param>
    public void Write(string sheetName, DataTable dataTable)
    {
        var worksheet = _package.Workbook.Worksheets[sheetName];
        Write(worksheet, dataTable);
    }

    /// <summary>
    /// 写入DataTable到指定工作表
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="dataTable">DataTable</param>
    private void Write(ExcelWorksheet worksheet, DataTable dataTable)
    {
        try
        {
            // 写入表头
            var headers = dataTable.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToList();
            WriteHeader(worksheet, headers);

            // 写入数据
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                var dataRow = dataTable.Rows[row];
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[row + 2, col + 1].Value = dataRow[col];
                }
            }

            // 自动调整列宽
            AutoFitColumns(worksheet, headers.Count);
        }
        catch (System.Exception ex)
        {
            throw new POIException("写入 Excel 数据失败", ex);
        }
    }

    /// <summary>
    /// 自动调整列宽
    /// </summary>
    /// <param name="worksheet">工作表</param>
    /// <param name="columnCount">列数</param>
    private void AutoFitColumns(ExcelWorksheet worksheet, int columnCount)
    {
        for (int col = 1; col <= columnCount; col++)
        {
            worksheet.Column(col).AutoFit();
        }
    }

    /// <summary>
    /// 保存 Excel 文件
    /// </summary>
    public void Save()
    {
        try
        {
            _package.Save();
        }
        catch (System.Exception ex)
        {
            throw new POIException("保存 Excel 文件失败", ex);
        }
    }

    /// <summary>
    /// 保存 Excel 到指定路径
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public void SaveAs(string filePath)
    {
        try
        {
            _package.SaveAs(new FileInfo(filePath));
        }
        catch (System.Exception ex)
        {
            throw new POIException("保存 Excel 文件失败", ex);
        }
    }

    /// <summary>
    /// 保存 Excel 到指定流
    /// </summary>
    /// <param name="stream">流</param>
    public void SaveAs(Stream stream)
    {
        try
        {
            _package.SaveAs(stream);
        }
        catch (System.Exception ex)
        {
            throw new POIException("保存 Excel 文件失败", ex);
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
