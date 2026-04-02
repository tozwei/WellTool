namespace WellTool.DB.Sql
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// LIKE 类型枚举
        /// </summary>
        public enum LikeType
        {
            /// <summary>以给定值开头</summary>
            StartWith,
            /// <summary>以给定值结尾</summary>
            EndWith,
            /// <summary>包含给定值</summary>
            Contains
        }

        private const string OperatorLike = "LIKE";
        private const string OperatorIn = "IN";
        private const string OperatorIs = "IS";
        private const string OperatorIsNot = "IS NOT";
        private const string OperatorBetween = "BETWEEN";
        private const string ValueNull = "NULL";

        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 运算符
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 是否使用占位符
        /// </summary>
        public bool IsPlaceHolder { get; set; } = true;

        /// <summary>
        /// BETWEEN 类型中第二个值
        /// </summary>
        public object SecondValue { get; set; }

        /// <summary>
        /// 与前一个Condition连接的逻辑运算符
        /// </summary>
        public LogicalOperator LinkOperator { get; set; } = LogicalOperator.AND;

        /// <summary>
        /// 构造
        /// </summary>
        public Condition()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="@operator">运算符</param>
        /// <param name="value">值</param>
        public Condition(string field, string @operator, object value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }

        /// <summary>
        /// 构造，使用等于表达式
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        public Condition(string field, object value)
        {
            Field = field;
            Operator = "=";
            Value = value;
            ParseValue();
        }

        /// <summary>
        /// 等于
        /// </summary>
        public static Condition Eq(string field, object value) => new Condition(field, "=", value);

        /// <summary>
        /// 不等于
        /// </summary>
        public static Condition Ne(string field, object value) => new Condition(field, "!=", value);

        /// <summary>
        /// 大于
        /// </summary>
        public static Condition Gt(string field, object value) => new Condition(field, ">", value);

        /// <summary>
        /// 大于等于
        /// </summary>
        public static Condition Ge(string field, object value) => new Condition(field, ">=", value);

        /// <summary>
        /// 小于
        /// </summary>
        public static Condition Lt(string field, object value) => new Condition(field, "<", value);

        /// <summary>
        /// 小于等于
        /// </summary>
        public static Condition Le(string field, object value) => new Condition(field, "<=", value);

        /// <summary>
        /// 包含
        /// </summary>
        public static Condition Like(string field, object value) => new Condition(field, "LIKE", value);

        /// <summary>
        /// 不包含
        /// </summary>
        public static Condition NotLike(string field, object value) => new Condition(field, "NOT LIKE", value);

        /// <summary>
        /// 属于
        /// </summary>
        public static Condition In(string field, object value) => new Condition(field, "IN", value);

        /// <summary>
        /// 不属于
        /// </summary>
        public static Condition NotIn(string field, object value) => new Condition(field, "NOT IN", value);

        /// <summary>
        /// 为空
        /// </summary>
        public static Condition IsNull(string field) => new Condition(field, "IS", null) { Value = ValueNull, IsPlaceHolder = false };

        /// <summary>
        /// 不为空
        /// </summary>
        public static Condition IsNotNull(string field) => new Condition(field, "IS NOT", null) { Value = ValueNull, IsPlaceHolder = false };

        /// <summary>
        /// 解析为Condition
        /// </summary>
        public static Condition Parse(string field, object expression)
        {
            return new Condition(field, expression);
        }

        /// <summary>
        /// 是否是 BETWEEN 类型
        /// </summary>
        public bool IsOperatorBetween => OperatorBetween.Equals(Operator, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否是 IN 条件
        /// </summary>
        public bool IsOperatorIn => OperatorIn.Equals(Operator, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否是 IS 条件
        /// </summary>
        public bool IsOperatorIs => OperatorIs.Equals(Operator, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否是 IS NOT 条件
        /// </summary>
        public bool IsOperatorIsNot => OperatorIsNot.Equals(Operator, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 是否是 LIKE 条件
        /// </summary>
        public bool IsOperatorLike => OperatorLike.Equals(Operator, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// 检查值是否为null，如果是则转换为 IS NULL 或 IS NOT NULL
        /// </summary>
        public void CheckValueNull()
        {
            if (Value == null)
            {
                if ("!=".Equals(Operator, StringComparison.OrdinalIgnoreCase) || "<>".Equals(Operator, StringComparison.OrdinalIgnoreCase))
                {
                    Operator = OperatorIsNot;
                }
                else
                {
                    Operator = OperatorIs;
                }
                Value = ValueNull;
            }
        }

        /// <summary>
        /// 转换为条件字符串，并回填占位符对应的参数值
        /// </summary>
        /// <param name="paramValues">参数列表</param>
        /// <returns>条件字符串</returns>
        public string ToString(List<object> paramValues)
        {
            var sb = new System.Text.StringBuilder();
            CheckValueNull();

            sb.Append(Field).Append(' ').Append(Operator);

            if (IsOperatorBetween)
            {
                BuildValuePartForBetween(sb, paramValues);
            }
            else if (IsOperatorIn)
            {
                BuildValuePartForIn(sb, paramValues);
            }
            else
            {
                if (IsPlaceHolder && !IsOperatorIs && !IsOperatorIsNot)
                {
                    sb.Append(" ?");
                    paramValues?.Add(Value);
                }
                else
                {
                    var valueStr = Value?.ToString() ?? "";
                    sb.Append(' ').Append(IsOperatorLike ? $"'{valueStr}'" : valueStr);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            return ToString(null);
        }

        private void BuildValuePartForBetween(System.Text.StringBuilder sb, List<object> paramValues)
        {
            if (IsPlaceHolder)
            {
                sb.Append(" ?");
                paramValues?.Add(Value);
            }
            else
            {
                sb.Append(' ').Append(Value);
            }

            sb.Append(' ').Append(LogicalOperator.AND);

            if (IsPlaceHolder)
            {
                sb.Append(" ?");
                paramValues?.Add(SecondValue);
            }
            else
            {
                sb.Append(' ').Append(SecondValue);
            }
        }

        private void BuildValuePartForIn(System.Text.StringBuilder sb, List<object> paramValues)
        {
            sb.Append(" (");
            if (IsPlaceHolder)
            {
                if (Value is IEnumerable<object> collection)
                {
                    var list = collection.ToList();
                    sb.Append(string.Join(",", Enumerable.Repeat("?", list.Count)));
                    paramValues?.AddRange(list);
                }
                else if (Value is string str)
                {
                    var parts = str.Split(',');
                    sb.Append(string.Join(",", Enumerable.Repeat("?", parts.Length)));
                    paramValues?.AddRange(parts);
                }
                else
                {
                    sb.Append("?");
                    paramValues?.Add(Value);
                }
            }
            else
            {
                if (Value is IEnumerable<object> collection)
                {
                    sb.Append(string.Join(",", collection));
                }
                else if (Value is string str)
                {
                    sb.Append(str);
                }
                else
                {
                    sb.Append(Value);
                }
            }
            sb.Append(')');
        }

        private void ParseValue()
        {
            if (Value == null)
            {
                Operator = OperatorIs;
                Value = ValueNull;
                return;
            }

            if (Value is IEnumerable<object> || Value is Array)
            {
                Operator = OperatorIn;
                return;
            }

            if (Value is not string valueStr)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(valueStr))
            {
                return;
            }

            valueStr = valueStr.Trim();

            if (valueStr.EndsWith("null", StringComparison.OrdinalIgnoreCase))
            {
                if (valueStr.Equals("= null", StringComparison.OrdinalIgnoreCase) || valueStr.Equals("is null", StringComparison.OrdinalIgnoreCase))
                {
                    Operator = OperatorIs;
                    Value = ValueNull;
                    IsPlaceHolder = false;
                    return;
                }
                if (valueStr.Equals("!= null", StringComparison.OrdinalIgnoreCase) || valueStr.Equals("is not null", StringComparison.OrdinalIgnoreCase))
                {
                    Operator = OperatorIsNot;
                    Value = ValueNull;
                    IsPlaceHolder = false;
                    return;
                }
            }

            var parts = valueStr.Split(' ', 2);
            if (parts.Length < 2)
            {
                return;
            }

            var firstPart = parts[0].Trim().ToUpper();
            var operators = new[] { "<>", "<=", "<", ">=", ">", "=", "!=", "IN", "LIKE", "BETWEEN" };

            if (operators.Contains(firstPart))
            {
                Operator = firstPart;
                Value = IsOperatorIn ? parts[1] : TryParseNumber(parts[1]);
            }

            // Handle BETWEEN x AND y
            if (firstPart == "BETWEEN")
            {
                var valueParts = parts[1].Split(new[] { "AND" }, StringSplitOptions.None);
                if (valueParts.Length >= 2)
                {
                    Value = TryParseNumber(valueParts[0].Trim());
                    SecondValue = TryParseNumber(valueParts[1].Trim());
                }
            }
        }

        private static object TryParseNumber(string value)
        {
            value = value.Trim();
            if (decimal.TryParse(value, out var d))
            {
                return d;
            }
            return value;
        }
    }
}