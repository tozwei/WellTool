using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace WellTool.Poi.Excel.Style
{
    /// <summary>
    /// Excel样式工具类
    /// </summary>
    public static class StyleUtil
    {
        /// <summary>
        /// 克隆新的<see cref="ICellStyle"/>
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellStyle">被复制的样式</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle CloneCellStyle(ICell cell, ICellStyle cellStyle)
        {
            return CloneCellStyle(cell.Sheet.Workbook, cellStyle);
        }

        /// <summary>
        /// 克隆新的<see cref="ICellStyle"/>
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="cellStyle">被复制的样式</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle CloneCellStyle(IWorkbook workbook, ICellStyle cellStyle)
        {
            var newCellStyle = CreateCellStyle(workbook);
            newCellStyle.CloneStyleFrom(cellStyle);
            return newCellStyle;
        }

        /// <summary>
        /// 设置cell文本对齐样式
        /// </summary>
        /// <param name="cellStyle"><see cref="ICellStyle"/></param>
        /// <param name="halign">横向位置</param>
        /// <param name="valign">纵向位置</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle SetAlign(ICellStyle cellStyle, HorizontalAlignment halign, VerticalAlignment valign)
        {
            cellStyle.Alignment = halign;
            cellStyle.VerticalAlignment = valign;
            return cellStyle;
        }

        /// <summary>
        /// 设置cell的四个边框粗细和颜色
        /// </summary>
        /// <param name="cellStyle"><see cref="ICellStyle"/></param>
        /// <param name="borderSize">边框粗细</param>
        /// <param name="colorIndex">颜色</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle SetBorder(ICellStyle cellStyle, BorderStyle borderSize, IndexedColors colorIndex)
        {
            cellStyle.BorderBottom = borderSize;
            cellStyle.BottomBorderColor = colorIndex.Index;

            cellStyle.BorderLeft = borderSize;
            cellStyle.LeftBorderColor = colorIndex.Index;

            cellStyle.BorderRight = borderSize;
            cellStyle.RightBorderColor = colorIndex.Index;

            cellStyle.BorderTop = borderSize;
            cellStyle.TopBorderColor = colorIndex.Index;

            return cellStyle;
        }

        /// <summary>
        /// 给cell设置颜色
        /// </summary>
        /// <param name="cellStyle"><see cref="ICellStyle"/></param>
        /// <param name="color">背景颜色</param>
        /// <param name="fillPattern">填充方式</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle SetColor(ICellStyle cellStyle, IndexedColors color, NPOI.SS.UserModel.FillPattern fillPattern)
        {
            return SetColor(cellStyle, color.Index, fillPattern);
        }

        /// <summary>
        /// 给cell设置颜色
        /// </summary>
        /// <param name="cellStyle"><see cref="ICellStyle"/></param>
        /// <param name="color">背景颜色</param>
        /// <param name="fillPattern">填充方式</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle SetColor(ICellStyle cellStyle, short color, NPOI.SS.UserModel.FillPattern fillPattern)
        {
            cellStyle.FillForegroundColor = color;
            cellStyle.FillPattern = fillPattern;
            return cellStyle;
        }

        /// <summary>
        /// 创建字体
        /// </summary>
        /// <param name="workbook"><see cref="IWorkbook"/></param>
        /// <param name="color">字体颜色</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称，可以为null使用默认字体</param>
        /// <returns><see cref="IFont"/></returns>
        public static IFont CreateFont(IWorkbook workbook, short color, short fontSize, string fontName)
        {
            var font = workbook.CreateFont();
            return SetFontStyle(font, color, fontSize, fontName);
        }

        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="font">字体<see cref="IFont"/></param>
        /// <param name="color">字体颜色</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称，可以为null使用默认字体</param>
        /// <returns><see cref="IFont"/></returns>
        public static IFont SetFontStyle(IFont font, short color, short fontSize, string fontName)
        {
            if (color > 0)
            {
                font.Color = color;
            }
            if (fontSize > 0)
            {
                font.FontHeightInPoints = fontSize;
            }
            if (!string.IsNullOrEmpty(fontName))
            {
                font.FontName = fontName;
            }
            return font;
        }

        /// <summary>
        /// 创建单元格样式
        /// </summary>
        /// <param name="workbook"><see cref="IWorkbook"/> 工作簿</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle CreateCellStyle(IWorkbook workbook)
        {
            if (workbook == null)
            {
                return null;
            }
            return workbook.CreateCellStyle();
        }

        /// <summary>
        /// 创建默认普通单元格样式
        /// </summary>
        /// <pre>
        /// 1. 文字上下左右居中
        /// 2. 细边框，黑色
        /// </pre>
        /// <param name="workbook"><see cref="IWorkbook"/> 工作簿</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle CreateDefaultCellStyle(IWorkbook workbook)
        {
            var cellStyle = CreateCellStyle(workbook);
            SetAlign(cellStyle, HorizontalAlignment.Center, VerticalAlignment.Center);
            SetBorder(cellStyle, BorderStyle.Thin, IndexedColors.Black);
            return cellStyle;
        }

        /// <summary>
        /// 创建默认头部样式
        /// </summary>
        /// <param name="workbook"><see cref="IWorkbook"/> 工作簿</param>
        /// <returns><see cref="ICellStyle"/></returns>
        public static ICellStyle CreateHeadCellStyle(IWorkbook workbook)
        {
            var cellStyle = CreateCellStyle(workbook);
            SetAlign(cellStyle, HorizontalAlignment.Center, VerticalAlignment.Center);
            SetBorder(cellStyle, BorderStyle.Thin, IndexedColors.Black);
            SetColor(cellStyle, IndexedColors.Grey25Percent, NPOI.SS.UserModel.FillPattern.SolidForeground);
            return cellStyle;
        }

        /// <summary>
        /// 给定样式是否为null（无样式）或默认样式，默认样式为{@code workbook.GetCellStyleAt(0)}
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="style">被检查的样式</param>
        /// <returns>是否为null（无样式）或默认样式</returns>
        public static bool IsNullOrDefaultStyle(IWorkbook workbook, ICellStyle style)
        {
            return (style == null) || style.Equals(workbook.GetCellStyleAt(0));
        }

        /// <summary>
        /// 创建数据格式并获取格式
        /// </summary>
        /// <param name="workbook"><see cref="IWorkbook"/></param>
        /// <param name="format">数据格式</param>
        /// <returns>数据格式</returns>
        public static short GetFormat(IWorkbook workbook, string format)
        {
            var dataFormat = workbook.CreateDataFormat();
            return dataFormat.GetFormat(format);
        }
    }
}