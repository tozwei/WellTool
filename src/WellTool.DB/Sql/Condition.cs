namespace WellTool.DB.Sql
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class Condition
    {
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
        /// 等于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Eq(string field, object value)
        {
            return new Condition(field, "=", value);
        }

        /// <summary>
        /// 不等于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Ne(string field, object value)
        {
            return new Condition(field, "!=", value);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Gt(string field, object value)
        {
            return new Condition(field, ">", value);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Ge(string field, object value)
        {
            return new Condition(field, ">=", value);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Lt(string field, object value)
        {
            return new Condition(field, "<", value);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Le(string field, object value)
        {
            return new Condition(field, "<=", value);
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition Like(string field, object value)
        {
            return new Condition(field, "LIKE", value);
        }

        /// <summary>
        /// 不包含
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition NotLike(string field, object value)
        {
            return new Condition(field, "NOT LIKE", value);
        }

        /// <summary>
        /// 属于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition In(string field, object value)
        {
            return new Condition(field, "IN", value);
        }

        /// <summary>
        /// 不属于
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>查询条件</returns>
        public static Condition NotIn(string field, object value)
        {
            return new Condition(field, "NOT IN", value);
        }

        /// <summary>
        /// 为空
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>查询条件</returns>
        public static Condition IsNull(string field)
        {
            return new Condition(field, "IS NULL", null);
        }

        /// <summary>
        /// 不为空
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>查询条件</returns>
        public static Condition IsNotNull(string field)
        {
            return new Condition(field, "IS NOT NULL", null);
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Field, Operator, Value);
        }
    }
}