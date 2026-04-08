using Xunit;
using WellTool.Core.Bean;
using System.Reflection;

namespace WellTool.Core.Tests.Util;

/// <summary>
/// BeanDesc 测试
/// </summary>
public class BeanDescLastTest
{
    [Fact]
    public void PropDescTest()
    {
        var desc = BeanUtil.GetBeanDesc(typeof(User));
        Assert.Equal("User", desc.SimpleName);

        Assert.Equal("age", desc.GetField("age").Name);
        Assert.Equal("getAge", desc.GetGetter("age").Name);
        Assert.Equal("setAge", desc.GetSetter("age").Name);
        Assert.Single(desc.GetSetter("age").GetParameters());
        Assert.Equal(typeof(int), desc.GetSetter("age").GetParameters()[0].ParameterType);
    }

    [Fact]
    public void PropDescTest2()
    {
        var desc = BeanUtil.GetBeanDesc(typeof(User));

        var prop = desc.GetProp("name");
        Assert.Equal("name", prop.FieldName);
        Assert.Equal("getName", prop.Getter.Name);
        Assert.Equal("setName", prop.Setter.Name);
        Assert.Single(prop.Setter.GetParameters());
        Assert.Equal(typeof(string), prop.Setter.GetParameters()[0].ParameterType);
    }

    [Fact]
    public void PropDescOfBooleanTest()
    {
        var desc = BeanUtil.GetBeanDesc(typeof(User));

        Assert.Equal("isAdmin", desc.GetGetter("isAdmin").Name);
        Assert.Equal("setAdmin", desc.GetSetter("isAdmin").Name);
        Assert.Equal("isGender", desc.GetGetter("gender").Name);
        Assert.Equal("setGender", desc.GetSetter("gender").Name);
    }

    [Fact]
    public void PropDescOfBooleanTest2()
    {
        var desc = BeanUtil.GetBeanDesc(typeof(User));

        Assert.Equal("getIsSuper", desc.GetGetter("isSuper").Name);
        Assert.Equal("setIsSuper", desc.GetSetter("isSuper").Name);
    }

    [Fact]
    public void GetSetTest()
    {
        var desc = BeanUtil.GetBeanDesc(typeof(User));

        var user = new User();
        desc.GetProp("name").SetValue(user, "张三");
        Assert.Equal("张三", user.Name);

        var value = desc.GetProp("name").GetValue(user);
        Assert.Equal("张三", value);
    }

    public class User
    {
        private string _name = "";
        private int _age;
        private bool _isAdmin;
        private bool _isSuper;
        private bool _gender;
        private bool? _lastPage;
        private bool? _isLastPage;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set => _isAdmin = value;
        }

        public bool IsSuper
        {
            get => _isSuper;
            set => _isSuper = value;
        }

        public bool Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public bool? LastPage
        {
            get => _lastPage;
            set => _lastPage = value;
        }

        public bool? IsLastPage
        {
            get => _isLastPage;
            set => _isLastPage = value;
        }

        public string GetName() => _name;
        public void SetName(string name) => _name = name;
        public int GetAge() => _age;
        public User SetAge(int age) { _age = age; return this; }
        public string TestMethod() => "test for " + _name;
        public bool IsAdmin() => _isAdmin;
        public void SetAdmin(bool isAdmin) => _isAdmin = isAdmin;
        public bool GetIsSuper() => _isSuper;
        public void SetIsSuper(bool isSuper) => _isSuper = isSuper;
        public bool IsGender() => _gender;
        public void SetGender(bool gender) => _gender = gender;
        public bool? GetLastPage() => _lastPage;
        public void SetLastPage(bool? lastPage) => _lastPage = lastPage;
        public bool? GetIsLastPage() => _isLastPage;
        public void SetIsLastPage(bool? isLastPage) => _isLastPage = isLastPage;
    }
}
