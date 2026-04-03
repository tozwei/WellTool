using System.Text;

namespace WellTool.Extra.Ftp;

/// <summary>
/// FTP操作接口
/// </summary>
public interface IFtp : IDisposable
{
    /// <summary>
    /// 连接状态
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// 编码
    /// </summary>
    Encoding Charset { get; set; }

    /// <summary>
    /// 连接到FTP服务器
    /// </summary>
    void Connect();

    /// <summary>
    /// 断开连接
    /// </summary>
    void Disconnect();

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="srcPath">源路径(本地)</param>
    /// <param name="destPath">目标路径(远程)</param>
    void Put(string srcPath, string destPath);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="srcStream">源数据流</param>
    /// <param name="destPath">目标路径(远程)</param>
    void Put(Stream srcStream, string destPath);

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcPath">源路径(远程)</param>
    /// <param name="destPath">目标路径(本地)</param>
    void Get(string srcPath, string destPath);

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="srcPath">源路径(远程)</param>
    /// <returns>文件数据流</returns>
    Stream Get(string srcPath);

    /// <summary>
    /// 列出目录下的文件或目录名称
    /// </summary>
    /// <param name="dirPath">目录路径</param>
    /// <returns>名称列表</returns>
    IEnumerable<string> ListNames(string dirPath);

    /// <summary>
    /// 列出目录下的文件信息
    /// </summary>
    /// <param name="dirPath">目录路径</param>
    /// <returns>文件信息列表</returns>
    IEnumerable<FtpFile> ListFiles(string dirPath);

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="dirPath">目录路径</param>
    void Mkdir(string dirPath);

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="dirPath">目录路径</param>
    /// <param name="recursive">是否递归删除</param>
    void Rmdir(string dirPath, bool recursive = false);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    void Del(string filePath);

    /// <summary>
    /// 重命名文件或目录
    /// </summary>
    /// <param name="oldPath">旧路径</param>
    /// <param name="newPath">新路径</param>
    void Rename(string oldPath, string newPath);

    /// <summary>
    /// 检查路径是否存在
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>是否存在</returns>
    bool Exists(string path);

    /// <summary>
    /// 获取当前目录
    /// </summary>
    /// <returns>当前目录路径</returns>
    string GetCurrentDirectory();

    /// <summary>
    /// 切换到指定目录
    /// </summary>
    /// <param name="dirPath">目录路径</param>
    void Cd(string dirPath);
}

/// <summary>
/// FTP文件信息
/// </summary>
public class FtpFile
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 完整路径
    /// </summary>
    public string FullPath { get; set; } = string.Empty;

    /// <summary>
    /// 是否是目录
    /// </summary>
    public bool IsDirectory { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastWriteTime { get; set; }

    /// <summary>
    /// 权限
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 所有者
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// 所属组
    /// </summary>
    public string? Group { get; set; }
}
