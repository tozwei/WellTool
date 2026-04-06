using System.IO;

namespace WellTool.Extra.Compress.Extractor
{
    /// <summary>
    /// 7-Zip 条目输入流
    /// 用于读取 7z 压缩包中的单个条目
    /// </summary>
    public class Seven7EntryInputStream : Stream
    {
        /// <summary>
        /// 读取字节数
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        /// <summary>
        /// 是否可读
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// 是否可查找
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// 是否可写
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// 长度
        /// </summary>
        public override long Length => 0;

        /// <summary>
        /// 位置
        /// </summary>
        public override long Position
        {
            get => 0;
            set { }
        }

        /// <summary>
        /// 查找
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        /// <summary>
        /// 设置长度
        /// </summary>
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Seven7EntryInputStream does not support writing.");
        }
    }
}
