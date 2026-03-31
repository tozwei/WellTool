namespace WellTool.Poi;

/// <summary>
/// 单元格位置
/// </summary>
public class CellLocation
{
    /// <summary>
    /// 列索引，从0开始
    /// </summary>
    public int X { get; }

    /// <summary>
    /// 行索引，从0开始
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="x">列索引</param>
    /// <param name="y">行索引</param>
    public CellLocation(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}