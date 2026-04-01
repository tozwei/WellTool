using System.Collections.Generic;
using System.Linq;

namespace WellTool.Extra.Pinyin
{
    /// <summary>
    /// 拼音引擎接口，具体的拼音实现通过实现此接口，完成具体实现功能
    /// </summary>
    public interface PinyinEngine
    {
        /// <summary>
        /// 如果c为汉字，则返回大写拼音；如果c不是汉字，则返回c.ToString()
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        string GetPinyin(char c);

        /// <summary>
        /// 如果c为汉字，则返回大写拼音；如果c不是汉字，则返回c.ToString()
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="tone">是否返回声调</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        string GetPinyin(char c, bool tone);

        /// <summary>
        /// 获取字符串对应的完整拼音，非中文返回原字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">拼音之间的分隔符</param>
        /// <returns>拼音</returns>
        string GetPinyin(string str, string separator);

        /// <summary>
        /// 获取字符串对应的完整拼音，非中文返回原字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">拼音之间的分隔符</param>
        /// <param name="tone">是否返回声调</param>
        /// <returns>拼音</returns>
        string GetPinyin(string str, string separator, bool tone);

        /// <summary>
        /// 将输入字符转为拼音首字母，其它字符原样返回
        /// </summary>
        /// <param name="c">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        char GetFirstLetter(char c);

        /// <summary>
        /// 将输入字符串转为拼音首字母，其它字符原样返回
        /// </summary>
        /// <param name="str">任意字符，汉字返回拼音，非汉字原样返回</param>
        /// <param name="separator">分隔符</param>
        /// <returns>汉字返回拼音，非汉字原样返回</returns>
        string GetFirstLetter(string str, string separator);
    }
}