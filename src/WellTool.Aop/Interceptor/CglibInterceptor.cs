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

namespace WellTool.Aop.Interceptor
{
    /// <summary>
    /// Cglib代理拦截器
    /// </summary>
    public class CglibInterceptor
    {
        private readonly Aspect _aspect;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aspect">切面实现</param>
        public CglibInterceptor(Aspect aspect)
        {
            _aspect = aspect;
        }

        /// <summary>
        /// 拦截方法调用
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">方法信息</param>
        /// <param name="parameters">方法参数</param>
        /// <param name="methodProxy">方法代理</param>
        /// <returns>方法执行结果</returns>
        public object Intercept(object target, MethodInfo method, object[] parameters, object methodProxy)
        {
            // 执行前置通知
            var result = _aspect.Before(target, method, parameters);
            if (result != null)
            {
                return result;
            }

            try
            {
                // 执行目标方法
                var returnValue = method.Invoke(target, parameters);

                // 执行后置通知
                return _aspect.After(target, method, parameters, returnValue);
            }
            catch (Exception ex)
            {
                // 执行异常通知
                var exceptionResult = _aspect.AfterException(target, method, parameters, ex);
                if (exceptionResult != null)
                {
                    return exceptionResult;
                }
                throw;
            }
            finally
            {
                // 执行最终通知
                _aspect.AfterFinally(target, method, parameters);
            }
        }
    }
}