using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace WellTool.Poi.Excel.Sax.Handler
{
    /// <summary>
    /// Sax方式读取Excel行处理器
    /// </summary>
    public interface IRowHandler
    {
        /// <summary>
        /// 处理一行数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号，从0开始计数</param>
        /// <param name="rowCells">行数据，每个object表示一个单元格的值</param>
        void Handle(int sheetIndex, long rowIndex, List<object> rowCells);

        /// <summary>
        /// 处理一个单元格的数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号</param>
        /// <param name="cellIndex">当前列号</param>
        /// <param name="value">单元格的值</param>
        /// <param name="cellStyle">单元格样式</param>
        void HandleCell(int sheetIndex, long rowIndex, int cellIndex, object value, ICellStyle cellStyle)
        {
            // 默认实现为空
        }

        /// <summary>
        /// 处理一个sheet页完成的操作
        /// </summary>
        void DoAfterAllAnalysed()
        {
            // 默认实现为空
        }
    }
}