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

namespace WellTool.Core.IO
{
    /// <summary>
    /// 文件类型魔数封装
    /// </summary>
    public abstract class FileMagicNumber
    {
        /// <summary>
        /// 未知文件类型
        /// </summary>
        public static readonly FileMagicNumber Unknown = new UnknownFileMagicNumber();

        /// <summary>
        /// JPEG 图片
        /// </summary>
        public static readonly FileMagicNumber Jpeg = new JpegFileMagicNumber();

        /// <summary>
        /// PNG 图片
        /// </summary>
        public static readonly FileMagicNumber Png = new PngFileMagicNumber();

        /// <summary>
        /// GIF 图片
        /// </summary>
        public static readonly FileMagicNumber Gif = new GifFileMagicNumber();

        /// <summary>
        /// BMP 图片
        /// </summary>
        public static readonly FileMagicNumber Bmp = new BmpFileMagicNumber();

        /// <summary>
        /// ZIP 压缩文件
        /// </summary>
        public static readonly FileMagicNumber Zip = new ZipFileMagicNumber();

        /// <summary>
        /// PDF 文件
        /// </summary>
        public static readonly FileMagicNumber Pdf = new PdfFileMagicNumber();

        /// <summary>
        /// EXE 可执行文件
        /// </summary>
        public static readonly FileMagicNumber Exe = new ExeFileMagicNumber();

        /// <summary>
        /// 所有文件类型
        /// </summary>
        public static readonly FileMagicNumber[] All = new FileMagicNumber[]
        {
            Jpeg,
            Png,
            Gif,
            Bmp,
            Zip,
            Pdf,
            Exe
        };

        /// <summary>
        /// MIME 类型
        /// </summary>
        public string MimeType { get; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mimeType">MIME 类型</param>
        /// <param name="extension">文件扩展名</param>
        protected FileMagicNumber(string mimeType, string extension)
        {
            MimeType = mimeType;
            Extension = extension;
        }

        /// <summary>
        /// 匹配文件类型
        /// </summary>
        /// <param name="bytes">文件字节</param>
        /// <returns>是否匹配</returns>
        public abstract bool Match(byte[] bytes);

        /// <summary>
        /// 根据给定的字节，获取对应识别到的文件类型
        /// </summary>
        /// <param name="bytes">文件字节</param>
        /// <returns>文件类型</returns>
        public static FileMagicNumber GetMagicNumber(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return Unknown;
            }

            foreach (var magicNumber in All)
            {
                if (magicNumber.Match(bytes))
                {
                    return magicNumber;
                }
            }

            return Unknown;
        }

        /// <summary>
        /// 未知文件类型
        /// </summary>
        private class UnknownFileMagicNumber : FileMagicNumber
        {
            public UnknownFileMagicNumber() : base(null, null)
            { }

            public override bool Match(byte[] bytes)
            {
                return false;
            }
        }

        /// <summary>
        /// JPEG 图片
        /// </summary>
        private class JpegFileMagicNumber : FileMagicNumber
        {
            public JpegFileMagicNumber() : base("image/jpeg", "jpg")
            { }

            public override bool Match(byte[] bytes)
            {
                return bytes.Length > 2
                    && bytes[0] == 0xff
                    && bytes[1] == 0xd8
                    && bytes[2] == 0xff;
            }
        }

        /// <summary>
        /// PNG 图片
        /// </summary>
        private class PngFileMagicNumber : FileMagicNumber
        {
            public PngFileMagicNumber() : base("image/png", "png")
            { }

            public override bool Match(byte[] bytes)
            {
                return bytes.Length > 3
                    && bytes[0] == 0x89
                    && bytes[1] == 0x50
                    && bytes[2] == 0x4e
                    && bytes[3] == 0x47;
            }
        }

        /// <summary>
        /// GIF 图片
        /// </summary>
        private class GifFileMagicNumber : FileMagicNumber
        {
            public GifFileMagicNumber() : base("image/gif", "gif")
            { }

            public override bool Match(byte[] bytes)
            {
                return bytes.Length > 2
                    && bytes[0] == 0x47
                    && bytes[1] == 0x49
                    && bytes[2] == 0x46;
            }
        }

        /// <summary>
        /// BMP 图片
        /// </summary>
        private class BmpFileMagicNumber : FileMagicNumber
        {
            public BmpFileMagicNumber() : base("image/bmp", "bmp")
            { }

            public override bool Match(byte[] bytes)
            {
                return bytes.Length > 1
                    && bytes[0] == 0x42
                    && bytes[1] == 0x4d;
            }
        }

        /// <summary>
        /// ZIP 压缩文件
        /// </summary>
        private class ZipFileMagicNumber : FileMagicNumber
        {
            public ZipFileMagicNumber() : base("application/zip", "zip")
            { }

            public override bool Match(byte[] bytes)
            {
                if (bytes.Length < 4)
                {
                    return false;
                }
                bool flag1 = bytes[0] == 0x50 && bytes[1] == 0x4b;
                bool flag2 = bytes[2] == 0x03 || bytes[2] == 0x05 || bytes[2] == 0x07;
                bool flag3 = bytes[3] == 0x04 || bytes[3] == 0x06 || bytes[3] == 0x08;
                return flag1 && flag2 && flag3;
            }
        }

        /// <summary>
        /// PDF 文件
        /// </summary>
        private class PdfFileMagicNumber : FileMagicNumber
        {
            public PdfFileMagicNumber() : base("application/pdf", "pdf")
            { }

            public override bool Match(byte[] bytes)
            {
                // 去除bom头并且跳过三个字节
                if (bytes.Length > 3 && bytes[0] == 0xEF
                    && bytes[1] == 0xBB && bytes[2] == 0xBF)
                {
                    var newBytes = new byte[bytes.Length - 3];
                    Array.Copy(bytes, 3, newBytes, 0, newBytes.Length);
                    bytes = newBytes;
                }
                return bytes.Length > 3
                    && bytes[0] == 0x25
                    && bytes[1] == 0x50
                    && bytes[2] == 0x44
                    && bytes[3] == 0x46;
            }
        }

        /// <summary>
        /// EXE 可执行文件
        /// </summary>
        private class ExeFileMagicNumber : FileMagicNumber
        {
            public ExeFileMagicNumber() : base("application/x-msdownload", "exe")
            { }

            public override bool Match(byte[] bytes)
            {
                return bytes.Length > 1
                    && bytes[0] == 0x4d
                    && bytes[1] == 0x5a;
            }
        }
    }
}