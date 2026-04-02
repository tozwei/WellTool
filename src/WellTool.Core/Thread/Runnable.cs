namespace WellTool.Core.Threading
{
    /// <summary>
    /// 可运行接口
    /// </summary>
    public abstract class Runnable
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// 隐式转换为 Action
        /// </summary>
        /// <param name="runnable">Runnable 实例</param>
        public static implicit operator System.Action(Runnable runnable)
        {
            return runnable.Run;
        }
    }
}