using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assignment
{
    /// <summary>
    /// A tracker class, used to track the user's daily intake.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DailyTracker
    {
        // The start of this day
        [JsonProperty, JsonConverter(typeof(IsoDateTimeConverter))]
        private DateTime startTime;

        // The end of this day
        [JsonProperty, JsonConverter(typeof(IsoDateTimeConverter))]
        private DateTime endTime;

        [JsonProperty]
        private NutrientData nutrients;

        [JsonProperty]
        private List<CustomRecipe> meals;

        public NutrientData Nutrients { get { return nutrients; } set { nutrients = value; } }
        public DateTime StartTime { get { return startTime; } }
        public DateTime EndTime { get { return endTime; } }
        public List<CustomRecipe> Meals { get { return meals; } }

        /// <summary>
        /// Begin a new daily tracker, setting start and end times+
        /// </summary>
        public DailyTracker()
        {
            nutrients = new NutrientData();

            startTime = DateTime.Now;
            endTime = startTime.AddDays(1);
            meals = new List<CustomRecipe>();
        }

        /// <summary>
        /// Adds a meal to today, indicating the user has eaten this today.
        /// </summary>
        /// <param name="recipe">The recipe for the meal the user has eaten.</param>
        public void AddMeal(CustomRecipe recipe)
        {
            // Append the recipe's nutrients
            nutrients += recipe.Nutrients;

            // Add meal to list
            meals.Add(recipe);
        }
    }
}
