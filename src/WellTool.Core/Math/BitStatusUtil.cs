namespace WellTool.Core.Math;

using System;

/// <summary>
/// 通过位运算表示状态的工具类
/// 参数必须是 `偶数` 且 `大于等于0`！
/// 
/// @author huangxingguang，senssic
/// @since 5.6.6
/// </summary>
public class BitStatusUtil
{
    /// <summary>
    /// 增加状态
    /// </summary>
    /// <param name="states">原状态</param>
    /// <param name="stat">要添加的状态</param>
    /// <returns>新的状态值</returns>
    public static int Add(int states, int stat)
    {
        Check(states, stat);
        return states | stat;
    }

    /// <summary>
    /// 判断是否含有状态
    /// </summary>
    /// <param name="states">原状态</param>
    /// <param name="stat">要判断的状态</param>
    /// <returns>true：有</returns>
    public static bool Has(int states, int stat)
    {
        Check(states, stat);
        return (states & stat) == stat;
    }

    /// <summary>
    /// 删除一个状态
    /// </summary>
    /// <param name="states">原状态</param>
    /// <param name="stat">要删除的状态</param>
    /// <returns>新的状态值</returns>
    public static int Remove(int states, int stat)
    {
        Check(states, stat);
        if (Has(states, stat))
        {
            return states ^ stat;
        }
        return states;
    }

    /// <summary>
    /// 清空状态就是0
    /// </summary>
    /// <returns>0</returns>
    public static int Clear()
    {
        return 0;
    }

    /// <summary>
    /// 检查
    /// - 必须大于0
    /// - 必须为偶数
    /// </summary>
    /// <param name="args">被检查的状态</param>
    private static void Check(params int[] args)
    {
        foreach (int arg in args)
        {
            if (arg < 0)
            {
                throw new ArgumentException($"{arg} 必须大于等于0");
            }
            if ((arg & 1) == 1)
            {
                throw new ArgumentException($"{arg} 不是偶数");
            }
        }
    }
}
