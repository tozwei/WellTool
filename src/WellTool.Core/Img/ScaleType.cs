using System;

#if WINDOWS
using System.Drawing;
#endif

namespace WellTool.Core.Img
{
    /// <summary>
    /// 图片缩略算法类型
    /// </summary>
    public enum ScaleType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 1,
        /// <summary>
        /// 快速
        /// </summary>
        Fast = 2,
        /// <summary>
        /// 平滑
        /// </summary>
        Smooth = 3,
        /// <summary>
        /// 使用 ReplicateScaleFilter 类中包含的图像缩放算法
        /// </summary>
        Replicate = 4,
        /// <summary>
        /// Area Averaging算法
        /// </summary>
        AreaAveraging = 5
    }
}