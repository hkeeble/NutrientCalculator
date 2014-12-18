using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace FatSecretAPI
{
    /// <summary>
    /// Represents an error passed back from the server.
    /// </summary>
    class Error
    {
        [JsonProperty("code"), JsonConverter(typeof(TypeConverter<int>))]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
