using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.Core.Img
{
    /// <summary>
    /// 图片背景识别处理、背景替换、背景设置为矢量图
    /// 根据一定规则算出图片背景色的RGB值，进行替换
    /// </summary>
    public static class BackgroundRemoval
    {
        /// <summary>
        /// 目前暂时支持的图片类型数组
        /// 其他格式的不保证结果
        /// </summary>
        public static string[] ImageTypes = { "jpg", "png" };

        /// <summary>
        /// 背景移除
        /// 图片去底工具
        /// 将 "纯色背景的图片" 还原成 "透明背景的图片"
        /// 将纯色背景的图片转成矢量图
        /// 取图片边缘的像素点和获取到的图片主题色作为要替换的背景色
        /// 再加入一定的容差值,然后将所有像素点与该颜色进行比较
        /// 发现相同则将颜色不透明度设置为0,使颜色完全透明.
        /// </summary>
        /// <param name="inputPath">要处理图片的路径</param>
        /// <param name="outputPath">输出图片的路径</param>
        /// <param name="tolerance">容差值[根据图片的主题色,加入容差值,值的范围在0~255之间]</param>
        /// <returns>返回处理结果 true:图片处理完成 false:图片处理失败</returns>
        public static bool Remove(string inputPath, string outputPath, int tolerance)
        {
            return Remove(new FileInfo(inputPath), new FileInfo(outputPath), tolerance);
        }

        /// <summary>
        /// 背景移除
        /// </summary>
        /// <param name="input">需要进行操作的图片</param>
        /// <param name="output">最后输出的文件</param>
        /// <param name="tolerance">容差值[根据图片的主题色,加入容差值,值的取值范围在0~255之间]</param>
        /// <returns>返回处理结果 true:图片处理完成 false:图片处理失败</returns>
        public static bool Remove(FileInfo input, FileInfo output, int tolerance)
        {
            return Remove(input, output, null, tolerance);
        }

        /// <summary>
        /// 背景移除
        /// </summary>
        /// <param name="input">需要进行操作的图片</param>
        /// <param name="output">最后输出的文件</param>
        /// <param name="overrideColor">指定替换成的背景颜色 为null时背景为透明</param>
        /// <param name="tolerance">容差值[根据图片的主题色,加入容差值,值的取值范围在0~255之间]</param>
        /// <returns>返回处理结果 true:图片处理完成 false:图片处理失败</returns>
        public static bool Remove(FileInfo input, FileInfo output, object? overrideColor, int tolerance)
        {
            if (!FileTypeValidation(input, ImageTypes))
            {
                return false;
            }
            // 跨平台实现：目前返回false，需要根据具体平台实现
            return false;
        }



        /// <summary>
        /// 获取要删除的 RGB 元素
        /// 分别获取图片左上、中上、右上、右中、右下、下中、左下、左中、8个像素点rgb的16进制值
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>String数组 包含 各个位置的rgb数值</returns>
        private static string[] GetRemoveRgb(object image)
        {
            // 跨平台实现：目前返回空数组，需要根据具体平台实现
            return Array.Empty<string>();
        }

        /// <summary>
        /// 十六进制颜色码转RGB颜色值
        /// </summary>
        /// <param name="hex">十六进制颜色码</param>
        /// <returns>返回 RGB颜色值</returns>
        public static object? HexToRgb(string hex)
        {
            // 跨平台实现：目前返回null，需要根据具体平台实现
            return null;
        }

        /// <summary>
        /// 判断颜色是否在容差范围内
        /// 对比两个颜色的相似度，判断这个相似度是否小于 tolerance 容差值
        /// </summary>
        /// <param name="color1">颜色1</param>
        /// <param name="color2">颜色2</param>
        /// <param name="tolerance">容差值</param>
        /// <returns>返回true:两个颜色在容差值之内 false: 不在</returns>
        public static bool AreColorsWithinTolerance(object color1, object color2, int tolerance)
        {
            // 跨平台实现：目前返回false，需要根据具体平台实现
            return false;
        }

        /// <summary>
        /// 判断颜色是否在容差范围内
        /// 对比两个颜色的相似度，判断这个相似度是否小于 tolerance 容差值
        /// </summary>
        /// <param name="color1">颜色1</param>
        /// <param name="color2">颜色2</param>
        /// <param name="tolerance">容差色值</param>
        /// <returns>返回true:两个颜色在容差值之内 false: 不在</returns>
        public static bool AreColorsWithinTolerance(object color1, object color2, object tolerance)
        {
            // 跨平台实现：目前返回false，需要根据具体平台实现
            return false;
        }

        /// <summary>
        /// 获取图片大概的主题色
        /// 循环所有的像素点,取出出现次数最多的一个像素点的RGB值
        /// </summary>
        /// <param name="input">图片文件路径</param>
        /// <returns>返回一个图片的大概的色值 一个16进制的颜色码</returns>
        public static string GetMainColor(string input)
        {
            return GetMainColor(new FileInfo(input));
        }

        /// <summary>
        /// 获取图片大概的主题色
        /// 循环所有的像素点,取出出现次数最多的一个像素点的RGB值
        /// </summary>
        /// <param name="input">图片文件</param>
        /// <returns>返回一个图片的大概的色值 一个16进制的颜色码</returns>
        public static string GetMainColor(FileInfo input)
        {
            try
            {
                // 跨平台实现：目前返回空字符串，需要根据具体平台实现
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }



        // -------------------------------------------------------------------------- private
        /// <summary>
        /// 文件类型验证
        /// 根据给定文件类型数据，验证给定文件类型.
        /// </summary>
        /// <param name="input">需要进行验证的文件</param>
        /// <param name="imagesType">文件包含的类型数组</param>
        /// <returns>返回布尔值 false:给定文件的文件类型在文件数组中 true:给定文件的文件类型 不在给定数组中。</returns>
        private static bool FileTypeValidation(FileInfo input, string[] imagesType)
        {
            if (!input.Exists)
            {
                throw new ArgumentException("给定文件为空");
            }
            // 获取图片类型
            string type = Path.GetExtension(input.Name).TrimStart('.');
            // 类型对比
            if (!imagesType.Contains(type.ToLower()))
            {
                throw new ArgumentException($"文件类型{type}不支持");
            }
            return true;
        }
    }
}