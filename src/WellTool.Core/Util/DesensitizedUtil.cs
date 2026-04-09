using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util;

/// <summary>
/// 脱敏工具类
/// </summary>
public static class DesensitizedUtil
{
	/// <summary>
	/// 脱敏手机号
	/// </summary>
	/// <param name="phone">手机号</param>
	/// <returns>脱敏后的手机号</returns>
	public static string MobilePhone(string phone)
	{
		if (string.IsNullOrEmpty(phone))
		{
			return phone;
		}
		return Regex.Replace(phone, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
	}

	/// <summary>
	/// 脱敏身份证号
	/// </summary>
	/// <param name="idCard">身份证号</param>
	/// <returns>脱敏后的身份证号</returns>
	public static string IdCard(string idCard)
	{
		if (string.IsNullOrEmpty(idCard))
		{
			return idCard;
		}
		if (idCard.Length == 15)
		{
			return Regex.Replace(idCard, "(\\d{3})\\d{11}(\\d{2})", "$1***********$2");
		}
		return Regex.Replace(idCard, "(\\d{4})\\d{10}(\\d{4})", "$1**********$2");
	}

	/// <summary>
	/// 脱敏银行卡号
	/// </summary>
	/// <param name="bankCard">银行卡号</param>
	/// <returns>脱敏后的银行卡号</returns>
	public static string BankCard(string bankCard)
	{
		if (string.IsNullOrEmpty(bankCard))
		{
			return bankCard;
		}
		return Regex.Replace(bankCard, "(\\\\d{4})\\\\d+(\\\\d{5})", "$1****$2");
	}

	/// <summary>
	/// 脱敏邮箱
	/// </summary>
	/// <param name="email">邮箱</param>
	/// <returns>脱敏后的邮箱</returns>
	public static string Email(string email)
	{
		if (string.IsNullOrEmpty(email))
		{
			return email;
		}
		return Regex.Replace(email, "(\\w+)\\w{1,5}@(\\w+)", "$1***@$2");
	}

	/// <summary>
	/// 脱敏姓名
	/// </summary>
	/// <param name="name">姓名</param>
	/// <returns>脱敏后的姓名</returns>
	public static string ChineseName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}
		if (name.Length == 1)
		{
			return name;
		}
		if (name.Length == 2)
		{
			return Regex.Replace(name, "(.{1})(.{1})", "$1*");
		}
		return name[0] + new string('*', name.Length - 1);
	}

	/// <summary>
	/// 脱敏密码
	/// </summary>
	/// <param name="password">密码</param>
	/// <returns>脱敏后的密码</returns>
	public static string Password(string password)
	{
		if (string.IsNullOrEmpty(password))
		{
			return password;
		}
		return new string('*', password.Length);
	}

	/// <summary>
	/// 脱敏地址
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="keepLength">保留前几位</param>
	/// <returns>脱敏后的地址</returns>
	public static string Address(string address, int keepLength = 4)
	{
		if (string.IsNullOrEmpty(address))
		{
			return address;
		}
		if (address.Length <= keepLength)
		{
			return address;
		}
		return address.Substring(0, keepLength) + "****";
	}

	/// <summary>
	/// 脱敏车牌号
	/// </summary>
	/// <param name="carLicense">车牌号</param>
	/// <returns>脱敏后的车牌号</returns>
	public static string CarLicense(string carLicense)
	{
		if (string.IsNullOrEmpty(carLicense))
		{
			return carLicense;
		}
		return Regex.Replace(carLicense, "(\\w{2})\\w+(\\w{2})", "$1**$2");
	}
}
