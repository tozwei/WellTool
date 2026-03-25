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

using WellTool.AI.Core;

namespace WellTool.AI.Model.Ollama
{
    /// <summary>
    /// Ollama支持的扩展接口
    /// </summary>
    public interface OllamaService : AIService
    {
        /// <summary>
        /// 列出所有模型列表
        /// </summary>
        /// <returns>model列表</returns>
        string Models();

        /// <summary>
        /// 拉取模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>拉取结果</returns>
        string PullModel(string model);

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>删除结果</returns>
        string DeleteModel(string model);
    }
}
