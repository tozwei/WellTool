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

            var map = BeanUtil.BeanToMap(person, null);

            Assert.Equal("测试A11", map["name"]);
            Assert.Equal(14, map["age"]);
            Assert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            Assert.False(map.ContainsKey("SUBNAME"));
        }

        [Fact]
        public void BeanToMapTest2()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var map = BeanUtil.BeanToMap(person, true, true);
            Assert.Equal("sub名字", map["sub_name"]);
        }

        [Fact]
        public void BeanToMapWithValueEditTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var map = BeanUtil.BeanToMap(person, new LinkedDictionary<string, object>(),
                CopyOptions.Create().SetFieldValueEditor((key, value) => $"{key}_{value}"));
            Assert.Equal("subName_sub名字", map["subName"]);
        }

        [Fact]
        public void BeanToMapWithAliasTest()
        {
            var person = new SubPersonWithAlias
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字",
                Slow = true,
                Booleana = true,
                Booleanb = true
            };

            var map = BeanUtil.BeanToMap(person);
            Assert.Equal("sub名字", map["aliasSubName"]);
        }

        [Fact]
        public void MapToBeanWithAliasTest()
        {
            var map = new Dictionary<string, object>
            {
                { "aliasSubName", "sub名字" },
                { "slow", true },
                { "is_booleana", "1" },
                { "is_booleanb", true }
            };

            var subPersonWithAlias = BeanUtil.ToBean(map, typeof(SubPersonWithAlias)) as SubPersonWithAlias;
            Assert.NotNull(subPersonWithAlias);
            Assert.Equal("sub名字", subPersonWithAlias.SubName);

            //https://gitee.com/chinabugotech/hutool/issues/I6H0XF
            Assert.False(subPersonWithAlias.Booleana);
            Assert.Null(subPersonWithAlias.Booleanb);
        }

        [Fact]
        public void BeanToMapWithLocalDateTimeTest()
        {
            var now = DateTime.Now;

            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字",
                Date = now,
                Date2 = DateOnly.FromDateTime(now)
            };

            var map = BeanUtil.BeanToMap(person, false, true);
            Assert.Equal(now, map["date"]);
            Assert.Equal(DateOnly.FromDateTime(now), map["date2"]);
        }

        [Fact]
        public void GetPropertyTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var name = BeanUtil.GetProperty(person, "name");
            Assert.Equal("测试A11", name);
            var subName = BeanUtil.GetProperty(person, "subName");
            Assert.Equal("sub名字", subName);
        }

        [Fact]
        public void GetNullPropertyTest()
        {
            var property = BeanUtil.GetProperty(null, "name");
            Assert.Null(property);
        }

        [Fact]
        public void GetPropertyDescriptorsTest()
        {
            var set = new HashSet<string>();
            var propertyDescriptors = BeanUtil.GetPropertyDescriptors(typeof(SubPerson));
            foreach (var propertyDescriptor in propertyDescriptors)
            {
                set.Add(propertyDescriptor.Name);
            }
            Assert.Contains("age", set);
            Assert.Contains("id", set);
            Assert.Contains("name", set);
            Assert.Contains("openid", set);
            Assert.Contains("slow", set);
            Assert.Contains("subName", set);
        }

        [Fact]
        public void CopyPropertiesTest()
        {
            var person = new SubPerson
            {
                Age = 14,
                Openid = "11213232",
                Name = "测试A11",
                SubName = "sub名字"
            };

            var person1 = BeanUtil.CopyProperties(person, typeof(SubPerson)) as SubPerson;
            Assert.NotNull(person1);
            Assert.Equal(14, person1.Age);
            Assert.Equal("11213232", person1.Openid);
            Assert.Equal("测试A11", person1.Name);
            Assert.Equal("sub名字", person1.SubName);
        }

        [Fact]
        public void CopyPropertiesHasBooleanTest()
        {
            var p1 = new SubPerson
            {
                Slow = true
            };

            // 测试boolean参数值isXXX形式
            var p2 = new SubPerson();
            BeanUtil.CopyProperties(p1, p2);
            Assert.True(p2.Slow);

            // 测试boolean参数值非isXXX形式
            var p3 = new SubPerson2();
            BeanUtil.CopyProperties(p1, p3);
            Assert.True(p3.Slow);
        }

        [Fact]
        public void CopyPropertiesIgnoreNullTest()
        {
            var p1 = new SubPerson
            {
                Slow = true,
                Name = null
            };

            var p2 = new SubPerson2
            {
                Name = "oldName"
            };

            // null值不覆盖目标属性
            BeanUtil.CopyProperties(p1, p2, CopyOptions.Create().IgnoreNullValue());
            Assert.Equal("oldName", p2.Name);

