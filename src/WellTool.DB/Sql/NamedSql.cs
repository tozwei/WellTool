using System.Collections.Generic;
using System.Text;
using WellTool.Core.Util;

namespace WellTool.DB.Sql;

/// <summary>
/// 使用命名占位符的SQL，例如：select * from table where field1=:name1<br>
/// 支持的占位符格式为：
/// <pre>
/// 1、:name
/// 2、@name
/// 3、?name
/// </pre>
/// </summary>
public class NamedSql
{
    private static readonly char[] NameStartChars = { ':', '@', '?' };

    private string _sql;
    private readonly List<object> _params;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="namedSql">命名占位符的SQL</param>
    /// <param name="paramMap">名和参数的对应Map</param>
    public NamedSql(string namedSql, Dictionary<string, object> paramMap)
    {
        _params = new List<object>();
        Parse(namedSql, paramMap);
    }

    /// <summary>
    /// 获取SQL
    /// </summary>
    /// <returns>SQL</returns>
    public string GetSql()
    {
        return _sql;
    }

    /// <summary>
    /// 获取参数列表，按照占位符顺序
    /// </summary>
    /// <returns>参数数组</returns>
    public object[] GetParams()
    {
        return _params.ToArray();
    }

    /// <summary>
    /// 获取参数列表，按照占位符顺序
    /// </summary>
    /// <returns>参数列表</returns>
    public List<object> GetParamList()
    {
        return _params;
    }

    /// <summary>
    /// 解析命名占位符的SQL
    /// </summary>
    /// <param name="namedSql">命名占位符的SQL</param>
    /// <param name="paramMap">名和参数的对应Map</param>
    private void Parse(string namedSql, Dictionary<string, object> paramMap)
    {
        if (paramMap == null || paramMap.Count == 0)
        {
            _sql = namedSql;
            return;
        }

        var len = namedSql.Length;
        var name = new StringBuilder();
        var sqlBuilder = new StringBuilder();
        char? nameStartChar = null;

        for (int i = 0; i < len; i++)
        {
            var c = namedSql[i];
            if (ArrayUtil.Contains(NameStartChars, c))
            {
                // 新的变量开始符出现，要处理之前的变量
                ReplaceVar(nameStartChar, name, sqlBuilder, paramMap);
                nameStartChar = c;
            }
            else if (nameStartChar.HasValue)
            {
                // 变量状态
                if (IsGenerateChar(c))
                {
                    // 变量名
                    name.Append(c);
                }
                else
                {
                    // 非标准字符也非变量开始的字符出现表示变量名结束，开始替换
                    ReplaceVar(nameStartChar, name, sqlBuilder, paramMap);
                    nameStartChar = null;
                    sqlBuilder.Append(c);
                }
            }
            else
            {
                // 变量以外的字符原样输出
                sqlBuilder.Append(c);
            }
        }

        // 收尾，如果SQL末尾存在变量，处理之
        if (name.Length > 0)
        {
            ReplaceVar(nameStartChar, name, sqlBuilder, paramMap);
        }

        _sql = sqlBuilder.ToString();
    }

    /// <summary>
    /// 替换变量，如果无变量，原样输出到SQL中去
    /// </summary>
    /// <param name="nameStartChar">变量开始字符</param>
    /// <param name="name">变量名</param>
    /// <param name="sqlBuilder">结果SQL缓存</param>
    /// <param name="paramMap">变量map（非空）</param>
    private void ReplaceVar(char? nameStartChar, StringBuilder name, StringBuilder sqlBuilder, Dictionary<string, object> paramMap)
    {
        if (name.Length == 0)
        {
            if (nameStartChar.HasValue)
            {
                // 类似于:的情况，需要补上:
                sqlBuilder.Append(nameStartChar.Value);
            }
            // 无变量，按照普通字符处理
            return;
        }

        // 变量结束
        var nameStr = name.ToString();
        if (paramMap.ContainsKey(nameStr))
        {
            // 有变量对应值（值可以为null），替换占位符为?，变量值放入相应index位置
            var paramValue = paramMap[nameStr];
            if (paramValue != null && paramValue.GetType().IsArray && SqlUtil.IsInClause(sqlBuilder.ToString()))
            {
                // 可能为select in (xxx)语句，则拆分参数为多个参数，变成in (?,?,?)
                var array = (Array)paramValue;
                var length = array.Length;
                for (int i = 0; i < length; i++)
                {
                    if (i > 0)
                    {
                        sqlBuilder.Append(',');
                    }
                    sqlBuilder.Append('?');
                    _params.Add(array.GetValue(i));
                }
            }
            else
            {
                sqlBuilder.Append('?');
                _params.Add(paramValue);
            }
        }
        else
        {
            // 无变量对应值，原样输出
            sqlBuilder.Append(nameStartChar.Value).Append(name);
        }

        //清空变量，表示此变量处理结束
        name.Clear();
    }

    /// <summary>
    /// 是否为标准的字符，包括大小写字母、下划线和数字
    /// </summary>
    /// <param name="c">字符</param>
    /// <returns>是否标准字符</returns>
    private static bool IsGenerateChar(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || (c >= '0' && c <= '9');
    }
}
