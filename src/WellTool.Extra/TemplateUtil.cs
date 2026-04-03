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

using System.Text.RegularExpressions;

namespace WellTool.Extra;

/// <summary>
/// 模板工具类
/// </summary>
public class TemplateUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static TemplateUtil Instance { get; } = new TemplateUtil();

    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="template">模板字符串</param>
    /// <param name="parameters">参数</param>
    /// <returns>渲染后的字符串</returns>
    public string Render(string template, IDictionary<string, object> parameters)
    {
        try
        {
            var result = template;
            foreach (var param in parameters)
            {
                var pattern = $"{{{{{param.Key}}}}}";
                result = Regex.Replace(result, pattern, param.Value?.ToString() ?? string.Empty);
            }
            return result;
        }
        catch (System.Exception ex)
        {
            throw new TemplateException("渲染模板失败", ex);
        }
    }

    /// <summary>
    /// 从文件渲染模板
    /// </summary>
    /// <param name="filePath">模板文件路径</param>
    /// <param name="parameters">参数</param>
    /// <returns>渲染后的字符串</returns>
    public string RenderFromFile(string filePath, IDictionary<string, object> parameters)
    {
        try
        {
            var template = File.ReadAllText(filePath);
            return Render(template, parameters);
        }
        catch (System.Exception ex)
        {
            throw new TemplateException("从文件渲染模板失败", ex);
        }
    }
}

/// <summary>
/// 模板异常
/// </summary>
public class TemplateException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public TemplateException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public TemplateException(string message, Exception innerException) : base(message, innerException)
    {}
}