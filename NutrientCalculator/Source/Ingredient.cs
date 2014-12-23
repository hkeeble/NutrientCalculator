using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using FatSecretAPI;

namespace Assignment
{
    /// <summary>
    /// Represents an individual ingredient in a custom recipe.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CustomIngredient
    {
        [JsonProperty]
        private Food foodItem;
        
        [JsonProperty]
        private Serving serving;

        /// <summary>
        /// Gets the food item stored in this ingredient.
        /// </summary>
        public Food FoodItem { get { return foodItem; } }
        
        /// <summary>
        /// Gets or sets the serving of this ingredient.
        /// </summary>
        public Serving Serving { get { return serving; } set { serving = value; } }

        public CustomIngredient(Food foodItem, Serving serving)
        {
            this.foodItem = foodItem;
            this.serving = serving;
        }
    }
}
