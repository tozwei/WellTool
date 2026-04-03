using System.IO;

namespace WellTool.Extra.Template.Engine.Velocity
{
    /// <summary>
    /// Velocity字符串资源加载器
    /// </summary>
    public class SimpleStringResourceLoader
    {
        /// <summary>
        /// 获取模板源
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <returns>模板内容</returns>
        public static string GetTemplateSource(string name)
        {
            return name;
        }
    }
}
