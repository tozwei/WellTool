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

namespace WellTool.DB;

/// <summary>
/// 分页参数
/// </summary>
public class Page
{
    /// <summary>
    /// 页码，从 1 开始
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pageNumber">页码</param>
    /// <param name="pageSize">每页大小</param>
    public Page(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber > 0 ? pageNumber : 1;
        PageSize = pageSize > 0 ? pageSize : 10;
    }

    /// <summary>
    /// 获取偏移量
    /// </summary>
    /// <returns>偏移量</returns>
    public int GetOffset()
    {
        return (PageNumber - 1) * PageSize;
    }

    /// <summary>
    /// 获取限制数
    /// </summary>
    /// <returns>限制数</returns>
    public int GetLimit()
    {
        return PageSize;
    }
}