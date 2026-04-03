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
using WellTool.Core.Text.Finder;

namespace WellTool.Core.Text.Split;

/// <summary>
/// 字符串切分迭代器
/// 此迭代器是字符串切分的懒模式实现，实例化后不完成切分，只有调用hasNext或遍历时才完成切分
/// </summary>
public class SplitIter : IEnumerable<string>, IEnumerator<string>
{
    private static readonly long serialVersionUID = 1L;

    private readonly string _text;
    private readonly TextFinder _finder;
    private readonly int _limit;
    private readonly bool _ignoreEmpty;

    private int _offset;
    private int _count = -1;
    private string _current;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="text">文本，不能为null</param>
    /// <param name="separatorFinder">分隔符匹配器</param>
    /// <param name="limit">限制数量，小于等于0表示无限制</param>
    /// <param name="ignoreEmpty">是否忽略空串</param>
    public SplitIter(string text, TextFinder separatorFinder, int limit, bool ignoreEmpty)
    {
        if (text == null)
        {
            throw new ArgumentException("Text must be not null!");
        }
        
        _text = text;
        _finder = separatorFinder;
        _limit = limit > 0 ? limit : int.MaxValue;
        _ignoreEmpty = ignoreEmpty;
    }

    /// <summary>
    /// 获取当前元素
    /// </summary>
    public string Current => _current;

    /// <summary>
    /// 获取当前元素
    /// </summary>
    object IEnumerator.Current => _current;

    /// <summary>
    /// 移动到下一个元素
    /// </summary>
    public bool MoveNext()
    {
        // 达到数量上限或末尾，结束
        if (_count >= _limit || _offset > _text.Length)
        {
            _current = null;
            return false;
        }

        // 达到数量上限
        if (_count == (_limit - 1))
        {
            // 当到达限制次数时，最后一个元素为剩余部分
            if (_ignoreEmpty && _offset == _text.Length)
            {
                // 最后一个是空串
                _current = null;
                return false;
            }
            // 结尾整个作为一个元素
            _count++;
            _current = _text.Substring(_offset);
            _offset = _text.Length + 1;
            return true;
        }

        string result = null;
        int start;
        do
        {
            start = _finder.Find(_text, _offset);
            // 无分隔符，结束
            if (start < 0)
            {
                // 如果不再有分隔符，但是遗留了字符，则单独作为一个段
                if (_offset <= _text.Length)
                {
                    result = _text.Substring(_offset);
                    if (!_ignoreEmpty || !string.IsNullOrEmpty(result))
                    {
                        // 返回非空串
                        _offset = _text.Length + 1;
                        _current = result;
                        return true;
                    }
                }
                _current = null;
                return false;
            }

            // 找到新的分隔符位置
            result = _text.Substring(_offset, start - _offset);
            _offset = start + 1;
        }
        while (_ignoreEmpty && string.IsNullOrEmpty(result)); // 空串则继续循环

        _count++;
        _current = result;
        return true;
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        _offset = 0;
        _count = -1;
        _current = null;
    }

    /// <summary>
    /// 获取迭代器
    /// </summary>
    public IEnumerator<string> GetEnumerator()
    {
        return this;
    }

    /// <summary>
    /// 获取迭代器
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    /// 获取切分后的对象数组
    /// </summary>
    /// <param name="trim">是否去除元素两边空格</param>
    /// <returns>切分后的数组</returns>
    public string[] ToArray(bool trim)
    {
        return ToList(trim).ToArray();
    }

    /// <summary>
    /// 获取切分后的对象列表
    /// </summary>
    /// <param name="trim">是否去除元素两边空格</param>
    /// <returns>切分后的列表</returns>
    public List<string> ToList(bool trim)
    {
        return ToList(s => trim ? s?.Trim() : s);
    }

    /// <summary>
    /// 获取切分后的对象列表
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    /// <param name="mapping">字符串映射函数</param>
    /// <returns>切分后的列表</returns>
    public List<T> ToList<T>(Func<string, T> mapping)
    {
        var result = new List<T>();
        Reset();
        while (MoveNext())
        {
            var apply = mapping(_current);
            if (_ignoreEmpty && apply == null)
            {
                continue;
            }
            result.Add(apply);
        }
        return result;
    }
}