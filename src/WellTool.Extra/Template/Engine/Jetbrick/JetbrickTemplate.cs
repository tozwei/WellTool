using System.IO;

namespace WellTool.Extra.Template.Engine.Jetbrick
{
    /// <summary>
    /// Jetbrick模板实现
    /// </summary>
    public class JetbrickTemplate : ITemplate
    {
        private readonly string _resource;

        public JetbrickTemplate(string resource)
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
            // 需要集成 Jetbrains.dotNET 或类似库
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
