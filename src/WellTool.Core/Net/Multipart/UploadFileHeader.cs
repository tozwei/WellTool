using System;
using WellTool.Core.IO;
using WellTool.Core.Util;

namespace WellTool.Core.Net.Multipart
{
    /// <summary>
    /// 上传的文件的头部信息
    /// </summary>
    public class UploadFileHeader
    {
        /// <summary>
        /// 表单字段名
        /// </summary>
        public string FormFieldName { get; private set; }

        /// <summary>
        /// 表单中的文件名，来自客户端传入
        /// </summary>
        public string FormFileName { get; private set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 文件名，不包括路径
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 是否为文件
        /// </summary>
        public bool IsFile { get; private set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// MIME类型
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// MIME子类型
        /// </summary>
        public string MimeSubtype { get; private set; }

        /// <summary>
        /// 内容处置
        /// </summary>
        public string ContentDisposition { get; private set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dataHeader">头信息</param>
        internal UploadFileHeader(string dataHeader)
        {
            ProcessHeaderString(dataHeader);
        }

        /// <summary>
        /// 获得头信息字符串字符串中指定的值
        /// </summary>
        /// <param name="dataHeader">头信息</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        private string GetDataFieldValue(string dataHeader, string fieldName)
        {
            string value = null;
            string token = StrUtil.Format("{}=\"", fieldName);
            int pos = dataHeader.IndexOf(token);
            if (pos > 0)
            {
                int start = pos + token.Length;
                int end = dataHeader.IndexOf('"', start);
                if (start > 0 && end > 0)
                {
                    value = dataHeader.Substring(start, end - start);
                }
            }
            return value;
        }

        /// <summary>
        /// 头信息中获得content type
        /// </summary>
        /// <param name="dataHeader">data header string</param>
        /// <returns>content type or an empty string if no content type defined</returns>
        private string GetContentType(string dataHeader)
        {
            string token = "Content-Type:";
            int start = dataHeader.IndexOf(token);
            if (start == -1)
            {
                return StrUtil.EMPTY;
            }
            start += token.Length;
            return dataHeader.Substring(start).Trim();
        }

        /// <summary>
        /// 获得内容处置
        /// </summary>
        /// <param name="dataHeader">头信息</param>
        /// <returns>内容处置</returns>
        private string GetContentDisposition(string dataHeader)
        {
            int start = dataHeader.IndexOf(':') + 1;
            int end = dataHeader.IndexOf(';');
            return dataHeader.Substring(start, end - start).Trim();
        }

        /// <summary>
        /// 获得MIME类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <returns>MIME类型</returns>
        private string GetMimeType(string contentType)
        {
            int pos = contentType.IndexOf('/');
            if (pos == -1)
            {
                return contentType;
            }
            return contentType.Substring(0, pos);
        }

        /// <summary>
        /// 获得MIME子类型
        /// </summary>
        /// <param name="contentType">内容类型</param>
        /// <returns>MIME子类型</returns>
        private string GetMimeSubtype(string contentType)
        {
            int start = contentType.IndexOf('/');
            if (start == -1)
            {
                return contentType;
            }
            start++;
            return contentType.Substring(start);
        }

        /// <summary>
        /// 处理头字符串，使之转化为字段
        /// </summary>
        /// <param name="dataHeader">头字符串</param>
        private void ProcessHeaderString(string dataHeader)
        {
            IsFile = dataHeader.IndexOf("filename") > 0;
            FormFieldName = GetDataFieldValue(dataHeader, "name");
            if (IsFile)
            {
                FormFileName = GetDataFieldValue(dataHeader, "filename");
                if (FormFileName == null)
                {
                    return;
                }
                if (FormFileName.Length == 0)
                {
                    Path = StrUtil.EMPTY;
                    FileName = StrUtil.EMPTY;
                }
                int ls = FileUtil.LastIndexOfSeparator(FormFileName);
                if (ls == -1)
                {
                    Path = StrUtil.EMPTY;
                    FileName = FormFileName;
                }
                else
                {
                    Path = FormFileName.Substring(0, ls);
                    FileName = FormFileName.Substring(ls + 1);
                }
                if (FileName.Length > 0)
                {
                    ContentType = GetContentType(dataHeader);
                    MimeType = GetMimeType(ContentType);
                    MimeSubtype = GetMimeSubtype(ContentType);
                    ContentDisposition = GetContentDisposition(dataHeader);
                }
            }
        }
    }
}