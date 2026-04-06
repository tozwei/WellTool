using System;
using System.Collections.Generic;
using WellTool.Extra.Expression;

namespace WellTool.Extra.Expression.Engine.Rhino
{
    /// <summary>
    /// Rhino表达式引擎实现
    /// 
    /// 需要安装 Rhino 或类似 NuGet 包
    /// 项目地址：https://github.com/mozilla/rhino
    /// </summary>
    public class RhinoEngine : IExpressionEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RhinoEngine()
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
            // TODO: 需要集成 Rhino.NET 或 Microsoft.JavaScript 或类似库
            return null;
        }
    }
}
