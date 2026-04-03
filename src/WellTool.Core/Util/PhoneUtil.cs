using System;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 手机号工具类
    /// </summary>
    public static class PhoneUtil
    {
        /// <summary>
        /// 验证手机号是否有效（中国大陆）
        /// </summary>
        public static bool IsValid(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }

            // 移除空格和短横线
            phone = phone.Replace(" ", "").Replace("-", "");

            // 中国大陆手机号正则：1[3-9]\d{9}
            if (phone.Length == 11 && phone[0] == '1' && phone[1] >= '3' && phone[1] <= '9')
            {
                foreach (char c in phone.Substring(2))
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取手机号运营商
        /// </summary>
        public static string GetOperator(string phone)
        {
            if (!IsValid(phone))
            {
                return null;
            }

            // 移除空格和短横线
            phone = phone.Replace(" ", "").Replace("-", "");

            var prefix = phone.Substring(0, 3);
            
            // 中国移动：134-139, 144, 147, 148, 150-152, 157, 158, 159, 172, 178, 182, 183, 184, 187, 188, 195, 197, 198
            string[] cmcc = { "134", "135", "136", "137", "138", "139", "144", "147", "148", "150", "151", "152", 
                              "157", "158", "159", "172", "178", "182", "183", "184", "187", "188", "195", "197", "198" };
            
            // 中国联通：130, 131, 132, 140, 145, 146, 155, 156, 166, 167, 171, 175, 176, 185, 186, 196
            string[] cucc = { "130", "131", "132", "140", "145", "146", "155", "156", "166", "167", 
                              "171", "175", "176", "185", "186", "196" };
            
            // 中国电信：133, 134-139, 141, 149, 153, 154, 174, 177, 180, 181, 189, 190, 191, 193, 199
            string[] ctcc = { "133", "134", "135", "136", "137", "138", "139", "141", "149", "153", 
                              "154", "174", "177", "180", "181", "189", "190", "191", "193", "199" };

            foreach (var p in cmcc)
            {
                if (prefix.StartsWith(p))
                {
                    return "中国移动";
                }
            }

            foreach (var p in cucc)
            {
                if (prefix.StartsWith(p))
                {
                    return "中国联通";
                }
            }

            foreach (var p in ctcc)
            {
                if (prefix.StartsWith(p))
                {
                    return "中国电信";
                }
            }

            return "未知运营商";
        }

        /// <summary>
        /// 脱敏手机号
        /// </summary>
        public static string Mask(string phone)
        {
            if (!IsValid(phone))
            {
                return phone;
            }

            phone = phone.Replace(" ", "").Replace("-", "");
            return phone.Substring(0, 3) + "****" + phone.Substring(7);
        }

        /// <summary>
        /// 获取手机号归属地
        /// </summary>
        public static string GetLocation(string phone)
        {
            if (!IsValid(phone))
            {
                return null;
            }

            // 这里可以接入第三方库或API获取归属地
            // 简化实现返回null
            return null;
        }
    }
}
