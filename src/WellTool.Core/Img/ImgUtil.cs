using System;
using System.IO;

namespace WellTool.Core.Img
{
    public static class ImgUtil
    {
        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <returns>缩放后的图像</returns>
        public static object Scale(object image, int width, int height)
        {
            // 跨平台实现
            return image;
        }

        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="scale">缩放比例</param>
        /// <returns>缩放后的图像</returns>
        public static object Scale(object image, double scale)
        {
            // 跨平台实现
            return image;
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="path">保存路径</param>
        public static void Save(object image, string path)
        {
            // 跨平台实现
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="path">保存路径</param>
        /// <param name="format">图像格式</param>
        public static void Save(object image, string path, object format)
        {
            // 跨平台实现
        }

        /// <summary>
        /// 将图像转换为字节数组
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="format">图像格式</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes(object image, object format)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 从字节数组创建图像
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>图像对象</returns>
        public static object FromBytes(byte[] bytes)
        {
            // 跨平台实现
            return null;
        }

        /// <summary>
        /// 图像灰度化
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <returns>灰度图像</returns>
        public static object Gray(object image)
        {
            // 跨平台实现
            return image;
        }

        /// <summary>
        /// 图像旋转
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>旋转后的图像</returns>
        public static object Rotate(object image, float angle)
        {
            // 跨平台实现
            return image;
        }

        /// <summary>
        /// 图像翻转
        /// </summary>
        /// <param name="image">图像对象</param>
        /// <param name="flipType">翻转类型</param>
        /// <returns>翻转后的图像</returns>
        public static object Flip(object image, FlipType flipType)
        {
            // 跨平台实现
            return image;
        }

        /// <summary>
        /// 翻转类型
        /// </summary>
        public enum FlipType
        {
            None = 0,
            Horizontal = 1,
            Vertical = 2,
            Both = 3
        }
    }
}
