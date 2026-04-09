// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using MiniExcelLibs;

namespace WellTool.Poi;

/// <summary>
/// Excel 写入器
/// </summary>
public class ExcelWriter : IDisposable
{
    private readonly string _filePath;
    private readonly Stream _stream;
    private readonly bool _isStreamMode;
    private Dictionary<string, object> _sheets = new();
    private string _currentSheetName = "Sheet1";

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public ExcelWriter(string filePath)
    {
        _filePath = filePath;
        _isStreamMode = false;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    public ExcelWriter(Stream stream)
    {
        _stream = stream;
        _isStreamMode = true;
    }

    /// <summary>
    /// 创建工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>工作表索引</returns>
    public int CreateSheet(string sheetName)
    {
        _currentSheetName = sheetName;
        return _sheets.Count;
    }

    /// <summary>
    /// 切换到指定的工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    public void SetCurrentSheet(int sheetIndex)
    {
        var keys = _sheets.Keys.ToList();
        if (sheetIndex >= 0 && sheetIndex < keys.Count)
        {
            _currentSheetName = keys[sheetIndex];
        }
    }

    /// <summary>
    /// 切换到指定的工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    public void SetCurrentSheet(string sheetName)
    {
        _currentSheetName = sheetName;
    }

    /// <summary>
    /// 写入数据到当前工作表
    /// </summary>
    /// <param name="data">数据</param>
    public void Write(List<List<object?>> data)
    {
        Write(_currentSheetName, data);
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">数据</param>
    public void Write(int sheetIndex, List<List<object?>> data)
    {
        var sheetName = sheetIndex == 0 && !_sheets.ContainsKey("Sheet1") 
            ? "Sheet1" 
            : $"Sheet{sheetIndex + 1}";
        Write(sheetName, data);
    }

    /// <summary>
    /// 写入数据到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">数据</param>
    public void Write(string sheetName, List<List<object?>> data)
    {
        var rows = data.Select(row => row.Cast<object>().ToList()).ToList();
        _sheets[sheetName] = rows;
        _currentSheetName = sheetName;
    }

    /// <summary>
    /// 写入表头到当前工作表
    /// </summary>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(List<string> headers)
    {
        WriteHeader(_currentSheetName, headers);
    }

    /// <summary>
    /// 写入表头到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(int sheetIndex, List<string> headers)
    {
        var sheetName = sheetIndex == 0 && !_sheets.ContainsKey("Sheet1") 
            ? "Sheet1" 
            : $"Sheet{sheetIndex + 1}";
        WriteHeader(sheetName, headers);
    }

    /// <summary>
    /// 写入表头到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="headers">表头数据</param>
    public void WriteHeader(string sheetName, List<string> headers)
    {
        if (!_sheets.ContainsKey(sheetName))
        {
            _sheets[sheetName] = new List<List<object>>();
        }
        
        var sheet = _sheets[sheetName] as List<List<object>>;
        var headerRow = headers.Cast<object>().ToList();
        
        if (sheet.Count == 0)
        {
            sheet.Add(headerRow);
        }
        else
        {
            sheet.Insert(0, headerRow);
        }
    }

    /// <summary>
    /// 写入字典列表到当前工作表
    /// </summary>
    /// <param name="data">字典列表</param>
    public void Write(List<Dictionary<string, object?>> data)
    {
        Write(_currentSheetName, data);
    }

    /// <summary>
    /// 写入字典列表到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">字典列表</param>
    public void Write(int sheetIndex, List<Dictionary<string, object?>> data)
    {
        var sheetName = sheetIndex == 0 && !_sheets.ContainsKey("Sheet1") 
            ? "Sheet1" 
            : $"Sheet{sheetIndex + 1}";
        Write(sheetName, data);
    }

    /// <summary>
    /// 写入字典列表到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">字典列表</param>
    public void Write(string sheetName, List<Dictionary<string, object?>> data)
    {
        _sheets[sheetName] = data;
        _currentSheetName = sheetName;
    }

    /// <summary>
    /// 写入对象列表到当前工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="data">对象列表</param>
    public void Write<T>(List<T> data)
    {
        Write(_currentSheetName, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="data">对象列表</param>
    public void Write<T>(int sheetIndex, List<T> data)
    {
        var sheetName = sheetIndex == 0 && !_sheets.ContainsKey("Sheet1") 
            ? "Sheet1" 
            : $"Sheet{sheetIndex + 1}";
        Write(sheetName, data);
    }

    /// <summary>
    /// 写入对象列表到指定工作表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="data">对象列表</param>
    public void Write<T>(string sheetName, List<T> data)
    {
        _sheets[sheetName] = data;
        _currentSheetName = sheetName;
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
    /// 写入DataTable到当前工作表
    /// </summary>
    /// <param name="dataTable">DataTable</param>
    public void Write(DataTable dataTable)
    {
        Write(_currentSheetName, dataTable);
    }

    /// <summary>
    /// 写入DataTable到指定工作表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="dataTable">DataTable</param>
    public void Write(int sheetIndex, DataTable dataTable)
    {
        var sheetName = sheetIndex == 0 && !_sheets.ContainsKey("Sheet1") 
            ? "Sheet1" 
            : $"Sheet{sheetIndex + 1}";
        Write(sheetName, dataTable);
    }

    /// <summary>
    /// 写入DataTable到指定工作表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="dataTable">DataTable</param>
    public void Write(string sheetName, DataTable dataTable)
    {
        _sheets[sheetName] = dataTable;
        _currentSheetName = sheetName;
    }

    /// <summary>
    /// 保存 Excel 文件
    /// </summary>
    public void Save()
    {
        if (_isStreamMode)
        {
            SaveAs(_stream);
        }
        else
        {
            SaveAs(_filePath);
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
            if (_sheets.Count == 0)
            {
                // 如果没有数据，创建一个空工作表
                _sheets["Sheet1"] = new List<Dictionary<string, object>>();
            }
            
            // 转换所有工作表数据为正确的格式
            var convertedSheets = new Dictionary<string, object>();
            foreach (var kvp in _sheets)
            {
                var value = kvp.Value;
                if (value is List<List<object>> rows)
                {
                    // 转换为字典列表格式
                    var dictList = ConvertToDictionaryList(rows);
                    convertedSheets[kvp.Key] = dictList;
                }
                else if (value is DataTable dt)
                {
                    // DataTable 已经是正确的格式
                    convertedSheets[kvp.Key] = dt;
                }
                else if (value is List<Dictionary<string, object>> dictList2)
                {
                    convertedSheets[kvp.Key] = dictList2;
                }
                else if (value is List<Dictionary<string, object?>> dictList3)
                {
                    // 转换可为null的字典为不可为null的
                    var converted = dictList3.Select(d => d.ToDictionary(k => k.Key, v => (object?)v.Value)).ToList();
                    convertedSheets[kvp.Key] = converted;
                }
                else
                {
                    convertedSheets[kvp.Key] = value;
                }
            }
            
            if (convertedSheets.Count == 1)
            {
                // 单个工作表直接保存
                var sheet = convertedSheets.Values.First();
                MiniExcel.SaveAs(filePath, sheet, overwriteFile: true);
            }
            else
            {
                // 多个工作表
                MiniExcel.SaveAs(filePath, convertedSheets, overwriteFile: true);
            }
        }
        catch (System.Exception ex)
        {
            throw new POIException("保存 Excel 文件失败", ex);
        }
    }

    /// <summary>
    /// 将List<List<object>>转换为List<Dictionary<string, object>>
    /// </summary>
    private List<Dictionary<string, object>> ConvertToDictionaryList(List<List<object>> rows)
    {
        var result = new List<Dictionary<string, object>>();
        
        if (rows.Count == 0)
        {
            return result;
        }

        // 获取第一行作为表头
        var headerRow = rows[0];
        var headers = new List<string>();
        
        // 使用实际的表头数据作为字典的键
        for (int i = 0; i < headerRow.Count; i++)
        {
            var header = headerRow[i]?.ToString() ?? GetColumnName(i + 1);
            headers.Add(header);
        }

        // 从第二行开始处理数据
        for (int i = 1; i < rows.Count; i++)
        {
            var row = rows[i];
            var dict = new Dictionary<string, object>();
            
            for (int j = 0; j < headers.Count && j < row.Count; j++)
            {
                dict[headers[j]] = row[j] ?? "";
            }
            
            result.Add(dict);
        }

        return result;
    }

    /// <summary>
    /// 获取列名（A, B, C, ...）
    /// </summary>
    private string GetColumnName(int columnIndex)
    {
        return ((char)('A' + (columnIndex - 1) % 26)).ToString();
    }

    /// <summary>
    /// 保存 Excel 到指定流
    /// </summary>
    /// <param name="stream">流</param>
    public void SaveAs(Stream stream)
    {
        try
        {
            if (_sheets.Count == 0)
            {
                _sheets["Sheet1"] = new List<Dictionary<string, object>>();
            }
            
            // 转换所有工作表数据为正确的格式
            var convertedSheets = new Dictionary<string, object>();
            foreach (var kvp in _sheets)
            {
                var value = kvp.Value;
                if (value is List<List<object>> rows)
                {
                    var dictList = ConvertToDictionaryList(rows);
                    convertedSheets[kvp.Key] = dictList;
                }
                else if (value is DataTable dt)
                {
                    convertedSheets[kvp.Key] = dt;
                }
                else if (value is List<Dictionary<string, object>> dictList2)
                {
                    convertedSheets[kvp.Key] = dictList2;
                }
                else if (value is List<Dictionary<string, object?>> dictList3)
                {
                    var converted = dictList3.Select(d => d.ToDictionary(k => k.Key, v => (object?)v.Value)).ToList();
                    convertedSheets[kvp.Key] = converted;
                }
                else
                {
                    convertedSheets[kvp.Key] = value;
                }
            }
            
            if (convertedSheets.Count == 1)
            {
                var sheet = convertedSheets.Values.First();
                stream.SaveAs(sheet);
            }
            else
            {
                stream.SaveAs(convertedSheets);
            }
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
        _sheets.Clear();
    }
}