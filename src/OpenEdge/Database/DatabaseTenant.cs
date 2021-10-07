
using Newtonsoft.Json;

namespace PSOpenEdge.OpenEdge.Database
{
    public class DatabaseTenant
    {
        [JsonIgnore]
        public Database Database { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id{get;set;}

        [JsonProperty("externalId")]
        public string ExternalId{get;set;}

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isDataEnabled")]
        public bool IsDataEnabled { get; set; }

        [JsonProperty("isAllocated")]
        public bool IsAllocated { get; set; }

        [JsonProperty("defaultDataAreaName")]
        public string DefaultDataAreaName { get; set; }

        [JsonProperty("defaultIndexAreaName")]
        public string DefaultIndexAreaName { get; set; }

        [JsonProperty("defaultLobAreaName")]
        public string DefaultLobAreaName { get; set; }

        [JsonProperty("defaultAllocation")]
        public string DefaultAllocation { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}