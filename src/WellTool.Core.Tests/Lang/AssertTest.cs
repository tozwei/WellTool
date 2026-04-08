using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Lang;

namespace WellTool.Core.Tests;

public class AssertTest
{
    #region IsNull
    [Fact]
    public void IsNullTest()
    {
        string? a = null;
        Xunit.Assert.True(AssertUtil.IsNull(a));
    }

    [Fact]
    public void IsNullNotMatchTest()
    {
        string? a = "test";
        Xunit.Assert.False(AssertUtil.IsNull(a));
    }

    [Fact]
    public void IsNullWithMessageTest()
    {
        string? a = "test";
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsNull(a, "Object must be null"));
    }
    #endregion

    #region NotNull
    [Fact]
    public void NotNullTest()
    {
        string? a = "test";
        Xunit.Assert.True(AssertUtil.NotNull(a));
    }

    [Fact]
    public void NotNullWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentNullException>(() => AssertUtil.NotNull(null, "Object cannot be null"));
    }

    [Fact]
    public void NotNullWithTemplateTest()
    {
        string? a = null;
        var ex = Xunit.Assert.Throws<ArgumentNullException>(() => AssertUtil.NotNull(a, "Value of {0} cannot be null", "param1"));
    }

    [Fact]
    public void NotNullWithSupplierTest()
    {
        string? a = null;
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.NotNull(a, () => new InvalidOperationException("Custom error")));
    }
    #endregion

    #region IsTrue
    [Fact]
    public void IsTrueTest()
    {
        int i = 1;
        Xunit.Assert.True(AssertUtil.IsTrue(i > 0));
    }

    [Fact]
    public void IsTrueWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsTrue(false, "Expression must be true"));
    }

    [Fact]
    public void IsTrueWithTemplateTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsTrue(false, "Value {0} must be greater than {1}", 5, 10));
    }

    [Fact]
    public void IsTrueWithSupplierTest()
    {
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.IsTrue(false, () => new InvalidOperationException("Custom error")));
    }
    #endregion

    #region IsFalse
    [Fact]
    public void IsFalseTest()
    {
        int i = 0;
        Xunit.Assert.True(AssertUtil.IsFalse(i > 0));
    }

    [Fact]
    public void IsFalseWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsFalse(true, "Expression must be false"));
    }

    [Fact]
    public void IsFalseWithTemplateTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsFalse(true, "Value {0} must be false", "flag"));
    }
    #endregion

    #region CheckBetween
    [Fact]
    public void CheckBetweenIntTest()
    {
        var result = AssertUtil.CheckBetween(5, 1, 10);
        Xunit.Assert.Equal(5, result);
    }

    [Fact]
    public void CheckBetweenLongTest()
    {
        var result = AssertUtil.CheckBetween(100L, 0L, 1000L);
        Xunit.Assert.Equal(100L, result);
    }

    [Fact]
    public void CheckBetweenDoubleTest()
    {
        var result = AssertUtil.CheckBetween(3.14, 0.0, 10.0);
        Xunit.Assert.Equal(3.14, result);
    }

    [Fact]
    public void CheckBetweenOutOfRangeTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => AssertUtil.CheckBetween(15, 1, 10));
    }
    #endregion

    #region CheckIndex
    [Fact]
    public void CheckIndexTest()
    {
        var result = AssertUtil.CheckIndex(3, 10);
        Xunit.Assert.Equal(3, result);
    }

    [Fact]
    public void CheckIndexNegativeTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => AssertUtil.CheckIndex(-1, 10));
    }

    [Fact]
    public void CheckIndexOutOfRangeTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => AssertUtil.CheckIndex(10, 10));
    }
    #endregion

    #region NotEmpty (Array)
    [Fact]
    public void NotEmptyArrayTest()
    {
        var array = new[] { "a", "b", "c" };
        Xunit.Assert.True(AssertUtil.NotEmpty(array));
    }

    [Fact]
    public void NotEmptyArrayNullTest()
    {
        string[]? array = null;
        Xunit.Assert.False(AssertUtil.NotEmpty(array));
    }

    [Fact]
    public void NotEmptyArrayEmptyTest()
    {
        var array = Array.Empty<string>();
        Xunit.Assert.False(AssertUtil.NotEmpty(array));
    }

    [Fact]
    public void NotEmptyArrayWithMessageTest()
    {
        var array = Array.Empty<string>();
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotEmpty(array, "Array cannot be empty"));
    }
    #endregion

    #region NotEmpty (Collection)
    [Fact]
    public void NotEmptyCollectionTest()
    {
        var list = new List<string> { "a", "b" };
        Xunit.Assert.True(AssertUtil.NotEmpty(list));
    }

    [Fact]
    public void NotEmptyCollectionNullTest()
    {
        List<string>? list = null;
        Xunit.Assert.False(AssertUtil.NotEmpty(list));
    }

    [Fact]
    public void NotEmptyCollectionEmptyTest()
    {
        var list = new List<string>();
        Xunit.Assert.False(AssertUtil.NotEmpty(list));
    }

    [Fact]
    public void NotEmptyCollectionWithMessageTest()
    {
        var list = new List<string>();
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotEmpty(list, "Collection cannot be empty"));
    }
    #endregion

    #region NotEmpty (Map)
    [Fact]
    public void NotEmptyMapTest()
    {
        var map = new Dictionary<string, int> { { "a", 1 } };
        Xunit.Assert.True(AssertUtil.NotEmpty(map));
    }

    [Fact]
    public void NotEmptyMapNullTest()
    {
        Dictionary<string, int>? map = null;
        Xunit.Assert.False(AssertUtil.NotEmpty(map));
    }

    [Fact]
    public void NotEmptyMapEmptyTest()
    {
        var map = new Dictionary<string, int>();
        Xunit.Assert.False(AssertUtil.NotEmpty(map));
    }

    [Fact]
    public void NotEmptyMapWithMessageTest()
    {
        var map = new Dictionary<string, int>();
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotEmpty(map, "Map cannot be empty"));
    }
    #endregion

    #region NoNullElements
    [Fact]
    public void NoNullElementsTest()
    {
        var array = new[] { "a", "b", "c" };
        AssertUtil.NoNullElements(array);
    }

    [Fact]
    public void NoNullElementsEmptyTest()
    {
        var array = Array.Empty<string>();
        AssertUtil.NoNullElements(array);
    }

    [Fact]
    public void NoNullElementsWithNullTest()
    {
        string[]? array = null;
        AssertUtil.NoNullElements(array);
    }

    [Fact]
    public void NoNullElementsWithNullItemTest()
    {
        var array = new string[] { "a", null, "c" };
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NoNullElements(array, "Array contains null elements"));
    }
    #endregion

    #region Empty
    [Fact]
    public void EmptyTest()
    {
        var list = new List<string>();
        Xunit.Assert.True(AssertUtil.Empty(list));
    }

    [Fact]
    public void EmptyNullTest()
    {
        List<string>? list = null;
        Xunit.Assert.True(AssertUtil.Empty(list));
    }

    [Fact]
    public void EmptyNotEmptyTest()
    {
        var list = new List<string> { "a" };
        Xunit.Assert.False(AssertUtil.Empty(list));
    }

    [Fact]
    public void EmptyWithMessageTest()
    {
        var list = new List<string> { "a" };
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.Empty(list, "Collection must be empty"));
    }
    #endregion

    #region NotBlank
    [Fact]
    public void NotBlankTest()
    {
        string str = "test";
        Xunit.Assert.True(AssertUtil.NotBlank(str));
    }

    [Fact]
    public void NotBlankWhiteSpaceTest()
    {
        string str = "   ";
        Xunit.Assert.False(AssertUtil.NotBlank(str));
    }

    [Fact]
    public void NotBlankNullTest()
    {
        string? str = null;
        Xunit.Assert.False(AssertUtil.NotBlank(str));
    }

    [Fact]
    public void NotBlankWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotBlank("", "String cannot be blank"));
    }

    [Fact]
    public void NotBlankWithTemplateTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotBlank("", "Value of {0} cannot be blank", "name"));
    }
    #endregion

    #region NotContain
    [Fact]
    public void NotContainTest()
    {
        AssertUtil.NotContain("hello world", "foo");
    }

    [Fact]
    public void NotContainFoundTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotContain("hello world", "world"));
    }

    [Fact]
    public void NotContainWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.NotContain("hello world", "world", "String should not contain {0}", "world"));
    }

    [Fact]
    public void NotContainNullSearchTest()
    {
        AssertUtil.NotContain(null, "test");
    }

    [Fact]
    public void NotContainNullSubstringTest()
    {
        AssertUtil.NotContain("test", null);
    }
    #endregion

    #region IsInstanceOf
    [Fact]
    public void IsInstanceOfTest()
    {
        AssertUtil.IsInstanceOf(typeof(string), "test");
    }

    [Fact]
    public void IsInstanceOfNotMatchTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsInstanceOf(typeof(string), 123));
    }

    [Fact]
    public void IsInstanceOfNullTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsInstanceOf(typeof(string), null));
    }

    [Fact]
    public void IsInstanceOfWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsInstanceOf(typeof(string), 123, "Object must be instance of {0}", "String"));
    }
    #endregion

    #region IsAssignable
    [Fact]
    public void IsAssignableTest()
    {
        AssertUtil.IsAssignable(typeof(IEnumerable<>), typeof(List<string>));
    }

    [Fact]
    public void IsAssignableNotMatchTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsAssignable(typeof(List<string>), typeof(string)));
    }

    [Fact]
    public void IsAssignableNullTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsAssignable(typeof(object), null));
    }

    [Fact]
    public void IsAssignableWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<ArgumentException>(() => AssertUtil.IsAssignable(typeof(List<string>), typeof(string), "{0} must be assignable to {1}", "string", "list"));
    }
    #endregion

    #region State
    [Fact]
    public void StateTest()
    {
        AssertUtil.State(true);
    }

    [Fact]
    public void StateFalseTest()
    {
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.State(false));
    }

    [Fact]
    public void StateWithMessageTest()
    {
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.State(false, "Invalid state"));
    }

    [Fact]
    public void StateWithTemplateTest()
    {
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.State(false, "State {0} is invalid", "SUSPENDED"));
    }

    [Fact]
    public void StateWithSupplierTest()
    {
        var ex = Xunit.Assert.Throws<InvalidOperationException>(() => AssertUtil.State(false, () => "Custom state error"));
    }
    #endregion
}
