using System;
using System.Collections.Generic;
using WellTool.Extra.Expression;

namespace WellTool.Extra.Expression.Engine
{
    /// <summary>
    /// 表达式引擎工厂
    /// </summary>
    public static class ExpressionFactory
    {
        private static IExpressionEngine _instance;

        /// <summary>
        /// 获取单例的表达式引擎
        /// </summary>
        /// <returns>表达式引擎</returns>
        public static IExpressionEngine Get()
        {
            if (_instance == null)
            {
                _instance = Create();
            }
            return _instance;
        }

        /// <summary>
        /// 创建表达式引擎
        /// </summary>
        /// <returns>表达式引擎</returns>
        public static IExpressionEngine Create()
        {
            var engine = DoCreate();
            return engine;
        }

        /// <summary>
        /// 执行创建表达式引擎
        /// </summary>
        /// <returns>表达式引擎</returns>
        private static IExpressionEngine DoCreate()
        {
            // 尝试加载可用的表达式引擎
            // 默认返回 Jint 引擎（基于 JavaScript 的表达式引擎）
            try
            {
                // 可以在这里添加更多的引擎检测逻辑
                // 例如检测是否有 Mvel, Aviator 等库
                return new JintExpressionEngine();
            }
            catch
            {
                throw new ExpressionException("No expression engine found! Please add one of the expression engine packages to your project!");
            }
        }
    }

    /// <summary>
    /// Jint表达式引擎实现（使用 Jint JavaScript 引擎）
    /// </summary>
    public class JintExpressionEngine : IExpressionEngine
    {
        /// <summary>
        /// 根据表达式计算结果
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="context">上下文变量</param>
        /// <param name="allowClassSet">允许的Class白名单</param>
        /// <returns>计算结果</returns>
        public object? Eval(string expression, Dictionary<string, object?> context, HashSet<Type>? allowClassSet = null)
        {
            // 使用 Jint 引擎执行表达式
            // 这里需要添加 Jint NuGet 包引用
            throw new NotImplementedException("Please add Jint package to use expression evaluation");
        }
    }
}
