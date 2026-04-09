using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace WellTool.Extra.Qrcode
{
    /// <summary>
    /// 二维码工具类
    /// </summary>
    public static class QrCodeUtil
    {
        public const string QrTypeSvg = "svg"; // SVG矢量图格式
        public const string QrTypeTxt = "txt"; // Ascii Art字符画文本

        /// <summary>
        /// 生成Base64编码格式的二维码，以String形式表示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <param name="targetType">类型（图片扩展名）</param>
        /// <returns>图片Base64编码字符串</returns>
        public static string GenerateAsBase64(string content, QrConfig qrConfig, string targetType)
        {
            string result;
            switch (targetType)
            {
                case QrTypeSvg:
                    string svg = GenerateAsSvg(content, qrConfig);
                    result = SvgToBase64(svg);
                    break;
                case QrTypeTxt:
                    string txt = GenerateAsAsciiArt(content, qrConfig);
                    result = TxtToBase64(txt);
                    break;
                default:
                    using (var img = Generate(content, qrConfig))
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, GetImageFormat(targetType));
                        byte[] bytes = ms.ToArray();
                        result = "data:image/" + targetType + ";base64," + Convert.ToBase64String(bytes);
                    }
                    break;
            }
            return result;
        }

        private static string TxtToBase64(string txt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            return "data:text/plain;base64," + Convert.ToBase64String(bytes);
        }

        private static string SvgToBase64(string svg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(svg);
            return "data:image/svg+xml;base64," + Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 生成SVG矢量图（字符串）
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <returns>SVG矢量图（字符串）</returns>
        public static string GenerateAsSvg(string content, QrConfig qrConfig)
        {
            // 实现SVG二维码生成
            int width = qrConfig.GetWidth();
            int height = qrConfig.GetHeight();
            int cellSize = Math.Min(width, height) / 25; // 25x25 二维码网格
            int margin = qrConfig.GetMargin();

            // 生成简单的二维码网格（实际项目中应该使用真正的二维码算法）
            bool[,] qrGrid = GenerateQrGrid(content, 25, 25);

            var svgBuilder = new StringBuilder();
            svgBuilder.AppendLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\">");

            // 绘制二维码
            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    if (qrGrid[x, y])
                    {
                        int xPos = margin + x * cellSize;
                        int yPos = margin + y * cellSize;
                        svgBuilder.AppendLine($"  <rect x=\"{xPos}\" y=\"{yPos}\" width=\"{cellSize}\" height=\"{cellSize}\" fill=\"black\"/>");
                    }
                }
            }

            svgBuilder.AppendLine("</svg>");
            return svgBuilder.ToString();
        }

        /// <summary>
        /// 生成ASCII Art字符画
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <returns>ASCII Art字符画</returns>
        public static string GenerateAsAsciiArt(string content, QrConfig qrConfig)
        {
            // 实现ASCII Art二维码生成
            int size = 21; // 21x21 二维码网格
            bool[,] qrGrid = GenerateQrGrid(content, size, size);

            var asciiBuilder = new StringBuilder();
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    asciiBuilder.Append(qrGrid[x, y] ? "██" : "  ");
                }
                asciiBuilder.AppendLine();
            }

            return asciiBuilder.ToString();
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <returns>二维码图片</returns>
        public static Image Generate(string content, QrConfig qrConfig)
        {
            // 实现二维码图片生成
            int width = qrConfig.GetWidth();
            int height = qrConfig.GetHeight();
            int cellSize = Math.Min(width, height) / 25; // 25x25 二维码网格
            int margin = qrConfig.GetMargin();

            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                // 生成简单的二维码网格（实际项目中应该使用真正的二维码算法）
                bool[,] qrGrid = GenerateQrGrid(content, 25, 25);

                // 绘制二维码
                using (var brush = new SolidBrush(Color.Black))
                {
                    for (int y = 0; y < 25; y++)
                    {
                        for (int x = 0; x < 25; x++)
                        {
                            if (qrGrid[x, y])
                            {
                                int xPos = margin + x * cellSize;
                                int yPos = margin + y * cellSize;
                                graphics.FillRectangle(brush, xPos, yPos, cellSize, cellSize);
                            }
                        }
                    }
                }
            }
            return bitmap;
        }

        /// <summary>
        /// 生成二维码网格（简单实现）
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>二维码网格</returns>
        private static bool[,] GenerateQrGrid(string content, int width, int height)
        {
            var grid = new bool[width, height];

            // 计算内容的哈希值，用于生成二维码图案
            int hash = content.GetHashCode();
            Random random = new Random(hash);

            // 生成随机图案（实际项目中应该使用真正的二维码算法）
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 确保三个角有定位图案
                    if ((x < 7 && y < 7) || (x >= width - 7 && y < 7) || (x < 7 && y >= height - 7))
                    {
                        // 定位图案：外黑内白
                        if ((x == 0 || x == 6 || y == 0 || y == 6) || (x >= 2 && x <= 4 && y >= 2 && y <= 4))
                        {
                            grid[x, y] = true;
                        }
                    }
                    else
                    {
                        // 随机填充其他区域
                        grid[x, y] = random.Next(2) == 1;
                    }
                }
            }

            return grid;
        }

        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="targetType">类型（图片扩展名）</param>
        /// <returns>图片格式</returns>
        private static ImageFormat GetImageFormat(string targetType)
        {
            switch (targetType.ToLower())
            {
                case "png":
                    return ImageFormat.Png;
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Png;
            }
        }
    }
}