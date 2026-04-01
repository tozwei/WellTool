namespace WellTool.Extra.Template
{
    /// <summary>
    /// 引擎接口，通过实现此接口从而使用对应的模板引擎
    /// </summary>
    public interface TemplateEngine
    {
        /// <summary>
        /// 使用指定配置文件初始化模板引擎
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <returns>this</returns>
        TemplateEngine Init(TemplateConfig config);

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="resource">资源，根据实现不同，此资源可以是模板本身，也可以是模板的相对路径</param>
        /// <returns>模板实现</returns>
        Template GetTemplate(string resource);
    }
}