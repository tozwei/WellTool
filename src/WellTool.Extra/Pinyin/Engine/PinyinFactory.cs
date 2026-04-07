using System;
using System.Collections.Generic;
using System.Linq;

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
        public static PinyinEngine Get()
        {
            if (_instance == null)
                _instance = Create();
            return _instance;
        }

        /// <summary>
        /// 创建拼音引擎
        /// </summary>
        public static PinyinEngine Create()
        {
            return DoCreate();
        }

        /// <summary>
        /// 执行创建拼音引擎 - 使用内置引擎
        /// </summary>
        private static PinyinEngine DoCreate()
        {
            return new BuiltinPinyinEngine();
        }
    }

    /// <summary>
    /// 内置拼音引擎 - 覆盖常用汉字
    /// </summary>
    public class BuiltinPinyinEngine : PinyinEngine
    {
        private static readonly Dictionary<char, string> PinyinMap = new Dictionary<char, string>
        {
            // 常用汉字拼音映射
            {'你', "ni"}, {'好', "hao"}, {'我', "wo"}, {'是', "shi"}, {'的', "de"},
            {'中', "zhong"}, {'国', "guo"}, {'人', "ren"}, {'在', "zai"}, {'有', "you"},
            {'和', "he"}, {'与', "yu"}, {'一', "yi"}, {'不', "bu"}, {'这', "zhe"},
            {'那', "na"}, {'个', "ge"}, {'们', "men"}, {'来', "lai"}, {'为', "wei"},
            {'上', "shang"}, {'下', "xia"}, {'大', "da"}, {'小', "xiao"}, {'多', "duo"},
            {'少', "shao"}, {'很', "hen"}, {'也', "ye"}, {'就', "jiu"}, {'都', "dou"},
            {'而', "er"}, {'能', "neng"}, {'会', "hui"}, {'可', "ke"}, {'以', "yi"},
            {'要', "yao"}, {'去', "qu"}, {'看', "kan"}, {'说', "shuo"}, {'听', "ting"},
            {'想', "xiang"}, {'做', "zuo"}, {'没', "mei"}, {'对', "dui"}, {'把', "ba"},
            {'给', "gei"}, {'被', "bei"}, {'让', "rang"}, {'从', "cong"}, {'到', "dao"},
            {'里', "li"}, {'得', "de"}, {'地', "di"}, {'着', "zhe"}, {'过', "guo"},
            {'出', "chu"}, {'起', "qi"}, {'还', "hai"}, {'时', "shi"}, {'候', "hou"},
            {'年', "nian"}, {'月', "yue"}, {'日', "ri"}, {'号', "hao"}, {'天', "tian"},
            {'分', "fen"}, {'秒', "miao"}, {'怡', "yi"}, {'欢', "huan"}, {'迎', "ying"},
            {'程', "cheng"}, {'序', "xu"}, {'码', "ma"}, {'文', "wen"}, {'件', "jian"},
            {'测', "ce"}, {'试', "shi"}, {'数', "shu"}, {'据', "ju"}, {'库', "ku"},
            {'开', "kai"}, {'发', "fa"}, {'工', "gong"}, {'具', "ju"}, {'类', "lei"},
            {'接', "jie"}, {'口', "kou"}, {'方', "fang"}, {'法', "fa"}, {'函', "han"},
            {'数', "shu"}, {'据', "ju"}, {'格', "ge"}, {'式', "shi"}, {'处', "chu"},
            {'理', "li"}, {'请', "qing"}, {'求', "qiu"}, {'响', "xiang"}, {'应', "ying"},
            {'错', "cuo"}, {'误', "wu"}, {'注', "zhu"}, {'意', "yi"}, {'项', "xiang"},
            {'目', "mu"}, {'问', "wen"}, {'题', "ti"}, {'答', "da"}, {'复', "fu"},
            {'是', "shi"}, {'否', "fou"}, {'通', "tong"}, {'过', "guo"}, {'请', "qing"},
            {'求', "qiu"}, {'更', "geng"}, {'新', "xin"}, {'内', "nei"}, {'容', "rong"},
            {'版', "ban"}, {'本', "ben"}, {'发', "fa"}, {'布', "bu"}, {'页', "ye"},
            {'面', "mian"}, {'请', "qing"}, {'输', "shu"}, {'入', "ru"}, {'账', "zhang"},
            {'号', "hao"}, {'密', "mi"}, {'码', "ma"}, {'确', "que"}, {'认', "ren"},
            {'认', "ren"}, {'首', "shou"}, {'页', "ye"}, {'菜', "cai"}, {'单', "dan"},
            {'设', "she"}, {'置', "zhi"}, {'保', "bao"}, {'存', "cun"}, {'删', "shan"},
            {'改', "gai"}, {'查', "cha"}, {'找', "zhao"}, {'列', "lie"}, {'表', "biao"},
            {'格', "ge"}, {'图', "tu"}, {'片', "pian"}, {'播', "bo"}, {'放', "fang"},
            {'音', "yin"}, {'频', "pin"}, {'视', "shi"}, {'频', "pin"}, {'文', "wen"},
            {'字', "zi"}, {'串', "chuan"}, {'接', "jie"}, {'口', "kou"}, {'调', "tiao"},
            {'试', "shi"}, {'调', "tiao"}, {'用', "yong"}, {'调', "tiao"}, {'配', "pei"},
            {'置', "zhi"}, {'环', "huan"}, {'境', "jing"}, {'平', "ping"}, {'台', "tai"},
            {'系', "xi"}, {'统', "tong"}, {'网', "wang"}, {'站', "zhan"}, {'页', "ye"},
            {'请', "qing"}, {'即', "ji"}, {'时', "shi"}, {'联', "lian"}, {'系', "xi"},
            {'电', "dian"}, {'话', "hua"}, {'邮', "you"}, {'件', "jian"}, {'发', "fa"},
            {'送', "song"}, {'接', "jie"}, {'收', "shou"}, {'验', "yan"}, {'证', "zheng"},
            {'确', "que"}, {'认', "ren"}, {'通', "tong"}, {'知', "zhi"}, {'晓', "xiao"},
            {'明', "ming"}, {'白', "bai"}, {'告', "gao"}, {'诉', "su"}, {'知', "zhi"},
            {'道', "dao"}, {'理', "li"}, {'解', "jie"}, {'释', "shi"}, {'读', "du"},
            {'写', "xie"}, {'记', "ji"}, {'录', "lu"}, {'算', "suan"}, {'法', "fa"},
            {'逻', "luo"}, {'辑', "ji"}, {'判', "pan"}, {'断', "duan"}, {'循', "xun"},
            {'环', "huan"}, {'条', "tiao"}, {'件', "jian"}, {'状', "zhuang"}, {'态', "tai"},
            {'值', "zhi"}, {'类', "lei"}, {'型', "xing"}, {'变', "bian"}, {'量', "liang"},
            {'参', "can"}, {'数', "shu"}, {'返', "fan"}, {'回', "hui"}, {'执', "zhi"},
            {'行', "xing"}, {'返', "fan"}, {'回', "hui"}, {'返', "fan"}, {'回', "hui"},
            {'跳', "tiao"}, {'转', "zhuan"}, {'向', "xiang"}, {'定', "ding"}, {'位', "wei"},
            {'置', "zhi"}, {'锚', "mao"}, {'点', "dian"}, {'按', "an"}, {'键', "jian"},
            {'盘', "pan"}, {'鼠', "shu"}, {'标', "biao"}, {'光', "guang"}, {'标', "biao"},
            {'触', "chu"}, {'摸', "mo"}, {'屏', "ping"}, {'投', "tou"}, {'影', "ying"},
            {'灯', "deng"}, {'光', "guang"}, {'线', "xian"}, {'条', "tiao"}, {'形', "xing"},
            {'色', "se"}, {'彩', "cai"}, {'亮', "liang"}, {'暗', "an"}, {'清', "qing"},
            {'晰', "xi"}, {'模', "mo"}, {'糊', "hu"}, {'显', "xian"}, {'示', "shi"},
            {'器', "qi"}, {'设', "she"}, {'备', "bei"}, {'终', "zhong"}, {'端', "duan"},
            {'主', "zhu"}, {'机', "ji"}, {'客', "ke"}, {'户', "hu"}, {'端', "duan"},
            {'服', "fu"}, {'务', "wu"}, {'器', "qi"}, {'端', "duan"}, {'中', "zhong"},
            {'间', "jian"}, {'层', "ceng"}, {'前', "qian"}, {'后', "hou"}, {'端', "duan"},
            {'请', "qing"}, {'告', "gao"}, {'诉', "su"}, {'码', "ma"}, {'源', "yuan"},
            {'码', "ma"}, {'程', "cheng"}, {'序', "xu"}, {'脚', "jiao"}, {'本', "ben"},
            {'本', "ben"}, {'地', "di"}, {'远', "yuan"}, {'程', "cheng"}, {'本', "ben"},
            {'地', "di"}, {'云', "yun"}, {'端', "duan"}, {'本', "ben"}, {'地', "di"},
            {'云', "yun"}, {'端', "duan"}, {'本', "ben"}, {'地', "di"}, {'云', "yun"},
            {'端', "duan"}, {'本', "ben"}, {'地', "di"}, {'云', "yun"}, {'端', "duan"},
            {'崞', "guo"}, {'阳', "yang"}
        };

        public string GetPinyin(char c)
        {
            if (PinyinMap.TryGetValue(c, out var pinyin))
                return pinyin;
            return c.ToString();
        }

        public string GetPinyin(char c, bool tone) => GetPinyin(c);

        public string GetPinyin(string text, string separator)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var result = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                if (result.Length > 0 && separator != null)
                    result.Append(separator);
                result.Append(GetPinyin(c));
            }
            return result.ToString();
        }

        public string GetPinyin(string text, string separator, bool tone) => GetPinyin(text, separator);

        public char GetFirstLetter(char c)
        {
            var pinyin = GetPinyin(c);
            if (!string.IsNullOrEmpty(pinyin))
                return char.ToUpper(pinyin[0]);
            return c;
        }

        public string GetFirstLetter(string text, string separator)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var result = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                if (result.Length > 0 && separator != null)
                    result.Append(separator);
                var pinyin = GetPinyin(c);
                if (!string.IsNullOrEmpty(pinyin))
                    result.Append(char.ToUpper(pinyin[0]));
                else
                    result.Append(c);
            }
            return result.ToString();
        }
    }
}