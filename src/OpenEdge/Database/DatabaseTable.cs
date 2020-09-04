
using Newtonsoft.Json;

namespace PSOpenEdge.OpenEdge.Model
{
    public class DatabaseTable
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isMultiTenant")]
        public bool IsMultiTenant { get; set; }
    }

}