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
/// Excel 读取器
/// </summary>
public class ExcelReader : IDisposable
{
    private readonly string? _filePath;
    private Stream? _stream;
    private readonly bool _isStreamMode;
    private List<string> _sheetNames = new();
    private string _currentSheetName = "Sheet1";

    /// <summary>
    /// 获取所有工作表名称
    /// </summary>
    public List<string> SheetNames => _sheetNames;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    public ExcelReader(string filePath)
    {
        _filePath = filePath;
        _isStreamMode = false;
        LoadSheetNames();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelReader(string filePath, string sheetName)
    {
        _filePath = filePath;
        _isStreamMode = false;
        _currentSheetName = sheetName;
        LoadSheetNames();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="sheetIndex">工作表索引</param>
    public ExcelReader(string filePath, int sheetIndex)
    {
        _filePath = filePath;
        _isStreamMode = false;
        LoadSheetNames();
        if (sheetIndex >= 0 && sheetIndex < _sheetNames.Count)
        {
            _currentSheetName = _sheetNames[sheetIndex];
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    public ExcelReader(Stream stream)
    {
        _stream = stream;
        _isStreamMode = true;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="sheetName">工作表名称</param>
    public ExcelReader(Stream stream, string sheetName)
    {
        _stream = stream;
        _isStreamMode = true;
        _currentSheetName = sheetName;
    }

    /// <summary>
    /// 加载工作表名称
    /// </summary>
    private void LoadSheetNames()
    {
        try
        {
            if (_isStreamMode && _stream != null)
            {
                _stream.Position = 0;
                _sheetNames = MiniExcel.GetSheetNames(_stream).ToList();
            }
            else if (_filePath != null)
            {
                _sheetNames = MiniExcel.GetSheetNames(_filePath).ToList();
            }
            
            if (_sheetNames.Count > 0 && string.IsNullOrEmpty(_currentSheetName))
            {
                _currentSheetName = _sheetNames[0];
            }
        }
        catch
        {
            _sheetNames = new List<string> { "Sheet1" };
        }
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
        return ReadSheet(_currentSheetName);
    }

    /// <summary>
    /// 读取指定工作表的数据
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <returns>数据列表</returns>
    public List<List<object?>> ReadSheet(int sheetIndex)
    {
        if (sheetIndex >= 0 && sheetIndex < _sheetNames.Count)
        {
            return ReadSheet(_sheetNames[sheetIndex]);
        }
        return new List<List<object?>>();
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
            var result = new List<List<object?>>();
            
            var rows = QueryAsDictList(sheetName);

            foreach (var row in rows)
            {
                var rowData = new List<object?>();
                int colIndex = 1;
                while (row.ContainsKey(GetColumnName(colIndex)))
                {
                    rowData.Add(row[GetColumnName(colIndex)]);
                    colIndex++;
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
    /// 获取列名（A, B, C, ...）
    /// </summary>
    private string GetColumnName(int columnIndex)
    {
        return ((char)('A' + (columnIndex - 1) % 26)).ToString();
    }

    /// <summary>
    /// 读取当前工作表的数据为字典列表
    /// </summary>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(int headerRowIndex = 0)
    {
        return ReadAsDictionaryList(_currentSheetName, headerRowIndex);
    }

    /// <summary>
    /// 读取指定工作表的数据为字典列表
    /// </summary>
    /// <param name="sheetIndex">工作表索引</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(int sheetIndex, int headerRowIndex = 0)
    {
        if (sheetIndex >= 0 && sheetIndex < _sheetNames.Count)
        {
            return ReadAsDictionaryList(_sheetNames[sheetIndex], headerRowIndex);
        }
        return new List<Dictionary<string, object?>>();
    }

    /// <summary>
    /// 读取指定工作表的数据为字典列表
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>字典列表</returns>
    public List<Dictionary<string, object?>> ReadAsDictionaryList(string sheetName, int headerRowIndex = 0)
    {
        try
        {
            var rows = QueryAsDictList(sheetName);
            
            // MiniExcel第一行始终作为表头
            // headerRowIndex 表示实际表头在数据中的位置（从0开始）
            // 如果headerRowIndex > 0，需要跳过前面的行
            if (headerRowIndex > 0)
            {
                rows = rows.Skip(headerRowIndex).ToList();
            }

            return rows;
        }
        catch (System.Exception ex)
        {
            throw new POIException("读取 Excel 工作表失败", ex);
        }
    }

    /// <summary>
    /// 查询数据为字典列表（第一行作为表头）
    /// </summary>
    private List<Dictionary<string, object?>> QueryAsDictList(string sheetName)
    {
        var result = new List<Dictionary<string, object?>>();
        
        if (_isStreamMode && _stream != null)
        {
            _stream.Position = 0;
            var rows = MiniExcel.Query<Dictionary<string, object?>>(_stream, sheetName: sheetName);
            result = rows.ToList();
        }
        else if (_filePath != null)
        {
            var rows = MiniExcel.Query<Dictionary<string, object?>>(_filePath, sheetName: sheetName);
            result = rows.ToList();
        }
        
        return result;
    }

    /// <summary>
    /// 读取当前工作表的数据为DataTable
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="headerRowIndex">表头行索引，从0开始</param>
    /// <returns>DataTable</returns>
    public DataTable ReadAsDataTable(string tableName = "Sheet1", int headerRowIndex = 0)
    {
        return ReadAsDataTable(_currentSheetName, tableName, headerRowIndex);
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
        if (sheetIndex >= 0 && sheetIndex < _sheetNames.Count)
        {
            return ReadAsDataTable(_sheetNames[sheetIndex], tableName, headerRowIndex);
        }
        return new DataTable(tableName);
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
        try
        {
            var dataTable = new DataTable(tableName);
            
            var rows = QueryAsDictList(sheetName);

            if (rows.Count == 0)
            {
                return dataTable;
            }

            // 添加列
            var firstRow = rows[0];
            foreach (var key in firstRow.Keys)
            {
                dataTable.Columns.Add(key, typeof(object));
            }

            // 跳过表头行（MiniExcel第一行是表头）
            var dataRows = rows.Skip(headerRowIndex + 1);
            foreach (var row in dataRows)
            {
                var dataRow = dataTable.NewRow();
                foreach (DataColumn col in dataTable.Columns)
                {
                    var colName = col.ColumnName;
                    if (row.ContainsKey(colName))
                    {
                        dataRow[colName] = row[colName] ?? DBNull.Value;
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
        return ReadAsList<T>(_currentSheetName, headerRowIndex);
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
        if (sheetIndex >= 0 && sheetIndex < _sheetNames.Count)
        {
            return ReadAsList<T>(_sheetNames[sheetIndex], headerRowIndex);
        }
        return new List<T>();
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
        try
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties();

            var rows = QueryAsDictList(sheetName);

            // 跳过表头行
            if (headerRowIndex > 0)
            {
                rows = rows.Skip(headerRowIndex).ToList();
            }

            foreach (var row in rows)
            {
                var item = new T();
                foreach (var property in properties)
                {
                    if (row.ContainsKey(property.Name))
                    {
                        var value = row[property.Name];
                        if (value != null)
                        {
                            try
                            {
                                var convertedValue = Convert.ChangeType(value, property.PropertyType);
                                property.SetValue(item, convertedValue);
                            }
                            catch
                            {
                                // 类型转换失败，跳过
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
        _stream?.Dispose();
    }
}