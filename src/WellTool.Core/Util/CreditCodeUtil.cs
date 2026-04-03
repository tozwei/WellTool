using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 统一社会信用代码工具类
    /// </summary>
    public static class CreditCodeUtil
    {
        private static readonly int[] Weights = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
        private static readonly char[] BaseCodes = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                                                     'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
                                                     'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 
                                                     'U', 'V', 'W', 'X', 'Y' };

        /// <summary>
        /// 验证统一社会信用代码是否有效
        /// </summary>
        public static bool IsValid(string creditCode)
        {
            if (string.IsNullOrEmpty(creditCode))
            {
                return false;
            }

            creditCode = creditCode.ToUpper();

            // 统一社会信用代码长度为18位
            if (creditCode.Length != 18)
            {
                return false;
            }

            // 第一位必须是大写字母
            if (!Regex.IsMatch(creditCode[0].ToString(), @"[A-Z]"))
            {
                return false;
            }

            // 验证前17位
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                char c = creditCode[i];
                int code = Array.IndexOf(BaseCodes, c);
                if (code < 0)
                {
                    return false;
                }
                sum += code * Weights[i];
            }

            // 计算校验位
            int remainder = sum % 31;
            char checkCode = remainder == 0 ? '0' : BaseCodes[30 - remainder];

            return creditCode[17] == checkCode;
        }

        /// <summary>
        /// 生成统一社会信用代码
        /// </summary>
        /// <param name="orgType">组织机构类型（大写字母）</param>
        /// <param name="regionCode">地区代码（6位数字）</param>
        /// <param name="orgCode">组织机构代码（8位）</param>
        public static string Generate(char orgType, string regionCode, string orgCode)
        {
            if (!Regex.IsMatch(orgType.ToString(), @"[A-Z]") || regionCode.Length != 6 || orgCode.Length != 8)
            {
                return null;
            }

            string codeWithoutCheck = orgType + regionCode + orgCode;
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                char c = codeWithoutCheck[i];
                int code = Array.IndexOf(BaseCodes, c);
                sum += code * Weights[i];
            }

            int remainder = sum % 31;
            char checkCode = remainder == 0 ? '0' : BaseCodes[30 - remainder];

            return codeWithoutCheck + checkCode;
        }

        /// <summary>
        /// 获取组织机构类型
        /// </summary>
        public static string GetOrgType(string creditCode)
        {
            if (!IsValid(creditCode))
            {
                return null;
            }

            var orgTypes = new System.Collections.Generic.Dictionary<char, string>
            {
                { '1', '个体工商户' },
                { '2', '企业' },
                { '3', '农民专业合作社' },
                { '4', '社会组织' },
                { '5', '律师事务所' },
                { '6', '会计师事务所' },
                { '7', '律师事务所（特殊普通合伙）' },
                { '8', '特殊普通合伙企业' },
                { '9', '普通合伙企业' },
                { 'A', '机关' },
                { 'B', '事业单位' },
                { 'C', '军队单位' },
                { 'D', '武警部队' },
                { 'E', '外国在华设立机构' },
                { 'F', '国际组织驻华机构' },
                { 'G', '宗教组织' },
                { 'Y', '其他' }
            };

            return orgTypes.TryGetValue(creditCode[0], out var orgType) ? orgType : null;
        }

        /// <summary>
        /// 获取地区代码
        /// </summary>
        public static string GetRegionCode(string creditCode)
        {
            if (!IsValid(creditCode))
            {
                return null;
            }

            return creditCode.Substring(1, 6);
        }
    }
}
