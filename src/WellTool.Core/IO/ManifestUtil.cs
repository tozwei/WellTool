using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WellTool.Core.IO;

/// <summary>
/// Jar包中manifest.mf文件获取和解析工具类
/// </summary>
public static class ManifestUtil
{
	/// <summary>
	/// 根据 class 获取所在程序集文件的 Manifest
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>Manifest信息</returns>
	public static Dictionary<string, string> GetManifest(Type type)
	{
		var assembly = type.Assembly;
		var location = assembly.Location;
		if (string.IsNullOrEmpty(location))
		{
			return null;
		}

		try
		{
			var manifest = new Dictionary<string, string>();
			var assemblyName = assembly.GetName();
			manifest["Implementation-Title"] = assemblyName.Name;
			manifest["Implementation-Version"] = assemblyName.Version?.ToString();
			manifest["Assembly-Version"] = assemblyName.Version?.ToString();
			return manifest;
		}
		catch
		{
			return null;
		}
	}

	/// <summary>
	/// 获取程序集的版本信息
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>版本信息字典</returns>
	public static Dictionary<string, string> GetVersionInfo(Type type)
	{
		var assembly = type.Assembly;
		var attributes = assembly.GetCustomAttributes(false);
		var result = new Dictionary<string, string>();

		foreach (var attr in attributes)
		{
			if (attr is AssemblyTitleAttribute title)
			{
				result["AssemblyTitle"] = title.Title;
			}
			else if (attr is AssemblyVersionAttribute version)
			{
				result["AssemblyVersion"] = version.Version;
			}
			else if (attr is AssemblyFileVersionAttribute fileVersion)
			{
				result["AssemblyFileVersion"] = fileVersion.Version;
			}
			else if (attr is AssemblyDescriptionAttribute desc)
			{
				result["AssemblyDescription"] = desc.Description;
			}
			else if (attr is AssemblyProductAttribute product)
			{
				result["AssemblyProduct"] = product.Product;
			}
			else if (attr is AssemblyCompanyAttribute company)
			{
				result["AssemblyCompany"] = company.Company;
			}
		}

		return result;
	}
}
