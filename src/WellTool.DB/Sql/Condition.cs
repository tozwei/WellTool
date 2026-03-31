namespace WellTool.DB.Sql
{
    public class Condition
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public string Operator { get; set; }
        
        public Condition(string field, string oper, object value)
        {
            Field = field;
            Operator = oper;
            Value = value;
        }
    }
}