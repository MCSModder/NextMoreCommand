using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.CustomMap
{
    public class ModCustomMapType
    {
        [JsonProperty("ID")] public string ID;
        [JsonProperty("CID")] public string Cid;
        [JsonProperty] public virtual string Type { get; set; }
        [JsonIgnore] public JObject RawJson { get; set; }
        [JsonProperty] public virtual MapNodeType NodeType { get; set; } = MapNodeType.Road;
        [JsonProperty] public virtual string BlockName { get; set; } = "";

        public string GetMapType()
        {
            return Type;
        }

        public MapNodeType GetMapNodeType()
        {
            return NodeType;
        }

        public virtual void Execute()
        {
        }
    }
}