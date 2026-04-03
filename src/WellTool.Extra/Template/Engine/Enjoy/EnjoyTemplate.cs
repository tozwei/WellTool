using System;
using System.IO;

namespace WellTool.Extra.Template.Engine.Enjoy
{
    /// <summary>
    /// Enjoy模板实现
    /// </summary>
    public class EnjoyTemplate : ITemplate
    {
        private readonly string _resource;

        public EnjoyTemplate(string resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// 渲染模板
        /// </summary>
        /// <param name="dataMap">数据模型</param>
        /// <param name="writer">输出流</param>
        public void Render(IDictionary<string, object> dataMap, TextWriter writer)
        {
            // 需要集成 JinianNet.JiTemplate 或类似库
            // 这里提供简化实现
            writer.Write(_resource);
        }

        /// <summary>
        /// 渲染模板并返回字符串
        /// </summary>
        /// <param name="dataMap">数据模型</param>
        /// <returns>渲染结果</returns>
        public string RenderToString(IDictionary<string, object> dataMap)
        {
            using (var writer = new StringWriter())
            {
                Render(dataMap, writer);
                return writer.ToString();
            }
        }
    }
}
