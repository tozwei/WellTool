using System.Collections.Generic;

namespace WellTool.Poi.Excel.Sax
{
    /// <summary>
    /// 单元格数据类型枚举
    /// </summary>
    public enum CellDataType
    {
        /// <summary>Boolean类型</summary>
        BOOL,
        
        /// <summary>类型错误</summary>
        ERROR,
        
        /// <summary>计算结果类型，此类型使用f标签辅助判断，而非属性</summary>
        FORMULA,
        
        /// <summary>富文本类型</summary>
        INLINESTR,
        
        /// <summary>共享字符串索引类型</summary>
        SSTINDEX,
        
        /// <summary>数字类型</summary>
        NUMBER,
        
        /// <summary>日期类型，此类型使用值判断，而非属性</summary>
        DATE,
        
        /// <summary>空类型</summary>
        NULL
    }

    /// <summary>
    /// 单元格数据类型扩展
    /// </summary>
    public static class CellDataTypeExtensions
    {
        private static readonly Dictionary<CellDataType, string> _nameMap = new()
        {
            { CellDataType.BOOL, "b" },
            { CellDataType.ERROR, "e" },
            { CellDataType.FORMULA, "formula" },
            { CellDataType.INLINESTR, "inlineStr" },
            { CellDataType.SSTINDEX, "s" },
            { CellDataType.NUMBER, "" },
            { CellDataType.DATE, "m/d/yy" },
            { CellDataType.NULL, "" }
        };

        /// <summary>
        /// 获取类型属性值
        /// </summary>
        /// <param name="type">单元格数据类型</param>
        /// <returns>类型属性值</returns>
        public static string GetName(this CellDataType type)
        {
            return _nameMap.TryGetValue(type, out var name) ? name : "";
        }

        /// <summary>
        /// 类型字符串转为枚举
        /// </summary>
        /// <param name="name">类型字符串</param>
        /// <returns>类型枚举</returns>
        public static CellDataType Of(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                //默认数字
                return CellDataType.NUMBER;
            }

            foreach (var (type, typeName) in _nameMap)
            {
                if (typeName.Equals(name))
                {
                    return type;
                }
            }

            return CellDataType.NULL;
        }
    }
}