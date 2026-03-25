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
    /// AI配置接口
    /// </summary>
    public interface AIConfig
    {
        /// <summary>
        /// 获取模型（厂商）名称
        /// </summary>
        /// <returns>模型（厂商）名称</returns>
        string GetModelName()
        {
            return GetType().Name;
        }

        /// <summary>
        /// 设置apiKey
        /// </summary>
        /// <param name="apiKey">apiKey</param>
        void SetApiKey(string apiKey);

        /// <summary>
        /// 获取apiKey
        /// </summary>
        /// <returns>apiKey</returns>
        string GetApiKey();

        /// <summary>
        /// 设置apiUrl
        /// </summary>
        /// <param name="apiUrl">api请求地址</param>
        void SetApiUrl(string apiUrl);

        /// <summary>
        /// 获取apiUrl
        /// </summary>
        /// <returns>apiUrl</returns>
        string GetApiUrl();

        /// <summary>
        /// 设置model
        /// </summary>
        /// <param name="model">model</param>
        void SetModel(string model);

        /// <summary>
        /// 返回model
        /// </summary>
        /// <returns>model</returns>
        string GetModel();

        /// <summary>
        /// 设置动态参数
        /// </summary>
        /// <param name="key">参数字段</param>
        /// <param name="value">参数值</param>
        void PutAdditionalConfigByKey(string key, object value);

        /// <summary>
        /// 获取动态参数
        /// </summary>
        /// <param name="key">参数字段</param>
        /// <returns>参数值</returns>
        object GetAdditionalConfigByKey(string key);

        /// <summary>
        /// 获取动态参数列表
        /// </summary>
        /// <returns>参数列表Map</returns>
        Dictionary<string, object> GetAdditionalConfigMap();

        /// <summary>
        /// 设置连接超时时间
        /// </summary>
        /// <param name="timeout">连接超时时间</param>
        void SetTimeout(int timeout);

        /// <summary>
        /// 获取连接超时时间
        /// </summary>
        /// <returns>timeout</returns>
        int GetTimeout();

        /// <summary>
        /// 设置读取超时时间
        /// </summary>
        /// <param name="readTimeout">连接超时时间</param>
        void SetReadTimeout(int readTimeout);

        /// <summary>
        /// 获取读取超时时间
        /// </summary>
        /// <returns>readTimeout</returns>
        int GetReadTimeout();

        /// <summary>
        /// 获取是否使用代理
        /// </summary>
        /// <returns>hasProxy</returns>
        bool GetHasProxy();

        /// <summary>
        /// 设置是否使用代理
        /// </summary>
        /// <param name="hasProxy">是否使用代理</param>
        void SetHasProxy(bool hasProxy);

        /// <summary>
        /// 获取代理配置
        /// </summary>
        /// <returns>proxy</returns>
        IWebProxy GetProxy();

        /// <summary>
        /// 设置代理配置
        /// </summary>
        /// <param name="proxy">连接超时时间</param>
        void SetProxy(IWebProxy proxy);
    }
}
