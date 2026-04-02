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
/// 字段注解扫描器测试
/// </summary>
public class FieldAnnotationScannerTest
{
    [AnnotationForScannerTest(Value = "field-annotation")]
    private class ClassWithFieldAnnotation
    {
        public string TestField;
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
}
