using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// Excel <see cref="ISheet"/>读取接口，通过实现此接口，将<see cref="ISheet"/>中的数据读取为不同类型。
    /// </summary>
    /// <typeparam name="T">读取的数据类型</typeparam>
    public interface ISheetReader<T>
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        T Read(ISheet sheet);
    }
}