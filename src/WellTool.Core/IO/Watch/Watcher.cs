using System.IO;

namespace WellTool.Core.IO.Watch
{
    /// <summary>
    /// 观察者（监视器）
    /// </summary>
    public interface Watcher
    {
        /// <summary>
        /// 文件创建时执行的方法
        /// </summary>
        /// <param name="e">文件系统事件参数</param>
        /// <param name="currentPath">事件发生的当前路径</param>
        void OnCreate(FileSystemEventArgs e, string currentPath);

        /// <summary>
        /// 文件修改时执行的方法<br>
        /// 文件修改可能触发多次
        /// </summary>
        /// <param name="e">文件系统事件参数</param>
        /// <param name="currentPath">事件发生的当前路径</param>
        void OnModify(FileSystemEventArgs e, string currentPath);

        /// <summary>
        /// 文件删除时执行的方法
        /// </summary>
        /// <param name="e">文件系统事件参数</param>
        /// <param name="currentPath">事件发生的当前路径</param>
        void OnDelete(FileSystemEventArgs e, string currentPath);

        /// <summary>
        /// 事件丢失或出错时执行的方法
        /// </summary>
        /// <param name="e">错误事件参数</param>
        /// <param name="currentPath">事件发生的当前路径</param>
        void OnError(ErrorEventArgs e, string currentPath);
    }
}