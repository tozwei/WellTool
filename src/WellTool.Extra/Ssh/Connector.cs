namespace WellTool.Extra.Ssh
{
    /// <summary>
    /// 连接者对象，提供一些连接的基本信息
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Connector()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="group">组</param>
        public Connector(string user, string password, string group)
        {
            User = user;
            Password = password;
            Group = group;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        public Connector(string host, int port, string user, string password)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("Connector [host={0}, port={1}, user={2}, password={3}]", Host, Port, User, Password);
        }
    }
}