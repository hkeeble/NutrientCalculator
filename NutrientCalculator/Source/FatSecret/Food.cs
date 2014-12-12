using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FatSecretAPI
{
    /// <summary>
    /// Represents an individual serving of a particular type of food. Contains all nutrional value for that food.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    class Serving
    {
        [JsonProperty("serving_id")]
        public string ServingID { get; set; }

        [JsonProperty("serving_description")]
        public string ServingDescription { get; set; }

        [JsonProperty("serving_url")]
        public string ServingURL { get; set; }

        [JsonProperty("metric_serving_amount"), JsonConverter(typeof(TypeConverter<double>))]
        public double MetricServingAmount { get; set; }

        [JsonProperty("metric_serving_unit")]
        public string MetricServingUnit { get; set; }

        [JsonProperty("number_of_units"), JsonConverter(typeof(TypeConverter<double>))]
        public double NumberOfUnits { get; set; }

        [JsonProperty("measurement_description")]
        public string MeasurementDescription { get; set; }

        [JsonProperty("calories"), JsonConverter(typeof(TypeConverter<double>))]
        public double Calories { get; set; }

        [JsonProperty("carbohydrate"), JsonConverter(typeof(TypeConverter<double>))]
        public double Carbohydrate { get; set; }

        [JsonProperty("protein"), JsonConverter(typeof(TypeConverter<double>))]
        public double Protein { get; set; }

        [JsonProperty("fat"), JsonConverter(typeof(TypeConverter<double>))]
        public double Fat { get; set; }

        [JsonProperty("saturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        public double SaturatedFat { get; set; }

        [JsonProperty("polyunsaturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        public double PolyunsaturatedFat { get; set; }

        [JsonProperty("monounsaturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        public double MonounsaturatedFat { get; set; }

        [JsonProperty("trans_fat"), JsonConverter(typeof(TypeConverter<double>))]
        public double TransFat { get; set; }

        [JsonProperty("cholesterol"), JsonConverter(typeof(TypeConverter<double>))]
        public double Cholesterol { get; set; }

        [JsonProperty("sodium"), JsonConverter(typeof(TypeConverter<double>))]
        public double Sodium { get; set; }

        [JsonProperty("potassium"), JsonConverter(typeof(TypeConverter<double>))]
        public double Potassium { get; set; }

        [JsonProperty("fiber"), JsonConverter(typeof(TypeConverter<double>))]
        public double Fiber { get; set; }

        [JsonProperty("sugar"), JsonConverter(typeof(TypeConverter<double>))]
        public double Sugar { get; set; }

        [JsonProperty("vitamin_a"), JsonConverter(typeof(TypeConverter<double>))]
        public double VitaminA { get; set; }

        [JsonProperty("vitamin_c"), JsonConverter(typeof(TypeConverter<double>))]
        public double VitaminC { get; set; }

        [JsonProperty("calcium"), JsonConverter(typeof(TypeConverter<double>))]
        public double Calcium { get; set; }

        [JsonProperty("iron"), JsonConverter(typeof(TypeConverter<double>))]
        public double Iron { get; set; }
    }

    /// <summary>
    /// Represents an individual type of food.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    class Food
    {
        [JsonProperty("food_id")]
        public string ID { get; set; }

        [JsonProperty("food_name")]
        public string Name { get; set; }

        [JsonProperty("food_type")]
        public string Type { get; set; }

        [JsonProperty("brand_name")]
        public string Brand { get; set; }

        [JsonProperty("food_url")]
        public string URL { get; set; }

        [JsonProperty("servings"), JsonConverter(typeof(ServingListConverter))]
        public Dictionary<string, Serving> servings;

        /// <summary>
        /// Retrieve a serving type by ID.
        /// </summary>
        /// <param name="id">ID of the serving to search for.</param>
        /// <returns>Returns the serving object found. If not found, returns null.</returns>
        public Serving GetServingByID(string id)
        {
            Serving serving = new Serving();
            if (servings.TryGetValue(id, out serving))
                return serving;
            else
                return null;
        }

        /// <summary>
        /// Retrieve a serving type by description.
        /// </summary>
        /// <param name="description">Description of the serving to search for.</param>
        /// <returns>Returns the serving object found. If not found, returns null.</returns>
        public Serving GetServingByDescription(string description)
        {
            foreach (KeyValuePair<string, Serving> pair in servings)
            {
                if (pair.Value.ServingDescription == description)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieve a serving type by measurement.
        /// </summary>
        /// <param name="description">Measurement of the serving to search for.</param>
        /// <returns>Returns the serving object found. If not found, returns null.</returns>
        public Serving GetServingByMeasurement(string measurement)
        {
            foreach (KeyValuePair<string, Serving> pair in servings)
            {
                if (pair.Value.MeasurementDescription == measurement)
                {
                    return pair.Value;
                }
            }

            return null;
        }
    }
}