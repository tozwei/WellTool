using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Annotation;
using WellTool.Core.Annotation.Scanner;

namespace WellTool.Core.Tests
{
    public class AnnotationTests
    {
        [Fact]
        public void GetCombinationAnnotationsTest()
        {
            var annotations = AnnotationUtil.GetAnnotations(typeof(ClassWithAnnotation), true);
            XAssert.NotNull(annotations);
            XAssert.Equal(2, annotations.Length);
        }

        [Fact]
        public void GetCombinationAnnotationsWithClassTest()
        {
            var annotations = AnnotationUtil.GetCombinationAnnotations<AnnotationForTest>(typeof(ClassWithAnnotation));
            XAssert.NotNull(annotations);
            XAssert.Equal(1, annotations.Length);
            XAssert.True(annotations[0].Value == "测试" || annotations[0].Value == "repeat-annotation");
        }

        [Fact]
        public void GetAnnotationValueTest()
        {
            var value = AnnotationUtil.GetAnnotationValue(typeof(ClassWithAnnotation), typeof(AnnotationForTest));
            XAssert.True(value.Equals("测试") || value.Equals("repeat-annotation"));
        }

        [Fact]
        public void GetAnnotationValueTest2()
        {
            var names = AnnotationUtil.GetAnnotationValue(typeof(ClassWithAnnotation), (AnnotationForTest a) => a.Names);
            XAssert.True((names.Length == 1 && names[0] == "") || (names.Length == 2 && names[0] == "测试1" && names[1] == "测试2"));
        }

        [Fact]
        public void GetAnnotationSyncAlias()
        {
            // 直接获取
            var annotation = typeof(ClassWithAnnotation).GetCustomAttribute<AnnotationForTest>();
            XAssert.Equal("", annotation.Retry);

            // 加别名适配
            var aliasAnnotation = AnnotationUtil.GetAnnotationAlias<AnnotationForTest>(typeof(ClassWithAnnotation));
            var retryValue = aliasAnnotation.Retry;
            XAssert.True(retryValue == "测试" || retryValue == "repeat-annotation");
            XAssert.True(AnnotationUtil.IsSynthesizedAnnotation(aliasAnnotation));
        }

        [Fact]
        public void GetAnnotationSyncAliasWhenNotAnnotation()
        {
            GetAnnotationSyncAlias();
            // 使用AnnotationUtil.GetAnnotationAlias获取对象上并不存在的注解
            var alias = AnnotationUtil.GetAnnotationAlias<Alias>(typeof(ClassWithAnnotation));
            XAssert.Null(alias);
        }

        [AnnotationForTest(Value = "测试", Names = new[] { "测试1", "测试2" })]
        [RepeatAnnotationForTest]
        private class ClassWithAnnotation
        {
            public void Test()
            {
            }
        }

        [Fact]
        public void ScanMetaAnnotationTest()
        {
            // RootAnnotation -> RootMetaAnnotation1 -> RootMetaAnnotation2 -> RootMetaAnnotation3
            //                -> RootMetaAnnotation3
            var annotations = AnnotationUtil.ScanMetaAnnotation(typeof(RootAnnotation));
            XAssert.True(annotations.Length > 0);
        }

        [Fact]
        public void ScanClassTest()
        {
            // TargetClass -> TargetSuperClass ----------------------------------> SuperInterface
            //             -> TargetSuperInterface -> SuperTargetSuperInterface -> SuperInterface
            var annotations = AnnotationUtil.ScanClass(typeof(TargetClass));
            XAssert.True(annotations.Length > 0);
        }

        [Fact]
        public void ScanMethodTest()
        {
            // TargetClass -> TargetSuperClass
            //             -> TargetSuperInterface
            var method = typeof(TargetClass).GetMethod("TestMethod");
            XAssert.NotNull(method);
            var annotations = AnnotationUtil.ScanMethod(method);
            XAssert.True(annotations.Length > 0);
        }

        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation3 : Attribute
        {}

        [RootMetaAnnotation3]
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation2 : Attribute
        {}

        [RootMetaAnnotation2]
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation1 : Attribute
        {}

        [RootMetaAnnotation3]
        [RootMetaAnnotation1]
        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        private class RootAnnotation : Attribute
        {}

        [AnnotationForTest("TargetClass")]
        private class TargetClass : TargetSuperClass, TargetSuperInterface
        {
            [AnnotationForTest("TargetClass")]
            public override object TestMethod() { return new List<object>(); }
        }

        [AnnotationForTest("TargetSuperClass")]
        private class TargetSuperClass : SuperInterface
        {
            [AnnotationForTest("TargetSuperClass")]
            public virtual object TestMethod() { return new List<object>(); }
        }

        private interface TargetSuperInterface : SuperTargetSuperInterface
        {
            object TestMethod();
        }

        private interface SuperTargetSuperInterface : SuperInterface
        {}

        private interface SuperInterface
        {}
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class AnnotationForTest : Attribute
    {
        public string Value { get; set; } = "";

        public string Retry { get; set; } = "";

        public string[] Names { get; set; } = new string[] { "" };

        public AnnotationForTest()
        {}

        public AnnotationForTest(string value)
        {
            Value = value;
        }
    }

    [AnnotationForTest("repeat-annotation")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RepeatAnnotationForTest : Attribute
    {
    }

    // 注解扫描器测试
    public class AnnotationScannerTests
    {
        [AnnotationForTest(Value = "测试", Names = new[] { "测试1", "测试2" })]
        [RepeatAnnotationForTest]
        private class ClassWithAnnotation
        {
            public void TestMethod() {}
        }

        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation3 : Attribute
        {}

        [RootMetaAnnotation3]
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation2 : Attribute
        {}

        [RootMetaAnnotation2]
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
        private class RootMetaAnnotation1 : Attribute
        {}

        [RootMetaAnnotation3]
        [RootMetaAnnotation1]
        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        private class RootAnnotation : Attribute
        {}

        [AnnotationForTest("TargetClass")]
        private class TargetClass : TargetSuperClass, TargetSuperInterface
        {
            [AnnotationForTest("TargetClass")]
            public override object TestMethod() { return new List<object>(); }
        }

        [AnnotationForTest("TargetSuperClass")]
        private class TargetSuperClass : SuperInterface
        {
            [AnnotationForTest("TargetSuperClass")]
            public virtual object TestMethod() { return new List<object>(); }
        }

        private interface TargetSuperInterface : SuperTargetSuperInterface
        {
            object TestMethod();
        }

        private interface SuperTargetSuperInterface : SuperInterface
        {}

        private interface SuperInterface
        {}

        [Fact]
        public void TestElementAnnotationScanner()
        {
            var scanner = new ElementAnnotationScanner();
            var annotations = scanner.GetAnnotations(typeof(ClassWithAnnotation));
            XAssert.NotNull(annotations);
        }

        [Fact]
        public void TestFieldAnnotationScanner()
        {
            var scanner = new FieldAnnotationScanner();
            var field = typeof(ClassWithFieldAnnotation).GetField("TestField");
            XAssert.NotNull(field);
            var annotations = scanner.GetAnnotations(field);
            XAssert.NotNull(annotations);
        }

        [Fact]
        public void TestGenericAnnotationScanner()
        {
            var scanner = new GenericAnnotationScanner(true, true, true);
            var annotations = scanner.GetAnnotations(typeof(ClassWithAnnotation));
            XAssert.NotNull(annotations);
        }

        [Fact]
        public void TestMetaAnnotationScanner()
        {
            var scanner = new MetaAnnotationScanner();
            var annotations = scanner.GetAnnotations(typeof(RootAnnotation));
            XAssert.NotNull(annotations);
        }

        [Fact]
        public void TestMethodAnnotationScanner()
        {
            var scanner = new MethodAnnotationScanner();
            var method = typeof(TargetClass).GetMethod("TestMethod");
            XAssert.NotNull(method);
            var annotations = scanner.GetAnnotations(method);
            XAssert.NotNull(annotations);
        }

        [Fact]
        public void TestTypeAnnotationScanner()
        {
            var scanner = new TypeAnnotationScanner();
            var annotations = scanner.GetAnnotations(typeof(ClassWithAnnotation));
            XAssert.NotNull(annotations);
        }

        [AnnotationForTest("field-annotation")]
        private class ClassWithFieldAnnotation
        {
            public string TestField;
        }
    }
}