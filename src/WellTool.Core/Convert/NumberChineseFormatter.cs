using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 数字转中文类
    /// 包括：
    /// 1. 数字转中文大写形式，比如一百二十一
    /// 2. 数字转金额用的大写形式，比如：壹佰贰拾壹
    /// 3. 转金额形式，比如：壹佰贰拾壹整
    /// </summary>
    public static class NumberChineseFormatter
    {
        /// <summary>
        /// 中文形式，奇数位置是简体，偶数位置是记账繁体，0共用
        /// 使用混合数组提高效率和数组复用
        /// </summary>
        private static readonly char[] DIGITS = { '零', '一', '壹', '二', '贰', '三', '叁', '四', '肆', '五', '伍', '六', '陆', '七', '柒', '八', '捌', '九', '玖' };

        /// <summary>
        /// 汉字转阿拉伯数字的
        /// </summary>
        private static readonly ChineseUnit[] CHINESE_NAME_VALUE = {
            new ChineseUnit(' ', 1, false),
            new ChineseUnit('十', 10, false),
            new ChineseUnit('拾', 10, false),
            new ChineseUnit('百', 100, false),
            new ChineseUnit('佰', 100, false),
            new ChineseUnit('千', 1000, false),
            new ChineseUnit('仟', 1000, false),
            new ChineseUnit('万', 10000, true),
            new ChineseUnit('亿', 100000000, true),
        };

        /// <summary>
        /// 口语化映射
        /// </summary>
        private static readonly Dictionary<string, string> COLLOQUIAL_WORDS = new Dictionary<string, string>
        {
            { "一十", "十" },
            { "一拾", "拾" },
            { "负一十", "负十" },
            { "负一拾", "负拾" }
        };

        /// <summary>
        /// 阿拉伯数字转换成中文,小数点后四舍五入保留两位. 使用于整数、小数的转换.
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <returns>中文</returns>
        public static string Format(double amount, bool isUseTraditional)
        {
            return Format(amount, isUseTraditional, false);
        }

        /// <summary>
        /// 阿拉伯数字转换成中文
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <param name="isMoneyMode">是否金额模式</param>
        /// <returns>中文</returns>
        public static string Format(double amount, bool isUseTraditional, bool isMoneyMode)
        {
            return Format(amount, isUseTraditional, isMoneyMode, "负", "元");
        }

        /// <summary>
        /// 阿拉伯数字转换成中文
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <param name="isMoneyMode">是否金额模式</param>
        /// <param name="negativeName">负号转换名称 如：负、(负数)</param>
        /// <param name="unitName">单位名称 如：元、圆</param>
        /// <returns>中文</returns>
        public static string Format(double amount, bool isUseTraditional, bool isMoneyMode, string negativeName, string unitName)
        {
            if (string.IsNullOrEmpty(unitName))
            {
                unitName = "元";
            }

            if (amount == 0)
            {
                return isMoneyMode ? "零" + unitName + "整" : "零";
            }

            if (amount < -99999999999999.99 || amount > 99999999999999.99)
            {
                throw new ArgumentException("Number support only: (-99999999999999.99 ~ 99999999999999.99)！");
            }

            var chineseStr = new System.Text.StringBuilder();

            // 负数
            if (amount < 0)
            {
                chineseStr.Append(string.IsNullOrEmpty(negativeName) ? "负" : negativeName);
                amount = -amount;
            }

            long yuan = (long)Math.Round(amount * 100);
            var fen = (int)(yuan % 10);
            yuan = yuan / 10;
            var jiao = (int)(yuan % 10);
            yuan = yuan / 10;

            // 元
            if (!isMoneyMode || yuan != 0)
            {
                chineseStr.Append(LongToChinese(yuan, isUseTraditional));
                if (isMoneyMode)
                {
                    chineseStr.Append(unitName);
                }
            }

            if (jiao == 0 && fen == 0)
            {
                //无小数部分的金额结尾
                if (isMoneyMode)
                {
                    chineseStr.Append("整");
                }
                return chineseStr.ToString();
            }

            // 小数部分
            if (!isMoneyMode)
            {
                chineseStr.Append("点");
            }

            // 角
            if (yuan == 0 && jiao == 0)
            {
                if (!isMoneyMode)
                {
                    chineseStr.Append("零");
                }
            }
            else
            {
                chineseStr.Append(NumberToChinese(jiao, isUseTraditional));
                if (isMoneyMode && jiao != 0)
                {
                    chineseStr.Append("角");
                }
            }

            // 分
            if (fen != 0)
            {
                chineseStr.Append(NumberToChinese(fen, isUseTraditional));
                if (isMoneyMode)
                {
                    chineseStr.Append("分");
                }
            }

            return chineseStr.ToString();
        }

        /// <summary>
        /// 阿拉伯数字（支持正负整数）转换成中文
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <returns>中文</returns>
        public static string Format(long amount, bool isUseTraditional)
        {
            if (amount == 0)
            {
                return "零";
            }

            if (amount < -99999999999999.99 || amount > 99999999999999.99)
            {
                throw new ArgumentException("Number support only: (-99999999999999.99 ~ 99999999999999.99)！");
            }

            var chineseStr = new System.Text.StringBuilder();

            // 负数
            if (amount < 0)
            {
                chineseStr.Append("负");
                amount = -amount;
            }

            chineseStr.Append(LongToChinese(amount, isUseTraditional));

            return chineseStr.ToString();
        }

        /// <summary>
        /// 阿拉伯数字（支持正负整数）四舍五入后转换成中文节权位简洁计数单位，例如 -5555 =》 -5.56万
        /// </summary>
        /// <param name="amount">数字</param>
        /// <returns>中文</returns>
        public static string FormatSimple(long amount)
        {
            if (amount < 10000 && amount > -10000)
            {
                return amount.ToString();
            }

            string res;
            if (amount < 100000000 && amount > -100000000)
            {
                res = Math.Round(amount / 10000.0, 2).ToString("0.##") + "万";
            }
            else if (amount < 1000000000000L && amount > -1000000000000L)
            {
                res = Math.Round(amount / 100000000.0, 2).ToString("0.##") + "亿";
            }
            else
            {
                res = Math.Round(amount / 1000000000000.0, 2).ToString("0.##") + "万亿";
            }

            return res;
        }

        /// <summary>
        /// 格式化-999~999之间的数字
        /// 这个方法显示10~19以下的数字时使用"十一"而非"一十一"。
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <returns>中文</returns>
        public static string FormatThousand(int amount, bool isUseTraditional)
        {
            if (amount < -999 || amount > 999)
            {
                throw new ArgumentException("Number support only: (-999 ~ 999)！");
            }

            var chinese = ThousandToChinese(amount, isUseTraditional);
            if (amount < 20 && amount >= 10)
            {
                // "十一"而非"一十一"
                return chinese.Substring(1);
            }

            return chinese;
        }

        /// <summary>
        /// 阿拉伯数字转换成中文. 使用于整数、小数的转换.
        /// 支持多位小数
        /// </summary>
        /// <param name="amount">数字</param>
        /// <param name="isUseTraditional">是否使用繁体</param>
        /// <param name="isUseColloquial">是否使用口语化(e.g. 一十 -》 十)</param>
        /// <returns>中文</returns>
        public static string Format(decimal amount, bool isUseTraditional, bool isUseColloquial)
        {
            string formatAmount;
            var decimalPlaces = amount.ToString().IndexOf('.');
            if (decimalPlaces < 0)
            {
                formatAmount = Format((long)amount, isUseTraditional);
            }
            else
            {
                var parts = amount.ToString().Split('.');
                var decimalPartStr = new System.Text.StringBuilder();
                foreach (var decimalChar in parts[1].ToCharArray())
                {
                    decimalPartStr.Append(NumberCharToChinese(decimalChar, isUseTraditional));
                }
                formatAmount = Format((long)amount, isUseTraditional) + "点" + decimalPartStr;
            }

            if (isUseColloquial)
            {
                foreach (var colloquialWord in COLLOQUIAL_WORDS)
                {
                    if (formatAmount.StartsWith(colloquialWord.Key))
                    {
                        formatAmount = formatAmount.Replace(colloquialWord.Key, colloquialWord.Value, StringComparison.Ordinal);
                        break;
                    }
                }
            }

            return formatAmount;
        }

        /// <summary>
        /// 数字字符转中文，非数字字符原样返回
        /// </summary>
        /// <param name="c">数字字符</param>
        /// <param name="isUseTraditional">是否繁体</param>
        /// <returns>中文字符</returns>
        public static string NumberCharToChinese(char c, bool isUseTraditional)
        {
            if (c < '0' || c > '9')
            {
                return c.ToString();
            }
            return NumberToChinese(c - '0', isUseTraditional).ToString();
        }

        /// <summary>
        /// 中文大写数字金额转换为数字，返回结果以元为单位的BigDecimal类型数字
        /// 如：
        /// "陆万柒仟伍佰伍拾陆元叁角贰分"返回"67556.32"
        /// "叁角贰分"返回"0.32"
        /// </summary>
        /// <param name="chineseMoneyAmount">中文大写数字金额</param>
        /// <returns>返回结果以元为单位的decimal类型数字</returns>
        public static decimal? ChineseMoneyToNumber(string chineseMoneyAmount)
        {
            if (string.IsNullOrWhiteSpace(chineseMoneyAmount))
            {
                return null;
            }

            int yi = chineseMoneyAmount.IndexOf("元");
            if (yi == -1)
            {
                yi = chineseMoneyAmount.IndexOf("圆");
            }
            var ji = chineseMoneyAmount.IndexOf("角");
            var fi = chineseMoneyAmount.IndexOf("分");

            // 先找到单位为元的数字
            string yStr = null;
            if (yi > 0)
            {
                yStr = chineseMoneyAmount.Substring(0, yi);
            }

            // 再找到单位为角的数字
            string jStr = null;
            if (ji > 0)
            {
                if (yi >= 0)
                {
                    if (ji > yi)
                    {
                        jStr = chineseMoneyAmount.Substring(yi + 1, ji - yi - 1);
                    }
                }
                else
                {
                    jStr = chineseMoneyAmount.Substring(0, ji);
                }
            }

            // 再找到单位为分的数字
            string fStr = null;
            if (fi > 0)
            {
                if (ji >= 0)
                {
                    if (fi > ji)
                    {
                        fStr = chineseMoneyAmount.Substring(ji + 1, fi - ji - 1);
                    }
                }
                else if (yi > 0)
                {
                    if (fi > yi)
                    {
                        fStr = chineseMoneyAmount.Substring(yi + 1, fi - yi - 1);
                    }
                }
                else
                {
                    fStr = chineseMoneyAmount.Substring(0, fi);
                }
            }

            //元、角、分
            int y = 0, j = 0, f = 0;
            if (!string.IsNullOrEmpty(yStr))
            {
                y = ChineseToNumber(yStr);
            }
            if (!string.IsNullOrEmpty(jStr))
            {
                j = ChineseToNumber(jStr);
            }
            if (!string.IsNullOrEmpty(fStr))
            {
                f = ChineseToNumber(fStr);
            }

            decimal amount = y;
            amount += Math.Round(j / 10.0m, 2);
            amount += Math.Round(f / 100.0m, 2);

            return amount;
        }

        /// <summary>
        /// 阿拉伯数字整数部分转换成中文，只支持正数
        /// </summary>
        private static string LongToChinese(long amount, bool isUseTraditional)
        {
            if (amount == 0)
            {
                return "零";
            }

            //将数字以万为单位分为多份
            var parts = new int[4];
            for (int i = 0; amount != 0; i++)
            {
                parts[i] = (int)(amount % 10000);
                amount = amount / 10000;
            }

            var chineseStr = new System.Text.StringBuilder();

            // 千
            var partValue = parts[0];
            if (partValue > 0)
            {
                var partChinese = ThousandToChinese(partValue, isUseTraditional);
                chineseStr.Insert(0, partChinese);

                if (partValue < 1000)
                {
                    AddPreZero(chineseStr);
                }
            }

            // 万
            partValue = parts[1];
            if (partValue > 0)
            {
                if (partValue % 10 == 0 && parts[0] > 0)
                {
                    AddPreZero(chineseStr);
                }
                var partChinese = ThousandToChinese(partValue, isUseTraditional);
                chineseStr.Insert(0, partChinese + "万");

                if (partValue < 1000)
                {
                    AddPreZero(chineseStr);
                }
            }
            else
            {
                AddPreZero(chineseStr);
            }

            // 亿
            partValue = parts[2];
            if (partValue > 0)
            {
                if (partValue % 10 == 0 && parts[1] > 0)
                {
                    AddPreZero(chineseStr);
                }
                var partChinese = ThousandToChinese(partValue, isUseTraditional);
                chineseStr.Insert(0, partChinese + "亿");

                if (partValue < 1000)
                {
                    AddPreZero(chineseStr);
                }
            }
            else
            {
                AddPreZero(chineseStr);
            }

            // 万亿
            partValue = parts[3];
            if (partValue > 0)
            {
                if (parts[2] == 0)
                {
                    chineseStr.Insert(0, "亿");
                }
                var partChinese = ThousandToChinese(partValue, isUseTraditional);
                chineseStr.Insert(0, partChinese + "万");
            }

            if (chineseStr.Length > 0 && chineseStr[0] == '零')
            {
                return chineseStr.ToString().Substring(1);
            }

            return chineseStr.ToString();
        }

        /// <summary>
        /// 把一个 0~9999 之间的整数转换为汉字的字符串，如果是 0 则返回 ""
        /// </summary>
        private static string ThousandToChinese(int amountPart, bool isUseTraditional)
        {
            if (amountPart == 0)
            {
                return DIGITS[0].ToString();
            }

            int temp = amountPart;
            var chineseStr = new System.Text.StringBuilder();
            bool lastIsZero = true;

            for (int i = 0; temp > 0; i++)
            {
                int digit = temp % 10;
                if (digit == 0)
                {
                    if (!lastIsZero)
                    {
                        chineseStr.Insert(0, "零");
                    }
                    lastIsZero = true;
                }
                else
                {
                    chineseStr.Insert(0, NumberToChinese(digit, isUseTraditional) + GetUnitName(i, isUseTraditional));
                    lastIsZero = false;
                }
                temp = temp / 10;
            }

            return chineseStr.ToString();
        }

        /// <summary>
        /// 把中文转换为数字 如 二百二十 220
        /// 一百一十二 -》 112
        /// 一千零一十二 -》 1012
        /// </summary>
        /// <param name="chinese">中文字符</param>
        /// <returns>数字</returns>
        public static int ChineseToNumber(string chinese)
        {
            var length = chinese.Length;
            int result = 0;
            int section = 0;
            int number = 0;
            ChineseUnit unit = null;

            for (int i = 0; i < length; i++)
            {
                char c = chinese[i];
                int num = ChineseToNumber(c);
                if (num >= 0)
                {
                    if (num == 0)
                    {
                        if (number > 0 && unit != null)
                        {
                            section += number * (unit.Value / 10);
                        }
                        unit = null;
                    }
                    else if (number > 0)
                    {
                        throw new ArgumentException($"Bad number '{chinese[i - 1]}{c}' at: {i}");
                    }
                    number = num;
                }
                else
                {
                    unit = ChineseToUnit(c);
                    if (unit == null)
                    {
                        throw new ArgumentException($"Unknown unit '{c}' at: {i}");
                    }

                    if (unit.SecUnit)
                    {
                        section = (section + number) * unit.Value;
                        result += section;
                        section = 0;
                    }
                    else
                    {
                        int unitNumber = number;
                        if (number == 0 && i == 0)
                        {
                            unitNumber = 1;
                        }
                        section += (unitNumber * unit.Value);
                    }
                    number = 0;
                }
            }

            if (number > 0 && unit != null)
            {
                number = number * (unit.Value / 10);
            }

            return result + section + number;
        }

        /// <summary>
        /// 查找对应的权对象
        /// </summary>
        private static ChineseUnit ChineseToUnit(char chinese)
        {
            foreach (var chineseNameValue in CHINESE_NAME_VALUE)
            {
                if (chineseNameValue.Name == chinese)
                {
                    return chineseNameValue;
                }
            }
            return null;
        }

        /// <summary>
        /// 将汉字单个数字转换为int类型数字
        /// </summary>
        private static int ChineseToNumber(char chinese)
        {
            if (chinese == '两')
            {
                chinese = '二';
            }

            for (int i = 0; i < DIGITS.Length; i++)
            {
                if (DIGITS[i] == chinese)
                {
                    return i > 0 ? (i + 1) / 2 : i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 单个数字转汉字
        /// </summary>
        private static char NumberToChinese(int number, bool isUseTraditional)
        {
            if (number == 0)
            {
                return DIGITS[0];
            }
            return DIGITS[number * 2 - (isUseTraditional ? 0 : 1)];
        }

        /// <summary>
        /// 获取对应级别的单位
        /// </summary>
        private static string GetUnitName(int index, bool isUseTraditional)
        {
            if (index == 0)
            {
                return "";
            }
            return CHINESE_NAME_VALUE[index * 2 - (isUseTraditional ? 0 : 1)].Name.ToString();
        }

        private static void AddPreZero(System.Text.StringBuilder chineseStr)
        {
            if (chineseStr.Length == 0)
            {
                return;
            }
            var c = chineseStr[0];
            if (c != '零')
            {
                chineseStr.Insert(0, '零');
            }
        }

        /// <summary>
        /// 权位
        /// </summary>
        private class ChineseUnit
        {
            /// <summary>
            /// 中文权名称
            /// </summary>
            public char Name { get; }
            /// <summary>
            /// 10的倍数值
            /// </summary>
            public int Value { get; }
            /// <summary>
            /// 是否为节权位，它不是与之相邻的数字的倍数，而是整个小节的倍数。
            /// </summary>
            public bool SecUnit { get; }

            public ChineseUnit(char name, int value, bool secUnit)
            {
                Name = name;
                Value = value;
                SecUnit = secUnit;
            }
        }
    }
}