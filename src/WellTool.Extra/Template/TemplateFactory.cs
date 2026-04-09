namespace WellTool.Extra.Template
{
    /// <summary>
    /// 模板引擎工厂
    /// </summary>
    public static class TemplateFactory
    {
        /// <summary>
        /// 创建模板引擎
        /// </summary>
        /// <returns>模板引擎</returns>
        public static TemplateEngine Create()
        {
            return Create(TemplateConfig.Default);
        }

        /// <summary>
        /// 创建模板引擎
        /// </summary>
        /// <param name="config">模板配置</param>
        /// <returns>模板引擎</returns>
        public static TemplateEngine Create(TemplateConfig config)
        {
            // 简化实现，实际项目中可以根据配置或依赖自动检测并创建对应的模板引擎
            return new DefaultTemplateEngine(config);
        }
    }

    /// <summary>
    /// 默认模板引擎实现
    /// </summary>
    internal class DefaultTemplateEngine : TemplateEngine
    {
        private TemplateConfig _config;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public DefaultTemplateEngine(TemplateConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// 使用指定配置文件初始化模板引擎
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <returns>this</returns>
        public TemplateEngine Init(TemplateConfig config)
        {
            _config = config;
            return this;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源，根据实现不同，此资源可以是模板本身，也可以是模板的相对路径</param>
        /// <returns>模板实现</returns>
        public Template GetTemplate(string resource)
        {
            return new DefaultTemplate(resource);
        }
    }

    /// <summary>
    /// 默认模板实现
    /// </summary>
    internal class DefaultTemplate : AbstractTemplate
    {
        private string _resource;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="resource">资源</param>
        public DefaultTemplate(string resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到Writer
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="writer">输出</param>
        public override void Render(System.Collections.Generic.IDictionary<object, object> bindingMap, System.IO.TextWriter writer)
        {
            // 实现模板渲染功能，支持变量替换
            string renderedContent = RenderTemplate(_resource, bindingMap);
            writer.Write(renderedContent);
        }

        /// <summary>
        /// 将模板与绑定参数融合后输出到流
        /// </summary>
        /// <param name="bindingMap">绑定的参数，此Map中的参数会替换模板中的变量</param>
        /// <param name="output">输出</param>
        public override void Render(System.Collections.Generic.IDictionary<object, object> bindingMap, System.IO.Stream output)
        {
            // 实现模板渲染功能，支持变量替换
            string renderedContent = RenderTemplate(_resource, bindingMap);
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(output, System.Text.Encoding.UTF8, bufferSize: 1024, leaveOpen: true))
            {
                writer.Write(renderedContent);
            }
        }

        /// <summary>
        /// 渲染模板，替换变量
        /// </summary>
        /// <param name="template">模板内容</param>
        /// <param name="bindingMap">绑定的参数</param>
        /// <returns>渲染后的内容</returns>
        private string RenderTemplate(string template, System.Collections.Generic.IDictionary<object, object> bindingMap)
        {
            if (string.IsNullOrEmpty(template) || bindingMap == null || bindingMap.Count == 0)
            {
                return template;
            }

            string result = template;

            // 简单的变量替换，支持 ${变量名} 格式
            foreach (var kvp in bindingMap)
            {
                string key = kvp.Key?.ToString();
                if (!string.IsNullOrEmpty(key))
                {
                    string placeholder = $"${{{key}}}";
                    string value = kvp.Value?.ToString() ?? string.Empty;
                    result = result.Replace(placeholder, value);
                }
            }

            return result;
        }
    }
}