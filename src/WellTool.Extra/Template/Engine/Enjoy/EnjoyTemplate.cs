using System.Collections.Generic;
using System.IO;

namespace WellTool.Extra.Template.Engine.Enjoy
{
    /// <summary>
    /// Enjoy模板实现
    /// </summary>
    public class EnjoyTemplate : AbstractTemplate
    {
        private readonly string _content;

        /// <summary>
        /// 包装Enjoy模板
        /// </summary>
        /// <param name="content">模板内容</param>
        /// <returns>EnjoyTemplate</returns>
        public static EnjoyTemplate Wrap(string content)
        {
            return content == null ? null! : new EnjoyTemplate(content);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="content">模板内容</param>
        public EnjoyTemplate(string content)
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
                result = result.Replace($"#{key}", value);
            }
            writer.Write(result);
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="output">输出</param>
        public override void Render(IDictionary<object, object> bindingMap, Stream output)
        {
            using var writer = new StreamWriter(output);
            Render(bindingMap, writer);
        }
    }
}
