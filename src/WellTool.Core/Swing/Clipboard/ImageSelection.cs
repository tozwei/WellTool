using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WellTool.Core.Swing.Clipboard
{
    /// <summary>
    /// 图片选择器，用于将图片放入剪贴板
    /// </summary>
    public class ImageSelection
    {
        private readonly BitmapSource _image;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="image">图片</param>
        public ImageSelection(BitmapSource image)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        /// <summary>
        /// 从文件创建图片选择器
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static ImageSelection FromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Image file not found", filePath);
            }

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return new ImageSelection(bitmap);
        }

        /// <summary>
        /// 从字节数组创建图片选择器
        /// </summary>
        /// <param name="data">图片数据</param>
        public static ImageSelection FromStream(Stream data)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = data;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();

            return new ImageSelection(bitmap);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        public BitmapSource GetImage()
        {
            return _image;
        }

        /// <summary>
        /// 设置到剪贴板
        /// </summary>
        public void SetToClipboard()
        {
            System.Windows.Clipboard.SetImage(_image);
        }
    }
}
