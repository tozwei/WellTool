using System;
using System.Collections.Generic;
using System.Text;
using WellTool.Core.Lang;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符串格式化工具
    /// </summary>
    public static class StrFormatter
    {
        /// <summary>
        /// 格式化字符串<br/>
        /// 此方法只是简单将占位符 {} 按照顺序替换为参数<br/>
        /// 如果想输出 {} 使用 \转义 { 即可，如果想输出 {} 之前的 \ 使用双转义符 \\ 即可<br/>
        /// 例：<br/>
        /// 通常使用：format("this is {} for {}", "a", "b") => this is a for b<br/>
        /// 转义{}：format("this is \\{} for {}", "a", "b") => this is \{} for a<br/>
        /// 转义\：format("this is \\\\{} for {}", "a", "b") => this is \a for b<br/>
        /// </summary>
        /// <param name="strPattern">字符串模板</param>
        /// <param name="argArray">参数列表</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string strPattern, params object[] argArray)
        {
            return FormatWith(strPattern, "{}", argArray);
        }

        /// <summary>
        /// 格式化字符串<br/>
        /// 此方法只是简单将指定占位符按照顺序替换为参数<br/>
        /// 如果想输出占位符使用 \\转义即可，如果想输出占位符之前的 \ 使用双转义符 \\ 即可<br/>
        /// </summary>
        /// <param name="strPattern">字符串模板</param>
        /// <param name="placeHolder">占位符，例如{}</param>
        /// <param name="argArray">参数列表</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWith(string strPattern, string placeHolder, params object[] argArray)
        {
            if (string.IsNullOrEmpty(strPattern) || string.IsNullOrEmpty(placeHolder) || argArray == null || argArray.Length == 0)
            {
                return strPattern;
            }

            var strPatternLength = strPattern.Length;
            var placeHolderLength = placeHolder.Length;

            // 初始化定义好的长度以获得更好的性能
            var sbuf = new StringBuilder(strPatternLength + 50);

            var handledPosition = 0; // 记录已经处理到的位置
            int delimIndex; // 占位符所在位置

            for (var argIndex = 0; argIndex < argArray.Length; argIndex++)
            {
                delimIndex = strPattern.IndexOf(placeHolder, handledPosition, StringComparison.Ordinal);
                if (delimIndex == -1) // 剩余部分无占位符
                {
                    if (handledPosition == 0) // 不带占位符的模板直接返回
                    {
                        return strPattern;
                    }

                    // 字符串模板剩余部分不再包含占位符，加入剩余部分后返回结果
                    sbuf.Append(strPattern.Substring(handledPosition));
                    return sbuf.ToString();
                }

                // 转义符
                if (delimIndex > 0 && strPattern[delimIndex - 1] == '\\') // 转义符
                {
                    if (delimIndex > 1 && strPattern[delimIndex - 2] == '\\') // 双转义符
                    {
                        // 转义符之前还有一个转义符，占位符依旧有效
                        sbuf.Append(strPattern, handledPosition, delimIndex - 1);
                        sbuf.Append(argArray[argIndex]?.ToString());
                        handledPosition = delimIndex + placeHolderLength;
                    }
                    else
                    {
                        // 占位符被转义
                        argIndex--;
                        sbuf.Append(strPattern, handledPosition, delimIndex - 1);
                        sbuf.Append(placeHolder[0]);
                        handledPosition = delimIndex + 1;
                    }
                }
                else // 正常占位符
                {
                    sbuf.Append(strPattern, handledPosition, delimIndex - handledPosition);
                    sbuf.Append(argArray[argIndex]?.ToString());
                    handledPosition = delimIndex + placeHolderLength;
                }
            }

            // 加入最后一个占位符后所有的字符
            sbuf.Append(strPattern.Substring(handledPosition));

            return sbuf.ToString();
        }

        /// <summary>
        /// 格式化文本，使用 {varName} 占位<br/>
        /// map = {a: "aValue", b: "bValue"} format("{a} and {b}", map) ===> aValue and bValue
        /// </summary>
        /// <param name="strPattern">字符串模板</param>
        /// <param name="valueMap">值 Map</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string strPattern, IDictionary<string, object> valueMap)
        {
            if (string.IsNullOrEmpty(strPattern) || valueMap == null || valueMap.Count == 0)
            {
                return strPattern;
            }

            foreach (var entry in valueMap)
            {
                strPattern = strPattern.Replace("{" + entry.Key + "}", entry.Value?.ToString());
            }

            return strPattern;
        }

        /// <summary>
        /// 格式化文本，使用 {varName} 占位<br/>
        /// </summary>
        /// <param name="strPattern">字符串模板</param>
        /// <param name="values">值</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatDict(string strPattern, params object[] values)
        {
            if (string.IsNullOrEmpty(strPattern) || values == null || values.Length == 0)
            {
                return strPattern;
            }

            for (var i = 0; i < values.Length; i++)
            {
                strPattern = strPattern.Replace("{" + i + "}", values[i]?.ToString());
            }

            return strPattern;
        }
    }
}
