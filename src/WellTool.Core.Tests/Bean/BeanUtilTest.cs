// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Xunit;
using WellTool.Core.Bean;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// BeanUtil 测试
/// </summary>
public class BeanUtilTest
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Openid { get; set; }
    }

    public class SubPerson : Person
    {
        public string SubName { get; set; }
    }

    [Fact]
    public void IsBeanTest()
    {
        // Dictionary 不包含 setXXX 方法，不是 bean
        bool isBean = BeanUtil.IsBean(typeof(System.Collections.Generic.Dictionary<string, object>));
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

        XAssert.Equal("测试A11", map["name"]);
        XAssert.Equal(14, map["age"]);
        XAssert.Equal("11213232", map["openid"]);
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
        XAssert.Equal("测试A11", name);
        var subName = BeanUtil.GetProperty(person, "subName");
        XAssert.Equal("sub名字", subName);
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
    public void SetPropertiesTest()
    {
        var person = new Person();
        BeanUtil.SetProperty(person, "Name", "张三");
        XAssert.Equal("张三", person.Name);
    }
}
