using System;

namespace WellTool.Core.Img
{
    /// <summary>
    /// 表示以 LAB 形式存储的颜色。
    /// L: 亮度
    /// A: 正数代表红色，负端代表绿色
    /// B: 正数代表黄色，负端代表蓝色
    /// </summary>
    public class LabColor
    {
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
            if (rgb.HasValue)
            {
                // 从 RGB 转换到 LAB
                int r = (rgb.Value >> 16) & 0xFF;
                int g = (rgb.Value >> 8) & 0xFF;
                int b = rgb.Value & 0xFF;
                
                // 1. RGB 到 XYZ 的转换
                double rLinear = r / 255.0;
                double gLinear = g / 255.0;
                double bLinear = b / 255.0;

                // 应用 gamma 校正
                rLinear = rLinear > 0.04045 ? System.Math.Pow((rLinear + 0.055) / 1.055, 2.4) : rLinear / 12.92;
                gLinear = gLinear > 0.04045 ? System.Math.Pow((gLinear + 0.055) / 1.055, 2.4) : gLinear / 12.92;
                bLinear = bLinear > 0.04045 ? System.Math.Pow((bLinear + 0.055) / 1.055, 2.4) : bLinear / 12.92;

                // 转换到 XYZ 颜色空间
                double x = rLinear * 0.4124 + gLinear * 0.3576 + bLinear * 0.1805;
                double y = rLinear * 0.2126 + gLinear * 0.7152 + bLinear * 0.0722;
                double z = rLinear * 0.0193 + gLinear * 0.1192 + bLinear * 0.9505;

                // 2. XYZ 到 LAB 的转换
                // 参考白点 (D65)
                double xRef = 0.95047;
                double yRef = 1.00000;
                double zRef = 1.08883;

                // 归一化到参考白点
                x /= xRef;
                y /= yRef;
                z /= zRef;

                // 应用 F 函数
                double fx = F(x);
                double fy = F(y);
                double fz = F(z);

                // 计算 LAB 值
                _l = 116 * fy - 16;
                _a = 500 * (fx - fy);
                _b = 200 * (fy - fz);
            }
        }

        public LabColor(object color)
        {
            // 处理其他颜色类型
            _l = 0;
            _a = 0;
            _b = 0;
        }

        /// <summary>
        /// 获取颜色差
        /// </summary>
        /// <param name="other">其他Lab颜色</param>
        /// <returns>颜色差</returns>
        // See https://en.wikipedia.org/wiki/Color_difference#CIE94
        public double GetDistance(LabColor other)
        {
            double c1 = System.Math.Sqrt(_a * _a + _b * _b);
            double deltaC = c1 - System.Math.Sqrt(other._a * other._a + other._b * other._b);
            double deltaA = _a - other._a;
            double deltaB = _b - other._b;
            double deltaH = System.Math.Sqrt(System.Math.Max(0.0, deltaA * deltaA + deltaB * deltaB - deltaC * deltaC));
            return System.Math.Sqrt(System.Math.Max(0.0, System.Math.Pow((_l - other._l), 2)
                    + System.Math.Pow(deltaC / (1 + 0.045 * c1), 2) + System.Math.Pow(deltaH / (1 + 0.015 * c1), 2.0)));
        }

        private static double F(double t)
        {
            return (t > (216.0 / 24389.0)) ? System.Math.Cbrt(t) : (1.0 / 3.0) * System.Math.Pow(29.0 / 6.0, 2) * t + (4.0 / 29.0);
        }
    }
}