using NPOI.SS.Formula;using NPOI.SS.UserModel;using NPOI.SS.Util;using System;using System.Collections.Generic;using System.Text;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 当单元格不存在时使用此对象表示，得到的值都为null,此对象只用于标注单元格所在位置信息。
    /// </summary>
    public class NullCell : ICell
    {
        private readonly IRow _row;
        private readonly int _columnIndex;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="columnIndex">列号，从0开始</param>
        public NullCell(IRow row, int columnIndex)
        {
            _row = row;
            _columnIndex = columnIndex;
        }

        /// <summary>
        /// 获取列索引
        /// </summary>
        public int ColumnIndex => _columnIndex;

        /// <summary>
        /// 获取行索引
        /// </summary>
        public int RowIndex => _row.RowNum;

        /// <summary>
        /// 获取工作表
        /// </summary>
        public ISheet Sheet => _row.Sheet;

        /// <summary>
        /// 获取行
        /// </summary>
        public IRow Row => _row;

        /// <summary>
        /// 设置单元格类型
        /// </summary>
        /// <param name="cellType">单元格类型</param>
        public void SetCellType(CellType cellType)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置为空白单元格
        /// </summary>
        public void SetBlank()
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        public CellType CellType => CellType.Blank;

        /// <summary>
        /// 获取缓存的公式结果类型
        /// </summary>
        public CellType CachedFormulaResultType => CellType.Blank;

        /// <summary>
        /// 设置单元格值为数值
        /// </summary>
        /// <param name="value">数值</param>
        public void SetCellValue(double value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格值为日期
        /// </summary>
        /// <param name="value">日期</param>
        public void SetCellValue(DateTime value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格值为富文本
        /// </summary>
        /// <param name="value">富文本</param>
        public void SetCellValue(IRichTextString value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格值为字符串
        /// </summary>
        /// <param name="value">字符串</param>
        public void SetCellValue(string value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格公式
        /// </summary>
        /// <param name="formula">公式</param>
        public void SetCellFormula(string formula)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 移除公式
        /// </summary>
        public void RemoveFormula()
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取数值单元格值
        /// </summary>
        public double NumericCellValue => throw new NotSupportedException("Cell value is null!");

        /// <summary>
        /// 获取日期单元格值
        /// </summary>
        public DateTime DateCellValue => throw new NotSupportedException("Cell value is null!");

        /// <summary>
        /// 获取富文本单元格值
        /// </summary>
        public IRichTextString RichStringCellValue => null;

        /// <summary>
        /// 获取字符串单元格值
        /// </summary>
        public string StringCellValue => null;

        /// <summary>
        /// 设置单元格值为布尔值
        /// </summary>
        /// <param name="value">布尔值</param>
        public void SetCellValue(bool value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格错误值
        /// </summary>
        /// <param name="value">错误值</param>
        public void SetCellErrorValue(byte value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取布尔单元格值
        /// </summary>
        public bool BooleanCellValue => throw new NotSupportedException("Cell value is null!");

        /// <summary>
        /// 获取错误单元格值
        /// </summary>
        public byte ErrorCellValue => throw new NotSupportedException("Cell value is null!");

        /// <summary>
        /// 设置为活动单元格
        /// </summary>
        public void SetAsActiveCell()
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取单元格地址
        /// </summary>
        public CellAddress Address => null;

        /// <summary>
        /// 移除单元格批注
        /// </summary>
        public void RemoveCellComment()
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 移除超链接
        /// </summary>
        public void RemoveHyperlink()
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取数组公式范围
        /// </summary>
        public CellRangeAddress ArrayFormulaRange => null;

        /// <summary>
        /// 是否是数组公式组的一部分
        /// </summary>
        public bool IsPartOfArrayFormulaGroup => throw new NotSupportedException("Cell value is null!");

        /// <summary>
        /// 复制单元格到指定列
        /// </summary>
        /// <param name="columnIndex">目标列索引</param>
        /// <returns>复制后的单元格</returns>
        public ICell CopyCellTo(int columnIndex)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }

#if NET6_0_OR_GREATER
        /// <summary>
        /// 设置单元格值为日期
        /// </summary>
        /// <param name="value">日期</param>
        public void SetCellValue(DateOnly value)
        {
            throw new NotSupportedException("Can not set any thing to null cell!");
        }
#endif

        /// <summary>
        /// 设置单元格公式
        /// </summary>
        public string CellFormula
        {
            get => null;
            set => throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        public ICellStyle CellStyle
        {
            get => null;
            set => throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置单元格批注
        /// </summary>
        public IComment CellComment
        {
            get => null;
            set => throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 设置超链接
        /// </summary>
        public IHyperlink Hyperlink
        {
            get => null;
            set => throw new NotSupportedException("Can not set any thing to null cell!");
        }

        /// <summary>
        /// 获取缓存的公式结果类型枚举
        /// </summary>
        /// <returns>单元格类型</returns>
        public CellType GetCachedFormulaResultTypeEnum()
        {
            return CellType.Blank;
        }

        /// <summary>
        /// 是否是合并单元格
        /// </summary>
        public bool IsMergedCell => false;
    }
}