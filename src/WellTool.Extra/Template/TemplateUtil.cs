namespace WellTool.Extra.Template
{
    /// <summary>
    /// 模板工具类
    /// </summary>
    public class TemplateUtil
    {
        private static readonly Dictionary<string, TemplateEngine> _engines = new Dictionary<string, TemplateEngine>();

        /// <summary>
        /// 根据用户引入的模板引擎，自动创建对应的模板引擎对象，使用默认配置
        /// 推荐创建的引擎单例使用，此方法每次调用会返回新的引擎
        /// </summary>
        /// <returns>模板引擎</returns>
        public static TemplateEngine CreateEngine()
        {
            return TemplateFactory.Create();
        }

        /// <summary>
        /// 根据用户引入的模板引擎，自动创建对应的模板引擎对象
        /// 推荐创建的引擎单例使用，此方法每次调用会返回新的引擎
        /// </summary>
        /// <param name="config">模板配置，包括编码、模板文件path等信息</param>
        /// <returns>模板引擎</returns>
        public static TemplateEngine CreateEngine(TemplateConfig config)
        {
            return TemplateFactory.Create(config);
        }

        /// <summary>
        /// 根据用户引入的模板引擎，自动创建对应的模板引擎对象
        /// </summary>
        /// <param name="engineName">引擎名称</param>
        /// <returns>模板引擎</returns>
        public static TemplateEngine CreateEngine(string engineName)
        {
            // 简化实现
            return CreateEngine();
        }

        /// <summary>
        /// 获取指定引擎的模板
        /// </summary>
        /// <param name="engineName">引擎名称</param>
        /// <param name="resource">资源</param>
        /// <returns>模板</returns>
        public static Template Get(string engineName, string resource)
        {
            if (string.IsNullOrEmpty(engineName) || string.IsNullOrEmpty(resource))
            {
                return null;
            }

            if (_engines.TryGetValue(engineName, out var engine))
            {
                return engine.GetTemplate(resource);
            }
            return null;
        }

        /// <summary>
        /// 根据路径获取模板
        /// </summary>
        /// <param name="path">模板路径</param>
        /// <returns>模板</returns>
        public static Template GetByPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            var engine = CreateEngine();
            return engine.GetTemplate(path);
        }

        /// <summary>
        /// 获取所有已注册的引擎名称
        /// </summary>
        /// <returns>引擎名称列表</returns>
        public static List<string> GetEngineNames()
        {
            return new List<string>(_engines.Keys);
        }

        /// <summary>
        /// 注册自定义引擎
        /// </summary>
        /// <param name="name">引擎名称</param>
        /// <param name="engine">引擎实例</param>
        public static void Register(string name, Template engine)
        {
            if (!string.IsNullOrEmpty(name) && engine != null)
            {
                _engines[name] = new TemplateEngineAdapter(engine);
            }
        }

        /// <summary>
        /// 内部适配器类：将 Template 适配为 TemplateEngine
        /// </summary>
        private class TemplateEngineAdapter : TemplateEngine
        {
            private readonly Template _template;

            public TemplateEngineAdapter(Template template)
            {
                _template = template;
            }

            public TemplateEngine Init(TemplateConfig config)
            {
                return this;
            }

            public Template GetTemplate(string resource)
            {
                return _template;
            }
        }

        /// <summary>
        /// 注销引擎
        /// </summary>
        /// <param name="name">引擎名称</param>
        public static void Unregister(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _engines.Remove(name);
            }
        }
    }
}