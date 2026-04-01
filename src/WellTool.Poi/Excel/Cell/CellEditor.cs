using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 单元格编辑器接口<br>
    /// 在读取Excel值时，有时我们需要针对所有单元格统一处理结果值（如null转默认值）的情况，实现接口并调用<br>
    /// reader.SetCellEditor()设置编辑器
    /// </summary>
    public interface ICellEditor
    {
        /// <summary>
        /// 编辑，根据单元格信息处理结果值，返回处理后的结果
        /// </summary>
        /// <param name="cell">单元格对象，可以获取单元格行、列样式等信息</param>
        /// <param name="value">单元格值</param>
        /// <returns>编辑后的对象</returns>
        object Edit(ICell cell, object value);
    }
}