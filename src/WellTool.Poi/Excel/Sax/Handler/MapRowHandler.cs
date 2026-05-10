using System;
using System.Collections.Generic;
using WellTool.Poi.Util;

namespace WellTool.Poi.Excel.Sax.Handler
{
    /// <summary>
    /// Map形式的行处理器<br>
    /// 将一行数据转换为Map，key为指定行，value为当前行对应位置的值
    /// </summary>
    public abstract class MapRowHandler : AbstractRowHandler<Dictionary<string, object>>
    {
        /// <summary>
        /// 标题所在行（从0开始计数）
        /// </summary>
        private readonly int _headerRowIndex;
        
        /// <summary>
        /// 标题行
        /// </summary>
        protected List<string> HeaderList;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="headerRowIndex">标题所在行（从0开始计数）</param>
        /// <param name="startRowIndex">读取起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">读取结束行（包含，从0开始计数）</param>
        public MapRowHandler(int headerRowIndex, int startRowIndex, int endRowIndex)
            : base(startRowIndex, endRowIndex)
        {
            _headerRowIndex = headerRowIndex;
            ConvertFunc = (rowList) => PoiUtil.ToDictionary(HeaderList, rowList, true);
        }

        /// <summary>
        /// 处理一行数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号，从0开始计数</param>
        /// <param name="rowCells">行数据，每个object表示一个单元格的值</param>
        public override void Handle(int sheetIndex, long rowIndex, List<object> rowCells)
        {
            if (rowIndex == _headerRowIndex)
            {
                HeaderList = rowCells.ConvertAll(obj => obj?.ToString() ?? string.Empty);
                return;
            }
            base.Handle(sheetIndex, rowIndex, rowCells);
        }
    }
}