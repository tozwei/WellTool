using System;
using System.Text;
using WellTool.Core.Lang;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 命名规则封装，主要是针对驼峰式命名的字符串转换为下划线方式、连接符方式等的封装
    /// </summary>
    public static class NamingCase
    {
        /// <summary>
        /// 将驼峰式命名的字符串转换为下划线方式，又称 SnakeCase、underScoreCase。<br/>
        /// 如果转换前的驼峰式命名的字符串为空，则返回空字符串。<br/>
        /// 规则为：
        /// <ul>
        ///     <li>单字之间以下划线隔开</li>
        ///     <li>每个单字的首字母亦用小写字母</li>
        /// </ul>
        /// 例如：
        /// <pre>
        /// HelloWorld => hello_world
        /// Hello_World => hello_world
        /// HelloWorld_test => hello_world_test
        /// </pre>
        /// </summary>
        /// <param name="str">转换前的驼峰式命名的字符串，也可以为下划线形式</param>
        /// <returns>转换后下划线方式命名的字符串</returns>
        public static string ToUnderlineCase(string str)
        {
            return ToSymbolCase(str, '_');
        }

        /// <summary>
        /// 将驼峰式命名的字符串转换为短横连接方式。<br/>
        /// 如果转换前的驼峰式命名的字符串为空，则返回空字符串。<br/>
        /// 规则为：
        /// <ul>
        ///     <li>单字之间横线线隔开</li>
        ///     <li>每个单字的首字母亦用小写字母</li>
        /// </ul>
        /// 例如：
        /// <pre>
        /// HelloWorld => hello-world
        /// Hello_World => hello-world
        /// HelloWorld_test => hello-world-test
        /// </pre>
        /// </summary>
        /// <param name="str">转换前的驼峰式命名的字符串，也可以为下划线形式</param>
        /// <returns>转换后下划线方式命名的字符串</returns>
        public static string ToKebabCase(string str)
        {
            return ToSymbolCase(str, '-');
        }

        /// <summary>
        /// 将驼峰式命名的字符串转换为使用符号连接方式。如果转换前的驼峰式命名的字符串为空，则返回空字符串。
        /// </summary>
        /// <param name="str">转换前的驼峰式命名的字符串，也可以为符号连接形式</param>
        /// <param name="symbol">连接符</param>
        /// <returns>转换后符号连接方式命名的字符串</returns>
        public static string ToSymbolCase(string str, char symbol)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsUpper(c))
                {
                    char? preChar = i > 0 ? str[i - 1] : null as char?;
                    char? nextChar = i < str.Length - 1 ? str[i + 1] : null as char?;

                    if (preChar.HasValue)
                    {
                        if (symbol == preChar)
                        {
                            // 前一个为分隔符
                            if (!nextChar.HasValue || char.IsLower(nextChar.Value))
                            {
                                // 普通首字母大写，如_Abb -> _abb
                                c = char.ToLower(c);
                            }
                            // 后一个为大写，按照专有名词对待，如_AB -> _AB
                        }
                        else if (char.IsLower(preChar.Value))
                        {
                            // 前一个为小写
                            sb.Append(symbol);
                            if (!nextChar.HasValue || char.IsLower(nextChar.Value) || char.IsDigit(nextChar.Value))
                            {
                                // 普通首字母大写，如 aBcc -> a_bcc
                                c = char.ToLower(c);
                            }
                            // 后一个为大写，按照专有名词对待，如 aBC -> a_BC
                        }
                        else
                        {
                            // 前一个为大写
                            if (nextChar.HasValue && char.IsLower(nextChar.Value))
                            {
                                // 普通首字母大写，如 ABcc -> A_bcc
                                sb.Append(symbol);
                                c = char.ToLower(c);
                            }
                            // 后一个为大写，按照专有名词对待，如 ABC -> ABC
                        }
                    }
                    else
                    {
                        // 首字母，需要根据后一个判断是否转为小写
                        if (!nextChar.HasValue || char.IsLower(nextChar.Value))
                        {
                            // 普通首字母大写，如 Abc -> abc
                            c = char.ToLower(c);
                        }
                        // 后一个为大写，按照专有名词对待，如 ABC -> ABC
                    }
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将下划线方式命名的字符串转换为帕斯卡式（大驼峰）。<br/>
        /// 规则为：
        /// <ul>
        ///     <li>单字之间不以空格或任何连接符断开</li>
        ///     <li>第一个单字首字母采用大写字母</li>
        ///     <li>后续单字的首字母亦用大写字母</li>
        /// </ul>
        /// 如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。<br/>
        /// 例如：hello_world => HelloWorld
        /// </summary>
        /// <param name="name">转换前的下划线大写方式命名的字符串</param>
        /// <returns>转换后的驼峰式命名的字符串（大驼峰）</returns>
        public static string ToPascalCase(string name)
        {
            return StringUtil.UpperFirst(ToCamelCase(name));
        }

        /// <summary>
        /// 将下划线方式命名的字符串转换为驼峰式（小驼峰）。如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。<br/>
        /// 规则为：
        /// <ul>
        ///     <li>单字之间不以空格或任何连接符断开</li>
        ///     <li>第一个单字首字母采用小写字母</li>
        ///     <li>后续单字的首字母亦用大写字母</li>
        /// </ul>
        /// 例如：hello_world => helloWorld
        /// </summary>
        /// <param name="name">转换前的下划线大写方式命名的字符串</param>
        /// <returns>转换后的驼峰式命名的字符串（小驼峰）</returns>
        public static string ToCamelCase(string name)
        {
            return ToCamelCase(name, '_', true);
        }

        /// <summary>
        /// 将连接符方式命名的字符串转换为驼峰式。如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。
        /// </summary>
        /// <param name="name">转换前的自定义方式命名的字符串</param>
        /// <param name="symbol">原字符串中的连接符</param>
        /// <returns>转换后的驼峰式命名的字符串</returns>
        public static string ToCamelCase(string name, char symbol)
        {
            return ToCamelCase(name, symbol, true);
        }

        /// <summary>
        /// 将连接符方式命名的字符串转换为驼峰式。如果转换前的下划线大写方式命名的字符串为空，则返回空字符串。
        /// </summary>
        /// <param name="name">转换前的自定义方式命名的字符串</param>
        /// <param name="symbol">原字符串中的连接符</param>
        /// <param name="otherCharToLower">其他非连接符后的字符是否需要转为小写</param>
        /// <returns>转换后的驼峰式命名的字符串</returns>
        public static string ToCamelCase(string name, char symbol, bool otherCharToLower)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            if (name.IndexOf(symbol) >= 0)
            {
                var sb = new StringBuilder(name.Length);
                var upperCase = false;
                for (var i = 0; i < name.Length; i++)
                {
                    var c = name[i];

                    if (c == symbol)
                    {
                        upperCase = true;
                    }
                    else if (upperCase)
                    {
                        sb.Append(char.ToUpper(c));
                        upperCase = false;
                    }
                    else
                    {
                        sb.Append(otherCharToLower ? char.ToLower(c) : c);
                    }
                }
                return sb.ToString();
            }
            else
            {
                // 如果字符串不包含连接符，检查它是否已经是驼峰式命名
                // 如果首字母小写，保持不变
                // 如果首字母大写，将其转换为小写
                if (name.Length > 0 && char.IsUpper(name[0]))
                {
                    return char.ToLower(name[0]) + name.Substring(1);
                }
                return name;
            }
        }
    }
}
