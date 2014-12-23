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
    /// Represents a custom user recipe.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CustomRecipe
    {
        [JsonProperty]
        private string name;

        [JsonProperty]
        private List<CustomIngredient> ingredients;

        [JsonProperty]
        NutrientData nutrients;

        // public accessors
        public string Name { get { return name; } set { name = value; } }
        public List<CustomIngredient> Ingredients { get { return ingredients; } }
        public NutrientData Nutrients { get { return nutrients; } }

        public CustomRecipe(string name, List<CustomIngredient> ingredients)
        {
            this.name = name;
            this.ingredients = ingredients;
            nutrients = new NutrientData();
            CalculateNutrients();
        }

        public CustomRecipe()
        {
            ingredients = new List<CustomIngredient>();
            nutrients = new NutrientData();
        }

        public CustomRecipe(string name) : this()
        {
            this.name = name;
        }

        public void AddIngredient(CustomIngredient ingredient)
        {
            ingredients.Add(ingredient);
            nutrients.Calories += ingredient.Serving.Calories;
            nutrients.Protein += ingredient.Serving.Protein;
            nutrients.Carbohydrate += ingredient.Serving.Carbohydrate;
            nutrients.Sugars += ingredient.Serving.Sugar;
            nutrients.Fat += ingredient.Serving.Fat;
            nutrients.Saturates += ingredient.Serving.SaturatedFat;
            nutrients.Fiber += ingredient.Serving.Fiber;
            nutrients.Salt += ingredient.Serving.Sodium;
        }

        public void RemoveIngredient(CustomIngredient ingredient)
        {
            ingredients.Remove(ingredient);
            nutrients.Calories -= ingredient.Serving.Calories;
            nutrients.Protein -= ingredient.Serving.Protein;
            nutrients.Carbohydrate -= ingredient.Serving.Carbohydrate;
            nutrients.Sugars -= ingredient.Serving.Sugar;
            nutrients.Fat -= ingredient.Serving.Fat;
            nutrients.Saturates -= ingredient.Serving.SaturatedFat;
            nutrients.Fiber -= ingredient.Serving.Fiber;
            nutrients.Salt -= ingredient.Serving.Sodium;
        }

        public void CalculateNutrients()
        {
            nutrients.Calories = 0;
            nutrients.Protein = 0;
            nutrients.Carbohydrate = 0;
            nutrients.Sugars = 0;
            nutrients.Fat = 0;
            nutrients.Saturates = 0;
            nutrients.Fiber = 0;
            nutrients.Salt = 0;

            foreach (CustomIngredient i in ingredients)
            {
                nutrients.Calories += i.Serving.Calories;
                nutrients.Protein += i.Serving.Protein;
                nutrients.Carbohydrate += i.Serving.Carbohydrate;
                nutrients.Sugars += i.Serving.Sugar;
                nutrients.Fat += i.Serving.Fat;
                nutrients.Saturates += i.Serving.SaturatedFat;
                nutrients.Fiber += i.Serving.Fiber;
                nutrients.Salt += i.Serving.Sodium;
            }
        }

        public void clear()
        {
            ingredients.Clear();
        }
    }
}
