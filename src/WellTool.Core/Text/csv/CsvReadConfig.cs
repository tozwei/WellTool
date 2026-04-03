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

namespace WellTool.Core.Text.Csv;

/// <summary>
/// CSV读取配置项
/// </summary>
public class CsvReadConfig : CsvConfig
{
    private static readonly long serialVersionUID = 5396453565371560052L;

    /// <summary>
    /// 指定标题行号，-1表示无标题行
    /// </summary>
    public long HeaderLineNo { get; set; } = -1;

    /// <summary>
    /// 是否跳过空白行，默认true
    /// </summary>
    public bool SkipEmptyRows { get; set; } = true;

    /// <summary>
    /// 每行字段个数不同时是否抛出异常，默认false
    /// </summary>
    public bool ErrorOnDifferentFieldCount { get; set; } = false;

    /// <summary>
    /// 定义开始的行（包括），此处为原始文件行号
    /// </summary>
    public long BeginLineNo { get; set; } = 0;

    /// <summary>
    /// 结束的行（包括），此处为原始文件行号
    /// </summary>
    public long EndLineNo { get; set; } = long.MaxValue - 1;

    /// <summary>
    /// 每个字段是否去除两边空白符
    /// </summary>
    public bool TrimField { get; set; } = false;

    /// <summary>
    /// 默认配置
    /// </summary>
    /// <returns>默认配置</returns>
    public static CsvReadConfig DefaultConfig()
    {
        return new CsvReadConfig();
    }

    /// <summary>
    /// 设置是否首行做为标题行，默认false
    /// </summary>
    /// <param name="containsHeader">是否首行做为标题行，默认false</param>
    /// <returns>this</returns>
    public CsvReadConfig SetContainsHeader(bool containsHeader)
    {
        HeaderLineNo = containsHeader ? BeginLineNo : -1;
        ContainsHeader = containsHeader;
        return this;
    }

    /// <summary>
    /// 设置标题行行号，默认-1，表示无标题行
    /// </summary>
    /// <param name="headerLineNo">标题行行号，-1表示无标题行</param>
    /// <returns>this</returns>
    public CsvReadConfig SetHeaderLineNo(long headerLineNo)
    {
        HeaderLineNo = headerLineNo;
        return this;
    }

    /// <summary>
    /// 设置是否跳过空白行，默认true
    /// </summary>
    /// <param name="skipEmptyRows">是否跳过空白行，默认true</param>
    /// <returns>this</returns>
    public CsvReadConfig SetSkipEmptyRows(bool skipEmptyRows)
    {
        SkipEmptyRows = skipEmptyRows;
        return this;
    }

    /// <summary>
    /// 设置每行字段个数不同时是否抛出异常，默认false
    /// </summary>
    /// <param name="errorOnDifferentFieldCount">每行字段个数不同时是否抛出异常，默认false</param>
    /// <returns>this</returns>
    public CsvReadConfig SetErrorOnDifferentFieldCount(bool errorOnDifferentFieldCount)
    {
        ErrorOnDifferentFieldCount = errorOnDifferentFieldCount;
        return this;
    }

    /// <summary>
    /// 设置开始的行（包括），默认0，此处为原始文件行号
    /// </summary>
    /// <param name="beginLineNo">开始的行号（包括）</param>
    /// <returns>this</returns>
    public CsvReadConfig SetBeginLineNo(long beginLineNo)
    {
        BeginLineNo = beginLineNo;
        return this;
    }

    /// <summary>
    /// 设置结束的行（包括），默认不限制，此处为原始文件行号
    /// </summary>
    /// <param name="endLineNo">结束的行号（包括）</param>
    /// <returns>this</returns>
    public CsvReadConfig SetEndLineNo(long endLineNo)
    {
        EndLineNo = endLineNo;
        return this;
    }

    /// <summary>
    /// 设置每个字段是否去除两边空白符
    /// </summary>
    /// <param name="trimField">去除两边空白符</param>
    /// <returns>this</returns>
    public CsvReadConfig SetTrimField(bool trimField)
    {
        TrimField = trimField;
        return this;
    }
}