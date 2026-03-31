using System.Text;

namespace WellTool.Json.Serialize
{
    public class JSONObjectSerializer : JSONSerializer
    {
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            
            if (obj is JSONObject jsonObject)
            {
                var sb = new StringBuilder();
                sb.Append("{");
                
                var first = true;
                foreach (var key in jsonObject.Keys)
                {
                    if (!first)
                    {
                        sb.Append(",");
                    }
                    sb.Append('"').Append(key).Append('"').Append(":");
                    sb.Append(Serialize(jsonObject[key]));
                    first = false;
                }
                
                sb.Append("}");
                return sb.ToString();
            }
            
            return obj.ToString();
        }
    }
}