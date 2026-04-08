using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;
using WellTool.Core.Util;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue IBLTZW 测试 - 自定义字段转换器
/// </summary>
public class IssueIBLTZWTest
{
    [Fact]
    public void TestCopy()
    {
        var bean = new TestBean
        {
            Name = "test",
            Date = DateTime.Parse("2025-02-17")
        };

        var testBean2 = new TestBean2();
        var copyOptions = CreateCopyOptions();
        BeanUtil.CopyProperties(bean, testBean2, copyOptions);
        Assert.Equal("2025", testBean2.Date);
    }

    public class TestBean
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public class TestBean2
    {
        public string Name { get; set; }
        public string Date { get; set; }
    }

    static CopyOptions CreateCopyOptions()
    {
        var copyOptions = CopyOptions.Create();
        copyOptions.IgnoreError = true;
        copyOptions.Converter = null;
        copyOptions.FieldValueEditor = (fieldName, value) =>
        {
            var targetField = typeof(TestBean2).GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (targetField != null && targetField.FieldType == typeof(string) && value is DateTime)
            {
                return ((DateTime)value).ToString("yyyy");
            }
            return copyOptions.Converter != null ? copyOptions.Converter(targetField?.FieldType, value) : value;
        };
        return copyOptions;
    }
}
