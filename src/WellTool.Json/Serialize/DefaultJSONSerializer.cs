using System.Text;

namespace WellTool.Json.Serialize
{
    public class DefaultJSONSerializer : JSONSerializer
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
                    var arraySerializer = new DefaultJSONArraySerializer();
                    arraySerializer.Serialize(jsonArray, sb);
                }
                else if (obj is JSONObject jsonObject)
                {
                    var objectSerializer = new DefaultJSONObjectSerializer();
                    objectSerializer.Serialize(jsonObject, sb);
                }
                else if (obj is string str)
                {
                    sb.Append('"').Append(str).Append('"');
                }
                else if (obj is bool b)
                {
                    sb.Append(b ? "true" : "false");
                }
                else
                {
                    sb.Append(obj.ToString());
                }
            }
        }
    }
}