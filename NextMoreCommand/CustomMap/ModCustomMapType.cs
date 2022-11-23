using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkySwordKill.NextMoreCommand.CustomMap
{
    public  class ModCustomMapType
    {
        [JsonProperty("ID")]
        public string ID;
        [JsonProperty("CID")]
        public string Cid;
        public virtual string Type { get; set; }
        public JObject RawJson { get; set; }
        public virtual FuBenMap.NodeType NodeType { get; set; }
        public string GetMapType()
        {
            return Type;
        }

        public FuBenMap.NodeType GetMapNodeType()
        {
            return NodeType;
        }
    }
}