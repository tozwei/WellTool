using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WellTool.Captcha.Generator;
using WellTool.Core.Img;

namespace WellTool.Captcha
{
    public class LineCaptcha : AbstractCaptcha
    {
        public LineCaptcha(int width, int height) : this(width, height, 5, 150)
        {
        }

        public LineCaptcha(int width, int height, int codeCount, int lineCount)
            : base(width, height, new RandomGenerator(codeCount), lineCount)
        {
        }

        public LineCaptcha(int width, int height, CodeGenerator generator, int interfereCount)
            : base(width, height, generator, interfereCount)
        {
        }

        public LineCaptcha(int width, int height, int codeCount, int interfereCount, float size)
            : base(width, height, new RandomGenerator(codeCount), interfereCount)
        {
        }

        protected override Image CreateImage(string code)
        {
            var imageType = Background == null ?
                System.Drawing.Imaging.ImageFormat.Png :
                System.Drawing.Imaging.ImageFormat.Png;

            using (var bitmap = new Bitmap(Width, Height))
            using (var g = Graphics.FromImage(bitmap))
            {
                if (Background != null)
                {
                    g.Clear(Background);
                }

                DrawInterfere(g);
                DrawString(g, code);

                return new Bitmap(bitmap);
            }
        }

        private void DrawString(Graphics g, string code)
        {
            if (TextAlpha > 0)
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
            }
            GraphicsUtil.DrawStringColourful(g, code, Font, Width, Height);
        }

        private void DrawInterfere(Graphics g)
        {
            var random = new Random();

            for (int i = 0; i < InterfereCount; i++)
            {
                int x1 = random.Next(Width);
                int y1 = random.Next(Height);
                int x2 = x1 + random.Next(Width / 8);
                int y2 = y1 + random.Next(Height / 8);

                using (var pen = new Pen(ImgUtil.RandomColor(random), 1))
                {
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }
    }
}
