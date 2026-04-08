using WellTool.Core.Text;

namespace TestStrFormatter;

class Program
{
    static void Main(string[] args)
    {
        // 测试 Format 方法
        var result1 = StrFormatter.Format("Hello {0}", "World");
        Console.WriteLine($"Format(\"Hello {{0}}\", \"World\"): {result1}");
        Console.WriteLine($"Expected: Hello World");
        Console.WriteLine($"Actual:   {result1}");
        Console.WriteLine($"Pass: {result1 == "Hello World"}");
        Console.WriteLine();

        // 测试 FormatWithBraces 方法
        var result2 = StrFormatter.Format("{{0}}", 123);
        Console.WriteLine($"Format(\"{{{0}}}\", 123): {result2}");
        Console.WriteLine($"Expected: {{0}}");
        Console.WriteLine($"Actual:   {result2}");
        Console.WriteLine($"Pass: {result2 == "{0}"}");
        Console.WriteLine();

        // 测试 FormatMultiple 方法
        var result3 = StrFormatter.Format("{0} + {1} = {2}", 1, 2, 3);
        Console.WriteLine($"Format(\"{{0}} + {{1}} = {{2}}\", 1, 2, 3): {result3}");
        Console.WriteLine($"Expected: 1 + 2 = 3");
        Console.WriteLine($"Actual:   {result3}");
        Console.WriteLine($"Pass: {result3 == "1 + 2 = 3"}");
        Console.WriteLine();

        // 测试 string.Format 直接调用
        var result4 = string.Format("Hello {0}", "World");
        Console.WriteLine($"string.Format(\"Hello {{0}}\", \"World\"): {result4}");
        Console.WriteLine($"Expected: Hello World");
        Console.WriteLine($"Actual:   {result4}");
        Console.WriteLine($"Pass: {result4 == "Hello World"}");
        Console.WriteLine();

        // 测试 string.Format 直接调用（双大括号）
        var result5 = string.Format("{{0}}", 123);
        Console.WriteLine($"string.Format(\"{{{0}}}\", 123): {result5}");
        Console.WriteLine($"Expected: {{0}}");
        Console.WriteLine($"Actual:   {result5}");
        Console.WriteLine($"Pass: {result5 == "{0}"}");
        Console.WriteLine();
    }
}