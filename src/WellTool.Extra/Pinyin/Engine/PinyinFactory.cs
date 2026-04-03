using System;
using System.Collections.Generic;

namespace WellTool.Extra.Pinyin.Engine
{
    /// <summary>
    /// 拼音引擎工厂
    /// </summary>
    public static class PinyinFactory
    {
        private static PinyinEngine _instance;

        /// <summary>
        /// 获取单例的拼音引擎
        /// </summary>
        /// <returns>拼音引擎</returns>
        public static PinyinEngine Get()
        {
            if (_instance == null)
            {
                _instance = Create();
            }
            return _instance;
        }

        /// <summary>
        /// 创建拼音引擎
        /// </summary>
        /// <returns>拼音引擎</returns>
        public static PinyinEngine Create()
        {
            var engine = DoCreate();
            return engine;
        }

        /// <summary>
        /// 执行创建拼音引擎
        /// </summary>
        /// <returns>拼音引擎</returns>
        private static PinyinEngine DoCreate()
        {
            // 尝试加载可用的拼音引擎
            // 默认返回简单的拼音引擎
            try
            {
                // 可以在这里添加更多的引擎检测逻辑
                // 例如检测是否有 TinyPinyin 或 JPinyin 库
                return new TinyPinyinEngine();
            }
            catch
            {
                throw new PinyinException("No pinyin engine found! Please add a pinyin package to your project!");
            }
        }
    }

    /// <summary>
    /// TinyPinyin 拼音引擎实现
    /// </summary>
    public class TinyPinyinEngine : PinyinEngine
    {
        // 简化的汉字到拼音映射
        private static readonly Dictionary<char, string> PinyinMap = new Dictionary<char, string>
        {
            {'中', "zhong"},
            {'国', "guo"},
            {'人', "ren"},
            {'我', "wo"},
            {'你', "ni"},
            {'是', "shi"},
            {'的', "de"},
            {'了', "le"},
            {'在', "zai"},
            {'有', "you"},
            {'和', "he"},
            {'与', "yu"},
            {'一', "yi"},
            {'不', "bu"},
            {'这', "zhe"},
            {'那', "na"},
            {'个', "ge"},
            {'们', "men"},
            {'来', "lai"},
            {'为', "wei"},
            {'上', "shang"},
            {'下', "xia"},
            {'大', "da"},
            {'小', "xiao"},
            {'多', "duo"},
            {'少', "shao"},
            {'好', "hao"},
            {'很', "hen"},
            {'也', "ye"},
            {'就', "jiu"},
            {'都', "dou"},
            {'而', "er"},
            {'能', "neng"},
            {'会', "hui"},
            {'可', "ke"},
            {'以', "yi"},
            {'要', "yao"},
            {'去', "qu"},
            {'看', "kan"},
            {'说', "shuo"},
            {'听', "ting"},
            {'想', "xiang"},
            {'做', "zuo"},
            {'没', "mei"},
            {'对', "dui"},
            {'把', "ba"},
            {'给', "gei"},
            {'被', "bei"},
            {'让', "rang"},
            {'从', "cong"},
            {'到', "dao"},
            {'里', "li"},
            {'得', "de"},
            {'地', "di"},
            {'着', "zhe"},
            {'过', "guo"},
            {'出', "chu"},
            {'起', "qi"},
            {'还', "hai"},
            {'时', "shi"},
            {'候', "hou"},
            {'年', "nian"},
            {'月', "yue"},
            {'日', "ri"},
            {'号', "hao"},
            {'天', "tian"},
            {'时', "shi"},
            {'分', "fen"},
            {'秒', "miao"}
        };

        /// <summary>
        /// 获取字符的拼音
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns>拼音，如果找不到返回原字符</returns>
        public string GetPinyin(char c)
        {
            if (PinyinMap.TryGetValue(c, out var pinyin))
            {
                return pinyin;
            }
            return c.ToString();
        }

        /// <summary>
        /// 获取字符串的拼音
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拼音</returns>
        public string GetPinyin(string text, string separator = "")
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var result = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                if (result.Length > 0 && separator != null)
                {
                    result.Append(separator);
                }
                result.Append(GetPinyin(c));
            }
            return result.ToString();
        }

        /// <summary>
        /// 获取字符串的首字母
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>首字母</returns>
        public string GetFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var result = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                var pinyin = GetPinyin(c);
                if (!string.IsNullOrEmpty(pinyin))
                {
                    result.Append(char.ToUpper(pinyin[0]));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
