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
using WellTool.AI.Core;

namespace WellTool.AI
{
    /// <summary>
    /// 创建AIModelService的工厂类
    /// </summary>
    public class AIServiceFactory
    {
        private static readonly ConcurrentDictionary<string, AIServiceProvider> providers = new ConcurrentDictionary<string, AIServiceProvider>();

        // 加载所有 AIServiceProvider 实现类
        static AIServiceFactory()
        {
            LoadProviders();
        }

        private static void LoadProviders()
        {
            // 直接添加已知的服务提供者类，确保它们被正确加载
            AddProvider(typeof(WellTool.AI.Model.OpenAI.OpenAIServiceProvider));
            AddProvider(typeof(WellTool.AI.Model.DeepSeek.DeepSeekProvider));
            AddProvider(typeof(WellTool.AI.Model.Doubao.DoubaoProvider));
            AddProvider(typeof(WellTool.AI.Model.Gemini.GeminiProvider));
            AddProvider(typeof(WellTool.AI.Model.Grok.GrokProvider));
            AddProvider(typeof(WellTool.AI.Model.Hutool.HutoolProvider));
            AddProvider(typeof(WellTool.AI.Model.Ollama.OllamaProvider));

            // 仍然尝试从所有程序集中加载，以支持动态加载
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (typeof(AIServiceProvider).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            AddProvider(type);
                        }
                    }
                }
                catch { }
            }
        }

        private static void AddProvider(Type type)
        {
            try
            {
                var instance = Activator.CreateInstance(type) as AIServiceProvider;
                if (instance != null)
                {
                    var serviceName = instance.GetServiceName().ToLower();
                    providers.TryAdd(serviceName, instance);
                }
            }
            catch { }
        }

        /// <summary>
        /// 获取AI服务
        /// </summary>
        /// <param name="config">AIConfig配置</param>
        /// <returns>AI服务实例</returns>
        public static AIService GetAIService(AIConfig config)
        {
            return GetAIService<AIService>(config, typeof(AIService));
        }

        /// <summary>
        /// 获取AI服务
        /// </summary>
        /// <param name="config">AIConfig配置</param>
        /// <param name="clazz">AI服务类</param>
        /// <returns>clazz对应的AI服务类实例</returns>
        /// <typeparam name="T">AI服务类</typeparam>
        public static T GetAIService<T>(AIConfig config, Type clazz) where T : AIService
        {
            var modelName = config.GetModelName();
            // 去掉Config后缀，以便正确匹配服务提供者
            if (modelName.EndsWith("Config"))
            {
                modelName = modelName.Substring(0, modelName.Length - 6);
            }
            var provider = providers.GetValueOrDefault(modelName.ToLower());
            if (provider == null)
            {
                throw new ArgumentException("Unsupported model: " + modelName);
            }

            var service = provider.Create(config);
            if (!clazz.IsInstanceOfType(service))
            {
                throw new AIException("Model service is not of type: " + clazz.Name);
            }

            return (T)service;
        }
    }
}
