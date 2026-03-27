using System.IO;

namespace WellTool.Core.IO.Watch
{
    /// <summary>
    /// 监听事件类型枚举
    /// </summary>
    public enum WatchKind
    {
        /// <summary>
        /// 事件丢失
        /// </summary>
        OVERFLOW,
        /// <summary>
        /// 修改事件
        /// </summary>
        MODIFY,
        /// <summary>
        /// 创建事件
        /// </summary>
        CREATE,
        /// <summary>
        /// 删除事件
        /// </summary>
        DELETE
    }

    /// <summary>
    /// WatchKind 扩展方法
    /// </summary>
    public static class WatchKindExtensions
    {
        /// <summary>
        /// 获取全部事件类型对应的NotifyFilters
        /// </summary>
        public static NotifyFilters All
        {
            get
            {
                return NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            }
        }

        /// <summary>
        /// 将WatchKind转换为对应的NotifyFilters
        /// </summary>
        /// <param name="watchKind">WatchKind枚举值</param>
        /// <returns>对应的NotifyFilters</returns>
        public static NotifyFilters ToNotifyFilter(this WatchKind watchKind)
        {
            switch (watchKind)
            {
                case WatchKind.MODIFY:
                    return NotifyFilters.LastWrite;
                case WatchKind.CREATE:
                    return NotifyFilters.FileName | NotifyFilters.DirectoryName;
                case WatchKind.DELETE:
                    return NotifyFilters.FileName | NotifyFilters.DirectoryName;
                default:
                    return 0;
            }
        }
    }
}