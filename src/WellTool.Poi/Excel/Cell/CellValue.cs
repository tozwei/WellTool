namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 抽象的单元格值接口，用于判断不同类型的单元格值
    /// </summary>
    /// <typeparam name="T">值得类型</typeparam>
    public interface ICellValue<T>
    {
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <returns>值</returns>
        T GetValue();
    }
}