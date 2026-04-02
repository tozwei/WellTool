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

namespace WellTool.DB.DS.Pooled;

/// <summary>
/// 数据库连接池配置
/// </summary>
public class DbConfig
{
    /// <summary>
    /// 驱动类名
    /// </summary>
    public string DriverClass { get; set; }

    /// <summary>
    /// JDBC URL
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 初始化连接数
    /// </summary>
    public int InitialSize { get; set; } = 10;

    /// <summary>
    /// 最大连接数
    /// </summary>
    public int MaxActive { get; set; } = 50;

    /// <summary>
    /// 最大空闲连接数
    /// </summary>
    public int MaxIdle { get; set; } = 10;

    /// <summary>
    /// 最小空闲连接数
    /// </summary>
    public int MinIdle { get; set; } = 5;

    /// <summary>
    /// 最大等待时间（毫秒）
    /// </summary>
    public int MaxWait { get; set; } = 60000;

    /// <summary>
    /// 连接超时时间（毫秒）
    /// </summary>
    public int ConnectionTimeout { get; set; } = 30000;

    /// <summary>
    /// 验证查询
    /// </summary>
    public string ValidationQuery { get; set; } = "SELECT 1";

    /// <summary>
    /// 验证超时时间（毫秒）
    /// </summary>
    public int ValidationTimeout { get; set; } = 5000;

    /// <summary>
    /// 是否在获取连接时验证
    /// </summary>
    public bool TestOnBorrow { get; set; } = true;

    /// <summary>
    /// 是否在归还连接时验证
    /// </summary>
    public bool TestOnReturn { get; set; } = false;

    /// <summary>
    /// 是否在空闲时验证
    /// </summary>
    public bool TestWhileIdle { get; set; } = true;

    /// <summary>
    /// 空闲连接回收间隔（毫秒）
    /// </summary>
    public int TimeBetweenEvictionRunsMillis { get; set; } = 60000;

    /// <summary>
    /// 连接最大生存时间（毫秒）
    /// </summary>
    public int MaxLifetime { get; set; } = 1800000;
}
