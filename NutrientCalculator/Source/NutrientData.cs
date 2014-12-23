using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FatSecretAPI;
using Newtonsoft.Json;

namespace Assignment
{
    /// <summary>
    /// Represents a set of nutrient data.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class NutrientData
    {
        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double calories;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double protein;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double carbohydrate;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double sugars;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double fat;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double saturates;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double fiber;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double salt;

        public NutrientData()
        {
            calories = 0;
            protein = 0;
            sugars = 0;
            fat = 0;
            saturates = 0;
            fiber = 0;
            salt = 0;
        }

        public double Calories      { get { return calories; } set { calories = value; } }
        public double Protein       { get { return protein; } set { protein = value; } }
        public double Carbohydrate  { get { return carbohydrate; } set { carbohydrate = value; } }
        public double Sugars        { get { return sugars; } set { sugars = value; } }
        public double Fat           { get { return fat; } set { fat = value; } }
        public double Saturates     { get { return saturates; } set { saturates = value; } }
        public double Fiber         { get { return fiber; } set { fiber = value; } }
        public double Salt          { get { return salt; } set { salt = value; } }

        /// <summary>
        /// Builds a nutrient data collection from the given diet specificaiton.
        /// </summary>
        /// <param name="specification">Specification to use.</param>
        /// <param name="useMax">Use the maximum or minimum values.</param>
        public static NutrientData CreateFromSpecification(DietSpecification specification, bool useMax)
        {
            NutrientData newData = new NutrientData();

            if(useMax)
            {
                newData.calories = specification.Calories.Maximum;
                newData.protein = specification.Protein.Maximum;
                newData.sugars = specification.Sugars.Maximum;
                newData.fat = specification.Fat.Maximum;
                newData.saturates = specification.Saturates.Maximum;
                newData.fiber = specification.Fibre.Maximum;
                newData.salt = specification.Salt.Maximum;
            }
            else
            {
                newData.calories = specification.Calories.Minimum;
                newData.protein = specification.Protein.Minimum;
                newData.sugars = specification.Sugars.Minimum;
                newData.fat = specification.Fat.Minimum;
                newData.saturates = specification.Saturates.Minimum;
                newData.fiber = specification.Fibre.Minimum;
                newData.salt = specification.Salt.Minimum;
            }

            return newData;
        }

        public static NutrientData operator+(NutrientData a, NutrientData b)
        {
            NutrientData total = new NutrientData();

            total.calories = a.calories + b.calories;
            total.protein = a.protein + b.protein;
            total.sugars = a.sugars + b.sugars;
            total.fat = a.fat + b.fat;
            total.saturates = a.saturates + b.saturates;
            total.fiber = a.fiber + b.fiber;
            total.salt = a.salt + b.salt;

            return total;
        }

        public static NutrientData operator -(NutrientData a, NutrientData b)
        {
            NutrientData total = new NutrientData();

            total.calories = a.calories - b.calories;
            total.protein = a.protein - b.protein;
            total.sugars = a.sugars - b.sugars;
            total.fat = a.fat - b.fat;
            total.saturates = a.saturates - b.saturates;
            total.fiber = a.fiber - b.fiber;
            total.salt = a.salt - b.salt;

            return total;
        }

        /// <summary>
        /// Converts all values to absolute, good for checking differences.
        /// </summary>
        public NutrientData Absolute()
        {
            NutrientData newData = new NutrientData();

            newData.calories = Math.Abs(calories);
            newData.protein = Math.Abs(protein);
            newData.sugars = Math.Abs(sugars);
            newData.fat = Math.Abs(fat);
            newData.saturates = Math.Abs(saturates);
            newData.fiber = Math.Abs(fiber);
            newData.salt = Math.Abs(salt);

            return newData;
        }
    }
}
