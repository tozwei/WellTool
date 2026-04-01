using System;using System.Collections.Generic;

namespace WellTool.Poi.Excel.Sax.Handler
{
    /// <summary>
    /// 抽象行数据处理器，通过实现<see cref="Handle(int, long, List{object})"/> 处理原始数据<br>
    /// 并调用<see cref="HandleData(int, long, T)"/>处理经过转换后的数据。
    /// </summary>
    /// <typeparam name="T">转换后的数据类型</typeparam>
    public abstract class AbstractRowHandler<T> : IRowHandler
    {
        /// <summary>
        /// 读取起始行（包含，从0开始计数）
        /// </summary>
        protected readonly int StartRowIndex;
        
        /// <summary>
        /// 读取结束行（包含，从0开始计数）
        /// </summary>
        protected readonly int EndRowIndex;
        
        /// <summary>
        /// 行数据转换函数
        /// </summary>
        protected Func<List<object>, T> ConvertFunc;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="startRowIndex">读取起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">读取结束行（包含，从0开始计数）</param>
        public AbstractRowHandler(int startRowIndex, int endRowIndex)
        {
            StartRowIndex = startRowIndex;
            EndRowIndex = endRowIndex;
        }

        /// <summary>
        /// 处理一行数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号，从0开始计数</param>
        /// <param name="rowCells">行数据，每个object表示一个单元格的值</param>
        public virtual void Handle(int sheetIndex, long rowIndex, List<object> rowCells)
        {
            if (ConvertFunc == null)
            {
                throw new ArgumentNullException(nameof(ConvertFunc));
            }
            if (rowIndex < StartRowIndex || rowIndex > EndRowIndex)
            {
                return;
            }
            HandleData(sheetIndex, rowIndex, ConvertFunc(rowCells));
        }

        /// <summary>
        /// 处理转换后的数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号，从0开始计数</param>
        /// <param name="data">行数据</param>
        public abstract void HandleData(int sheetIndex, long rowIndex, T data);

        /// <summary>
        /// 处理一个单元格的数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号</param>
        /// <param name="cellIndex">当前列号</param>
        /// <param name="value">单元格的值</param>
        /// <param name="cellStyle">单元格样式</param>
        public virtual void HandleCell(int sheetIndex, long rowIndex, int cellIndex, object value, NPOI.SS.UserModel.ICellStyle cellStyle)
        {
            // 默认实现为空
        }

        /// <summary>
        /// 处理一个sheet页完成的操作
        /// </summary>
        public virtual void DoAfterAllAnalysed()
        {
            // 默认实现为空
        }
    }
}