using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util;

/// <summary>
/// 统一社会信用代码工具类
/// </summary>
public static class CreditCodeUtil
{
	/// <summary>
	/// 统一社会信用代码权重因子
	/// </summary>
	private static readonly int[] WEIGHTS = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };

	/// <summary>
	/// 统一社会信用代码校验字符集
	/// </summary>
	private const string CHARS = "0123456789ABCDEFGHJKLMOPQWRUWXY";

	/// <summary>
	/// 验证统一社会信用代码
	/// </summary>
	/// <param name="code">统一社会信用代码</param>
	/// <returns>是否有效</returns>
	public static bool IsValid(string code)
	{
		if (string.IsNullOrEmpty(code) || code.Length != 18)
		{
			return false;
		}

		code = code.ToUpperInvariant();

		// 检查格式
		if (!Regex.IsMatch(code, "^[0-9A-HJ-NPQRTUWXY]{2}\\d{6}[0-9A-HJ-NPQRTUWXY]{10}$"))
		{
			return false;
		}

		// 校验校验位
		char checkCode = code[17];
		return GetCheckCode(code) == checkCode;
	}

	/// <summary>
	/// 获取校验位
	/// </summary>
	/// <param name="code">统一社会信用代码（前17位）</param>
	/// <returns>校验位</returns>
	private static char GetCheckCode(string code)
	{
		int sum = 0;
		for (int i = 0; i < 17; i++)
		{
			char c = code[i];
			int value;
			if (char.IsDigit(c))
			{
				value = c - '0';
			}
			else
			{
				value = CHARS.IndexOf(c);
			}
			sum += value * WEIGHTS[i];
		}
		int index = sum % 31;
		return CHARS[31 - index];
	}

	/// <summary>
	/// 生成统一社会信用代码
	/// </summary>
	/// <param name="orgCode">组织机构代码（前8位）</param>
	/// <returns>统一社会信用代码</returns>
	public static string Generate(string orgCode)
	{
		if (string.IsNullOrEmpty(orgCode) || orgCode.Length != 8)
		{
			throw new ArgumentException("组织机构代码必须为8位");
		}

		string codeWithoutCheck = "91" + "000000" + orgCode.ToUpperInvariant();
		char checkCode = GetCheckCode(codeWithoutCheck + "00000000000".Substring(0, 17 - codeWithoutCheck.Length));
		return codeWithoutCheck + checkCode;
	}
}
