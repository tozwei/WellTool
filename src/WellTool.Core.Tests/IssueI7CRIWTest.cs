using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I7CRIW 测试
    /// </summary>
    public class IssueI7CRIWTest
    {
        [Fact]
        public void GetTypeArgumentsTest()
        {
            // 无法从继承获取泛型，则从接口获取
            var type = TypeUtil.GetTypeArgument(typeof(C));
            Assert.Equal(typeof(string), type);

            // 继承和第一个接口都非泛型接口，则从找到的第一个泛型接口获取
            type = TypeUtil.GetTypeArgument(typeof(D));
            Assert.Equal(typeof(string), type);
        }

        class A
        {
        }

        class AT<T>
        {
        }

        interface Face1<T>
        {
        }

        interface Face2
        {
        }

        class B : A
        {
        }

        class C : A, Face1<string>
        {
        }

        class D : A, Face2, Face1<string>
        {
        }
    }
}
