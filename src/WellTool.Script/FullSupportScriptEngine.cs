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

using Jint;
using Jint.Native;
using System.Text.Json;

namespace WellTool.Script;

/// <summary>
/// 全功能脚本引擎类，支持编译和调用功能
/// </summary>
public class FullSupportScriptEngine
{
    /// <summary>
    /// Jint 引擎
    /// </summary>
    private readonly Engine _engine;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="engine">Jint 引擎</param>
    public FullSupportScriptEngine(Engine engine)
    {
        _engine = engine;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    public FullSupportScriptEngine(string nameOrExtOrMime)
    {
        // 目前只支持 JavaScript
        if (!IsJavaScript(nameOrExtOrMime))
        {
            throw new ScriptRuntimeException($"脚本类型 [{nameOrExtOrMime}] 不支持！");
        }

        _engine = new Engine();
    }

    /// <summary>
    /// 检查是否为 JavaScript
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    /// <returns>是否为 JavaScript</returns>
    private bool IsJavaScript(string nameOrExtOrMime)
    {
        return nameOrExtOrMime.Equals("javascript", StringComparison.OrdinalIgnoreCase) ||
               nameOrExtOrMime.Equals("js", StringComparison.OrdinalIgnoreCase) ||
               nameOrExtOrMime.Equals("text/javascript", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 执行脚本
    /// </summary>
    /// <param name="script">脚本代码</param>
    /// <returns>执行结果</returns>
    public object? Execute(string script)
    {
        try
        {
            var result = _engine.Evaluate(script);
            return result.ToObject();
        }
        catch (System.Exception ex)
        {
            throw new ScriptRuntimeException("执行脚本失败", ex);
        }
    }

    /// <summary>
    /// 执行脚本并返回指定类型的结果
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="script">脚本代码</param>
    /// <returns>执行结果</returns>
    public T? Execute<T>(string script)
    {
        var result = Execute(script);
        if (result == null)
        {
            return default;
        }
        return (T)result;
    }

    /// <summary>
    /// 调用脚本中的函数
    /// </summary>
    /// <param name="functionName">函数名</param>
    /// <param name="args">参数</param>
    /// <returns>函数执行结果</returns>
    public object? InvokeFunction(string functionName, params object?[] args)
    {
        try
        {
            var result = _engine.Evaluate($"{functionName}({string.Join(", ", args.Select(arg => JsonSerializer.Serialize(arg)))}");
            return result.ToObject();
        }
        catch (System.Exception ex)
        {
            throw new ScriptRuntimeException("调用脚本函数失败", ex);
        }
    }

    /// <summary>
    /// 调用脚本中的函数并返回指定类型的结果
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="functionName">函数名</param>
    /// <param name="args">参数</param>
    /// <returns>函数执行结果</returns>
    public T? InvokeFunction<T>(string functionName, params object?[] args)
    {
        var result = InvokeFunction(functionName, args);
        if (result == null)
        {
            return default;
        }
        return (T)result;
    }

    /// <summary>
    /// 向脚本引擎中添加变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <param name="value">变量值</param>
    public void SetVariable(string name, object? value)
    {
        _engine.SetValue(name, value);
    }

    /// <summary>
    /// 从脚本引擎中获取变量
    /// </summary>
    /// <param name="name">变量名</param>
    /// <returns>变量值</returns>
    public object? GetVariable(string name)
    {
        var value = _engine.GetValue(name);
        return value.ToObject();
    }

    /// <summary>
    /// 从脚本引擎中获取变量并转换为指定类型
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="name">变量名</param>
    /// <returns>变量值</returns>
    public T? GetVariable<T>(string name)
    {
        var value = GetVariable(name);
        if (value == null)
        {
            return default;
        }
        return (T)value;
    }

    /// <summary>
    /// 获取 Jint 引擎实例
    /// </summary>
    /// <returns>Jint 引擎实例</returns>
    public Engine GetEngine()
    {
        return _engine;
    }
}