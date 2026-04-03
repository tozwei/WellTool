using System;

namespace WellTool.Extra.Pinyin.Engine.TinyPinyin
{
    /// <summary>
    /// 封装了TinyPinyin的引擎。
    /// 
    /// 需要安装对应NuGet包
    /// </summary>
    public class TinyPinyinEngine : PinyinEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public TinyPinyinEngine()
        {
        }

        /// <summary>
        /// 获取字符的拼音（无了声调）
        /// </summary>
        /// <param name="c">任意字符</param>
        /// <returns>拼音或原字符</returns>
        public string GetPinyin(char c)
        {
            return PinyinUtil.GetPinyin(c, false);
        }

        /// <summary>
        /// 获取字符的拼音
        /// </summary>
        /// <param name="c">任意字符</param>
        /// <param name="tone">是否带声调</param>
        /// <returns>拼音或原字符</returns>
        public string GetPinyin(char c, bool tone)
        {
            return PinyinUtil.GetPinyin(c, tone);
        }

        /// <summary>
        /// 获取字符串的拼音
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拼音字符串</returns>
        public string GetPinyin(string str, string separator)
        {
            return PinyinUtil.GetPinyin(str, separator, false);
        }

        /// <summary>
        /// 获取字符串的拼音
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="tone">是否带声调</param>
        /// <returns>拼音字符串</returns>
        public string GetPinyin(string str, string separator, bool tone)
        {
            return PinyinUtil.GetPinyin(str, separator, tone);
        }

        /// <summary>
        /// 获取首字母
        /// </summary>
        /// <param name="c">任意字符</param>
        /// <returns>首字母或原字符</returns>
        public char GetFirstLetter(char c)
        {
            return PinyinUtil.GetFirstLetter(c);
        }

        /// <summary>
        /// 获取首字母字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>首字母字符串</returns>
        public string GetFirstLetter(string str, string separator)
        {
            return PinyinUtil.GetFirstLetter(str, separator);
        }
    }
}
