using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WellTool.Extra.Qrcode
{
    /// <summary>
    /// BufferedImage图片二维码源
    /// </summary>
    public class BufferedImageLuminanceSource
    {
        private readonly Bitmap _image;
        private readonly int _left;
        private readonly int _top;
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="image">图片</param>
        public BufferedImageLuminanceSource(Bitmap image)
            : this(image, 0, 0, image.Width, image.Height)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="left">左边间隔</param>
        /// <param name="top">顶部间隔</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public BufferedImageLuminanceSource(Bitmap image, int left, int top, int width, int height)
        {
            _width = width;
            _height = height;
            _left = left;
            _top = top;

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            if (left + width > sourceWidth || top + height > sourceHeight)
            {
                throw new ArgumentException("Crop rectangle does not fit within image data.");
            }

            _image = new Bitmap(sourceWidth, sourceHeight, PixelFormat.Format8bppIndexed);

            // 转换图像为灰度
            using (var g = Graphics.FromImage(_image))
            {
                g.DrawImage(image, 0, 0);
                g.Dispose();
            }

            // 将图像转换为灰度
            ConvertToGrayscale(_image);
        }

        private void ConvertToGrayscale(Bitmap img)
        {
            var palette = img.Palette;
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            img.Palette = palette;
        }

        /// <summary>
        /// 获取指定行的亮度数据
        /// </summary>
        /// <param name="y">行索引</param>
        /// <param name="row">行数据缓冲区</param>
        /// <returns>行数据</returns>
        public byte[] GetRow(int y, byte[] row)
        {
            if (y < 0 || y >= _height)
            {
                throw new ArgumentException($"Requested row is outside the image: {y}");
            }

            if (row == null || row.Length < _width)
            {
                row = new byte[_width];
            }

            for (int x = 0; x < _width; x++)
            {
                var pixel = _image.GetPixel(_left + x, _top + y);
                row[x] = (byte)((pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114) + 0.5);
            }

            return row;
        }

        /// <summary>
        /// 获取所有亮度数据
        /// </summary>
        /// <returns>亮度数据</returns>
        public byte[] GetMatrix()
        {
            int area = _width * _height;
            byte[] matrix = new byte[area];

            int index = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var pixel = _image.GetPixel(_left + x, _top + y);
                    matrix[index++] = (byte)((pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114) + 0.5);
                }
            }

            return matrix;
        }

        /// <summary>
        /// 获取原始图像
        /// </summary>
        public Bitmap Image => _image;

        /// <summary>
        /// 获取左边间隔
        /// </summary>
        public int Left => _left;

        /// <summary>
        /// 获取顶部间隔
        /// </summary>
        public int Top => _top;

        /// <summary>
        /// 获取宽度
        /// </summary>
        public new int Width => _width;

        /// <summary>
        /// 获取高度
        /// </summary>
        public new int Height => _height;
    }
}
