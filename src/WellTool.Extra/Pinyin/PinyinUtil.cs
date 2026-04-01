using System.Text.RegularExpressions;

namespace WellTool.Extra.Pinyin
{
    /// <summary>
    /// 拼音工具类
    /// </summary>
    public static class PinyinUtil
    {
        private static readonly Regex ChineseRegex = new Regex("[\\u4e00-\\u9fa5]");

        /// <summary>
        /// 获得全局单例的拼音引擎
        /// </summary>
        /// <returns>全局单例的拼音引擎</returns>
        public static PinyinEngine GetEngine()
        {
            return PinyinFactory.Get();
        }

        /// <summary>
        /// 如果c为汉字，则返回大写拼音；如果c不是汉字，则返回c.ToString()
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(char c)
        {
            return GetEngine().GetPinyin(c);
        }

        /// <summary>
        /// 如果c为汉字，则返回大写拼音；如果c不是汉字，则返回c.ToString()
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="tone">是否返回声调</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(char c, bool tone)
        {
            return GetEngine().GetPinyin(c, tone);
        }

        /// <summary>
        /// 将输入字符串转为拼音，每个字之间的拼音使用空格分隔
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(string str)
        {
            return GetPinyin(str, " ");
        }

        /// <summary>
        /// 将输入字符串转为拼音，每个字之间的拼音使用空格分隔
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="tone">是否返回声调</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(string str, bool tone)
        {
            return GetPinyin(str, " ", tone);
        }

        /// <summary>
        /// 将输入字符串转为拼音，以字符为单位插入分隔符
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="separator">每个字拼音之间的分隔符</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(string str, string separator)
        {
            return GetEngine().GetPinyin(str, separator);
        }

        /// <summary>
        /// 将输入字符串转为拼音，以字符为单位插入分隔符
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="separator">每个字拼音之间的分隔符</param>
        /// <param name="tone">是否返回声调</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetPinyin(string str, string separator, bool tone)
        {
            return GetEngine().GetPinyin(str, separator, tone);
        }

        /// <summary>
        /// 将输入字符转为拼音首字母，其它字符原样返回
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static char GetFirstLetter(char c)
        {
            return GetEngine().GetFirstLetter(c);
        }

        /// <summary>
        /// 将输入字符串转为拼音首字母，其它字符原样返回
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="separator">分隔符</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        public static string GetFirstLetter(string str, string separator)
        {
            return str == null ? null : GetEngine().GetFirstLetter(str, separator);
        }

        /// <summary>
        /// 是否为中文字符
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>是否为中文字符</returns>
        public static bool IsChinese(char c)
        {
            return c == '〇' || ChineseRegex.IsMatch(c.ToString());
        }
    }
}