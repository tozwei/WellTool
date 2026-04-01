using NPOI.XWPF.UserModel;
using System.IO;
using WellTool.Poi.Exceptions;

namespace WellTool.Poi.Word
{
    /// <summary>
    /// Word Document工具
    /// </summary>
    public static class DocUtil
    {
        /// <summary>
        /// 创建<see cref="XWPFDocument"/>，如果文件已存在则读取之，否则创建新的
        /// </summary>
        /// <param name="file">docx文件</param>
        /// <returns><see cref="XWPFDocument"/></returns>
        public static XWPFDocument Create(string file)
        {
            return Create(new FileInfo(file));
        }

        /// <summary>
        /// 创建<see cref="XWPFDocument"/>，如果文件已存在则读取之，否则创建新的
        /// </summary>
        /// <param name="file">docx文件</param>
        /// <returns><see cref="XWPFDocument"/></returns>
        public static XWPFDocument Create(FileInfo file)
        {
            try
            {
                if (file.Exists)
                {
                    using (var stream = file.OpenRead())
                    {
                        return new XWPFDocument(stream);
                    }
                }
                else
                {
                    return new XWPFDocument();
                }
            }
            catch (Exception e)
            {
                throw new POIException(e.Message, e);
            }
        }
    }
}