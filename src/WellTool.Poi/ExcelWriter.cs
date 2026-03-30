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
/// Excel 写入器
/// </summary>
public class ExcelWriter : IDisposable
{
    /// <summary>
    /// Excel 包
    /// </summary>
    private readonly ExcelPackage _package;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="package">Excel 包</param>
    public ExcelWriter(ExcelPackage package)
    {
        _package = package;
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
        catch (Exception ex)
        {
            throw new POIException("创建 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">数据</param>
    public void Write(int sheetIndex, List<List<object?>> data)
    {
        try
        {
            var worksheet = _package.Workbook.Worksheets[sheetIndex];
            for (int row = 0; row < data.Count; row++)
            {
                var rowData = data[row];
                for (int col = 0; col < rowData.Count; col++)
                {
                    worksheet.Cells[row + 1, col + 1].Value = rowData[col];
                }
            }
        }
        catch (Exception ex)
        {
            throw new POIException("写入 Excel 数据失败", ex);
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
        catch (Exception ex)
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
