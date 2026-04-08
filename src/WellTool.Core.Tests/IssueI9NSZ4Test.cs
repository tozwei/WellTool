using Xunit;
using WellTool.Core;
using WellTool.Core.Util;
using System.Collections.Generic;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I9NSZ4 测试
    /// </summary>
    public class IssueI9NSZ4Test
    {
        [Fact]
        public void GetByTest()
        {
            // AnimalKindInZoo所有枚举结果的getMappedValue结果值中都无AnimalKind.DOG，返回null
            var by = EnumUtil.GetByOrNull<AnimalKindInZoo, AnimalKind?>(x => GetMappedValue(x), AnimalKind.DOG);
            Assert.Null(by);
        }

        [Fact]
        public void GetByTest2()
        {
            var by = EnumUtil.GetByOrNull<AnimalKindInZoo, AnimalKind?>(x => GetMappedValue(x), AnimalKind.BIRD);
            Assert.Equal(AnimalKindInZoo.BIRD, by);
        }

        private static AnimalKind? GetMappedValue(AnimalKindInZoo kind)
        {
            return kind switch
            {
                AnimalKindInZoo.CAT => AnimalKind.CAT,
                AnimalKindInZoo.BIRD => AnimalKind.BIRD,
                _ => null
            };
        }

        /// <summary>
        /// 动物类型
        /// </summary>
        public enum AnimalKind
        {
            /// <summary>
            /// 猫
            /// </summary>
            CAT,
            /// <summary>
            /// 狗
            /// </summary>
            DOG,
            /// <summary>
            /// 鸟
            /// </summary>
            BIRD
        }

        /// <summary>
        /// 动物园里的动物类型
        /// </summary>
        public enum AnimalKindInZoo
        {
            /// <summary>
            /// 猫
            /// </summary>
            CAT,
            /// <summary>
            /// 蛇
            /// </summary>
            SNAKE,
            /// <summary>
            /// 鸟
            /// </summary>
            BIRD
        }
    }
}
