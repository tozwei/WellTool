using System.Xml.Linq;

namespace WellTool.Poi.Excel.Sax
{
    /// <summary>
    /// Excel的XML中属性名枚举
    /// </summary>
    public enum AttributeName
    {
        /// <summary>
        /// 行列号属性，行标签下此为行号属性名，cell标签下下为列号属性名
        /// </summary>
        r,
        
        /// <summary>
        /// ST（StylesTable） 的索引，样式index，用于获取行或单元格样式
        /// </summary>
        s,
        
        /// <summary>
        /// Type类型，单元格类型属性，见<see cref="CellDataType"/>
        /// </summary>
        t
    }

    /// <summary>
    /// AttributeName 扩展方法
    /// </summary>
    public static class AttributeNameExtensions
    {
        /// <summary>
        /// 是否匹配给定属性
        /// </summary>
        /// <param name="attributeName">属性枚举</param>
        /// <param name="attributeNameString">属性名字符串</param>
        /// <returns>是否匹配</returns>
        public static bool Match(this AttributeName attributeName, string attributeNameString)
        {
            return attributeName.ToString().Equals(attributeNameString);
        }

        /// <summary>
        /// 从XElement中获取对应属性值
        /// </summary>
        /// <param name="attributeName">属性枚举</param>
        /// <param name="element">XElement</param>
        /// <returns>属性值</returns>
        public static string GetValue(this AttributeName attributeName, XElement element)
        {
            return element.Attribute(attributeName.ToString())?.Value;
        }
    }
}