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
using System.Reflection;
using Xunit;
using WellTool.Core.Annotation;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests.Annotation;

/// <summary>
/// 注解工具测试
/// </summary>
public class AnnotationUtilTest
{
    [AnnotationForTestAttribute(Value = "测试", Names = new[] { "测试1", "测试2" })]
    [RepeatAnnotationForTestAttribute]
    private class ClassWithAnnotation
    {
        public void Test()
        {
        }
    }

    [Fact]
    public void GetCombinationAnnotationsTest()
    {
        var annotations = AnnotationUtil.GetAnnotations(typeof(ClassWithAnnotation), true);
        XAssert.NotNull(annotations);
        XAssert.True(annotations.Length > 0);
    }

    [Fact]
    public void GetCombinationAnnotationsWithClassTest()
    {
        var annotations = AnnotationUtil.GetCombinationAnnotations<AnnotationForTestAttribute>(typeof(ClassWithAnnotation));
        XAssert.NotNull(annotations);
        XAssert.True(annotations.Length > 0);
    }

    [Fact]
    public void GetAnnotationValueTest()
    {
        var value = AnnotationUtil.GetAnnotationValue(typeof(ClassWithAnnotation), typeof(AnnotationForTestAttribute));
        XAssert.NotNull(value);
    }

    [Fact]
    public void GetAnnotationValueTest2()
    {
        var names = AnnotationUtil.GetAnnotationValue(typeof(ClassWithAnnotation), (AnnotationForTestAttribute a) => a.Names);
        XAssert.NotNull(names);
        XAssert.True(names.Length > 0);
    }

    [Fact]
    public void GetAnnotationSyncAlias()
    {
        // 直接获取
        var annotation = typeof(ClassWithAnnotation).GetCustomAttribute<AnnotationForTestAttribute>();
        XAssert.NotNull(annotation);

        // 加别名适配
        var aliasAnnotation = AnnotationUtil.GetAnnotationAlias<AnnotationForTestAttribute>(typeof(ClassWithAnnotation));
        XAssert.NotNull(aliasAnnotation);
        XAssert.True(AnnotationUtil.IsSynthesizedAnnotation(aliasAnnotation));
    }

    [Fact]
    public void GetAnnotationSyncAliasWhenNotAnnotation()
    {
        GetAnnotationSyncAlias();
        // 使用AnnotationUtil.GetAnnotationAlias获取对象上并不存在的注解
        var alias = AnnotationUtil.GetAnnotationAlias<AliasAttribute>(typeof(ClassWithAnnotation));
        XAssert.Null(alias);
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

    [AnnotationForTestAttribute("TargetClass")]
    private class TargetClass : TargetSuperClass, TargetSuperInterface
    {
        [AnnotationForTestAttribute("TargetClass")]
        public override object TestMethod() { return new object(); }
    }

    [AnnotationForTestAttribute("TargetSuperClass")]
    private class TargetSuperClass : SuperInterface
    {
        [AnnotationForTestAttribute("TargetSuperClass")]
        public virtual object TestMethod() { return new object(); }
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
