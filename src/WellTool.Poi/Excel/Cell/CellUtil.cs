using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using WellTool.Poi.Excel.Editors;
using WellTool.Poi.Excel.Cell.Values;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// Excel表格中单元格工具类
    /// </summary>
    public static class CellUtil
    {
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>值，类型可能为：Date、Double、Boolean、String</returns>
        public static object GetCellValue(ICell cell)
        {
            return GetCellValue(cell, false);
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="isTrimCellValue">如果单元格类型为字符串，是否去掉两边空白符</param>
        /// <returns>值，类型可能为：Date、Double、Boolean、String</returns>
        public static object GetCellValue(ICell cell, bool isTrimCellValue)
        {
            if (cell == null)
            {
                return null;
            }
            return GetCellValue(cell, cell.CellType, isTrimCellValue);
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellEditor">单元格值编辑器。可以通过此编辑器对单元格值做自定义操作</param>
        /// <returns>值，类型可能为：Date、Double、Boolean、String</returns>
        public static object GetCellValue(ICell cell, ICellEditor cellEditor)
        {
            return GetCellValue(cell, null, cellEditor);
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellType">单元格值类型</param>
        /// <param name="isTrimCellValue">如果单元格类型为字符串，是否去掉两边空白符</param>
        /// <returns>值，类型可能为：Date、Double、Boolean、String</returns>
        public static object GetCellValue(ICell cell, CellType? cellType, bool isTrimCellValue)
        {
            return GetCellValue(cell, cellType, isTrimCellValue ? new TrimEditor() : null);
        }

        /// <summary>
        /// 获取单元格值<br>
        /// 如果单元格值为数字格式，则判断其格式中是否有小数部分，无则返回Long类型，否则返回Double类型
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellType">单元格值类型，如果为null默认使用cell的类型</param>
        /// <param name="cellEditor">单元格值编辑器。可以通过此编辑器对单元格值做自定义操作</param>
        /// <returns>值，类型可能为：Date、Double、Boolean、String</returns>
        public static object GetCellValue(ICell cell, CellType? cellType, ICellEditor cellEditor)
        {
            if (cell == null)
            {
                return null;
            }
            if (cell is NullCell)
            {
                return cellEditor == null ? null : cellEditor.Edit(cell, null);
            }
            if (cellType == null)
            {
                cellType = cell.CellType;
            }

            // 尝试获取合并单元格，如果是合并单元格，则重新获取单元格类型
            var mergedCell = GetMergedRegionCell(cell);
            if (mergedCell != cell)
            {
                cell = mergedCell;
                cellType = cell.CellType;
            }

            object value;
            switch (cellType)
            {
                case CellType.Numeric:
                    value = new NumericCellValue(cell).GetValue();
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Formula:
                    value = GetCellValue(cell, cell.CachedFormulaResultType, cellEditor);
                    break;
                case CellType.Blank:
                    value = string.Empty;
                    break;
                case CellType.Error:
                    value = new ErrorCellValue(cell).GetValue();
                    break;
                default:
                    value = cell.StringCellValue;
                    break;
            }

            return cellEditor == null ? value : cellEditor.Edit(cell, value);
        }

        /// <summary>
        /// 设置单元格值<br>
        /// 根据传入的styleSet自动匹配样式<br>
        /// 当为头部样式时默认赋值头部样式，但是头部中如果有数字、日期等类型，将按照数字、日期样式设置
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">值</param>
        /// <param name="style">自定义样式，null表示无样式</param>
        public static void SetCellValue(ICell cell, object value, ICellStyle style)
        {
            SetCellValue(cell, new CellSetterImpl(value, style));
        }

        private class CellSetterImpl : ICellSetter
        {
            private readonly object _value;
            private readonly ICellStyle _style;

            public CellSetterImpl(object value, ICellStyle style)
            {
                _value = value;
                _style = style;
            }

            public void SetValue(ICell cell)
            {
                SetCellValue(cell, _value);
                if (_style != null)
                {
                    cell.CellStyle = _style;
                }
            }
        }

        /// <summary>
        /// 设置单元格值<br>
        /// 根据传入的styleSet自动匹配样式<br>
        /// 当为头部样式时默认赋值头部样式，但是头部中如果有数字、日期等类型，将按照数字、日期样式设置
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">值或<see cref="ICellSetter"/></param>
        public static void SetCellValue(ICell cell, object value)
        {
            if (cell == null)
            {
                return;
            }

            // 在使用BigWriter(SXSSF)模式写出数据时，单元格值为直接值，非引用值
            // 而再使用ExcelWriter(XSSF)编辑时，会写出引用值，导致失效。
            // 此处做法是先清空单元格值，再写入
            if (cell.CellType != CellType.Blank)
            {
                cell.SetCellType(CellType.Blank);
            }

            Setters.CellSetterFactory.CreateCellSetter(value).SetValue(cell);
        }

        /// <summary>
        /// 获取单元格，如果单元格不存在，返回<see cref="NullCell"/>
        /// </summary>
        /// <param name="row">Excel表的行</param>
        /// <param name="cellIndex">列号</param>
        /// <returns>单元格</returns>
        public static ICell GetCell(IRow row, int cellIndex)
        {
            if (row == null)
            {
                return null;
            }
            var cell = row.GetCell(cellIndex);
            if (cell == null)
            {
                return new NullCell(row, cellIndex);
            }
            return cell;
        }

        /// <summary>
        /// 获取已有单元格或创建新单元格
        /// </summary>
        /// <param name="row">Excel表的行</param>
        /// <param name="cellIndex">列号</param>
        /// <returns>单元格</returns>
        public static ICell GetOrCreateCell(IRow row, int cellIndex)
        {
            if (row == null)
            {
                return null;
            }
            var cell = row.GetCell(cellIndex);
            if (cell == null)
            {
                cell = row.CreateCell(cellIndex);
            }
            return cell;
        }

        /// <summary>
        /// 判断指定的单元格是否是合并单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
        /// <returns>是否是合并单元格</returns>
        public static bool IsMergedRegion(ISheet sheet, string locationRef)
        {
            var cellLocation = ExcelUtil.ToLocation(locationRef);
            return IsMergedRegion(sheet, cellLocation.X, cellLocation.Y);
        }

        /// <summary>
        /// 判断指定的单元格是否是合并单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>是否是合并单元格</returns>
        public static bool IsMergedRegion(ICell cell)
        {
            return IsMergedRegion(cell.Sheet, cell.ColumnIndex, cell.RowIndex);
        }

        /// <summary>
        /// 判断指定的单元格是否是合并单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="x">列号，从0开始</param>
        /// <param name="y">行号，从0开始</param>
        /// <returns>是否是合并单元格，如果提供的sheet为null，返回false</returns>
        public static bool IsMergedRegion(ISheet sheet, int x, int y)
        {
            if (sheet != null)
            {
                var sheetMergeCount = sheet.NumMergedRegions;
                for (int i = 0; i < sheetMergeCount; i++)
                {
                    var ca = sheet.GetMergedRegion(i);
                    if (y >= ca.FirstRow && y <= ca.LastRow
                            && x >= ca.FirstColumn && x <= ca.LastColumn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取合并单元格<see cref="CellRangeAddress"/>，如果不是返回null
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
        /// <returns>合并单元格地址</returns>
        public static CellRangeAddress GetCellRangeAddress(ISheet sheet, string locationRef)
        {
            var cellLocation = ExcelUtil.ToLocation(locationRef);
            return GetCellRangeAddress(sheet, cellLocation.X, cellLocation.Y);
        }

        /// <summary>
        /// 获取合并单元格<see cref="CellRangeAddress"/>，如果不是返回null
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>合并单元格地址</returns>
        public static CellRangeAddress GetCellRangeAddress(ICell cell)
        {
            return GetCellRangeAddress(cell.Sheet, cell.ColumnIndex, cell.RowIndex);
        }

        /// <summary>
        /// 获取合并单元格<see cref="CellRangeAddress"/>，如果不是返回null
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="x">列号，从0开始</param>
        /// <param name="y">行号，从0开始</param>
        /// <returns>合并单元格地址</returns>
        public static CellRangeAddress GetCellRangeAddress(ISheet sheet, int x, int y)
        {
            if (sheet != null)
            {
                var sheetMergeCount = sheet.NumMergedRegions;
                for (int i = 0; i < sheetMergeCount; i++)
                {
                    var ca = sheet.GetMergedRegion(i);
                    if (y >= ca.FirstRow && y <= ca.LastRow
                            && x >= ca.FirstColumn && x <= ca.LastColumn)
                    {
                        return ca;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 设置合并单元格样式，如果不是则不设置
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellStyle">单元格样式</param>
        public static void SetMergedRegionStyle(ICell cell, ICellStyle cellStyle)
        {
            var cellRangeAddress = GetCellRangeAddress(cell);
            if (cellRangeAddress != null)
            {
                SetMergeCellStyle(cellStyle, cellRangeAddress, cell.Sheet);
            }
        }

        /// <summary>
        /// 合并单元格，可以根据设置的值来合并行和列
        /// </summary>
        /// <param name="sheet">表对象</param>
        /// <param name="firstRow">起始行，0开始</param>
        /// <param name="lastRow">结束行，0开始</param>
        /// <param name="firstColumn">起始列，0开始</param>
        /// <param name="lastColumn">结束列，0开始</param>
        /// <returns>合并后的单元格号</returns>
        public static int MergingCells(ISheet sheet, int firstRow, int lastRow, int firstColumn, int lastColumn)
        {
            return MergingCells(sheet, firstRow, lastRow, firstColumn, lastColumn, null);
        }

        /// <summary>
        /// 合并单元格，可以根据设置的值来合并行和列
        /// </summary>
        /// <param name="sheet">表对象</param>
        /// <param name="firstRow">起始行，0开始</param>
        /// <param name="lastRow">结束行，0开始</param>
        /// <param name="firstColumn">起始列，0开始</param>
        /// <param name="lastColumn">结束列，0开始</param>
        /// <param name="cellStyle">单元格样式，只提取边框样式，null表示无样式</param>
        /// <returns>合并后的单元格号</returns>
        public static int MergingCells(ISheet sheet, int firstRow, int lastRow, int firstColumn, int lastColumn, ICellStyle cellStyle)
        {
            var cellRangeAddress = new CellRangeAddress(
                    firstRow, // first row (0-based)
                    lastRow, // last row (0-based)
                    firstColumn, // first column (0-based)
                    lastColumn // last column (0-based)
            );

            SetMergeCellStyle(cellStyle, cellRangeAddress, sheet);
            return sheet.AddMergedRegion(cellRangeAddress);
        }

        /// <summary>
        /// 获取合并单元格的值<br>
        /// 传入的x,y坐标（列行数）可以是合并单元格范围内的任意一个单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="locationRef">单元格地址标识符，例如A11，B5</param>
        /// <returns>合并单元格的值</returns>
        public static object GetMergedRegionValue(ISheet sheet, string locationRef)
        {
            var cellLocation = ExcelUtil.ToLocation(locationRef);
            return GetMergedRegionValue(sheet, cellLocation.X, cellLocation.Y);
        }

        /// <summary>
        /// 获取合并单元格的值<br>
        /// 传入的x,y坐标（列行数）可以是合并单元格范围内的任意一个单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="x">列号，从0开始，可以是合并单元格范围中的任意一列</param>
        /// <param name="y">行号，从0开始，可以是合并单元格范围中的任意一行</param>
        /// <returns>合并单元格的值</returns>
        public static object GetMergedRegionValue(ISheet sheet, int x, int y)
        {
            // 合并单元格的识别在GetCellValue已经集成，无需重复获取合并单元格
            var row = sheet.GetRow(y);
            return GetCellValue(GetCell(row, x));
        }

        /// <summary>
        /// 获取合并单元格<br>
        /// 传入的x,y坐标（列行数）可以是合并单元格范围内的任意一个单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>合并单元格</returns>
        public static ICell GetMergedRegionCell(ICell cell)
        {
            if (cell == null)
            {
                return null;
            }
            return GetCellIfMergedRegion(cell.Sheet, cell.ColumnIndex, cell.RowIndex) ?? cell;
        }

        /// <summary>
        /// 获取合并单元格<br>
        /// 传入的x,y坐标（列行数）可以是合并单元格范围内的任意一个单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="x">列号，从0开始，可以是合并单元格范围中的任意一列</param>
        /// <param name="y">行号，从0开始，可以是合并单元格范围中的任意一行</param>
        /// <returns>合并单元格，如果非合并单元格，返回坐标对应的单元格</returns>
        public static ICell GetMergedRegionCell(ISheet sheet, int x, int y)
        {
            var row = sheet.GetRow(y);
            return GetCellIfMergedRegion(sheet, x, y) ?? GetCell(row, x);
        }

        /// <summary>
        /// 为特定单元格添加批注
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="commentText">批注内容</param>
        /// <param name="commentAuthor">作者</param>
        /// <param name="anchor">批注的位置、大小等信息，null表示使用默认</param>
        public static void SetComment(ICell cell, string commentText, string commentAuthor, IClientAnchor anchor)
        {
            var sheet = cell.Sheet;
            var wb = sheet.Workbook;
            var drawing = sheet.CreateDrawingPatriarch();
            var factory = wb.GetCreationHelper();
            if (anchor == null)
            {
                anchor = factory.CreateClientAnchor();
                // 默认位置，在注释的单元格的右方
                anchor.Col1 = cell.ColumnIndex + 1;
                anchor.Col2 = cell.ColumnIndex + 3;
                anchor.Row1 = cell.RowIndex;
                anchor.Row2 = cell.RowIndex + 2;
                // 自适应
                anchor.AnchorType = AnchorType.MoveAndResize;
            }
            var comment = drawing.CreateCellComment(anchor);
            // 修正在XSSFCell中未设置地址导致错位问题
            comment.Address = cell.Address;
            comment.String = factory.CreateRichTextString(commentText);
            comment.Author = commentAuthor ?? string.Empty;
            cell.CellComment = comment;
        }

        // -------------------------------------------------------------------------------------------------------------- Private method start

        /// <summary>
        /// 获取合并单元格，非合并单元格返回null<br>
        /// 传入的x,y坐标（列行数）可以是合并单元格范围内的任意一个单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="x">列号，从0开始，可以是合并单元格范围中的任意一列</param>
        /// <param name="y">行号，从0开始，可以是合并单元格范围中的任意一行</param>
        /// <returns>合并单元格，如果非合并单元格，返回null</returns>
        private static ICell GetCellIfMergedRegion(ISheet sheet, int x, int y)
        {
            for (int i = 0; i < sheet.NumMergedRegions; i++)
            {
                var ca = sheet.GetMergedRegion(i);
                if (ca.IsInRange(y, x))
                {
                    var row = sheet.GetRow(ca.FirstRow);
                    return GetCell(row, ca.FirstColumn);
                }
            }
            return null;
        }

        /// <summary>
        /// 根据<see cref="ICellStyle"/>设置合并单元格边框样式
        /// </summary>
        /// <param name="cellStyle">单元格样式</param>
        /// <param name="cellRangeAddress">合并单元格地址</param>
        /// <param name="sheet">工作表</param>
        private static void SetMergeCellStyle(ICellStyle cellStyle, CellRangeAddress cellRangeAddress, ISheet sheet)
        {
            if (cellStyle != null)
            {
                RegionUtil.SetBorderTop((int)cellStyle.BorderTop, cellRangeAddress, sheet);
                RegionUtil.SetBorderRight((int)cellStyle.BorderRight, cellRangeAddress, sheet);
                RegionUtil.SetBorderBottom((int)cellStyle.BorderBottom, cellRangeAddress, sheet);
                RegionUtil.SetBorderLeft((int)cellStyle.BorderLeft, cellRangeAddress, sheet);
                RegionUtil.SetTopBorderColor(cellStyle.TopBorderColor, cellRangeAddress, sheet);
                RegionUtil.SetRightBorderColor(cellStyle.RightBorderColor, cellRangeAddress, sheet);
                RegionUtil.SetLeftBorderColor(cellStyle.LeftBorderColor, cellRangeAddress, sheet);
                RegionUtil.SetBottomBorderColor(cellStyle.BottomBorderColor, cellRangeAddress, sheet);
            }
        }
        // -------------------------------------------------------------------------------------------------------------- Private method end
    }
}