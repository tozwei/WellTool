using System;

#if WINDOWS
using System.Drawing;
#endif

namespace WellTool.Core.Img
{
    public static class ColorUtil
    {
#if WINDOWS
        public static Color FromRgb(int r, int g, int b)
        {
            return Color.FromArgb(255, r, g, b);
        }

        public static Color FromRgba(int r, int g, int b, int a)
        {
            return Color.FromArgb(a, r, g, b);
        }

        public static Color FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return Color.Empty;
            }
            hex = hex.TrimStart('#');
            if (hex.Length == 6)
            {
                return Color.FromArgb(
                    int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)
                );
            }
            else if (hex.Length == 8)
            {
                return Color.FromArgb(
                    int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)
                );
            }
            return Color.Empty;
        }

        public static string ToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static string ToHexArgb(Color color)
        {
            return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static Color Lighter(Color color, double factor)
        {
            int r = (int)(color.R + (255 - color.R) * factor);
            int g = (int)(color.G + (255 - color.G) * factor);
            int b = (int)(color.B + (255 - color.B) * factor);
            return Color.FromArgb(color.A, Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        public static Color Darker(Color color, double factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));
            return Color.FromArgb(color.A, Math.Max(0, r), Math.Max(0, g), Math.Max(0, b));
        }

        public static Color Complementary(Color color)
        {
            return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }

        public static double GetBrightness(Color color)
        {
            return color.GetBrightness();
        }

        public static double GetSaturation(Color color)
        {
            return color.GetSaturation();
        }

        public static double GetHue(Color color)
        {
            return color.GetHue();
        }
#endif
    }
}
