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
/// Oracle 数据库方言
/// </summary>
public class OracleDialect : AnsiSqlDialect
{
    /// <summary>
    /// 获取数据库类型名称
    /// </summary>
    public override string Name => "Oracle";

    /// <summary>
    /// 构建分页 SQL
    /// </summary>
    /// <param name="sql">原始 SQL</param>
    /// <param name="offset">偏移量</param>
    /// <param name="limit">限制数</param>
    /// <returns>分页 SQL</returns>
    public override string BuildPaginationSql(string sql, int offset, int limit)
    {
        return $"SELECT * FROM (SELECT A.*, ROWNUM AS RN FROM ({sql}) A WHERE ROWNUM <= {offset + limit}) WHERE RN > {offset}";
    }

    /// <summary>
    /// 构建获取自增键的 SQL
    /// </summary>
    /// <returns>获取自增键的 SQL</returns>
    public override string BuildGetGeneratedKeysSql()
    {
        return "SELECT LAST_INSERT_ID()";
    }
}
