namespace WellTool.Extra.Ssh
{
    /// <summary>
    /// SSH支持的Channel类型
    /// </summary>
    public enum ChannelType
    {
        /// <summary>Session</summary>
        Session,
        /// <summary>shell</summary>
        Shell,
        /// <summary>exec</summary>
        Exec,
        /// <summary>x11</summary>
        X11,
        /// <summary>agent forwarding</summary>
        AgentForwarding,
        /// <summary>direct tcpip</summary>
        DirectTcpip,
        /// <summary>forwarded tcpip</summary>
        ForwardedTcpip,
        /// <summary>sftp</summary>
        Sftp,
        /// <summary>subsystem</summary>
        Subsystem
    }

    /// <summary>
    /// ChannelType 扩展方法
    /// </summary>
    public static class ChannelTypeExtensions
    {
        /// <summary>
        /// 获取通道类型的值
        /// </summary>
        /// <param name="channelType">通道类型</param>
        /// <returns>通道类型值</returns>
        public static string GetValue(this ChannelType channelType)
        {
            switch (channelType)
            {
                case ChannelType.Session:
                    return "session";
                case ChannelType.Shell:
                    return "shell";
                case ChannelType.Exec:
                    return "exec";
                case ChannelType.X11:
                    return "x11";
                case ChannelType.AgentForwarding:
                    return "auth-agent@openssh.com";
                case ChannelType.DirectTcpip:
                    return "direct-tcpip";
                case ChannelType.ForwardedTcpip:
                    return "forwarded-tcpip";
                case ChannelType.Sftp:
                    return "sftp";
                case ChannelType.Subsystem:
                    return "subsystem";
                default:
                    return string.Empty;
            }
        }
    }
}