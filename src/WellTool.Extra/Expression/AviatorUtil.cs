using System.Collections.Generic;

namespace WellTool.Extra.Expression;

/// <summary>
/// Aviator表达式引擎工具类
/// </summary>
public static class AviatorUtil
{
    /// <summary>
    /// 执行表达式
    /// </summary>
    /// <param name="expression">表达式字符串</param>
    /// <param name="env">环境变量</param>
    /// <returns>执行结果</returns>
    public static object Exec(string expression, IDictionary<string, object> env = null)
    {
        // 简化实现
        if (string.IsNullOrEmpty(expression))
        {
            return null;
        }

        // 使用 ExpressionUtil 的实现
        if (env != null && env.Count > 0)
        {
            return WellTool.Extra.ExpressionUtil.Instance.Evaluate(expression, env);
        }
        return WellTool.Extra.ExpressionUtil.Instance.Evaluate(expression);
    }

    /// <summary>
    /// 编译表达式
    /// </summary>
    /// <param name="expression">表达式字符串</param>
    /// <returns>编译后的表达式对象</returns>
    public static object Compile(string expression)
    {
        // 简化实现，直接返回表达式字符串
        return expression;
    }

    /// <summary>
    /// 执行编译后的表达式
    /// </summary>
    /// <param name="compiled">编译后的表达式</param>
    /// <param name="env">环境变量</param>
    /// <returns>执行结果</returns>
    public static object Execute(object compiled, IDictionary<string, object> env = null)
    {
        if (compiled is string expression)
        {
            return Exec(expression, env);
        }
        return null;
    }
}