using System.Text;

namespace WellTool.Json.Serialize
{
    public class DefaultJSONArraySerializer : JSONSerializer
    {
        public void Serialize(object obj, object writer)
        {
            if (writer is StringBuilder sb)
            {
                if (obj == null)
                {
                    sb.Append("null");
                    return;
                }
                
                if (obj is JSONArray jsonArray)
                {
                    sb.Append("[");
                    
                    var first = true;
                    foreach (var item in jsonArray)
                    {
                        if (!first)
                        {
                            sb.Append(",");
                        }
                        // 递归序列化每个元素
                        var itemSerializer = new DefaultJSONSerializer();
                        itemSerializer.Serialize(item, sb);
                        first = false;
                    }
                    
                    sb.Append("]");
                }
                else
                {
                    sb.Append(obj.ToString());
                }
            }
        }
    }
}