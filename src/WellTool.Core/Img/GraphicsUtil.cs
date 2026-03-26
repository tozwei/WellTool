using System;

#if WINDOWS
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
#endif

namespace WellTool.Core.Img
{
    public static class GraphicsUtil
    {
#if WINDOWS
        public static Graphics CreateGraphics(Image image)
        {
            return image != null ? Graphics.FromImage(image) : null;
        }

        public static void DrawString(Graphics g, string text, Font font, Brush brush, int x, int y)
        {
            g?.DrawString(text, font, brush, x, y);
        }

        public static void DrawString(Graphics g, string text, Font font, Brush brush, Rectangle rect)
        {
            g?.DrawString(text, font, brush, rect);
        }

        public static void DrawLine(Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            g?.DrawLine(pen, x1, y1, x2, y2);
        }

        public static void DrawRectangle(Graphics g, Pen pen, int x, int y, int width, int height)
        {
            g?.DrawRectangle(pen, x, y, width, height);
        }

        public static void FillRectangle(Graphics g, Brush brush, int x, int y, int width, int height)
        {
            g?.FillRectangle(brush, x, y, width, height);
        }

        public static void DrawEllipse(Graphics g, Pen pen, int x, int y, int width, int height)
        {
            g?.DrawEllipse(pen, x, y, width, height);
        }

        public static void FillEllipse(Graphics g, Brush brush, int x, int y, int width, int height)
        {
            g?.FillEllipse(brush, x, y, width, height);
        }

        public static void DrawImage(Graphics g, Image image, int x, int y)
        {
            g?.DrawImage(image, x, y);
        }

        public static void DrawImage(Graphics g, Image image, int x, int y, int width, int height)
        {
            g?.DrawImage(image, x, y, width, height);
        }

        public static void SetSmoothingMode(Graphics g, SmoothingMode mode)
        {
            g?.SmoothingMode = mode;
        }

        public static void SetTextRenderingHint(Graphics g, TextRenderingHint hint)
        {
            g?.TextRenderingHint = hint;
        }

        public static void SetCompositingQuality(Graphics g, CompositingQuality quality)
        {
            g?.CompositingQuality = quality;
        }

        public static void SetInterpolationMode(Graphics g, InterpolationMode mode)
        {
            g?.InterpolationMode = mode;
        }
#endif
    }
}
