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
/// 分页结果
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public class PageResult<T>
{
    /// <summary>
    /// 数据列表
    /// </summary>
    public List<T> Data { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    /// 页码
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="total">总记录数</param>
    /// <param name="pageNumber">页码</param>
    /// <param name="pageSize">每页大小</param>
    public PageResult(List<T> data, long total, int pageNumber, int pageSize)
    {
        Data = data;
        Total = total;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="total">总记录数</param>
    /// <param name="page">分页参数</param>
    public PageResult(List<T> data, long total, Page page)
        : this(data, total, page.PageNumber, page.PageSize)
    {}
}