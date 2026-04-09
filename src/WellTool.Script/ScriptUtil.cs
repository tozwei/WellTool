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

namespace WellTool.Script;

/// <summary>
/// 脚本工具类
/// </summary>
public class ScriptUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ScriptUtil Instance { get; } = new ScriptUtil();

    /// <summary>
    /// 执行 JavaScript 代码
    /// </summary>
    /// <param name="script">JavaScript 代码</param>
    /// <returns>执行结果</returns>
    public object? ExecuteJavaScript(string script)
    {
        var engine = new JavaScriptEngine();
        return engine.Execute(script);
    }

    /// <summary>
    /// 执行 JavaScript 代码并返回指定类型的结果
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="script">JavaScript 代码</param>
    /// <returns>执行结果</returns>
    public T? ExecuteJavaScript<T>(string script)
    {
        var engine = new JavaScriptEngine();
        return engine.Execute<T>(script);
    }

    /// <summary>
    /// 创建 JavaScript 引擎
    /// </summary>
    /// <returns>JavaScript 引擎</returns>
    public JavaScriptEngine CreateJavaScriptEngine()
    {
        return new JavaScriptEngine();
    }

    /// <summary>
    /// 创建全功能脚本引擎
    /// </summary>
    /// <returns>全功能脚本引擎</returns>
    public FullSupportScriptEngine CreateFullSupportScriptEngine()
    {
        return new FullSupportScriptEngine("javascript");
    }

    /// <summary>
    /// 创建全功能脚本引擎
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    /// <returns>全功能脚本引擎</returns>
    public FullSupportScriptEngine CreateFullSupportScriptEngine(string nameOrExtOrMime)
    {
        return new FullSupportScriptEngine(nameOrExtOrMime);
    }

    /// <summary>
    /// 创建全功能脚本引擎
    /// </summary>
    /// <param name="engine">Jint 引擎</param>
    /// <returns>全功能脚本引擎</returns>
    public FullSupportScriptEngine CreateFullSupportScriptEngine(Jint.Engine engine)
    {
        return new FullSupportScriptEngine(engine);
    }

    /// <summary>
    /// 创建脚本引擎（静态方法）
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    /// <returns>脚本引擎</returns>
    public static object CreateEngine(string nameOrExtOrMime)
    {
        return Instance.CreateFullSupportScriptEngine(nameOrExtOrMime);
    }

    /// <summary>
    /// 执行脚本（静态方法）
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    /// <param name="script">脚本代码</param>
    /// <returns>执行结果</returns>
    public static object? Eval(string nameOrExtOrMime, string script)
    {
        var engine = Instance.CreateFullSupportScriptEngine(nameOrExtOrMime);
        return engine.Execute(script);
    }

    /// <summary>
    /// 执行脚本（静态方法）
    /// </summary>
    /// <param name="nameOrExtOrMime">脚本名或者脚本语言扩展名或者MineType</param>
    /// <param name="script">脚本代码</param>
    /// <param name="bindings">绑定的变量</param>
    /// <returns>执行结果</returns>
    public static object? Eval(string nameOrExtOrMime, string script, System.Collections.Generic.Dictionary<string, object> bindings)
    {
        var engine = Instance.CreateFullSupportScriptEngine(nameOrExtOrMime);
        foreach (var binding in bindings)
        {
            engine.SetValue(binding.Key, binding.Value);
        }
        return engine.Execute(script);
    }
}
