using System;
using System.Text;
using WellTool.Core.Math;
using WellTool.Core.Util;

namespace WellTool.Captcha.Generator
{
    public class MathGenerator : CodeGenerator
    {
        private const string Operators = "+-*";
        private readonly int NumberLength;
        private readonly bool ResultHasNegativeNumber;

        public MathGenerator() : this(2, true)
        {
        }

        public MathGenerator(bool resultHasNegativeNumber) : this(2, resultHasNegativeNumber)
        {
        }

        public MathGenerator(int numberLength) : this(numberLength, true)
        {
        }

        public MathGenerator(int numberLength, bool resultHasNegativeNumber)
        {
            NumberLength = numberLength;
            ResultHasNegativeNumber = resultHasNegativeNumber;
        }

        public string Generate()
        {
            int limit = GetLimit();
            char op = RandomUtil.RandomChar(Operators.ToCharArray());
            int num1 = 0;
            int num2 = 0;

            num1 = RandomUtil.RandomInt(limit);

            if (!ResultHasNegativeNumber && op == '-')
            {
                num2 = num1 == 0 ? 0 : RandomUtil.RandomInt(0, num1);
            }
            else
            {
                num2 = RandomUtil.RandomInt(limit);
            }

            string number1 = num1.ToString();
            string number2 = num2.ToString();

            number1 = StrUtil.PadAfter(number1, NumberLength, ' ');
            number2 = StrUtil.PadAfter(number2, NumberLength, ' ');

            return new StringBuilder()
                .Append(number1)
                .Append(op)
                .Append(number2)
                .Append('=')
                .ToString();
        }

        public bool Verify(string code, string userInputCode)
        {
            int result;
            if (!int.TryParse(userInputCode, out result))
            {
                return false;
            }

            int calcResult = (int)Calculator.Conversion(code);
            return result == calcResult;
        }

        public int GetLength()
        {
            return NumberLength * 2 + 2;
        }

        private int GetLimit()
        {
            return int.Parse("1" + StrUtil.Repeat('0', NumberLength));
        }
    }
}
