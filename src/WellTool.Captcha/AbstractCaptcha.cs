using System;
using System.Drawing;
using System.IO;
using WellTool.Core.Codec;
using WellTool.Core.Img;
using WellTool.Captcha.Generator;

namespace WellTool.Captcha
{
    public abstract class AbstractCaptcha : ICaptcha
    {
        protected int Width;
        protected int Height;
        protected int InterfereCount;
        protected Font Font;
        protected string Code;
        protected byte[] ImageBytes;
        protected CodeGenerator Generator;
        protected Color Background = Color.White;
        protected float TextAlpha;
        protected object Stroke;

        public AbstractCaptcha(int width, int height, int codeCount, int interfereCount)
        {
            Init(width, height, new RandomGenerator(codeCount), interfereCount);
        }

        public AbstractCaptcha(int width, int height, CodeGenerator generator, int interfereCount)
        {
            Init(width, height, generator, interfereCount);
        }

        public AbstractCaptcha(int width, int height, CodeGenerator generator, int interfereCount, float size)
        {
            Init(width, height, generator, interfereCount);
        }

        private void Init(int width, int height, CodeGenerator generator, int interfereCount)
        {
            Width = width;
            Height = height;
            Generator = generator;
            InterfereCount = interfereCount;
            Font = new Font(FontFamily.GenericSansSerif, (int)(Height * 0.75));
        }

        public virtual void CreateCode()
        {
            GenerateCode();
            using (var ms = new MemoryStream())
            {
                var image = CreateImage(Code);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ImageBytes = ms.ToArray();
            }
        }

        protected virtual void GenerateCode()
        {
            Code = Generator.Generate();
        }

        protected abstract Image CreateImage(string code);

        public virtual string GetCode()
        {
            if (string.IsNullOrEmpty(Code))
            {
                CreateCode();
            }
            return Code;
        }

        public virtual bool Verify(string userInputCode)
        {
            return Generator.Verify(GetCode(), userInputCode);
        }

        public virtual void Write(Stream outStream)
        {
            if (ImageBytes == null)
            {
                CreateCode();
            }
            outStream.Write(ImageBytes, 0, ImageBytes.Length);
        }

        public virtual void Write(string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                Write(fs);
            }
        }

        public byte[] GetImageBytes()
        {
            if (ImageBytes == null)
            {
                CreateCode();
            }
            return ImageBytes;
        }

        public Image GetImage()
        {
            if (ImageBytes == null)
            {
                CreateCode();
            }
            using (var ms = new MemoryStream(ImageBytes))
            {
                return Image.FromStream(ms);
            }
        }

        public string GetImageBase64()
        {
            return Base64.Encode(GetImageBytes());
        }

        public string GetImageBase64Data()
        {
            return $"data:image/png;base64,{GetImageBase64()}";
        }

        public void SetFont(Font font)
        {
            Font = font;
        }

        public CodeGenerator GetGenerator()
        {
            return Generator;
        }

        public void SetGenerator(CodeGenerator generator)
        {
            Generator = generator;
        }

        public void SetBackground(Color background)
        {
            Background = background;
        }

        public void SetTextAlpha(float alpha)
        {
            TextAlpha = alpha;
        }

        public void SetStroke(object stroke)
        {
            Stroke = stroke;
        }
    }
}
