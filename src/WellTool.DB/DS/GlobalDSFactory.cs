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

using System.Data;

namespace WellTool.DB.DS;

/// <summary>
/// 全局数据源工厂
/// </summary>
public static class GlobalDSFactory
{
    /// <summary>
    /// 数据源工厂映射
    /// </summary>
    private static readonly Dictionary<string, AbstractDSFactory> _factories = new()
    {
        { "simple", new Simple.SimpleDSFactory() },
        { "pooled", new Pooled.PooledDSFactory() }
    };

    /// <summary>
    /// 默认数据源类型
    /// </summary>
    private static string _defaultType = "simple";

    /// <summary>
    /// 设置默认数据源类型
    /// </summary>
    /// <param name="type">数据源类型</param>
    public static void SetDefaultType(string type)
    {
        _defaultType = type;
    }

    /// <summary>
    /// 获取默认数据源类型
    /// </summary>
    /// <returns>默认数据源类型</returns>
    public static string GetDefaultType()
    {
        return _defaultType;
    }

    /// <summary>
    /// 根据类型获取数据源工厂
    /// </summary>
    /// <param name="type">数据源类型</param>
    /// <returns>数据源工厂</returns>
    public static AbstractDSFactory GetFactory(string type)
    {
        if (_factories.TryGetValue(type, out var factory))
        {
            return factory;
        }
        return _factories[_defaultType]; // 使用默认数据源工厂
    }

    /// <summary>
    /// 注册数据源工厂
    /// </summary>
    /// <param name="type">数据源类型</param>
    /// <param name="factory">数据源工厂</param>
    public static void RegisterFactory(string type, AbstractDSFactory factory)
    {
        _factories[type] = factory;
    }

    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <param name="type">数据源类型</param>
    /// <param name="url">连接字符串</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>数据源</returns>
    public static IDbConnection CreateDataSource(string type, string url, string username, string password)
    {
        return GetFactory(type).CreateDataSource(url, username, password);
    }

    /// <summary>
    /// 创建默认数据源
    /// </summary>
    /// <param name="url">连接字符串</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns>数据源</returns>
    public static IDbConnection CreateDataSource(string url, string username, string password)
    {
        return CreateDataSource(_defaultType, url, username, password);
    }
}
