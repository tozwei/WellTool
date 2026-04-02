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

namespace WellTool.DB.Dialect.Impl;

/// <summary>
/// H2 数据库方言
/// </summary>
public class H2Dialect : AnsiSqlDialect
{
    /// <summary>
    /// 获取数据库类型名称
    /// </summary>
    public override string Name => "H2";
}
