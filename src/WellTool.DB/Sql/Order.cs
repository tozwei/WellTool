namespace WellTool.DB.Sql
{
    public class Order
    {
        public string Field { get; set; }
        public Direction Direction { get; set; }
        
        public Order(string field, Direction direction)
        {
            Field = field;
            Direction = direction;
        }
    }
    
    public enum Direction
    {
        ASC,
        DESC
    }
}