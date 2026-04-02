using Xunit;

namespace WellTool.Captcha.Tests
{
    /// <summary>
    /// Generator 测试
    /// </summary>
    public class GeneratorTest
    {
        [Fact]
        public void TestRandomGenerator()
        {
            Generator.RandomGenerator randomGenerator = new Generator.RandomGenerator(4);
            string code = randomGenerator.Generate();
            
            Assert.NotNull(code);
            Assert.Equal(4, code.Length);
        }

        [Fact]
        public void TestRandomGeneratorLength()
        {
            for (int i = 3; i <= 8; i++)
            {
                Generator.RandomGenerator generator = new Generator.RandomGenerator(i);
                string code = generator.Generate();
                
                Assert.Equal(i, code.Length);
            }
        }

        [Fact]
        public void TestMathGenerator()
        {
            Generator.MathGenerator mathGenerator = new Generator.MathGenerator();
            string question = mathGenerator.Generate();
            
            Assert.NotNull(question);
            Assert.Contains("=", question);
        }
    }
}
