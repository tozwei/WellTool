using System;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件账户工具类
    /// </summary>
    public static class MailAccountUtil
    {
        /// <summary>
        /// 解析邮件账户配置字符串
        /// </summary>
        /// <param name="config">配置字符串，格式如：smtp.example.com:465:user@example.com:password</param>
        /// <returns>邮件账户</returns>
        public static MailAccount Parse(string config)
        {
            if (string.IsNullOrEmpty(config))
            {
                return new MailAccount();
            }

            var account = new MailAccount();

            // 尝试解析不同格式
            if (config.Contains("://"))
            {
                // URL格式: smtps://user:pass@smtp.example.com:465
                ParseUrlFormat(config, account);
            }
            else
            {
                // 简单格式: host:port:user:pass
                ParseSimpleFormat(config, account);
            }

            return account;
        }

        private static void ParseUrlFormat(string config, MailAccount account)
        {
            try
            {
                var uri = new Uri(config);
                account.SetHost(uri.Host);
                account.SetPort(uri.Port > 0 ? uri.Port : (uri.Scheme == "smtps" ? 465 : 25));

                if (!string.IsNullOrEmpty(uri.UserInfo))
                {
                    var parts = uri.UserInfo.Split(':');
                    if (parts.Length >= 1)
                    {
                        account.SetUser(Uri.UnescapeDataString(parts[0]));
                    }
                    if (parts.Length >= 2)
                    {
                        account.SetPass(Uri.UnescapeDataString(parts[1]));
                    }
                }

                account.SetAuth(!string.IsNullOrEmpty(account.User));
                account.SetSslEnable(uri.Scheme == "smtps");
            }
            catch
            {
                // 解析失败，返回默认账户
            }
        }

        private static void ParseSimpleFormat(string config, MailAccount account)
        {
            var parts = config.Split(':');
            if (parts.Length >= 1)
            {
                account.SetHost(parts[0]);
            }
            if (parts.Length >= 2 && int.TryParse(parts[1], out var port))
            {
                account.SetPort(port);
            }
            if (parts.Length >= 3)
            {
                account.SetUser(parts[2]);
            }
            if (parts.Length >= 4)
            {
                account.SetPass(parts[3]);
            }
            account.SetAuth(!string.IsNullOrEmpty(account.User));
        }

        /// <summary>
        /// 检查账户是否可登录
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <returns>是否可登录</returns>
        public static bool IsLogin(MailAccount account)
        {
            return account != null && !string.IsNullOrEmpty(account.User) && !string.IsNullOrEmpty(account.Pass);
        }

        /// <summary>
        /// 获取SMTP主机
        /// </summary>
        /// <param name="host">主机名</param>
        /// <returns>SMTP主机</returns>
        public static string GetSmtpHost(string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                return string.Empty;
            }
            return host;
        }

        /// <summary>
        /// 获取SMTP端口
        /// </summary>
        /// <param name="ssl">是否使用SSL</param>
        /// <param name="starttls">是否使用STARTTLS</param>
        /// <returns>SMTP端口</returns>
        public static int GetSmtpPort(bool ssl, bool starttls)
        {
            if (ssl)
            {
                return 465;
            }
            if (starttls)
            {
                return 587;
            }
            return 25;
        }
    }
}