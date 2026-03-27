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
/// JavaScript 引擎
/// </summary>
public class JavaScriptEngine
{
    /// <summary>
    /// 执行 JavaScript 代码
    /// </summary>
    /// <param name="script">JavaScript 代码</param>
    /// <returns>执行结果</returns>
    public object? Execute(string script)
    {
        try
        {
            // 使用 Jint 库来执行 JavaScript 代码
            var engine = new Jint.Engine();
            return engine.Execute(script).GetCompletionValue().ToObject();
        }
        catch (Exception ex)
        {
            throw new ScriptRuntimeException("执行 JavaScript 代码失败", ex);
        }
    }

    /// <summary>
    /// 执行 JavaScript 代码并返回指定类型的结果
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="script">JavaScript 代码</param>
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
}
