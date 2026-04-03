using System;

namespace WellTool.Json
{
    /// <summary>
    /// JSON字符串解析器
    /// </summary>
    public class JSONParser
    {
        private readonly JSONTokener _tokener;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tokener">JSONTokener</param>
        public JSONParser(JSONTokener tokener)
        {
            this._tokener = tokener;
        }

        /// <summary>
        /// 创建JSONParser
        /// </summary>
        /// <param name="tokener">JSONTokener</param>
        /// <returns>JSONParser</returns>
        public static JSONParser Of(JSONTokener tokener)
        {
            return new JSONParser(tokener);
        }

        /// <summary>
        /// 解析JSONTokener中的字符到目标的JSONObject中
        /// </summary>
        /// <param name="jsonObject">JSONObject</param>
        /// <param name="filter">键值对过滤编辑器，可以通过实现此接口，完成解析前对键值对的过滤和修改操作</param>
        public void ParseTo(JSONObject jsonObject, Func<string, object, bool> filter = null)
        {
            var tokener = this._tokener;

            if (tokener.NextClean() != '{')
            {
                throw tokener.SyntaxError("A JSONObject text must begin with '{'");
            }

            char prev = (char)0;
            char c;
            string key;

            while (true)
            {
                prev = tokener.GetPrevious();
                c = tokener.NextClean();
                switch (c)
                {
                    case (char)0:
                        throw tokener.SyntaxError("A JSONObject text must end with '}'");
                    case '}':
                        return;
                    case '{':
                    case '[':
                        if (prev == '{')
                        {
                            throw tokener.SyntaxError("A JSONObject can not directly nest another JSONObject or JSONArray.");
                        }
                        goto default;
                    default:
                        tokener.Back();
                        key = tokener.NextStringValue();
                        break;
                }

                // The key is followed by ':'.
                c = tokener.NextClean();
                if (c != ':')
                {
                    throw tokener.SyntaxError("Expected a ':' after a key");
                }

                var value = tokener.NextValue();
                if (filter != null && !filter(key, value))
                {
                    continue;
                }
                jsonObject.Set(key, value, jsonObject.Config.IsCheckDuplicate());

                // Pairs are separated by ','.
                switch (tokener.NextClean())
                {
                    case ';':
                    case ',':
                        if (tokener.NextClean() == '}')
                        {
                            // 尾后逗号（Trailing Commas），JSON中虽然不支持，但是ECMAScript 2017支持，此处做兼容。
                            return;
                        }
                        tokener.Back();
                        break;
                    case '}':
                        return;
                    default:
                        throw tokener.SyntaxError("Expected a ',' or '}'");
                }
            }
        }

        /// <summary>
        /// 解析JSON字符串到JSONArray中
        /// </summary>
        /// <param name="jsonArray">JSONArray</param>
        /// <param name="filter">值过滤编辑器</param>
        public void ParseTo(JSONArray jsonArray, Func<object, bool> filter = null)
        {
            var x = this._tokener;

            if (x.NextClean() != '[')
            {
                throw x.SyntaxError("A JSONArray text must start with '['");
            }
            if (x.NextClean() != ']')
            {
                x.Back();
                while (true)
                {
                    if (x.NextClean() == ',')
                    {
                        x.Back();
                        jsonArray.AddRaw(JSONNull.NULL, filter);
                    }
                    else
                    {
                        x.Back();
                        jsonArray.AddRaw(x.NextValue(), filter);
                    }
                    switch (x.NextClean())
                    {
                        case ',':
                            if (x.NextClean() == ']')
                            {
                                return;
                            }
                            x.Back();
                            break;
                        case ']':
                            return;
                        default:
                            throw x.SyntaxError("Expected a ',' or ']'");
                    }
                }
            }
        }
    }
}
