using WellTool.Http.UserAgent;

namespace WellTool.Http.Http.UserAgent
{
    /// <summary>
    /// User-Agent信息
    /// </summary>
    public class UserAgentInfo
    {
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public Browser Browser { get; set; }

        /// <summary>
        /// 引擎信息
        /// </summary>
        public Engine Engine { get; set; }

        /// <summary>
        /// 操作系统信息
        /// </summary>
        public OS OS { get; set; }

        /// <summary>
        /// 平台信息
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 是否为移动设备
        /// </summary>
        public bool IsMobile { get; set; }
    }
}