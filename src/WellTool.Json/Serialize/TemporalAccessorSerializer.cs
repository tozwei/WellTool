using System;

namespace WellTool.Json.Serialize;

/// <summary>
/// 日期时间类型的JSON自定义序列化实现
/// </summary>
/// <typeparam name="T">日期时间类型</typeparam>
public class TemporalAccessorSerializer<T> where T : struct
{
    private const string YearKey = "year";
    private const string MonthKey = "month";
    private const string DayKey = "day";
    private const string HourKey = "hour";
    private const string MinuteKey = "minute";
    private const string SecondKey = "second";
    private const string NanoKey = "nano";

    private readonly Type _temporalAccessorType;

    public TemporalAccessorSerializer()
    {
        _temporalAccessorType = typeof(T);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="json">JSON对象</param>
    /// <param name="bean">日期时间对象</param>
    public void Serialize(JSONObject json, T bean)
    {
        if (typeof(T) == typeof(DateTime))
        {
            var dateTime = (DateTime)(object)bean;
            json.Set(YearKey, dateTime.Year);
            json.Set(MonthKey, dateTime.Month);
            json.Set(DayKey, dateTime.Day);
            json.Set(HourKey, dateTime.Hour);
            json.Set(MinuteKey, dateTime.Minute);
            json.Set(SecondKey, dateTime.Second);
            json.Set(NanoKey, dateTime.Millisecond * 1000000);
        }
        else
        {
            throw new JSONException($"Unsupported type to JSON: {_temporalAccessorType.Name}");
        }
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="json">JSON对象</param>
    /// <returns>日期时间对象</returns>
    public T Deserialize(JSONObject json)
    {
        if (typeof(T) == typeof(DateTime))
        {
            var year = json.GetInt(YearKey);
            var month = json.GetInt(MonthKey);
            var day = json.GetInt(DayKey);
            if (day == 0)
            {
                day = json.GetInt("dayOfMonth");
                if (day == 0)
                {
                    throw new JSONException("Field 'day' or 'dayOfMonth' must be not null");
                }
            }
            var hour = json.GetInt(HourKey);
            var minute = json.GetInt(MinuteKey);
            var second = json.GetInt(SecondKey);
            var nano = json.GetInt(NanoKey);
            var millisecond = nano / 1000000;
            return (T)(object)new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        throw new JSONException($"Unsupported type from JSON: {_temporalAccessorType.Name}");
    }
}
