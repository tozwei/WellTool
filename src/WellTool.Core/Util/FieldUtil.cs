using System;
using System.Reflection;

namespace WellTool.Core.Util;

/// <summary>
/// 字段工具类
/// </summary>
public static class FieldUtil
{
    /// <summary>
    /// 获取对象的字段值
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段值</returns>
    public static object GetFieldValue(object obj, string fieldName)
    {
        if (obj == null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var type = obj.GetType();
        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (field != null)
        {
            return field.GetValue(obj);
        }

        // try property
        var prop = type.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (prop != null)
        {
            return prop.GetValue(obj);
        }

        // try auto-property backing field: <Name>k__BackingField
        var backing = type.GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        return backing?.GetValue(obj);
    }

    /// <summary>
    /// 设置对象的字段值
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="value">字段值</param>
    public static void SetFieldValue(object obj, string fieldName, object value)
    {
        if (obj == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var type = obj.GetType();
        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (field != null)
        {
            field.SetValue(obj, value);
            return;
        }

        // try property
        var prop = type.GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(obj, value);
            return;
        }

        // try auto-property backing field
        var backing = type.GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
        if (backing != null)
        {
            backing.SetValue(obj, value);
        }
    }

    /// <summary>
    /// 获取类型的所有字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>字段数组</returns>
    public static FieldInfo[] GetFields(Type type)
    {
        return type?.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) ?? Array.Empty<FieldInfo>();
    }

    /// <summary>
    /// 获取类型的公共字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>字段数组</returns>
    public static FieldInfo[] GetPublicFields(Type type)
    {
        return type?.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public) ?? Array.Empty<FieldInfo>();
    }

    /// <summary>
    /// 获取类型的私有字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>字段数组</returns>
    public static FieldInfo[] GetPrivateFields(Type type)
    {
        return type?.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic) ?? Array.Empty<FieldInfo>();
    }

    /// <summary>
    /// 检查类型是否包含指定的字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="fieldName">字段名</param>
    /// <returns>是否包含</returns>
    public static bool HasField(Type type, string fieldName)
    {
        if (type == null || string.IsNullOrEmpty(fieldName))
        {
            return false;
        }

        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        return field != null;
    }

    /// <summary>
    /// 获取字段的类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段类型</returns>
    public static Type GetFieldType(Type type, string fieldName)
    {
        if (type == null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        return field?.FieldType;
    }

    /// <summary>
    /// 获取类型的字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段信息</returns>
    public static FieldInfo GetField(Type type, string fieldName)
    {
        if (type == null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (field != null)
        {
            return field;
        }

        // try to find property backing field for auto-properties
        var backing = type.GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
        if (backing != null)
        {
            return backing;
        }

        return null;
    }

    /// <summary>
    /// 获取类型的声明字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="fieldName">字段名</param>
    /// <returns>字段信息</returns>
    public static FieldInfo GetDeclaredField(Type type, string fieldName)
    {
        if (type == null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        if (field != null)
        {
            return field;
        }

        var backing = type.GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        if (backing != null)
        {
            return backing;
        }

        return null;
    }

    /// <summary>
    /// 获取类型的所有声明字段
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>字段数组</returns>
    public static FieldInfo[] GetDeclaredFields(Type type)
    {
        return type?.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly) ?? Array.Empty<FieldInfo>();
    }

    /// <summary>
    /// 设置对象的字段值
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="field">字段信息</param>
    /// <param name="value">字段值</param>
    public static void SetField(object obj, FieldInfo field, object value)
    {
        field?.SetValue(obj, value);
    }
}
