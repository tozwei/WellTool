using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Compiler
{
    /// <summary>
    /// 诊断工具类
    /// </summary>
    public static class DiagnosticUtil
    {
        /// <summary>
        /// 获取诊断收集器收集到的诊断信息，以文本返回
        /// </summary>
        /// <param name="collector">诊断收集器</param>
        /// <returns>诊断信息文本</returns>
        public static string GetMessages(object collector)
        {
            if (collector == null)
            {
                return string.Empty;
            }

            // 尝试获取诊断信息
            try
            {
                var sb = new StringBuilder();
                // 这里可以根据实际情况调整实现
                sb.AppendLine(collector.ToString());
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}