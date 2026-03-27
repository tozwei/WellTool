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
/// 脚本运行时异常
/// </summary>
public class ScriptRuntimeException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ScriptRuntimeException()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常信息</param>
    public ScriptRuntimeException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常信息</param>
    /// <param name="innerException">内部异常</param>
    public ScriptRuntimeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
