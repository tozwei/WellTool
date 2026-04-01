using System;
using System.IO;

namespace WellTool.Poi.Ofd
{
    /// <summary>
    /// OFD文件生成器
    /// </summary>
    public class OfdWriter : IDisposable
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">生成的文件</param>
        public OfdWriter(string file) : this(new FileInfo(file))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">生成的文件</param>
        public OfdWriter(FileInfo file)
        {
            // TODO: 实现 OFD 文件生成逻辑
            // 需要使用第三方库来支持 OFD 格式
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="outputStream">需要输出的流</param>
        public OfdWriter(Stream outputStream)
        {
            // TODO: 实现 OFD 文件生成逻辑
            // 需要使用第三方库来支持 OFD 格式
        }

        /// <summary>
        /// 增加文本内容
        /// </summary>
        /// <param name="texts">文本</param>
        /// <returns>this</returns>
        public OfdWriter AddText(params string[] texts)
        {
            // TODO: 实现添加文本的逻辑
            return this;
        }

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public OfdWriter AddPicture(string picFile, int width, int height)
        {
            return AddPicture(new FileInfo(picFile), width, height);
        }

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public OfdWriter AddPicture(FileInfo picFile, int width, int height)
        {
            // TODO: 实现添加图片的逻辑
            return this;
        }

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>this</returns>
        public OfdWriter Add(object element)
        {
            // TODO: 实现添加元素的逻辑
            return this;
        }

        /// <summary>
        /// 增加注释，比如水印等
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="annotation">注释</param>
        /// <returns>this</returns>
        public OfdWriter Add(int page, object annotation)
        {
            // TODO: 实现添加注释的逻辑
            return this;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // TODO: 实现资源释放逻辑
        }
    }
}