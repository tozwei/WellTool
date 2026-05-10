using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using WellTool.Captcha.Generator;
using WellTool.Core.Img.Gif;

namespace WellTool.Captcha
{
    public class GifCaptcha : AbstractCaptcha
    {
        private int Quality = 10;
        private int Repeat = 0;
        private int MinColor = 0;
        private int MaxColor = 255;

        public GifCaptcha(int width, int height) : this(width, height, 5, 10)
        {
        }

        public GifCaptcha(int width, int height, int codeCount) : this(width, height, codeCount, 10)
        {
        }

        public GifCaptcha(int width, int height, int codeCount, int interfereCount)
            : base(width, height, new RandomGenerator(codeCount), interfereCount)
        {
        }

        public GifCaptcha(int width, int height, CodeGenerator generator, int interfereCount)
            : base(width, height, generator, interfereCount)
        {
        }

        public GifCaptcha(int width, int height, int codeCount, int interfereCount, float size)
            : base(width, height, new RandomGenerator(codeCount), interfereCount)
        {
        }

        public GifCaptcha SetQuality(int quality)
        {
            if (quality < 1) quality = 1;
            Quality = quality;
            return this;
        }

        public GifCaptcha SetRepeat(int repeat)
        {
            Repeat = Math.Max(repeat, 0);
            return this;
        }

        public GifCaptcha SetMaxColor(int maxColor)
        {
            MaxColor = maxColor;
            return this;
        }

        public GifCaptcha SetMinColor(int minColor)
        {
            MinColor = minColor;
            return this;
        }

        public override void CreateCode()
        {
            GenerateCode();

            using (var ms = new MemoryStream())
            {
                var encoder = new AnimatedGifEncoder();
                encoder.Start(ms);
                encoder.SetQuality(Quality);
                encoder.SetDelay(100);
                encoder.SetRepeat(Repeat);

                var chars = Code.ToCharArray();
                var fontColors = new Color[chars.Length];

                for (int i = 0; i < chars.Length; i++)
                {
                    fontColors[i] = GetRandomColor(MinColor, MaxColor);
                    var frame = GraphicsImage(chars, fontColors, chars, i);
                    encoder.AddFrame(frame);
                    frame.Dispose();
                }

                encoder.Finish();
                ImageBytes = ms.ToArray();
            }
        }

        protected override Image CreateImage(string code)
        {
            return null;
        }

        private Image GraphicsImage(char[] chars, Color[] fontColor, char[] words, int flag)
        {
            using (var bitmap = new Bitmap(Width, Height))
            using (var g = Graphics.FromImage(bitmap))
            {
                if (Background != null)
                {
                    g.Clear(Background);
                }

                var y = (Height / 2) + (Font.Size / 2);
                var m = 1.0f * (Width - (chars.Length * Font.Size)) / chars.Length;
                var x = Math.Max(m / 2.0f, 2);

                g.SmoothingMode = SmoothingMode.HighQuality;

                for (int i = 0; i < chars.Length; i++)
                {
                    using (var brush = new SolidBrush(fontColor[i]))
                    {
                        var rx = new Random().Next(Width);
                        var ry = new Random().Next(Height);
                        var rw = new Random().Next(5, 30);
                        var rh = 5 + new Random().Next(5, 30);
                        g.FillEllipse(brush, rx, ry, rw, rh);

                        g.DrawString(words[i].ToString(), Font, brush, x + (Font.Size + m) * i, y);
                    }
                }

                return new Bitmap(bitmap);
            }
        }

        private float GetAlpha(int v, int i, int j)
        {
            int num = i + j;
            float r = (float)1 / v;
            float s = (v + 1) * r;
            return num > v ? (num * r - s) : num * r;
        }

        private Color GetRandomColor(int min, int max)
        {
            if (min > 255) min = 255;
            if (max > 255) max = 255;
            if (min < 0) min = 0;
            if (max < 0) max = 0;
            if (min > max)
            {
                min = 0;
                max = 255;
            }

            var random = new Random();
            return Color.FromArgb(
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max));
        }
    }
}
