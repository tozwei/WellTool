using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WellTool.Extra.Validation
{
    /// <summary>
/// java bean 校验工具类，此工具类基于 System.ComponentModel.DataAnnotations 封装
/// </summary>
public class ValidationUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ValidationUtil Instance { get; } = new ValidationUtil();

    /// <summary>
    /// 校验对象
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <param name="bean">bean</param>
    /// <returns>校验结果集合</returns>
    public ICollection<ValidationResult> Validate<T>(T bean)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(bean, new ValidationContext(bean), validationResults, true);
            return validationResults;
        }

        /// <summary>
    /// 校验对象
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <param name="bean">bean</param>
    /// <returns>BeanValidationResult</returns>
    public BeanValidationResult WarpValidate<T>(T bean)
        {
            var validationResults = Validate(bean);
            return WarpBeanValidationResult(validationResults);
        }

        /// <summary>
    /// 包装校验结果
    /// </summary>
    /// <param name="validationResults">校验结果集</param>
    /// <returns>BeanValidationResult</returns>
    private BeanValidationResult WarpBeanValidationResult(ICollection<ValidationResult> validationResults)
        {
            BeanValidationResult result = new BeanValidationResult(validationResults.Count == 0);
            foreach (var validationResult in validationResults)
            {
                var errorMessage = new BeanValidationResult.ErrorMessage
                {
                    PropertyName = validationResult.MemberNames.FirstOrDefault() ?? string.Empty,
                    Message = validationResult.ErrorMessage,
                    Value = null // C# 的 ValidationResult 不包含无效值信息
                };
                result.AddErrorMessage(errorMessage);
            }
            return result;
        }
    }
}