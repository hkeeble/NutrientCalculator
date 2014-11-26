using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Represents a search result.
/// </summary>
[JsonObject(MemberSerialization.OptOut)]
abstract class SearchResult
{
    [JsonProperty("max_results")]
    public int MaxResults { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }

    [JsonProperty("page_number")]
    public int PageNumber { get; set; }
}