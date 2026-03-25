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
using WellTool.Aop.Aspects;

namespace WellTool.Aop
{
    /// <summary>
    /// AOP 示例
    /// </summary>
    public class Examples
    {
        public static void TestAop()
        {
            // 创建目标对象
            var target = new TestService();

            // 创建切面
            var aspect = new TestAspect();

            // 创建代理对象
            ITestService proxy = ProxyUtil.Proxy(target, aspect);

            // 调用代理方法
            Console.WriteLine("调用 SayHello 方法:");
            proxy.SayHello("World");

            Console.WriteLine("\n调用 Divide 方法:");
            try
            {
                proxy.Divide(10, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获到异常: {ex.Message}");
            }
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
            Console.WriteLine($"TestService.SayHello: Hello, {name}!");
            return $"Hello, {name}!";
        }

        public int Divide(int a, int b)
        {
            Console.WriteLine($"TestService.Divide: {a} / {b}");
            return a / b;
        }
    }

    /// <summary>
    /// 测试切面
    /// </summary>
    public class TestAspect : Aspect
    {
        public bool Before(object target, MethodInfo method, object[] args)
        {
            Console.WriteLine($"TestAspect.Before: {method.Name} 方法开始执行");
            return true;
        }

        public bool After(object target, MethodInfo method, object[] args, object returnVal)
        {
            Console.WriteLine($"TestAspect.After: {method.Name} 方法执行完成，返回值: {returnVal}");
            return true;
        }

        public bool AfterException(object target, MethodInfo method, object[] args, Exception e)
        {
            Console.WriteLine($"TestAspect.AfterException: {method.Name} 方法执行异常: {e.Message}");
            return true;
        }
    }
}