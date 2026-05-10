using WellTool.Captcha.Generator;

namespace WellTool.Captcha
{
    public static class CaptchaUtil
    {
        public static LineCaptcha CreateLineCaptcha(int width, int height)
        {
            return new LineCaptcha(width, height);
        }

        public static LineCaptcha CreateLineCaptcha(int width, int height, int codeCount, int lineCount)
        {
            return new LineCaptcha(width, height, codeCount, lineCount);
        }

        public static LineCaptcha CreateLineCaptcha(int width, int height, CodeGenerator generator, int lineCount)
        {
            return new LineCaptcha(width, height, generator, lineCount);
        }

        public static LineCaptcha CreateLineCaptcha(int width, int height, int codeCount, int lineCount, float size)
        {
            return new LineCaptcha(width, height, codeCount, lineCount, size);
        }

        public static CircleCaptcha CreateCircleCaptcha(int width, int height)
        {
            return new CircleCaptcha(width, height);
        }

        public static CircleCaptcha CreateCircleCaptcha(int width, int height, int codeCount, int circleCount)
        {
            return new CircleCaptcha(width, height, codeCount, circleCount);
        }

        public static CircleCaptcha CreateCircleCaptcha(int width, int height, CodeGenerator generator, int circleCount)
        {
            return new CircleCaptcha(width, height, generator, circleCount);
        }

        public static CircleCaptcha CreateCircleCaptcha(int width, int height, int codeCount, int circleCount, float size)
        {
            return new CircleCaptcha(width, height, codeCount, circleCount, size);
        }

        public static ShearCaptcha CreateShearCaptcha(int width, int height)
        {
            return new ShearCaptcha(width, height);
        }

        public static ShearCaptcha CreateShearCaptcha(int width, int height, int codeCount, int thickness)
        {
            return new ShearCaptcha(width, height, codeCount, thickness);
        }

        public static ShearCaptcha CreateShearCaptcha(int width, int height, CodeGenerator generator, int thickness)
        {
            return new ShearCaptcha(width, height, generator, thickness);
        }

        public static ShearCaptcha CreateShearCaptcha(int width, int height, int codeCount, int thickness, float size)
        {
            return new ShearCaptcha(width, height, codeCount, thickness, size);
        }

        public static GifCaptcha CreateGifCaptcha(int width, int height)
        {
            return new GifCaptcha(width, height);
        }

        public static GifCaptcha CreateGifCaptcha(int width, int height, int codeCount)
        {
            return new GifCaptcha(width, height, codeCount);
        }

        public static GifCaptcha CreateGifCaptcha(int width, int height, CodeGenerator generator, int thickness)
        {
            return new GifCaptcha(width, height, generator, thickness);
        }

        public static GifCaptcha CreateGifCaptcha(int width, int height, int codeCount, int thickness, float size)
        {
            return new GifCaptcha(width, height, codeCount, thickness, size);
        }
    }
}
