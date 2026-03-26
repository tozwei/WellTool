using System;
using System.IO;

#if WINDOWS
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
#endif

namespace WellTool.Core.Img
{
    public static class ImgUtil
    {
#if WINDOWS
        public static Image Scale(Image image, int width, int height)
        {
            if (image == null)
            {
                return null;
            }
            var scaledImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(scaledImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return scaledImage;
        }

        public static Image Scale(Image image, double scale)
        {
            if (image == null)
            {
                return null;
            }
            int width = (int)(image.Width * scale);
            int height = (int)(image.Height * scale);
            return Scale(image, width, height);
        }

        public static void Save(Image image, string path)
        {
            if (image == null || string.IsNullOrEmpty(path))
            {
                return;
            }
            image.Save(path);
        }

        public static void Save(Image image, string path, ImageFormat format)
        {
            if (image == null || string.IsNullOrEmpty(path))
            {
                return;
            }
            image.Save(path, format);
        }

        public static byte[] ToBytes(Image image, ImageFormat format)
        {
            if (image == null)
            {
                return null;
            }
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }

        public static Image FromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var stream = new MemoryStream(bytes))
            {
                return Image.FromStream(stream);
            }
        }

        public static Image Gray(Image image)
        {
            if (image == null)
            {
                return null;
            }
            var grayImage = new Bitmap(image.Width, image.Height);
            using (var g = Graphics.FromImage(grayImage))
            {
                var colorMatrix = new ColorMatrix(new float[][] {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                var imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return grayImage;
        }

        public static Image Rotate(Image image, float angle)
        {
            if (image == null)
            {
                return null;
            }
            var rotatedImage = new Bitmap(image.Width, image.Height);
            using (var g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                g.DrawImage(image, 0, 0);
            }
            return rotatedImage;
        }

        public static Image Flip(Image image, FlipType flipType)
        {
            if (image == null)
            {
                return null;
            }
            var flippedImage = new Bitmap(image);
            flippedImage.RotateFlip((RotateFlipType)flipType);
            return flippedImage;
        }

        public enum FlipType
        {
            None = 0,
            Horizontal = RotateFlipType.RotateNoneFlipX,
            Vertical = RotateFlipType.RotateNoneFlipY,
            Both = RotateFlipType.RotateNoneFlipXY
        }
#endif
    }
}
