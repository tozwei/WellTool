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

using OfficeOpenXml;

namespace WellTool.Poi;

/// <summary>
/// EPPlus 许可证初始化器
/// </summary>
public static class EPPlusInitializer
{
    /// <summary>
    /// 静态构造函数，设置EPPlus许可证
    /// </summary>
    static EPPlusInitializer()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    /// <summary>
    /// 初始化方法，确保静态构造函数被调用
    /// </summary>
    public static void Initialize()
    {
        // 访问静态属性以触发静态构造函数
        _ = ExcelPackage.LicenseContext;
    }
}