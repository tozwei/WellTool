using System;
using System.Linq;
using WellTool.Core.Util;

namespace WellTool.Captcha.Generator
{
    public class RandomGenerator : AbstractGenerator
    {
        public RandomGenerator(int count) : base(count)
        {
        }

        public RandomGenerator(string baseStr, int length) : base(baseStr, length)
        {
        }

        public override string Generate()
        {
            return RandomUtil.RandomString(BaseStr, Length);
        }

        public override bool Verify(string code, string userInputCode)
        {
            if (!string.IsNullOrEmpty(userInputCode))
            {
                return string.Equals(code, userInputCode, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
