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

using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WellTool.AI.Core
{
    /// <summary>
    /// 基础AIService，包含基公共参数和公共方法
    /// </summary>
    public class BaseAIService
    {
        protected readonly AIConfig config;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config">AI配置</param>
        public BaseAIService(AIConfig config)
        {
            this.config = config;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="endpoint">请求节点</param>
        /// <returns>请求响应</returns>
        protected async Task<string> SendGetAsync(string endpoint)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(config.GetApiUrl() + endpoint);
                request.Method = "GET";
                request.Accept = "application/json";
                request.Headers["Authorization"] = "Bearer " + config.GetApiKey();
                request.Timeout = config.GetTimeout();

                if (config.GetHasProxy())
                {
                    request.Proxy = config.GetProxy();
                }

                using var response = (HttpWebResponse)await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                return await reader.ReadToEndAsync();
            }
            catch (System.Exception e)
            {
                throw new AIException("Failed to send GET request: " + e.Message, e);
            }
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="endpoint">请求节点</param>
        /// <param name="paramJson">请求参数json</param>
        /// <returns>请求响应</returns>
        protected async Task<string> SendPostAsync(string endpoint, string paramJson)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(config.GetApiUrl() + endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Headers["Authorization"] = "Bearer " + config.GetApiKey();
                request.Timeout = config.GetTimeout();

                if (config.GetHasProxy())
                {
                    request.Proxy = config.GetProxy();
                }

                using var stream = await request.GetRequestStreamAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync(paramJson);
                await writer.FlushAsync();

                using var response = (HttpWebResponse)await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                return await reader.ReadToEndAsync();
            }
            catch (System.Exception e)
            {
                throw new AIException("Failed to send POST request: " + e.Message, e);
            }
        }

        /// <summary>
        /// 支持流式返回的 POST 请求
        /// </summary>
        /// <param name="endpoint">请求地址</param>
        /// <param name="paramJson">请求参数JSON</param>
        /// <param name="callback">流式数据回调函数</param>
        protected async Task SendPostStreamAsync(string endpoint, string paramJson, System.Action<string> callback)
        {
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(config.GetApiUrl() + endpoint);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Headers["Authorization"] = "Bearer " + config.GetApiKey();
                request.Timeout = config.GetTimeout();
                request.ReadWriteTimeout = config.GetReadTimeout();

                if (config.GetHasProxy())
                {
                    request.Proxy = config.GetProxy();
                }

                using var stream = await request.GetRequestStreamAsync();
                using var writer = new StreamWriter(stream);
                await writer.WriteAsync(paramJson);
                await writer.FlushAsync();

                response = (HttpWebResponse)await request.GetResponseAsync();
                using var reader = new StreamReader(response.GetResponseStream()!);
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    callback(line);
                }
            }
            catch (System.Exception e)
            {
                callback($"{{\"error\": \"{e.Message}\"}}");
            }
            finally
            {
                response?.Close();
            }
        }
    }
}
