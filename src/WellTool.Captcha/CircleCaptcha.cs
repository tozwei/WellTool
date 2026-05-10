using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WellTool.Captcha.Generator;
using WellTool.Core.Img;

namespace WellTool.Captcha
{
    public class CircleCaptcha : AbstractCaptcha
    {
        public CircleCaptcha(int width, int height) : this(width, height, 5, 15)
        {
        }

        public CircleCaptcha(int width, int height, int codeCount) : this(width, height, codeCount, 15)
        {
        }

        public CircleCaptcha(int width, int height, int codeCount, int interfereCount)
            : base(width, height, new RandomGenerator(codeCount), interfereCount)
        {
        }

        public CircleCaptcha(int width, int height, CodeGenerator generator, int interfereCount)
            : base(width, height, generator, interfereCount)
        {
        }

        public CircleCaptcha(int width, int height, int codeCount, int interfereCount, float size)
            : base(width, height, new RandomGenerator(codeCount), interfereCount)
        {
        }

        protected override Image CreateImage(string code)
        {
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
                int x = random.Next(Width);
                int y = random.Next(Height);
                int w = random.Next(Height >> 1);
                int h = random.Next(Height >> 1);

                using (var pen = new Pen(ImgUtil.RandomColor(random), 1))
                {
                    g.DrawEllipse(pen, x, y, w, h);
                }
            }
        }
    }
}
