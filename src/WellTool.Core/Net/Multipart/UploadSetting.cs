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
        /// 如果文件大小小于这个边界，将保存