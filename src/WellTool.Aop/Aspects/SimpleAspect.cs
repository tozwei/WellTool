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
    /// 简单切面类，不做任何操作
    /// 可以继承此类实现自己需要的方法即可
    /// </summary>
    public class SimpleAspect : Aspect
    {
        /// <summary>
        /// 目标方法执行前的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <returns>是否继续执行接下来的操作</returns>
        public virtual bool Before(object target, MethodInfo method, object[] args)
        {
            //继承此类后实现此方法
            return true;
        }

        /// <summary>
        /// 目标方法执行后的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <param name="returnVal">目标方法执行返回值</param>
        /// <returns>是否允许返回值（接下来的操作）</returns>
        public virtual bool After(object target, MethodInfo method, object[] args, object returnVal)
        {
            //继承此类后实现此方法
            return true;
        }

        /// <summary>
        /// 目标方法抛出异常时的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <param name="e">异常</param>
        /// <returns>是否允许抛出异常</returns>
        public virtual bool AfterException(object target, MethodInfo method, object[] args, Exception e)
        {
            //继承此类后实现此方法
            return true;
        }
    }
}