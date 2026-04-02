using System;
using System.Threading;

namespace WellTool.Core.Thread
{
    /// <summary>
    /// 带有信号量控制的{@link Action} 接口抽象实现
    /// </summary>
    /// <remarks>
    /// 通过设置信号量，可以限制可以访问某些资源（物理或逻辑的）线程数目。<br>
    /// 例如：设置信号量为2，表示最多有两个线程可以同时执行方法逻辑，其余线程等待，直到此线程逻辑执行完毕
    /// </remarks>
    public class SemaphoreRunnable : Runnable
    {
        /// <summary>
        /// 实际执行的逻辑
