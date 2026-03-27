using System;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 转换器接口
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        object Convert(object value, Type targetType);

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        Type[] GetSupportedSourceTypes();

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        Type[] GetSupportedTargetTypes();
    }
}