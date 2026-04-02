using System;
using System.Threading.Tasks;

namespace WellTool.Core.Thread
{
    /// <summary>
    /// 可终结的执行器服务代理
    /// </summary>
    public class FinalizableDelegatedExecutorService : DelegatedExecutorService
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="executor"><see cref="IExecutorService"/></param>
        public FinalizableDelegatedExecutorService(IExecutorService executor) : base(executor)
        {
        }

        /// <summary>
        /// 终结器
        /// </summary>
        ~FinalizableDelegatedExecutorService()
        {
            try
            {
                Shutdown();
            }
            catch (Exception ex)
            {
                // 忽略异常
            }
        }
    }
}