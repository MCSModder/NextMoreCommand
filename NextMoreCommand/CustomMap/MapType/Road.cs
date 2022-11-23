using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.MapType
{
    [MapType(nameof(Road),"路")]
    public class Road:ModCustomMapType
    {
        public override string Type => "Road";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.Road;
    }
}