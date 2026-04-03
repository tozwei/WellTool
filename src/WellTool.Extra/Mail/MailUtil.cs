using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件工具类
    /// </summary>
    public class MailUtil
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="mail">邮件对象</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(MailAccount account, Mail mail)
        {
            try
            {
                using var client = CreateSmtpClient(account);
                using var message = CreateMailMessage(account, mail);
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件（同步）
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="mail">邮件对象</param>
        public static void SendSync(MailAccount account, Mail mail)
        {
            using var client = CreateSmtpClient(account);
            using var message = CreateMailMessage(account, mail);
            client.Send(message);
        }

        /// <summary>
        /// 创建SmtpClient
        /// </summary>
        private static SmtpClient CreateSmtpClient(MailAccount account)
        {
            account.DefaultIfEmpty();

            var client = new SmtpClient
            {
                Host = account.Host,
                Port = account.Port ?? 25,
                EnableSsl = account.SslEnable ?? account.StarttlsEnable,
                UseDefaultCredentials = !account.Auth.GetValueOrDefault(false),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (account.Auth.GetValueOrDefault(false))
            {
                client.Credentials = new System.Net.NetworkCredential(account.User, account.Pass);
            }

            if (account.Timeout > 0)
            {
                client.Timeout = (int)account.Timeout;
            }



            return client;
        }

        /// <summary>
        /// 创建MailMessage
        /// </summary>
        private static MailMessage CreateMailMessage(MailAccount account, Mail mail)
        {
            var message = new MailMessage
            {
                From = new MailAddress(account.From),
                Subject = mail.Subject,
                SubjectEncoding = mail.Charset,
                Body = mail.Body,
                BodyEncoding = mail.Charset,
                IsBodyHtml = mail.IsHtml
            };

            // 添加收件人
            foreach (var to in mail.Tos)
            {
                if (!string.IsNullOrWhiteSpace(to.From))
                {
                    message.To.Add(new MailAddress(to.From));
                }
            }

            // 如果没有设置收件人，使用发件人作为收件人
            if (message.To.Count == 0 && !string.IsNullOrWhiteSpace(account.From))
            {
                message.To.Add(new MailAddress(account.From));
            }

            // 添加附件
            foreach (var attachment in mail.Attachments)
            {
                message.Attachments.Add(attachment);
            }

            // 添加自定义头
            foreach (var header in mail.Headers)
            {
                message.Headers[header.Key] = header.Value;
            }

            return message;
        }

        /// <summary>
        /// 发送简单文本邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        public static void SendText(MailAccount account, string to, string subject, string body)
        {
            var mail = new Mail()
                .SetFrom(account.From)
                .SetTo(to)
                .SetTitle(subject)
                .SetBody(body, false);
            Send(account, mail);
        }

        /// <summary>
        /// 发送HTML邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="htmlBody">HTML正文</param>
        public static void SendHtml(MailAccount account, string to, string subject, string htmlBody)
        {
            var mail = new Mail()
                .SetFrom(account.From)
                .SetTo(to)
                .SetTitle(subject)
                .SetBody(htmlBody, true);
            Send(account, mail);
        }

        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="account">邮件账户</param>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="attachmentPaths">附件路径列表</param>
        public static void SendWithAttachments(MailAccount account, string to, string subject, string body, params string[] attachmentPaths)
        {
            var mail = new Mail()
                .SetFrom(account.From)
                .SetTo(to)
                .SetTitle(subject)
                .SetBody(body, false);

            foreach (var path in attachmentPaths)
            {
                mail.AddAttachment(path);
            }

            Send(account, mail);
        }
    }
}
