using System;
using WellTool.Core.Text;

namespace TestStrFormatterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 测试 Format 方法
            Console.WriteLine("Testing StrFormatter.Format with params object[] args:");
            var result1 = StrFormatter.Format("Hello {0}", "World");
            Console.WriteLine($"StrFormatter.Format(\"Hello {{0}}\", \"World\"): {result1}");
            Console.WriteLine($"Expected: Hello World");
            Console.WriteLine($"Pass: {result1 == "Hello World"}");
            Console.WriteLine();

            // 测试 FormatWithBraces 方法
            var result2 = StrFormatter.Format("{{0}}", 123);
            Console.WriteLine($"StrFormatter.Format(\"{{{0}}}\", 123): {result2}");
            Console.WriteLine($"Expected: {{0}}");
            Console.WriteLine($"Pass: {result2 == "{0}"}");
            Console.WriteLine();

            // 测试 FormatMultiple 方法
            var result3 = StrFormatter.Format("{0} + {1} = {2}", 1, 2, 3);
            Console.WriteLine($"StrFormatter.Format(\"{{0}} + {{1}} = {{2}}\", 1, 2, 3): {result3}");
            Console.WriteLine($"Expected: 1 + 2 = 3");
            Console.WriteLine($"Pass: {result3 == "1 + 2 = 3"}");
            Console.WriteLine();

            // 直接测试 string.Format
            Console.WriteLine("Testing string.Format directly:");
            var stringFormatResult1 = string.Format("Hello {0}", "World");
            Console.WriteLine($"string.Format(\"Hello {{0}}\", \"World\"): {stringFormatResult1}");
            Console.WriteLine($"Expected: Hello World");
            Console.WriteLine($"Pass: {stringFormatResult1 == "Hello World"}");
            Console.WriteLine();

            // 测试 string.Format 直接调用（双大括号）
            var stringFormatResult2 = string.Format("{{0}}", 123);
            Console.WriteLine($"string.Format(\"{{{0}}}\", 123): {stringFormatResult2}");
            Console.WriteLine($"Expected: {{0}}");
            Console.WriteLine($"Pass: {stringFormatResult2 == "{0}"}");
            Console.WriteLine();
        }
    }
}