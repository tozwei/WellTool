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
/// CSV写出配置项
/// </summary>
public class CsvWriteConfig : CsvConfig
{
    private static readonly long serialVersionUID = 5396453565371560052L;

    /// <summary>
    /// 是否始终使用文本分隔符，文本包装符，默认false，按需添加
    /// </summary>
    public bool AlwaysDelimitText { get; set; } = false;

    /// <summary>
    /// 换行符
    /// </summary>
    public char[] LineDelimiter { get; set; } = new char[] { '\r', '\n' };

    /// <summary>
    /// 文件末尾是否添加换行符
    /// </summary>
    public bool EndingLineBreak { get; set; } = true;

    /// <summary>
    /// 默认配置
    /// </summary>
    /// <returns>默认配置</returns>
    public static CsvWriteConfig DefaultConfig()
    {
        return new CsvWriteConfig();
    }

    /// <summary>
    /// 设置是否始终使用文本分隔符，文本包装符，默认false，按需添加
    /// </summary>
    /// <param name="alwaysDelimitText">是否始终使用文本分隔符</param>
    /// <returns>this</returns>
    public CsvWriteConfig SetAlwaysDelimitText(bool alwaysDelimitText)
    {
        AlwaysDelimitText = alwaysDelimitText;
        return this;
    }

    /// <summary>
    /// 设置换行符
    /// </summary>
    /// <param name="lineDelimiter">换行符</param>
    /// <returns>this</returns>
    public CsvWriteConfig SetLineDelimiter(char[] lineDelimiter)
    {
        LineDelimiter = lineDelimiter;
        return this;
    }

    /// <summary>
    /// 设置文件末尾是否添加换行符
    /// </summary>
    /// <param name="endingLineBreak">文件末尾是否添加换行符</param>
    /// <returns>this</returns>
    public CsvWriteConfig SetEndingLineBreak(bool endingLineBreak)
    {
        EndingLineBreak = endingLineBreak;
        return this;
    }
}