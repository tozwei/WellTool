using System;
using System.Collections.Generic;
using WellTool.Extra.Expression;

namespace WellTool.Extra.Expression.Engine.JfireEL
{
    /// <summary>
    /// JfireEL引擎封装
    /// 项目地址：https://gitee.com/eric_ds/jfireEL
    /// </summary>
    public class JfireELEngine : IExpressionEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public JfireELEngine()
        {
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="context">上下文</param>
        /// <param name="allowClassSet">允许的Class白名单</param>
        /// <returns>计算结果</returns>
        public object? Eval(string expression, Dictionary<string, object?> context, HashSet<Type>? allowClassSet = null)
        {
            // JfireEL表达式引擎实现
            // 由于需要依赖 JfireEL 库，这里提供一个基础实现
            // 实际使用需要引入对应的 NuGet 包
            throw new NotImplementedException("JfireEL引擎需要依赖 JfireEL 库，请安装对应包后实现");
        }
    }
}
