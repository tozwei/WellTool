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

using System.Data;

namespace WellTool.Extra;

/// <summary>
/// 表达式工具类
/// </summary>
public class ExpressionUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ExpressionUtil Instance { get; } = new ExpressionUtil();

    /// <summary>
    /// 执行表达式
    /// </summary>
    /// <param name="expression">表达式字符串</param>
    /// <param name="parameters">参数</param>
    /// <returns>执行结果</returns>
    public object Evaluate(string expression, IDictionary<string, object> parameters = null)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ExpressionException("表达式不能为空");
        }

        try
        {
            using var dataTable = new DataTable();
            
            if (parameters != null && parameters.Count > 0)
            {
                var processedExpression = expression;
                foreach (var param in parameters)
                {
                    var value = param.Value;
                    string replacement;
                    if (value is string strValue)
                    {
                        replacement = $"'{strValue}'";
                    }
                    else if (value == null)
                    {
                        replacement = "0";
                    }
                    else
                    {
                        replacement = value.ToString();
                    }
                    processedExpression = processedExpression.Replace(param.Key, replacement);
                }
                return dataTable.Compute(processedExpression, string.Empty);
            }
            
            return dataTable.Compute(expression, string.Empty);
        }
        catch (EvaluateException ex)
        {
            throw new ExpressionException($"表达式执行失败: {ex.Message}", ex);
        }
        catch (System.Exception ex)
        {
            throw new ExpressionException("表达式执行失败", ex);
        }
    }

    /// <summary>
    /// 执行表达式并转换为指定类型
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    /// <param name="expression">表达式字符串</param>
    /// <param name="parameters">参数</param>
    /// <returns>执行结果</returns>
    public T Evaluate<T>(string expression, IDictionary<string, object> parameters = null)
    {
        var result = Evaluate(expression, parameters);
        
        if (result == null)
        {
            return default;
        }
        
        // 确保返回正确的类型
        if (typeof(T) == typeof(int))
        {
            if (result is int intResult)
                return (T)(object)intResult;
            if (result is double doubleResult)
                return (T)(object)(int)doubleResult;
            if (result is decimal decimalResult)
                return (T)(object)(int)decimalResult;
            return (T)Convert.ChangeType(result, typeof(T));
        }
        
        if (typeof(T) == typeof(double))
        {
            if (result is double dResult)
                return (T)(object)dResult;
            if (result is int intResult)
                return (T)(object)(double)intResult;
            if (result is decimal decimalResult)
                return (T)(object)(double)decimalResult;
            return (T)Convert.ChangeType(result, typeof(T));
        }
        
        if (typeof(T) == typeof(decimal))
        {
            if (result is decimal decResult)
                return (T)(object)decResult;
            if (result is double dResult)
                return (T)(object)(decimal)dResult;
            if (result is int intResult)
                return (T)(object)(decimal)intResult;
            return (T)Convert.ChangeType(result, typeof(T));
        }
        
        return (T)Convert.ChangeType(result, typeof(T));
    }
}

/// <summary>
/// 表达式异常
/// </summary>
public class ExpressionException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public ExpressionException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public ExpressionException(string message, Exception innerException) : base(message, innerException)
    {}
}
