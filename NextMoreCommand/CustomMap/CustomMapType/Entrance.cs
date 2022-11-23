using SkySwordKill.NextMoreCommand.Attribute;

namespace SkySwordKill.NextMoreCommand.CustomMap.CustomMapType
{
    [MapType(nameof(Entrance),"入口")]
    public class Entrance:ModCustomMapType
    {
        public override string Type => "Exit";
        public override FuBenMap.NodeType NodeType => FuBenMap.NodeType.Entrance;
       
    }
}