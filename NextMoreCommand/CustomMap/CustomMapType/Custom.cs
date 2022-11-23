using Newtonsoft.Json.Linq;
using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [CustomMapType(nameof(Custom),"自定义")]
    public class Custom:ModCustomMapType
    {
        public override string Type => "Road";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.Road;
        public string CustomType { get; set; }
     
    }
}