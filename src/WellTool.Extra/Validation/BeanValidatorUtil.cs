using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


namespace WellTool.Extra.Validation;

/// <summary>
/// 标记特性，用于指示验证器需要递归验证此属性。
/// 用法：[Valid] public Address Address { get; set; }
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidAttribute : Attribute
{
}

/// <summary>
/// Bean验证工具类
/// </summary>
public static class BeanValidatorUtil
{
    /// <summary>
    /// 验证Bean对象
    /// </summary>
    /// <param name="bean">要验证的Bean</param>
    /// <returns>验证是否通过</returns>
    public static bool Validate(object bean)
    {
        var errors = ValidateEntity(bean);
        return errors.Count == 0;
    }

    /// <summary>
    /// 验证Bean对象并返回错误信息
    /// </summary>
    /// <param name="bean">要验证的Bean</param>
    /// <returns>错误信息列表</returns>
    public static List<string> ValidateEntity(object bean)
    {
        var errors = new List<string>();

        if (bean == null)
        {
            errors.Add("Bean cannot be null");
            return errors;
        }

        var context = new ValidationContext(bean, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        // 执行标准验证
        bool isValid = Validator.TryValidateObject(bean, context, results, validateAllProperties: true);

        if (!isValid)
        {
            errors.AddRange(results.Select(r => r.ErrorMessage));
        }

        // 递归验证嵌套对象 (如果属性也是复杂对象)
        ValidateNavigationProperties(bean, errors, context);

        return errors;
    }

    /// <summary>
    /// 获取约束违规列表
    /// </summary>
    /// <param name="bean">要验证的Bean</param>
    /// <returns>约束违规列表 (返回 ValidationResult 对象以便获取详细信息)</returns>
    public static List<ValidationResult> GetConstraintViolations(object bean)
    {
        var results = new List<ValidationResult>();

        if (bean == null)
        {
            results.Add(new ValidationResult("Bean cannot be null"));
            return results;
        }

        var context = new ValidationContext(bean, serviceProvider: null, items: null);
        Validator.TryValidateObject(bean, context, results, validateAllProperties: true);

        // 注意：这里简化处理，未包含递归验证嵌套对象的详细违规信息，
        // 如需包含，需类似 ValidateEntity 那样递归收集。

        return results;
    }

    /// <summary>
    /// 验证Bean对象
    /// </summary>
    /// <param name="bean">要验证的Bean</param>
    /// <param name="group">验证组 (目前实现仅作为标记，实际逻辑复用基础验证)</param>
    /// <returns>验证是否通过</returns>
    public static bool Validate(object bean, Type group)
    {
        // 如果需要支持 ValidationContext 的 ValidationGroup，可以在此处扩展
        // 目前简化实现直接调用基础验证
        return Validate(bean);
    }

    /// <summary>
    /// 递归验证导航属性 (嵌套对象)
    /// </summary>
    private static void ValidateNavigationProperties(object bean, List<string> errors, ValidationContext parentContext)
    {
        if (bean == null) return;

        var type = bean.GetType();
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // 跳过值类型和字符串，只检查复杂对象
            if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                continue;

            // 检查是否标记了 Valid 特性 (用于触发嵌套验证)
            var hasValidAttribute = property.GetCustomAttribute<ValidAttribute>() != null;

            // 如果没有 Valid 特性，但你想强制验证所有嵌套对象，可以移除这个判断
            if (!hasValidAttribute)
                continue;

            var propertyValue = property.GetValue(bean);

            if (propertyValue != null)
            {
                // 如果是集合
                if (propertyValue is IEnumerable enumerable && !(propertyValue is string))
                {
                    int index = 0;
                    foreach (var item in enumerable)
                    {
                        if (item != null)
                        {
                            var itemContext = new ValidationContext(item, parentContext, null)
                            {
                                DisplayName = $"{parentContext.DisplayName}.{property.Name}[{index}]"
                            };
                            var itemResults = new List<ValidationResult>();
                            Validator.TryValidateObject(item, itemContext, itemResults, true);

                            if (itemResults.Count > 0)
                            {
                                errors.AddRange(itemResults.Select(r => $"{itemContext.DisplayName}: {r.ErrorMessage}"));
                            }

                            // 递归更深层次
                            ValidateNavigationProperties(item, errors, itemContext);
                        }
                        index++;
                    }
                }
                // 如果是单个对象
                else
                {
                    var subContext = new ValidationContext(propertyValue, parentContext, null)
                    {
                        DisplayName = $"{parentContext.DisplayName}.{property.Name}"
                    };
                    var subResults = new List<ValidationResult>();
                    Validator.TryValidateObject(propertyValue, subContext, subResults, true);

                    if (subResults.Count > 0)
                    {
                        errors.AddRange(subResults.Select(r => $"{subContext.DisplayName}: {r.ErrorMessage}"));
                    }

                    // 递归更深层次
                    ValidateNavigationProperties(propertyValue, errors, subContext);
                }
            }
        }
    }
}