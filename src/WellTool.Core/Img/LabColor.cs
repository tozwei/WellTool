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
                // 简化实现，实际应用中可能需要更复杂的转换
                int r = (rgb.Value >> 16) & 0xFF;
                int g = (rgb.Value >> 8) & 0xFF;
                int b = rgb.Value & 0xFF;
                
                // 简化的 LAB 转换
                _l = 0.299 * r + 0.587 * g + 0.114 * b;
                _a = (r - g) * 0.707;
                _b = (r + g - 2 * b) * 0.408;
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