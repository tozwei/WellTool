using System.Collections.Generic;
using System.IO;
using WellTool.Extra.Template;

namespace WellTool.Extra.Template.Engine.Enjoy
{
    /// <summary>
    /// Enjoy模板引擎封装
    /// 文档: https://www.jfinal.com/
    /// </summary>
    public class EnjoyEngine : TemplateEngine
    {
        private readonly Dictionary<string, string> _templates = new Dictionary<string, string>();
        private TemplateConfig.ResourceModeType _resourceMode;

        /// <summary>
        /// 默认构造
        /// </summary>
        public EnjoyEngine()
        {
            _resourceMode = TemplateConfig.ResourceModeType.String;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public EnjoyEngine(TemplateConfig config)
        {
            Init(config);
        }

        /// <summary>
        /// 使用指定配置文件初始化模板引擎
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <returns>this</returns>
        public TemplateEngine Init(TemplateConfig config)
        {
            if (config == null)
            {
                config = TemplateConfig.Default;
            }
            this._resourceMode = config.ResourceMode;
            return this;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源</param>
        /// <returns>模板实现</returns>
        public Template GetTemplate(string resource)
        {
            if (_resourceMode == TemplateConfig.ResourceModeType.String)
            {
                return EnjoyTemplate.Wrap(resource)!;
            }

            // 尝试从文件加载
            if (_templates.TryGetValue(resource, out var content))
            {
                return EnjoyTemplate.Wrap(content)!;
            }

            // 尝试从文件读取
            try
            {
                if (File.Exists(resource))
                {
                    content = File.ReadAllText(resource);
                    return EnjoyTemplate.Wrap(content)!;
                }
            }
            catch
            {
                // 忽略
            }

            // 返回简单模板
            return EnjoyTemplate.Wrap($"${{{resource}}}")!;
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="content">模板内容</param>
        public void AddTemplate(string name, string content)
        {
            _templates[name] = content;
        }

        /// <summary>
        /// 获取原始引擎的钩子方法，用于自定义特殊属性
        /// </summary>
        /// <returns>引擎对象</returns>
        public object GetRawEngine()
        {
            return this;
        }
    }
}
