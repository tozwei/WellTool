using NPOI.Util;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using WellTool.Poi.Exceptions;

namespace WellTool.Poi.Word
{
    /// <summary>
    /// Word docx生成器
    /// </summary>
    public class Word07Writer : IDisposable
    {
        private readonly XWPFDocument doc;
        /// <summary>
        /// 目标文件
        /// </summary>
        protected FileInfo destFile;
        /// <summary>
        /// 是否被关闭
        /// </summary>
        protected bool isClosed;
        /// <summary>
        /// 是否已执行过 Flush 操作
        /// </summary>
        protected bool hasFlushed;

        /// <summary>
        /// 构造
        /// </summary>
        public Word07Writer() : this(new XWPFDocument())
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="destFile">写出的文件</param>
        public Word07Writer(string destFile) : this(new FileInfo(destFile))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="destFile">写出的文件</param>
        public Word07Writer(FileInfo destFile)
        {
            this.doc = DocUtil.Create(destFile);
            this.destFile = destFile;
            this.hasFlushed = false;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="doc"><see cref="XWPFDocument"/></param>
        public Word07Writer(XWPFDocument doc) : this(doc, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="doc"><see cref="XWPFDocument"/></param>
        /// <param name="destFile">写出的文件</param>
        public Word07Writer(XWPFDocument doc, FileInfo destFile)
        {
            this.doc = doc;
            this.destFile = destFile;
            this.hasFlushed = false;
        }

        /// <summary>
        /// 获取<see cref="XWPFDocument"/>
        /// </summary>
        /// <returns><see cref="XWPFDocument"/></returns>
        public XWPFDocument GetDoc()
        {
            return this.doc;
        }

        /// <summary>
        /// 设置写出的目标文件
        /// </summary>
        /// <param name="destFile">目标文件</param>
        /// <returns>this</returns>
        public Word07Writer SetDestFile(string destFile)
        {
            return SetDestFile(new FileInfo(destFile));
        }

        /// <summary>
        /// 设置写出的目标文件
        /// </summary>
        /// <param name="destFile">目标文件</param>
        /// <returns>this</returns>
        public Word07Writer SetDestFile(FileInfo destFile)
        {
            this.destFile = destFile;
            return this;
        }

        /// <summary>
        /// 增加一个段落
        /// </summary>
        /// <param name="font">字体信息</param>
        /// <param name="texts">段落中的文本，支持多个文本作为一个段落</param>
        /// <returns>this</returns>
        public Word07Writer AddText(Font font, params string[] texts)
        {
            return AddText(null, font, null, texts);
        }

        /// <summary>
        /// 增加一个段落
        /// </summary>
        /// <param name="font">字体信息</param>
        /// <param name="color">字体颜色</param>
        /// <param name="texts">段落中的文本，支持多个文本作为一个段落</param>
        /// <returns>this</returns>
        public Word07Writer AddText(Font font, Color color, params string[] texts)
        {
            return AddText(null, font, color, texts);
        }

        /// <summary>
        /// 增加一个段落
        /// </summary>
        /// <param name="align">段落对齐方式</param>
        /// <param name="font">字体信息</param>
        /// <param name="texts">段落中的文本，支持多个文本作为一个段落</param>
        /// <returns>this</returns>
        public Word07Writer AddText(ParagraphAlignment align, Font font, params string[] texts)
        {
            return AddText(align, font, null, texts);
        }

        /// <summary>
        /// 增加一个段落
        /// </summary>
        /// <param name="align">段落对齐方式</param>
        /// <param name="font">字体信息</param>
        /// <param name="color">字体颜色</param>
        /// <param name="texts">段落中的文本，支持多个文本作为一个段落</param>
        /// <returns>this</returns>
        public Word07Writer AddText(ParagraphAlignment? align, Font font, Color? color, params string[] texts)
        {
            var p = this.doc.CreateParagraph();
            if (align.HasValue)
            {
                p.Alignment = align.Value;
            }
            if (texts != null && texts.Length > 0)
            {
                foreach (var text in texts)
                {
                    var run = p.CreateRun();
                    run.SetText(text);
                    if (font != null)
                    {
                        run.SetFontFamily(font.FontFamily.Name, NPOI.XWPF.UserModel.FontCharRange.None);
                        run.FontSize = (int)font.Size;
                        run.IsBold = font.Bold;
                        run.IsItalic = font.Italic;
                    }
                    if (color.HasValue)
                    {
                        // setColor expects a pure RGB hex string (no alpha channel)
                        var hexColor = $"{color.Value.R:X2}{color.Value.G:X2}{color.Value.B:X2}";
                        run.SetColor(hexColor);
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// 增加表格数据
        /// </summary>
        /// <param name="data">表格数据，多行数据。元素表示一行数据，当为集合或者数组时，为一行；当为Map或者Bean时key表示标题，values为数据</param>
        /// <returns>this</returns>
        public Word07Writer AddTable(IEnumerable<object> data)
        {
            TableUtil.CreateTable(this.doc, data);
            return this;
        }

        /// <summary>
        /// 增加图片，单独成段落
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public Word07Writer AddPicture(string picFile, int width, int height)
        {
            return AddPicture(new FileInfo(picFile), width, height);
        }

        /// <summary>
        /// 增加图片，单独成段落
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public Word07Writer AddPicture(FileInfo picFile, int width, int height)
        {
            var fileName = picFile.Name;
            var extName = Path.GetExtension(fileName).TrimStart('.').ToUpper();
            PicType picType;
            try
            {
                picType = (PicType)Enum.Parse(typeof(PicType), extName);
            }
            catch (ArgumentException)
            {
                // 默认值
                picType = PicType.JPEG;
            }
            using (var stream = picFile.OpenRead())
            {
                return AddPicture(stream, picType, fileName, width, height);
            }
        }

        /// <summary>
        /// 增加图片，单独成段落，增加后图片流关闭，默认居中对齐
        /// </summary>
        /// <param name="stream">图片流</param>
        /// <param name="picType">图片类型</param>
        /// <param name="fileName">文件名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public Word07Writer AddPicture(Stream stream, PicType picType, string fileName, int width, int height)
        {
            return AddPicture(stream, picType, fileName, width, height, ParagraphAlignment.CENTER);
        }

        /// <summary>
        /// 增加图片，单独成段落，增加后图片流关闭
        /// </summary>
        /// <param name="stream">图片流</param>
        /// <param name="picType">图片类型</param>
        /// <param name="fileName">文件名</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="align">图片的对齐方式</param>
        /// <returns>this</returns>
        public Word07Writer AddPicture(Stream stream, PicType picType, string fileName, int width, int height, ParagraphAlignment align)
        {
            var paragraph = doc.CreateParagraph();
            paragraph.Alignment = align;
            var run = paragraph.CreateRun();
            try
            {
                run.AddPicture(stream, (int)picType, fileName, Units.ToEMU(width), Units.ToEMU(height));
            }
            catch (Exception e)
            {
                throw new POIException(e.Message, e);
            }
            return this;
        }

        /// <summary>
        /// 将Word Document刷出到预定义的文件<br>
        /// 如果用户未自定义输出的文件，将抛出<see cref="NullReferenceException"/><br>
        /// 预定义文件可以通过<see cref="SetDestFile(FileInfo)"/> 方法预定义，或者通过构造定义
        /// </summary>
        /// <returns>this</returns>
        public Word07Writer Flush()
        {
            return Flush(this.destFile);
        }

        /// <summary>
        /// 将Word Document刷出到文件<br>
        /// 如果用户未自定义输出的文件，将抛出<see cref="NullReferenceException"/>
        /// </summary>
        /// <param name="destFile">写出到的文件</param>
        /// <returns>this</returns>
        public Word07Writer Flush(string destFile)
        {
            return Flush(new FileInfo(destFile));
        }

        /// <summary>
        /// 将Word Document刷出到文件<br>
        /// 如果用户未自定义输出的文件，将抛出<see cref="NullReferenceException"/>
        /// </summary>
        /// <param name="destFile">写出到的文件</param>
        /// <returns>this</returns>
        public Word07Writer Flush(FileInfo destFile)
        {
            if (destFile == null)
            {
                throw new NullReferenceException("[destFile] is null, and you must call setDestFile(File) first or call flush(OutputStream).");
            }
            using (var stream = destFile.OpenWrite())
            {
                return Flush(stream, true);
            }
        }

        /// <summary>
        /// 将Word Document刷出到输出流
        /// </summary>
        /// <param name="stream">输出流</param>
        /// <returns>this</returns>
        public Word07Writer Flush(Stream stream)
        {
            return Flush(stream, false);
        }

        /// <summary>
        /// 将Word Document刷出到输出流
        /// </summary>
        /// <param name="stream">输出流</param>
        /// <param name="isCloseStream">是否关闭输出流</param>
        /// <returns>this</returns>
        public Word07Writer Flush(Stream stream, bool isCloseStream)
        {
            if (this.isClosed)
            {
                throw new InvalidOperationException("WordWriter has been closed!");
            }
            try
            {
                this.doc.Write(stream);
                stream.Flush();
                this.hasFlushed = true;
            }
            catch (Exception e)
            {
                throw new IOException(e.Message, e);
            }
            finally
            {
                if (isCloseStream)
                {
                    stream.Close();
                }
            }
            return this;
        }

        /// <summary>
        /// 关闭Word文档<br>
        /// 如果用户设定了目标文件，先写出目标文件后给关闭工作簿
        /// </summary>
        public void Dispose()
        {
            if (this.destFile != null)
            {
                Flush();
            }
            CloseWithoutFlush();
        }

        /// <summary>
        /// 关闭Word文档但是不写出
        /// </summary>
        protected void CloseWithoutFlush()
        {
            this.doc.Close();
            this.isClosed = true;
        }
    }
}