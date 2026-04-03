namespace WellTool.Core.Text.Escape;

/// <summary>
/// 内部Escape工具类
/// </summary>
internal static class InternalEscapeUtil
{
    /// <summary>
    /// 将数组中的0和1位置的值互换，即键值转换
    /// </summary>
    /// <param name="array">被转换的数组</param>
    /// <returns>转换后的数组</returns>
    public static string[][] Invert(string[][] array)
    {
        var newarray = new string[array.Length][];
        for (int i = 0; i < array.Length; i++)
        {
            newarray[i] = new[] { array[i][1], array[i][0] };
        }
        return newarray;
    }
}
