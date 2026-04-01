using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 单元格处理器接口<br>
    /// 用于在读取Excel单元格值时自定义结果值的获取，如在获取值的同时，获取单元格样式、坐标等信息，或根据单元格信息，装饰转换结果值
    /// </summary>
    public interface ICellHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="cell">单元格对象，可以获取单元格行、列样式等信息</param>
        /// <param name="value">单元格值</param>
        void Handle(ICell cell, object value);
    }
}