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
using WellTool.Aop.Interceptor;

namespace WellTool.Aop.Proxy
{
    /// <summary>
    /// JDK实现的切面代理
    /// </summary>
    public class JdkProxyFactory : ProxyFactory
    {
        /// <summary>
        /// 获取单例
        /// </summary>
        public static JdkProxyFactory Instance { get; } = new JdkProxyFactory();

        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T">代理对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面实现</param>
        /// <returns>代理对象</returns>
        public override T Proxy<T>(T target, Aspect aspect)
        {
            var targetType = target.GetType();
            var interfaces = targetType.GetInterfaces();

            if (interfaces.Length == 0)
            {
                throw new ArgumentException("被代理对象必须实现至少一个接口");
            }

            // 创建代理类型
            var proxyType = CreateProxyType(targetType, interfaces);
            
            // 创建代理实例
            var interceptor = new JdkInterceptor(target, aspect);
            var proxy = Activator.CreateInstance(proxyType, interceptor);

            // 检查 T 是否为接口类型
            if (typeof(T).IsInterface)
            {
                // 直接返回代理对象，因为我们已经确保它实现了接口 T
                return (T)proxy;
            }
            else
            {
                // 如果 T 是具体类，尝试将代理对象转换为 T 实现的第一个接口
                var firstInterface = interfaces[0];
                if (proxy is object obj && firstInterface.IsInstanceOfType(obj))
                {
                    // 尝试将代理对象转换为接口类型，然后再转换为 T
                    // 注意：这只在 T 是接口类型时有效
                    // 对于具体类，我们无法直接转换，因为代理对象不是 T 的子类
                    throw new InvalidCastException($"无法将代理对象转换为具体类 {typeof(T).FullName}，请使用接口类型作为泛型参数");
                }
                throw new InvalidCastException($"无法将代理对象转换为类型 {typeof(T).FullName}，请使用接口类型作为泛型参数");
            }
        }

        /// <summary>
        /// 创建代理类型
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="interfaces">接口列表</param>
        /// <returns>代理类型</returns>
        private Type CreateProxyType(Type targetType, Type[] interfaces)
        {
            var assemblyName = new AssemblyName("WellTool.Aop.DynamicProxy");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicProxyModule");
            var typeBuilder = moduleBuilder.DefineType(
                $"Proxy_{Guid.NewGuid().ToString("N")}",
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                null, // 不继承任何类
                interfaces
            );

            // 添加拦截器字段
            var interceptorField = typeBuilder.DefineField("_interceptor", typeof(JdkInterceptor), FieldAttributes.Private);

            // 添加构造函数
            var ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                CallingConventions.Standard,
                new[] { typeof(JdkInterceptor) }
            );
            var ctorIL = ctorBuilder.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes)!);
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Ldarg_1);
            ctorIL.Emit(OpCodes.Stfld, interceptorField);
            ctorIL.Emit(OpCodes.Ret);

            // 为每个接口方法创建实现
            foreach (var iface in interfaces)
            {
                foreach (var method in iface.GetMethods())
                {
                    var methodBuilder = typeBuilder.DefineMethod(
                        method.Name,
                        MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot,
                        method.ReturnType,
                        method.GetParameters().Select(p => p.ParameterType).ToArray()
                    );

                    // 实现方法
                    var methodIL = methodBuilder.GetILGenerator();
                    var paramsArray = methodIL.DeclareLocal(typeof(object[]));
                    
                    // 准备参数数组
                    var paramCount = method.GetParameters().Length;
                    methodIL.Emit(OpCodes.Ldc_I4, paramCount);
                    methodIL.Emit(OpCodes.Newarr, typeof(object));
                    methodIL.Emit(OpCodes.Stloc, paramsArray);

                    // 填充参数数组
                    for (int i = 0; i < paramCount; i++)
                    {
                        methodIL.Emit(OpCodes.Ldloc, paramsArray);
                        methodIL.Emit(OpCodes.Ldc_I4, i);
                        methodIL.Emit(OpCodes.Ldarg, i + 1); // 0 is 'this', 1+ are parameters
                        methodIL.Emit(OpCodes.Box, method.GetParameters()[i].ParameterType);
                        methodIL.Emit(OpCodes.Stelem_Ref);
                    }

                    // 调用拦截器
                    methodIL.Emit(OpCodes.Ldarg_0);
                    methodIL.Emit(OpCodes.Ldfld, interceptorField);
                    methodIL.Emit(OpCodes.Ldarg_0); // 代理对象
                    methodIL.Emit(OpCodes.Ldtoken, method);
                    methodIL.Emit(OpCodes.Call, typeof(MethodBase).GetMethod("GetMethodFromHandle", new[] { typeof(RuntimeMethodHandle) })!);
                    methodIL.Emit(OpCodes.Castclass, typeof(MethodInfo)); // 方法信息
                    methodIL.Emit(OpCodes.Ldloc, paramsArray); // 参数数组
                    // 直接使用方法名调用
                    methodIL.Emit(OpCodes.Callvirt, typeof(JdkInterceptor).GetMethod("Invoke", BindingFlags.Public | BindingFlags.Instance));

                    // 处理返回值
                    if (method.ReturnType != typeof(void))
                    {
                        if (method.ReturnType.IsValueType)
                        {
                            methodIL.Emit(OpCodes.Unbox_Any, method.ReturnType);
                        }
                        else
                        {
                            methodIL.Emit(OpCodes.Castclass, method.ReturnType);
                        }
                    }
                    else
                    {
                        methodIL.Emit(OpCodes.Pop);
                    }

                    methodIL.Emit(OpCodes.Ret);

                    // 重写方法
                    typeBuilder.DefineMethodOverride(methodBuilder, method);
                }
            }

            return typeBuilder.CreateType()!;
        }
    }
}