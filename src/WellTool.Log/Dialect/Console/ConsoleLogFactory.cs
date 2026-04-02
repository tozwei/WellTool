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

namespace WellTool.Log.Dialect.Console;

/// <summary>
/// 控制台日志工厂
/// </summary>
public class ConsoleLogFactory : LogFactory
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ConsoleLogFactory() : base("Console")
    {
    }

    /// <summary>
    /// 创建日志对象
    /// </summary>
    /// <param name="name">日志对象名</param>
    /// <returns>日志对象</returns>
    public override ILog CreateLog(string name)
    {
        return new ConsoleLog(name);
    }

    /// <summary>
    /// 创建日志对象
    /// </summary>
    /// <param name="clazz">日志对应类</param>
    /// <returns>日志对象</returns>
    public override ILog CreateLog(Type clazz)
    {
        return new ConsoleLog(clazz);
    }
}