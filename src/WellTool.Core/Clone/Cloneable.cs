namespace WellTool.Core.Clone
{
    /// <summary>
    /// 克隆接口
    /// </summary>
    public interface Cloneable<T>
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <returns>克隆后的对象</returns>
        T Clone();
    }
}