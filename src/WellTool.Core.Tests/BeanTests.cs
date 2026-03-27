using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WellTool.Core.Bean;
using WellTool.Core.Collection;
using WellTool.Core.Date;
using WellTool.Core.Lang;
using WellTool.Core.Map;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Bean工具单元测试
    /// </summary>
    public class BeanTests
    {
        [Fact]
        public void IsBeanTest()
        {
            // HashMap不包含setXXX方法，不是bean
            bool isBean = BeanUtil.IsBean(typeof(Dictionary<string, object>));
            Assert.False(isBean);
        }

        [Fact]
        public void FillBeanTest()
        {
            var person = BeanUtil.FillBean(new Person(), new TestValueProvider(), new CopyOptions());

            Assert.Equal("张三", person.Name);
            Assert.Equal(18, person.Age);
        }

        [Fact]
        public void FillBeanWithMapIgnoreCaseTest()
        {
            var map = new Dictionary<string, object>
            {
                { "Name", "Joe" },
                { "aGe", 12 },
                { "openId", "DFDFSDFWERWER" }
            };
            var person = BeanUtil.FillBeanWithMapIgnoreCase(map, new SubPerson(), false);
            Assert.Equal("Joe", person.Name);
            Assert.Equal(12, person.Age);
            Assert.Equal("DFDFSDFWERWER", person.Openid);
        }

        [Fact]
        public void ToBeanTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var map = BeanUtil.ToBean(person, typeof(Dictionary<string, object>)) as Dictionary<string, object>;
            Assert.NotNull(map);
            Assert.Equal("测试A11", map["name"]);
            Assert.Equal(14, map["age"]);
            Assert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            Assert.False(map.ContainsKey("SUBNAME"));
        }

        /// <summary>
        /// 忽略转换错误测试
        /// </summary>
        [Fact]
        public void ToBeanIgnoreErrorTest()
        {
            var map = new Dictionary<string, object>
            {
                { "name", "Joe" },
                // 错误的类型，此处忽略
                { "age", "aaaaaa" }
            };

            var person = BeanUtil.ToBeanIgnoreError(map, typeof(Person)) as Person;
            Assert.NotNull(person);
            Assert.Equal("Joe", person.Name);
            // 错误的类型，不copy这个字段，使用对象创建的默认值
            Assert.Equal(0, person.Age);
        }

        [Fact]
        public void MapToBeanIgnoreCaseTest()
        {
            var map = new Dictionary<string, object>
            {
                { "Name", "Joe" },
                { "aGe", 12 }
            };

            var person = BeanUtil.ToBeanIgnoreCase(map, typeof(Person), false) as Person;
            Assert.NotNull(person);
            Assert.Equal("Joe", person.Name);
            Assert.Equal(12, person.Age);
        }

        [Fact]
        public void MapToBeanTest()
        {
            var map = new Dictionary<string, object>
            {
                { "a_name", "Joe" },
                { "b_age", 12 }
            };

            // 别名，用于对应bean的字段名
            var mapping = new Dictionary<string, string>
            {
                { "a_name", "name" },
                { "b_age", "age" }
            };

            var person = BeanUtil.ToBean(map, typeof(Person), CopyOptions.Create().SetFieldMapping(mapping)) as Person;
            Assert.NotNull(person);
            Assert.Equal("Joe", person.Name);
            Assert.Equal(12, person.Age);
        }

        /// <summary>
        /// 测试public类型的字段注入是否成功
        /// </summary>
        [Fact]
        public void MapToBeanTest2()
        {
            var map = new Dictionary<string, object>
            {
                { "name", "Joe" },
                { "age", 12 }
            };

            // 非空构造也可以实例化成功
            var person = BeanUtil.ToBean(map, typeof(Person2), CopyOptions.Create()) as Person2;
            Assert.NotNull(person);
            Assert.Equal("Joe", person.name);
            Assert.Equal(12, person.age);
        }

        /// <summary>
        /// 测试在不忽略错误情况下，转换失败需要报错。
        /// </summary>
        [Fact]
        public void MapToBeanWinErrorTest()
        {
            Assert.Throws<FormatException>(() => {
                var map = new Dictionary<string, object>
                {
                    { "age", "哈哈" }
                };
                BeanUtil.ToBean(map, typeof(Person));
            });
        }

        [Fact]
        public void BeanToMapTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var map = BeanUtil.BeanToMap(person);

            Assert.Equal("测试A11", map["name"]);
            Assert.Equal(14, map["age"]);
            Assert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            Assert.False(map.ContainsKey("SUBNAME"));
        }

        [Fact]
        public void BeanToMapNullPropertiesTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var map = Bean