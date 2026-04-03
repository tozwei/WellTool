namespace WellTool.Extra.Expression;

/// <summary>
/// 表达式引擎API接口，通过实现此接口，完成表达式的解析和执行
/// </summary>
public interface IExpressionEngine
{
    /// <summary>
    /// 执行表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="context">表达式上下文，用于存储表达式中所需的变量值等</param>
    /// <param name="allowClassSet">允许的Class白名单</param>
    /// <returns>执行结果</returns>
    object? Eval(string expression, Dictionary<string, object?> context, HashSet<Type>? allowClassSet = null);
}
