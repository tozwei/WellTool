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

using System.IO;
using System.Net;

namespace WellTool.Extra;

/// <summary>
/// FTP工具类
/// </summary>
public class FtpUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static FtpUtil Instance { get; } = new FtpUtil();

    /// <summary>
    /// 上传文件到FTP服务器
    /// </summary>
    /// <param name="ftpUrl">FTP服务器地址</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="remoteFilePath">远程文件路径</param>
    /// <param name="username">FTP用户名</param>
    /// <param name="password">FTP密码</param>
    public void Upload(string ftpUrl, string localFilePath, string remoteFilePath, string username, string password)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpUrl}/{remoteFilePath}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, password);

            using var fileStream = new FileStream(localFilePath, FileMode.Open);
            using var requestStream = request.GetRequestStream();
            fileStream.CopyTo(requestStream);

            using var response = (FtpWebResponse)request.GetResponse();
            if (response.StatusCode != FtpStatusCode.ClosingData)
            {
                throw new FtpException($"上传失败: {response.StatusDescription}");
            }
        }
        catch (System.Exception ex)
        {
            throw new FtpException("上传文件失败", ex);
        }
    }

    /// <summary>
    /// 从FTP服务器下载文件
    /// </summary>
    /// <param name="ftpUrl">FTP服务器地址</param>
    /// <param name="remoteFilePath">远程文件路径</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="username">FTP用户名</param>
    /// <param name="password">FTP密码</param>
    public void Download(string ftpUrl, string remoteFilePath, string localFilePath, string username, string password)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpUrl}/{remoteFilePath}");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);

            using var response = (FtpWebResponse)request.GetResponse();
            using var responseStream = response.GetResponseStream();
            using var fileStream = new FileStream(localFilePath, FileMode.Create);
            responseStream.CopyTo(fileStream);

            if (response.StatusCode != FtpStatusCode.ClosingData)
            {
                throw new FtpException($"下载失败: {response.StatusDescription}");
            }
        }
        catch (System.Exception ex)
        {
            throw new FtpException("下载文件失败", ex);
        }
    }

    /// <summary>
    /// 删除FTP服务器上的文件
    /// </summary>
    /// <param name="ftpUrl">FTP服务器地址</param>
    /// <param name="remoteFilePath">远程文件路径</param>
    /// <param name="username">FTP用户名</param>
    /// <param name="password">FTP密码</param>
    public void Delete(string ftpUrl, string remoteFilePath, string username, string password)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpUrl}/{remoteFilePath}");
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(username, password);

            using var response = (FtpWebResponse)request.GetResponse();
            if (response.StatusCode != FtpStatusCode.FileActionOK)
            {
                throw new FtpException($"删除失败: {response.StatusDescription}");
            }
        }
        catch (System.Exception ex)
        {
            throw new FtpException("删除文件失败", ex);
        }
    }

    /// <summary>
    /// 列出FTP服务器上的文件
    /// </summary>
    /// <param name="ftpUrl">FTP服务器地址</param>
    /// <param name="remoteDirectoryPath">远程目录路径</param>
    /// <param name="username">FTP用户名</param>
    /// <param name="password">FTP密码</param>
    /// <returns>文件列表</returns>
    public List<string> ListFiles(string ftpUrl, string remoteDirectoryPath, string username, string password)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpUrl}/{remoteDirectoryPath}");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(username, password);

            using var response = (FtpWebResponse)request.GetResponse();
            using var responseStream = response.GetResponseStream();
            using var reader = new StreamReader(responseStream);

            var files = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                files.Add(line);
            }

            return files;
        }
        catch (System.Exception ex)
        {
            throw new FtpException("列出文件失败", ex);
        }
    }
}

/// <summary>
/// FTP异常
/// </summary>
public class FtpException : System.Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public FtpException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public FtpException(string message, Exception innerException) : base(message, innerException)
    {}
}