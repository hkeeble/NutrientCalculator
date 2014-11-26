using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Represents an individual result in a food search.
/// </summary>
[JsonObject(MemberSerialization.OptOut)]
class FoodSearchResult
{
    [JsonProperty("food_id")]
    public string ID { get; set; }

    [JsonProperty("food_name")]
    public string Name { get; set; }

    [JsonProperty("food_type")]
    public string Type { get; set; }

    [JsonProperty("brand_name")]
    public string BrandName { get; set; }

    [JsonProperty("food_url")]
    public string URL { get; set; }

    [JsonProperty("food_description")]
    public string Description { get; set; }
}

/// <summary>
/// Represents the results of a food search.
/// </summary>
[JsonObject(MemberSerialization.OptOut)]
class FoodSearchResults : SearchResult
{
    [JsonProperty("food"), JsonConverter(typeof(ListConverter<List<FoodSearchResult>>))]
    List<FoodSearchResult> Results;
}