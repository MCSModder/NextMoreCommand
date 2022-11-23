using Newtonsoft.Json;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [CustomMapType(nameof(Event),"原版事件")]
    public class Event:ModCustomMapType
    {
        public override string Type => "Event";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.Event;
        [JsonProperty]
        public int TaskID { get; set; }
    }
}