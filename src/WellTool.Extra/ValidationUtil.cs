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

using System.ComponentModel.DataAnnotations;

namespace WellTool.Extra;

/// <summary>
/// 验证工具类
/// </summary>
public class ValidationUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static ValidationUtil Instance { get; } = new ValidationUtil();

    /// <summary>
    /// 验证对象
    /// </summary>
    /// <param name="obj">要验证的对象</param>
    /// <returns>验证结果</returns>
    public ValidationResult Validate(object obj)
    {
        try
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            
            return new ValidationResult(isValid, validationResults);
        }
        catch (Exception ex)
        {
            throw new ValidationException("验证失败", ex);
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的电子邮件地址
    /// </summary>
    /// <param name="email">电子邮件地址</param>
    /// <returns>是否有效</returns>
    public bool IsValidEmail(string email)
    {
        try
        {
            var emailAttribute = new EmailAddressAttribute();
            return emailAttribute.IsValid(email);
        }
        catch (Exception ex)
        {
            throw new ValidationException("验证电子邮件失败", ex);
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的URL
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>是否有效</returns>
    public bool IsValidUrl(string url)
    {
        try
        {
            var urlAttribute = new UrlAttribute();
            return urlAttribute.IsValid(url);
        }
        catch (Exception ex)
        {
            throw new ValidationException("验证URL失败", ex);
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的手机号
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <returns>是否有效</returns>
    public bool IsValidPhone(string phone)
    {
        try
        {
            // 简单实现，实际项目中可能需要更复杂的验证规则
            return System.Text.RegularExpressions.Regex.IsMatch(phone, "^1[3-9]\\d{9}$");
        }
        catch (Exception ex)
        {
            throw new ValidationException("验证手机号失败", ex);
        }
    }

    /// <summary>
    /// 验证字符串是否为有效的身份证号
    /// </summary>
    /// <param name="idCard">身份证号</param>
    /// <returns>是否有效</returns>
    public bool IsValidIdCard(string idCard)
    {
        try
        {
            // 简单实现，实际项目中可能需要更复杂的验证规则
            return System.Text.RegularExpressions.Regex.IsMatch(idCard, "^[1-9]\\d{5}(18|19|20)\\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])\\d{3}[\\dXx]$");
        }
        catch (Exception ex)
        {
            throw new ValidationException("验证身份证号失败", ex);
        }
    }
}

/// <summary>
/// 验证结果
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// 是否验证通过
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// 验证错误信息
    /// </summary>
    public List<string> ErrorMessages { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="isValid">是否验证通过</param>
    /// <param name="validationResults">验证结果</param>
    public ValidationResult(bool isValid, List<System.ComponentModel.DataAnnotations.ValidationResult> validationResults)
    {
        IsValid = isValid;
        ErrorMessages = validationResults.Select(r => r.ErrorMessage).Where(m => m != null).ToList();
    }
}

/// <summary>
/// 验证异常
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public ValidationException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {}
}