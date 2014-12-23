using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FatSecretAPI
{
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

            // Attempt to parse as both array and token (some lists will only contain a single serving type)
            JArray array = new JArray();
            bool isArray = false;
            try {
                array = JArray.Parse(token.ToString());
                isArray = true;
            }
            catch {
                isArray = false;
            }

            // Determine how to deserialize based on data type
            List<Serving> servingsList = new List<Serving>();
            if(isArray)
                servingsList = JsonConvert.DeserializeObject<List<Serving>>(array.ToString());
            else
                servingsList.Add(JsonConvert.DeserializeObject<Serving>(token.ToString()));

            // Create a dictionary for faster retrieval later on
            Dictionary<string, Serving> dict = new Dictionary<string, Serving>();
            foreach (Serving serving in servingsList)
                dict.Add(serving.ServingID, serving);

            return dict;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Dictionary<string, Serving> servings = value as Dictionary<string, Serving>;

            writer.WriteStartObject();
            writer.WritePropertyName("serving");
            writer.WriteStartArray();
            foreach (KeyValuePair<string, Serving> e in servings)
                serializer.Serialize(writer, e.Value);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }

    /// <summary>
    /// Converts a Json Array into a list, containing items of type T.
    /// </summary>
    /// <typeparam name="T">The type of data in the list.</typeparam>
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
            JToken obj = JToken.Load(reader);
            string content = obj.ToString();
            var token = JToken.Parse(content);

            if (token is JArray)
                return JsonConvert.DeserializeObject<T>((token as JArray).ToString());
            else if (token is JObject)
            {
                JArray array = new JArray();
                foreach (JProperty prop in (token as JObject).Properties())
                {
                    JObject newObj = new JObject();
                    newObj.Add(prop.Name, prop.Value);
                    array.Add(newObj);
                }
                return JsonConvert.DeserializeObject<T>(array.ToString());
            }

            return null;
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
                int val = System.Int32.Parse(reader.Value.ToString());
                return val;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }
    }
}