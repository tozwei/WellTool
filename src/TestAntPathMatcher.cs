using WellTool.Core.Text;

namespace TestAntPathMatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var antPathMatcher = new AntPathMatcher();
            
            // 测试用例 1: 基本路径匹配
            Console.WriteLine("Test 1: Basic path matching");
            var matched1 = antPathMatcher.Match("/api/org/organization/{orgId}", "/api/org/organization/999");
            Console.WriteLine($"/api/org/organization/{{orgId}} matches /api/org/organization/999: {matched1}");
            
            // 测试用例 2: ** 通配符
            Console.WriteLine("\nTest 2: ** wildcard");
            var pattern2 = "/**/*.xml*";
            var path2 = "/WEB-INF/web.xml";
            var matched2 = antPathMatcher.Match(pattern2, path2);
            Console.WriteLine($"{pattern2} matches {path2}: {matched2}");
            
            var pattern3 = "org/codelabor/*/**/*Service";
            var path3 = "org/codelabor/example/HelloWorldService";
            var matched3 = antPathMatcher.Match(pattern3, path3);
            Console.WriteLine($"{pattern3} matches {path3}: {matched3}");
            
            var pattern4 = "org/codelabor/*/**/*Service?";
            var path4 = "org/codelabor/example/HelloWorldServices";
            var matched4 = antPathMatcher.Match(pattern4, path4);
            Console.WriteLine($"{pattern4} matches {path4}: {matched4}");
            
            // 测试用例 3: 其他通配符
            Console.WriteLine("\nTest 3: Other wildcards");
            var pathMatcher = new AntPathMatcher();
            pathMatcher.CachePatterns = true;
            pathMatcher.CaseSensitive = true;
            pathMatcher.TrimTokens = true;
            
            Console.WriteLine($"a matches a: {pathMatcher.Match("a", "a")}");
            Console.WriteLine($"a* matches ab: {pathMatcher.Match("a*", "ab")}");
            Console.WriteLine($"a*/**/a matches ab/asdsa/a: {pathMatcher.Match("a*/**/a", "ab/asdsa/a")}");
            Console.WriteLine($"a*/**/a matches ab/asdsa/asdasd/a: {pathMatcher.Match("a*/**/a", "ab/asdsa/asdasd/a")}");
            Console.WriteLine($"* matches a: {pathMatcher.Match("*", "a")}");
            Console.WriteLine($"*/* matches a/a: {pathMatcher.Match("*/*", "a/a")}");
            
            // 测试用例 4: 精确匹配和通配符
            Console.WriteLine("\nTest 4: Exact matching and wildcards");
            Console.WriteLine($"/test matches /test: {pathMatcher.Match("/test", "/test")}");
            Console.WriteLine($"test matches /test: {pathMatcher.Match("test", "/test")}");
            Console.WriteLine($"t?st matches test: {pathMatcher.Match("t?st", "test")}");
            Console.WriteLine($"te?? matches test: {pathMatcher.Match("te??", "test")}");
            Console.WriteLine($"tes? matches tes: {pathMatcher.Match("tes?", "tes")}");
            Console.WriteLine($"tes? matches testt: {pathMatcher.Match("tes?", "testt")}");
            Console.WriteLine($"* matches test: {pathMatcher.Match("*", "test")}");
            Console.WriteLine($"test* matches test: {pathMatcher.Match("test*", "test")}");
            Console.WriteLine($"test/* matches test/Test: {pathMatcher.Match("test/*", "test/Test")}");
            Console.WriteLine($"*.* matches test.: {pathMatcher.Match("*.*", "test.")}");
            Console.WriteLine($"*.* matches test.test.test: {pathMatcher.Match("*.*", "test.test.test")}");
        }
    }
}