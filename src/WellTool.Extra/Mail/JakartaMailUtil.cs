using System.Collections.Generic;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// Jakarta邮件工具类
    /// 提供基于 Jakarta Mail (jakarta.mail) 的邮件发送功能
    /// </summary>
    public class JakartaMailUtil
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="isHtml">是否HTML格式</param>
        /// <returns>发送是否成功</returns>
        public static bool Send(MailAccount account, string to, string subject, string content, bool isHtml = false)
        {
            // TODO: 需要集成 Jakarta.Mail 或 MailKit NuGet 包
            return false;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="to">收件人列表</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="isHtml">是否HTML格式</param>
        /// <returns>发送是否成功</returns>
        public static bool Send(MailAccount account, IEnumerable<string> to, string subject, string content, bool isHtml = false)
        {
            // TODO: 需要集成 Jakarta.Mail 或 MailKit NuGet 包
            return false;
        }
    }
}
