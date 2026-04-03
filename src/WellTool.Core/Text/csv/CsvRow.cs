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
using System.Reflection;

namespace WellTool.Core.Text.Csv;

/// <summary>
/// CSV行数据
/// </summary>
public class CsvRow : IList<string>
{
    private readonly int _originalLineNumber;
    private readonly List<string> _fields;
    private readonly List<string> _header;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="originalLineNumber">原始行号</param>
    /// <param name="fields">字段列表</param>
    /// <param name="header">表头列表</param>
    public CsvRow(int originalLineNumber, List<string> fields, List<string> header)
    {
        _originalLineNumber = originalLineNumber;
        _fields = fields;
        _header = header;
    }

    /// <summary>
    /// 获取原始行号
    /// </summary>
    public int OriginalLineNumber => _originalLineNumber;

    /// <summary>
    /// 根据表头名称获取字段值
    /// </summary>
    /// <param name="name">表头名称</param>
    /// <returns>字段值</returns>
    public string GetByName(string name)
    {
        if (_header == null || _fields == null)
        {
            return null;
        }

        int index = _header.IndexOf(name);
        if (index >= 0 && index < _fields.Count)
        {
            return _fields[index];
        }

        return null;
    }

    /// <summary>
    /// 获取字段映射（表头名 -> 字段值）
    /// </summary>
    /// <returns>字段映射</returns>
    public Dictionary<string, string> GetFieldMap()
    {
        var map = new Dictionary<string, string>();

        if (_header != null && _fields != null)
        {
            for (int i = 0; i < _header.Count && i < _fields.Count; i++)
            {
                map[_header[i]] = _fields[i];
            }
        }

        return map;
    }

    /// <summary>
    /// 转换为Bean对象
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <returns>Bean对象</returns>
    public T ToBean<T>() where T : new()
    {
        var result = new T();
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.CanWrite)
            {
                var value = GetByName(property.Name);
                if (value != null)
                {
                    try
                    {
                        var targetType = property.PropertyType;
                        if (targetType == typeof(string))
                        {
                            property.SetValue(result, value);
                        }
                        else if (targetType == typeof(int))
                        {
                            if (int.TryParse(value, out int intValue))
                            {
                                property.SetValue(result, intValue);
                            }
                        }
                        else if (targetType == typeof(long))
                        {
                            if (long.TryParse(value, out long longValue))
                            {
                                property.SetValue(result, longValue);
                            }
                        }
                        else if (targetType == typeof(double))
                        {
                            if (double.TryParse(value, out double doubleValue))
                            {
                                property.SetValue(result, doubleValue);
                            }
                        }
                        else if (targetType == typeof(decimal))
                        {
                            if (decimal.TryParse(value, out decimal decimalValue))
                            {
                                property.SetValue(result, decimalValue);
                            }
                        }
                        else if (targetType == typeof(bool))
                        {
                            if (bool.TryParse(value, out bool boolValue))
                            {
                                property.SetValue(result, boolValue);
                            }
                        }
                        else
                        {
                            property.SetValue(result, Convert.ChangeType(value, targetType));
                        }
                    }
                    catch
                    {
                        // 忽略转换错误
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 获取字段数量
    /// </summary>
    public int Count => _fields.Count;

    /// <summary>
    /// 是否为只读
    /// </summary>
    public bool IsReadOnly => true;

    /// <summary>
    /// 获取或设置指定索引的字段值
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>字段值</returns>
    public string this[int index]
    {
        get => _fields[index];
        set => throw new NotSupportedException();
    }

    /// <summary>
    /// 添加字段
    /// </summary>
    /// <param name="item">字段值</param>
    public void Add(string item) => throw new NotSupportedException();

    /// <summary>
    /// 清空字段
    /// </summary>
    public void Clear() => throw new NotSupportedException();

    /// <summary>
    /// 是否包含指定字段
    /// </summary>
    /// <param name="item">字段值</param>
    /// <returns>是否包含</returns>
    public bool Contains(string item) => _fields.Contains(item);

    /// <summary>
    /// 复制到数组
    /// </summary>
    /// <param name="array">目标数组</param>
    /// <param name="arrayIndex">起始索引</param>
    public void CopyTo(string[] array, int arrayIndex) => _fields.CopyTo(array, arrayIndex);

    /// <summary>
    /// 获取索引
    /// </summary>
    /// <param name="item">字段值</param>
    /// <returns>索引</returns>
    public int IndexOf(string item) => _fields.IndexOf(item);

    /// <summary>
    /// 插入字段
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="item">字段值</param>
    public void Insert(int index, string item) => throw new NotSupportedException();

    /// <summary>
    /// 移除字段
    /// </summary>
    /// <param name="item">字段值</param>
    /// <returns>是否成功</returns>
    public bool Remove(string item) => throw new NotSupportedException();

    /// <summary>
    /// 移除指定索引的字段
    /// </summary>
    /// <param name="index">索引</param>
    public void RemoveAt(int index) => throw new NotSupportedException();

    /// <summary>
    /// 获取迭代器
    /// </summary>
    /// <returns>迭代器</returns>
    public IEnumerator<string> GetEnumerator() => _fields.GetEnumerator();

    /// <summary>
    /// 获取迭代器
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => _fields.GetEnumerator();
}