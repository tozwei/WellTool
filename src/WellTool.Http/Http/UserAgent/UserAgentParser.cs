using WellTool.Http.UserAgent;

namespace WellTool.Http.Http.UserAgent
{
    /// <summary>
    /// User-Agent解析器接口
    /// </summary>
    public interface IUserAgentParser
    {
        /// <summary>
        /// 解析User-Agent字符串
        /// </summary>
        /// <param name="userAgentString">User-Agent字符串</param>
        /// <returns>User-Agent信息</returns>
        WellTool.Http.UserAgent.UserAgent Parse(string userAgentString);
    }

    /// <summary>
    /// 默认User-Agent解析器
    /// </summary>
    public class DefaultUserAgentParser : IUserAgentParser
    {
        /// <summary>
        /// 解析User-Agent字符串
        /// </summary>
        /// <param name="userAgentString">User-Agent字符串</param>
        /// <returns>User-Agent信息</returns>
        public WellTool.Http.UserAgent.UserAgent Parse(string userAgentString)
        {
            return UserAgentUtil.Parse(userAgentString);
        }
    }
}