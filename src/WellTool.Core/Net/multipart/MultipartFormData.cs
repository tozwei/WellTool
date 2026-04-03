using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellTool.Core.Map.Multi;
using WellTool.Core.Collection;

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

        /// <summary>
        /// 构造
        /// </summary>
        public MultipartFormData() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="uploadSetting">上传设定</param>
        public MultipartFormData(UploadSetting uploadSetting)
        {
            _setting = uploadSetting ?? new UploadSetting();
        }

        /// <summary>
        /// 提取上传的文件和表单数据
        /// </summary>
        /// <param name="inputStream">HttpRequest流</param>
        /// <param name="encoding">编码</param>
        /// <exception cref="IOException">IO异常</exception>
        public void ParseRequestStream(Stream inputStream, Encoding encoding)
        {
            SetLoaded();

            var input = new MultipartRequestInputStream(inputStream);
            input.ReadBoundary();
            while (true)
            {
                var header = input.ReadDataHeader(encoding);
                if (header == null)
                {
                    break;
                }

                if (header.IsFile)
                {
                    // 文件类型的表单项
                    var fileName = header.FileName;
                    if (!string.IsNullOrEmpty(fileName) && header.ContentType.Contains("application/x-macbinary"))
                    {
                        input.SkipBytes(128);
                    }
                    var newFile = new UploadFile(header, _setting);
                    if (newFile.ProcessStream(input))
                    {
                        PutFile(header.FormFieldName, newFile);
                    }
                }
                else
                {
                    // 标准表单项
                    PutParameter(header.FormFieldName, input.ReadString(encoding));
                }

                input.SkipBytes(1);
                input.Mark(1);

                // read byte, but may be end of stream
                try
                {
                    var nextByte = input.ReadByte();
                    if (nextByte == '-')
                    {
                        input.Reset();
                        break;
                    }
                }
                catch (IOException)
                {
                    // 到达流的末尾
                    input.Reset();
                    break;
                }
                input.Reset();
            }
        }

        /// <summary>
        /// 返回单一参数值，如果有多个只返回第一个
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <returns>null未找到，否则返回值</returns>
        public string GetParam(string paramName)
        {
            return _requestParameters.Get(paramName, 0);
        }

        /// <summary>
        /// 获得参数名集合
        /// </summary>
        /// <returns>参数名集合</returns>
        public ISet<string> GetParamNames()
        {
            return new HashSet<string>(_requestParameters.Keys);
        }

        /// <summary>
        /// 获得数组表单值
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <returns>数组表单值</returns>
        public string[] GetArrayParam(string paramName)
        {
            var listParam = GetListParam(paramName);
            if (listParam != null)
            {
                return listParam.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 获得集合表单值
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <returns>数组表单值</returns>
        public List<string> GetListParam(string paramName)
        {
            return _requestParameters[paramName];
        }

        /// <summary>
        /// 获取所有属性的集合
        /// </summary>
        /// <returns>所有属性的集合</returns>
        public Dictionary<string, string[]> GetParamMap()
        {
            var result = new Dictionary<string, string[]>();
            foreach (var entry in GetParamListMap())
            {
                result[entry.Key] = entry.Value.ToArray();
            }
            return result;
        }

        /// <summary>
        /// 获取所有属性的集合
        /// </summary>
        /// <returns>所有属性的集合</returns>
        public ListValueMap<string, string> GetParamListMap()
        {
            return _requestParameters;
        }

        /// <summary>
        /// 获取上传的文件
        /// </summary>
        /// <param name="paramName">文件参数名称</param>
        /// <returns>上传的文件， 如果无为null</returns>
        public UploadFile GetFile(string paramName)
        {
            return _requestFiles.Get(paramName, 0);
        }

        /// <summary>
        /// 获得某个属性名的所有文件
        /// 当表单中两个文件使用同一个name的时候
        /// </summary>
        /// <param name="paramName">属性名</param>
        /// <returns>上传的文件列表</returns>
        public UploadFile[] GetFiles(string paramName)
        {
            var fileList = GetFileList(paramName);
            if (fileList != null)
            {
                return fileList.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 获得某个属性名的所有文件
        /// 当表单中两个文件使用同一个name的时候
        /// </summary>
        /// <param name="paramName">属性名</param>
        /// <returns>上传的文件列表</returns>
        public List<UploadFile> GetFileList(string paramName)
        {
            return _requestFiles[paramName];
        }

        /// <summary>
        /// 获取上传的文件属性名集合
        /// </summary>
        /// <returns>上传的文件属性名集合</returns>
        public ISet<string> GetFileParamNames()
        {
            return new HashSet<string>(_requestFiles.Keys);
        }

        /// <summary>
        /// 获取文件映射
        /// </summary>
        /// <returns>文件映射</returns>
        public Dictionary<string, UploadFile[]> GetFileMap()
        {
            var result = new Dictionary<string, UploadFile[]>();
            foreach (var entry in GetFileListValueMap())
            {
                result[entry.Key] = entry.Value.ToArray();
            }
            return result;
        }

        /// <summary>
        /// 获取文件映射
        /// </summary>
        /// <returns>文件映射</returns>
        public ListValueMap<string, UploadFile> GetFileListValueMap()
        {
            return _requestFiles;
        }

        /// <summary>
        /// 是否已被解析
        /// </summary>
        /// <returns>如果流已被解析返回true</returns>
        public bool IsLoaded()
        {
            return _loaded;
        }

        /// <summary>
        /// 加入上传文件
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="uploadFile">文件</param>
        private void PutFile(string name, UploadFile uploadFile)
        {
            _requestFiles.PutValue(name, uploadFile);
        }

        /// <summary>
        /// 加入普通参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        private void PutParameter(string name, string value)
        {
            _requestParameters.PutValue(name, value);
        }

        /// <summary>
        /// 设置使输入流为解析状态，如果已解析，则抛出异常
        /// </summary>
        /// <exception cref="IOException">IO异常</exception>
        private void SetLoaded()
        {
            if (_loaded)
            {
                throw new IOException("Multi-part request already parsed.");
            }
            _loaded = true;
        }
    }
}