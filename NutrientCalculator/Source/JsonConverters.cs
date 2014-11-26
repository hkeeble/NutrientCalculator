using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// A list converter. Used to convert a specific token into a list.
/// </summary>
public class ServingListConverter : JsonConverter
{
    public ServingListConverter()
    {

    }

    public override bool CanConvert(System.Type type)
    {
        return (type == typeof(Food));
    }

    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        List<JObject> objects = new List<JObject>();

        JToken token = jo.SelectToken("serving");
        JArray array = JArray.Parse(token.ToString());

        List<Serving> servingsList = JsonConvert.DeserializeObject<List<Serving>>(array.ToString());

        Dictionary<string, Serving> dict = new Dictionary<string, Serving>();

        foreach(Serving serving in servingsList)
        {
            dict.Add(serving.ServingID, serving);
        }

        return dict;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new System.NotImplementedException();
    }
}

public class ListConverter<T> : JsonConverter
{
    public ListConverter()
    {

    }

    public override bool CanConvert(System.Type type)
    {
        return (type == typeof(Food));
    }

    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
    {
        JArray array = JArray.ReadFrom(reader).Value<JArray>();
        return JsonConvert.DeserializeObject<T>(array.ToString());
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// Converts a value from string to double for deserialization only.
/// </summary>
public class TypeConverter<T> : JsonConverter
{
    public override bool CanConvert(System.Type objectType)
    {
        return objectType == typeof(string);
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (typeof(T) == typeof(double))
        {
            double val = System.Double.Parse(reader.Value.ToString());
            return val;
        }

        if (typeof(T) == typeof(int))
        {
            double val = System.Int32.Parse(reader.Value.ToString());
            return val;
        }

        return null;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new System.NotImplementedException();
    }
}