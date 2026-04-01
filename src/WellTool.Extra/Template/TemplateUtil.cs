namespace WellTool.Extra.Template
{
    /// <summary>
    /// 模板工具类
    /// </summary>
    public class TemplateUtil
    {
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
    }
}