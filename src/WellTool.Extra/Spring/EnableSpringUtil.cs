using System;

namespace WellTool.Extra.Spring
{
    /// <summary>
    /// 启用SpringUtil，即注入SpringUtil到容器中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableSpringUtilAttribute : Attribute
    {
    }
}
