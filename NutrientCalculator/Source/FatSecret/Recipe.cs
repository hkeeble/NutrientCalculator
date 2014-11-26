using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Represents an individual recipe.
/// </summary>
class Recipe
{
    [JsonProperty("food_description")]
    public string ID { get; set; }

    [JsonProperty("food_description")]
    public string Name { get; set; }

    [JsonProperty("food_description")]
    public string URL { get; set; }

    [JsonProperty("food_description")]
    public string Description { get; set; }

    [JsonProperty("food_description")]
    public double NumberOfServings { get; set; }

    [JsonProperty("food_description")]
    public int PreperationTime { get; set; }

    [JsonProperty("food_description")]
    public int CookingTime { get; set; }

    [JsonProperty("food_description")]
    public int Rating { get; set; }
}