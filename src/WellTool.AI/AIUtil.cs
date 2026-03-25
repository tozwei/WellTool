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

namespace WellTool.AI
{
    /// <summary>
    /// AI工具类
    /// </summary>
    public class AIUtil
    {
        /// <summary>
        /// 创建AI配置构建器
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <returns>AI配置构建器</returns>
        public static AIConfigBuilder CreateConfigBuilder(string modelName)
        {
            return new AIConfigBuilder(modelName);
        }

        /// <summary>
        /// 获取AI服务
        /// </summary>
        /// <param name="config">AI配置</param>
        /// <returns>AI服务实例</returns>
        public static AIService GetService(AIConfig config)
        {
            return AIServiceFactory.GetAIService(config);
        }

        /// <summary>
        /// 获取AI服务
        /// </summary>
        /// <param name="config">AI配置</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>AI服务实例</returns>
        /// <typeparam name="T">服务类型</typeparam>
        public static T GetService<T>(AIConfig config, Type serviceType) where T : AIService
        {
            return AIServiceFactory.GetAIService<T>(config, serviceType);
        }
    }
}
