using System.Collections.Generic;

namespace WellTool.Extra.Spring;

/// <summary>
/// Spring工具类 - 提供与Spring框架交互的静态方法
/// </summary>
public static class EnableSpringUtil
{
    /// <summary>
    /// 检查是否在Spring环境中
    /// </summary>
    /// <returns>是否在Spring环境中</returns>
    public static bool IsInSpring()
    {
        return SpringUtil.IsInSpring();
    }

    /// <summary>
    /// 获取所有指定类型的Bean
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <returns>Bean数组</returns>
    public static T[] GetBeans<T>() where T : class
    {
        return SpringUtil.GetBeans<T>();
    }

    /// <summary>
    /// 根据名称获取Bean
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    /// <param name="name">Bean名称</param>
    /// <returns>Bean实例</returns>
    public static T GetBean<T>(string name)
    {
        try
        {
            return SpringUtil.GetBean<T>(name);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 根据类型获取Bean
    /// </summary>
    /// <param name="type">Bean类型</param>
    /// <returns>Bean实例</returns>
    public static object GetBean(Type type)
    {
        try
        {
            return SpringUtil.GetBean(type);
        }
        catch
        {
            return null;
        }
    }
}