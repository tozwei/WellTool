using System;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// TaskFactory 扩展方法
    /// </summary>
    public static class TaskFactoryExtensions
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="taskFactory">TaskFactory</param>
        /// <param name="action">要执行的操作</param>
        public static void Execute(this TaskFactory taskFactory, Action action)
        {
            taskFactory.StartNew(action);
        }
    }
}