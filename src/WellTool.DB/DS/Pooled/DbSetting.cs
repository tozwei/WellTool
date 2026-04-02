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

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.DB.DS.Pooled;

/// <summary>
/// 数据库配置设置
/// </summary>
public class DbSetting
{
    /// <summary>
    /// 配置映射
    /// </summary>
    private readonly Dictionary<string, string> _config = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configPath">配置文件路径</param>
    public DbSetting(string configPath)
    {
        Load(configPath);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="reader">配置文件读取器</param>
    public DbSetting(TextReader reader)
    {
        Load(reader);
    }

    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="configPath">配置文件路径</param>
    private void Load(string configPath)
    {
        using var reader = new StreamReader(configPath);
        Load(reader);
    }

    /// <summary>
    /// 加载配置
    /// </summary>
    /// <param name="reader">配置文件读取器</param>
    private void Load(TextReader reader)
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {
                continue;
            }

            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                _config[parts[0].Trim()] = parts[1].Trim();
            }
        }
    }

    /// <summary>
    /// 获取配置值
    /// </summary>
    /// <param name="key">配置键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>配置值</returns>
    public string Get(string key, string defaultValue = null)
    {
        return _config.TryGetValue(key, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// 获取配置值（整数）
    /// </summary>
    /// <param name="key">配置键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>配置值</returns>
    public int GetInt(string key, int defaultValue = 0)
    {
        if (_config.TryGetValue(key, out var value) && int.TryParse(value, out var intValue))
        {
            return intValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// 获取配置值（布尔值）
    /// </summary>
    /// <param name="key">配置键</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>配置值</returns>
    public bool GetBool(string key, bool defaultValue = false)
    {
        if (_config.TryGetValue(key, out var value) && bool.TryParse(value, out var boolValue))
        {
            return boolValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// 获取所有配置键
    /// </summary>
    /// <returns>配置键集合</returns>
    public IEnumerable<string> GetKeys()
    {
        return _config.Keys;
    }

    /// <summary>
    /// 检查是否包含指定配置键
    /// </summary>
    /// <param name="key">配置键</param>
    /// <returns>是否包含</returns>
    public bool ContainsKey(string key)
    {
        return _config.ContainsKey(key);
    }
}
