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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Text.Csv;

/// <summary>
/// CSV数据类
/// 包含CSV文件的头部信息和行数据
/// </summary>
public class CsvData : IEnumerable<CsvRow>
{
    private static readonly long serialVersionUID = 1L;

    private readonly List<string> _header;
    private readonly List<CsvRow> _rows;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="header">头部信息</param>
    /// <param name="rows">行数据列表</param>
    public CsvData(List<string> header, List<CsvRow> rows)
    {
        _header = header;
        _rows = rows;
    }

    /// <summary>
    /// 获取总行数
    /// </summary>
    public int RowCount => _rows.Count;

    /// <summary>
    /// 获取头部信息
    /// </summary>
    public List<string> Header => _header;

    /// <summary>
    /// 获取指定行
    /// </summary>
    /// <param name="index">行索引</param>
    /// <returns>行数据</returns>
    public CsvRow GetRow(int index)
    {
        return _rows[index];
    }

    /// <summary>
    /// 获取所有行
    /// </summary>
    public List<CsvRow> GetRows()
    {
        return _rows;
    }

    /// <summary>
    /// 获取迭代器
    /// </summary>
    public IEnumerator<CsvRow> GetEnumerator()
    {
        return _rows.GetEnumerator();
    }

    /// <summary>
    /// 获取迭代器
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _rows.GetEnumerator();
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    public override string ToString()
    {
        return $"CsvData [header={_header}, rows={_rows.Count}]";
    }
}