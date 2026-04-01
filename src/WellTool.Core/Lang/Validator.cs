using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 验证器工具类
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// 验证是否为有效的邮箱地址
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns>是否有效</returns>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// 验证是否为有效的手机号码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>是否有效</returns>
        public static bool IsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            var pattern = @"1[3-9]\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }

        /// <summary>
        /// 验证是否为有效的身份证号码
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <returns>是否有效</returns>
        public static bool IsIdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return false;
            }
            var pattern = @"[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}[\dXx]";
            return Regex.IsMatch(idCard, pattern);
        }

        /// <summary>
        /// 验证是否为有效的URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>是否有效</returns>
        public static bool IsUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            var pattern = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$";
            return Regex.IsMatch(url, pattern);
        }

        /// <summary>
        /// 验证是否为有效的IP地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否有效</returns>
        public static bool IsIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }
            return System.Net.IPAddress.TryParse(ip, out _);
        }

        /// <summary>
        /// 验证是否为有效的日期
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return false;
            }
            return DateTime.TryParse(date, out _);
        }

        /// <summary>
        /// 验证是否为有效的数字
        /// </summary>
        /// <param name="number">数字字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            return double.TryParse(number, out _);
        }

        /// <summary>
        /// 验证是否为有效的整数
        /// </summary>
        /// <param name="integer">整数字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsInteger(string integer)
        {
            if (string.IsNullOrEmpty(integer))
            {
                return false;
            }
            return int.TryParse(integer, out _);
        }

        /// <summary>
        /// 验证是否为有效的小数
        /// </summary>
        /// <param name="decimal">小数字符串</param>
        /// <returns>是否有效</returns>
        public static bool IsDecimal(string @decimal)
        {
            if (string.IsNullOrEmpty(@decimal))
            {
                return false;
            }
            return decimal.TryParse(@decimal, out _);
        }

        /// <summary>
        /// 验证是否为有效的密码
        /// 密码长度至少8位，包含至少一个大写字母、一个小写字母和一个数字
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>是否有效</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                return false;
            }
            var hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            var hasLowerCase = Regex.IsMatch(password, "[a-z]");
            var hasNumber = Regex.IsMatch(password, "[0-9]");
            return hasUpperCase && hasLowerCase && hasNumber;
        }
    }
}