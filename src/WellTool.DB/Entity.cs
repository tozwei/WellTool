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

namespace WellTool.DB;

/// <summary>
/// 数据库实体
/// </summary>
public class Entity
{
    /// <summary>
    /// 字段值映射
    /// </summary>
    private readonly Dictionary<string, object?> _fields = new();

    /// <summary>
    /// 获取或设置字段值
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段值</returns>
    public object? this[string fieldName]
    {
        get => _fields.TryGetValue(fieldName, out var value) ? value : null;
        set => _fields[fieldName] = value;
    }

    /// <summary>
    /// 获取所有字段名
    /// </summary>
    /// <returns>字段名列表</returns>
    public ICollection<string> GetFieldNames()
    {
        return _fields.Keys;
    }

    /// <summary>
    /// 获取所有字段值
    /// </summary>
    /// <returns>字段值映射</returns>
    public Dictionary<string, object?> GetFields()
    {
        return new Dictionary<string, object?>(_fields);
    }

    /// <summary>
    /// 设置字段值
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <param name="value">字段值</param>
    /// <returns>当前实体</returns>
    public Entity Set(string fieldName, object? value)
    {
        _fields[fieldName] = value;
        return this;
    }

    /// <summary>
    /// 获取字段值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段值</returns>
    public T? Get<T>(string fieldName)
    {
        var value = this[fieldName];
        if (value == null)
        {
            return default;
        }
        return (T)value;
    }

    /// <summary>
    /// 获取字段值，如为 null 则返回默认值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="fieldName">字段名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>字段值</returns>
    public T Get<T>(string fieldName, T defaultValue)
    {
        var value = Get<T>(fieldName);
        return value == null ? defaultValue : value;
    }

    /// <summary>
    /// 检查字段是否存在
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns>是否存在</returns>
    public bool ContainsKey(string fieldName)
    {
        return _fields.ContainsKey(fieldName);
    }

    /// <summary>
    /// 移除字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns>是否移除成功</returns>
    public bool Remove(string fieldName)
    {
        return _fields.Remove(fieldName);
    }

    /// <summary>
    /// 清空所有字段
    /// </summary>
    public void Clear()
    {
        _fields.Clear();
    }

    /// <summary>
    /// 获取字段数量
    /// </summary>
    public int Count => _fields.Count;
}
