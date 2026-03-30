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
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    public ExcelReader(ExcelPackage package)
    {
        _package = package;
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadSheet(int sheetIndex)
    {
        try
        {
            var worksheet = _package.Workbook.Worksheets[sheetIndex];
            var result = new List<List<object?>>();
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
        catch (Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadSheet(string sheetName)
    {
        try
        {
            var worksheet = _package.Workbook.Worksheets[sheetName];
            var result = new List<List<object?>>();
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
        catch (Exception ex)
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
