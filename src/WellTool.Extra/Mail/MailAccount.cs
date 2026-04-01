using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件账户对象
    /// </summary>
    public class MailAccount
    {
        // SMTP 配置键
        private const string MailProtocol = "mail.transport.protocol";
        private const string SmtpHost = "mail.smtp.host";
        private const string SmtpPort = "mail.smtp.port";
        private const string SmtpAuth = "mail.smtp.auth";
        private const string SmtpTimeout = "mail.smtp.timeout";
        private const string SmtpConnectionTimeout = "mail.smtp.connectiontimeout";
        private const string SmtpWriteTimeout = "mail.smtp.writetimeout";

        // SSL 配置键
        private const string StartTlsEnable = "mail.smtp.starttls.enable";
        private const string SslEnableKey = "mail.smtp.ssl.enable";
        private const string SslProtocols = "mail.smtp.ssl.protocols";
        private const string SocketFactory = "mail.smtp.socketFactory.class";
        private const string SocketFactoryFallback = "mail.smtp.socketFactory.fallback";
        private const string SocketFactoryPort = "smtp.socketFactory.port";

        // 系统属性
        private const string SplitLongParams = "mail.mime.splitlongparameters";

        // 其他
        private const string MailDebug = "mail.debug";

        /// <summary>
        /// SMTP服务器域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP服务端口
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// 是否需要用户名密码验证
        /// </summary>
        public bool? Auth { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// 发送方，遵循RFC-822标准
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 是否打开调试模式，调试模式会显示与邮件服务器通信过程，默认不开启
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 编码用于编码邮件正文和发送人、收件人等中文
        /// </summary>
        public Encoding Charset { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 对于超长参数是否切分为多份，默认为false（国内邮箱附件不支持切分的附件名）
        /// </summary>
        public bool Splitlongparameters { get; set; } = false;

        /// <summary>
        /// 对于文件名是否使用Charset编码，默认为 true
        /// </summary>
        public bool Encodefilename { get; set; } = true;

        /// <summary>
        /// 使用 STARTTLS安全连接，STARTTLS是对纯文本通信协议的扩展。它将纯文本连接升级为加密连接（TLS或SSL）， 而不是使用一个单独的加密通信端口。
        /// </summary>
        public bool StarttlsEnable { get; set; } = false;

        /// <summary>
        /// 使用 SSL安全连接
        /// </summary>
        public bool? SslEnable { get; set; }

        /// <summary>
        /// SSL协议，多个协议用空格分隔
        /// </summary>
        public string SslProtocolsValue { get; set; }

        /// <summary>
        /// 指定实现SocketFactory接口的类的名称,这个类将被用于创建SMTP的套接字
        /// </summary>
        public string SocketFactoryClass { get; set; } = "System.Net.Security.SslStream";

        /// <summary>
        /// SMTP超时时长，单位毫秒，缺省值不超时
        /// </summary>
        public long Timeout { get; set; }

        /// <summary>
        /// Socket连接超时值，单位毫秒，缺省值不超时
        /// </summary>
        public long ConnectionTimeout { get; set; }

        /// <summary>
        /// Socket写出超时值，单位毫秒，缺省值不超时
        /// </summary>
        public long WriteTimeout { get; set; }

        /// <summary>
        /// 自定义的其他属性，此自定义属性会覆盖默认属性
        /// </summary>
        public Dictionary<string, object> CustomProperty { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 构造,所有参数需自行定义或保持默认值
        /// </summary>
        public MailAccount() { }

        /// <summary>
        /// 设置SMTP服务器域名
        /// </summary>
        /// <param name="host">SMTP服务器域名</param>
        /// <returns>this</returns>
        public MailAccount SetHost(string host)
        {
            Host = host;
            return this;
        }

        /// <summary>
        /// 设置SMTP服务端口
        /// </summary>
        /// <param name="port">SMTP服务端口</param>
        /// <returns>this</returns>
        public MailAccount SetPort(int port)
        {
            Port = port;
            return this;
        }

        /// <summary>
        /// 设置是否需要用户名密码验证
        /// </summary>
        /// <param name="auth">是否需要用户名密码验证</param>
        /// <returns>this</returns>
        public MailAccount SetAuth(bool auth)
        {
            Auth = auth;
            return this;
        }

        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <param name="user">用户名</param>
        /// <returns>this</returns>
        public MailAccount SetUser(string user)
        {
            User = user;
            return this;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="pass">密码</param>
        /// <returns>this</returns>
        public MailAccount SetPass(string pass)
        {
            Pass = pass;
            return this;
        }

        /// <summary>
        /// 设置发送方，遵循RFC-822标准
        /// </summary>
        /// <param name="from">发送方，遵循RFC-822标准</param>
        /// <returns>this</returns>
        public MailAccount SetFrom(string from)
        {
            From = from;
            return this;
        }

        /// <summary>
        /// 设置是否打开调试模式，调试模式会显示与邮件服务器通信过程，默认不开启
        /// </summary>
        /// <param name="debug">是否打开调试模式</param>
        /// <returns>this</returns>
        public MailAccount SetDebug(bool debug)
        {
            Debug = debug;
            return this;
        }

        /// <summary>
        /// 设置字符集编码
        /// </summary>
        /// <param name="charset">字符集编码</param>
        /// <returns>this</returns>
        public MailAccount SetCharset(Encoding charset)
        {
            Charset = charset;
            return this;
        }

        /// <summary>
        /// 设置对于超长参数是否切分为多份，默认为false（国内邮箱附件不支持切分的附件名）
        /// </summary>
        /// <param name="splitlongparameters">对于超长参数是否切分为多份</param>
        public void SetSplitlongparameters(bool splitlongparameters)
        {
            Splitlongparameters = splitlongparameters;
        }

        /// <summary>
        /// 设置对于文件名是否使用Charset编码，默认为 true
        /// </summary>
        /// <param name="encodefilename">对于文件名是否使用Charset编码</param>
        public void SetEncodefilename(bool encodefilename)
        {
            Encodefilename = encodefilename;
        }

        /// <summary>
        /// 设置是否使用STARTTLS安全连接
        /// </summary>
        /// <param name="starttlsEnable">是否使用STARTTLS安全连接</param>
        /// <returns>this</returns>
        public MailAccount SetStarttlsEnable(bool starttlsEnable)
        {
            StarttlsEnable = starttlsEnable;
            return this;
        }

        /// <summary>
        /// 设置是否使用SSL安全连接
        /// </summary>
        /// <param name="sslEnable">是否使用SSL安全连接</param>
        /// <returns>this</returns>
        public MailAccount SetSslEnable(bool sslEnable)
        {
            SslEnable = sslEnable;
            return this;
        }

        /// <summary>
        /// 设置SSL协议，多个协议用空格分隔
        /// </summary>
        /// <param name="sslProtocols">SSL协议，多个协议用空格分隔</param>
        public void SetSslProtocols(string sslProtocols)
        {
            SslProtocolsValue = sslProtocols;
        }

        /// <summary>
        /// 设置指定实现SocketFactory接口的类的名称
        /// </summary>
        /// <param name="socketFactoryClass">指定实现SocketFactory接口的类的名称</param>
        /// <returns>this</returns>
        public MailAccount SetSocketFactoryClass(string socketFactoryClass)
        {
            SocketFactoryClass = socketFactoryClass;
            return this;
        }

        /// <summary>
        /// 设置SMTP超时时长，单位毫秒，缺省值不超时
        /// </summary>
        /// <param name="timeout">SMTP超时时长，单位毫秒</param>
        /// <returns>this</returns>
        public MailAccount SetTimeout(long timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <summary>
        /// 设置Socket连接超时值，单位毫秒，缺省值不超时
        /// </summary>
        /// <param name="connectionTimeout">Socket连接超时值，单位毫秒</param>
        /// <returns>this</returns>
        public MailAccount SetConnectionTimeout(long connectionTimeout)
        {
            ConnectionTimeout = connectionTimeout;
            return this;
        }

        /// <summary>
        /// 设置Socket写出超时值，单位毫秒，缺省值不超时
        /// </summary>
        /// <param name="writeTimeout">Socket写出超时值，单位毫秒</param>
        /// <returns>this</returns>
        public MailAccount SetWriteTimeout(long writeTimeout)
        {
            WriteTimeout = writeTimeout;
            return this;
        }

        /// <summary>
        /// 设置自定义属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        /// <returns>this</returns>
        public MailAccount SetCustomProperty(string key, object value)
        {
            if (!string.IsNullOrWhiteSpace(key) && value != null)
            {
                CustomProperty[key] = value;
            }
            return this;
        }

        /// <summary>
        /// 配置 SmtpClient
        /// </summary>
        /// <param name="client">SmtpClient 实例</param>
        public void ConfigureSmtpClient(SmtpClient client)
        {
            client.Host = Host;
            client.Port = Port ?? (SslEnable == true ? 465 : 25);
            client.EnableSsl = SslEnable ?? StarttlsEnable;
            client.UseDefaultCredentials = !Auth.GetValueOrDefault(false);

            if (Auth.GetValueOrDefault(false))
            {
                client.Credentials = new System.Net.NetworkCredential(User, Pass);
            }

            if (Timeout > 0)
            {
                client.Timeout = (int)Timeout;
            }

            // 其他配置...
        }

        /// <summary>
        /// 如果某些值为null，使用默认值
        /// </summary>
        /// <returns>this</returns>
        public MailAccount DefaultIfEmpty()
        {
            // 提取发件人邮箱地址
            var fromAddress = ExtractEmailAddress(From);

            if (string.IsNullOrWhiteSpace(Host))
            {
                // 如果SMTP地址为空，默认使用smtp.<发件人邮箱后缀>
                var atIndex = fromAddress.IndexOf('@');
                if (atIndex > 0)
                {
                    var domain = fromAddress.Substring(atIndex + 1);
                    Host = $"smtp.{domain}";
                }
            }

            if (string.IsNullOrWhiteSpace(User))
            {
                // 如果用户名为空，默认为发件人
                User = fromAddress;
            }

            if (Auth == null)
            {
                // 如果密码非空白，则使用认证模式
                Auth = !string.IsNullOrWhiteSpace(Pass);
            }

            if (Port == null)
            {
                // 端口在SSL状态下默认465，非SSL状态下默认为25
                Port = (SslEnable == true) ? 465 : 25;
            }

            if (Charset == null)
            {
                // 默认UTF-8编码
                Charset = Encoding.UTF8;
            }

            return this;
        }

        /// <summary>
        /// 提取邮件地址
        /// </summary>
        /// <param name="from">发件人字符串</param>
        /// <returns>邮件地址</returns>
        private string ExtractEmailAddress(string from)
        {
            if (string.IsNullOrWhiteSpace(from))
            {
                return string.Empty;
            }

            // 处理 "name <user@example.com>" 格式
            var angleBracketStart = from.IndexOf('<');
            var angleBracketEnd = from.IndexOf('>');
            if (angleBracketStart >= 0 && angleBracketEnd > angleBracketStart)
            {
                return from.Substring(angleBracketStart + 1, angleBracketEnd - angleBracketStart - 1).Trim();
            }

            // 直接返回邮箱地址
            return from.Trim();
        }

        /// <summary>
        /// 重写 ToString 方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("MailAccount [host={0}, port={1}, auth={2}, user={3}, pass={4}, from={5}, starttlsEnable={6}, socketFactoryClass={7}]",
                Host, Port, Auth, User, string.IsNullOrWhiteSpace(Pass) ? "" : "******", From, StarttlsEnable, SocketFactoryClass);
        }
    }
}