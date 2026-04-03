using Xunit;
using System.Reflection;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Reflect工具单元测试
    /// </summary>
    public class ReflectUtilTest
    {
        [Fact]
        public void GetFieldValueTest()
        {
            var obj = new TestClass { Name = "Test" };
            var value = ReflectUtil.GetFieldValue(obj, "Name");
            Assert.Equal("Test", value);
        }

        [Fact]
        public void SetFieldValueTest()
        {
            var obj = new TestClass();
            ReflectUtil.SetFieldValue(obj, "Name", "NewName");
            Assert.Equal("NewName", obj.Name);
        }

        [Fact]
        public void GetMethodTest()
        {
            var method = ReflectUtil.GetMethod(typeof(TestClass), "TestMethod");
            Assert.NotNull(method);
        }

        [Fact]
        public void InvokeMethodTest()
        {
            var obj = new TestClass();
            var result = ReflectUtil.InvokeMethod(obj, "TestMethod");
            Assert.Equal("result", result);
        }

        [Fact]
        public void GetFieldsTest()
        {
            var fields = ReflectUtil.GetFields(typeof(TestClass));
            Assert.NotEmpty(fields);
        }

        [Fact]
        public void GetMethodsTest()
        {
            var methods = ReflectUtil.GetMethods(typeof(TestClass));
            Assert.NotEmpty(methods);
        }

        [Fact]
        public void GetTypeInfoTest()
        {
            var typeInfo = ReflectUtil.GetTypeInfo(typeof(TestClass));
            Assert.NotNull(typeInfo);
        }

        [Fact]
        public void GetAnnotationTest()
        {
            var annotation = ReflectUtil.GetAnnotation<ObsoleteAttribute>(typeof(TestClass));
            Assert.Null(annotation); // TestClass没有这个特性
        }

        [Fact]
        public void HasDeclaredMethodTest()
        {
            Assert.True(ReflectUtil.HasDeclaredMethod(typeof(TestClass), "ToString"));
        }

        [Fact]
        public void IsPublicTest()
        {
            Assert.True(ReflectUtil.IsPublic(typeof(TestClass).GetMethod("PublicMethod")!));
            Assert.False(ReflectUtil.IsPublic(typeof(TestClass).GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance)!));
        }

        [Fact]
        public void IsStaticTest()
        {
            Assert.True(ReflectUtil.IsStatic(typeof(TestClass).GetField("StaticField", BindingFlags.Public | BindingFlags.Static)!));
        }

        [Fact]
        public void GetClassNameTest()
        {
            Assert.Equal("TestClass", ReflectUtil.GetClassName(typeof(TestClass)));
        }

        [Fact]
        public void GetNameTest()
        {
            var name = ReflectUtil.GetName(typeof(TestClass));
            Assert.Equal("WellTool.Core.Tests.ReflectUtilTest+TestClass", name);
        }

        [Fact]
        public void GetGetterTest()
        {
            var getter = ReflectUtil.GetGetter(typeof(TestClass), "Name");
            Assert.NotNull(getter);
        }

        [Fact]
        public void GetSetterTest()
        {
            var setter = ReflectUtil.GetSetter(typeof(TestClass), "Name");
            Assert.NotNull(setter);
        }

        [Fact]
        public void GetTypeValueTest()
        {
            var obj = new TestClass { Age = 25 };
            var age = ReflectUtil.GetTypeValue<int>(obj, "Age");
            Assert.Equal(25, age);
        }

        public class TestClass
        {
            public static string StaticField = "";

            public string Name { get; set; } = "";

            public int Age { get; set; }

            public string TestMethod()
            {
                return "result";
            }

            public void PublicMethod() { }

            private void PrivateMethod() { }
        }
    }
}
