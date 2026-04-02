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

namespace WellTool.Core.Lang.Ansi
{
    /// <summary>
    /// ANSI可转义节点接口，实现为ANSI颜色等
    /// <p>来自Spring Boot</p>
    /// </summary>
    public interface IAnsiElement
    {
        /// <summary>
        /// 获取ANSI转义编码
        /// </summary>
        /// <returns>ANSI转义编码</returns>
        string ToString();

        /// <summary>
        /// 获取ANSI代码，默认返回-1
        /// </summary>
        /// <returns>ANSI代码</returns>
        int GetCode()
        {
            return -1;
        }
    }
}