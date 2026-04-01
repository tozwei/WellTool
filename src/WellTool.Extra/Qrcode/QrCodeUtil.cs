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
            // 简化实现，实际项目中需要使用二维码库生成SVG
            return $"<svg width=\"{qrConfig.GetWidth()}\" height=\"{qrConfig.GetHeight()}\" xmlns=\"http://www.w3.org/2000/svg\"><text x=\"50%\" y=\"50%\" text-anchor=\"middle\" dominant-baseline=\"middle\">{content}</text></svg>";
        }

        /// <summary>
        /// 生成ASCII Art字符画
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <returns>ASCII Art字符画</returns>
        public static string GenerateAsAsciiArt(string content, QrConfig qrConfig)
        {
            // 简化实现，实际项目中需要使用二维码库生成ASCII Art
            return $"ASCII Art: {content}";
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="qrConfig">二维码配置，包括宽度、高度、边距、颜色等</param>
        /// <returns>二维码图片</returns>
        public static Image Generate(string content, QrConfig qrConfig)
        {
            // 简化实现，实际项目中需要使用二维码库生成图片
            var bitmap = new Bitmap(qrConfig.GetWidth(), qrConfig.GetHeight());
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                using (var font = new Font("Arial", 12))
                using (var brush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(content, font, brush, 10, 10);
                }
            }
            return bitmap;
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