using System.Collections.Generic;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// 查询条件组
    /// </summary>
    public class ConditionGroup
    {
        /// <summary>
        /// 逻辑运算符
        /// </summary>
        public LogicalOperator LogicalOperator { get; set; }

        /// <summary>
        /// 条件列表
        /// </summary>
        public List<object> Conditions { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public ConditionGroup() : this(LogicalOperator.AND)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="logicalOperator">逻辑运算符</param>
        public ConditionGroup(LogicalOperator logicalOperator)
        {
            LogicalOperator = logicalOperator;
            Conditions = new List<object>();
        }

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>this</returns>
        public ConditionGroup Add(Condition condition)
        {
            Conditions.Add(condition);
            return this;
        }

        /// <summary>
        /// 添加条件组
        /// </summary>
        /// <param name="conditionGroup">条件组</param>
        /// <returns>this</returns>
        public ConditionGroup Add(ConditionGroup conditionGroup)
        {
            Conditions.Add(conditionGroup);
            return this;
        }

        /// <summary>
        /// 添加等于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Eq(string field, object value)
        {
            return Add(Condition.Eq(field, value));
        }

        /// <summary>
        /// 添加不等于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Ne(string field, object value)
        {
            return Add(Condition.Ne(field, value));
        }

        /// <summary>
        /// 添加大于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Gt(string field, object value)
        {
            return Add(Condition.Gt(field, value));
        }

        /// <summary>
        /// 添加大于等于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Ge(string field, object value)
        {
            return Add(Condition.Ge(field, value));
        }

        /// <summary>
        /// 添加小于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Lt(string field, object value)
        {
            return Add(Condition.Lt(field, value));
        }

        /// <summary>
        /// 添加小于等于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Le(string field, object value)
        {
            return Add(Condition.Le(field, value));
        }

        /// <summary>
        /// 添加包含条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup Like(string field, object value)
        {
            return Add(Condition.Like(field, value));
        }

        /// <summary>
        /// 添加不包含条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup NotLike(string field, object value)
        {
            return Add(Condition.NotLike(field, value));
        }

        /// <summary>
        /// 添加属于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup In(string field, object value)
        {
            return Add(Condition.In(field, value));
        }

        /// <summary>
        /// 添加不属于条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public ConditionGroup NotIn(string field, object value)
        {
            return Add(Condition.NotIn(field, value));
        }

        /// <summary>
        /// 添加为空条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>this</returns>
        public ConditionGroup IsNull(string field)
        {
            return Add(Condition.IsNull(field));
        }

        /// <summary>
        /// 添加不为空条件
        /// </summary>
        /// <param name="field">字段名</param>
        /// <returns>this</returns>
        public ConditionGroup IsNotNull(string field)
        {
            return Add(Condition.IsNotNull(field));
        }

        /// <summary>
        /// 创建并添加条件组
        /// </summary>
        /// <param name="logicalOperator">逻辑运算符</param>
        /// <returns>条件组</returns>
        public ConditionGroup Group(LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            ConditionGroup group = new ConditionGroup(logicalOperator);
            Add(group);
            return group;
        }
    }
}