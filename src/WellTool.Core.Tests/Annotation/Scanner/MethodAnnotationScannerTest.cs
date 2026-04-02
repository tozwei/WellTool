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
using WellTool.Core.Annotation.Scanner;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests.Annotation.Scanner;

/// <summary>
/// 方法注解扫描器测试
/// </summary>
public class MethodAnnotationScannerTest
{
    [AnnotationForScannerTest("TargetClass")]
    private class TargetClass : TargetSuperClass, TargetSuperInterface
    {
        [AnnotationForScannerTest("TargetClass")]
        public override object TestMethod() { return new object(); }
    }

    [AnnotationForScannerTest("TargetSuperClass")]
    private class TargetSuperClass : SuperInterface
    {
        [AnnotationForScannerTest("TargetSuperClass")]
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

    [Fact]
    public void TestMethodAnnotationScanner()
    {
        var scanner = new MethodAnnotationScanner();
        var method = typeof(TargetClass).GetMethod("TestMethod");
        XAssert.NotNull(method);
        var annotations = scanner.GetAnnotations(method);
        XAssert.NotNull(annotations);
    }
}
