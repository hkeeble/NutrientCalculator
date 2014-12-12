using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FatSecretAPI
{
    /// <summary>
    /// Represents an individual result in a recipe search.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    class RecipeSearchResult
    {
        [JsonProperty("recipe_id")]
        public int ID { get; set; }

        [JsonProperty("recipe_name")]
        public string Name { get; set; }

        [JsonProperty("recipe_url")]
        public string URL { get; set; }

        [JsonProperty("recipe_description")]
        public string Description { get; set; }

        [JsonProperty("recipe_image")]
        public string ImageURL { get; set; }
    }

    /// <summary>
    /// Represents the results of a recipe search.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    class RecipeSearchResults : SearchResult
    {
        [JsonProperty("recipe"), JsonConverter(typeof(ListConverter<List<RecipeSearchResult>>))]
        List<RecipeSearchResult> Results;
    }
}
