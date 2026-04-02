using System.Text;

namespace WellTool.Json.Serialize
{
    public class DefaultJSONObjectSerializer : JSONSerializer
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
                
                if (obj is JSONObject jsonObject)
                {
                    sb.Append("{");
                    
                    var first = true;
                    foreach (var key in jsonObject.Keys)
                    {
                        if (!first)
                        {
                            sb.Append(",");
                        }
                        sb.Append('"').Append(key).Append('"').Append(":");
                        // 递归序列化每个值
                        var valueSerializer = new DefaultJSONSerializer();
                        valueSerializer.Serialize(jsonObject[key], sb);
                        first = false;
                    }
                    
                    sb.Append("}");
                }
                else
                {
                    sb.Append(obj.ToString());
                }
            }
        }
    }
}