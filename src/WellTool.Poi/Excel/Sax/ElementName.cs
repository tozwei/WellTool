namespace WellTool.Poi.Excel.Sax
{
    /// <summary>
    /// 标签名枚举
    /// </summary>
    public enum ElementName
    {
        /// <summary>
        /// 行标签名，表示一行
        /// </summary>
        row,
        
        /// <summary>
        /// Cell单元格标签名，表示一个单元格
        /// </summary>
        c,
        
        /// <summary>
        /// Value单元格值的标签，表示单元格内的值
        /// </summary>
        v,
        
        /// <summary>
        /// Formula公式，表示一个存放公式的单元格
        /// </summary>
        f
    }

    /// <summary>
    /// ElementName 扩展方法
    /// </summary>
    public static class ElementNameExtensions
    {
        /// <summary>
        /// 给定标签名是否匹配当前标签
        /// </summary>
        /// <param name="elementName">标签枚举</param>
        /// <param name="elementNameString">标签名字符串</param>
        /// <returns>是否匹配</returns>
        public static bool Match(this ElementName elementName, string elementNameString)
        {
            return elementName.ToString().Equals(elementNameString);
        }

        /// <summary>
        /// 解析支持的节点名枚举
        /// </summary>
        /// <param name="elementNameString">节点名字符串</param>
        /// <returns>节点名枚举</returns>
        public static ElementName? Of(string elementNameString)
        {
            if (Enum.TryParse<ElementName>(elementNameString, out var result))
            {
                return result;
            }
            return null;
        }
    }
}