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

            // 移除空格和短横线
            phone = phone.Replace(" ", "").Replace("-", "");

            // 获取手机号前7位
            var prefix = phone.Substring(0, 7);

            // 这里使用简单的映射表，实际项目中可以接入第三方库或API获取归属地
            // 以下是一些常见的手机号前缀归属地映射
            var locationMap = new Dictionary<string, string>
            {
                { "1380013", "北京市" },
                { "1390013", "北京市" },
                { "1380021", "上海市" },
                { "1390021", "上海市" },
                { "1380051", "江苏省南京市" },
                { "1390051", "江苏省南京市" },
                { "1380073", "湖南省长沙市" },
                { "1390073", "湖南省长沙市" },
                { "1380081", "四川省成都市" },
                { "1390081", "四川省成都市" },
                { "1380091", "陕西省西安市" },
                { "1390091", "陕西省西安市" }
            };

            // 尝试匹配前7位
            if (locationMap.TryGetValue(prefix, out var location))
            {
                return location;
            }

            // 尝试匹配前3位
            var threePrefix = phone.Substring(0, 3);
            var threePrefixMap = new Dictionary<string, string>
            {
                { "134", "中国" },
                { "135", "中国" },
                { "136", "中国" },
                { "137", "中国" },
                { "138", "中国" },
                { "139", "中国" },
                { "147", "中国" },
                { "150", "中国" },
                { "151", "中国" },
                { "152", "中国" },
                { "157", "中国" },
                { "158", "中国" },
                { "159", "中国" },
                { "178", "中国" },
                { "182", "中国" },
                { "183", "中国" },
                { "184", "中国" },
                { "187", "中国" },
                { "188", "中国" },
                { "198", "中国" }
            };

            if (threePrefixMap.TryGetValue(threePrefix, out var country))
            {
                return country;
            }

            return "未知归属地";
        }
    }
}
