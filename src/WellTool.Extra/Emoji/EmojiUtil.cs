using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Extra.Emoji
{
    /// <summary>
    /// Emoji表情工具类
    /// </summary>
    public static class EmojiUtil
    {
        // Emoji正则表达式范围
        private static readonly Dictionary<string, string> EmojiAliases = new Dictionary<string, string>
        {
            { "smile", "😄" },
            { "laughing", "😆" },
            { "blush", "😊" },
            { "smiley", "😃" },
            { "relaxed", "😌" },
            { "smirk", "😏" },
            { "heart_eyes", "😍" },
            { "kissing_heart", "😘" },
            { "kissing_closed_eyes", "😚" },
            { "flushed", "😳" },
            { "relieved", "😌" },
            { "satisfied", "😆" },
            { "grin", "😁" },
            { "wink", "😉" },
            { "stuck_out_tongue_winking_eye", "😜" },
            { "stuck_out_tongue_closed_eyes", "😝" },
            { "grinning", "😀" },
            { "kissing", "😗" },
            { "kissing_smiling_eyes", "😙" },
            { "stuck_out_tongue", "😛" },
            { "sleeping", "😴" },
            { "worried", "😟" },
            { "frowning", "😦" },
            { "anguished", "😧" },
            { "open_mouth", "😮" },
            { "hushed", "😯" },
            { "tired_face", "😫" },
            { "cry", "😢" },
            { "sob", "😭" },
            { "joy", "😂" },
            { "つ", "😂" },
            { "roll", "🙄" },
            { "shit", "💩" },
            { "shitface", "💩" },
            { "poop", "💩" },
            { "hankey", "💩" },
            { "disappointed_relieved", "😥" },
            { "pensive", "😔" },
            { "confused", "😕" },
            { "ok_hand", "👌" },
            { "pray", "🙏" },
            { "clap", "👏" },
            { "muscle", "💪" },
            { "v", "✌️" },
            { "writing_hand", "✍️" },
            { "+1", "👍" },
            { "thumbsup", "👍" },
            { "-1", "👎" },
            { "thumbsdown", "👎" },
            { "point_right", "👉" },
            { "point_left", "👈" },
            { "point_up_2", "👆" },
            { "point_down", "👇" },
            { "eyes", "👀" },
            { "hear_no_evil", "🙉" },
            { "see_no_evil", "🙈" },
            { "speak_no_evil", "🙊" },
            { "warning", "⚠️" },
            { "x", "❌" },
            { "heavy_check_mark", "✔️" },
            { "heavy_multiplication_x", "✖️" },
            { "star", "⭐" },
            { "star2", "🌟" },
            { "glow", "✨" },
            { "sparkles", "✨" },
            { "collision", "💥" },
            { "dizzy", "💫" },
            { "anger", "💢" },
            { "sweat_drops", "💦" },
            { "droplet", "💧" },
            { "zzz", "💤" },
            { "bulb", "💡" },
            { "heart", "❤️" },
            { "yellow_heart", "💛" },
            { "green_heart", "💚" },
            { "blue_heart", "💙" },
            { "purple_heart", "💜" },
            { "black_heart", "🖤" },
            { "broken_heart", "💔" },
            { "heartbeat", "💓" },
            { "heartpulse", "💗" },
            { "cupid", "💘" },
            { "sparkling_heart", "💖" },
            { "mending_heart", "‍❤️‍‍" },
            { "heart_decoration", "💟" },
            { "gift_heart", "💝" },
            { "revolving_hearts", "💕" },
            { "two_hearts", "💕" },
            { "paw_prints", "🐾" },
            { "fire", "🔥" },
            { "100", "💯" },
            { "hash", "#️⃣" },
            { "keycap_star", "*️⃣" },
            { "zero", "0️⃣" },
            { "one", "1️⃣" },
            { "two", "2️⃣" },
            { "three", "3️⃣" },
            { "four", "4️⃣" },
            { "five", "5️⃣" },
            { "six", "6️⃣" },
            { "seven", "7️⃣" },
            { "eight", "8️⃣" },
            { "nine", "9️⃣" },
            { "ten", "🔟" }
        };

        // 正则匹配 :alias: 格式
        private static readonly Regex AliasPattern = new Regex(@":([a-zA-Z0-9_+-]+):", RegexOptions.Compiled);
        // 正则匹配 &#12345; 格式
        private static readonly Regex HtmlDecimalPattern = new Regex(@"&#(\d+);", RegexOptions.Compiled);
        // 正则匹配 &#x1F600; 格式
        private static readonly Regex HtmlHexPattern = new Regex(@"&#x([0-9a-fA-F]+);", RegexOptions.Compiled);

        /// <summary>
        /// 是否为Emoji表情的Unicode符
        /// </summary>
        /// <param name="str">被测试的字符串</param>
        /// <returns>是否为Emoji表情的Unicode符</returns>
        public static bool IsEmoji(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return EmojiAliases.ContainsValue(str);
        }

        /// <summary>
        /// 是否包含Emoji表情的Unicode符
        /// </summary>
        /// <param name="str">被测试的字符串</param>
        /// <returns>是否包含Emoji表情的Unicode符</returns>
        public static bool ContainsEmoji(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return str.Any(c => EmojiAliases.ContainsValue(c.ToString()));
        }

        /// <summary>
        /// 通过别名获取Emoji
        /// </summary>
        /// <param name="alias">别名，例如"smile"</param>
        /// <returns>Emoji字符，如果找不到返回null</returns>
        public static string Get(string alias)
        {
            return EmojiAliases.TryGetValue(alias.ToLower(), out var emoji) ? emoji : null;
        }

        /// <summary>
        /// 将子串中的Emoji别名替换为Unicode Emoji符号
        /// 例如：:smile: 替换为 😄
        /// </summary>
        /// <param name="str">包含Emoji别名或者HTML表现形式的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ToUnicode(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            // 替换 :alias: 格式
            str = AliasPattern.Replace(str, match =>
            {
                var alias = match.Groups[1].Value;
                return Get(alias) ?? match.Value;
            });

            // 替换 &#12345; 格式
            str = HtmlDecimalPattern.Replace(str, match =>
            {
                try
                {
                    var codePoint = int.Parse(match.Groups[1].Value);
                    return char.ConvertFromUtf32(codePoint);
                }
                catch
                {
                    return match.Value;
                }
            });

            // 替换 &#x1F600; 格式
            str = HtmlHexPattern.Replace(str, match =>
            {
                try
                {
                    var codePoint = Convert.ToInt32(match.Groups[1].Value, 16);
                    return char.ConvertFromUtf32(codePoint);
                }
                catch
                {
                    return match.Value;
                }
            });

            return str;
        }

        /// <summary>
        /// 将字符串中的Unicode Emoji字符转换为别名表现形式
        /// </summary>
        /// <param name="str">包含Emoji Unicode字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ToAlias(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var sb = new StringBuilder(str);
            foreach (var kvp in EmojiAliases.OrderByDescending(x => x.Value.Length))
            {
                sb.Replace(kvp.Value, $":{kvp.Key}:");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串中的Unicode Emoji字符转换为HTML 16进制表现形式
        /// </summary>
        /// <param name="str">包含Emoji Unicode字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ToHtmlHex(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var sb = new StringBuilder();
            foreach (int codePoint in str)
            {
                if (codePoint > 0xFFFF)
                {
                    sb.Append($"&#x{codePoint:X};");
                }
                else
                {
                    sb.Append((char)codePoint);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串中的Unicode Emoji字符转换为HTML 10进制表现形式
        /// </summary>
        /// <param name="str">包含Emoji Unicode字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ToHtml(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var sb = new StringBuilder();
            foreach (int codePoint in str)
            {
                if (codePoint > 0xFFFF)
                {
                    sb.Append($"&#{codePoint};");
                }
                else
                {
                    sb.Append((char)codePoint);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 去除字符串中所有的Emoji Unicode字符
        /// </summary>
        /// <param name="str">包含Emoji字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string RemoveAllEmojis(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (!EmojiAliases.ContainsValue(c.ToString()))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 去除字符串中所有的Emoji Unicode字符
        /// </summary>
        /// <param name="str">包含Emoji字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string RemoveEmojis(string str)
        {
            return RemoveAllEmojis(str);
        }

        /// <summary>
        /// 将Emoji转换为字符串
        /// </summary>
        /// <param name="str">包含Emoji字符的字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string EmojiToString(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return ToAlias(str);
        }

        /// <summary>
        /// 将字符串中的Unicode Emoji字符转换为HTML 10进制表现形式
        /// </summary>
        /// <param name="str">包含Emoji Unicode字符的字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ToHtmlDecimal(string str)
        {
            return ToHtml(str);
        }

        /// <summary>
        /// 提取字符串中所有的Emoji Unicode
        /// </summary>
        /// <param name="str">包含Emoji字符的字符串</param>
        /// <returns>Emoji字符列表</returns>
        public static List<string> ExtractEmojis(string str)
        {
            if (string.IsNullOrEmpty(str)) return new List<string>();

            return str.Where(c => EmojiAliases.ContainsValue(c.ToString()))
                .Select(c => c.ToString())
                .Distinct()
                .ToList();
        }
    }
}
