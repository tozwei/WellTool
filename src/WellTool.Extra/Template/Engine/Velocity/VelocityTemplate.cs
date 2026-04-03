using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template.Engine.Velocity
{
    /// <summary>
    /// Velocity模板包装
    /// </summary>
    public class VelocityTemplate : AbstractTemplate
    {
        private readonly string _content;
        private string? _charset;

        /// <summary>
        /// 包装Velocity模板
        /// </summary>
        /// <param name="content">模板内容</param>
        /// <returns>VelocityTemplate</returns>
        public static VelocityTemplate Wrap(string content)
        {
            return content == null ? null! : new VelocityTemplate(content);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="content">模板内容</param>
        public VelocityTemplate(string content)
        {
            _content = content;
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="writer">输出</param>
        public override void Render(IDictionary<object, object> bindingMap, TextWriter writer)
        {
            var result = _content;
            foreach (var kvp in bindingMap)
            {
                var key = kvp.Key?.ToString() ?? "";
                var value = kvp.Value?.ToString() ?? "";
                result = result.Replace($"${{{key}}}", value);
                result = result.Replace($"${key}", value);
            }
            writer.Write(result);
            writer.Flush();
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="output">输出</param>
        public override void Render(IDictionary<object, object> bindingMap, Stream output)
        {
            _charset ??= "UTF-8";
            using var writer = new StreamWriter(output, System.Text.Encoding.GetEncoding(_charset));
            Render(bindingMap, writer);
        }
    }
}
