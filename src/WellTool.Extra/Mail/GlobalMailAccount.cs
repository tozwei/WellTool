using System;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 全局邮件帐户
    /// </summary>
    public class GlobalMailAccount
    {
        /// <summary>
        /// 全局邮件帐户单例
        /// </summary>
        public static readonly GlobalMailAccount Instance = new GlobalMailAccount();

        private readonly MailAccount _mailAccount;

        /// <summary>
        /// 构造
        /// </summary>
        private GlobalMailAccount()
        {
            _mailAccount = CreateDefaultAccount();
        }

        /// <summary>
        /// 获得邮件帐户
        /// </summary>
        /// <returns>邮件帐户</returns>
        public MailAccount GetAccount()
        {
            return _mailAccount;
        }

        /// <summary>
        /// 创建默认帐户
        /// </summary>
        /// <returns>MailAccount</returns>
        private MailAccount CreateDefaultAccount()
        {
            // 从配置文件中加载或返回null
            return null;
        }
    }
}
