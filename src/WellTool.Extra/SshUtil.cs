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

namespace WellTool.Extra;

/// <summary>
/// SSH工具类
/// </summary>
public class SshUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static SshUtil Instance { get; } = new SshUtil();

    /// <summary>
    /// 执行SSH命令
    /// </summary>
    /// <param name="host">主机地址</param>
    /// <param name="port">端口</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="command">命令</param>
    /// <returns>命令执行结果</returns>
    public string ExecuteCommand(string host, int port, string username, string password, string command)
    {
        try
        {
            // 简单实现，实际项目中可能需要使用更专业的SSH库
            // 这里只是模拟执行结果
            return $"Command executed: {command}\nHost: {host}:{port}\nUser: {username}";
        }
        catch (System.Exception ex)
        {
            throw new SshException("执行SSH命令失败", ex);
        }
    }

    /// <summary>
    /// 上传文件到SSH服务器
    /// </summary>
    /// <param name="host">主机地址</param>
    /// <param name="port">端口</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="remoteFilePath">远程文件路径</param>
    public void UploadFile(string host, int port, string username, string password, string localFilePath, string remoteFilePath)
    {
        try
        {
            // 简单实现，实际项目中可能需要使用更专业的SSH库
            // 这里只是模拟上传操作
            Console.WriteLine($"Uploading file: {localFilePath} to {remoteFilePath} on {host}:{port}");
        }
        catch (System.Exception ex)
        {
            throw new SshException("上传文件失败", ex);
        }
    }

    /// <summary>
    /// 从SSH服务器下载文件
    /// </summary>
    /// <param name="host">主机地址</param>
    /// <param name="port">端口</param>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="remoteFilePath">远程文件路径</param>
    /// <param name="localFilePath">本地文件路径</param>
    public void DownloadFile(string host, int port, string username, string password, string remoteFilePath, string localFilePath)
    {
        try
        {
            // 简单实现，实际项目中可能需要使用更专业的SSH库
            // 这里只是模拟下载操作
            Console.WriteLine($"Downloading file: {remoteFilePath} to {localFilePath} from {host}:{port}");
        }
        catch (System.Exception ex)
        {
            throw new SshException("下载文件失败", ex);
        }
    }
}

/// <summary>
/// SSH异常
/// </summary>
public class SshException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public SshException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public SshException(string message, Exception innerException) : base(message, innerException)
    {}
}