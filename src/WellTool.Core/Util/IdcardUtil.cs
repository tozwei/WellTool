using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 身份证工具类
    /// </summary>
    public static class IdcardUtil
    {
        private static readonly int[] Weights = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        private static readonly char[] CheckCodes = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        /// <summary>
        /// 验证身份证号码是否有效
        /// </summary>
        public static bool IsValid(string idCard)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return false;
            }

            idCard = idCard.ToUpper();

            // 15位或18位
            if (!Regex.IsMatch(idCard, @"^\d{15}$|^\d{17}[\dXx]$"))
            {
                return false;
            }

            // 验证出生日期
            string birthDate;
            if (idCard.Length == 15)
            {
                birthDate = "19" + idCard.Substring(6, 6);
            }
            else
            {
                birthDate = idCard.Substring(6, 8);
            }

            if (!DateTime.TryParseExact(birthDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                return false;
            }

            // 18位身份证验证校验位
            if (idCard.Length == 18)
            {
                return ValidateCheckCode(idCard);
            }

            return true;
        }

        /// <summary>
        /// 验证校验位
        /// </summary>
        private static bool ValidateCheckCode(string idCard)
        {
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += (idCard[i] - '0') * Weights[i];
            }

            int mod = sum % 11;
            char checkCode = CheckCodes[mod];

            return checkCode == idCard[17];
        }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        public static DateTime? GetBirthDate(string idCard)
        {
            if (!IsValid(idCard))
            {
                return null;
            }

            string birthDate;
            if (idCard.Length == 15)
            {
                birthDate = "19" + idCard.Substring(6, 6);
            }
            else
            {
                birthDate = idCard.Substring(6, 8);
            }

            if (DateTime.TryParseExact(birthDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// 获取性别（1=男，2=女）
        /// </summary>
        public static int? GetGender(string idCard)
        {
            if (string.IsNullOrEmpty(idCard) || !Regex.IsMatch(idCard, @"^\d{15}$|^\d{17}[\dXx]$"))
            {
                return null;
            }

            int genderCode;
            if (idCard.Length == 15)
            {
                genderCode = idCard[14] - '0';
            }
            else
            {
                genderCode = idCard[16] - '0';
            }

            return genderCode % 2 == 0 ? 2 : 1;
        }

        /// <summary>
        /// 获取性别描述
        /// </summary>
        public static string GetGenderStr(string idCard)
        {
            var gender = GetGender(idCard);
            return gender == 1 ? "男" : gender == 2 ? "女" : "未知";
        }

        /// <summary>
        /// 获取年龄
        /// </summary>
        public static int? GetAge(string idCard)
        {
            var birthDate = GetBirthDate(idCard);
            if (birthDate == null)
            {
                return null;
            }

            var today = DateTime.Today;
            int age = today.Year - birthDate.Value.Year;

            if (today < birthDate.Value.AddYears(age))
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        public static string GetProvince(string idCard)
        {
            if (string.IsNullOrEmpty(idCard) || idCard.Length < 2)
            {
                return null;
            }

            var provinceCode = idCard.Substring(0, 2);

            var provinces = new System.Collections.Generic.Dictionary<string, string>
            {
                { "11", "北京市" }, { "12", "天津市" }, { "13", "河北省" }, { "14", "山西省" },
                { "15", "内蒙古自治区" }, { "21", "辽宁省" }, { "22", "吉林省" }, { "23", "黑龙江省" },
                { "31", "上海市" }, { "32", "江苏省" }, { "33", "浙江省" }, { "34", "安徽省" },
                { "35", "福建省" }, { "36", "江西省" }, { "37", "山东省" }, { "41", "河南省" },
                { "42", "湖北省" }, { "43", "湖南省" }, { "44", "广东省" }, { "45", "广西壮族自治区" },
                { "46", "海南省" }, { "50", "重庆市" }, { "51", "四川省" }, { "52", "贵州省" },
                { "53", "云南省" }, { "54", "西藏自治区" }, { "61", "陕西省" }, { "62", "甘肃省" },
                { "63", "青海省" }, { "64", "宁夏回族自治区" }, { "65", "新疆维吾尔自治区" },
                { "71", "台湾省" }, { "81", "香港特别行政区" }, { "82", "澳门特别行政区" }
            };

            return provinces.TryGetValue(provinceCode, out var province) ? province : null;
        }

        /// <summary>
        /// 验证15位身份证号码（支持带括号和星号的版本）
        /// </summary>
        /// <param name="idCard15">15位身份证号码</param>
        /// <returns>验证结果数组：[出生日期, 性别, 是否有效]</returns>
        public static string[] IsValidCard10(string idCard15)
        {
            if (string.IsNullOrEmpty(idCard15))
            {
                return new string[] { null, null, "false" };
            }

            // 提取纯数字部分
            string pureNumber = Regex.Replace(idCard15, @"[()（）*]", "");

            // 验证是否是15位数字
            if (!Regex.IsMatch(pureNumber, @"^\d{15}$"))
            {
                return new string[] { null, null, "false" };
            }

            // 提取出生日期 (6-12位)
            string birthDateStr = pureNumber.Substring(6, 6);
            string birthDate = "19" + birthDateStr.Substring(0, 2) + "-" + birthDateStr.Substring(2, 2) + "-" + birthDateStr.Substring(4, 2);

            // 提取性别 (第14位，奇数为男，偶数为女)
            int genderDigit = pureNumber[13] - '0';
            string gender = (genderDigit % 2 == 1) ? "male" : "female";

            // 15位身份证默认有效
            return new string[] { birthDate, gender, "true" };
        }
    }
}
