using System;
using System.Text;

namespace WellTool.Json
{
    /// <summary>
    /// JSON字符串格式化工具，用于简单格式化JSON字符串
    /// from http://blog.csdn.net/lovelong8808/article/details/54580278
    /// </summary>
    public static class JSONStrFormatter
    {
        /// <summary>
        /// 单位缩进字符串
        /// </summary>
        private const string SPACE = "    ";

        /// <summary>
        /// 换行符
        /// </summary>
        private const char NEW_LINE = '\n';

        /// <summary>
        /// 返回格式化JSON字符串
        /// </summary>
        /// <param name="json">未格式化的JSON字符串</param>
        /// <returns>格式化的JSON字符串</returns>
        public static string Format(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return json;
            }

            var result = new StringBuilder();

            char? wrapChar = null;
            bool isEscapeMode = false;
            int length = json.Length;
            int number = 0;
            char key;
            for (int i = 0; i < length; i++)
            {
                key = json[i];

                if (key == '"' || key == '\'')
                {
                    if (wrapChar == null)
                    {
                        //字符串模式开始
                        wrapChar = key;
                    }
                    else if (wrapChar == key)
                    {
                        if (isEscapeMode)
                        {
                            //字符串模式下，遇到结束符号，也同时结束转义
                            isEscapeMode = false;
                        }

                        //字符串包装结束
                        wrapChar = null;
                    }

                    if (i > 1 && json[i - 1] == ':')
                    {
                        result.Append(' ');
                    }

                    result.Append(key);
                    continue;
                }

                if (key == '\\')
                {
                    if (wrapChar.HasValue)
                    {
                        //字符串模式下转义有效
                        isEscapeMode = !isEscapeMode;
                        result.Append(key);
                        continue;
                    }
                    else
                    {
                        result.Append(key);
                    }
                }

                if (wrapChar.HasValue)
                {
                    //字符串模式
                    result.Append(key);
                    continue;
                }

                //如果当前字符是前方括号、前花括号做如下处理
                if (key == '[' || key == '{')
                {
                    //如果前面还有字符，并且字符为"："，打印：换行和缩进字符字符串
                    if (i > 1 && json[i - 1] == ':')
                    {
                        result.Append(NEW_LINE);
                        result.Append(Indent(number));
                    }
                    result.Append(key);
                    //前方括号、前花括号，的后面必须换行
                    result.Append(NEW_LINE);
                    //每出现一次前方括号、前花括号，缩进次数增加一次
                    number++;
                    result.Append(Indent(number));

                    continue;
                }

                //如果当前字符是后方括号、后花括号做如下处理
                if (key == ']' || key == '}')
                {
                    //后方括号、后花括号的前面必须换行
                    result.Append(NEW_LINE);
                    //每出现一次后方括号、后花括号，缩进次数减少一次
                    number--;
                    result.Append(Indent(number));
                    //打印当前字符
                    result.Append(key);
                    continue;
                }

                //如果当前字符是逗号，逗号后面换行，并缩进，不改变缩进次数
                if (key == ',')
                {
                    result.Append(key);
                    result.Append(NEW_LINE);
                    result.Append(Indent(number));
                    continue;
                }

                if (i > 1 && json[i - 1] == ':')
                {
                    result.Append(' ');
                }

                //打印当前字符
                result.Append(key);
            }

            return result.ToString();
        }

        /// <summary>
        /// 返回指定次数的缩进字符串。每一次缩进4个空格
        /// </summary>
        /// <param name="number">缩进次数</param>
        /// <returns>指定缩进次数的字符串</returns>
        private static string Indent(int number)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < number; i++)
            {
                sb.Append(SPACE);
            }
            return sb.ToString();
        }
    }
}
