using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 脱敏工具类
    /// </summary>
    public static class DesensitizedUtil
    {
        /// <summary>
        /// 邮箱脱敏
        /// </summary>
        public static string Email(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return email;
            }

            var parts = email.Split('@');
            if (parts.Length != 2)
            {
                return email;
            }

            var name = parts[0];
            var domain = parts[1];

            if (name.Length <= 2)
            {
                return name[0] + "***@" + domain;
            }

            return name.Substring(0, 2) + "***@" + domain;
        }

        /// <summary>
        /// 手机号脱敏
        /// </summary>
        public static string Mobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return mobile;
            }

            if (mobile.Length < 7)
            {
                return mobile;
            }

            return mobile.Substring(0, 3) + "****" + mobile.Substring(mobile.Length - 4);
        }

        /// <summary>
        /// 身份证号脱敏
        /// </summary>
        public static string IdCard(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return idCard;
            }

            if (idCard.Length < 10)
            {
                return idCard;
            }

            return idCard.Substring(0, 4) + "**********" + idCard.Substring(idCard.Length - 4);
        }

        /// <summary>
        /// 姓名脱敏
        /// </summary>
        public static string ChineseName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            if (name.Length == 1)
            {
                return name;
            }

            if (name.Length == 2)
            {
                return name[0] + "*";
            }

            return name.Substring(0, 1) + new string('*', name.Length - 1);
        }

        /// <summary>
        /// 银行卡号脱敏
        /// </summary>
        public static string BankCard(string cardNo)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                return cardNo;
            }

            if (cardNo.Length < 8)
            {
                return cardNo;
            }

            return cardNo.Substring(0, 4) + "****" + cardNo.Substring(cardNo.Length - 4);
        }

        /// <summary>
        /// 密码脱敏
        /// </summary>
        public static string Password(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return password;
            }

            return "******";
        }

        /// <summary>
        /// 地址脱敏
        /// </summary>
        public static string Address(string address, int showLength = 4)
        {
            if (string.IsNullOrEmpty(address))
            {
                return address;
            }

            if (address.Length <= showLength)
            {
                return address;
            }

            return address.Substring(0, showLength) + new string('*', Math.Min(address.Length - showLength, 10));
        }

        /// <summary>
        /// 通用脱敏方法
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="prefixShowLength">前缀显示长度</param>
        /// <param name="suffixShowLength">后缀显示长度</param>
        /// <param name="mask">掩码字符</param>
        public static string Desensitize(string str, int prefixShowLength = 0, int suffixShowLength = 0, char mask = '*')
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            int length = str.Length;
            if (length <= prefixShowLength + suffixShowLength)
            {
                return str;
            }

            var prefix = prefixShowLength > 0 ? str.Substring(0, prefixShowLength) : "";
            var suffix = suffixShowLength > 0 ? str.Substring(length - suffixShowLength) : "";
            var maskLength = length - prefixShowLength - suffixShowLength;

            return prefix + new string(mask, Math.Min(maskLength, 8)) + suffix;
        }
    }
}
