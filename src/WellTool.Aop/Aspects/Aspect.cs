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

namespace WellTool.Aop.Aspects
{
    /// <summary>
    /// 切面接口
    /// </summary>
    public interface Aspect
    {
        /// <summary>
        /// 目标方法执行前的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <returns>是否继续执行接下来的操作</returns>
        bool Before(object target, MethodInfo method, object[] args);

        /// <summary>
        /// 目标方法执行后的操作
        /// 如果 target.method 抛出异常且 <see cref="AfterException"/> 返回true,则不会执行此操作
        /// 如果 <see cref="AfterException"/> 返回false,则无论target.method是否抛出异常，均会执行此操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <param name="returnVal">目标方法执行返回值</param>
        /// <returns>是否允许返回值（接下来的操作）</returns>
        bool After(object target, MethodInfo method, object[] args, object returnVal);

        /// <summary>
        /// 目标方法抛出异常时的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <param name="e">异常</param>
        /// <returns>是否允许抛出异常</returns>
        bool AfterException(object target, MethodInfo method, object[] args, Exception e);

        /// <summary>
        /// 目标方法执行完成后的操作，无论是否抛出异常
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        void AfterFinally(object target, MethodInfo method, object[] args);
    }
}