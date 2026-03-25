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

namespace WellTool.AI.Model.Gemini
{
    /// <summary>
    /// Gemini支持的扩展接口
    /// </summary>
    public interface GeminiService : AIService
    {
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <returns>生成的内容</returns>
        string Generate(string prompt);

        /// <summary>
        /// 生成内容-SSE流式输出
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <param name="callback">流式数据回调函数</param>
        void Generate(string prompt, System.Action<string> callback);

        /// <summary>
        /// 列出所有模型列表
        /// </summary>
        /// <returns>model列表</returns>
        string Models();
    }
}
