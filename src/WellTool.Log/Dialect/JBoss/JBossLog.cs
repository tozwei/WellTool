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

using WellTool.Log.Level;

namespace WellTool.Log.Dialect.JBoss;

/// <summary>
/// JBoss 日志实现
/// </summary>
public class JBossLog : AbstractLog
{
    private readonly object _logger;
    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    /// <param name="logger">JBoss 日志对象</param>
    public JBossLog(string name, object logger) : base(name)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">JBoss 日志对象</param>
    public JBossLog(object logger) : this("JBoss", logger)
    {
    }
    
    /// <summary>
    /// 日志对象
    /// </summary>
    public object Logger => _logger;
    
    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public override bool IsEnabled(WellTool.Log.Level.Level level)
    {
        try
        {
            var method = _logger.GetType().GetMethod("isEnabled", new[] { typeof(object) });
            if (method != null)
            {
                return (bool)method.Invoke(_logger, new[] { level.ToString() });
            }
        }
        catch
        {
        }
        return false;
    }
    
    /// <summary>
    /// 记录指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(WellTool.Log.Level.Level level, Exception t, string format, params object[] arguments)
    {
        try
        {
            var methodName = GetMethodName(level);
            var method = _logger.GetType().GetMethod(methodName, new[] { typeof(string), typeof(Exception) });
            if (method != null)
            {
                var message = format == null ? string.Empty : string.Format(format, arguments);
                method.Invoke(_logger, new object[] { message, t });
            }
        }
        catch
        {
        }
    }
    
    /// <summary>
    /// 记录指定级别的日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(string fqcn, WellTool.Log.Level.Level level, Exception t, string format, params object[] arguments)
    {
        Log(level, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Trace级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Trace(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Trace, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Debug级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Debug(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Debug, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Info级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Info(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Info, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Warn级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Warn(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Warn, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Error级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Error(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Error, t, format, arguments);
    }
    
    /// <summary>
    /// 记录Fatal级别的日志
    /// </summary>
    /// <param name="t">异常</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Fatal(Exception t, string format, params object[] arguments)
    {
        Log(WellTool.Log.Level.Level.Fatal, t, format, arguments);
    }
    
    /// <summary>
    /// 获取日志方法名
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>方法名</returns>
    private string GetMethodName(WellTool.Log.Level.Level level)
    {
        return level switch
        {
            WellTool.Log.Level.Level.Trace => "trace",
            WellTool.Log.Level.Level.Debug => "debug",
            WellTool.Log.Level.Level.Info => "info",
            WellTool.Log.Level.Level.Warn => "warn",
            WellTool.Log.Level.Level.Error => "error",
            WellTool.Log.Level.Level.Fatal => "error",
            _ => "info"
        };
    }
}