using Xunit;
using WellTool.Captcha.Generator;

namespace WellTool.Captcha.Tests
{
    public class GeneratorTests
    {
        [Fact]
        public void TestRandomGenerator()
        {
            var generator = new RandomGenerator(5);
            var code = generator.Generate();

            Assert.NotNull(code);
            Assert.Equal(5, code.Length);
        }

        [Fact]
        public void TestRandomGeneratorVerify()
        {
            var generator = new RandomGenerator(4);
            var code = generator.Generate();

            Assert.True(generator.Verify(code, code));
            Assert.True(generator.Verify(code, code.ToLower()));
            Assert.False(generator.Verify(code, "wrong"));
        }

        [Fact]
        public void TestMathGenerator()
        {
            var generator = new MathGenerator();
            var code = generator.Generate();

            Assert.NotNull(code);
            Assert.Contains("=", code);
            Assert.True(code.Contains("+") || code.Contains("-") || code.Contains("*"));
        }

        [Fact]
        public void TestMathGeneratorVerify()
        {
            var generator = new MathGenerator();
            var code = generator.Generate();

            int result;
            Assert.True(int.TryParse(code.Replace(" ", "").Split('=')[1], out result));

            Assert.True(generator.Verify(code, result.ToString()));
            Assert.False(generator.Verify(code, "wrong"));
        }
    }
}
