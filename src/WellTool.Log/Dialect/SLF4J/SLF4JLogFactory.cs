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

using System.Reflection;

namespace WellTool.Log.Dialect.SLF4J;

/// <summary>
/// SLF4J 日志工厂
/// </summary>
public class SLF4JLogFactory : LogFactory
{
    private static readonly Type LoggerFactoryType;
    private static readonly MethodInfo GetLoggerMethod;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    public SLF4JLogFactory() : base("SLF4J")
    {
    }
    
    static SLF4JLogFactory()
    {
        try
        {
            // 尝试加载 SLF4J 相关类型
            LoggerFactoryType = Type.GetType("org.slf4j.LoggerFactory, slf4j-api");
            if (LoggerFactoryType != null)
            {
                GetLoggerMethod = LoggerFactoryType.GetMethod("getLogger", new[] { typeof(Type) });
            }
        }
        catch
        {
        }
    }
    
    /// <summary>
    /// 是否可用
    /// </summary>
    public static bool IsAvailable => LoggerFactoryType != null && GetLoggerMethod != null;
    
    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(Type type)
    {
        if (!IsAvailable)
        {
            return null;
        }
        
        try
        {
            var logger = GetLoggerMethod.Invoke(null, new object[] { type });
            return new SLF4JLog(type.FullName, logger);
        }
        catch
        {
            return null;
        }
    }
    
    /// <summary>
    /// 创建日志实例
    /// </summary>
    /// <param name="name">名称</param>
    /// <returns>日志实例</returns>
    public override ILog CreateLog(string name)
    {
        if (!IsAvailable)
        {
            return null;
        }
        
        try
        {
            var getLoggerMethod = LoggerFactoryType.GetMethod("getLogger", new[] { typeof(string) });
            if (getLoggerMethod != null)
            {
                var logger = getLoggerMethod.Invoke(null, new object[] { name });
                return new SLF4JLog(name, logger);
            }
        }
        catch
        {
        }
        return null;
    }
}