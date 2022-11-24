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
        public virtual MapNodeType NodeType { get; set; } = MapNodeType.Road;
        public Block Block;
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