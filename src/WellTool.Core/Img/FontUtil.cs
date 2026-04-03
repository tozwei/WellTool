using System;
using System.Linq;

namespace WellTool.Core.Img
{
    public static class FontUtil
    {
        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="familyName">字体家族名称</param>
        /// <param name="size">字体大小</param>
        /// <returns>字体对象</returns>
        public static object CreateFont(string familyName, float size)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="familyName">字体家族名称</param>
        /// <param name="size">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <returns>字体对象</returns>
        public static object CreateFont(string familyName, float size, int style)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="familyName">字体家族名称</param>
        /// <param name="size">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">图形单位</param>
        /// <returns>字体对象</returns>
        public static object CreateFont(string familyName, float size, int style, int unit)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="familyName">字体家族名称</param>
        /// <param name="size">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">图形单位</param>
        /// <param name="gdiCharSet">GDI字符集</param>
        /// <returns>字体对象</returns>
        public static object CreateFont(string familyName, float size, int style, int unit, byte gdiCharSet)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="familyName">字体家族名称</param>
        /// <param name="size">字体大小</param>
        /// <param name="style">字体样式</param>
        /// <param name="unit">图形单位</param>
        /// <param name="gdiCharSet">GDI字符集</param>
        /// <param name="gdiVerticalFont">是否为GDI垂直字体</param>
        /// <returns>字体对象</returns>
        public static object CreateFont(string familyName, float size, int style, int unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 默认字体
        /// </summary>
        public static object DefaultFont
        {
            get { return null; }
        }

        /// <summary>
        /// 获取已安装的字体
        /// </summary>
        /// <returns>字体数组</returns>
        public static object[] GetInstalledFonts()
        {
            // 跨平台实现
            return new object[0];
        }

        /// <summary>
        /// 获取已安装的字体家族
        /// </summary>
        /// <returns>字体家族数组</returns>
        public static object[] GetInstalledFontFamilies()
        {
            // 跨平台实现
            return new object[0];
        }

        /// <summary>
        /// 检查字体是否已安装
        /// </summary>
        /// <param name="fontName">字体名称</param>
        /// <returns>是否已安装</returns>
        public static bool IsFontInstalled(string fontName)
        {
            // 跨平台实现
            return false;
        }
    }
}
