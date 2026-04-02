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

namespace WellTool.Core.Text.Csv;

/// <summary>
/// CSV配置类<br>
/// 用于配置CSV的读写行为
/// </summary>
public class CsvConfig
{
    /// <summary>
    /// 默认分隔符
    /// </summary>
    public const char DEFAULT_SEPARATOR = ',';

    /// <summary>
    /// 默认引号字符
    /// </summary>
    public const char DEFAULT_QUOTE_CHAR = '"';

    /// <summary>
    /// 默认转义字符
    /// </summary>
    public const char DEFAULT_ESCAPE_CHAR = '\\';

    /// <summary>
    /// 分隔符
    /// </summary>
    public char Separator { get; set; } = DEFAULT_SEPARATOR;

    /// <summary>
    /// 引号字符
    /// </summary>
    public char QuoteChar { get; set; } = DEFAULT_QUOTE_CHAR;

    /// <summary>
    /// 转义字符
    /// </summary>
    public char EscapeChar { get; set; } = DEFAULT_ESCAPE_CHAR;

    /// <summary>
    /// 是否忽略空白
    /// </summary>
    public bool IgnoreBlank { get; set; } = false;

    /// <summary>
    /// 是否包含表头
    /// </summary>
    public bool ContainsHeader { get; set; } = false;

    /// <summary>
    /// 表头
    /// </summary>
    public string[]? Header { get; set; }

    /// <summary>
    /// 创建默认配置
    /// </summary>
    /// <returns>默认配置</returns>
    public static CsvConfig Default()
    {
        return new CsvConfig();
    }
}
