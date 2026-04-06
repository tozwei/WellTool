using System;

namespace WellTool.Extra.Template.Engine.Rythm
{
    /// <summary>
    /// Rythm模板引擎封装
    /// 需要安装 Rythm.NET 或类似 NuGet 包
    /// </summary>
    public class RythmEngine : TemplateEngine
    {
        private object _engine;

        /// <summary>
        /// 默认构造
        /// </summary>
        public RythmEngine()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public RythmEngine(TemplateConfig config)
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
                config = TemplateConfig.Default;
            }
            return this;
        }

        /// <summary>
        /// 获取原始引擎对象
        /// </summary>
        /// <returns>原始引擎</returns>
        public object GetRawEngine()
        {
            return _engine;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源路径</param>
        /// <returns>模板对象</returns>
        public Template GetTemplate(string resource)
        {
            if (_engine == null)
            {
                Init(TemplateConfig.Default);
            }
            return new RythmTemplate(resource);
        }
    }
}
