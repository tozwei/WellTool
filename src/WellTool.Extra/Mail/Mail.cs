using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace WellTool.Extra.Mail
{
    /// <summary>
    /// 邮件对象
    /// </summary>
    public class Mail
    {
        private readonly List<Mail> _tos = new List<Mail>();
        private readonly List<Attachment> _attachments = new List<Attachment>();
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public List<Mail> Tos => _tos;

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 正文是否为HTML
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> Attachments => _attachments;

        /// <summary>
        /// 自定义头信息
        /// </summary>
        public Dictionary<string, string> Headers => _headers;

        /// <summary>
        /// 编码
        /// </summary>
        public System.Text.Encoding Charset { get; set; } = System.Text.Encoding.UTF8;

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Mail()
        {
        }

        /// <summary>
        /// 设置发件人
        /// </summary>
        /// <param name="from">发件人地址</param>
        /// <returns>this</returns>
        public Mail SetFrom(string from)
        {
            From = from;
            return this;
        }

        /// <summary>
        /// 添加收件人
        /// </summary>
        /// <param name="to">收件人地址</param>
        /// <returns>this</returns>
        public Mail SetTo(string to)
        {
            var mailTo = new Mail();
            mailTo.From = to;
            _tos.Add(mailTo);
            return this;
        }

        /// <summary>
        /// 添加收件人
        /// </summary>
        /// <param name="to">收件人</param>
        /// <returns>this</returns>
        public Mail SetTo(Mail to)
        {
            _tos.Add(to);
            return this;
        }

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="subject">主题</param>
        /// <returns>this</returns>
        public Mail SetTitle(string subject)
        {
            Subject = subject;
            return this;
        }

        /// <summary>
        /// 设置正文
        /// </summary>
        /// <param name="body">正文</param>
        /// <param name="isHtml">是否为HTML</param>
        /// <returns>this</returns>
        public Mail SetBody(string body, bool isHtml = false)
        {
            Body = body;
            IsHtml = isHtml;
            return this;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="filePath">附件文件路径</param>
        /// <returns>this</returns>
        public Mail AddAttachment(string filePath)
        {
            var contentType = new ContentType(MediaTypeNames.Application.Octet);
            var attachment = new Attachment(filePath, contentType);
            _attachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="stream">附件流</param>
        /// <param name="fileName">文件名</param>
        /// <returns>this</returns>
        public Mail AddAttachment(Stream stream, string fileName)
        {
            var contentType = new ContentType(MediaTypeNames.Application.Octet);
            var attachment = new Attachment(stream, fileName);
            _attachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// 添加头信息
        /// </summary>
        /// <param name="key">头信息键</param>
        /// <param name="value">头信息值</param>
        /// <returns>this</returns>
        public Mail AddHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>this</returns>
        public Mail SetCharset(System.Text.Encoding charset)
        {
            Charset = charset;
            return this;
        }

        /// <summary>
        /// 设置发送时间
        /// </summary>
        /// <param name="sendDate">发送时间</param>
        /// <returns>this</returns>
        public Mail SetSendDate(DateTime sendDate)
        {
            SendDate = sendDate;
            return this;
        }
    }
}
