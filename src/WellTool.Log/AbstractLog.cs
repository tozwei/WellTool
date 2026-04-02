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

namespace WellTool.Log;

/// <summary>
/// 日志抽象类，所有日志实现的基类
/// </summary>
public abstract class AbstractLog : ILog
{
    /// <summary>
    /// 日志名称
    /// </summary>
    protected readonly string Name;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public AbstractLog(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type">日志类型</param>
    public AbstractLog(Type type)
    {
        Name = type.FullName;
    }

    /// <summary>
    /// 获取日志名称
    /// </summary>
    /// <returns>日志名称</returns>
    public virtual string GetName()
    {
        return Name;
    }

    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public abstract bool IsEnabled(Level.Level level);

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Log(Level.Level level, string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Log(level, (Exception)arguments[0], format);
        }
        else
        {
            Log(level, null, format, arguments);
        }
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Log(Level.Level level, Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Log(string fqcn, Level.Level level, Exception t, string format, params object[] arguments);

    // 以下是各个级别的日志方法

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Trace(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Trace((Exception)arguments[0], format);
        }
        else
        {
            Trace(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Trace(Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Debug(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Debug((Exception)arguments[0], format);
        }
        else
        {
            Debug(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Debug(Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Info(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Info((Exception)arguments[0], format);
        }
        else
        {
            Info(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Info(Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Warn(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Warn((Exception)arguments[0], format);
        }
        else
        {
            Warn(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Warn(Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Error(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Error((Exception)arguments[0], format);
        }
        else
        {
            Error(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Error(Exception t, string format, params object[] arguments);

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public virtual void Fatal(string format, params object[] arguments)
    {
        if (arguments != null && arguments.Length == 1 && arguments[0] is Exception)
        {
            // 兼容Slf4j中的xxx(String message, Throwable e)
            Fatal((Exception)arguments[0], format);
        }
        else
        {
            Fatal(null, format, arguments);
        }
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public abstract void Fatal(Exception t, string format, params object[] arguments);

    // 以下是各个级别的日志方法（带异常参数）

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Trace(Exception t)
    {
        Trace(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Trace(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Trace, t, format, arguments);
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Debug(Exception t)
    {
        Debug(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Debug(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Debug, t, format, arguments);
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Info(Exception t)
    {
        Info(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Info(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Info, t, format, arguments);
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Warn(Exception t)
    {
        Warn(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Warn(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Warn, t, format, arguments);
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Error(Exception t)
    {
        Error(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Error(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Error, t, format, arguments);
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    public void Fatal(Exception t)
    {
        Fatal(t, t?.Message ?? string.Empty);
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public void Fatal(string fqcn, Exception t, string format, params object[] arguments)
    {
        Log(fqcn, Level.Level.Fatal, t, format, arguments);
    }

    // 以下是各个级别的IsEnabled方法

    /// <summary>
    /// Trace级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsTraceEnabled()
    {
        return IsEnabled(Level.Level.Trace);
    }

    /// <summary>
    /// Debug级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsDebugEnabled()
    {
        return IsEnabled(Level.Level.Debug);
    }

    /// <summary>
    /// Info级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsInfoEnabled()
    {
        return IsEnabled(Level.Level.Info);
    }

    /// <summary>
    /// Warn级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsWarnEnabled()
    {
        return IsEnabled(Level.Level.Warn);
    }

    /// <summary>
    /// Error级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsErrorEnabled()
    {
        return IsEnabled(Level.Level.Error);
    }

    /// <summary>
    /// Fatal级别是否启用
    /// </summary>
    /// <returns>是否启用</returns>
    public bool IsFatalEnabled()
    {
        return IsEnabled(Level.Level.Fatal);
    }
}