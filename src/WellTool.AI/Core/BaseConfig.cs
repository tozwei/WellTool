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
using System.Net;

namespace WellTool.AI.Core
{
    /// <summary>
    /// Config基础类，定义模型配置的基本属性
    /// </summary>
    public class BaseConfig : AIConfig
    {
        //apiKey
        protected volatile string? apiKey;
        //API请求地址
        protected volatile string? apiUrl;
        //具体模型
        protected volatile string? model;
        //动态扩展字段
        protected ConcurrentDictionary<string, object> additionalConfig = new ConcurrentDictionary<string, object>();
        //连接超时时间
        protected volatile int timeout = 180000;
        //读取超时时间
        protected volatile int readTimeout = 300000;
        //是否设置代理
        protected volatile bool hasProxy = false;
        //代理设置
        protected volatile IWebProxy? proxy;

        /// <summary>
        /// 获取模型名称
        /// </summary>
        /// <returns>模型名称</returns>
        public virtual string GetModelName()
        {
            return GetType().Name;
        }

        /// <summary>
        /// 设置apiKey
        /// </summary>
        /// <param name="apiKey">apiKey</param>
        public void SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// 获取apiKey
        /// </summary>
        /// <returns>apiKey</returns>
        public string GetApiKey()
        {
            return apiKey ?? string.Empty;
        }

        /// <summary>
        /// 设置apiUrl
        /// </summary>
        /// <param name="apiUrl">api请求地址</param>
        public void SetApiUrl(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        /// <summary>
        /// 获取apiUrl
        /// </summary>
        /// <returns>apiUrl</returns>
        public string GetApiUrl()
        {
            return apiUrl ?? string.Empty;
        }

        /// <summary>
        /// 设置model
        /// </summary>
        /// <param name="model">model</param>
        public void SetModel(string model)
        {
            this.model = model;
        }

        /// <summary>
        /// 返回model
        /// </summary>
        /// <returns>model</returns>
        public string GetModel()
        {
            return model ?? string.Empty;
        }

        /// <summary>
        /// 设置动态参数
        /// </summary>
        /// <param name="key">参数字段</param>
        /// <param name="value">参数值</param>
        public void PutAdditionalConfigByKey(string key, object value)
        {
            additionalConfig[key] = value;
        }

        /// <summary>
        /// 获取动态参数
        /// </summary>
        /// <param name="key">参数字段</param>
        /// <returns>参数值</returns>
        public object GetAdditionalConfigByKey(string key)
        {
            additionalConfig.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// 获取动态参数列表
        /// </summary>
        /// <returns>参数列表Map</returns>
        public Dictionary<string, object> GetAdditionalConfigMap()
        {
            return additionalConfig.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// 获取连接超时时间
        /// </summary>
        /// <returns>timeout</returns>
        public int GetTimeout()
        {
            return timeout;
        }

        /// <summary>
        /// 设置连接超时时间
        /// </summary>
        /// <param name="timeout">连接超时时间</param>
        public void SetTimeout(int timeout)
        {
            this.timeout = timeout;
        }

        /// <summary>
        /// 获取读取超时时间
        /// </summary>
        /// <returns>readTimeout</returns>
        public int GetReadTimeout()
        {
            return readTimeout;
        }

        /// <summary>
        /// 设置读取超时时间
        /// </summary>
        /// <param name="readTimeout">读取超时时间</param>
        public void SetReadTimeout(int readTimeout)
        {
            this.readTimeout = readTimeout;
        }

        /// <summary>
        /// 获取是否使用代理
        /// </summary>
        /// <returns>hasProxy</returns>
        public bool GetHasProxy()
        {
            return hasProxy;
        }

        /// <summary>
        /// 设置是否使用代理
        /// </summary>
        /// <param name="hasProxy">是否使用代理</param>
        public void SetHasProxy(bool hasProxy)
        {
            this.hasProxy = hasProxy;
        }

        /// <summary>
        /// 获取代理配置
        /// </summary>
        /// <returns>proxy</returns>
        public IWebProxy GetProxy()
        {
            return proxy!;
        }

        /// <summary>
        /// 设置代理配置
        /// </summary>
        /// <param name="proxy">连接超时时间</param>
        public void SetProxy(IWebProxy proxy)
        {
            this.proxy = proxy;
        }
    }
}
