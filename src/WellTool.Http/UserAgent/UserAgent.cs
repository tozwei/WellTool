using System;

namespace WellTool.Http.UserAgent
{
    /// <summary>
    /// User-Agent信息对象
    /// </summary>
    [Serializable]
    public class UserAgent
    {
        /// <summary>
        /// 是否为移动平台
        /// </summary>
        public bool IsMobile { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public Browser Browser { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public OS Os { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// 引擎类型
        /// </summary>
        public Engine Engine { get; set; }

        /// <summary>
        /// 引擎版本
        /// </summary>
        public string EngineVersion { get; set; }

        /// <summary>
        /// 获得浏览器名称
        /// </summary>
        /// <returns>浏览器名称</returns>
        public string GetBrowserName()
        {
            return Browser?.Name ?? "Unknown";
        }

        /// <summary>
        /// 获得操作系统名称
        /// </summary>
        /// <returns>操作系统名称</returns>
        public string GetOsName()
        {
            return Os?.Name ?? "Unknown";
        }

        /// <summary>
        /// 获得平台名称
        /// </summary>
        /// <returns>平台名称</returns>
        public string GetPlatformName()
        {
            return Platform?.Name ?? "Unknown";
        }

        /// <summary>
        /// 获得引擎名称
        /// </summary>
        /// <returns>引擎名称</returns>
        public string GetEngineName()
        {
            return Engine?.Name ?? "Unknown";
        }

        /// <summary>
        /// 是否为移动设备
        /// </summary>
        /// <returns>是否为移动设备</returns>
        public bool IsMobileDevice()
        {
            return IsMobile;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"UserAgent {{ Browser: {GetBrowserName()} {Version}, OS: {GetOsName()} {OsVersion}, Platform: {GetPlatformName()}, Engine: {GetEngineName()} {EngineVersion}, Mobile: {IsMobile} }}";
        }
    }
}
