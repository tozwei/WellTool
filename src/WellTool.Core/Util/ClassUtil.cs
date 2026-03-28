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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 类工具类
    /// </summary>
    public static class ClassUtil
    {
        /// <summary>
        /// 获取对象的类名
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>类名</returns>
        public static string GetClassName(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.GetType().FullName;
        }

        /// <summary>
        /// 获取类型的类名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类名</returns>
        public static string GetClassName(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return type.FullName;
        }

        /// <summary>
        /// 获取类型的简单类名
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>简单类名</returns>
        public static string GetSimpleClassName(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.GetType().Name;
        }

        /// <summary>
        /// 获取类型的简单类名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>简单类名</returns>
        public static string GetSimpleClassName(Type type)
        {
            if (type == null)
            {
                return null;
            }

            return type.Name;
        }

        /// <summary>
        /// 加载类
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns>类型</returns>
        public static Type LoadClass(string className)
        {
            return Type.GetType(className);
        }

        /// <summary>
        /// 加载类
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="classLoader">类加载器</param>
        /// <returns>类型</returns>
        public static Type LoadClass(string className, Assembly classLoader)
        {
            return classLoader.GetType(className);
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>对象实例</returns>
        public static object NewInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>对象实例</returns>
        public static T NewInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>对象实例</returns>
        public static object NewInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="args">构造函数参数</param>
        /// <returns>对象实例</returns>
        public static T NewInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// 获取类型的所有字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共字段</param>
        /// <returns>字段数组</returns>
        public static FieldInfo[] GetFields(Type type, bool includeNonPublic = false)
        {
            if (type == null)
            {
                return new FieldInfo[0];
            }

            var bindingFlags = BindingFlags.Instance | BindingFlags.Static;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            else
            {
                bindingFlags |= BindingFlags.Public;
            }

            return type.GetFields(bindingFlags);
        }

        /// <summary>
        /// 获取类型的所有属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共属性</param>
        /// <returns>属性数组</returns>
        public static PropertyInfo[] GetProperties(Type type, bool includeNonPublic = false)
        {
            if (type == null)
            {
                return new PropertyInfo[0];
            }

            var bindingFlags = BindingFlags.Instance | BindingFlags.Static;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            else
            {
                bindingFlags |= BindingFlags.Public;
            }

            return type.GetProperties(bindingFlags);
        }

        /// <summary>
        /// 获取类型的所有方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共方法</param>
        /// <returns>方法数组</returns>
        public static MethodInfo[] GetMethods(Type type, bool includeNonPublic = false)
        {
            if (type == null)
            {
                return new MethodInfo[0];
            }

            var bindingFlags = BindingFlags.Instance | BindingFlags.Static;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            else
            {
                bindingFlags |= BindingFlags.Public;
            }

            return type.GetMethods(bindingFlags);
        }

        /// <summary>
        /// 获取类型的所有构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共构造函数</param>
        /// <returns>构造函数数组</returns>
        public static ConstructorInfo[] GetConstructors(Type type, bool includeNonPublic = false)
        {
            if (type == null)
            {
                return new ConstructorInfo[0];
            }

            var bindingFlags = BindingFlags.Instance;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            else
            {
                bindingFlags |= BindingFlags.Public;
            }

            return type.GetConstructors(bindingFlags);
        }

        /// <summary>
        /// 检查类型是否为基本类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是基本类型则返回 true，否则返回 false</returns>
        public static bool IsPrimitive(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(DateTimeOffset);
        }

        /// <summary>
        /// 检查类型是否为数组
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是数组则返回 true，否则返回 false</returns>
        public static bool IsArray(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsArray;
        }

        /// <summary>
        /// 检查类型是否为集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是集合则返回 true，否则返回 false</returns>
        public static bool IsCollection(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return typeof(System.Collections.ICollection).IsAssignableFrom(type) || typeof(System.Collections.Generic.ICollection<>).IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查类型是否为枚举
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是枚举则返回 true，否则返回 false</returns>
        public static bool IsEnum(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsEnum;
        }

        /// <summary>
        /// 检查类型是否为接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是接口则返回 true，否则返回 false</returns>
        public static bool IsInterface(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsInterface;
        }

        /// <summary>
        /// 检查类型是否为抽象类
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是抽象类则返回 true，否则返回 false</returns>
        public static bool IsAbstract(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsAbstract;
        }

        /// <summary>
        /// 检查类型是否为静态类
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是静态类则返回 true，否则返回 false</returns>
        public static bool IsStatic(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsAbstract && type.IsSealed;
        }

        /// <summary>
        /// 检查类型是否为密封类
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是密封类则返回 true，否则返回 false</returns>
        public static bool IsSealed(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsSealed;
        }

        /// <summary>
        /// 检查类型是否为泛型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>如果是泛型则返回 true，否则返回 false</returns>
        public static bool IsGeneric(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsGenericType;
        }

        /// <summary>
        /// 获取类型的泛型参数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>泛型参数数组</returns>
        public static Type[] GetGenericArguments(Type type)
        {
            if (type == null || !type.IsGenericType)
            {
                return new Type[0];
            }

            return type.GetGenericArguments();
        }
    }
}
