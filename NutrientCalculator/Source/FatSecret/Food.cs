using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FatSecretAPI
{
    /// <summary>
    /// Represents an individual serving of a particular type of food. Contains all nutrional value for that food.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Serving
    {
        [JsonProperty("serving_id")]
        private string servingID;

        [JsonProperty("serving_description")]
        private string servingDescription;

        [JsonProperty("metric_serving_unit")]
        private string metricServingUnit;

        [JsonProperty("measurement_description")]
        private string measurementDescription;

        [JsonProperty("serving_url")]
        private string servingURL;

        [JsonProperty("metric_serving_amount"), JsonConverter(typeof(TypeConverter<double>))]
        private double metricServingAmount;

        [JsonProperty("number_of_units"), JsonConverter(typeof(TypeConverter<double>))]
        private double numberOfUnits;

        [JsonProperty("calories"), JsonConverter(typeof(TypeConverter<double>))]
        private double calories;

        [JsonProperty("carbohydrate"), JsonConverter(typeof(TypeConverter<double>))]
        private double carbohydrate;

        [JsonProperty("protein"), JsonConverter(typeof(TypeConverter<double>))]
        private double protein;

        [JsonProperty("fat"), JsonConverter(typeof(TypeConverter<double>))]
        private double fat;

        [JsonProperty("saturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        private double saturatedFat;

        [JsonProperty("polyunsaturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        private double polyunsaturatedFat;

        [JsonProperty("monounsaturated_fat"), JsonConverter(typeof(TypeConverter<double>))]
        private double monounsaturatedFat;

        [JsonProperty("trans_fat"), JsonConverter(typeof(TypeConverter<double>))]
        private double transFat;

        [JsonProperty("cholesterol"), JsonConverter(typeof(TypeConverter<double>))]
        private double cholesterol;

        [JsonProperty("sodium"), JsonConverter(typeof(TypeConverter<double>))]
        private double sodium;

        [JsonProperty("potassium"), JsonConverter(typeof(TypeConverter<double>))]
        private double potassium;

        [JsonProperty("fiber"), JsonConverter(typeof(TypeConverter<double>))]
        private double fiber;

        [JsonProperty("sugar"), JsonConverter(typeof(TypeConverter<double>))]
        private double sugar;

        [JsonProperty("vitamin_a"), JsonConverter(typeof(TypeConverter<double>))]
        private double vitaminA;

        [JsonProperty("vitamin_c"), JsonConverter(typeof(TypeConverter<double>))]
        private double vitaminC;

        [JsonProperty("calcium"), JsonConverter(typeof(TypeConverter<double>))]
        private double calcium;

        [JsonProperty("iron"), JsonConverter(typeof(TypeConverter<double>))]
        private double iron;

        // Public accessors
        public string ServingID                 { get { return servingID; } }
        public string ServingDescription        { get { return servingDescription; } }
        public string ServingURL                { get { return servingURL; } }
        public double MetricServingAmount       { get { return metricServingAmount; } }
        public string MetricServingUnit         { get { return metricServingUnit; } }
        public double NumberOfUnits             { get { return numberOfUnits; } }
        public string MeasurementDescription    { get { return measurementDescription; } }
        public double Calories                  { get { return calories; } }
        public double Carbohydrate              { get { return carbohydrate; } }
        public double Protein                   { get { return protein; } }
        public double Fat                       { get { return fat; } }
        public double SaturatedFat              { get { return saturatedFat; } }
        public double PolyunsaturatedFat        { get { return polyunsaturatedFat; } }
        public double MonounsaturatedFat        { get { return monounsaturatedFat; } }
        public double TransFat                  { get { return transFat; } }
        public double Cholesterol               { get { return cholesterol; } }
        public double Sodium                    { get { return sodium; } }
        public double Potassium                 { get { return potassium; } }
        public double Fiber                     { get { return fiber; } }
        public double Sugar                     { get { return sugar; } }
        public double VitaminA                  { get { return vitaminA; } }
        public double VitaminC                  { get { return vitaminC; } }
        public double Calcium                   { get { return calcium; } }
        public double Iron                      { get { return iron; } }

        /// <summary>
        /// Creates a version of this serving with 1 metric unit, nutrients are adjusted accordingly.
        /// </summary>
        /// <returns></returns>
        public Serving CreateBase()
        {
            Serving serving = new Serving();

            double original = MetricServingAmount;

            if (original != 0)
            {
                serving.metricServingAmount = 1.0;
                serving.calories = Calories / original;
                serving.carbohydrate = Carbohydrate / original;
                serving.protein = Protein / original;
                serving.fat = Fat / original;
                serving.saturatedFat = SaturatedFat / original;
                serving.polyunsaturatedFat = PolyunsaturatedFat / original;
                serving.monounsaturatedFat = MonounsaturatedFat / original;
                serving.transFat = TransFat / original;
                serving.cholesterol = Cholesterol / original;
                serving.sodium = Sodium / original;
                serving.potassium = Potassium / original;
                serving.fiber = Fiber / original;
                serving.sugar = Sugar / original;
                serving.vitaminA = VitaminA / original;
                serving.vitaminC = VitaminC / original;
                serving.calcium = Calcium / original;
                serving.iron = Iron / original;

                serving.metricServingUnit = MetricServingUnit;
                serving.servingDescription = "1" + MetricServingUnit;
            }
            else // If there is no metric serving amount, use the serving measurement description
            {
                serving.metricServingAmount = 1.0;
                serving.calories = Calories;
                serving.carbohydrate = Carbohydrate;
                serving.protein = Protein;
                serving.fat = Fat;
                serving.saturatedFat = SaturatedFat;
                serving.polyunsaturatedFat = PolyunsaturatedFat;
                serving.monounsaturatedFat = MonounsaturatedFat;
                serving.transFat = TransFat;
                serving.cholesterol = Cholesterol;
                serving.sodium = Sodium;
                serving.potassium = Potassium;
                serving.fiber = Fiber;
                serving.sugar = Sugar;
                serving.vitaminA = VitaminA;
                serving.vitaminC = VitaminC;
                serving.calcium = Calcium;
                serving.iron = Iron;

                serving.metricServingUnit = " x " + ServingDescription;
                serving.servingDescription = "1" + MetricServingUnit;
            }

            return serving;
        }

        /// <summary>
        /// Creates a multiple of this serving, multiplying all nutrients by the given unit.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Serving CreateMultiple(double amount)
        {
            Serving serving = new Serving();

            serving.metricServingAmount = amount;
 
            serving.calories = Calories * amount;
            serving.carbohydrate = Carbohydrate * amount;
            serving.protein = Protein * amount;
            serving.fat = Fat * amount;
            serving.saturatedFat = SaturatedFat * amount;
            serving.polyunsaturatedFat = PolyunsaturatedFat * amount;
            serving.monounsaturatedFat = MonounsaturatedFat * amount;
            serving.transFat = TransFat * amount;
            serving.cholesterol = Cholesterol * amount;
            serving.sodium = Sodium * amount;
            serving.potassium = Potassium * amount;
            serving.fiber = Fiber * amount;
            serving.sugar = Sugar * amount;
            serving.vitaminA = VitaminA * amount;
            serving.vitaminC = VitaminC * amount;
            serving.calcium = Calcium * amount;
            serving.iron = Iron * amount;

            serving.metricServingUnit = MetricServingUnit;
            serving.servingDescription = Convert.ToString(amount) + serving.MetricServingUnit;

            return serving;
        }
    }

    /// <summary>
    /// Represents an individual type of food.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Food
    {
        [JsonProperty("food_id")]
        private string id;

        [JsonProperty("food_name")]
        private string name;

        [JsonProperty("food_type")]
        private string type;

        [JsonProperty("brand_name")]
        private string brand;

        [JsonProperty("food_url")]
        private string url;

        [JsonProperty("servings"), JsonConverter(typeof(ServingListConverter))]
        private Dictionary<string, Serving> servings;

        // Public accessors
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Type { get { return type; } }
        public string Brand { get { return brand; } }
        public string URL { get { return url; } }
        public Dictionary<string, Serving> Servings { get { return servings; } }

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