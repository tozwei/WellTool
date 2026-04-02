namespace WellTool.Core.Net.Multipart
{
    /// <summary>
    /// 上传文件设定文件
    /// </summary>
    public class UploadSetting
    {
        /// <summary>
        /// 最大文件大小，默认无限制
        /// </summary>
        protected long _maxFileSize = -1;

        /// <summary>
        /// 文件保存到内存的边界
        /// </summary>
        protected int _memoryThreshold = 8192;

        /// <summary>
        /// 临时文件目录
        /// </summary>
        protected string _tmpUploadPath;

        /// <summary>
        /// 文件扩展名限定
        /// </summary>
        protected string[] _fileExts;

        /// <summary>
        /// 扩展名是允许列表还是禁止列表
        /// </summary>
        protected bool _isAllowFileExts = true;

        /// <summary>
        /// 构造
        /// </summary>
        public UploadSetting()
        {
        }

        /// <summary>
        /// 获得最大文件大小，-1表示无限制
        /// </summary>
        public long MaxFileSize => _maxFileSize;

        /// <summary>
        /// 设定最大文件大小，-1表示无限制
        /// </summary>
        /// <param name="maxFileSize">最大文件大小</param>
        public void SetMaxFileSize(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        /// <summary>
        /// 文件保存到内存的边界
        /// </summary>
        public int MemoryThreshold => _memoryThreshold;

        /// <summary>
        /// 设定文件保存到内存的边界
        /// 如果文件大小小于这个边界，将保存于内存中，否则保存至临时目录中
        /// </summary>
        /// <param name="memoryThreshold">文件保存到内存的边界</param>
        public void SetMemoryThreshold(int memoryThreshold)
        {
            _memoryThreshold = memoryThreshold;
        }

        /// <summary>
        /// 上传文件的临时目录，若为空，使用系统目录
        /// </summary>
        public string TmpUploadPath => _tmpUploadPath;

        /// <summary>
        /// 设定上传文件的临时目录，null表示使用系统临时目录
        /// </summary>
        /// <param name="tmpUploadPath">临时目录，绝对路径</param>
        public void SetTmpUploadPath(string tmpUploadPath)
        {
            _tmpUploadPath = tmpUploadPath;
        }

        /// <summary>
        /// 文件扩展名限定列表
        /// </summary>
        public string[] FileExts => _fileExts;

        /// <summary>
        /// 设定文件扩展名限定里列表
        /// 禁止列表还是允许列表取决于isAllowFileExts
        /// </summary>
        /// <param name="fileExts">文件扩展名列表</param>
        public void SetFileExts(string[] fileExts)
        {
            _fileExts = fileExts;
        }

        /// <summary>
        /// 是否允许文件扩展名
        /// 若true表示只允许列表里的扩展名，否则是禁止列表里的扩展名
        /// </summary>
        public bool IsAllowFileExts => _isAllowFileExts;

        /// <summary>
        /// 设定是否允许扩展名
        /// 若true表示只允许列表里的扩展名，否则是禁止列表里的扩展名
        /// </summary>
        /// <param name="isAllowFileExts">是否允许文件扩展名</param>
        public void SetAllowFileExts(bool isAllowFileExts)
        {
            _isAllowFileExts = isAllowFileExts;
        }
    }
}