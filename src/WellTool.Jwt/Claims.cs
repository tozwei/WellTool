using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WellTool.JWT
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
            if (value != null)
            {
                // 尝试使用反射来获取 JsonElement 的 ValueKind 属性
                var valueType = value.GetType();
                var valueKindProperty = valueType.GetProperty("ValueKind");
                if (valueKindProperty != null)
                {
                    var valueKind = valueKindProperty.GetValue(value);
                    var valueKindStr = valueKind?.ToString();
                    
                    // 尝试使用反射来调用 GetString 方法
                    var getStringMethod = valueType.GetMethod("GetString");
                    if (getStringMethod != null && valueKindStr == "String")
                    {
                        return getStringMethod.Invoke(value, null);
                    }
                    
                    // 尝试使用反射来调用 GetInt32 方法
                    var getInt32Method = valueType.GetMethod("GetInt32");
                    if (getInt32Method != null && valueKindStr == "Number")
                    {
                        try
                        {
                            return getInt32Method.Invoke(value, null);
                        }
                        catch
                        {
                            // 尝试使用反射来调用 GetInt64 方法
                            var getInt64Method = valueType.GetMethod("GetInt64");
                            if (getInt64Method != null)
                            {
                                try
                                {
                                    return getInt64Method.Invoke(value, null);
                                }
                                catch
                                {
                                    // 尝试使用反射来调用 GetDouble 方法
                                    var getDoubleMethod = valueType.GetMethod("GetDouble");
                                    if (getDoubleMethod != null)
                                    {
                                        try
                                        {
                                            return getDoubleMethod.Invoke(value, null);
                                        }
                                        catch
                                        {
                                            // 尝试使用反射来调用 GetDecimal 方法
                                            var getDecimalMethod = valueType.GetMethod("GetDecimal");
                                            if (getDecimalMethod != null)
                                            {
                                                return getDecimalMethod.Invoke(value, null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    // 处理布尔值
                    if (valueKindStr == "True")
                    {
                        return true;
                    }
                    if (valueKindStr == "False")
                    {
                        return false;
                    }
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