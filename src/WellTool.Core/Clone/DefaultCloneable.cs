namespace WellTool.Core.Clone
{
    /// <summary>
    /// 默认克隆接口实现
    /// </summary>
    /// <typeparam name="T">克隆类型</typeparam>
    public class DefaultCloneable<T> : CloneSupport<T>
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <returns>克隆后的对象</returns>
        public override T Clone()
        {
            return base.Clone();
        }
    }
}