using System;
using System.Collections.Generic;
using WellTool.Extra.Expression;

namespace WellTool.Extra.Expression.Engine.Aviator
{
    /// <summary>
    /// Aviator表达式引擎实现
    /// 
    /// 需要安装 Aviator 或类似 NuGet 包
    /// 项目地址：https://github.com/killme2008/aviatorscript
    /// </summary>
    public class AviatorEngine : IExpressionEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AviatorEngine()
        {
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="context">上下文</param>
        /// <param name="allowClassSet">允许的Class白名单</param>
        /// <returns>执行结果</returns>
        public object? Eval(string expression, Dictionary<string, object?> context, HashSet<Type>? allowClassSet = null)
        {
            // TODO: 需要集成 Aviator.NET 或类似库
            return null;
        }
    }
}
