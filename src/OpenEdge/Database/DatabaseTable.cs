
using Newtonsoft.Json;

namespace PSOpenEdge.OpenEdge.Database
{
    public class DatabaseTable
    {

        [JsonIgnore]
        public Database Database { get; set; }

        [JsonProperty("schemaName")]
        public string SchemaName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isMultiTenant")]
        public bool IsMultiTenant { get; set; }

        [JsonProperty("isPartitioned")]
        public bool IsPartitioned { get; set; }

        [JsonProperty("isCDCEnabled")]
        public bool IsCDCEnabled { get; set; }

        [JsonProperty("isChangeTable")]
        public bool IsChangeTable { get; set; }

        [JsonProperty("partitionPolicyName")]
        public string PartitionPolicyName { get; set; }

        [JsonProperty("cdcTablePolicyName")]
        public string CdcTablePolicyName { get; set; }

        [JsonProperty("keepDefaultArea")]
        public bool KeepDefaultArea { get; set; }

        [JsonProperty("areaName")]
        public string AreaName { get; set; }

        public override string ToString()
        {
            return $"{this.SchemaName}.{this.Name}";
        }
    }
}