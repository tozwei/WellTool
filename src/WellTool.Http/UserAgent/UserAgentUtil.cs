namespace WellTool.Http.UserAgent
{
    /// <summary>
    /// User-Agent工具类
    /// </summary>
    public static class UserAgentUtil
    {
        /// <summary>
        /// 解析User-Agent
        /// </summary>
        /// <param name="userAgentString">User-Agent字符串</param>
        /// <returns>UserAgent</returns>
        public static UserAgent Parse(string userAgentString)
        {
            return UserAgentParser.Parse(userAgentString);
        }
    }
}
