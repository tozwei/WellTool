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

using System.Net;

namespace WellTool.AI.Core
{
    /// <summary>
    /// 用于AIConfig的创建，创建同时支持链式设置参数
    /// </summary>
    public class AIConfigBuilder
    {
        private readonly AIConfig config;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="modelName">模型厂商的名称（注意不是指具体的模型）</param>
        public AIConfigBuilder(string modelName)
        {
            // 获取配置类
            var configClass = AIConfigRegistry.GetConfigClass(modelName);
            if (configClass == null)
            {
                throw new ArgumentException("Unsupported model: " + modelName);
            }

            // 使用反射创建实例
            config = (AIConfig)Activator.CreateInstance(configClass)!;
        }

        /// <summary>
        /// 设置apiKey
        /// </summary>
        /// <param name="apiKey">apiKey</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetApiKey(string apiKey)
        {
            if (apiKey != null)
            {
                config.SetApiKey(apiKey);
            }
            return this;
        }

        /// <summary>
        /// 设置AI模型请求API接口的地址，不设置为默认值
        /// </summary>
        /// <param name="apiUrl">API接口地址</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetApiUrl(string apiUrl)
        {
            if (apiUrl != null)
            {
                config.SetApiUrl(apiUrl);
            }
            return this;
        }

        /// <summary>
        /// 设置具体的model，不设置为默认值
        /// </summary>
        /// <param name="model">具体model的名称</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetModel(string model)
        {
            if (model != null)
            {
                config.SetModel(model);
            }
            return this;
        }

        /// <summary>
        /// 动态设置Request请求体中的属性字段，每个模型功能支持的字段请参照对应的官方文档
        /// </summary>
        /// <param name="key">Request中的支持的属性名</param>
        /// <param name="value">设置的属性值</param>
        /// <returns>config</returns>
        public AIConfigBuilder PutAdditionalConfig(string key, object value)
        {
            if (value != null)
            {
                config.PutAdditionalConfigByKey(key, value);
            }
            return this;
        }

        /// <summary>
        /// 设置连接超时时间，不设置为默认值
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetTimeout(int timeout)
        {
            if (timeout > 0)
            {
                config.SetTimeout(timeout);
            }
            return this;
        }

        /// <summary>
        /// 设置读取超时时间，不设置为默认值
        /// </summary>
        /// <param name="readTimeout">读取超时时间</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetReadTimeout(int readTimeout)
        {
            if (readTimeout > 0)
            {
                config.SetReadTimeout(readTimeout);
            }
            return this;
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="proxy">代理设置</param>
        /// <returns>config</returns>
        public AIConfigBuilder SetProxy(IWebProxy proxy)
        {
            if (proxy != null)
            {
                config.SetHasProxy(true);
                config.SetProxy(proxy);
            }
            return this;
        }

        /// <summary>
        /// 返回config实例
        /// </summary>
        /// <returns>config</returns>
        public AIConfig Build()
        {
            return config;
        }
    }
}
