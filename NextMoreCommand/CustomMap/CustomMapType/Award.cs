using Newtonsoft.Json;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [CustomMapType(nameof(Award),"原版奖励")]
    public class Award:ModCustomMapType
    {
        public override string Type => "Award";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.Award;
        [JsonProperty]
        public int TaskID { get; set; }
    }
}