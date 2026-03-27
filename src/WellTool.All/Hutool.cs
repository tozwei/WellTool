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

namespace WellTool;

/// <summary>
/// WellTool 统一入口类
/// </summary>
public static class Hutool
{
    // Poi 模块
    public static class Poi
    {
        /// <summary>
        /// Excel 工具类
        /// </summary>
        public static WellTool.Poi.ExcelUtil Excel => WellTool.Poi.ExcelUtil.Instance;
    }

    // Script 模块
    public static class Script
    {
        /// <summary>
        /// 脚本工具类
        /// </summary>
        public static WellTool.Script.ScriptUtil Util => WellTool.Script.ScriptUtil.Instance;
    }
}
