using System;
using System.Collections.Generic;

namespace WellTool.Extra.Expression.Engine.QlExpress
{
    /// <summary>
    /// QLExpress表达式引擎实现
    /// 
    /// 需要安装 QLExpress 或类似 NuGet 包
    /// 项目地址：https://github.com/alibaba/QLExpress
    /// </summary>
    public class QlExpressEngine : ExpressionEngine
    {
        /// <summary>
        /// 构造
        /// </summary>
        public QlExpressEngine()
        {
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="env">环境变量</param>
        /// <returns>执行结果</returns>
        public override object Eval(string expression, IDictionary<string, object> env)
        {
            // TODO: 需要集成 QLExpress.NET 或类似库
            return null;
        }
    }
}
