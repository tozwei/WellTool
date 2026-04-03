using System;

namespace WellTool.Extra.Template.Engine.Jetbrick
{
    /// <summary>
    /// Jetbrick模板引擎封装
    /// 需要安装 Jetbrains.dotNET 或类似 NuGet 包
    /// </summary>
    public class JetbrickEngine : TemplateEngine
    {
        private object _engine;

        /// <summary>
        /// 默认构造
        /// </summary>
        public JetbrickEngine()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">模板配置</param>
        public JetbrickEngine(TemplateConfig config)
        {
            Init(config);
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="config">模板配置</param>
        /// <returns>引擎本身</returns>
        public override TemplateEngine Init(TemplateConfig config)
        {
            if (config == null)
            {
                config = TemplateConfig.DEFAULT;
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
        public override ITemplate GetTemplate(string resource)
        {
            if (_engine == null)
            {
                Init(TemplateConfig.DEFAULT);
            }
            return new JetbrickTemplate(resource);
        }
    }
}
