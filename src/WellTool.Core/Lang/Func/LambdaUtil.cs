using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using WellTool.Core.Bean;
using WellTool.Core.Util;

namespace WellTool.Core.Lang.Func
{
    /// <summary>
    /// Lambda相关工具类
    /// </summary>
    public class LambdaUtil
    {
        private static readonly ConcurrentDictionary<string, Type> _typeCache = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 通过对象的方法或类的静态方法引用，获取lambda实现类
        /// </summary>
        /// <typeparam name="R">类型</typeparam>
        /// <param name="func">lambda</param>
        /// <returns>lambda实现类</returns>
        public static Type GetRealClass<R>(System.Linq.Expressions.Expression<Func0<R>> func)
        {
            var expression = GetExpression(func);
            return GetRealClassFromExpression(expression);
        }

        /// <summary>
        /// 通过对象的方法或类的静态方法引用，获取lambda实现类
        /// </summary>
        /// <typeparam name="P">方法调用方类型</typeparam>
        /// <typeparam name="R">返回值类型</typeparam>
        /// <param name="func">lambda</param>
        /// <returns>lambda实现类</returns>
        public static Type GetRealClass<P, R>(System.Linq.Expressions.Expression<Func1<P, R>> func)
        {
            var expression = GetExpression(func);
            return GetRealClassFromExpression(expression);
        }

        /// <summary>
        /// 获取lambda表达式函数（方法）名称
        /// </summary>
        /// <typeparam name="P">Lambda参数类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>函数名称</returns>
        public static string GetMethodName<P>(System.Linq.Expressions.Expression<Func1<P, object>> func)
        {
            var expression = GetExpression(func);
            return GetMethodNameFromExpression(expression);
        }

        /// <summary>
        /// 获取lambda表达式函数（方法）名称
        /// </summary>
        /// <typeparam name="R">Lambda返回类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>函数名称</returns>
        public static string GetMethodName<R>(System.Linq.Expressions.Expression<Func0<R>> func)
        {
            var expression = GetExpression(func);
            return GetMethodNameFromExpression(expression);
        }

        /// <summary>
        /// 获取lambda表达式Getter或Setter函数（方法）对应的字段名称
        /// </summary>
        /// <typeparam name="T">Lambda类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>字段名称</returns>
        public static string GetFieldName<T>(System.Linq.Expressions.Expression<Func1<T, object>> func)
        {
            var methodName = GetMethodName(func);
            return BeanUtil.GetFieldName(methodName);
        }

        /// <summary>
        /// 获取lambda表达式Getter或Setter函数（方法）对应的字段名称
        /// </summary>
        /// <typeparam name="T">Lambda类型</typeparam>
        /// <param name="func">函数</param>
        /// <returns>字段名称</returns>
        public static string GetFieldName<T>(System.Linq.Expressions.Expression<Func0<T>> func)
        {
            var methodName = GetMethodName(func);
            return BeanUtil.GetFieldName(methodName);
        }

        //region Private methods
        /// <summary>
        /// 从Lambda表达式中获取Expression
        /// </summary>
        /// <typeparam name="T">Lambda类型</typeparam>
        /// <param name="func">Lambda表达式</param>
        /// <returns>Expression</returns>
        private static Expression GetExpression<T>(System.Linq.Expressions.Expression<Func0<T>> func)
        {
            return func.Body;
        }

        /// <summary>
        /// 从Lambda表达式中获取Expression
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="R">返回类型</typeparam>
        /// <param name="func">Lambda表达式</param>
        /// <returns>Expression</returns>
        private static Expression GetExpression<T, R>(System.Linq.Expressions.Expression<Func1<T, R>> func)
        {
            return func.Body;
        }

        /// <summary>
        /// 从Expression中获取真实类型
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>真实类型</returns>
        private static Type GetRealClassFromExpression(Expression expression)
        {
            if (expression is MethodCallExpression methodCall)
            {
                if (methodCall.Object != null)
                {
                    return methodCall.Object.Type;
                }
                return methodCall.Method.DeclaringType;
            }
            throw new ArgumentException("该lambda不是合适的方法引用");
        }

        /// <summary>
        /// 从Expression中获取方法名称
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>方法名称</returns>
        private static string GetMethodNameFromExpression(Expression expression)
        {
            if (expression is MethodCallExpression methodCall)
            {
                return methodCall.Method.Name;
            }
            throw new ArgumentException("该lambda不是方法引用");
        }
        //endregion
    }
}