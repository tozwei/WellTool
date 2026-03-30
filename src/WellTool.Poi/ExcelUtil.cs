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

using System.IO;
using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// Excel 工具类
/// </summary>
public class ExcelUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ExcelUtil Instance { get; } = new ExcelUtil();

    /// <summary>
    /// 创建 Excel 读取器
    /// </summary>
    /// <param name="filePath">Excel 文件路径</param>
    /// <returns>Excel 读取器</returns>
    public ExcelReader GetReader(string filePath)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelReader(package);
        }
        catch (Exception ex)
        {
            throw new POIException("创建 Excel 读取器失败", ex);
        }
    }

    /// <summary>
    /// 创建 Excel 写入器
    /// </summary>
    /// <param name="filePath">Excel 文件路径</param>
    /// <returns>Excel 写入器</returns>
    public ExcelWriter GetWriter(string filePath)
    {
        try
        {
            var package = new ExcelPackage(new FileInfo(filePath));
            return new ExcelWriter(package);
        }
        catch (Exception ex)
        {
            throw new POIException("创建 Excel 写入器失败", ex);
        }
    }
}
