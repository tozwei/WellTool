using System.Net.Mail;
using System.Collections.Generic;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 内部邮件工具类
    /// 提供基于 System.Net.Mail 的邮件发送功能
    /// </summary>
    public class InternalMailUtil
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
            using (var client = new SmtpClient(account.Host, account.Port))
            {
                client.Credentials = new System.Net.NetworkCredential(account.User, account.Password);
                client.EnableSsl = account.Ssl;

                var message = new MailMessage();
                message.From = new MailAddress(account.From);
                message.To.Add(to);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = isHtml;

                client.Send(message);
                return true;
            }
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
            using (var client = new SmtpClient(account.Host, account.Port))
            {
                client.Credentials = new System.Net.NetworkCredential(account.User, account.Password);
                client.EnableSsl = account.Ssl;

                var message = new MailMessage();
                message.From = new MailAddress(account.From);
                foreach (var recipient in to)
                {
                    message.To.Add(recipient);
                }
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = isHtml;

                client.Send(message);
                return true;
            }
        }
    }
}
