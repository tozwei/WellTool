namespace WellTool.DB.Sql
{
    /// <summary>
    /// 排序条件
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="direction">排序方向</param>
        public Order(string field, Direction direction)
        {
            Field = field;
            Direction = direction;
        }

        /// <summary>
        /// 构造（默认升序）
        /// </summary>
        /// <param name="field">字段名</param>
        public Order(string field) : this(field, Direction.ASC)
        {
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Field, Direction.ToString());
        }
    }
}