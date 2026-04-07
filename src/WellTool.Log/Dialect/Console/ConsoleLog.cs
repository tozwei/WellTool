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

using System;
using WellTool.Log.Level;

namespace WellTool.Log.Dialect.Console;

/// <summary>
/// 字符串扩展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 替换第一个匹配的字符串
    /// </summary>
    /// <param name="str">原始字符串</param>
    /// <param name="oldValue">要替换的旧值</param>
    /// <param name="newValue">替换的新值</param>
    /// <returns>替换后的字符串</returns>
    public static string ReplaceFirst(this string str, string oldValue, string newValue)
    {
        int index = str.IndexOf(oldValue);
        if (index == -1)
        {
            return str;
        }
        return str.Substring(0, index) + newValue + str.Substring(index + oldValue.Length);
    }
}

/// <summary>
/// 控制台日志实现
/// </summary>
public class ConsoleLog : AbstractLog
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public ConsoleLog(string name) : base(name)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type">日志类型</param>
    public ConsoleLog(Type type) : base(type)
    {
    }

    /// <summary>
    /// 是否启用指定级别的日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <returns>是否启用</returns>
    public override bool IsEnabled(Level.Level level)
    {
        return true; // 控制台日志默认全部启用
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(Level.Level level, string format, params object[] arguments)
    {
        Log(level, null, format, arguments);
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(Level.Level level, Exception t, string format, params object[] arguments)
    {
        Log(typeof(ConsoleLog).FullName, level, t, format, arguments);
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="fqcn">完全限定类名</param>
    /// <param name="level">日志级别</param>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Log(string fqcn, Level.Level level, Exception t, string format, params object[] arguments)
        {
            // 处理 {} 占位符格式，转换为 C# 风格的 {0}, {1}...
            var formattedFormat = format;
            var message = "";
            if (formattedFormat != null)
            {
                if (formattedFormat.Contains("{}"))
                {
                    int index = 0;
                    while (formattedFormat.Contains("{}"))
                    {
                        formattedFormat = formattedFormat.ReplaceFirst("{}", $"{{{index}}}");
                        index++;
                    }
                }
                try
                {
                    message = string.Format(formattedFormat, arguments);
                }
                catch (FormatException)
                {
                    // 如果格式化失败，直接使用原始格式和参数
                    message = $"{formattedFormat} {string.Join(", ", arguments ?? new object[0])}";
                }
            }
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] [{Name}] {message}";

        // 根据日志级别设置控制台颜色
        System.ConsoleColor originalColor = System.Console.ForegroundColor;
        try
        {
            switch (level)
            {
                case Level.Level.Trace:
                    System.Console.ForegroundColor = System.ConsoleColor.Gray;
                    break;
                case Level.Level.Debug:
                    System.Console.ForegroundColor = System.ConsoleColor.Blue;
                    break;
                case Level.Level.Info:
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                    break;
                case Level.Level.Warn:
                    System.Console.ForegroundColor = System.ConsoleColor.Yellow;
                    break;
                case Level.Level.Error:
                case Level.Level.Fatal:
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    break;
            }

            // 输出日志
            System.Console.WriteLine(logMessage);

            // 输出异常信息
            if (t != null)
            {
                System.Console.WriteLine(t.ToString());
            }
        }
        finally
        {
            // 恢复控制台颜色
            System.Console.ForegroundColor = originalColor;
        }
    }

    // 以下是各个级别的日志方法实现

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Trace(string format, params object[] arguments)
    {
        Log(Level.Level.Trace, format, arguments);
    }

    /// <summary>
    /// 打印Trace级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Trace(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Trace, t, format, arguments);
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Debug(string format, params object[] arguments)
    {
        Log(Level.Level.Debug, format, arguments);
    }

    /// <summary>
    /// 打印Debug级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Debug(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Debug, t, format, arguments);
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Info(string format, params object[] arguments)
    {
        Log(Level.Level.Info, format, arguments);
    }

    /// <summary>
    /// 打印Info级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Info(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Info, t, format, arguments);
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Warn(string format, params object[] arguments)
    {
        Log(Level.Level.Warn, format, arguments);
    }

    /// <summary>
    /// 打印Warn级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Warn(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Warn, t, format, arguments);
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Error(string format, params object[] arguments)
    {
        Log(Level.Level.Error, format, arguments);
    }

    /// <summary>
    /// 打印Error级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Error(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Error, t, format, arguments);
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Fatal(string format, params object[] arguments)
    {
        Log(Level.Level.Fatal, format, arguments);
    }

    /// <summary>
    /// 打印Fatal级别日志
    /// </summary>
    /// <param name="t">异常信息</param>
    /// <param name="format">消息格式</param>
    /// <param name="arguments">消息参数</param>
    public override void Fatal(Exception t, string format, params object[] arguments)
    {
        Log(Level.Level.Fatal, t, format, arguments);
    }
}