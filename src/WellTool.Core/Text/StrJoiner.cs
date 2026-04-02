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

using System.Text;

namespace WellTool.Core.Text;

/// <summary>
/// 字符串拼接器<br>
/// 用于高效地拼接字符串
/// </summary>
public class StrJoiner
{
    private readonly StringBuilder _builder = new();
    private readonly string _delimiter;
    private readonly string? _prefix;
    private readonly string? _suffix;
    private bool _isStarted;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="delimiter">分隔符</param>
    public StrJoiner(string delimiter) : this(delimiter, null, null)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="delimiter">分隔符</param>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    public StrJoiner(string delimiter, string? prefix, string? suffix)
    {
        _delimiter = delimiter;
        _prefix = prefix;
        _suffix = suffix;
    }

    /// <summary>
    /// 添加元素
    /// </summary>
    /// <param name="element">要添加的元素</param>
    /// <returns>当前实例，用于链式调用</returns>
    public StrJoiner Add(object? element)
    {
        PrepareAdd();
        _builder.Append(element);
        return this;
    }

    /// <summary>
    /// 添加多个元素
    /// </summary>
    /// <param name="elements">要添加的元素集合</param>
    /// <returns>当前实例，用于链式调用</returns>
    public StrJoiner AddRange(IEnumerable<object?> elements)
    {
        foreach (var element in elements)
        {
            Add(element);
        }
        return this;
    }

    /// <summary>
    /// 准备添加元素
    /// </summary>
    private void PrepareAdd()
    {
        if (!_isStarted)
        {
            if (_prefix != null)
            {
                _builder.Append(_prefix);
            }
            _isStarted = true;
        }
        else
        {
            _builder.Append(_delimiter);
        }
    }

    /// <summary>
    /// 构建最终的字符串
    /// </summary>
    /// <returns>拼接后的字符串</returns>
    public string Build()
    {
        if (_suffix != null)
        {
            _builder.Append(_suffix);
        }
        return _builder.ToString();
    }

    /// <summary>
    /// 重置拼接器
    /// </summary>
    /// <returns>当前实例，用于链式调用</returns>
    public StrJoiner Reset()
    {
        _builder.Clear();
        _isStarted = false;
        return this;
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>拼接后的字符串</returns>
    public override string ToString()
    {
        return Build();
    }
}
