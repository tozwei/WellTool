using System;

namespace WellTool.Extra.Template.Engine.Enjoy
{
    /// <summary>
    /// Enjoy模板引擎封装
    /// 需要安装 JinianNet.JiTemplate 或类似 NuGet 包
    /// </summary>
    public class EnjoyEngine : TemplateEngine
    {
        private object _engine;
        private TemplateConfig _config;

        /// <summary>
        /// 默认构造
        /// </summary>
        public EnjoyEngine()
        {
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
        /// 初始化引擎
        /// </summary>
        /// <param name="config">模板配置</param>
        /// <returns>引擎本身</returns>
        public TemplateEngine Init(TemplateConfig config)
        {
            if (config == null)
            {
                config = TemplateConfig.DEFAULT;
            }
            _config = config;
            // 使用反射或依赖注入创建底层引擎
            // _engine = CreateEngine(config);
            return this;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源路径或字符串</param>
        /// <returns>模板对象</returns>
        public Template GetTemplate(string resource)
        {
            if (_engine == null)
            {
                Init(TemplateConfig.DEFAULT);
            }
            
            if (_config != null && _config.ResourceMode == TemplateConfig.ResourceModeEnum.STRING)
            {
                return new EnjoyTemplate(resource);
            }
            
            // 文件或类路径模板处理
            return new EnjoyTemplate(resource);
        }
    }
}
