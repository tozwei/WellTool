using System;
using System.Linq;

#if WINDOWS
using System.Drawing;
using System.Drawing.Text;
#endif

namespace WellTool.Core.Img
{
    public static class FontUtil
    {
#if WINDOWS
        public static Font CreateFont(string familyName, float size)
        {
            return new Font(familyName, size);
        }

        public static Font CreateFont(string familyName, float size, FontStyle style)
        {
            return new Font(familyName, size, style);
        }

        public static Font CreateFont(string familyName, float size, FontStyle style, GraphicsUnit unit)
        {
            return new Font(familyName, size, style, unit);
        }

        public static Font CreateFont(string familyName, float size, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            return new Font(familyName, size, style, unit, gdiCharSet);
        }

        public static Font CreateFont(string familyName, float size, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            return new Font(familyName, size, style, unit, gdiCharSet, gdiVerticalFont);
        }

        public static Font DefaultFont
        {
            get { return SystemFonts.DefaultFont; }
        }

        public static Font[] GetInstalledFonts()
        {
            using (var fontCollection = new InstalledFontCollection())
            {
                return fontCollection.Families
                    .Select(family => new Font(family, 12))
                    .ToArray();
            }
        }

        public static FontFamily[] GetInstalledFontFamilies()
        {
            using (var fontCollection = new InstalledFontCollection())
            {
                return fontCollection.Families;
            }
        }

        public static bool IsFontInstalled(string fontName)
        {
            using (var fontCollection = new InstalledFontCollection())
            {
                return fontCollection.Families.Any(family => family.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
            }
        }
#endif
    }
}
