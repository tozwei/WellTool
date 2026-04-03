using System;

#if WINDOWS
using System.Drawing;
using System.Drawing.Drawing2D;
#endif

namespace WellTool.Core.Img
{
    /// <summary>
    /// 表示以 LAB 形式存储的颜色。
    /// L: 亮度
    /// A: 正数代表红色，负端代表绿色
    /// B: 正数代表黄色，负端代表蓝色
    /// </summary>
#if WINDOWS
    public class LabColor
    {
        private static readonly ColorSpace XyzColorSpace = ColorSpace.GetInstance(ColorSpace.CS_CIEXYZ);

        /// <summary>
        /// L: 亮度
        /// </summary>
        private readonly double _l;
        /// <summary>
        /// A: 正数代表红色，负端代表绿色
        /// </summary>
        private readonly double _a;
        /// <summary>
        /// B: 正数代表黄色，负端代表蓝色
        /// </summary>
        private readonly double _b;

        public LabColor(int? rgb)
        {
            this(rgb.HasValue ? Color.FromArgb(rgb.Value) : null);
        }

        public LabColor(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException("Color must not be null");
            }

            float[] lab = FromXyz(color.GetColorComponents(XyzColorSpace, null));
            _l = lab[0];
            _a = lab[1];
            _b = lab[2];
        }

        /// <summary>
        /// 获取颜色差
        /// </summary>
        /// <param name="other">其他Lab颜色</param>
        /// <returns>颜色差</returns>
        // See https://en.wikipedia.org/wiki/Color_difference#CIE94
        public double GetDistance(LabColor other)
        {
            double c1 = Math.Sqrt(_a * _a + _b * _b);
            double deltaC = c1 - Math.Sqrt(other._a * other._a + other._b * other._b);
            double deltaA = _a - other._a;
            double deltaB = _b - other._b;
            double deltaH = Math.Sqrt(Math.Max(0.0, deltaA * deltaA + deltaB * deltaB - deltaC * deltaC));
            return Math.Sqrt(Math.Max(0.0, Math.Pow((_l - other._l), 2)
                    + Math.Pow(deltaC / (1 + 0.045 * c1), 2) + Math.Pow(deltaH / (1 + 0.015 * c1), 2.0)));
        }

        private float[] FromXyz(float[] xyz)
        {
            return FromXyz(xyz[0], xyz[1], xyz[2]);
        }

        /// <summary>
        /// 从xyz换算
        /// L=116f(y)-16
        /// a=500[f(x/0.982)-f(y)]
        /// b=200[f(y)-f(z/1.183 )]
        /// 其中： f(x)=7.787x+0.138, x&lt;0.008856; f(x)=(x)1/3,x&gt;0.008856
        /// </summary>
        private static float[] FromXyz(float x, float y, float z)
        {
            double l = (F(y) - 16.0) * 116.0;
            double a = (F(x) - F(y)) * 500.0;
            double b = (F(y) - F(z)) * 200.0;
            return new float[] { (float)l, (float)a, (float)b };
        }

        private static double F(double t)
        {
            return (t > (216.0 / 24389.0)) ? Math.Cbrt(t) : (1.0 / 3.0) * Math.Pow(29.0 / 6.0, 2) * t + (4.0 / 29.0);
        }
    }
#else
    public class LabColor
    {
        public LabColor(int? rgb) { }
        public LabColor(object color) { }
        public double GetDistance(LabColor other) => 0;
    }
#endif
}