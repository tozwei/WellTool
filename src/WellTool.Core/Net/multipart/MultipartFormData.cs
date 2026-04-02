using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellTool.Core.Collection;
using WellTool.Core.Convert;
using WellTool.Core.Map.Multi;

namespace WellTool.Core.Net.Multipart
{
    /// <summary>
    /// HttpRequest解析器
    /// </summary>
    public class MultipartFormData
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        private readonly ListValueMap<string, string> _requestParameters = new ListValueMap<string, string>();
        /// <summary>
        /// 请求文件
        /// </summary>
        private readonly ListValueMap<string, UploadFile> _requestFiles = new ListValueMap<string, UploadFile>();
        /// <summary>
        /// 上传选项
        /// </summary>
        private readonly UploadSetting _setting;

        /// <summary>
        /// 是否解析完毕
        /// </summary>
        private bool _loaded;
