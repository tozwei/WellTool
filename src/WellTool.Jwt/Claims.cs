using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WellTool.Jwt
{
    /// <summary>
    /// Claims 认证，简单的JsonDocument包装
    /// </summary>
    public class Claims
    {
        private Dictionary<string, object> _claims;

        /// <summary>
        /// 增加Claims属性，如果属性值为{@code null}，则移除这个属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        protected void SetClaim(string name, object value)
        {
            Init();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name must be not null!");
            }
            if (value == null)
            {
                _claims.Remove(name);
                return;
            }
            _claims[name] = value;
        }

        /// <summary>
        /// 加入多个Claims属性
        /// </summary>
        /// <param name="headerClaims">多个Claims属性</param>
        protected void PutAll(Dictionary<string, object> headerClaims)
        {
            if (headerClaims != null && headerClaims.Count > 0)
            {
                foreach (var entry in headerClaims)
                {
                    SetClaim(entry.Key, entry.Value);
                }
            }
        }

        /// <summary>
        /// 获取指定名称属性
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>属性</returns>
        public object? GetClaim(string name)
        {
            Init();
            _claims.TryGetValue(name, out var value);
            
            // 处理 JsonElement 类型，转换为相应的基本类型
            if (value is System.Text.Json.JsonElement jsonElement)
            {
                switch (jsonElement.ValueKind)
                {
                    case System.Text.Json.JsonValueKind.String:
                        return jsonElement.GetString();
                    case System.Text.Json.JsonValueKind.Number:
                        if (jsonElement.TryGetInt32(out int intValue))
                        {
                            return intValue;
                        }
                        else if (jsonElement.TryGetInt64(out long longValue))
                        {
                            return longValue;
                        }
                        else if (jsonElement.TryGetDouble(out double doubleValue))
                        {
                            return doubleValue;
                        }
                        else if (jsonElement.TryGetDecimal(out decimal decimalValue))
                        {
                            return decimalValue;
                        }
                        break;
                    case System.Text.Json.JsonValueKind.True:
                        return true;
                    case System.Text.Json.JsonValueKind.False:
                        return false;
                    case System.Text.Json.JsonValueKind.Array:
                        var array = new List<object>();
                        foreach (var item in jsonElement.EnumerateArray())
                        {
                            if (item.ValueKind == System.Text.Json.JsonValueKind.String)
                            {
                                array.Add(item.GetString());
                            }
                            else if (item.ValueKind == System.Text.Json.JsonValueKind.Number)
                            {
                                if (item.TryGetInt64(out long itemLongValue))
                                {
                                    array.Add(itemLongValue);
                                }
                                else if (item.TryGetDouble(out double itemDoubleValue))
                                {
                                    array.Add(itemDoubleValue);
                                }
                            }
                            else if (item.ValueKind == System.Text.Json.JsonValueKind.True || item.ValueKind == System.Text.Json.JsonValueKind.False)
                            {
                                array.Add(item.GetBoolean());
                            }
                        }
                        return array;
                }
            }
            
            return value;
        }

        /// <summary>
        /// 获取指定名称属性（字符串类型）
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>属性</returns>
        public string? GetClaimAsString(string name)
        {
            var value = GetClaim(name);
            if (value is string strValue)
            {
                return strValue;
            }
            else if (value is System.Text.Json.JsonElement jsonElement && jsonElement.ValueKind == System.Text.Json.JsonValueKind.String)
            {
                return jsonElement.GetString();
            }
            return null;
        }

        /// <summary>
        /// 获取Claims的JSON字符串形式
        /// </summary>
        /// <returns>JSON字符串</returns>
        public Dictionary<string, object> GetClaimsJson()
        {
            Init();
            return _claims;
        }

        /// <summary>
        /// 解析JWT JSON
        /// </summary>
        /// <param name="tokenPart">JWT JSON</param>
        public void Parse(string tokenPart)
        {
            var jsonStr = JWT.Base64UrlDecode(tokenPart, Encoding.UTF8);
            _claims = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonStr) ?? new Dictionary<string, object>();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            Init();
            // 处理 JsonElement 类型的值，确保它们被正确序列化为字符串
            var claims = new Dictionary<string, object>();
            foreach (var kvp in _claims)
            {
                if (kvp.Value is System.Text.Json.JsonElement jsonElement)
                {
                    if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.String)
                    {
                        claims[kvp.Key] = jsonElement.GetString();
                    }
                    else if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.Number)
                    {
                        claims[kvp.Key] = jsonElement.GetInt64();
                    }
                    else if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.True || jsonElement.ValueKind == System.Text.Json.JsonValueKind.False)
                    {
                        claims[kvp.Key] = jsonElement.GetBoolean();
                    }
                    else if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.Array)
                    {
                        var array = new List<object>();
                        foreach (var item in jsonElement.EnumerateArray())
                        {
                            if (item.ValueKind == System.Text.Json.JsonValueKind.String)
                            {
                                array.Add(item.GetString());
                            }
                            else if (item.ValueKind == System.Text.Json.JsonValueKind.Number)
                            {
                                array.Add(item.GetInt64());
                            }
                            else if (item.ValueKind == System.Text.Json.JsonValueKind.True || item.ValueKind == System.Text.Json.JsonValueKind.False)
                            {
                                array.Add(item.GetBoolean());
                            }
                        }
                        claims[kvp.Key] = array;
                    }
                    else
                    {
                        claims[kvp.Key] = kvp.Value;
                    }
                }
                else
                {
                    claims[kvp.Key] = kvp.Value;
                }
            }
            return JsonSerializer.Serialize(claims);
        }

        private void Init()
        {
            if (_claims == null)
            {
                _claims = new Dictionary<string, object>();
            }
        }
    }
}