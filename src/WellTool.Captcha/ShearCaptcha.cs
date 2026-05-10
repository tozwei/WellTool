using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WellTool.Captcha.Generator;
using WellTool.Core.Img;

namespace WellTool.Captcha
{
    public class ShearCaptcha : AbstractCaptcha
    {
        public ShearCaptcha(int width, int height) : this(width, height, 5, 4)
        {
        }

        public ShearCaptcha(int width, int height, int codeCount) : this(width, height, codeCount, 4)
        {
        }

        public ShearCaptcha(int width, int height, int codeCount, int thickness)
            : base(width, height, new RandomGenerator(codeCount), thickness)
        {
        }

        public ShearCaptcha(int width, int height, CodeGenerator generator, int interfereCount)
            : base(width, height, generator, interfereCount)
        {
        }

        public ShearCaptcha(int width, int height, int codeCount, int interfereCount, float size)
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

                DrawString(g, code);
                DrawInterfere(g);

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
            int x1 = 0;
            int y1 = random.Next(Height) + 1;
            int x2 = Width;
            int y2 = random.Next(Height) + 1;

            using (var pen = new Pen(ImgUtil.RandomColor(random), InterfereCount))
            {
                g.DrawLine(pen, x1, y1, x2, y2);
            }

            for (int i = 0; i < InterfereCount; i++)
            {
                int x = random.Next(Width);
                int y = random.Next(Height);
                using (var pen = new Pen(ImgUtil.RandomColor(random), 1))
                {
                    g.DrawLine(pen, x, y, x + random.Next(20) - 10, y + random.Next(20) - 10);
                }
            }
        }
    }
}
