namespace WellTool.Json.Serialize
{
    public interface JSONDeserializer
    {
        T Deserialize<T>(string json);
    }
}