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
using WellTool.Core.Annotation.Scanner;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests.Annotation.Scanner;

/// <summary>
/// 元注解扫描器测试
/// </summary>
public class MateAnnotationScannerTest
{
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

    [Fact]
    public void TestMetaAnnotationScanner()
    {
        var scanner = new MetaAnnotationScanner();
        var annotations = scanner.GetAnnotations(typeof(RootAnnotation));
        XAssert.NotNull(annotations);
    }
}
