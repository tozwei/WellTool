using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace WellTool.Extra.Template.Engine.Freemarker
{
    /// <summary>
    /// Freemarker模板实现
    /// </summary>
    public class FreemarkerTemplate : AbstractTemplate, ISerializable
    {
        private readonly dynamic _rawTemplate;

        /// <summary>
        /// 包装Freemarker模板
        /// </summary>
        /// <param name="freemarkerTemplate">Freemarker的模板对象</param>
        /// <returns>FreemarkerTemplate</returns>
        public static FreemarkerTemplate? Wrap(dynamic freemarkerTemplate)
        {
            return freemarkerTemplate == null ? null : new FreemarkerTemplate(freemarkerTemplate);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="freemarkerTemplate">Freemarker的模板对象</param>
        public FreemarkerTemplate(dynamic freemarkerTemplate)
        {
            _rawTemplate = freemarkerTemplate;
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        protected FreemarkerTemplate(SerializationInfo info, StreamingContext context)
        {
            // 反序列化时需要重新获取模板
        }

        /// <summary>
        /// 获取序列化数据
        /// </summary>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数</param>
        /// <param name="writer">输出</param>
        public override void Render(IDictionary<object, object> bindingMap, TextWriter writer)
        {
            _rawTemplate.Process(bindingMap, writer);
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
