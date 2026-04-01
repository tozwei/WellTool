using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 单元格值自定义设置器，主要用于Excel数据导出，用户通过自定义此接口，实现可定制化的单元格值设定
    /// </summary>
    public interface ICellSetter
    {
        /// <summary>
        /// 自定义单元格值设置，同时可以设置单元格样式、格式等信息
        /// </summary>
        /// <param name="cell">单元格</param>
        void SetValue(ICell cell);
    }
}