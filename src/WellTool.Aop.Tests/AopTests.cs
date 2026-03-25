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

using System.Reflection;
using WellTool.Aop;
using WellTool.Aop.Aspects;

namespace WellTool.Aop.Tests
{
    public class AopTests
    {
        [Fact]
        public void TestAopBasicFunctionality()
        {
            // Arrange
            var target = new TestService();
            var testAspect = new TestAspect();

            // Act
            ITestService proxy = ProxyUtil.Proxy<ITestService>(target, testAspect);
            var result = proxy.SayHello("Test");

            // Assert
            Assert.Equal("Hello, Test!", result);
            Assert.True(testAspect.BeforeCalled);
            Assert.True(testAspect.AfterCalled);
            Assert.False(testAspect.AfterExceptionCalled);
        }

        [Fact]
        public void TestAopExceptionHandling()
        {
            // Arrange
            var target = new TestService();
            var testAspect = new TestAspect();

            // Act
            ITestService proxy = ProxyUtil.Proxy<ITestService>(target, testAspect);
            var exception = Assert.Throws<DivideByZeroException>(() => proxy.Divide(10, 0));

            // Assert
            Assert.NotNull(exception);
            Assert.True(testAspect.BeforeCalled);
            Assert.False(testAspect.AfterCalled);
            Assert.True(testAspect.AfterExceptionCalled);
        }

        [Fact]
        public void TestAopWithAspectClass()
        {
            // Arrange
            var target = new TestService();

            // Act
            ITestService proxy = ProxyUtil.Proxy<ITestService>(target, typeof(TestAspect));
            var result = proxy.SayHello("Test with Class");

            // Assert
            Assert.Equal("Hello, Test with Class!", result);
        }
    }

    /// <summary>
    /// 测试服务接口
    /// </summary>
    public interface ITestService
    {
        string SayHello(string name);
        int Divide(int a, int b);
    }

    /// <summary>
    /// 测试服务实现
    /// </summary>
    public class TestService : ITestService
    {
        public string SayHello(string name)
        {
            return $"Hello, {name}!";
        }

        public int Divide(int a, int b)
        {
            return a / b;
        }
    }

    /// <summary>
    /// 测试切面
    /// </summary>
    public class TestAspect : Aspect
    {
        public bool BeforeCalled { get; private set; } = false;
        public bool AfterCalled { get; private set; } = false;
        public bool AfterExceptionCalled { get; private set; } = false;

        public bool Before(object target, MethodInfo method, object[] args)
        {
            BeforeCalled = true;
            return true;
        }

        public bool After(object target, MethodInfo method, object[] args, object returnVal)
        {
            AfterCalled = true;
            return true;
        }

        public bool AfterException(object target, MethodInfo method, object[] args, Exception e)
        {
            AfterExceptionCalled = true;
            return true;
        }
    }
}