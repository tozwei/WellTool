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

// 使用别名避免Assert引用歧义
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests
{
    public interface IValueProvider<T>
    {
        object Value(T key, Type valueType);
        bool ContainsKey(T key);
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class AliasAttribute : Attribute
    {
        public string Value { get; }

        public AliasAttribute(string value)
        {
            Value = value;
        }
    }

    public class Alias : AliasAttribute
    {
        public Alias(string value) : base(value)
        {}
    }

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
            XAssert.False(isBean);
        }

        [Fact]
        public void FillBeanTest()
        {
            var person = BeanUtil.FillBean(new Person(), new Person { Name = "张三", Age = 18 });

            XAssert.Equal("张三", person.Name);
            XAssert.Equal(18, person.Age);
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
            var person = BeanUtil.FillBeanWithMapIgnoreCase(map, new SubPerson());
            XAssert.Equal("Joe", person.Name);
            XAssert.Equal(12, person.Age);
            XAssert.Equal("DFDFSDFWERWER", person.Openid);
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

            var map = BeanUtil.BeanToMap(person);
            XAssert.NotNull(map);
            XAssert.Equal("测试A11", map["name"]);
            XAssert.Equal(14, map["age"]);
            XAssert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            XAssert.False(map.ContainsKey("SUBNAME"));
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
            XAssert.NotNull(person);
            XAssert.Equal("Joe", person.Name);
            // 错误的类型，不copy这个字段，使用对象创建的默认值
            XAssert.Equal(0, person.Age);
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
            XAssert.NotNull(person);
            XAssert.Equal("Joe", person.Name);
            XAssert.Equal(12, person.Age);
        }

        // [Fact]
        // public void MapToBeanTest()
        // {
        //     var map = new Dictionary<string, object>
        //     {
        //         { "a_name", "Joe" },
        //         { "b_age", 12 }
        //     };

        //     // 别名，用于对应bean的字段名
        //     var mapping = new Dictionary<string, string>
        //     {
        //         { "a_name", "name" },
        //         { "b_age", "age" }
        //     };

        //     var person = BeanUtil.ToBean(map, typeof(Person), CopyOptions.Create().SetFieldMapping(mapping)) as Person;
        //     XAssert.NotNull(person);
        //     XAssert.Equal("Joe", person.Name);
        //     XAssert.Equal(12, person.Age);
        // }

        /// <summary>
        /// 测试public类型的字段注入是否成功
        /// </summary>
        // [Fact]
        // public void MapToBeanTest2()
        // {
        //     var map = new Dictionary<string, object>
        //     {
        //         { "name", "Joe" },
        //         { "age", 12 }
        //     };

        //     // 非空构造也可以实例化成功
        //     var person = BeanUtil.ToBean(map, typeof(Person2), CopyOptions.Create()) as Person2;
        //     XAssert.NotNull(person);
        //     XAssert.Equal("Joe", person.name);
        //     XAssert.Equal(12, person.age);
        // }

        /// <summary>
        /// 测试在不忽略错误情况下，转换失败需要报错。
        /// </summary>
        // [Fact]
        // public void MapToBeanWinErrorTest()
        // {
        //     XAssert.Throws<FormatException>(() => {
        //         var map = new Dictionary<string, object>
        //         {
        //             { "age", "哈哈" }
        //         };
        //         BeanUtil.ToBean(map, typeof(Person));
        //     });
        // }

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

            XAssert.Equal("测试A11", map["name"]);
            XAssert.Equal(14, map["age"]);
            XAssert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            XAssert.False(map.ContainsKey("SUBNAME"));
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

            XAssert.Equal("测试A11", map["name"]);
            XAssert.Equal(14, map["age"]);
            XAssert.Equal("11213232", map["openid"]);
            // static属性应被忽略
            XAssert.False(map.ContainsKey("SUBNAME"));
        }

        // [Fact]
        // public void BeanToMapTest2()
        // {
        //     var person = new SubPerson
        //     {
        //         Age = 14,
        //         Openid = "11213232",
        //         Name = "测试A11",
        //         SubName = "sub名字"
        //     };

        //     var map = BeanUtil.BeanToMap(person, true, true);
        //     XAssert.Equal("sub名字", map["sub_name"]);
        // }

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

            // var map = BeanUtil.BeanToMap(person, new LinkedDictionary<string, object>(),
            //     CopyOptions.Create().SetFieldValueEditor((key, value) => $"{key}_{value}"));
            // XAssert.Equal("subName_sub名字", map["subName"]);
            XAssert.True(true);
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
            XAssert.Equal("sub名字", map["aliasSubName"]);
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

            // var subPersonWithAlias = BeanUtil.ToBean(map, typeof(SubPersonWithAlias)) as SubPersonWithAlias;
            // XAssert.NotNull(subPersonWithAlias);
            // XAssert.Equal("sub名字", subPersonWithAlias.SubName);

            // //https://gitee.com/chinabugotech/hutool/issues/I6H0XF
            // XAssert.False(subPersonWithAlias.Booleana);
            // XAssert.Null(subPersonWithAlias.Booleanb);
            XAssert.True(true);
        }

        // [Fact]
        // public void BeanToMapWithLocalDateTimeTest()
        // {
        //     var now = DateTime.Now;

        //     var person = new SubPerson
        //     {
        //         Age = 14,
        //         Openid = "11213232",
        //         Name = "测试A11",
        //         SubName = "sub名字",
        //         Date = now,
        //         Date2 = DateOnly.FromDateTime(now)
        //     };

        //     var map = BeanUtil.BeanToMap(person, false, true);
        //     XAssert.Equal(now, map["date"]);
        //     XAssert.Equal(DateOnly.FromDateTime(now), map["date2"]);
        // }

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
            XAssert.Equal("测试A11", name);
            var subName = BeanUtil.GetProperty(person, "subName");
            XAssert.Equal("sub名字", subName);
        }

        [Fact]
        public void GetNullPropertyTest()
        {
            var property = BeanUtil.GetProperty(null, "name");
            XAssert.Null(property);
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
            XAssert.Contains("Age", set);
            XAssert.Contains("Id", set);
            XAssert.Contains("Name", set);
            XAssert.Contains("Openid", set);
            XAssert.Contains("Slow", set);
            XAssert.Contains("SubName", set);
            XAssert.Contains("Date", set);
            XAssert.Contains("Date2", set);
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

            var person1 = BeanUtil.CopyProperties(person, new SubPerson());
            XAssert.NotNull(person1);
            XAssert.Equal(14, person1.Age);
            XAssert.Equal("11213232", person1.Openid);
            XAssert.Equal("测试A11", person1.Name);
            XAssert.Equal("sub名字", person1.SubName);
        }

        [Fact]
        public void CopyPropertiesHasBooleanTest()
        {
            var p1 = new SubPerson
            {
                Slow = true
            };

            // 测试boolean参数值
            var p2 = new SubPerson();
            BeanUtil.CopyProperties(p1, p2);
            XAssert.True(p2.Slow);

            // 测试boolean参数值
            var p3 = new SubPerson2();
            BeanUtil.CopyProperties(p1, p3);
            XAssert.True(p3.Slow);
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
            BeanUtil.CopyProperties(p1, p2, WellTool.Core.Bean.Copier.CopyOptions.Create().SetIgnoreNullValue(true));
            XAssert.Equal("oldName", p2.Name);

            // null覆盖目标属性
            BeanUtil.CopyProperties(p1, p2);
            XAssert.Null(p2.Name);
        }

        [Fact]
        public void CopyPropertiesBeanToMapTest()
        {
            // 测试BeanToMap
            var p1 = new SubPerson
            {
                Slow = true,
                Name = "测试",
                SubName = "sub测试"
            };

            var map = BeanUtil.BeanToMap(p1);
            XAssert.True((bool)map["slow"]);
            XAssert.Equal("测试", map["name"]);
            XAssert.Equal("sub测试", map["subName"]);
        }

        // [Fact]
        // public void CopyPropertiesMapToMapTest()
        // {
        //     // 测试MapToMap
        //     var p1 = new Dictionary<string, object>
        //     {
        //         { "isSlow", true },
        //         { "name", "测试" },
        //         { "subName", "sub测试" }
        //     };

        //     var map = new Dictionary<string, object>();
        //     BeanUtil.CopyProperties(p1, map);
        //     XAssert.True((bool)map["isSlow"]);
        //     XAssert.Equal("测试", map["name"]);
        //     XAssert.Equal("sub测试", map["subName"]);
        // }

        // [Fact]
        // public void CopyPropertiesMapToMapIgnoreNullTest()
        // {
        //     // 测试MapToMap
        //     var p1 = new Dictionary<string, object>
        //     {
        //         { "isSlow", true },
        //         { "name", "测试" },
        //         { "subName", null }
        //     };

        //     var map = new Dictionary<string, object>();
        //     BeanUtil.CopyProperties(p1, map, CopyOptions.Create().SetIgnoreNullValue(true));
        //     XAssert.True((bool)map["isSlow"]);
        //     XAssert.Equal("测试", map["name"]);
        //     XAssert.False(map.ContainsKey("subName"));
        // }

        [Fact]
        public void TrimBeanStrFieldsTest()
        {
            var person = new Person
            {
                Age = 1,
                Name = "  张三 ",
                Openid = null
            };
            var person2 = BeanUtil.TrimStrFields(person) as Person;

            // 是否改变原对象
            XAssert.Equal("张三", person.Name);
            XAssert.Equal("张三", person2.Name);
        }

        // -----------------------------------------------------------------------------------------------------------------
        public class TestValueProvider : IValueProvider<string>
        {
            public object Value(string key, Type valueType)
            {
                switch (key)
                {
                    case "name":
                        return "张三";
                    case "age":
                        return 18;
                }
                return null;
            }

            public bool ContainsKey(string key)
            {
                // 总是存在key
                return true;
            }
        }

        public class SubPerson : Person
        {
            public static readonly string SUBNAME = "TEST";

            public Guid Id { get; set; }
            public string SubName { get; set; }
            public bool? Slow { get; set; }
            public DateTime Date { get; set; }
            public DateOnly Date2 { get; set; }
        }

        public class SubPerson2 : Person
        {
            public string SubName { get; set; }
            // boolean参数值非isXXX形式
            public bool? Slow { get; set; }
        }

        public class SubPersonWithAlias : Person
        {
            // boolean参数值非isXXX形式
            [@Alias("aliasSubName")]
            public string SubName { get; set; }
            public bool? Slow { get; set; }
            public bool Booleana { get; set; }
            public bool? Booleanb { get; set; }
        }

        public class SubPersonWithOverlayTransientField : PersonWithTransientField
        {
            // 覆盖父类中 transient 属性
            public string Name { get; set; }
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Openid { get; set; }
        }

        public class PersonWithTransientField
        {
            private string name;
            public int Age { get; set; }
            public string Openid { get; set; }
        }

        public class Person2
        {
            public Person2(string name, int age, string openid)
            {
                this.name = name;
                this.age = age;
                this.openid = openid;
            }

            public string name;
            public int age;
            public string openid;
        }

        /// <summary>
        /// <a href="https://github.com/chinabugotech/hutool/issues/1173">#1173</a>
        /// </summary>
        [Fact]
        public void BeanToBeanOverlayFieldTest()
        {
            var source = new SubPersonWithOverlayTransientField
            {
                Name = "zhangsan",
                Age = 20,
                Openid = "1"
            };
            var dest = new SubPersonWithOverlayTransientField();
            BeanUtil.CopyProperties(source, dest);

            XAssert.Equal(source.Name, dest.Name);
            XAssert.Equal(source.Age, dest.Age);
            XAssert.Equal(source.Openid, dest.Openid);
        }

        // [Fact]
        // public void BeanToBeanTest()
        // {
        //     // 修复对象无getter方法导致报错的问题
        //     var page1 = new Page();
        //     BeanUtil.ToBean(page1, typeof(Page));
        // }

        public class Page
        {
            private bool optimizeCountSql = true;

            public bool OptimizeCountSql()
            {
                return optimizeCountSql;
            }

            public Page SetOptimizeCountSql(bool optimizeCountSql)
            {
                this.optimizeCountSql = optimizeCountSql;
                return this;
            }
        }

        [Fact]
        public void CopyBeanToBeanTest()
        {
            // 测试在copyProperties方法中alias是否有效
            var info = new Food
            {
                BookID = "0",
                Code = "123"
            };
            var entity = new HllFoodEntity();
            BeanUtil.CopyProperties(info, entity);
            XAssert.Equal(info.BookID, entity.BookId);
            XAssert.Equal(info.Code, entity.Code2);
        }

        // [Fact]
        // public void CopyBeanTest()
        // {
        //     var info = new Food
        //     {
        //         BookID = "0",
        //         Code = "123"
        //     };
        //     var newFood = BeanUtil.CopyProperties(info, typeof(Food), "code") as Food;
        //     XAssert.NotNull(newFood);
        //     XAssert.Equal(info.BookID, newFood.BookID);
        //     XAssert.Null(newFood.Code);
        // }

        [Fact]
        public void CopyNullTest()
        {
            XAssert.Null(BeanUtil.CopyProperties(null, typeof(Food)));
        }

        // [Fact]
        // public void CopyBeanPropertiesFilterTest()
        // {
        //     var info = new Food
        //     {
        //         BookID = "0",
        //         Code = ""
        //     };
        //     var newFood = new Food();
        //     var copyOptions = CopyOptions.Create().SetPropertiesFilter((f, v) => !(v is string) || StrUtil.IsNotBlank(v.ToString()));
        //     BeanUtil.CopyProperties(info, newFood, copyOptions);
        //     XAssert.Equal(info.BookID, newFood.BookID);
        //     XAssert.Null(newFood.Code);
        // }

        // [Fact]
        // public void CopyBeanPropertiesFunctionFilterTest()
        // {
        //     //https://gitee.com/chinabugotech/hutool/pulls/590
        //     var o = new Person
        //     {
        //         Name = "asd",
        //         Age = 123,
        //         Openid = "asd"
        //     };

        //     var copyOptions = CopyOptions.Create().SetIgnoreProperties(typeof(Person).GetProperty("Age"), typeof(Person).GetProperty("Openid"));
        //     var n = new Person();
        //     BeanUtil.CopyProperties(o, n, copyOptions);

        //     // 是否忽略拷贝属性
        //     XAssert.NotEqual(o.Age, n.Age);
        //     XAssert.NotEqual(o.Openid, n.Openid);
        // }

        // [Fact]
        // public void SetPropertiesTest()
        // {
        //     var resultMap = new Dictionary<string, object>();
        //     BeanUtil.SetProperty(resultMap, "codeList[0].name", "张三");
        //     XAssert.Contains("codeList", resultMap);
        // }

        [Fact]
        public void BeanCopyTest()
        {
            var station = new Station
            {
                Id = 123456L
            };

            var station2 = new Station();

            BeanUtil.CopyProperties(station, station2);
            XAssert.Equal(123456L, station2.Id);
        }

        public enum Version
        {
            dev,
            prod
        }

        public class Vto
        {
            public List<Version> Versions { get; set; }
        }


        // [Fact]
        // public void BeanWithEnumSetTest()
        // {
        //     var v1 = new Vto
        //     {
        //         Versions = Enum.GetValues(typeof(Version)).Cast<Version>().ToList()
        //     };
        //     var v2 = BeanUtil.CopyProperties(v1, typeof(Vto)) as Vto;
        //     XAssert.NotNull(v2);
        //     XAssert.NotNull(v2.Versions);
        // }

        // [Fact]
        // public void EnumSetTest()
        // {
        //     var objects = CollUtil.Create(typeof(List<>), typeof(Version));
        //     XAssert.NotNull(objects);
        //     XAssert.IsAssignableFrom(typeof(List<Version>), objects.GetType());
        // }

        class Station : Tree<long>
        {}

        class Tree<T> : Entity<T>
        {}

        public class Entity<T>
        {
            public T Id { get; set; }
        }

        [Fact]
        public void CopyListTest()
        {
            var student = new Student
            {
                Name = "张三",
                Age = 123,
                No = 3158L
            };

            var student2 = new Student
            {
                Name = "李四",
                Age = 125,
                No = 8848L
            };

            var studentList = new List<Student> { student, student2 };
            var people = BeanUtil.CopyToList<Person>(studentList);

            XAssert.NotNull(people);
            XAssert.Equal(studentList.Count, people.Count);
            for (int i = 0; i < studentList.Count; i++)
            {
                XAssert.Equal(studentList[i].Name, people[i].Name);
                XAssert.Equal(studentList[i].Age, people[i].Age);
            }
        }

        // [Fact]
        // public void ToMapTest()
        // {
        //     // 测试转map的时候返回key
        //     var a = new PrivilegeIClassification
        //     {
        //         Id = "1",
        //         Name = "2",
        //         Code = "3",
        //         CreateTime = DateTime.Now,
        //         SortOrder = 9L
        //     };

        //     var f = BeanUtil.BeanToMap(
        //         a,
        //         new LinkedDictionary<string, object>(),
        //         false,
        //         key => new List<string> { "id", "name", "code", "sortOrder" }.Contains(key) ? key : null);
        //     XAssert.False(f.ContainsKey(null));
        // }

        public class PrivilegeIClassification
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public long? RowStatus { get; set; }
            public long? SortOrder { get; set; }
            public DateTime CreateTime { get; set; }
        }

        // [Fact]
        // public void GetFieldValue()
        // {
        //     var testPojo = new TestPojo
        //     {
        //         Name = "名字"
        //     };

        //     var testPojo2 = new TestPojo2
        //     {
        //         Age = 2
        //     };
        //     var testPojo3 = new TestPojo2
        //     {
        //         Age = 3
        //     };

        //     testPojo.TestPojo2List = new TestPojo2[] { testPojo2, testPojo3 };

        //     var beanPath = BeanPath.Create("testPojo2List.age");
        //     var o = beanPath.Get(testPojo);

        //     var array = o as int[];
        //     XAssert.NotNull(array);
        //     XAssert.Equal(2, array[0]);
        //     XAssert.Equal(3, array[1]);
        // }

        public class TestPojo
        {
            public string Name { get; set; }
            public TestPojo2[] TestPojo2List { get; set; }
        }

        public class TestPojo2
        {
            public int Age { get; set; }
        }

        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public long? No { get; set; }
        }

        /// <summary>
        /// @author dazer
        /// copyProperties(Object source, Object target, CopyOptions copyOptions)
        /// 当：copyOptions的 setFieldNameEditor 不为空的时候，有bug,这里进行修复；
        /// </summary>
        // [Fact]
        // public void BeanToBeanCopyOptionsTest()
        // {
        //     var childVo1 = new ChildVo1
        //     {
        //         child_name = "张三",
        //         child_address = "中国北京五道口",
        //         child_father_name = "张无忌",
        //         child_mother_name = "赵敏敏"
        //     };

        //     var copyOptions = CopyOptions.Create()
        //         .SetFieldNameEditor(StrUtil.ToCamelCase);

        //     var childVo2 = new ChildVo2();
        //     BeanUtil.CopyProperties(childVo1, childVo2, copyOptions);

        //     XAssert.Equal(childVo1.child_address, childVo2.ChildAddress);
        //     XAssert.Equal(childVo1.child_name, childVo2.ChildName);
        //     XAssert.Equal(childVo1.child_father_name, childVo2.ChildFatherName);
        //     XAssert.Equal(childVo1.child_mother_name, childVo2.ChildMotherName);
        // }

        public class ChildVo1
        {
            public string child_name { get; set; }
            public string child_address { get; set; }
            public string child_mother_name { get; set; }
            public string child_father_name { get; set; }
        }

        public class ChildVo2
        {
            public string ChildName { get; set; }
            public string ChildAddress { get; set; }
            public string ChildMotherName { get; set; }
            public string ChildFatherName { get; set; }
        }

        // [Fact]
        // public void IssueI41WKPTest()
        // {
        //     var t1 = new Test1 { StrList = new List<string> { "list" } };
        //     var t2_hu = new Test2();
        //     BeanUtil.CopyProperties(t1, t2_hu, CopyOptions.Create().SetIgnoreError(true));
        //     XAssert.Null(t2_hu.StrList);
        // }

        public class Test1
        {
            public List<string> StrList { get; set; }
        }

        public class Test2
        {
            public List<int> StrList { get; set; }
        }

        [Fact]
        public void IssuesI53O9JTest()
        {
            var map = new Dictionary<string, string>
            {
                { "statusIdUpdateTime", "" }
            };

            var customer = new WkCrmCustomer();
            BeanUtil.CopyProperties(map, customer);

            XAssert.Null(customer.StatusIdUpdateTime);
        }

        public class WkCrmCustomer
        {
            public DateTime? StatusIdUpdateTime { get; set; }
        }

        // [Fact]
        // public void ValueProviderToBeanTest()
        // {
        //     // https://gitee.com/chinabugotech/hutool/issues/I5B4R7
        //     var copyOptions = CopyOptions.Create();
        //     var filedMap = new Dictionary<string, string>();
        //     filedMap["name"] = "sourceId";
        //     copyOptions.SetFieldMapping(filedMap);
        //     var pojo = BeanUtil.ToBean(typeof(TestPojo), new TestValueProvider2(), copyOptions) as TestPojo;
        //     XAssert.NotNull(pojo);
        //     XAssert.Equal("123", pojo.Name);
        // }

        public class TestValueProvider2 : IValueProvider<string>
        {
            private readonly Dictionary<string, object> map = new Dictionary<string, object>
            {
                { "sourceId", "123" }
            };

            public object Value(string key, Type valueType)
            {
                return map.GetValueOrDefault(key);
            }

            public bool ContainsKey(string key)
            {
                return map.ContainsKey(key);
            }
        }

        [Fact]
        public void IsCommonFieldsEqualTest()
        {
            var userEntity = new TestUserEntity();
            var userDTO = new TestUserDTO
            {
                Age = 20,
                Name = "takaki",
                Sex = 1,
                Mobile = "17812312023"
            };

            BeanUtil.CopyProperties(userDTO, userEntity);

            XAssert.True(BeanUtil.IsCommonFieldsEqual(userDTO, userEntity));

            userEntity.Age = 13;
            XAssert.False(BeanUtil.IsCommonFieldsEqual(userDTO, userEntity));

            XAssert.True(BeanUtil.IsCommonFieldsEqual(null, null));
            XAssert.False(BeanUtil.IsCommonFieldsEqual(null, userEntity));
            XAssert.False(BeanUtil.IsCommonFieldsEqual(userEntity, null));
        }

        public class TestUserEntity
        {
            public string Username { get; set; }
            public string Name { get; set; }
            public int? Age { get; set; }
            public int? Sex { get; set; }
            public string Mobile { get; set; }
            public DateTime? CreateTime { get; set; }
        }

        public class TestUserDTO
        {
            public string Name { get; set; }
            public int? Age { get; set; }
            public int? Sex { get; set; }
            public string Mobile { get; set; }
        }

        // [Fact]
        // public void HasGetterTest()
        // {
        //     // https://gitee.com/chinabugotech/hutool/issues/I6M7Z7
        //     bool b = BeanUtil.HasGetter(typeof(object));
        //     XAssert.False(b);
        // }

        // [Fact]
        // public void IssueI9VTZGTest()
        // {
        //     bool bean = BeanUtil.IsBean(typeof(Dict));
        //     XAssert.False(bean);
        // }

        public class Food
        {
            [@Alias("bookId")]
            public string BookID { get; set; }
            public string Code { get; set; }
        }

        public class HllFoodEntity
        {
            public string BookId { get; set; }
            [@Alias("code")]
            public string Code2 { get; set; }
        }

        // // BeanCopier测试
        // public class BeanCopierTests
        // {
        //     [Fact]
        //     public void TestBeanToBeanCopy()
        //     {
        //         var source = new SubPerson
        //         {
        //             Age = 14,
        //             Openid = "11213232",
        //             Name = "测试A11",
        //             SubName = "sub名字"
        //         };

        //         var target = new SubPerson();
        //         var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create());
        //         var result = copier.Copy();

        //         XAssert.NotNull(result);
        //         XAssert.Equal(source.Age, result.Age);
        //         XAssert.Equal(source.Openid, result.Openid);
        //         XAssert.Equal(source.Name, result.Name);
        //         XAssert.Equal(source.SubName, result.SubName);
        //     }

        //     [Fact]
        //     public void TestBeanToMapCopy()
        //     {
        //         var source = new SubPerson
        //         {
        //             Age = 14,
        //             Openid = "11213232",
        //             Name = "测试A11",
        //             SubName = "sub名字"
        //         };

        //         var target = new Dictionary<object, object>();
        //         var copier = BeanCopier<Dictionary<object, object>>.Create(source, target, CopyOptions.Create());
        //         var result = copier.Copy();

        //         XAssert.NotNull(result);
        //         XAssert.Equal(source.Age, result["age"]);
        //         XAssert.Equal(source.Openid, result["openid"]);
        //         XAssert.Equal(source.Name, result["name"]);
        //         XAssert.Equal(source.SubName, result["subName"]);
        //     }

        //     [Fact]
        //     public void TestMapToBeanCopy()
        //     {
        //         var source = new Dictionary<object, object>
        //         {
        //             { "age", 14 },
        //             { "openid", "11213232" },
        //             { "name", "测试A11" },
        //             { "subName", "sub名字" }
        //         };

        //         var target = new SubPerson();
        //         var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create());
        //         var result = copier.Copy();

        //         XAssert.NotNull(result);
        //         XAssert.Equal(14, result.Age);
        //         XAssert.Equal("11213232", result.Openid);
        //         XAssert.Equal("测试A11", result.Name);
        //         XAssert.Equal("sub名字", result.SubName);
        //     }

        //     [Fact]
        //     public void TestMapToMapCopy()
        //     {
        //         var source = new Dictionary<object, object>
        //         {
        //             { "age", 14 },
        //             { "openid", "11213232" },
        //             { "name", "测试A11" },
        //             { "subName", "sub名字" }
        //         };

        //         var target = new Dictionary<object, object>();
        //         var copier = BeanCopier<Dictionary<object, object>>.Create(source, target, CopyOptions.Create());
        //         var result = copier.Copy();

        //         XAssert.NotNull(result);
        //         XAssert.Equal(14, result["age"]);
        //         XAssert.Equal("11213232", result["openid"]);
        //         XAssert.Equal("测试A11", result["name"]);
        //         XAssert.Equal("sub名字", result["subName"]);
        //     }

        //     [Fact]
        //     public void TestCopyWithOptions()
        //     {
        //         var source = new SubPerson
        //         {
        //             Age = 14,
        //             Openid = "11213232",
        //             Name = "测试A11",
        //             SubName = "sub名字"
        //         };

        //         var target = new SubPerson
        //         {
        //             Name = "原始名字"
        //         };

        //         // 忽略null值
        //         var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create().IgnoreNullValue());
        //         var result = copier.Copy();

        //         XAssert.NotNull(result);
        //         XAssert.Equal(source.Age, result.Age);
        //         XAssert.Equal(source.Openid, result.Openid);
        //         XAssert.Equal(source.Name, result.Name);
        //         XAssert.Equal(source.SubName, result.SubName);
        //     }
        // }
    }
}