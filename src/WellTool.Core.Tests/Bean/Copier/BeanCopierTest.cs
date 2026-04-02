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

using Xunit;
using WellTool.Core.Bean.Copier;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests.Bean.Copier;

/// <summary>
/// BeanCopier 测试
/// </summary>
public class BeanCopierTest
{
    public class SubPerson
    {
        public int Age { get; set; }
        public string Openid { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
    }

    [Fact]
    public void TestBeanToBeanCopy()
    {
        var source = new SubPerson
        {
            Age = 14,
            Openid = "11213232",
            Name = "测试A11",
            SubName = "sub名字"
        };

        var target = new SubPerson();
        var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        XAssert.NotNull(result);
        XAssert.Equal(source.Age, result.Age);
        XAssert.Equal(source.Openid, result.Openid);
        XAssert.Equal(source.Name, result.Name);
        XAssert.Equal(source.SubName, result.SubName);
    }

    [Fact]
    public void TestBeanToMapCopy()
    {
        var source = new SubPerson
        {
            Age = 14,
            Openid = "11213232",
            Name = "测试A11",
            SubName = "sub名字"
        };

        var target = new System.Collections.Generic.Dictionary<object, object>();
        var copier = BeanCopier<System.Collections.Generic.Dictionary<object, object>>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        XAssert.NotNull(result);
        XAssert.Equal(source.Age, result["age"]);
        XAssert.Equal(source.Openid, result["openid"]);
        XAssert.Equal(source.Name, result["name"]);
        XAssert.Equal(source.SubName, result["subname"]);
    }

    [Fact]
    public void TestMapToBeanCopy()
    {
        var source = new System.Collections.Generic.Dictionary<object, object>
        {
            { "age", 14 },
            { "openid", "11213232" },
            { "name", "测试A11" },
            { "subname", "sub名字" }
        };

        var target = new SubPerson();
        var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        XAssert.NotNull(result);
        XAssert.Equal(14, result.Age);
        XAssert.Equal("11213232", result.Openid);
        XAssert.Equal("测试A11", result.Name);
        XAssert.Equal("sub名字", result.SubName);
    }

    [Fact]
    public void TestMapToMapCopy()
    {
        var source = new System.Collections.Generic.Dictionary<object, object>
        {
            { "age", 14 },
            { "openid", "11213232" },
            { "name", "测试A11" },
            { "subname", "sub名字" }
        };

        var target = new System.Collections.Generic.Dictionary<object, object>();
        var copier = BeanCopier<System.Collections.Generic.Dictionary<object, object>>.Create(source, target, CopyOptions.Create());
        var result = copier.Copy();

        XAssert.NotNull(result);
        XAssert.Equal(14, result["age"]);
        XAssert.Equal("11213232", result["openid"]);
        XAssert.Equal("测试A11", result["name"]);
        XAssert.Equal("sub名字", result["subname"]);
    }

    [Fact]
    public void TestCopyWithOptions()
    {
        var source = new SubPerson
        {
            Age = 14,
            Openid = "11213232",
            Name = "测试A11",
            SubName = "sub名字"
        };

        var target = new SubPerson
        {
            Name = "原始名字"
        };

        // 忽略null值
        var copier = BeanCopier<SubPerson>.Create(source, target, CopyOptions.Create().SetIgnoreNullValue(true));
        var result = copier.Copy();

        XAssert.NotNull(result);
        XAssert.Equal(source.Age, result.Age);
        XAssert.Equal(source.Openid, result.Openid);
        XAssert.Equal(source.Name, result.Name);
        XAssert.Equal(source.SubName, result.SubName);
    }
}
