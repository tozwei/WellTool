using Xunit;
using WellTool.Core.Util;
using System;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3516 测试 - 获取泛型类型参数
    /// https://github.com/chinabugotech/hutool/issues/3516
    /// </summary>
    public class Issue3516Test
    {
        /// <summary>
        /// 测试从实现接口的类中获取泛型参数
        /// </summary>
        [Fact]
        public void GetTypeArgumentTest()
        {
            var typeArgument = TypeUtil.GetTypeArgument(typeof(Demo), 0);
            Assert.Equal(typeof(B), typeArgument);
        }

        /// <summary>
        /// 测试获取泛型参数（默认index=0）
        /// </summary>
        [Fact]
        public void GetTypeArgumentDefaultTest()
        {
            var typeArgument = TypeUtil.GetTypeArgument(typeof(Demo));
            Assert.Equal(typeof(B), typeArgument);
        }

        /// <summary>
        /// 测试非泛型类返回null
        /// </summary>
        [Fact]
        public void GetTypeArgumentNonGenericTest()
        {
            var typeArgument = TypeUtil.GetTypeArgument(typeof(string));
            Assert.Null(typeArgument);
        }

        /// <summary>
        /// 测试GetTypeArguments方法
        /// </summary>
        [Fact]
        public void GetTypeArgumentsTest()
        {
            var typeArguments = TypeUtil.GetTypeArguments(typeof(Demo));
            Assert.NotNull(typeArguments);
            Assert.Single(typeArguments);
            Assert.Equal(typeof(B), typeArguments[0]);
        }

        #region Test Classes

        class A
        {
            public string Name { get; set; }
        }

        class B
        {
            public string Name { get; set; }
        }

        interface IConverter<TInput, TOutput>
        {
            TOutput Convert(TInput input);
        }

        class Demo : IConverter<B, A>
        {
            public A Convert(B input)
            {
                var a = new A();
                return a;
            }
        }

        #endregion
    }
}