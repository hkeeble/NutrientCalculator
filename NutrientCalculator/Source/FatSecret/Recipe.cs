using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FatSecretAPI
{
    [JsonObject(MemberSerialization.OptOut)]
    class Direction
    {
        [JsonProperty("direction_description")]
        public string description { get; set; }

        [JsonProperty("direction_number"), JsonConverter(typeof(TypeConverter<int>))]
        public int number { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    class Ingredient
    {
        [JsonProperty("food_id")]
        public string ID { get; set; }

        [JsonProperty("food_name")]
        public string Name { get; set; }

        [JsonProperty("ingredient_description")]
        public string Description { get; set; }

        [JsonProperty("ingredient_url")]
        public string URL { get; set; }

        [JsonProperty("measurement_description")]
        public string MeasurementDescription { get; set; }

        [JsonProperty("number_of_units"), JsonConverter(typeof(TypeConverter<double>))]
        public double NumberOfUnits { get; set; }

        [JsonProperty("serving_id")]
        public string ServingID { get; set; }
    }

    class RecipeCategory
    {
        [JsonProperty("recipe_category_name")]
        public string Name { get; set; }

        [JsonProperty("recipe_category_url")]
        public string URL { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    class RecipeImage
    {
        [JsonProperty("recipe_image")]
        public string URL { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    class RecipeType
    {
        [JsonProperty("recipe_type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents an individual recipe.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    class Recipe
    {
        [JsonProperty("recipe_id")]
        public string ID { get; set; }

        [JsonProperty("recipe_name")]
        public string Name { get; set; }

        [JsonProperty("recipe_url")]
        public string URL { get; set; }

        [JsonProperty("recipe_description", Required = Required.AllowNull)]
        public string Description { get; set; }

        [JsonProperty("number_of_servings", Required = Required.AllowNull), JsonConverter(typeof(TypeConverter<double>))]
        public double NumberOfServings { get; set; }

        [JsonProperty("preparation_time_min", Required = Required.AllowNull), JsonConverter(typeof(TypeConverter<int>))]
        public int PreperationTime { get; set; }

        [JsonProperty("cooking_time_min", Required = Required.AllowNull), JsonConverter(typeof(TypeConverter<int>))]
        public int CookingTime { get; set; }

        [JsonProperty("rating", Required = Required.AllowNull), JsonConverter(typeof(TypeConverter<int>))]
        public int Rating { get; set; }

        [JsonProperty("directions.direction"), JsonConverter(typeof(ListConverter<List<Direction>>))]
        public List<Direction> Directions;

        [JsonProperty("ingredients.ingredient"), JsonConverter(typeof(ListConverter<List<Ingredient>>))]
        public List<Ingredient> Ingredients;

        [JsonProperty("recipe_categories.recipe_category"), JsonConverter(typeof(ListConverter<List<RecipeCategory>>))]
        public List<RecipeCategory> Categories;

        //[JsonProperty("recipe_images"), JsonConverter(typeof(ListConverter<List<RecipeImage>>))]
        //public List<RecipeImage> Images;

        [JsonProperty("recipe_types"), JsonConverter(typeof(ListConverter<List<RecipeType>>))]
        public List<RecipeType> Types;

        [JsonProperty("serving_sizes"), JsonConverter(typeof(ListConverter<List<Serving>>))]
        public List<Serving> ServingSizes;
    }
}