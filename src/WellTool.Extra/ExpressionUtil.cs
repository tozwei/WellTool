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
using System.Data.Common;

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
        try
        {
            // 简单实现，使用DataTable.Compute方法
            using var dataTable = new DataTable();
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    dataTable.Columns.Add(param.Key, param.Value.GetType());
                }
                var row = dataTable.NewRow();
                foreach (var param in parameters)
                {
                    row[param.Key] = param.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable.Compute(expression, string.Empty);
        }
        catch (Exception ex)
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