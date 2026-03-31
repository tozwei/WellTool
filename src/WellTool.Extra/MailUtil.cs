// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Net.Mail;
using System.Net;

namespace WellTool.Extra;

/// <summary>
/// 邮件工具类
/// </summary>
public class MailUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static MailUtil Instance { get; } = new MailUtil();

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="smtpServer">SMTP服务器地址</param>
    /// <param name="smtpPort">SMTP服务器端口</param>
    /// <param name="username">SMTP用户名</param>
    /// <param name="password">SMTP密码</param>
    /// <param name="from">发件人邮箱</param>
    /// <param name="to">收件人邮箱</param>
    /// <param name="subject">邮件主题</param>
    /// <param name="body">邮件正文</param>
    /// <param name="isHtml">是否为HTML邮件</param>
    public void Send(string smtpServer, int smtpPort, string username, string password, string from, string to, string subject, string body, bool isHtml = false)
    {
        try
        {
            using var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            client.Send(message);
        }
        catch (Exception ex)
        {
            throw new MailException("发送邮件失败", ex);
        }
    }

    /// <summary>
    /// 发送邮件（带附件）
    /// </summary>
    /// <param name="smtpServer">SMTP服务器地址</param>
    /// <param name="smtpPort">SMTP服务器端口</param>
    /// <param name="username">SMTP用户名</param>
    /// <param name="password">SMTP密码</param>
    /// <param name="from">发件人邮箱</param>
    /// <param name="to">收件人邮箱</param>
    /// <param name="subject">邮件主题</param>
    /// <param name="body">邮件正文</param>
    /// <param name="attachments">附件路径列表</param>
    /// <param name="isHtml">是否为HTML邮件</param>
    public void SendWithAttachments(string smtpServer, int smtpPort, string username, string password, string from, string to, string subject, string body, List<string> attachments, bool isHtml = false)
    {
        try
        {
            using var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            // 添加附件
            foreach (var attachmentPath in attachments)
            {
                message.Attachments.Add(new Attachment(attachmentPath));
            }

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            client.Send(message);
        }
        catch (Exception ex)
        {
            throw new MailException("发送邮件失败", ex);
        }
    }
}

/// <summary>
/// 邮件异常
/// </summary>
public class MailException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public MailException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public MailException(string message, Exception innerException) : base(message, innerException)
    {}
}