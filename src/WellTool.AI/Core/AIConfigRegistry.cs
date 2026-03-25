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

using System.Collections.Concurrent;
using System.Reflection;

namespace WellTool.AI.Core
{
    /// <summary>
    /// AIConfig实现类的加载器
    /// </summary>
    public class AIConfigRegistry
    {
        private static readonly ConcurrentDictionary<string, Type> configClasses = new ConcurrentDictionary<string, Type>();

        // 加载所有 AIConfig 实现类
        static AIConfigRegistry()
        {
            LoadConfigClasses();
        }

        private static void LoadConfigClasses()
        {
            // 直接添加已知的配置类，确保它们被正确加载
            AddConfigClass(typeof(WellTool.AI.Model.OpenAI.OpenAIConfig));
            AddConfigClass(typeof(WellTool.AI.Model.DeepSeek.DeepSeekConfig));
            AddConfigClass(typeof(WellTool.AI.Model.Doubao.DoubaoConfig));
            AddConfigClass(typeof(WellTool.AI.Model.Gemini.GeminiConfig));
            AddConfigClass(typeof(WellTool.AI.Model.Grok.GrokConfig));
            AddConfigClass(typeof(WellTool.AI.Model.Hutool.HutoolConfig));
            AddConfigClass(typeof(WellTool.AI.Model.Ollama.OllamaConfig));

            // 仍然尝试从所有程序集中加载，以支持动态加载
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (typeof(AIConfig).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            AddConfigClass(type);
                        }
                    }
                }
                catch { }
            }
        }

        private static void AddConfigClass(Type type)
        {
            try
            {
                var instance = Activator.CreateInstance(type) as AIConfig;
                if (instance != null)
                {
                    var modelName = instance.GetModelName().ToLower();
                    configClasses.TryAdd(modelName, type);
                }
            }
            catch { }
        }

        /// <summary>
        /// 根据模型名称获取AIConfig实现类
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <returns>AIConfig实现类</returns>
        public static Type GetConfigClass(string modelName)
        {
            configClasses.TryGetValue(modelName.ToLower(), out var type);
            return type;
        }
    }
}
