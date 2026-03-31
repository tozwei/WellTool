using System.Text;

namespace WellTool.Json.Serialize
{
    public class JSONArraySerializer : JSONSerializer
    {
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            
            if (obj is JSONArray jsonArray)
            {
                var sb = new StringBuilder();
                sb.Append("[");
                
                var first = true;
                foreach (var item in jsonArray)
                {
                    if (!first)
                    {
                        sb.Append(",");
                    }
                    sb.Append(Serialize(item));
                    first = false;
                }
                
                sb.Append("]");
                return sb.ToString();
            }
            
            return obj.ToString();
        }
    }
}