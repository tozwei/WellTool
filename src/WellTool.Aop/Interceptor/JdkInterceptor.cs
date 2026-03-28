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
using System.Reflection.Emit;
using WellTool.Aop.Aspects;

namespace WellTool.Aop.Interceptor
{
    /// <summary>
    /// JDK实现的动态代理切面
    /// </summary>
    public class JdkInterceptor
    {
        private readonly object target;
        private readonly Aspect aspect;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面实现</param>
        public JdkInterceptor(object target, Aspect aspect)
        {
            this.target = target;
            this.aspect = aspect;
        }

        public object Target => target;

        /// <summary>
        /// 拦截方法调用
        /// </summary>
        /// <param name="proxy">代理对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">方法参数</param>
        /// <returns>方法返回值</returns>
        public object? Invoke(object proxy, MethodInfo method, object[]? args)
        {
            var target = this.target;
            var aspect = this.aspect;
            object? result = null;

            // 开始前回调
            var beforeResult = aspect.Before(target, method, args ?? Array.Empty<object>());
            
            // 记录调用情况
            Console.WriteLine("Before called: " + beforeResult);

            if (beforeResult)
            {
                // 在 .NET 中，我们不需要手动设置 IsAccessible，反射会自动处理

                try
                {
                    result = method.Invoke(method.IsStatic ? null : target, args);
                    
                    // 正常结束执行回调
                    var afterResult = aspect.After(target, method, args ?? Array.Empty<object>(), result);
                    Console.WriteLine("After called: " + afterResult);
                    
                    if (afterResult)
                    {
                        return result;
                    }
                }
                catch (TargetInvocationException e)
                {
                    // 异常回调（只捕获业务代码导致的异常，而非反射导致的异常）
                    var afterExceptionResult = aspect.AfterException(target, method, args ?? Array.Empty<object>(), e.InnerException!);
                    Console.WriteLine("AfterException called: " + afterExceptionResult);
                    
                    if (afterExceptionResult)
                    {
                        throw e.InnerException!;
                    }
                }
            }

            return null;
        }
    }
}