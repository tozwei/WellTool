using System;
using System.IO;

namespace WellTool.Extra.Template.Engine.Freemarker
{
    /// <summary>
    /// FreeMarker字符串模板加载器
    /// 用于加载字符串模板
    /// </summary>
    public class SimpleStringTemplateLoader
    {
        private string _templateContent = string.Empty;

        /// <summary>
        /// 设置模板内容
        /// </summary>
        /// <param name="templateSource">模板内容</param>
        public void SetTemplate(string templateSource)
        {
            _templateContent = templateSource;
        }

        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <returns>模板内容</returns>
        public object GetTemplateSource(string templateSource)
        {
            return templateSource ?? _templateContent;
        }

        /// <summary>
        /// 获取模板最后修改时间
        /// </summary>
        /// <returns>最后修改时间</returns>
        public DateTime? GetLastModified(object templateSource)
        {
            return DateTime.Now;
        }
    }
}
