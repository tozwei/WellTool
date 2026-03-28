using System;

namespace WellTool.Core.Img
{
    public static class ColorUtil
    {
        public static string ToHex(int r, int g, int b)
        {
            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public static string ToHexArgb(int a, int r, int g, int b)
        {
            return $"#{a:X2}{r:X2}{g:X2}{b:X2}";
        }

        public static (int r, int g, int b) FromHex(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return (0, 0, 0);
            }
            hex = hex.TrimStart('#');
            if (hex.Length == 6)
            {
                return (
                    int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber)
                );
            }
            return (0, 0, 0);
        }

        public static (int a, int r, int g, int b) FromHexArgb(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return (255, 0, 0, 0);
            }
            hex = hex.TrimStart('#');
            if (hex.Length == 8)
            {
                return (
                    int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber)
                );
            }
            return (255, 0, 0, 0);
        }
    }
}
