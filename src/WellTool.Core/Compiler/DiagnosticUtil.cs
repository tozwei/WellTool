using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINDOWS
using System.CodeDom.Compiler;
#endif

namespace WellTool.Core.Compiler
{
    /// <summary>
    /// 诊断工具类
    /// </summary>
    public static class DiagnosticUtil
    {
        /// <summary>
        /// 获取 DiagnosticCollector 收集到的诊断信息，以文本返回
        /// </summary>
        /// <param name="collector">诊断收集器</param>
        /// <returns>诊断信息文本</returns>
#if WINDOWS
        public static string GetMessages(DiagnosticCollector<object> collector)
        {
            if (collector == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var diagnostic in collector.Diagnostics)
            {
                sb.AppendLine(diagnostic.ToString());
            }
            return sb.ToString();
        }
#else
        public static string GetMessages(object collector)
        {
            return string.Empty;
        }
#endif
    }
}