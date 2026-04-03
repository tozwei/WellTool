using System;
using System.IO;
using WellTool.Extra.Template;

namespace WellTool.Extra.Template.Engine.Velocity
{
    /// <summary>
    /// Velocity模板引擎
    /// 文档: http://velocity.apache.org/
    /// </summary>
    public class VelocityEngine : TemplateEngine
    {
        private readonly TemplateConfig _config;

        /// <summary>
        /// 默认构造
        /// </summary>
        public VelocityEngine()
        {
            _config = TemplateConfig.Default;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public VelocityEngine(TemplateConfig config)
        {
            _config = config ?? TemplateConfig.Default;
        }

        /// <summary>
        /// 使用指定配置文件初始化模板引擎
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <returns>this</returns>
        public TemplateEngine Init(TemplateConfig config)
        {
            return this;
        }

        /// <summary>
        /// 获取原始的引擎对象
        /// </summary>
        /// <returns>原始引擎对象</returns>
        public object GetRawEngine()
        {
            return this;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源</param>
        /// <returns>模板实现</returns>
        public Template GetTemplate(string resource)
        {
            string content;

            switch (_config.ResourceMode)
            {
                case TemplateConfig.ResourceModeType.String:
                    content = resource;
                    break;
                case TemplateConfig.ResourceModeType.File:
                    if (File.Exists(resource))
                    {
                        content = File.ReadAllText(resource, _config.Charset ?? System.Text.Encoding.UTF8);
                    }
                    else
                    {
                        var fullPath = Path.Combine(_config.Path ?? "", resource);
                        content = File.Exists(fullPath)
                            ? File.ReadAllText(fullPath, _config.Charset ?? System.Text.Encoding.UTF8)
                            : $"${{{resource}}}";
                    }
                    break;
                case TemplateConfig.ResourceModeType.ClassPath:
                case TemplateConfig.ResourceModeType.WebRoot:
                    content = $"${{{resource}}}";
                    break;
                default:
                    content = $"${{{resource}}}";
                    break;
            }

            return VelocityTemplate.Wrap(content)!;
        }
    }
}
