using System;

namespace WellTool.Extra.Ftp
{
    /// <summary>
    /// FTP配置项，提供FTP各种参数信息
    /// </summary>
    public class FtpConfig
    {
        /// <summary>
        /// 创建FTP配置
        /// </summary>
        /// <returns>FTP配置</returns>
        public static FtpConfig Create()
        {
            return new FtpConfig();
        }

        /// <summary>
        /// 主机
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
        /// 编码
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// 连接超时时长，单位毫秒
        /// </summary>
        public long ConnectionTimeout { get; set; }

        /// <summary>
        /// Socket连接超时时长，单位毫秒
        /// </summary>
        public long SoTimeout { get; set; }

        /// <summary>
        /// 设置服务器语言
        /// </summary>
        public string ServerLanguageCode { get; set; }

        /// <summary>
        /// 设置服务器系统关键词
        /// </summary>
        public string SystemKey { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public FtpConfig() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="host">主机</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="charset">编码</param>
        public FtpConfig(string host, int port, string user, string password, string charset)
            : this(host, port, user, password, charset, null, null)
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="host">主机</param>
        /// <param name="port">端口</param>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="charset">编码</param>
        /// <param name="serverLanguageCode">服务器语言</param>
        /// <param name="systemKey">系统关键字</param>
        public FtpConfig(string host, int port, string user, string password, string charset, string serverLanguageCode, string systemKey)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            Charset = charset;
            ServerLanguageCode = serverLanguageCode;
            SystemKey = systemKey;
        }

        /// <summary>
        /// 设置主机
        /// </summary>
        /// <param name="host">主机</param>
        /// <returns>this</returns>
        public FtpConfig SetHost(string host)
        {
            Host = host;
            return this;
        }

        /// <summary>
        /// 设置端口
        /// </summary>
        /// <param name="port">端口</param>
        /// <returns>this</returns>
        public FtpConfig SetPort(int port)
        {
            Port = port;
            return this;
        }

        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <param name="user">用户名</param>
        /// <returns>this</returns>
        public FtpConfig SetUser(string user)
        {
            User = user;
            return this;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>this</returns>
        public FtpConfig SetPassword(string password)
        {
            Password = password;
            return this;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>this</returns>
        public FtpConfig SetCharset(string charset)
        {
            Charset = charset;
            return this;
        }

        /// <summary>
        /// 设置连接超时时长
        /// </summary>
        /// <param name="connectionTimeout">连接超时时长，单位毫秒</param>
        /// <returns>this</returns>
        public FtpConfig SetConnectionTimeout(long connectionTimeout)
        {
            ConnectionTimeout = connectionTimeout;
            return this;
        }

        /// <summary>
        /// 设置Socket连接超时时长
        /// </summary>
        /// <param name="soTimeout">Socket连接超时时长，单位毫秒</param>
        /// <returns>this</returns>
        public FtpConfig SetSoTimeout(long soTimeout)
        {
            SoTimeout = soTimeout;
            return this;
        }

        /// <summary>
        /// 设置服务器语言
        /// </summary>
        /// <param name="serverLanguageCode">服务器语言</param>
        /// <returns>this</returns>
        public FtpConfig SetServerLanguageCode(string serverLanguageCode)
        {
            ServerLanguageCode = serverLanguageCode;
            return this;
        }

        /// <summary>
        /// 设置服务器系统关键词
        /// </summary>
        /// <param name="systemKey">服务器系统关键词</param>
        /// <returns>this</returns>
        public FtpConfig SetSystemKey(string systemKey)
        {
            SystemKey = systemKey;
            return this;
        }
    }
}