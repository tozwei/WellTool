using System.Text;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// 多条件构建封装
    /// 可以将多个条件构建为SQL语句的一部分，并将参数值转换为占位符
    /// 例如：name = ? AND type IN (?, ?) AND other LIKE ?
    /// </summary>
    public class ConditionBuilder
    {
        /// <summary>
        /// 条件数组
        /// </summary>
        private readonly Condition[] _conditions;

        /// <summary>
        /// 占位符对应的值列表
        /// </summary>
        private List<object> _paramValues;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="conditions">条件列表</param>
        public ConditionBuilder(params Condition[] conditions)
        {
            _conditions = conditions;
        }

        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <param name="conditions">条件列表</param>
        /// <returns>ConditionBuilder</returns>
        public static ConditionBuilder Of(params Condition[] conditions)
        {
            return new ConditionBuilder(conditions);
        }

        /// <summary>
        /// 返回构建后的参数列表
        /// </summary>
        /// <returns>参数列表</returns>
        public List<object> GetParamValues()
        {
            if (_paramValues == null)
            {
                return new List<object>();
            }
            return new List<object>(_paramValues);
        }

        /// <summary>
        /// 构建组合条件
        /// 例如：name = ? AND type IN (?, ?) AND other LIKE ?
        /// </summary>
        /// <returns>构建后的SQL语句条件部分</returns>
        public string Build()
        {
            if (_paramValues == null)
            {
                _paramValues = new List<object>();
            }
            else
            {
                _paramValues.Clear();
            }
            return Build(_paramValues);
        }

        /// <summary>
        /// 构建组合条件
        /// 例如：name = ? AND type IN (?, ?) AND other LIKE ?
        /// </summary>
        /// <param name="paramValues">用于写出参数的List</param>
        /// <returns>构建后的SQL语句条件部分</returns>
        public string Build(List<object> paramValues)
        {
            if (_conditions == null || _conditions.Length == 0)
            {
                return string.Empty;
            }

            var conditionStrBuilder = new StringBuilder();
            bool isFirst = true;

            foreach (var condition in _conditions)
            {
                // 添加逻辑运算符
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    conditionStrBuilder.Append(' ');
                    conditionStrBuilder.Append(condition.LinkOperator);
                    conditionStrBuilder.Append(' ');
                }

                // 构建条件部分
                conditionStrBuilder.Append(condition.ToString(paramValues));
            }

            return conditionStrBuilder.ToString();
        }

        /// <summary>
        /// 构建组合条件
        /// </summary>
        /// <returns>构建后的SQL语句条件部分</returns>
        public override string ToString()
        {
            return Build();
        }
    }
}
